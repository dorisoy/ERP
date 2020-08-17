//初始化
$(function () {
    initTable();
    //刷新
    $('#noSupplier').click(function () {
        bindSerarchLickEvent(1);
    });
    //刷新当前页
    $('#refreshCurrentPage').click(function () {
        var grid = $("#grid");
        var options = grid.datagrid("getPager").data("pagination").options;
        var currPageNumber = options.pageNumber;
        bindSerarchLickEvent(currPageNumber);
    });
    //刷新计划采购数量
    $("#lblNum").click(refreshNum);

    //返回
    $("#back").click(function () {
        location.href = "/Purchase/Plan/Index";
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
        var planID = $("#hdnPlanID").val();
        showLocalWindow("添加计划采购商品", "/Purchase/PlanItem/Add?warehouseCode=" + warehouseCode + "&billNo=" + billNo + "&planID=" + planID, 700, 450, true, false, false);
    });
    //导入
    $('#import').click(function () {
        var planID = $("#hdnPlanID").val();
        var billNo = $("#hdnBillNo").val();
        var warehouseCode = $("#hdnWarehouseCode").val();
        showLocalWindow("导入商品", "/Purchase/PlanItem/Import?planID=" + planID + "&billNo=" + billNo + "&warehouseCode=" + warehouseCode, 400, 180, true, false, false);
    });
    //生成采购单
    $('#generation').click(function () {
        var planID = $("#hdnPlanID").val();
        var keyWordType = $("#keyWordType").combobox('getValue');
        var keyWord = $("#txtKeyWord").val();
        var noSupplier = $("#noSupplier").attr("checked") ? 1 : 0;
        var rows = $("#grid").datagrid("getSelections");
            var ids = [];
            var isNoSupplier = false;
            for (var i = 0; i < rows.length; i++) {
                ids.push(rows[i].ID);
                if (rows[i].AliasName == null) {
                    isNoSupplier = true;
                }
            }
            if (isNoSupplier) {
                $.MsgBox.Alert("提示", "有商品没有选择供应商，不能采购！");
                return false;
            }
            $.messager.confirm('提示', "生成采购单后不能再修改采购计划单，确认采购？", function (r) {
                if (r) {
                    $.ajax({  
                        url: "/Purchase/PlanItem/Generation?planID=" + planID + "&keyWordType=" + keyWordType + "&keyWord=" + keyWord + "&noSupplier=" + noSupplier + "&ids=" + ids.join(','),
                        type: "GET",
                        cache: false,
                        success: function (r) {
                            var map = $.parseJSON(r);
                            if (map.result == 1) {
                                $.messager.confirm('提示', "成功生成" + map.purchaseOrderCount + "条采购单，可以在采购单列表查看及操作。<br>按确定查看采购单？", function (r) {
                                    if (r) {
                                        var src = "/Purchase/Purchase/index?ram=" + Math.random();
                                        var title = '<span class="nav">  <span class="icon-empty" style=" width:16px; height:16px;">&nbsp;&nbsp;&nbsp;&nbsp;</span> 采购单</span>';
                                        var isExist = parent.$("#worktab").tabs('exists', title);
                                        if (!isExist) {
                                            parent.$('#worktab').tabs('add', {
                                                title: title,
                                                content: '<iframe id="frmWorkAreaAdd00511" data-options="fit:true" frameborder="0" style="  width: 100%; height: 100%; overflow: hidden;" src="' + src + '"></iframe>',
                                                closable: true
                                            });
                                        } else {
                                            parent.$("#worktab").tabs('select', title);
                                            var targetTab = parent.$("#worktab").tabs("getSelected");
                                            parent.$('#worktab').tabs('update', {
                                                tab: targetTab,
                                                options: {
                                                    content: '<iframe id="frmWorkAreaAdd00511" scrolling="no" data-options="fit:true" frameborder="0" style="  width: 100%; height: 100%; overflow: hidden;" src="' + src + '"></iframe>'
                                                }
                                            });
                                        }
                                    }
                                })
                                $('#refreshCurrentPage').click();
                            } else {
                                $.MsgBox.Alert("提示", map.message);
                            }
                        },
                        error: function () {
                            $.MsgBox.Alert("提示", "生成采购单失败！");
                        }
                    });
                }
            });
        

    });
    //删除
    $("#del").click(function () {
        if ($(this).hasClass('unclick')) return false;
        var rows = $("#grid").datagrid("getSelections");
        if (rows.length > 0) {
            var ids = [];
            for (var i = 0; i < rows.length; i++) {
                ids.push(rows[i].ID);
            }
            $.messager.confirm('提示', "确认删除这 " + ids.length + " 条商品吗？", function (r) {
                if (r) {
                    $.ajax({
                        url: "/Purchase/PlanItem/Delete?planID=" + $("#hdnPlanID").val() + "&ids=" + ids.join(','),
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


});

//绑定搜索按钮的的点击事件
function bindSerarchLickEvent(pageNumber) {
    //得到用户输入的参数
    var queryData = {
        keyWordType: $("#keyWordType").combobox('getValue'),
        keyWord: $("#txtKeyWord").val(),
        noSupplier: $("#noSupplier").attr("checked") ? 1 : 0,
    }
    //将值传递给
    initTable(queryData, pageNumber);
}
//加载列表
function initTable(queryData, pageNumber) {
    $('#grid').datagrid({
        url: '/Purchase/PlanItem/Search?planID=' + $("#hdnPlanID").val() + '&ram=' + Math.random(),
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
			{ title: '当前可用库存', field: 'KyNum', width: 150, align: 'center', sortable: false},
			{ title: '计划采购数量', field: 'Num', width: 150, align: 'center', sortable: false },
			{ title: '已采购数量', field: 'PurchasedNum', width: 150, align: 'center', sortable: false },
			{
			    title: '供应商', field: 'AliasName', width: 150, align: 'center',
			    formatter: function (value, row, index) {
			        if (value == null) {
			            value = "点击选择";
			        }
			        if (row.PurchasedNum > 0) {
			            return value;
			        } else {
			            return '<a href="javascript:void(0)" onclick=\'EditPlanItemSupplier(' + row.ID + ',' + row.ProductsSkuID + ')\'>' + value + '</a>';
			        }
			    },
			    sortable: true
			}
        ]],
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
//编辑供应商
function EditPlanItemSupplier(planItemID, productsSkuID) {
    showLocalWindow("选择供应商", "/Purchase/PlanItem/EditPlanItemSupplier?planItemID=" + planItemID + "&productsSkuID=" + productsSkuID, 620, 350, true, false, false);
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
    var purchaseOrderCount = $("#hdnPurchaseOrderCount").val();
    switch(status)
    {
        case "10":
            //已采购
            if (purchaseOrderCount > 0) {
                $("#import").hide();
                $("#add").hide();
                $("#del").hide();
                $("#generation").show();
                $("#grid").datagrid('showColumn', "KyNum");
            } else {
                //未采购
                $("#import").show();
                $("#add").show();
                $("#del").show();
                $("#generation").show();
                $("#grid").datagrid('showColumn', "KyNum");
            }
            break;
        case "20":
            //已结束
            $("#import").hide();
            $("#add").hide();
            $("#generation").hide();
            $("#del").hide();
            $("#grid").datagrid('hideColumn', "KyNum");
            break;
    }
}

function refreshNum() {
    $.ajax({  
        url: "/Purchase/PlanItem/GetNum?planID=" + $("#hdnPlanID").val(),
        type: "GET",
        cache: false,
        success: function (r) {
            var map = $.parseJSON(r);
            $("#lblNum").text(map.num);
        },
        error: function () {
            $.MsgBox.Alert("提示", "读取计划采购数量失败！");
        }
    });
}