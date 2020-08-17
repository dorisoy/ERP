var gridID = "grid";
var status = 1;
//初始化
$(function () {
    initTable("", 1);
    //返回
    $('#back').click(function () {
        location.href = "/Finance/FinancialAudit/Index";
    });

    //刷新
    $('#refresh').click(function () {
        bindSerarchLickEvent(1);
    });
    //刷新当前页
    $('#refreshCurrentPage').click(function () {
        var grid = $("#" + gridID);
        var options = grid.datagrid("getPager").data("pagination").options;
        var currPageNumber = options.pageNumber;
        bindSerarchLickEvent(currPageNumber);
    });
    //搜索
    $('#btnSearch').click(function () {
        bindSerarchLickEvent(1);
    });
    //清空条件
    $('#btnReset').click(function () {
        window.location.reload(true);
    });
  
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

//入库单   加载列表
function initTable(queryData, pageNumber) {
    $('#' + gridID).datagrid({
        url: '/Warehouse/WarehouseOutStockProducts/Search?outInStockID=' + $('#hdnOutInStockID').val() + '&ram=' + Math.random(),
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
        columns: [[
            //{ field: 'ck', width: 52, checkbox: true },   //选择
            { title: '主键', field: 'ID', hidden: true },
            { title: '商品编码', field: 'ProductsCode', width: 150, align: 'center', sortable: true },
            { title: '商品名称', field: 'ProductsName', width: 220, align: 'center', sortable: true },
            { title: '属性', field: 'ProductsSkuSaleprop', width: 150, align: 'center', sortable: true },
            { title: 'SKU码', field: 'ProductsSkuCode', width: 150, align: 'center', sortable: true },
            { title: '库位编码', field: 'LocationCode', width: 160, align: 'center', sortable: false },
            { title: '批次', field: 'ProductsBatchCode', width: 200, align: 'center', sortable: true },
           {
               title: '出库数量', field: 'ProductsNum', width: 100, align: 'center',
               sortable: true
           },
          
            {
                title: '出库价格', field: 'action', width: 100, align: 'center',
                formatter: function (value, row, index) {
                    var html = '';
                        html = '￥' + row.CostPrice ;               
                    return html;
                },
                sortable: true
            }
        ]]
      
    });
}