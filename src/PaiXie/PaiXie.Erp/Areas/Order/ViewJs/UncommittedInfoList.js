//初始化
$(function () {
    initTable("", 1);

    //刷新
    $('#refresh').click(function () {
        search(1);
    });

    //刷新当前页
    $('#refreshCurrentPage').click(function () {
        var grid = $("#grid");
        var options = grid.datagrid("getPager").data("pagination").options;
        var currPageNumber = options.pageNumber;
        search(currPageNumber);
    });

    //添加订单
    $("#add").click(function () {
        location.href = "/Order/AddOrder/Index";
    });

    //搜索
    $('#btnSearch').click(function () {
        search(1);
    });

    //清空条件
    $('#btnReset').click(function () {
        window.location.reload();
    });
});

//加载列表
function initTable(queryData, pageNumber) {
    $('#grid').datagrid({
        url: '/Order/Uncommitted/Search?ram=' + Math.random(),
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
        idField: 'ID',
        queryParams: queryData,  //异步查询的参数
        onClickRow: function (rowIndex, rowData) {
        },
        onDblClickRow: function (rowIndex, rowData) {
            editProducts(rowData.ID);
        },
        columns:
        [[
            { field: 'ck', checkbox: true },
            { title: '主键', field: 'ID', width: 100, align: 'center', sortable: true, hidden: true },
            { title: '订单编号', field: 'ErpOrderCode', width: 150, align: 'center', sortable: false },
            { title: '店铺', field: 'ShopName', width: 100, align: 'center', sortable: false },
            { title: '创建时间', field: 'CreateDate', width: 100, align: 'center', sortable: false },
            { title: '收件人', field: 'BuyName', width: 100, align: 'center', sortable: false },
            { title: '手机', field: 'BuyMtel', width: 100, align: 'center', sortable: false },
            { title: '订单总额', field: 'ReceivableAmount', width: 100, formatter: function (value, row) { return value.toFixed(3) }, align: 'center', sortable: false },
            { title: '订单商品', field: 'ProductsNum', width: 50, align: 'center', sortable: false },
            { title: '发货物流', field: 'LogisticsName', width: 100, align: 'center', sortable: false },
            { title: '创建人', field: 'CreatePerson', width: 50, align: 'center', sortable: false },
            {
                title: '操作', field: 'Permit', width: 155, align: 'center',
                formatter: function (value, row) {
                    var html = '<a href="javascript:void(0);" onclick=\'showOrder(' + row.ErpOrderCode + ')\'>提交订单</a>';
                    html += ' | <a href="javascript:void(0);" onclick=\'del(' + row.ID + ')\'>删除</a>';
                    return html;
                },
                sortable: false
            }
        ]],
        onLoadSuccess: function (data) {
            $("#grid").datagrid('clearSelections');
            DataGridNoData(this);
        }
    });
}

//搜索
function search(pageNumber) {
    var queryData = {
        keyWordType: $("#keyWordType").combobox('getValue'),
        keyWord: $("#txtKeyWord").val()
    }
    initTable(queryData, pageNumber);
}

//提交订单
function showOrder(erpOrderCode) {
    location.href = "/Order/AddOrder/Index?erpOrderCode=" + erpOrderCode;
}

//删除
function del(id) {
    $.messager.confirm('提示', "确认删除该订单？", function (r) {
        if (r) {
            $.ajax({
                url: "/Order/Uncommitted/Delete?id=" + id,
                type: "GET",
                cache: false,
                success: function (r) {
                    var map = $.parseJSON(r);
                    if (map.result == 1) {
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
    });
}
