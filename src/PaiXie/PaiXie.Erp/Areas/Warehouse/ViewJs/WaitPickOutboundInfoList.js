var isWaitPurchase = $("#hdnIsWaitPurchase").val();
var gridID = "grid" + isWaitPurchase;
//初始化
$(function () {
    //绑定店铺下拉列表
    BindDictItem("shop", "Shop");
    $('#shop').combobox("setValue", '0');
    //绑定快递下拉列表
    $('#express').combobox({
        url: '/Warehouse/Express/JsonTree',
        valueField: 'Value',
        textField: 'Text',
        onLoadSuccess: function () { //数据加载完毕事件
            $("#express").combobox('select', "0");
        } 
    });
    //刷新
    $('#refresh').click(function () {
        bindSerarchLickEvent(1);
    });
    //安排打印
    $('#arrangePrint').click(function () {
        arrangePrint(this, undefined);
    });
    //生成采购计划单
    $('#generatePurchasePlan').click(function () {
        generatePurchasePlan(this);
    });
    //刷新当前页
    $('#refreshCurrentPage').click(function () {
        var grid = $("#" + gridID);
        var options = grid.datagrid("getPager").data("pagination").options;
        var currPageNumber = options.pageNumber;
        bindSerarchLickEvent(currPageNumber);
    });
    //清空条件
    $('#btnReset').click(function () {
        location.href = "/Warehouse/Outbound/WaitPickIndex?isWaitPurchase=" + isWaitPurchase + "&ram=" + Math.random();
    });
    //搜索
    $('#btnSearch').click(function () {
        bindSerarchLickEvent(1);
    });
    $('#tt').tabs({
        onSelect: function (title, index) {
            switch (title) {
                case "出库单":
                    isWaitPurchase = 0;
                    gridID = "grid" + isWaitPurchase;
                    bindSerarchLickEvent(1);
                    break;
                case "待采出库单":
                    isWaitPurchase = 1;
                    gridID = "grid" + isWaitPurchase;
                    bindSerarchLickEvent(1);
                    break;
            }
            $("#hdnIsWaitPurchase").val(isWaitPurchase);
        }
    });
    if (isWaitPurchase == 0) {
        bindSerarchLickEvent(1);
    } else {
        //选中待采出库单选项卡
        $("#tt").tabs("select", 1);
    }
});
//绑定搜索按钮的的点击事件
function bindSerarchLickEvent(pageNumber) {
    //得到用户输入的参数
    var queryData = {
        keyWordType: $("#keyWordType").combobox('getValue'),
        keyWord: $("#txtKeyWord").val(),
        expressID: $("#express").combobox('getValue'),
        isNeedInvoice: $("#isNeedInvoice").combobox('getValue'),
        messageRemark: $("#messageRemark").combobox('getValue'),
        isCod: $("#isCod").combobox('getValue'),
        shopID: $("#shop").combobox('getValue'),
        containNormal: $("#chkNormal").attr("checked") ? 1 : 0,
        containApplyRefund: $("#chkApplyRefund").attr("checked") ? 1 : 0,
        containHang: $("#chkIsHang").attr("checked") ? 1 : 0,
        isWaitPurchase: isWaitPurchase
    }
    //将值传递给
    initTable(queryData, pageNumber);
    return false;
}
//搜索申请退款出库单
function searchRefund() {
    $("#keyWordType").combobox('setValue', '订单编号');
    $("#txtKeyWord").val('');
    $("#express").combobox('setValue', 0);
    $("#isNeedInvoice").combobox('setValue', -1);
    $("#messageRemark").combobox('setValue', -1);
    $("#isCod").combobox('setValue', -1);
    $("#shop").combobox('setValue', 0);
    $("#chkApplyRefund").attr("checked", true);
    $("#chkIsHang").attr("checked", true);
    var queryData = {
        isApplyRefund: 1,
        isWaitPurchase: isWaitPurchase
    }
    //将值传递给
    initTable(queryData, 1);
    return false;
}
//获取申请退款笔数
function getApplyRefundCount() {
    $.ajax({
        url: '/Warehouse/Outbound/GetApplyRefundCount?status=0&isWaitPurchase=' + isWaitPurchase + '&ram=' + Math.random(),
        type: "GET",
        async: false,
        cache: false,
        success: function (r) {
            var map = $.parseJSON(r);
            $("#lblApplyRefundCount" + isWaitPurchase).text(map.appRefundCount);            
        },
        error: function () {
            $.MsgBox.Alert("提示", "获取申请退款笔数失败！", 1000);
        }
    });
}
//加载列表
function initTable(queryData, pageNumber) {
    $('#' + gridID).datagrid({
        url: '/Warehouse/Outbound/SearchWaitPick?ram=' + Math.random(),
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
        sortName: 'ID',
        sortOrder: 'desc',
        remoteSort: false,
        idField: 'ID',
        onSelect: function (rowIndex, rowData) {
            showControl();
        },
        onUnselect: function (rowIndex, rowData) {
            showControl();
        },
        queryParams: queryData,  //异步查询的参数
        columns: [[
        { field: 'ck', checkbox: true },   //选择
        { title: '主键', field: 'ID', width: 10, sortable: true, hidden: true },
        {
            title: '出库单号', field: 'BillNo', width: 170, align: 'center',
            formatter: function (value, row) {
                var html = '<a href="javascript:void(0);" onclick=\'showOutboundDetail("' + value + '")\'>' + value + '</a>';
                if (row.ReturnCount > 0) {
                    html += '<img src="../../Content/images/back.gif" width="16px" height="16px" alt="" title="返回的出库单" />';
                }
                html += '<br/><span style="color:#999;">(' + row.ShopName + ')</span>';
                if (isWaitPurchase == 1 && row.IsPurchasePlan == 1) {
                    html += '<br/><span style="color:red;">(已采)</span>';
                }
                return html;
            }, sortable: true
        },
        {
            title: '订单编号', field: 'ErpOrderCode', width: 130, align: 'center',
            formatter: function (value, row, index) {
                var html = value;
                html += '<br/><span style="color:#999;">(' + row.BuyName + ')</span>';
                if (row.IsApplyRefund == 1) {
                    html += '<br/><span style="color:#ff0000;">[申请退款]</span>';
                }
                return html;
            },
            sortable: true
        },
        { title: '创建时间', field: 'CreateDate', width: 80, align: 'center', sortable: true },
        { title: '商品数量', field: 'ProductsNum', width: 60, align: 'center', sortable: true },
        {
            title: '发货快递', field: 'ExpressName', width: 140, align: 'center',
            formatter: function (value, row) {
                var html = (row.ExpressName == null || row.ExpressName == "" ? "未指定" : row.ExpressName)
                html += '<input type="hidden" id="hdnExpressID_' + row.ID + '" value="' + row.ExpressID + '"/>';
                return html;
            },
            sortable: false
        },
        {
            title: '货到付款', field: 'IsCod', width: 65, align: 'center',
            formatter: function (value, row) {
                return value == 1 ? "是" : "否";
            },
            sortable: false
        },
        {
            title: '需要发票', field: 'IsNeedInvoice', width: 65, align: 'center',
            formatter: function (value, row) {
                return value == 1 ? '<span style="color:#ff0000;">是</span>' : '否';
            },
            sortable: false
        },
        {
            title: '留言备注', field: 'messageRemark', width: 65, align: 'center',
            formatter: function (value, row, index) {
                var html = '';
                if (row.BuyMessage != '' || row.SellerRemark != '') {
                    var contentText = '';
                    if (row.BuyMessage != '') {
                        contentText += '买家留言：' + row.BuyMessage;
                    }
                    if (row.SellerRemark != '') {
                        if (contentText != '') {
                            contentText += '<br/>';
                        }
                        contentText += '卖家备注：' + row.SellerRemark;
                    }
                    html += '<div id="messageRemark_' + index + '" class="easyui-panel easyui-tooltip" title="' + contentText + '" style="color:#265cff;">有</div>';
                } else {
                    html += '<div>无</div>';
                }
                if (row.IsHang == 1) {
                    html += '<div id="hangRemark_' + index + '" class="easyui-panel easyui-tooltip" title="挂起备注：' + row.HangRemark + '" style="color:#ff0000;">[挂起]</div>';
                }
                return html;
            },
            sortable: false
        },
        {
            title: '要求到货时间', field: 'ExpectedDeliDate', width: 100, align: 'center',
            formatter: function (value, row) {
                var html = '';
                if (value != "0001-01-01 00:00:00") {
                    html = value.split(' ')[0];
                } else {
                    html = '－';
                }
                return html;
            },
            sortable: true
        },
        {
            title: '操作', field: 'Permit', width: 160, align: 'center',
            formatter: function (value, row) {
                var html = '<a href="javascript:void(0);" onclick="reject(' + row.ID + ');">驳回</a>';
                if (isWaitPurchase == 0) {
                    if (row.IsHang == 0) {
                        html += '<br/><a href="javascript:void(0);" onclick="setHang(0,' + row.ID + ');">标记挂起</a>';
                    } else {
                        html += '<a href="javascript:void(0);" onclick="setHang(1,' + row.ID + ');">取消挂起</a>';
                    }
                    if (row.IsHang == 0 && row.IsApplyRefund == 0) {
                        html += '&nbsp;|&nbsp;<a href="javascript:void(0);" onclick="arrangePrint(this,' + row.ID + ');">安排打印</a>';
                    } else {
                        html += '&nbsp;|&nbsp;<a href="javascript:void(0);" class="unclick" style="color:#999; cursor:default;">安排打印</a>';
                    }
                } else {
                    html += '&nbsp;|&nbsp;<a href="javascript:void(0);" onclick=\'searchStock("' + row.ID + '")\'>查询</a><br/>';
                    html += '<a href="javascript:void(0);" onclick=\'showOutboundDetail("' + row.BillNo + '")\'>查看</a>';
                    html += '&nbsp;|&nbsp;<a href="javascript:void(0);" onclick=\'splitOutbound("' + row.ID + '")\'>拆分</a>';
                }
                return html;
            },
            sortable: false
        }
        ]],
        onLoadSuccess: function (data) {
            getApplyRefundCount();
            $.each(data.rows, function (index, element) {
                //留言备注
                $('#messageRemark_' + index).tooltip({
                    position: 'top',
                    onShow: function () {
                        $(this).tooltip('tip').css({
                            backgroundColor: '#ffffea',
                            borderColor: '#fdcb99',
                            color: '#666666',
                            padding: '10px',
                            borderRadius: '0px'
                        });
                    }
                });
                //挂起备注
                $('#hangRemark_' + index).tooltip({
                    position: 'top',
                    onShow: function () {
                        $(this).tooltip('tip').css({
                            backgroundColor: '#ffffea',
                            borderColor: '#fdcb99',
                            color: '#666666',
                            padding: '10px',
                            borderRadius: '0px'
                        });
                    }
                });
            });
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

//驳回
function reject(ids) {
    showLocalWindow("出库单驳回", "/Warehouse/Outbound/Reject?ids=" + ids, 470, 300, true, false, false);
}
//拆分出库单
function splitOutbound(id) {
    showLocalWindow("拆分待采", "/Warehouse/Outbound/Split?id=" + id, 470, 280, true, false, false);
}
function searchStock(id) {
    $.ajax({
        url: "/Warehouse/Outbound/SearchStock?id=" + id,
        type: "GET",
        cache: false,
        success: function (r) {
            var map = $.parseJSON(r);
            if (map.result == 1) {
                $.MsgBox.Alert("提示", map.message, 3000);
                $('#refreshCurrentPage').click();
            } else {
                $.MsgBox.Alert("提示", map.message);
            }
        },
        error: function () {
            $.MsgBox.Alert("提示", "查询失败！");
        }
    });
    
}
//挂起、取消挂起
function setHang(type, id) {
    var name = "";
    if (type == 0) {
        name = "挂起";
        showLocalWindow("出库单挂起", "/Warehouse/Outbound/Hang?id=" + id, 470, 300, true, false, false);
    } else {
        name = "取消挂起";
        $.ajax({
            url: "/Warehouse/Outbound/SetHang?type=" + type + "&id=" + id,
            type: "GET",
            cache: false,
            success: function (r) {
                var map = $.parseJSON(r);
                if (map.result == 1) {
                    $.MsgBox.Alert("提示", name + "成功！", 1000);
                    $('#refreshCurrentPage').click();
                } else {
                    $.MsgBox.Alert("提示", map.message);
                }
            },
            error: function () {
                $.MsgBox.Alert("提示", name + "失败！");
            }
        });
    }
}
//生成采购计划单
function generatePurchasePlan(obj) {
    if ($(obj).hasClass('unclick')) return false;
    var rows = $("#" + gridID).datagrid("getSelections");
    var isApplyRefund = false;
    var isPurchasePlan = false;
    var ids = [];
    for (var i = 0; i < rows.length; i++) {
        ids.push(rows[i].ID);
        if (rows[i].IsApplyRefund == 1) isApplyRefund = true;
        if (rows[i].IsPurchasePlan == 1) isPurchasePlan = true;
    }
    if (isApplyRefund) {
        $.MsgBox.Alert("提示", "申请退款出库单，不能生成采购计划单！");
    } else {
        var strParam = '?ids=' + ids + "&ram=" + Math.random();
        if (ids == "") {
            var keyWordType = $("#keyWordType").combobox('getValue');
            var keyWord = $("#txtKeyWord").val();
            var expressID = $("#express").combobox('getValue');
            var isNeedInvoice = $("#isNeedInvoice").combobox('getValue');
            var messageRemark = $("#messageRemark").combobox('getValue');
            var isCod = $("#isCod").combobox('getValue');
            var shopID = $("#shop").combobox('getValue');
            var containNormal = $("#chkNormal").attr("checked") ? 1 : 0;
            var containApplyRefund = $("#chkApplyRefund").attr("checked") ? 1 : 0;
            var containHang = $("#chkIsHang").attr("checked") ? 1 : 0;
            var isWaitPurchase = isWaitPurchase;
            strParam += '&keyWordType=' + keyWordType + '&keyWord=' + keyWord +
            '&expressID=' + expressID + '&isNeedInvoice=' + isNeedInvoice + '&messageRemark=' + messageRemark + '&isCod=' + isCod +
            '&shopID=' + shopID + '&containNormal=' + containNormal + '&containApplyRefund=' + containApplyRefund + '&containHang=' + containHang + '&isWaitPurchase=' + isWaitPurchase;
        }
        if (!isPurchasePlan) {
            //前端没有检测到已采，ajax后端再检查一次
            isPurchasePlan = ajaxCheckHasPurchase(strParam);
        }
        if (isPurchasePlan) {
            $.messager.confirm('提示', "部分出库单已采，是否继续？", function (r) {
                if (r) {
                    ajaxGeneratePurchasePlan(strParam);
                }
            });
        } else {
            ajaxGeneratePurchasePlan(strParam);
        }
    }
}
//生成采购计划单ajax
function ajaxGeneratePurchasePlan(strParam) {
    $.ajax({
        url: "/Warehouse/Outbound/Generation" + strParam,
        type: "GET",
        cache: false,
        success: function (r) {
            var map = $.parseJSON(r);
            if (map.result == 1) {
                $.messager.confirm('提示', "成功生成采购计划单，可以在采购计划单列表查看及操作。<br>按确定查看采购计划单？", function (r) {
                    if (r) {
                        var src = "/Warehouse/PurchasePlan/index?ram=" + Math.random();
                        var title = '<span class="nav">  <span class="icon-empty" style=" width:16px; height:16px;">&nbsp;&nbsp;&nbsp;&nbsp;</span> 采购计划单</span>';
                        //拼接一个Iframe标签
                        var str = '  <iframe id="frmWorkAreaAdd00150" data-options="fit:true" frameborder="0" style="  width: 100%; height: 100%; overflow: hidden;" src="' + src + '"></iframe>'
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
                $('#refreshCurrentPage').click();
            } else {
                $.MsgBox.Alert("提示", map.message);
            }
        },
        error: function () {
            $.MsgBox.Alert("提示", "生成采购计划单失败！");
        }
    });
}
//ajax检查是否包含已采
function ajaxCheckHasPurchase(strParam) {
    var hasPurchase = false;
    $.ajax({
        url: "/Warehouse/Outbound/GetPurchasedCount" + strParam,
        type: "GET",
        async: false,
        cache: false,
        success: function (r) {
            var map = $.parseJSON(r);
            if (map.purchasedCount > 0) {
                hasPurchase = true;
            }
        },
        error: function () {
        }
    });
    return hasPurchase;
}
//安排打印
function arrangePrint(obj, outboundID) {
    if ($(obj).hasClass('unclick')) return false;
    if (undefined != outboundID) {
        if (Number($("#hdnExpressID_" + outboundID).val()) > 0) {
            ajaxSavePrintBatch(outboundID);
        } else {
            showLocalWindow("安排打印", "/Warehouse/Outbound/ArrangePrint?ids=" + outboundID, 380, 250, true, false, false);
        }
    } else {
        var rows = $("#" + gridID).datagrid("getSelections");
        var isApplyRefund = false;
        var isHang = false;
        var hasManyExpress = false;
        if (rows.length > 0) {
            var ids = [];
            for (var i = 0; i < rows.length; i++) {
                ids.push(rows[i].ID);
                if (rows[i].IsApplyRefund == 1) isApplyRefund = true;
                if (rows[i].IsHang == 1) isHang = true;
                if (rows[i].ExpressID == 0) hasManyExpress = true;
                if (i > 0) {
                    if (rows[i].ExpressID != rows[i - 1].ExpressID) {
                        hasManyExpress = true;
                    }
                }
            }
            if (isApplyRefund) {
                $.MsgBox.Alert("提示", "申请退款出库单，不能安排打印！");
            } else {
                if (isHang) {
                    $.MsgBox.Alert("提示", "挂起出库单，不能安排打印！");
                } else {
                    var expressCount = 0;
                    if (!hasManyExpress) {
                        //前端没检查到有多个快递，ajax后端再检查一次
                        $.ajax({
                            url: "/Warehouse/Outbound/GetExpressCount?ids=" + ids.join(','),
                            type: "GET",
                            async: false,
                            cache: false,
                            success: function (r) {
                                var map = $.parseJSON(r);
                                expressCount = map.expressCount;
                                if (expressCount > 1) {
                                    hasManyExpress = true;
                                }
                            },
                            error: function () {
                            }
                        });
                    }
                    if (hasManyExpress) {
                        showLocalWindow("安排打印", "/Warehouse/Outbound/ArrangePrint?ids=" + ids.join(','), 380, 250, true, false, false);
                    } else {
                        if (expressCount == 0) {
                            $.MsgBox.Alert("提示", "获取出库单快递个数时，程序出现异常！");
                        } else {
                            ajaxSavePrintBatch(ids.join(','));
                        }
                    }
                }
            }
        } else {
            $.MsgBox.Alert("提示", "请选择出库单！");
        }
    }
}
//安排打印ajax
function ajaxSavePrintBatch(ids) {
    $.ajax({
        url: "/Warehouse/Outbound/SavePrintBatch?ids=" + ids,
        type: "GET",
        cache: false,
        success: function (r) {
            var map = $.parseJSON(r);
            if (map.result == 1) {
                $.MsgBox.Alert("提示", "安排成功！", 1000);
                $('#refreshCurrentPage').click();
            } else {
                $.MsgBox.Alert("提示", map.message);
            }
        },
        error: function () {
            $.MsgBox.Alert("提示", "安排打印失败！");
        }
    });
}
//按钮显示控制
function showControl() {
    var rows = $("#" + gridID).datagrid("getSelections");
    var ids = [];
    for (var i = 0; i < rows.length; i++) {
        ids.push(rows[i].ID);
    }
    if (isWaitPurchase == 0) {
        if (ids.length > 0) {
            //启用安排打印
            $('#arrangePrint').removeClass('unclick');
        } else {
            //禁用安排打印
            $('#arrangePrint').addClass('unclick');
        }
        //禁用生成采购计划单
        $('#generatePurchasePlan').addClass('unclick');
    } else {
        //禁用安排打印
        $("#arrangePrint").addClass("unclick");
        $('#generatePurchasePlan').removeClass('unclick');
    }
}