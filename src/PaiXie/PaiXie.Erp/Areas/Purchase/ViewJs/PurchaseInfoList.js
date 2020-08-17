$(function () {
    //初始化数据
    BindDictItem("Warehouse", "Warehouse");
    BindDictItem("Suppliers", "Suppliers");
    //选中特定值
    $("#Warehouse").combobox('setValue', '0');
    $("#Suppliers").combobox('setValue', '0');
    var queryData = {
        state: "0,10"
    };
    initTable(queryData, 1);
    //刷新
    $('#refresh').click(function () {
        bindSerarchLickEvent(1);
    });
    //刷新当前页
    $('#refreshCurrentPage').click(function () {
        var grid = $("#grid");
        var options = grid.datagrid("getPager").data("pagination").options;
        var currPageNumber = options.pageNumber;
        bindSerarchLickEvent(currPageNumber);
    });
    //搜索
    $('#btnSearch').click(function () {
        bindSerarchLickEvent(1);
    });
    //添加
    $('#add').click(function () {
        showLocalWindow("添加采购单", "/Purchase/Purchase/Add", 425, 240, true, false, false);
    });
    //清空条件
    $('#btnReset').click(function () {
        window.location.reload(true);
    });
});

//绑定搜索按钮的的点击事件
function bindSerarchLickEvent(pageNumber) {
    var strState = '';
    $("input[name='state']:checkbox").each(function () {
        if ($(this).attr("checked")) {
            strState += $(this).val() + ","
        }
    })
    strState = strState.substr(0, strState.length - 1);
    //得到用户输入的参数
    var queryData = {
        keyWordType: $("#keyWordType").combobox('getValue'),
        keyWord: $("#txtKeyWord").val(),
        warehouseCode: $("#Warehouse").combobox('getValue'),
        suppliersID: $("#Suppliers").combobox('getValue'),
        state: strState
    }
    //将值传递给
    initTable(queryData, pageNumber);
}
//加载列表
function initTable(queryData, pageNumber) {
    $('#grid').datagrid({
        url: '/Purchase/Purchase/Search?ram=' + Math.random(),
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
        columns: [[
			{ field: 'ck', width: 52, checkbox: true },   //选择
			{ title: '主键', field: 'ID', hidden: true },
			{
			    title: '采购单号', field: 'BillNo', width: 200, align: 'center',
			    formatter: function (value, row) {
			        var html = "";
			        html = '<a href="javascript:void(0);" onclick=\'manage("' + row.AliasName + '",' + row.ID + ')\'>' + value + '</a>';
			        if(row.PlanBillNo!=null){
			            html += '<br/><span style="color:#999;">(' + row.PlanBillNo + ')</span>';
			        }
			        return html;
			    },
			    sortable: true
			},

			{ title: '供应商名称', field: 'AliasName', width: 120, align: 'center', sortable: true },
			{ title: '仓库名称', field: 'WarehouseName', width: 120, align: 'center', sortable: true },
			{ title: '采购数量', field: 'Num', width: 120, align: 'center', sortable: true },
			{ title: '已入库数量', field: 'InStockNum', width: 120, align: 'center', sortable: true },
			{ title: '入库单', field: 'InStockOrderCount', width: 100, align: 'center', sortable: true },
			{ title: '创建时间', field: 'CreateDate', width: 140, align: 'center', sortable: true },
			{ title: '创建人', field: 'CreatePerson', width: 120, align: 'center', sortable: true },
			{
			    title: '状态', field: 'Status', width: 100, align: 'center',
			    formatter: function (value, row, index) {
			        var state = "";
			        if (value == 0) {
			            state= '未确认';
			        } else if (value == 10) {
			            state = '已确认';
			        } else if (value == 20) {
			            state = '已结束';
			        } else {
			            //nothing
			        }
			        return state;
			    },
			    sortable: true
			},
			{
			    title: '操作', field: 'Permit', width: 150, align: 'center',
			    formatter: function (value, row, index) {
			        var html = "";
			        var endHtml = "";
			        var operaTxt = "";
			        if (row.Status == 0) {
			            operaTxt = "管理";
			            if (row.Num > 0) {
			                endHtml = ' | <a href="javascript:void(0);" onclick=\'confirm(' + row.ID + ')\'>确认</a>';
			            }
			            endHtml = endHtml + ' | <a href="javascript:void(0);" onclick=\'del(' + row.ID + ')\'>删除</a>';
			        } else if (row.Status == 10) {
			            operaTxt = "查看";
			            if (row.InStockOrderCount == 0) {
			                endHtml = ' | <a href="javascript:void(0);" onclick=\'del(' + row.ID + ')\'>删除</a>';
			            } else {
			                endHtml = ' | <a href="javascript:void(0)" onclick=\'end(' + row.ID + ')\'>结束</a>';
			            }
			        } else {
			            operaTxt = "查看";
			            endHtml = ' | <a href="javascript:void(0);" onclick=\'rePurchase(' + row.ID + ')\'>重新采购</a>';
			        }
			        html = '<a href="javascript:void(0);" onclick=\'manage("' + row.AliasName + '",' + row.ID + ')\'>' + operaTxt + '</a>' + endHtml;
			        return html;
			    },
			    sortable: false
			}
        ]],
        onLoadSuccess: function (data) {
            $("#grid").datagrid('clearSelections');
            DataGridNoData(this);
        }
    });
}

