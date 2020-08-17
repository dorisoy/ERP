var gridID = "grid";
var status = 1;

var oldvalue = 0;
function mytextare() {
    var newValue = $('#refreshPermit').val();
    if (newValue == oldvalue) {
        return;
    } else {
        BindSerarchLickEvent();
        oldvalue = newValue;
    }
};

function updateActions(index) {
    $('#' + gridID).datagrid('updateRow', {
        index: index,
        row: {}
    });
}
function editrow(index) {
    $('#grid').datagrid('beginEdit', index);
}
function saverow(index) {
    $('#' + gridID).datagrid('endEdit', index);
}


//初始化
$(function () {
    initTable();


    $('#btnReset').click(function () {
        window.location.reload(true);
    });

    //$.extend($.messager.defaults,{ok:"我知道了"});

    //添加
    $('#add').click(function () {
        showLocalWindow("添加入库商品", "/Warehouse/WarehouseInStock/AddPro?BillNo=" + $('#BillNo').val(), 700, 500, true, false, false);
    });

    //导入
    $('#import').click(function () {
        showLocalWindow("导入商品", "/html-erp/Html/Purchase/plan-import.html", 400, 180, true, false, false);
    });


    //删除
    $("#del").click(function () {
        if ($(this).hasClass('unclick')) return false;
        var rows = $("#" + gridID).datagrid("getSelections");
        if (rows.length > 0) {
            var ids = [];
            for (var i = 0; i < rows.length; i++) {
                ids.push(rows[i].ID);
            }
            parent.$.messager.confirm('提示', "确认删除这 " + ids.length + " 件商品吗？", function (r) {
                if (r) {
                    $.ajax({
                        url: "json.html?ids=" + ids.join(','),
                        type: "GET",
                        cache: false,
                        success: function (r) {
                            var map = $.parseJSON(r);
                            if (map.result == 1) {
                                parent.$.messager.alert("提示", "操作成功！", "");
                                $('#del').addClass('unclick');
                                BindSerarchLickEvent();
                            } else {
                                parent.$.messager.alert("提示", map.message, "error");
                            }
                        },
                        error: function () {
                            parent.$.messager.alert("提示", "删除失败！", "error");
                        }
                    });
                }
            });
        }
        else {
            parent.$.messager.alert("提示", "请选择商品", "error");
        }
    });

    //相同生产日期
    $('#jsProductionDate').live('click', function () {
        parent.$.messager.confirm('提示', "操作后当前列表所显示SKU的生产日期将修改为与第一行一致？", function (r) {
            if (r) {
                var rows = $("#" + gridID).datagrid("getRows");
                var ids = [];
                for (var i = 0; i < rows.length; i++) {
                    ids.push(rows[i].ID);
                }
                $('.datagrid-btable [field="ProductionDate"]').each(function (index, element) {
                    $(element).find('div').text(rows[0].ProductionDate);
                });
                $.ajax({
                    url: "json.html?ids=" + ids.join(',') + '&PurchasePrice=' + rows[0].ProductionDate,
                    type: "GET",
                    cache: false,
                    success: function (r) {

                    },
                    error: function () {
                    }
                });
            }
        });
    });
    //相同库位编码
    $('#jsLibrary').live('click', function () {
        parent.$.messager.confirm('提示', "操作后当前列表所显示SKU的库位编码将修改为与第一行一致？", function (r) {
            if (r) {
                var rows = $("#" + gridID).datagrid("getRows");
                var ids = [];
                for (var i = 0; i < rows.length; i++) {
                    ids.push(rows[i].ID);
                }
                $('.datagrid-btable [field="Library"]').each(function (index, element) {
                    $(element).find('div').text(rows[0].Library);
                });
                $.ajax({
                    url: "json.html?ids=" + ids.join(',') + '&Library=' + rows[0].Library,
                    type: "GET",
                    cache: false,
                    success: function (r) {
                    },
                    error: function () {
                    }
                });
            }
        });
    });

});


//加载列表
function initTable(queryData) {
    $('#' + gridID).datagrid({
        url: 'json-Storagelist-no.html?ram=' + Math.random(),
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
        border: false,
        pageSize: 15,
        pageList: [15, 20, 30],
        rownumbers: false,
        remoteSort: false,
        onSelect: function (rowIndex, rowData) {
            $('#del').removeClass('unclick');
        },
        onUnselect: function (rowIndex, rowData) {
            var rows = $("#" + gridID).datagrid("getSelections");
            if (rows.length <= 0) {
                $('#del').addClass('unclick');
            }
        },
        columns: [[
            { field: 'ck', width: 52, checkbox: true },   //选择
            { title: '', field: 'ID', hidden: true },
            { title: '商品编码', field: 'Code', width: 150, align: 'center', sortable: true },
            { title: '商品名称', field: 'Name', width: 350, align: 'center', sortable: true },
            { title: '属性', field: 'Attribute', width: 150, align: 'center', sortable: true },
            { title: 'SKU码', field: 'SKU', width: 150, align: 'center', sortable: true },
            { title: '采购数量', field: 'PurchaseNum', width: 150, align: 'center', sortable: true },
            { title: '已入数量', field: 'AlreadyNum', width: 150, align: 'center', sortable: true },
            {
                title: '采购价格', field: 'PurchasePrice', width: 150, align: 'center',
                editor: {
                    type: 'textbox',
                    options: {
                        height: 30
                    }
                },
                sortable: true
            },
            {
                title: '生产日期<br><span style="color:#ffff00; cursor:pointer" id="jsProductionDate">[相同]</span>', field: 'ProductionDate', width: 150, align: 'center',
                editor: {
                    type: 'datebox',
                    options: {
                        height: 30
                    }
                },
                sortable: false
            },
            {
                title: '库位编码<br><span style="color:#ffff00; cursor:pointer" id="jsLibrary">[相同]</span>', field: 'Library', width: 150, align: 'center',
                editor: {
                    type: 'textbox',
                    options: {
                        height: 30
                    }
                },
                sortable: false
            },
            {
                title: '入库数量', field: 'StorageNum', width: 150, align: 'center',
                editor: {
                    type: 'numberbox',
                    options: {
                        height: 30
                    }
                },
                sortable: true
            },

            {
                title: '操作', field: 'action', width: 150, align: 'center',
                formatter: function (value, row, index) {

                    if (row.editing) {
                        var s = '<a href="#" onclick="saverow(' + index + ')">保存</a> ';
                        return s;
                    } else {
                        var e = '<a href="#" onclick="editrow(' + index + ')">编辑</a> ';
                        return e;
                    }


                },
                sortable: true
            }
        ]],
        onBeforeEdit: function (index, row) {
            row.editing = true;
            updateActions(index);
        },
        onAfterEdit: function (index, row) {
            row.editing = false;
            updateActions(index);
        },
        onCancelEdit: function (index, row) {
            row.editing = false;
            updateActions(index);
        }
    });
}

//绑定搜索按钮的的点击事件
function BindSerarchLickEvent() {
    //得到用户输入的参数
    var queryData = {
        name: $("#txtName").val(),
        code: $("#txtCode").val(),
        no: $("#txtNo").val(),
        skuCode: $("#txtSkuCode").val(),
        status: status
    }
    //将值传递给
    initTable(queryData);
}