//初始化
$(function () {
    search(1);
    bindWarehouseBrand();

    //选中特定值
    $("#brand").combobox('setValue', '0');
    $("#category").combotree('setValue', '-1');

    //刷新
    $('#refresh').click(function () {
        search(1);
    });

    //搜索
    $("#btnSearch").click(function () {
        search(1);
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
            categoryID: $("#category").combotree('getValue'),
            brandID: $("#brand").combobox('getValue'),
            locationID: $("#hdnLocationID").val()
        };
        var options = {
            url: '/Warehouse/WasteBin/Export',
            data: queryData,
            title: '导出废品区',
            fileTitle: '废品区'
        };
        $.AjaxExport(options);
    });

    $('#btnReset').click(function () {
        window.location.reload(true);
    });

});


//加载列表
function initTable(queryData, pageNumber) {
    $('#grid').datagrid({
        url: '/Warehouse/WasteBin/Search?ram=' + Math.random(),
        height: '100%',
        width: '100%',
        nowrap: false,
        autoRowHeight: false,
        striped: true,
        collapsible: true,
        pagination: true,
        fitColumns: true,
        fit: true, //datagrid自适应宽度
        fitColumn: false, //列自适应宽度
        pageNumber: pageNumber,
        border: false,
        pageSize: 15,
        pageList: [15, 20, 30],
        rownumbers: false,
        remoteSort: false,
        queryParams: queryData,  //异步查询的参数
        columns: [[
			{ field: 'ck', width: 52, checkbox: true },   //选择
			{ title: '商品编码', field: 'ProductsCode', width: 150, align: 'center', sortable: true },
			{ title: '商品名称', field: 'Name', width: 350, align: 'center', sortable: true },
			{ title: '属性', field: 'Saleprop', width: 150, align: 'center', sortable: true },
			{ title: 'SKU码', field: 'ProductsSkuCode', width: 150, align: 'center', sortable: true },
			{ title: '入库批次', field: 'ProductsBatchCode', width: 150, align: 'center', sortable: true },
			{ title: '数量', field: 'ZkNum', width: 150, align: 'center', sortable: true }
        ]]
    });
}

//搜索
function search(pageNumber) {
    var queryData = {
        keyWordType: $("#keyWordType").combobox('getValue'),
        keyWord: $("#txtKeyWord").val(),
        categoryID: $("#category").combotree('getValue'),
        brandID: $("#brand").combobox('getValue'),
        locationID: $("#hdnLocationID").val()
    }
    initTable(queryData, pageNumber);
}

//绑定品牌
function bindWarehouseBrand() {
    $('#brand').combobox({
        url: '/Warehouse/Products/GetWarehouseBrandJson',
        valueField: 'Value',
        textField: 'Text'
    });
}