//查看或管理
function manage(aliasName, purchaseID) {
    location.href = "/Purchase/PurchaseItem/Index?aliasName=" + aliasName + "&purchaseID=" + purchaseID + "&ram=" + Math.random();
}
//确认
function confirm(purchaseID) {
    $.ajax({
        url: "/Purchase/Purchase/Confirm?ids=" + purchaseID,
        type: "GET",
        cache: false,
        success: function (r) {
            var map = $.parseJSON(r);
            if (map.result == 1) {
                $.MsgBox.Alert("提示", "操作成功！", 1000);
                $('#refreshCurrentPage').click();
            } else {
                $.MsgBox.Alert("提示", map.message);
            }
        },
        error: function () {
            $.MsgBox.Alert("提示", "确认失败！");
        }
    });
}
//删除操作
function del(purchaseID) {
    $.messager.confirm('提示', "确认删除？", function (r) {
        if (r) {
            $.ajax({
                url: "/Purchase/Purchase/Delete?ids=" + purchaseID,
                type: "GET",
                cache: false,
                success: function (r) {
                    var map = $.parseJSON(r);
                    if (map.result == 1) {
                        $.MsgBox.Alert("提示", "删除成功！", 1000);
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
//结束操作
function end(purchaseID) {
    $.messager.confirm('提示', "确认结束？", function (r) {
        if (r) {
            $.ajax({
                url: "/Purchase/Purchase/End?ids=" + purchaseID,
                type: "GET",
                cache: false,
                success: function (r) {
                    var map = $.parseJSON(r);
                    if (map.result == 1) {
                        $.MsgBox.Alert("提示", "操作成功！", 1000);
                        $('#refreshCurrentPage').click();
                    } else {
                        $.MsgBox.Alert("提示", map.message);
                    }
                },
                error: function () {
                    $.MsgBox.Alert("提示", "结束失败！");
                }
            });
        }
    });
}

//重新采购
function rePurchase(purchaseID) {
    $.messager.confirm('提示', "该采购单的采购商品重新生成新的采购单？", function (r) {
        if (r) {
            $.ajax({
                url: "/Purchase/Purchase/RePurchase?purchaseID=" + purchaseID,
                type: "GET",
                cache: false,
                success: function (r) {
                    var map = $.parseJSON(r);
                    if (map.result == 1) {
                        $.MsgBox.Alert("提示", "操作成功！", 1000);
                        $('#refreshCurrentPage').click();
                    } else {
                        $.MsgBox.Alert("提示", map.message);
                    }
                },
                error: function () {
                    $.MsgBox.Alert("提示", "重新采购失败！");
                }
            });
        }
    });
}