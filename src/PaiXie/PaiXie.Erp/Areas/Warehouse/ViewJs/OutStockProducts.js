//初始化
$(function () {
    initTable("", 1);
    //刷新当前页
    $('#refreshCurrentPage').click(function () {
        var grid = $("#grid");
        var options = grid.datagrid("getPager").data("pagination").options;
        var currPageNumber = options.pageNumber;
        bindSerarchLickEvent(currPageNumber);
    });
    //返回
    $("#back").click(function () {
        location.href = "/Warehouse/WarehouseOutStock/Index";
    });
    //搜索
    $('#btnSearch').click(function () {
        bindSerarchLickEvent(1);
    });
    //清空条件
    $('#btnReset').click(function () {
        window.location.reload(true);
    });
    //添加
    $('#add').click(function () {
        var sourceID = $('#hdnSourceID').val();
        var outInStockBillNo = $('#hdnOutInStockBillNo').val();
        var outInStockID = $('#hdnOutInStockID').val();
        showLocalWindow("添加出库商品", "/Warehouse/WarehouseOutStockProducts/Add?sourceID=" + sourceID + "&outInStockID=" + outInStockID + "&outInStockBillNo=" + outInStockBillNo, 700, 500, true, false, false);
    });
    //导入
    $('#import').click(function () {
        var outInStockID = $('#hdnOutInStockID').val();
        showLocalWindow("导入商品", "/Warehouse/WarehouseOutStockProducts/Import?outInStockID=" + outInStockID, 400, 180, true, false, false);
    });
    //删除
    $("#del").click(function () {
        if ($(this).hasClass('unclick')) return false;
        var outInStockID = $('#hdnOutInStockID').val();
        var rows = $("#grid").datagrid("getSelections");
        if (rows.length > 0) {
            var ids = [];
            for (var i = 0; i < rows.length; i++) {
                ids.push(rows[i].ID);
            }
            $.messager.confirm('提示', "确认删除这 " + ids.length + " 件商品吗？", function (r) {
                if (r) {
                    $.ajax({
                        url: "/Warehouse/WarehouseOutStockProducts/Delete?outInStockID=" + outInStockID + "&ids=" + ids.join(','),
                        type: "GET",
                        cache: false,
                        success: function (r) {
                            var map = $.parseJSON(r);
                            if (map.result == 1) {
                                $.MsgBox.Alert("提示", "删除成功！", 1000);
                                refreshNum();
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
        else {
            $.MsgBox.Alert("提示", "请选择商品！");
        }
    });
    $("#lblOutStockNum").click(refreshNum);
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
//加载列表
function initTable(queryData, pageNumber) {
    $('#grid').datagrid({
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
        onSelect: function (rowIndex, rowData) {
            showControl();
        },
        onUnselect: function (rowIndex, rowData) {
            showControl();
        },
        columns: [[
            { field: 'ck', width: 52, checkbox: true },   //选择
            { title: '主键', field: 'ID', hidden: true },
            { title: '商品编码', field: 'ProductsCode', width: 150, align: 'center', sortable: true },
            { title: '商品名称', field: 'ProductsName', width: 220, align: 'center', sortable: true },
            { title: '属性', field: 'ProductsSkuSaleprop', width: 150, align: 'center', sortable: true },
            { title: 'SKU码', field: 'ProductsSkuCode', width: 150, align: 'center', sortable: true },
            { title: '库位编码', field: 'LocationCode', width: 160, align: 'center', sortable: false },
            { title: '批次', field: 'ProductsBatchCode', width: 200, align: 'center', sortable: true },
            //{ title: '当前可用库存', field: 'KyNum', width: 100, align: 'center', sortable: true },
           {
               title: '出库数量', field: 'ProductsNum', width: 100, align: 'center',
               editor: {
                   type: 'numberbox',
                   options: {
                       required: true,
                       min: 1,
                       precision: 0,
                       height: 30
                   }
               },
               sortable: true
           },
            {
                title: '操作', field: 'action', width: 100, align: 'center',
                formatter: function (value, row, index) {
                    var html = '';
                    if (row.editing) {
                        html = '<a href="javascript:void(0);" onclick="saverow(' + row.ID + ',' + index + ')">保存</a> ';
                    } else {
                        html = '<a href="javascript:void(0);" onclick="editrow(' + index + ')">编辑</a> ';
                    }
                    return html;
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
        },
        onLoadSuccess: function (data) {
            //注册全选事件
            $('#grid').parent().find("div .datagrid-header-check").children("input[type='checkbox']").eq(0).click(function () {
                showControl();
            });
            $("#grid").datagrid('clearSelections');
            showControl();
            DataGridNoData(this);
        }
    });
}
function updateActions(index) {
    $('#grid').datagrid('updateRow', {
        index: index,
        row: {}
    });
}
function editrow(index) {
    $('#grid').datagrid('beginEdit', index);
    var ed = $('#grid').datagrid('getEditor', { index: index, field: 'ProductsNum' });
    $(ed.target).next('span').find('input').focus();
    $(ed.target).next('span').find('input').select();
}
function saverow(outStockItemID, index) {
    var ed = $('#grid').datagrid('getEditor', { index: index, field: 'ProductsNum' });
    var outStockNum = $(ed.target).numberbox('getValue');
    if (outStockNum == "") {
        //$.MsgBox.Alert("提示", "请输入出库数量！", 1000);
        $(ed.target).next('span').find('input').focus();
    } else {
        $.ajax({
            url: "/Warehouse/WarehouseOutStockProducts/UpdateOutStockNum?ram=" + Math.random(),
            type: "POST",
            data: { outStockItemID: outStockItemID, outStockNum: outStockNum },
            async: false,
            cache: false,
            success: function (r) {
                var map = $.parseJSON(r);
                if (map.result == 1) {
                    $('#grid').datagrid('endEdit', index);
                    $("#grid").datagrid('clearSelections');
                    refreshNum();
                } else {
                    $.MsgBox.Alert("提示", map.message, 1000);
                    $(ed.target).next('span').find('input').focus();
                    $(ed.target).next('span').find('input').select();
                }
            },
            error: function () {
                $.MsgBox.Alert("提示", "操作失败！", 1000);
                $(ed.target).next('span').find('input').focus();
                $(ed.target).next('span').find('input').select();
            }
        });
    }
}

//按钮可用控制
function showControl() {
    var rows = $("#grid").datagrid("getSelections");
    var ids = [];
    for (var i = 0; i < rows.length; i++) {
        ids.push(rows[i].ID);
    }
    if (ids.length <= 0) {
        //禁用删除
        $("#del").addClass("unclick");
    } else {
        //启用删除
        $('#del').removeClass('unclick');
    }
    var status = $("#hdnStatus").val();
    switch (status) {
        case "1":
            //未提交
            $("#import").show();
            $("#add").show();
            $("#del").show();
            $("#grid").datagrid('showColumn', "action")
            break;
        case "2":
            //待审核
            $("#import").hide();
            $("#add").hide();
            $("#del").hide();
            $("#grid").datagrid('hideColumn', "action")
            break;
        case "3":
            //已结束
            $("#import").hide();
            $("#add").hide();
            $("#del").hide();
            $("#grid").datagrid('hideColumn', "action")
            break;
    }
}
//刷新数量
function refreshNum() {
    $.ajax({
        url: "/Warehouse/WarehouseOutStockProducts/GetNum?outInStockID=" + $('#hdnOutInStockID').val(),
        type: "GET",
        cache: false,
        success: function (r) {
            var map = $.parseJSON(r);
            $("#lblOutStockNum").text(map.outStockNum);
        },
        error: function () {
            $.MsgBox.Alert("提示", "读取出库数量失败！", 1000);
        }
    });
}