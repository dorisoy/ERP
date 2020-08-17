//初始化
$(function () {
    initTable("", 1);

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
        batchSource: $("#batchSource").combobox('getValue')
    }
    //将值传递给
    initTable(queryData, pageNumber);
}

//加载列表
function initTable(queryData, pageNumber) {
    $('#grid').datagrid({
        url: '/Warehouse/Batch/Search?ram=' + Math.random(),
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
           {
               title: '批次号', field: 'BatchCode', width: 150, align: 'center',
               formatter: function (value, row) {
                   return '<a href="#" onclick=\'show(\"' + row.BatchCode + '\",\"' + row.BatchSource + '\",\"' + row.CreateDate + '\")\'>' + value + '</a>';
               },
               sortable: false
           },
           {
               title: '来源', field: 'BatchSource', width: 150, align: 'center',
               formatter: function (value, row) {
                   var strBatchSource = "";
                   switch (value) {
                       case 1:
                           strBatchSource = "入库";
                           break;
                       case 2:
                           strBatchSource = "盘盈";
                           break;
                       case 3:
                           strBatchSource = "商品转换";
                           break;
                   }
                   return strBatchSource;
               },
               sortable: false
           },
           { title: '商品', field: 'ProductsCount', width: 110, align: 'center', sortable: false },
           { title: '入库数量', field: 'StorageNum', width: 110, align: 'center', sortable: false },
           { title: '出库数量', field: 'OutboundNum', width: 110, align: 'center', sortable: false },
           { title: '转入数量', field: 'QuantityOfTransfer', width: 110, align: 'center', sortable: false },
           { title: '转出数量', field: 'RollOutQuantity', width: 110, align: 'center', sortable: false },
           { title: '销售数量', field: 'SalesVolumes', width: 110, align: 'center', sortable: false },
           { title: '调整数量', field: 'AdjustQuantity', width: 110, align: 'center', sortable: false },
           { title: '当前库存', field: 'Inventory', width: 110, align: 'center', sortable: false },
           { title: '创建时间', field: 'CreateDate', width: 110, align: 'center', sortable: false }
        ]]
    });
}

function show(batchCode, batchSource, createDate) {
    var strBatchSource = "";
    switch (batchSource) {
        case "1":
            strBatchSource = "入库";
            break;
        case "2":
            strBatchSource = "盘盈";
            break;
        case "3":
            strBatchSource = "商品转换";
            break;
    }
    location.href = "/Warehouse/BatchItem/Index?batchCode=" + batchCode + "&batchSource=" + strBatchSource + "&createDate=" + createDate;
}