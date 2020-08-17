var status = 1;
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

    //刷新当前页
    $('#refreshCurrentPage').click(function () {
        var grid = $("#" + gridID);
        var options = grid.datagrid("getPager").data("pagination").options;
        var currPageNumber = options.pageNumber;
        search(currPageNumber);
    });

    //搜索
    $("#btnSearch").click(function () {
        search(1);
    });

    //清空条件
    $('#btnReset').click(function () {
        window.location.reload(true);
    });

    //上架
    $("#onSale").click(function () {
        if ($(this).hasClass('unclick')) return false;
        onSale();
    });

    //下架
    $("#offSale").click(function () {
        if ($(this).hasClass('unclick')) return false;
        offSale();
    });

    //删除
    $("#del").click(function () {
        if ($(this).hasClass('unclick')) return false;
        del();
    });

    $('#tt').tabs({
        onSelect: function (title, index) {
            switch (title) {
                case "销售中商品":
                    status = 1;
                    gridID = "grid";
                    search(1);

                    break;
                case "仓库中商品":
                    status = 2;
                    gridID = "gridOff";
                    search(1);
                    break;
            }
        }
    });
});

//上架
function onSale() {
    var rows = $("#" + gridID).datagrid("getSelections");
    if (rows.length > 0) {
        var ids = [];
        for (var i = 0; i < rows.length; i++) {
            ids.push(rows[i].ID);
        }
        $.messager.confirm('提示', "确认上架这 " + ids.length + " 件商品？", function (r) {
            if (r) {
                $.ajax({
                    url: "/Warehouse/WarehouseProducts/OnSale?ids=" + ids.join(','),
                    type: "GET",
                    cache: false,
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
                        $.MsgBox.Alert("提示", "上架失败！");
                    }
                });
            }
        });
    }
    else {
        $.MsgBox.Alert("提示", "请选择商品！");
    }
}

//下架
function offSale() {
    var rows = $("#" + gridID).datagrid("getSelections");
    if (rows.length > 0) {
        var ids = [];
        for (var i = 0; i < rows.length; i++) {
            ids.push(rows[i].ID);
        }
        $.messager.confirm('提示', "确认下架这 " + ids.length + " 件商品？", function (r) {
            if (r) {
                $.ajax({
                    url: "/Warehouse/WarehouseProducts/OffSale?ids=" + ids.join(','),
                    type: "GET",
                    cache: false,
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
                        $.MsgBox.Alert("提示", "下架失败！");
                    }
                });
            }
        });
    }
    else {
        $.MsgBox.Alert("提示", "请选择商品！");
    }
}

//删除
function del() {
    var rows = $("#" + gridID).datagrid("getSelections");
    if (rows.length > 0) {
        var ids = [];
        var inventoryLimit = 0;
        for (var i = 0; i < rows.length; i++) {
            ids.push(rows[i].ID);
            if (rows[i].Num > 0) {
                inventoryLimit++;
            }
        }
        if (inventoryLimit > 0) {
            $.MsgBox.Alert("提示", "有库存的商品不能删除！");
        }
        else {
            $.messager.confirm('提示', "确认删除这 " + ids.length + " 件商品？", function (r) {
                if (r) {
                    $.ajax({
                        url: "/Warehouse/WarehouseProducts/Delete?ids=" + ids.join(','),
                        type: "GET",
                        cache: false,
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
                            $.MsgBox.Alert("提示", "删除失败！");
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
    $("#" + gridID).datagrid({
        url: '/Warehouse/WarehouseProducts/Search?ram=' + Math.random(),
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
        onSelect: function (rowIndex, rowData) {
            showControl();
        },
        onUnselect: function (rowIndex, rowData) {
            showControl();
        },
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
            { title: '库存数量', field: 'Num', width: 100, align: 'center', formatter: function (value, row) { return value + '<br/><a href="#" onclick=\'showProductsKuc(' + row.ID + ')\'>查看</a>'; }, sortable: false },
            { title: '操作', field: 'Permit', width: 100, align: 'center', formatter: function (value, row) { return '<a href="#" onclick=\'showProducts(' + row.ID + ')\'>查看</a>'; }, sortable: false }
        ]],
        onLoadSuccess: function (data) {
            //注册全选事件
            $('#' + gridID).parent().find("div .datagrid-header-check").children("input[type='checkbox']").eq(0).click(function () {
                showControl();
            });
            $("#" + gridID).datagrid('clearSelections');
            showControl();
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
        status: status
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

//查看商品信息
function showProducts(id) {
    location.href = "/Warehouse/Products/Show?id=" + id;
}

//按钮显示控制
function showControl() {
    var rows = $("#" + gridID).datagrid("getSelections");
    if (rows.length <= 0) {
        //禁用上架
        $("#onSale").addClass("unclick");
        //禁用下架
        $("#offSale").addClass("unclick");
        //禁用删除
        $("#del").addClass("unclick");
    } else {
        if (status == "2") {
            //仓库中启用上架
            $('#onSale').removeClass('unclick');
        } else {
            //销售中启用下架
            $('#offSale').removeClass('unclick');
        }
        //启用删除
        $('#del').removeClass('unclick');
    }
}

//查看商品库存
function showProductsKuc(id) {
    location.href = "/Warehouse/WarehouseProducts/ShowKuc?id=" + id;
}

function formatimg(val, row) {
    var firstSmallPic = "../../Upload/Products/NoImg.jpg";
    if (val != null && val != '') {
        firstSmallPic = val.split(',')[0];
    }
    return '<div class="d48"><dfn></dfn><img src=' + firstSmallPic + ' width=\'120px\' height=\'120px\'></div>';
}
