var gridID = "grid";

//初始化
$(function () {
    search(1);
    bindWarehouseBrand();

    //选中特定值
    $("#brand").combobox('setValue', '0');
    $("#category").combotree('setValue', '-1');

    //刷新
    $("#refresh").click(function () {
        search(1);
    });

    //添加转换规则
    $("#add").click(function () {
        location.href = "/Warehouse/ConversionRule/Edit";
    });

    //搜索
    $("#btnSearch").click(function () {
        search(1);
    });

    $('#tt').tabs({
        onSelect: function (title, index) {
            switch (title) {
                case "商品列表":
                    //禁用删除
                    $("#del").unbind("click");
                    $("#del").css("color", "#ccc");
                    gridID = "grid";
                    search();

                    break;
                case "商品转换规则":
                    //启用删除
                    $("#del").click(del);
                    $("#del").css("color", "#1d72df");
                    gridID = "gridRule";
                    search();
                    break;
            }
        }
    });
});

//加载列表
function initTable(queryData, pageNumber) {
    var columns = "";
    if (gridID == "grid") {
        columns = "[[" +
                      "{ field: 'ck', checkbox: true }," +
                      "{ title: '主键', field: 'ID', width: 100, align: 'center', sortable: true, hidden: true }," +
                      "{ title: '商品编码', field: 'ProductsCode', width: 100, align: 'center', sortable: false }," +
                      "{ title: '商品名称', field: 'Name', width: 100, align: 'center', sortable: false }," +
                      "{ title: '属性', field: 'Saleprop', width: 100, align: 'center', sortable: false }," +
                      "{ title: 'SKU码', field: 'ProductsSkuCode', width: 100, align: 'center', sortable: false }," +
                      "{ title: '数量', field: 'ZkNum', width: 100, align: 'center', formatter: function (value, row) { return '<a href=\"#\" onclick=\"showLocationProducts(\' + row.ID + \')\">' + value + '</a>'; }, sortable: false }," +
                      "{ title: '操作', field: 'Permit', width: 100, align: 'center', formatter: function (value, row) { return '<a href=\"#\" onclick=\"conversion(\' + row.ID + \')\">转换</a>'; }, sortable: false }" +
                  "]]";
    }
    else {
        columns = "[[" +
                      "{ field: 'ck', checkbox: true }," +
                      "{ title: '主键', field: 'ID', width: 100, align: 'center', sortable: true, hidden: true }," +
                      "{ title: '规则名称', field: 'Name', width: 300, align: 'center', sortable: false }," +
                      "{ title: '操作', field: 'Permit', width: 100, align: 'center', formatter: function (value, row) { return '<a href=\"#\" onclick=\"editRule(\' + row.ID + \')\">编辑</a>'; }, sortable: false }" +
                  "]]";
    }

    $("#" + gridID).datagrid({
        url: '/Warehouse/ConversionRule/Search?ram=' + Math.random(),
        height: $(document).height() * 0.8,
        width: function () { return document.body.clientWidth * 0.9 },
        nowrap: false,
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
        onClickRow: function (rowIndex, rowData) {
        },
        onDblClickRow: function (rowIndex, rowData) {
            editProducts(rowData.ID);
        },
        columns: eval('(' + columns + ')')
    });
}

//搜索
function search(pageNumber) {
    var queryData = {
        keyWordType: $("#keyWordType").combobox('getValue'),
        keyWord: $("#txtKeyWord").val(),
        categoryID: $("#category").combotree('getValue'),
        brandID: $("#brand").combobox('getValue'),
        gridID: gridID,
        locationID: $("#hdnLocationID").val()
    }
    initTable(queryData, pageNumber);
}

//删除
function del() {
    var rows = $("#" + gridID).datagrid("getSelections");
    if (rows.length > 0) {
        var ids = [];
        for (var i = 0; i < rows.length; i++) {
            ids.push(rows[i].ID);
        }
        $.messager.confirm('提示', "确认删除这 " + ids.length + " 件转换规则？", function (r) {
            if (r) {
                $.ajax({
                    url: "/Warehouse/ConversionRule/Delete?ids=" + ids.join(','),
                    type: "GET",
                    cache: false,
                    success: function (r) {
                        var map = $.parseJSON(r);
                        if (map.result == 1) {
                            $.MsgBox.Alert("提示", map.message, 1000);
                            initTable();
                            $("#" + gridID).datagrid('clearSelections');
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
}

//编辑规则
function editRule(id) {
    location.href = "/Warehouse/ConversionRule/Edit?id=" + id;
}

//绑定品牌
function bindWarehouseBrand() {
    $('#brand').combobox({
        url: '/Warehouse/Products/GetWarehouseBrandJson',
        valueField: 'Value',
        textField: 'Text'
    });
}

//选择转换规则
function conversion(productsSkuID) {
    $.ajax({
        url: "/Warehouse/ConversionRule/getConversionRule?productsSkuID=" + productsSkuID,
        type: "GET",
        cache: false,
        success: function (r) {
            var map = $.parseJSON(r);
            if (map.length > 0) {
                if (map.length == 1) {
                    showMyWindow("商品转换", "/Warehouse/ConversionRule/Conversion?ruleID=" + map[0].ID, 750, 450, true, false, false);
                }
                else {
                    showMyWindow("选择转换规则", "/Warehouse/ConversionRule/SelectRule?productsSkuID=" + productsSkuID, 600, 250, true, false, false);
                }
            }
            else {
                $.MsgBox.Alert("提示", "该商品没有转换规则！", 1000);
            }
        },
        error: function () {
            $.MsgBox.Alert("提示", "转换失败！", 1000);
        }
    });
}

//查看库位商品
function showLocationProducts(productsSkuID) {
    location.href = "/Warehouse/ConversionRule/Show?locationID=" + $("#hdnLocationID").val() + "&productsSkuID=" + productsSkuID;
}
