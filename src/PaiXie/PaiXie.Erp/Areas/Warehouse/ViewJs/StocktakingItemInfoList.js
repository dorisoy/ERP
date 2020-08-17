//初始化
$(function () {
    search(1);

    //刷新
    $('#refresh').click(function () {
        search(1);
    });

    //搜索
    $('#btnSearch').click(function () {
        search(1);
    });

    //清空条件
    $('#btnReset').click(function () {
        window.location.reload(true);
    });

    //导出
    $('#export').click(function () {
        var queryData = {
            keyWordType: $("#keyWordType").combobox('getValue'),
            keyWord: $("#txtKeyWord").val(),
            stocktakingID: $("#hdnStocktakingID").val()
        };
        var options = {
            url: '/Warehouse/StocktakingItem/Export',
            data: queryData,
            title: '导出盘点明细',
            fileTitle: '盘点明细'
        };
        $.AjaxExport(options);
    });

    //导入
    $('#import').click(function () {
        var stocktakingID = $("#hdnStocktakingID").val();
        showLocalWindow("导入盘点结果", "/Warehouse/StocktakingItem/Import?stocktakingID=" + stocktakingID, 400, 180, true, false, false);
    });

    showControl();
});

//加载列表
function initTable(queryData, pageNumber) {
    $('#grid').datagrid({
        url: '/Warehouse/StocktakingItem/Search?ram=' + Math.random(),
        height: '100%',
        width: '100%',
        nowrap: false,
        autoRowHeight: false,
        striped: true,
        collapsible: true,
        pagination: true,
        fitColumns: true,
        pageNumber: pageNumber,
        fit: true, //datagrid自适应宽度
        fitColumn: false, //列自适应宽度
        border: false,
        pageSize: 15,
        pageList: [15, 20, 30],
        rownumbers: false,
        queryParams: queryData,  //异步查询的参数
        columns: [[
			{ field: 'ck', width: 52, checkbox: true },   //选择
			{ title: '主键', field: 'ID', hidden: true },
			{ title: '库位编码', field: 'LocationCode', width: 150, align: 'center', sortable: false },
			{ title: '商品编码', field: 'ProductsCode', width: 150, align: 'center', sortable: false },
			{ title: '商品名称', field: 'ProductsName', width: 350, align: 'center', sortable: false },
			{ title: '属性', field: 'ProductsSkuSaleprop', width: 150, align: 'center', sortable: false },
			{ title: 'SKU码', field: 'ProductsSkuCode', width: 150, align: 'center', sortable: false },
			{ title: '批次', field: 'ProductsBatchCode', width: 150, align: 'center', sortable: false },
			{ title: '系统库存', field: 'ZkNum', width: 150, align: 'center', sortable: false },
			{ title: '盘点数量', field: 'PdNum', width: 150, align: 'center', formatter: function (value, row, index) { if (row.IsImport == 0) { return '-' } else { return row.PdNum } }, sortable: false },
			{
			    title: '盈亏', field: 'ProfitAndLoss', width: 150, align: 'center',
			    formatter: function (value, row, index) {
			        if (row.IsImport == 0)
			        {
			            return "-";
			        }
			        else {
			            if(row.PdNum < row.ZyNum + row.DjNum)
			            {
			                return '<a href="javascript:void(0)" onclick="showAbnormalInfo(' + row.ID + ')" style="color:red;">异常</a>';
			            }
			            else if (row.PdNum - row.ZkNum != 0)
			            {
			                return "<span style='color:red'>" + (row.PdNum - row.ZkNum) + "</span>";
			            }
			            else
			            {
			                return "0";
			            }
			        }
			    },
			    sortable: false
			}
        ]],
        onLoadSuccess: function (data) {
            DataGridNoData(this);
            GetProfitAndLossCount();
            GetAbnormalCount();
        }
    });
}

//搜索
function search(pageNumber) {
    //得到用户输入的参数
    var queryData = {
        keyWordType: $("#keyWordType").combobox('getValue'),
        keyWord: $("#txtKeyWord").val(),
        stocktakingID: $("#hdnStocktakingID").val()
    }
    //将值传递给
    initTable(queryData, pageNumber);
}

//获取盈亏数量
function GetProfitAndLossCount() {
    $.ajax({
        type: "GET",
        async: false,
        cache: false,
        url: "/Warehouse/StocktakingItem/GetProfitAndLossCount",
        data: { stocktakingID: $("#hdnStocktakingID").val(), isProfitAndLoss: 1 },
        dataType: "json",
        success: function (data) {
            $("#lblProfitAndLossCount").text(data);
        }
    });
}

//获取异常数量
function GetAbnormalCount() {
    $.ajax({
        type: "GET",
        async: false,
        cache: false,
        url: "/Warehouse/StocktakingItem/GetAbnormalCount",
        data: { stocktakingID: $("#hdnStocktakingID").val(), isAbnormal: 1 },
        dataType: "json",
        success: function (data) {
            $("#lblAbnormalCount").text(data);
        }
    });
}

//查询盈亏订单
function searchProfitAndLossOrder(pageNumber) {
    $("#txtKeyWord").val('');
    var queryData = {
        stocktakingID: $("#hdnStocktakingID").val(),
        isProfitAndLoss: 1
    }

    //将值传递给
    initTable(queryData, pageNumber);
}

//查询异常订单
function searchAbnormalOrder(pageNumber) {
    $("#txtKeyWord").val('');
    var queryData = {
        stocktakingID: $("#hdnStocktakingID").val(),
        isAbnormal: 1
    }

    //将值传递给
    initTable(queryData, pageNumber);
}

//异常原因
function showAbnormalInfo(stocktakingItemID) {
    showLocalWindow("异常原因", "/Warehouse/StocktakingItem/showAbnormalInfo?stocktakingItemID=" + stocktakingItemID, 600, 380, true, false, false);
}

//按钮可用控制
function showControl() {
    var status = $("#hdnStatus").val();
    if (status != "0") {
        $("#export").hide();
        $("#import").hide();
    }
}