//初始化
$(function () {
    //初始化数据
    BindDictItem("Brand", "Brand");
    //选中特定值
    $("#Brand").combobox('setValue', '0');
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
        var ids = [];
        var rows = $("#grid").datagrid("getSelections");
        for (var i = 0; i < rows.length; i++) {
            ids.push(rows[i].ID);
        }
        var queryData = {
            keyWordType: $("#keyWordType").combobox('getValue'),
            keyWord: $("#txtKeyWord").val(),
            categoryID: $("#Category").combotree('getValue'),
            brandID: $("#Brand").combobox('getValue'),
            startDate: $("#txtStartDate").datebox("getValue"),
            endDate: $("#txtEndDate").datebox("getValue"),
            ids: ids.join(",")
        }
        var options = {
            url: '/Finance/Sales/Export',
            data: queryData,
            title: '导出销售明细',
            fileTitle: '销售明细'
        };
        $.AjaxExport(options);
    });
});

//绑定搜索按钮的的点击事件
function search(pageNumber) {
    //得到用户输入的参数
    var queryData = {
        keyWordType: $("#keyWordType").combobox('getValue'),
        keyWord: $("#txtKeyWord").val(),
        categoryID: $("#Category").combotree('getValue'),
        brandID: $("#Brand").combobox('getValue'),
        startDate: $("#txtStartDate").datebox("getValue"),
        endDate: $("#txtEndDate").datebox("getValue")
    }
    //将值传递给
    initTable(queryData, pageNumber);
}

//加载列表
function initTable(queryData, pageNumber) {
    $('#grid').datagrid({
        url: '/Finance/Sales/Search?ram=' + Math.random(),
        height: $(document).height() * 0.8,
        width: function () { return document.body.clientWidth * 0.9 },
        nowrap: true,
        autoRowHeight: false,
        striped: true,
        collapsible: true,
        pagination: true,
        fitColumns: true,
        fit: true, //datagrid自适应宽度
        pageNumber: pageNumber,
        border: false,
        pageSize: 15,
        pageList: [15, 20, 30],
        rownumbers: false,
        remoteSort: false,
        idField: 'ID',
        queryParams: queryData,  //异步查询的参数
        columns: [[
        { field: 'ck', width: 52, checkbox: true },   //选择
        { title: '主键', field: 'ID', width: 40, align: 'center', sortable: true, hidden: true },
        { title: '发货日期', field: 'BillDate', width: 100, align: 'center', sortable: false },
        { title: '商品编码', field: 'ProductsCode', width: 120, align: 'center', sortable: false },
        { title: '商品名称', field: 'ProductsName', width: 200, align: 'center', sortable: false },
        { title: '分类', field: 'CategoryName', width: 100, align: 'center', sortable: false },
        { title: '品牌', field: 'BrandName', width: 100, align: 'center', sortable: false },
        { title: '商品属性', field: 'ProductsSkuSaleprop', width: 100, align: 'center', sortable: false },
        { title: '商品SKU码', field: 'ProductsSkuCode', width: 100, align: 'center', sortable: false },
        { title: '数量', field: 'ProductsNum', width: 100, align: 'center', formatter: formatProductsNum, sortable: false },
        { title: '销售额', field: 'ProductsAmount', width: 100, align: 'center', formatter: formatPrice, sortable: false },
        { title: '税率', field: 'TaxRate', width: 100, align: 'center', formatter: formatTaxRate, sortable: false },
        { title: '批次', field: 'ProductsBatchCode', width: 100, align: 'center', sortable: false },
        { title: '成本价', field: 'CostPrice', width: 100, align: 'center', formatter: formatPrice, sortable: false },
        { title: '关联单号', field: 'BillNo', width: 150, align: 'center', sortable: false }
        ]],
        onLoadSuccess: function (data) {
            DataGridNoData(this);
        }
    });
}

function formatPrice(val, row) {
    return val.toFixed(3);
}

function formatTaxRate(val, row) {
    return val * 100 + '%';
}

function formatProductsNum(val, row) {
    return val * row.BillType;
}