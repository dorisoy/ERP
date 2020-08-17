//初始化
$(function () {
    initTable("", 1);
    bindWarehouseBrand();

    //选中特定值
    $("#brand").combobox('setValue', '0');
    $("#category").combotree('setValue', '-1');

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

    //搜索
    $('#btnSearch').click(function () {
        search(1);
    });

    //导入仓库
    $("#importWarehouse").click(function () {
        importWarehouse();
    });
});

//加载列表
function initTable(queryData, pageNumber) {
    $('#grid').datagrid({
        url: '/Warehouse/Products/Search?ram=' + Math.random(),
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
            { title: '商品编码', field: 'Code', width: 100, align: 'center', sortable: false },
            { title: '图片', field: 'SmallPic', width: 100, align: 'center', formatter: formatimg, sortable: false },
            { title: '商品名称', field: 'Name', width: 100, align: 'center', sortable: false },
            { title: '分类', field: 'CategoryName', width: 100, align: 'center', sortable: false },
            { title: '品牌', field: 'BrandName', width: 100, align: 'center', sortable: false },
            { title: '销售价', field: 'SellingPrice', width: 100, formatter: function (value, row) { return value.toFixed(3) }, align: 'center', sortable: false },
            { title: '成本价', field: 'CostPrice', width: 100, formatter: function (value, row) { return value.toFixed(3) }, align: 'center', sortable: false },
            { title: '状态', field: 'Status', width: 100, align: 'center', formatter: function (value, row) { if (value == "1") return "上架"; else return "下架"; }, sortable: false },
            { title: '操作', field: 'Permit', width: 100, align: 'center', formatter: function (value, row) { return '<a href="#" onclick=\'showProducts(' + row.ID + ')\'>查看</a>&nbsp;|&nbsp;<a href="#" onclick=\'importWarehouse(' + row.ID + ')\'>导入仓库</a>'; }, sortable: false }
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
        keyWord: $("#txtKeyWord").val(),
        categoryID: $("#category").combotree('getValue'),
        brandID: $("#brand").combobox('getValue'),
        status: $("#status").combobox('getValue')
    }
    initTable(queryData, pageNumber);
}

//导入仓库
function importWarehouse(id) {
    var total = $("#grid").datagrid('getData').total;
    if (total == 0) {
        $.MsgBox.Alert("提示", "未找到商品！", 1000);
    }
    else {
        var ids = [];
        if (id) {
            ids.push(id);
        }
        else {
            var rows = $("#grid").datagrid("getSelections");
            for (var i = 0; i < rows.length; i++) {
                ids.push(rows[i].ID);
            }
            var queryData;
            if (ids.length == 0) {
                queryData = {
                    keyWordType: $("#keyWordType").combobox('getValue'),
                    keyWord: $("#txtKeyWord").val(),
                    categoryID: $("#category").combotree('getValue'),
                    brandID: $("#brand").combobox('getValue'),
                    status: $("#status").combobox('getValue')
                }
            }
        }
        $.messager.confirm('提示', "确认把这" + (ids.length == 0 ? total : ids.length) + "件商品导入仓库吗？", function (r) {
            if (r) {
                $.ajax({
                    url: "/Warehouse/Products/ImportWarehouse?ids=" + ids.join(','),
                    type: "GET",
                    cache: false,
                    data: queryData,
                    success: function (r) {
                        var map = $.parseJSON(r);
                        if (map.result == 1) {
                            $.MsgBox.Alert("提示", map.message, 1000);
                            $('#refreshCurrentPage').click();
                        } else {
                            $.MsgBox.Alert("提示", map.message);
                        }
                    },
                    error: function () {
                        $.MsgBox.Alert("提示", "导入失败！");
                    }
                });
            }
            else {
                $("#grid").datagrid('clearSelections');
            }
        });
    }
}

//绑定品牌
function bindWarehouseBrand() {
    $('#brand').combobox({
        url: '/Warehouse/Products/GetWarehouseBrandJson',
        valueField: 'Value',
        textField: 'Text'
    });
}

//查看商品信息
function showProducts(id) {
    location.href = "/Warehouse/Products/Show?id=" + id;
}

function formatimg(val, row) {
    var firstSmallPic = "../../Upload/Products/NoImg.jpg";
    if (val != null && val != '') {
        firstSmallPic = val.split(',')[0];
    }
    return '<div class="d48"><dfn></dfn><img src=' + firstSmallPic + ' width=\'120px\' height=\'120px\'></div>';
}