//初始化
$(function () {

    //初始化数据
    BindDictItem("Warehouse", "Warehouse");
    //选中特定值
    $("#Warehouse").combobox('setValue', '0');
    var queryData = {
        noPurchase:  1,
        purchased: 1,
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
    //清空条件
    $('#btnReset').click(function () {
        window.location.reload(true);
    });
    //添加采购计划单
    $("#add").click(function () {
        showLocalWindow("添加采购计划单", "/Purchase/Plan/Add", 425, 240, true, false, false);
    });
    //删除
    $("#del").click(function () {
        if ($(this).hasClass('unclick')) return false;
        var rows = $("#grid").datagrid("getSelections");
        var ispurchased = false;
        if (rows.length > 0) {
            var ids = [];
            for (var i = 0; i < rows.length; i++) {
                ids.push(rows[i].ID);
                if (rows[i].PurchaseOrderCount > 0) ispurchased = true;
            }
            if (ispurchased) {
                $.MsgBox.Alert("提示", "计划单已生成采购单，不能删除！");
                return false;
            }
            $.messager.confirm('提示', "确认删除这 " + ids.length + " 条采购计划单？", function (r) {
                if (r) {
                    $.ajax({
                        url: "/Purchase/Plan/Delete?ids=" + ids.join(','),
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
        else {
            $.MsgBox.Alert("提示", "请选择采购计划单！");
        }
    });
});

//绑定搜索按钮的的点击事件
function bindSerarchLickEvent(pageNumber) {

    //得到用户输入的参数
    var queryData = {
        keyWordType: $("#keyWordType").combobox('getValue'),
        keyWord: $("#txtKeyWord").val(),
        warehouseCode: $("#Warehouse").combobox('getValue'),
        noPurchase: $("#noPurchase").attr("checked") ? 1 : 0,
        purchased: $("#purchased").attr("checked") ? 1 : 0,
        end: $("#end").attr("checked") ? 1 : 0
    }
    //将值传递给
    initTable(queryData, pageNumber);
}

//加载列表
function initTable(queryData, pageNumber) {
    $('#grid').datagrid({
        url: '/Purchase/Plan/Search?ram=' + Math.random(),
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
        {
            title: '采购计划单号', field: 'BillNo', width: 180, align: 'center',
            formatter: function (value, row, index) {
                return '<a href="javascript:void(0)" onclick=\'manage("' + row.WarehouseName + '",' + row.ID + ')\'>' + row.BillNo + '</a>';
            }, sortable: false
        },
         {
             title: '计划单名称', field: 'Name', width: 150, align: 'center', sortable: false
         },

          { title: '仓库名称', field: 'WarehouseName', width: 100, align: 'center', sortable: false },
          { title: '计划数量', field: 'Num', width: 100, align: 'center', sortable: false },
          { title: '已采购数量', field: 'PurchasedNum', width: 100, align: 'center', sortable: false },
          { title: '采购单', field: 'PurchaseOrderCount', width: 100, align: 'center', sortable: false },
          { title: '创建时间', field: 'CreateDate', width: 180, align: 'center', sortable: true },
          { title: '创建人', field: 'CreatePerson', width: 140, align: 'center', sortable: true },
          {
              title: '状态', field: 'Status', width: 100, align: 'center',
              formatter: function (value, row, index) {
                  //状态 未采购(采购单数为0)，已采购(采购单数大于0)，已结束
                  var state = "";
                  if (value == 20) {
                      state = "已结束";
                  } else if (row.PurchaseOrderCount > 0) {
                      state = "已采购";
                  } else if (value == 10) {
                      state = "未采购";
                  } else {
                      //nothing
                  }
                  return state;
              },
              sortable: true
          },
        {
            title: '操作', field: 'Permit', width: 155, align: 'center',
            formatter: function (value, row, index) {
                var endHtml = "";
                var opearTxt = "";
                if (row.Status == 20) {
                    opearTxt = "查看";
                } else if (row.PurchaseOrderCount > 0) {
                    opearTxt = "查看";
                    endHtml = ' | <a href="javascript:void(0)" onclick=\'end(' + row.ID + ')\'>结束</a>';
                } else {
                    opearTxt = "管理";
                }

                html = '<a href="javascript:void(0)" onclick=\'manage("' + row.WarehouseName + '",' + row.ID + ')\'>' + opearTxt + '</a>' + endHtml;
                return html;
            },
            sortable: false
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
//控制按钮是否可用
function showControl() {
    var rows = $("#grid").datagrid("getSelections");
    var ids = [];
    var ispurchased = false;
    for (var i = 0; i < rows.length; i++) {
        ids.push(rows[i].ID);
        if (rows[i].PurchaseOrderCount > 0) ispurchased = true;
    }
    if (ids.length <= 0 || ispurchased) {
        //禁用删除
        $("#del").addClass("unclick");
    } else {
        //启用删除
        $('#del').removeClass('unclick');
    }
}
//查看或管理
function manage(warehouseName, planID) {
    location.href = "/Purchase/PlanItem/Index?warehouseName=" + warehouseName + "&planID=" + planID + "&ram=" + Math.random();
}
//结束操作
function end(planID) {
    $.messager.confirm('提示', "确认结束？", function (r) {
        if (r) {
            $.ajax({
                url: "/Purchase/Plan/End?ids=" + planID,
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