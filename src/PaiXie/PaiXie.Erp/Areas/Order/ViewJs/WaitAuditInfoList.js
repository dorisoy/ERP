$(function () {
    BindDictItem("shop", "shop");
    $('#shop').combobox('select', "0");
    search(1);

    //刷新
    $('#refresh').click(function () {
        search(1);
    });

    //刷新当前页
    $('#refreshCurrentPage').click(function () {
        var grid = $("#grid");
        var options = grid.datagrid("getPager").data("pagination").options;
        var currPageNumber = options.pageNumber;
        search(currPageNumber);
    });

    //批量分配仓库
    $('#distributionWarehouse').click(function () {
        distributionWarehouse();
    });

    //搜索
    $('#btnSearch').click(function () {
        search(1);
    });

    //清空条件
    $('#btnReset').click(function () {
        window.location.reload();
    });
});

//加载列表
function initTable(queryData, pageNumber) {
    $('#grid').datagrid({
        url: '/Order/WaitAudit/Search?ram=' + Math.random(),
        height: '100%',
        width: '100%',
        nowrap: false,
        autoRowHeight: false,
        striped: true,
        collapsible: true,
        pagination: true,
        fitColumns: true,
        pageNumber: pageNumber,
        fit: true, //datagrid自适应宽度
        fitColumn: false, //列自适应宽度
        border: false,
        pageSize: 15,
        pageList: [15, 20, 30],
        rownumbers: false,
        remoteSort: false,
        queryParams: queryData,  //异步查询的参数
        onSelect: function (rowIndex, rowData) {
        },
        onUnselect: function (rowIndex, rowData) {
        },
        columns: [[
			{ field: 'ck', width: 50, checkbox: true },
			{ title: '', field: 'ID', hidden: true },
            {
                title: '订单编号', field: 'ErpOrderCode', width: 300, align: 'center',
                formatter: function (value, row, index) {
                    var html = '<a href="javascript:void(0)" onclick="showOrderDetails(' + row.ErpOrderCode + ')">' + value + '</a>';
                    if (row.IsReject > 0) {
                        html += '<img src="../../Content/images/back.gif" width="16px" height="16px" alt="" title="驳回的订单" />';
                    }
                    html += '<br/>(' + row.ShopName + ')';
                    return html;
                },
                sortable: true
            },
			{
			    title: '下单时间', field: 'Created', width: 150, align: 'center',
			    formatter: function (value, row) {
			        if (row.CreateType == 1) {
			            return row.CreateDate;
			        }
			        else {
			            return value;
			        }
			    },
			    sortable: true
			},
			{ title: '下单总额', field: 'ReceivableAmount', width: 150, align: 'center', formatter: function (value, row) { return '￥' + value.toFixed(3) }, sortable: true },
			{ title: '收件人', field: 'BuyName', width: 100, align: 'center', sortable: true },
            { title: '订单商品', field: 'ProductsNum', width: 100, align: 'center', sortable: true },
            { title: '发货物流', field: 'LogisticsName', width: 150, align: 'center', sortable: true },
            { title: '货到付款', field: 'IsCod', width: 100, align: 'center', formatter: function (value, row) { return (value == 0) ? "否" : "是" }, sortable: true },
            { title: '需要发票', field: 'IsNeedInvoice', width: 100, align: 'center', formatter: function (value, row) { return (value == 0) ? "否" : "是" }, sortable: true },
            {
                title: '买家留言', field: 'BuyMessage', width: 100, align: 'center',
                formatter: function (value, row) {
                    if (value == "" || value == null) {
                        return "-";
                    }
                    else {
                        return value;
                    }
                },
                sortable: true
            },
            {
                title: '卖家备注', field: 'SellerRemark', width: 100, align: 'center',
                formatter: function (value, row) {
                    if (value == "" || value == null) {
                        return "-";
                    }
                    else {
                        return value;
                    }
                },
                sortable: true
            },
            {
                title: '操作', field: 'Permit', width: 250, align: 'center',
                formatter: function (value, row) {
                    var html = '<a href="javascript:void(0)" onclick="showOrderDetails(' + row.ErpOrderCode + ')">查看详情</a>';
                    html += ' | <a href="javascript:void(0)" onclick="showDistributionWarehouse(' + row.ErpOrderCode + ')">分配仓库</a>';
                    html += ' | <a href="javascript:void(0)" onclick="cancelOrder(' + row.ErpOrderCode + ')">取消</a>';
                    return html;
                },
                sortable: true
            },
            {
                title: '备注', field: 'IsSysRemark', width: 70, align: 'center',
                formatter: function (value, row) {
                    var html = '';
                    if (value == 1) {
                        html += '<img src="../../Content/images/flag-red.png" width="16px" height="16px" alt="" title="点击查看备注" onclick="addRemarks(' + row.ErpOrderCode + ');" />';
                    }
                    else {
                        html += '<img src="../../Content/images/flag-gray.png" width="16px" height="16px" alt="" title="点击添加备注" onclick="addRemarks(' + row.ErpOrderCode + ');"/>';
                    }
                    return html;
                },
                sortable: true
            }
        ]],
        onLoadSuccess: function (data) { DataGridNoData(this); }
    });
}

