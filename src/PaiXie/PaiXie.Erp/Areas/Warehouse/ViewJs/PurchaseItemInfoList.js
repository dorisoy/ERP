//初始化
$(function () {
    search(1);

    //返回
    $("#back").click(function () {
        location.href = "/Warehouse/Purchase/Index";
    });

    //搜索
    $('#btnSearch').click(function () {
        search(1);
    });

    //清空条件
    $('#btnReset').click(function () {
        window.location.reload(true);
    });

    //入库
    $('#storage').click(function () {
        var ids = [];
        var rows = $("#grid").datagrid("getSelections");
        for (var i = 0; i < rows.length; i++) {
            ids.push(rows[i].ID);
        }
        var queryData;
        if (ids.length == 0) {
            queryData = {
                keyWordType: $("#keyWordType").combobox('getValue'),
                keyWord: $("#txtKeyWord").val()
            }
        }
        parent.$.messager.confirm('提示', "本操作会创建一条与本采购单关联的入库单，确认创建？", function (r) {
            if (r) {
                $.ajax({
                    url: "/Warehouse/PurchaseItem/Storage?purchaseID=" + $("#hdnPurchaseID").val() + "&ids=" + ids.join(','),
                    type: "GET",
                    cache: false,
                    data: queryData,
                    success: function (r) {
                        var map = $.parseJSON(r);
                        if (map.result == 1) {
                            parent.$.messager.confirm('提示', "成功生成入库单，可以在入库单列表查看及操作。<br>是否查看入库单？", function (r) {
                                if (r) {
                                    var src = "/Warehouse/WarehouseInStock/index?ram=" + Math.random();
                                    var title = '<span class=\"nav\">  <span class=\"icon-empty\" style=\"width: 16px; height: 16px;\">&nbsp;&nbsp;&nbsp;&nbsp;</span> 入库单</span>';
                                    //拼接一个Iframe标签
                                    var str = '<iframe id="frmWorkAreaAdd001111" scrolling="no" data-options="fit:true" frameborder="0" style="  width: 100%; height: 100%; overflow: hidden;" src="' + src + '"></iframe>';
                                    var isExist = parent.$("#worktab").tabs('exists', title);
                                    if (!isExist) {
                                        parent.$('#worktab').tabs('add', {
                                            title: title,
                                            content: str,
                                            closable: true
                                        });
                                    } else {
                                        parent.$("#worktab").tabs('select', title);
                                        var targetTab = parent.$("#worktab").tabs("getSelected");
                                        parent.$('#worktab').tabs('update', {
                                            tab: targetTab,
                                            options: {
                                                content: str
                                            }
                                        });
                                    }
                                }
                            });
                            $('#refresh').click();
                        } else {
                            $.MsgBox.Alert("提示", map.message);
                        }
                    },
                    error: function () {
                        $.MsgBox.Alert("提示", "入库失败！");
                    }
                });
            }
        });
        return false;
    });
});

//搜索
function search(pageNumber) {
    //得到用户输入的参数
    var queryData = {
        keyWordType: $("#keyWordType").combobox('getValue'),
        keyWord: $("#txtKeyWord").val(),
        purchaseID: $("#hdnPurchaseID").val(),
        suppliersID: $("#hdnSuppliersID").val(),
    }
    //将值传递给
    initTable(queryData, pageNumber);
}

//加载列表
function initTable(queryData, pageNumber) {
    $('#grid').datagrid({
        url: '/Warehouse/PurchaseItem/Search?&ram=' + Math.random(),
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
        },
        onUnselect: function (rowIndex, rowData) {
        },
        columns: [[
                    { field: 'ck', width: 52, checkbox: true },   //选择
                    { title: '主键', field: 'ID', hidden: true },
                    { title: '商品编码', field: 'ProductsCode', width: 150, align: 'center', sortable: true },
                    { title: '商品名称', field: 'ProductsName', width: 350, align: 'center', sortable: true },
                    { title: '属性', field: 'ProductsSkuSaleprop', width: 150, align: 'center', sortable: true },
                    { title: 'SKU码', field: 'ProductsSkuCode', width: 150, align: 'center', sortable: true },
                    { title: '当前库存', field: 'KyNum', width: 150, align: 'center', sortable: true },
                    { title: '采购数量', field: 'Num', width: 150, align: 'center', sortable: true },
                    { title: '预计金额', field: 'ExpectedAmount', width: 150, align: 'center', formatter: formatcost, sortable: false },
                    { title: '入库数量', field: 'InStockNum', width: 150, align: 'center', sortable: true }
        ]]
    });
}

function formatcost(val, row) {
    return '<span style="color:#008c23;">' + val.toFixed(3) + '</span>';
}