//初始化
$(function () {
    search(1);
    bindWarehouseBrand();

    //选中特定值
    $("#brand").combobox('setValue', '0');
    $("#category").combotree('setValue', '-1');

    //导出
    $('#export').click(function () {

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
        categoryID: $("#category").combotree('getValue'),
        brandID: $("#brand").combobox('getValue'),
        startDate: $("#txtStartDate").combobox('getValue'),
        endDate: $("#txtEndDate").combobox('getValue')
    }
    //将值传递给
    initTable(queryData, pageNumber);
}

//加载列表
function initTable(queryData, pageNumber) {
    $('#grid').datagrid({
        url: '/Warehouse/StockItem/Search?ram=' + Math.random(),
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
			{ title: '主键', field: 'ID', width: 40, align: 'center', sortable: true, hidden: true },
			{ title: '商品编码', field: 'Code', width: 80, align: 'center', sortable: false },
			{ title: '商品名称', field: 'Name', width: 150, align: 'center', sortable: false },
			{ title: '分类', field: 'CategoryName', width: 110, align: 'center', sortable: false },
			{ title: '品牌', field: 'BrandName', width: 100, align: 'center', sortable: false },
            { title: '属性', field: 'ProductsSkuSaleprop', width: 100, align: 'center', sortable: false },
			{ title: 'SKU码', field: 'ProductsSkuCode', width: 60, align: 'center', sortable: false },
			{ title: '税率', field: 'TaxRate', width: 60, align: 'center', sortable: false },
			{ title: '期初库存', field: 'InitialInventory', width: 90, align: 'center', sortable: false },
			{ title: '期初成本', field: 'InitialCost', width: 90, align: 'center', sortable: false },
			{ title: '出库数量', field: 'OutboundNum', width: 90, align: 'center', sortable: false },
			{ title: '入库数量', field: 'StorageNum', width: 90, align: 'center', sortable: false },
			{ title: '销售数量', field: 'SalesVolumes', width: 90, align: 'center', sortable: false },
			{ title: '销售金额', field: 'SalesAmount', width: 90, align: 'center', sortable: false },
			{ title: '调整数量', field: 'AdjustQuantity', width: 90, align: 'center', sortable: false },
			{ title: '转出数量', field: 'RollOutQuantity', width: 90, align: 'center', sortable: false },
			{ title: '转入数量', field: 'QuantityOfTransfer', width: 90, align: 'center', sortable: false },
			{ title: '期末数量', field: 'FinalQuantity', width: 90, align: 'center', sortable: false },
			{ title: '期末成本', field: 'FinalCost', width: 90, align: 'center', sortable: false }
        ]]
    });
}

//绑定品牌
function bindWarehouseBrand() {
    $('#brand').combobox({
        url: '/Warehouse/Products/GetWarehouseBrandJson',
        valueField: 'Value',
        textField: 'Text'
    });
}