//搜索
function search(pageNumber) {
    //得到用户输入的参数
    var queryData = {
        keyWordType: $("#keyWordType").combobox('getValue'),
        keyWord: $("#txtKeyWord").val(),
        isCod: $("#isCod").combobox('getValue'),
        isNeedInvoice: $("#isNeedInvoice").combobox('getValue'),
        isRemark: $("#isRemark").combobox('getValue'),
        shopID: $("#shop").combobox('getValue'),
        isNormal: $("#chkIsNormal").attr("checked") ? 1 : 0,
        isRefund: $("#chkIsRefund").attr("checked") ? 1 : 0,
        isReject: $("#chkIsReject").attr("checked") ? 1 : 0,
        isHang: $("#chkIsHang").attr("checked") ? 1 : 0,
    }
    //将值传递给
    initTable(queryData, pageNumber);
}

//分配仓库
function showDistributionWarehouse(erpOrderCode) {
    var title = '分配仓库' + erpOrderCode;
    var src = "/Order/DistributionWarehouse/Index?erpOrderCode=" + erpOrderCode;
    var mid = "DistributionWarehouse" + erpOrderCode;
    //拼接一个Iframe标签
    var str = '  <iframe id="frmWorkArea' + mid + '" data-options="fit:true" frameborder="0" style="  width: 100%; height: 100%; overflow: hidden;" src="' + src + '"></iframe>   ';
    //首先判断用户是否已经单击了此项，如果单击了直接获取焦点，负责打开
    var isExist = parent.$("#worktab").tabs('exists', title);
    if (!isExist) {
        parent.$('#worktab').tabs('add', { title: title, content: str, closable: true });
    }
    else {
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

//取消订单
function cancelOrder(erpOrderCode) {
    $.messager.confirm('提示', '取消后不能恢复，确认取消该订单吗？', function (r) {
        if (r) {
            $.ajax({
                url: "/Order/WaitAudit/CancelOrder",
                type: "GET",
                cache: false,
                data: { erpOrderCode: erpOrderCode },
                success: function (r) {
                    var map = $.parseJSON(r);
                    if (map.result == 1) {
                        $.MsgBox.Alert("提示", "取消成功！", 0);
                        $('#btnSearch').click();
                    } else {
                        $.MsgBox.Alert("提示", map.message);
                    }
                },
                error: function () {
                    $.MsgBox.Alert("提示", "取消失败！");
                }
            });
        }
    });
}

//添加备注
function addRemarks(erpOrderCode) {
    showLocalWindow("备注", "/Order/Details/AddRemark?erpOrderCode=" + erpOrderCode, 540, 350, true, false, false);
    $('#localWin').window({
        onClose: function () {
            $('#refreshCurrentPage').click();
        }
    });
}

//批量分配仓库
function distributionWarehouse() {
    var rows = $("#grid").datagrid("getSelections");
    if (rows.length > 0) {
        var ids = [];
        for (var i = 0; i < rows.length; i++) {
            ids.push(rows[i].ID);
        }
        if (ids.length > 0)
            $.messager.confirm('提示', "确认分配这 " + ids.length + " 单？", function (r) {
                if (r) {
                    $.ajax({
                        url: "/Order/WaitAudit/BatchDistributionWarehouse?ids=" + ids.join(','),
                        type: "GET",
                        cache: false,
                        success: function (r) {
                            var map = $.parseJSON(r);
                            if (map.result == 1) {
                                $.MsgBox.Alert("提示", map.message);
                                $('#refreshCurrentPage').click();
                            } else {
                                $.MsgBox.Alert("提示", map.message);
                            }
                        },
                        error: function () {
                            $.MsgBox.Alert("提示", "分配仓库失败！");
                        }
                    });
                }
            });
    }
    else {
        $.MsgBox.Alert("提示", "请选择订单！");
    }
}