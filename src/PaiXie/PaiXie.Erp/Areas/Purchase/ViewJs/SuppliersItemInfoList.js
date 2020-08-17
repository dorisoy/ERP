
//初始化
$(function () {
    initTable("", 1);
    //添加
    $('#add').click(function () {
        var suppliersID = $("#hdnSuppliersID").val();
        showLocalWindow("添加采购价", "/Purchase/SuppliersItem/Edit?suppliersID=" + suppliersID + "&productsID=0", 700, 420, true, false, false);
    });
    //刷新当前页
    $('#refreshCurrentPage').click(function () {
        var grid = $("#grid");
        var options = grid.datagrid("getPager").data("pagination").options;
        var currPageNumber = options.pageNumber;
        initTable(currPageNumber);
    });
    //返回
    $('#back').click(function () {
        location.href = "/Purchase/Suppliers/Index";
    });
    //导入
    $('#import').click(function () {
        var suppliersID = $("#hdnSuppliersID").val();
        showLocalWindow("表格导入", "/Purchase/SuppliersItem/Import?suppliersID=" + suppliersID, 400, 180, true, false, false);
    });
    //删除
    $("#del").click(function () {
        if ($(this).hasClass('unclick')) return false;
        var rows = $("#grid").datagrid("getSelections");
        if (rows.length > 0) {
            var productsIDs = [];
            for (var i = 0; i < rows.length; i++) {
                productsIDs.push(rows[i].ProductsID);
            }
            $.ajax({
                url: "/Purchase/SuppliersItem/Delete?productsIDs=" + productsIDs.join(','),
                type: "GET",
                cache: false,
                success: function (r) {
                    var map = $.parseJSON(r);
                    if (map.result == 1) {
                        $.MsgBox.Alert("提示", "删除成功！", 1000);
                        refreshProductsCount();
                        $('#refreshCurrentPage').click();
                    } else {
                        $.MsgBox.Alert("提示", map.message);
                    }
                },
                error: function () {
                    $.MsgBox.Alert("提示", "删除失败！");
                }
            });
        }
        else {
            $.MsgBox.Alert("提示", "请选择商品！");
        }
    });
    //搜索
    $("#btnSearch").click(function () {
        bindSerarchLickEvent(1);
    });
    //清空条件
    $('#btnReset').click(function () {
        window.location.reload(true);
    });
    //刷新商品数量
    $("#lblProductsCount").click(refreshProductsCount);
});

//绑定搜索按钮的的点击事件
function bindSerarchLickEvent(pageNumber) {
    //得到用户输入的参数
    var queryData = {
        keyWordType: $("#keyWordType").combobox('getValue'),
        keyWord: $("#txtKeyWord").val()
    }
    //将值传递给
    initTable(queryData, pageNumber);
}
//加载列表
function initTable(queryData, pageNumber) {
    $('#grid').datagrid({
        url: '/Purchase/SuppliersItem/Search?suppliersID=' + $("#hdnSuppliersID").val() + '&ram=' + Math.random(),
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
        remoteSort: false,
        queryParams: queryData,  //异步查询的参数
        onSelect: function (rowIndex, rowData) {
            showControl();
        },
        onUnselect: function (rowIndex, rowData) {
            showControl();
        },
        onDblClickRow: function (rowIndex, rowData) {
            modify(rowData.ProductsID);
        },
        columns: [[
			{ field: 'ck', width: 52, checkbox: true },   //选择
			{ title: '商品ID', field: 'ProductsID', hidden: true },
			{ title: '商品编码', field: 'ProductsCode', width: 150, align: 'center', sortable: false },
			{ title: '商品名称', field: 'ProductsName', width: 250, align: 'center', sortable: false },
			{
			    title: '采购价', field: 'PurchasePrice', width: 200, align: 'center',
			    formatter: function (value, row, index) {
			        return '￥' + row.MinPurchasePrice.toFixed(3) + '－￥' + row.MaxPurchasePrice.toFixed(3);
			    },
			    sortable: false
			},
			{ title: '到货周期(天)', field: 'ArrivalCycle', width: 150, align: 'center', sortable: true },
			{
			    title: '操作', field: 'Permit', width: 150, align: 'center',
			    formatter: function (value, row, index) {
			        return '<a href="javascript:void(0);" onclick=\'modify(' + row.ProductsID + ')\'>修改</a>';
			    },
			    sortable: true
			}
        ]]
    });
}

//控制按钮是否可用
function showControl() {
    var rows = $("#grid").datagrid("getSelections");
    var productsIDs = [];
    for (var i = 0; i < rows.length; i++) {
        productsIDs.push(rows[i].ProductsID);
    }
    if (productsIDs.length <= 0) {
        //禁用删除
        $("#del").addClass("unclick");
    } else {
        //启用删除
        $('#del').removeClass('unclick');
    }
}
//修改
function modify(productsID) {
    var suppliersID = $("#hdnSuppliersID").val();
    showLocalWindow("修改采购价", "/Purchase/SuppliersItem/Edit?suppliersID=" + suppliersID + "&productsID=" + productsID, 700, 420, true, false, false);
}

//刷新数量
function refreshProductsCount() {
    $.ajax({
        url: "/Purchase/SuppliersItem/GetProductsCount?suppliersID=" + $("#hdnSuppliersID").val(),
        type: "GET",
        cache: false,
        success: function (r) {
            var map = $.parseJSON(r);
            $("#lblProductsCount").text(map.productsCount);
        },
        error: function () {
            $.MsgBox.Alert("提示", "读取计划采购数量失败！", 1000);
        }
    });
}