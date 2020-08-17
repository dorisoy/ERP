//初始化
$(function () {
    initTable("", 1);

    //刷新当前页
    $('#refreshCurrentPage').click(function () {
        var grid = $("#grid");
        var options = grid.datagrid("getPager").data("pagination").options;
        var currPageNumber = options.pageNumber;
        search(currPageNumber);
    });
    
    //刷新计划采购数量
    $("#lblNum").click(refreshNum);

    //返回
    $("#back").click(function () {
        location.href = "/Warehouse/PurchasePlan/Index";
    });

    //搜索
    $('#btnSearch').click(function () {
        search(1);
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
        showLocalWindow("添加计划采购商品", "/Warehouse/PurchasePlanItem/Add?&billNo=" + billNo + "&planID=" + planID, 700, 450, true, false, false);
    });

    //导入
    $('#import').click(function () {
        var planID = $("#hdnPlanID").val();
        var billNo = $("#hdnBillNo").val();
        var warehouseCode = $("#hdnWarehouseCode").val();
        showLocalWindow("导入商品", "/Warehouse/PurchasePlanItem/Import?planID=" + planID + "&billNo=" + billNo, 400, 180, true, false, false);
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
                        url: "/Warehouse/PurchasePlanItem/Delete?planID=" + $("#hdnPlanID").val() + "&ids=" + ids.join(','),
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

//加载列表
function initTable(queryData, pageNumber) {
    $('#grid').datagrid({
        url: '/Warehouse/PurchasePlanItem/Search?planID=' + $("#hdnPlanID").val() + '&ram=' + Math.random(),
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
			{ title: '当前可用库存', field: 'KyNum', width: 150, align: 'center', sortable: false, hddden: $("#hdnStatus").val() == "20" ? false : true },
			{ title: '计划采购数量', field: 'Num', width: 150, align: 'center', sortable: false }
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

//搜索
function search(pageNumber) {
    //得到用户输入的参数
    var queryData = {
        keyWordType: $("#keyWordType").combobox('getValue'),
        keyWord: $("#txtKeyWord").val()
    }
    //将值传递给
    initTable(queryData, pageNumber);
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
    if(status!="0")
    {
        $("#import").hide();
        $("#add").hide();
        $("#del").hide();
    }
}

function refreshNum() {
    $.ajax({
        url: "/Warehouse/PurchasePlanItem/GetNum?planID=" + $("#hdnPlanID").val(),
        type: "GET",
        cache: false,
        success: function (r) {
            var map = $.parseJSON(r);
            $("#lblNum").text(map.num);
        },
        error: function () {
            $.MsgBox.Alert("提示", "读取计划采购数量失败！", 1000);
        }
    });
}