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
        location.href = "/Purchase/Purchase/Index";
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
        var warehouseCode = $("#hdnWarehouseCode").val();
        var billNo = $("#hdnBillNo").val();
        var purchaseID = $("#hdnPurchaseID").val();
        var suppliersID = $("#hdnSuppliersID").val();
        showLocalWindow("添加采购商品", "/Purchase/PurchaseItem/Add?warehouseCode=" + warehouseCode + "&billNo=" + billNo + "&purchaseID=" + purchaseID + "&suppliersID=" + suppliersID, 700, 450, true, false, false);
    });
    //导入
    $('#import').click(function () {
        var warehouseCode = $("#hdnWarehouseCode").val();
        var billNo = $("#hdnBillNo").val();
        var purchaseID = $("#hdnPurchaseID").val();
        showLocalWindow("导入商品", "/Purchase/PurchaseItem/Import?warehouseCode=" + warehouseCode + "&billNo=" + billNo + "&purchaseID=" + purchaseID, 400, 180, true, false, false);
    });

    //刷新数量和金额
    $("#lblNum").click(function () {
        refreshNum();
    });
    $('#del').click(function () {
        del();
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
//加载列表
function initTable(queryData, pageNumber) {
    $('#grid').datagrid({
        url: '/Purchase/PurchaseItem/Search?purchaseID=' + $("#hdnPurchaseID").val() + '&suppliersID=' + $("#hdnSuppliersID").val() + '&ram=' + Math.random(),
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
            { title: '主键', field: 'ID', width: 40, align: 'center', sortable: false, hidden: true },
			{ title: '商品编码', field: 'ProductsCode', width: 150, align: 'center', sortable: true },
			{ title: '商品名称', field: 'ProductsName', width: 250, align: 'center', sortable: false },
			{ title: '属性', field: 'ProductsSkuSaleprop', width: 150, align: 'center', sortable: false },
			{ title: 'SKU码', field: 'ProductsSkuCode', width: 150, align: 'center', sortable: false },
			{ title: '当前可用库存', field: 'KyNum', width: 150, align: 'center', sortable: false },
			{
			    title: '采购数量', field: 'Num', width: 150, align: 'center', sortable: false,
			    editor: {
			        type: 'numberbox',
			        options: {
			            required: true,
			            min: 1,
			            precision: 0,
			            height: 30
			        }
			    }
			},
            { title: '预计金额', field: 'ExpectedAmount', width: 150, align: 'center', formatter: formatcost, sortable: false },
			{ title: '入库数量', field: 'InStockNum', width: 150, align: 'center', sortable: false },
            {
                title: '操作', field: 'Permit', width: 150, align: 'center',
                formatter: function (value, row, index) {
                    var html = "";
                    if (row.editing) {
                        html = '<a href="javascript:void(0);" onclick="saverow(' + row.ID + ',' + index + ',' + row.ProductsSkuID + ')">保存</a> ';
                    } else {
                        html = '<a href="javascript:void(0);" onclick="editrow(' + index + ')">编辑</a> ';
                    }
                    return html + ' | <a href="javascript:void(0);" onclick=\'del(' + row.ID + ')\'>删除</a>';
                },
                sortable: false
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
        case "0":
            //未确认
            $("#import").show();
            $("#add").show();
            $("#del").show();
            $("#export").hide();
            $("#spanInStockNum").hide();
            $("#grid").datagrid('hideColumn', "InStockNum");
            $("#grid").datagrid('showColumn', "Permit");
            break;
        case "10":
            //已确认
            $("#import").hide();
            $("#add").hide();
            $("#del").hide();
            $("#export").show();
            $("#spanInStockNum").show();
            $("#grid").datagrid('showColumn', "InStockNum");
            $("#grid").datagrid('hideColumn', "Permit");
            break;
        case "20":
            //已结束
            $("#import").hide();
            $("#add").hide();
            $("#del").hide();
            $("#export").show();
            $("#spanInStockNum").show();
            $("#grid").datagrid('showColumn', "InStockNum");
            $("#grid").datagrid('hideColumn', "Permit");
            break;
    }
}
//删除
function del(ids) {
    if (undefined == ids) {
        if ($("#del").hasClass('unclick')) return false;

        var rows = $("#grid").datagrid("getSelections");
        if (rows.length > 0) {
            var ids = [];
            for (var i = 0; i < rows.length; i++) {
                ids.push(rows[i].ID);
            }
            $.messager.confirm('提示', "确认删除这 " + ids.length + " 条商品吗？", function (r) {
                if (r) {
                    deleteAjax(ids.join(','));
                }
            });
        }
        else {
            $.MsgBox.Alert("提示", "请选择商品！", 1000);
        }
    }
    else {
        $.messager.confirm('提示', "确认删除吗？", function (r) {
            if (r) {
                deleteAjax(ids);
            }
        });
    }
}

function deleteAjax(ids) {
    $.ajax({
        url: "/Purchase/PurchaseItem/Delete?purchaseID=" + $("#hdnPurchaseID").val() + "&ids=" + ids,
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
//刷新数量和金额
function refreshNum() {
    $.ajax({
        url: "/Purchase/PurchaseItem/GetNum?warehouseCode=" + $("#hdnWarehouseCode").val() + "&purchaseID=" + $("#hdnPurchaseID").val(),
        type: "GET",
        cache: false,
        success: function (r) {
            var map = $.parseJSON(r);
            $("#lblNum").text(map.num);
            $("#lblExpectedAmount").text(map.expectedAmount.toFixed(3));
            $("#lblInStockNum").text(map.inStockNum);
        },
        error: function () {
            $.MsgBox.Alert("提示", "读取计划采购数量失败！", 1000);
        }
    });
}
function formatcost(val, row) {
    return '<span style="color:#008c23;">' + val.toFixed(3) + '</span>';
}

function updateActions(index) {
    $('#grid').datagrid('updateRow', {
        index: index,
        row: {}
    });
}
function editrow(index) {
    $('#grid').datagrid('beginEdit', index);
    var ed = $('#grid').datagrid('getEditor', { index: index, field: 'Num' });
    $(ed.target).next('span').find('input').focus();
    $(ed.target).next('span').find('input').select();
}
function saverow(purchaseItemID, index, productsSkuID) {
    var purchaseID = $("#hdnPurchaseID").val();
    var suppliersID = $("#hdnSuppliersID").val();
    var ed = $('#grid').datagrid('getEditor', { index: index, field: 'Num' });
    var num = $(ed.target).numberbox('getValue');
    if (num == "") {
        //$.MsgBox.Alert("提示", "请输入采购数量！", 1000);
        $(ed.target).next('span').find('input').focus();
    } else {
        $.ajax({
            url: "/Purchase/PurchaseItem/UpdateNum?purchaseID=" + purchaseID + "&purchaseItemID=" + purchaseItemID + "&num=" + num + "&suppliersID=" + suppliersID + "&productsSkuID=" + productsSkuID,
            type: "GET",
            cache: false,
            success: function (r) {
                var map = $.parseJSON(r);
                if (map.result == 1) {
                    $('#grid').datagrid('getRows')[index]['ExpectedAmount'] = num * map.price;
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
                $.MsgBox.Alert("提示", "保存采购数量失败！", 1000);
                $(ed.target).next('span').find('input').focus();
                $(ed.target).next('span').find('input').select();
            }
        });
    }
}