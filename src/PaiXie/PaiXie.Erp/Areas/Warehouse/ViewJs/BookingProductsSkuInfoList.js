//初始化
$(function () {
    initTable("", 1);
    bindWarehouseBrand();

    //选中特定值
    $("#brand").combobox('setValue', '0');
    $("#category").combotree('setValue', '-1');

    //刷新
    $("#refresh").click(function () {
        search(1);
    });

    //刷新当前页
    $('#refreshCurrentPage').click(function () {
        var grid = $("#grid");
        var options = grid.datagrid("getPager").data("pagination").options;
        var currPageNumber = options.pageNumber;
        search(currPageNumber);
    });

    //添加预售
    $("#add").click(function () {
        location.href = "/Warehouse/BookingProductsSku/Add";
    });

    //取消预售
    $("#del").click(function () {
        del();
    });

    //搜索
    $("#btnSearch").click(function () {
        search();
    });
});

//取消预售
function del() {
    var rows = $("#grid").datagrid("getSelections");
    if (rows.length > 0) {
        var ids = [];
        var inventoryLimit = 0;
        for (var i = 0; i < rows.length; i++) {
            ids.push(rows[i].ID);
            if (rows[i].BookingZyNum > 0) {
                inventoryLimit++;
            }
        }
        if (inventoryLimit > 0) {
            $.MsgBox.Alert("提示", "预售占用为0的商品才可以取消！", 1000);
        }
        else {
            $.messager.confirm('提示', "确认取消这 " + ids.length + " 条预售？", function (r) {
                if (r) {
                    $.ajax({
                        url: "/Warehouse/BookingProductsSku/Delete?ids=" + ids.join(','),
                        type: "GET",
                        cache: false,
                        success: function (r) {
                            var map = $.parseJSON(r);
                            $.MsgBox.Alert("提示", map.message);
                            if (map.result == 1) {
                                $('#refreshCurrentPage').click();
                            }
                        },
                        error: function () {
                            $.MsgBox.Alert("提示", "取消预售失败！", 1000);
                        }
                    });
                }
            });
        }
    }
    else {
        $.MsgBox.Alert("提示", "请选择商品！");
    }
}

//加载列表
function initTable(queryData, pageNumber) {
    $("#grid").datagrid({
        url: '/Warehouse/BookingProductsSku/Search?ram=' + Math.random(),
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
            { title: '是否扣减', field: 'BookingModel', width: 100, align: 'center', formatter: function (value, row) { return row.BookingModel == 0 ? '扣减' : '不扣减' }, sortable: false },
            { title: '预售数量', field: 'BookingNum', width: 100, align: 'center', sortable: false },
            { title: '预售可用', field: 'KyNum', width: 100, align: 'center', sortable: false },
            { title: '预售占用', field: 'ZyNum', width: 100, align: 'center', sortable: false },
            { title: '冲抵数量', field: 'CdNum', width: 100, align: 'center', sortable: false },
            { title: '操作', field: 'Permit', width: 100, align: 'center', formatter: function (value, row) { return '<a href="#" onclick=\'edit("' + row.Code + '")\'>查看</a>' }, sortable: false }
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
        bookingModel: $("#bookingModel").combobox('getValue')
    }
    initTable(queryData, pageNumber);
}

//查看（修改预售数量）
function edit(ProductsCode) {
    location.href = "/Warehouse/BookingProductsSku/Edit?productsCode=" + ProductsCode;
}

//绑定品牌
function bindWarehouseBrand() {
    $('#brand').combobox({
        url: '/Warehouse/Products/GetWarehouseBrandJson',
        valueField: 'Value',
        textField: 'Text'
    });
}

function formatimg(val, row) {
    var firstSmallPic = "../../Upload/Products/NoImg.jpg";
    if (val != null && val != '') {
        firstSmallPic = val.split(',')[0];
    }
    return '<div class="d48"><dfn></dfn><img src=' + firstSmallPic + ' width=\'120px\' height=\'120px\'></div>';
}
