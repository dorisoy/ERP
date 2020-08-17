//初始化
$(function () {
    //绑定物流公司下拉列表
    BindDictItem("Logistics", "Logistics");
    $('#Logistics').combobox("setValue", '0');
    //绑定仓库下拉列表
    BindDictItem("Warehouse", "Warehouse");
    $("#Warehouse").combobox('setValue', '0');
    //刷新
    $('#refresh').click(function () {
        bindSerarchLickEvent(1);
    });
    //导出
    $('#export').click(function () {
        var ids = [];
        var rows = $("#grid").datagrid("getSelections");
        for (var i = 0; i < rows.length; i++) {
            ids.push(rows[i].ID);
        }
        var queryData = {
            ids: ids.join(","),
            keyWordType: $("#keyWordType").combobox('getValue'),
            keyWord: $("#txtKeyWord").val(),
            LogisticsID: $("#Logistics").combobox('getValue'),
            WarehouseCode: $("#Warehouse").combobox('getValue'),
            startDate: $("#startDate").datebox('getValue'),
            endDate: $("#endDate").datebox('getValue')
        };
        var options = {
            url: '/Finance/ExpressCost/Export',
            data: queryData,
            title: '导出快递费用',
            fileTitle: '快递费用'
        };
        $.AjaxExport(options);
    });
    //清空条件
    $('#btnReset').click(function () {
        window.location.reload(true);
    });
    //搜索
    $('#btnSearch').click(function () {
        bindSerarchLickEvent(1);
    });
    //第一次加载带默认时间
    $('#btnSearch').click();
});

//绑定搜索按钮的的点击事件
function bindSerarchLickEvent(pageNumber) {
    //得到用户输入的参数
    var queryData = {
        keyWordType: $("#keyWordType").combobox('getValue'),
        keyWord: $("#txtKeyWord").val(),
        LogisticsID: $("#Logistics").combobox('getValue'),
        WarehouseCode: $("#Warehouse").combobox('getValue'),
        startDate: $("#startDate").datebox('getValue'),
        endDate: $("#endDate").datebox('getValue')

    }
    //将值传递给
    initTable(queryData, pageNumber);
    return false;
}

//加载列表
function initTable(queryData, pageNumber) {
    $('#grid').datagrid({
        url: '/Finance/ExpressCost/Search?ram=' + Math.random(),
        height: $(document).height() * 0.8,
        width: function () { return document.body.clientWidth * 0.9 },
        nowrap: false,
        autoRowHeight: false,
        striped: true,
        collapsible: true,
        pagination: true,
        fitColumns: true,
        pageNumber: pageNumber,
        fit: true, //datagrid自适应宽度      
        border: false,
        pageSize: 15,
        pageList: [15, 20, 30],
        rownumbers: false,
        sortName: 'ID',
        sortOrder: 'desc',
        remoteSort: false,
        idField: 'ID',
        queryParams: queryData,  //异步查询的参数
        columns: [[
        { field: 'ck', checkbox: true },   //选择
        { title: '主键', field: 'ID', width: 10, sortable: true, hidden: true },
        {
            title: '出库单号', field: 'BillNo', width: 150, align: 'center',
            formatter: function (value, row) {
                var html = '<a href="javascript:void(0);" onclick=\'showOutboundDetail("' + value + '")\'>' + value + '</a>';
                return html;
            }, sortable: true
        },
        { title: '订单编号', field: 'ErpOrderCode', width: 120, align: 'center', sortable: true },
        { title: '仓库', field: 'WarehouseName', width: 120, align: 'center', sortable: true },
        { title: '发货快递', field: 'ExpressName', width: 130, align: 'center', sortable: false },
        { title: '运单号', field: 'WaybillNo', width: 140, align: 'center', sortable: true },
        {
            title: '运费', field: 'ExpressFreight', width: 100, align: 'center',
            formatter: function (value, row) {
                return value.toFixed(3);
            },
            sortable: true
        },
        {
            title: '手续费', field: 'BuyCodFee', width: 100, align: 'center',
            formatter: function (value, row) {
                return value.toFixed(3);
            },
            sortable: true
        },
        { title: '发货时间', field: 'DeliveryDate', width: 130, align: 'center', sortable: true }
        ]],
        onLoadSuccess: function (data) {
            $("#grid").datagrid('clearSelections');
            getTotalAmount();
            DataGridNoData(this);
        }
    });
}

//获取汇总金额
function getTotalAmount() {
    var queryData = {
        keyWordType: $("#keyWordType").combobox('getValue'),
        keyWord: $("#txtKeyWord").val(),
        LogisticsID: $("#Logistics").combobox('getValue'),
        WarehouseCode: $("#Warehouse").combobox('getValue'),
        startDate: $("#startDate").datebox('getValue'),
        endDate: $("#endDate").datebox('getValue')

    }
    $.ajax({
        url: "/Finance/ExpressCost/GetTotalAmount",
        type: "GET",
        data: queryData,
        cache: false,
        success: function (r) {
            var map = $.parseJSON(r);
            $("#lblTotalExpressFreight").text(map.TotalExpressFreight.toFixed(3));
            $("#lblTotalBuyCodFee").text(map.TotalBuyCodFee.toFixed(3));
        },
        error: function () {
            $.MsgBox.Alert("提示", "安排打印失败！");
        }
    });
}