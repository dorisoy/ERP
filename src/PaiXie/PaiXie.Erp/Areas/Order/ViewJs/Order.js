$(function () {
    initTable("", 1);
    BindDictItem("shop", "shop");
    $('#shop').combobox('select', "0");

    $('#orderStatus').combobox({
        url: '/Order/Order/GetOrdBaseStatusJson',
        valueField: 'Value',
        textField: 'Name',
        onLoadSuccess: function () {
            $('#orderStatus').combobox('select', "-1");
        }
    });

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
});

//加载列表
function initTable(queryData, pageNumber) {
    $('#grid').datagrid({
        url: '/Order/Order/Search?ram=' + Math.random(),
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
        remoteSort: false,
        queryParams: queryData,  //异步查询的参数
        onSelect: function (rowIndex, rowData) {
        },
        onUnselect: function (rowIndex, rowData) {
        },
        columns: [[
			{ field: 'ck', width: 50, checkbox: true },
			{ title: '', field: 'ID', hidden: true },
            {
                title: '订单编号', field: 'ErpOrderCode', width: 300, align: 'center',
                formatter: function (value, row, index) {
                    return '<a href="javascript:void(0)" onclick="showOrderDetails(' + row.ErpOrderCode + ')">' + value + '</a><br/>(' + row.ShopName + ')';
                },
                sortable: true
            },
			{
			    title: '下单时间', field: 'Created', width: 150, align: 'center',
			    formatter: function (value, row) {
			        if (row.CreateType == 1) {
			            return row.CreateDate;
			        }
			        else {
			            return value;
			        }
			    },
			    sortable: true
			},
			{ title: '下单总额', field: 'ReceivableAmount', width: 150, align: 'center', formatter: function (value, row) { return '￥' + value.toFixed(3) }, sortable: true },
			{ title: '收件人', field: 'BuyName', width: 100, align: 'center', sortable: true },
            { title: '订单商品', field: 'ProductsNum', width: 100, align: 'center', sortable: true },
            { title: '发货物流', field: 'LogisticsName', width: 150, align: 'center', sortable: true },
            { title: '货到付款', field: 'IsCod', width: 100, align: 'center', formatter: function (value, row) { return (value == 0) ? "否" : "是" }, sortable: true },
            { title: '需要发票', field: 'IsNeedInvoice', width: 100, align: 'center', formatter: function (value, row) { return (value == 0) ? "否" : "是" }, sortable: true },
            { title: '买家留言', field: 'BuyMessage', width: 150, align: 'center', sortable: true },
            { title: '卖家备注', field: 'SellerRemark', width: 150, align: 'center', sortable: true },
            {
                title: '订单状态', field: 'StrOrderStatus', width: 150, align: 'center',
                formatter: function (value, row) {
                    var html = value;
                    return html;
                },
                sortable: true
            },
            {
                title: '操作', field: 'Permit', width: 200, align: 'center',
                formatter: function (value, row) {
                    if (row.StrOrderStatus == "已取消") {
                        return '<a href="javascript:void(0)" onclick="showOrderDetails(' + row.ErpOrderCode + ')">查看详情</a> | <a href="javascript:void(0)" onclick=\'del(' + row.ID + ')\'>删除</a>';
                    }
                    else {
                        return '<a href="javascript:void(0)" onclick="showOrderDetails(' + row.ErpOrderCode + ')">查看详情</a>';
                    }
                },
                sortable: true
            }
        ]],
        onLoadSuccess: function (data) { DataGridNoData(this); }
    });
}

//搜索
function search(pageNumber) {
    //得到用户输入的参数
    var queryData = {
        keyWordType: $("#keyWordType").combobox('getValue'),
        keyWord: $("#txtKeyWord").val(),
        isCod: $("#isCod").combobox('getValue'),
        isNeedInvoice: $("#isNeedInvoice").combobox('getValue'),
        isRemark: $("#isRemark").combobox('getValue'),
        shopID: $("#shop").combobox('getValue'),
        orderStatus: $("#orderStatus").combobox('getValue'),
    }
    //将值传递给
    initTable(queryData, pageNumber);
}

//删除订单
function del(id) {
    var url = "/Order/Order/Delete";
    $.ajax({
        url: url,
        type: "POST",
        cache: false,
        data: { id: id },
        success: function (r) {
            var map = $.parseJSON(r);
            if (map.result == 1) {
                initTable();
            } else {
                $.MsgBox.Alert("提示", map.message);
            }
        },
        error: function () {
            $.MsgBox.Alert("提示", "删除失败！");
        }
    });
}
