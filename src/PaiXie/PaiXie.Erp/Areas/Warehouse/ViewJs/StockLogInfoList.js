//初始化
$(function () {
    initTable("", 1);
    bindWarehouseBillType();

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

//搜索
function search(pageNumber) {
    //得到用户输入的参数
    var queryData = {
        keyWordType: $("#keyWordType").combobox('getValue'),
        keyWord: $("#txtKeyWord").val(),
        billType: $("#billType").combobox('getValue'),
        stockWay: $("#stockWay").combobox('getValue'),
        startDate: $("#startDate").datebox('getValue'),
        endDate: $("#endDate").datebox('getValue')
    }
    //将值传递给
    initTable(queryData, pageNumber);
}

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
        billType: $("#billType").combobox('getValue'),
        stockWay: $("#stockWay").combobox('getValue'),
        startDate: $("#startDate").datebox('getValue'),
        endDate: $("#endDate").datebox('getValue')
    };
    var options = {
        url: '/Warehouse/StockLog/Export',
        data: queryData,
        title: '导出库存日志',
        fileTitle: '库存日志'
    };
    $.AjaxExport(options);
});

//加载列表
function initTable(queryData, pageNumber) {
    $('#grid').datagrid({
        url: '/Warehouse/StockLog/Search?ram=' + Math.random(),
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
        idField: 'ID',
        queryParams: queryData,  //异步查询的参数
        columns: [[
           { field: 'ck', width: 30, checkbox: true },   //选择  
           { title: '单据类型', field: 'BillTypeName', width: 80, align: 'center',sortable: true},
           { title: '单据编号', field: 'SourceNo', width: 160, align: 'center', sortable: false },
           //{ title: '商品货号', field: 'ProductsNo', width: 110, align: 'center', sortable: false },
           { title: '商品名称', field: 'ProductsName', width: 110, align: 'center', sortable: false },
           { title: '商品编码', field: 'ProductsCode', width: 110, align: 'center', sortable: false },
           { title: '商品SKU码', field: 'ProductsSkuCode', width: 110, align: 'center', sortable: false },
           { title: '商品属性', field: 'ProductsSkuSaleprop', width: 110, align: 'center', sortable: false },
           {
               title: '库位编码/批次号', field: 'LocationCode', width: 160, align: 'center',
               formatter: function (value, row) {
                   return value + "<br/>" + row.ProductsBatchCode;
               }, sortable: false
           },
           { title: '数量', field: 'Num', width: 110, align: 'center', sortable: false },
           {
               title: '出入方向', field: 'StockWay', width: 80, align: 'center',
               formatter: function (value, row) {
                   return row.StockWay == 1 ? "入库" : "出库";
               },
               sortable: true
           },
           { title: '创建人', field: 'CreatePerson', width: 80, align: 'center', sortable: false },
           { title: '创建时间', field: 'CreateDate', width: 100, align: 'center', sortable: false }
        ]]
    });
}

//绑定单据类型
function bindWarehouseBillType() {
    $('#billType').combobox({
        url: '/Warehouse/StockLog/GetWarehouseBillTypeJson',
        valueField: 'Value',
        textField: 'Name'
    });
}