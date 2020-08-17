var status = 10;
var isSearchPrintBatch = true;
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
            $(this).combobox('select', "0");
        }
    });
    //绑定更换发货快递下拉列表
    $('#changeDeliveryExpress').combobox({
        url: '/Warehouse/Express/JsonTree',
        valueField: 'Value',
        textField: 'Text',
        onLoadSuccess: function () { //数据加载完毕事件
            $(this).combobox('select', "0");
        },
        onChange: function (n, o) {
            if (o != "" && n != "0") {
                var printBatchCode = $("#printBatch").combobox("getValue");
                if (printBatchCode == "" || printBatchCode == "0") {
                    $.MsgBox.Alert("提示", "请选择一个批次号！");
                    $(this).combobox('select', "0");
                } else {
                    var rows = $("#grid").datagrid("getRows");
                    if (rows.length > 0) {
                        //更换发货快递
                        var oldDeliveryExpressID = rows[0].DeliveryExpressID;
                        var deliveryExpressID = $(this).combobox("getValue");
                        if (oldDeliveryExpressID != deliveryExpressID) {
                            $.messager.confirm('提示', "更换发货快递会清除出库单的运单号，确认更换？", function (r) {
                                if (r) {
                                    ajaxChangeDeliveryExpress(printBatchCode, deliveryExpressID);
                                }
                            });
                        } else {
                            $(this).combobox('select', "0");
                        }

                    } else {
                        $.MsgBox.Alert("提示", "打印批次 " + printBatchCode + " 不存在！");
                        $(this).combobox('select', "0");
                    }
                }
            }
        }
    });
    //绑定打印批次
    bindPrintBatch();
    //加载列表
    initTable("", 1);
    //刷新
    $('#refresh').click(function () {
        bindSerarchLickEvent(1);
    });
    //获取运单号
    $('#getWaybillNo').click(getWaybillNo);
    //打印拣货单
    $('#printPickOrder').click(function () {
        printOrder(undefined, 1);
    });
    //打印发货单
    $('#printDeliveryOrder').click(function () {
        printOrder(undefined, 0);
    });
    //打印快递单
    $('#printExpressOrder').click(function () {
        printOrder(undefined, 2);
    });
    //保存快递单号
    $('#setWaybillNo').click(setWaybillNo);
    //打印完毕
    $('#printFinish').click(function () {
        printFinish(undefined);
    });
    //刷新当前页
    $('#refreshCurrentPage').click(function () {
        var grid = $("#grid");
        var options = grid.datagrid("getPager").data("pagination").options;
        var currPageNumber = options.pageNumber;
        var printBatchCode = $("#printBatch").combobox('getValue');
        if (printBatchCode == "" || printBatchCode == "0") {
            bindSerarchLickEvent(currPageNumber);
        } else {
            searchPrintBatch(currPageNumber);
        }
    });
    //清空条件
    $('#btnReset').click(function () {
        window.location.reload(true);
    });
    //搜索
    $('#btnSearch').click(function () {
        bindSerarchLickEvent(1);
    });
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
        shopID: $("#shop").combobox('getValue')
    }
    bindPrintBatch();
    //将值传递给
    initTable(queryData, pageNumber);
    return false;
}
function bindPrintBatch() {
    $('#printBatch').combobox({
        url: '/Warehouse/Outbound/GetPrintBatch?status=' + status + '&ram=' + Math.random(),
        valueField: 'Value',
        textField: 'Text',
        onLoadSuccess: function () { //数据加载完毕事件
            $("#printBatch").combobox('select', "0");
        },
        onChange: function (n, o) {
            if (o != "") {
                searchPrintBatch(1);
            }
        }
    });
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
    isSearchPrintBatch = false;
    $("#printBatch").combobox('setValue', 0);
    isSearchPrintBatch = true;
    var queryData = {
        isApplyRefund: 1
    }
    //将值传递给
    initTable(queryData, 1);
    return false;
}
//根据打印批次搜索出库单
function searchPrintBatch(pageNumber) {
    if (!isSearchPrintBatch) return false;
    $("#keyWordType").combobox('setValue', '订单编号');
    $("#txtKeyWord").val('');
    $("#express").combobox('setValue', 0);
    $("#isNeedInvoice").combobox('setValue', -1);
    $("#messageRemark").combobox('setValue', -1);
    $("#isCod").combobox('setValue', -1);
    $("#shop").combobox('setValue', 0);
    $("#changeDeliveryExpress").combobox('setValue', 0);
    var _printBatchCode = $("#printBatch").combobox('getValue');
    var queryData = {
        printBatchCode: _printBatchCode
    }
    //将值传递给
    initTable(queryData, pageNumber);
    return false;
}
//获取申请退款笔数
function getApplyRefundCount() {
    $.ajax({
        url: '/Warehouse/Outbound/GetApplyRefundCount?status=' + status + '&ram=' + Math.random(),
        type: "GET",
        cache: false,
        success: function (r) {
            var map = $.parseJSON(r);
            $("#lblApplyRefundCount").text(map.appRefundCount);
        },
        error: function () {
            $.MsgBox.Alert("提示", "获取申请退款笔数失败！", 1000);
        }
    });
}
//加载列表
function initTable(queryData, pageNumber) {
    $('#grid').datagrid({
        url: '/Warehouse/Outbound/SearchWaitPrint?ram=' + Math.random(),
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
        queryParams: queryData,  //异步查询的参数
        onDblClickCell: function (index, field, value) {
            if (field == "WaybillNo") {
                var deliveryExpressID = $("#hdnDeliveryExpressID_" + index).val();
                var isHot = ajaxCheckIsHot(deliveryExpressID);
                if (!isHot) {
                    startEditWaybillNo(index);
                }
            }
        },
        columns: [[
        {
            title: '主键', field: 'ID', width: 10,
            formatter: function (value, row, index) {
                var html = '<input type="hidden" id="hdnID_' + index + '" value="' + row.ID + '" />';
                html += '<input type="hidden" id="hdnDeliveryExpressID_' + index + '" value="' + row.DeliveryExpressID + '" />';
                return html;
            },
            sortable: true, hidden: true
        },
        {
            title: '出库单号', field: 'BillNo', width: 170, align: 'center',
            formatter: function (value, row) {
                var html = '<a href="javascript:void(0);" onclick=\'showOutboundDetail("' + value + '")\'>' + value + '</a>';
                if (row.ReturnCount > 0) {
                    html += '<img src="../../Content/images/back.gif" width="16px" height="16px" alt="" title="返回的出库单" />';
                }
                html += '<br/><span style="color:#999;">(' + row.ShopName + ')</span>';
                return html;
            }, sortable: true
        },
        {
            title: '订单编号', field: 'ErpOrderCode', width: 120, align: 'center',
            formatter: function (value, row, index) {
                var html = value;
                html += '<br/><span style="color:#999;">(' + row.BuyName + ')</span>';
                if (row.IsApplyRefund == 1) {
                    html += '<br/><span style="color:#ff0000;">[申请退款]</span>';
                    html += '<input type="hidden" id="hdnIsApplyRefund_' + row.ID + '" value="' + row.IsApplyRefund + '" />';
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
                var html = row.DeliveryExpressName;
                if (row.DeliveryExpressName == null || row.DeliveryExpressName == "") html = "未指定";
                if (row.ExpressID != row.DeliveryExpressID) {
                    html += '<br/><span style="color:#999;" title="下单选择快递">（' + (row.ExpressName == null || row.ExpressName == "" ? "未指定" : row.ExpressName) + '）</span>';
                }
                return html;
            },
            sortable: false
        },
        {
            title: '运单号', field: 'WaybillNo', width: 130, align: 'center',
            formatter: function (value, row, index) {
                if (value == null) value = "";
                var html = '<span id="spanWaybillNo_' + index + '">' + value + '</span>';
                html += '<div id="divWaybillNo_' + index + '" style="display:none;"><input type="text" id="txtWaybillNo_' + index + '" class="easyui-validatebox" data-options="validType:[\'code\',\'length[1,20]\'],height:30" value="' + value + '" style="width:120px; text-align:center;" /></div>';
                html += '<span id="spanErrorMsg" style="color:red;"></span>';
                return html;
            },
            sortable: true
        },
        {
            title: '货到付款', field: 'IsCod', width: 60, align: 'center',
            formatter: function (value, row) {
                return value == 1 ? "是" : "否";
            },
            sortable: false
        },
        {
            title: '需要发票', field: 'IsNeedInvoice', width: 60, align: 'center',
            formatter: function (value, row) {
                return value == 1 ? '<span style="color:#ff0000;">是</span>' : '否';
            },
            sortable: false
        },
        {
            title: '留言备注', field: 'messageRemark', width: 60, align: 'center',
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
                return html;
            },
            sortable: false
        },
        {
            title: '打印', field: 'PrintOperate', width: 112, align: 'center',
            formatter: function (value, row) {
                var hasPrintPickOrder = row.PickPrintDate != "0001-01-01 00:00:00";
                var hasPrintDeliveryOrder = row.DeliveryPrintDate != "0001-01-01 00:00:00";
                var hasPrintExpressOrder = row.ExpressPrintDate != "0001-01-01 00:00:00";
                var html = '<ul class="printMenu">'
                html += '<li><a href="javascript:void(0);" id="pick_' + row.ID + '" onclick="printOrder(' + row.ID + ',1);"' + (hasPrintPickOrder ? "class=\"hasPrint\"" : "") + '>拣</a></li>';
                html += '<li><a href="javascript:void(0);" id="delivery_' + row.ID + '" onclick="printOrder(' + row.ID + ',0);"' + (hasPrintDeliveryOrder ? "class=\"hasPrint\"" : "") + '>发</a></li>';
                html += '<li><a href="javascript:void(0);" id="express_' + row.ID + '" onclick="printOrder(' + row.ID + ',2);"' + (hasPrintExpressOrder ? "class=\"hasPrint\"" : "") + '>快</a></li>';
                html += '</ul>';
                return html;
            },
            sortable: false
        },
        {
            title: '操作', field: 'Permit', width: 100, align: 'center',
            formatter: function (value, row) {
                var html = '<a href="javascript:void(0);" onclick="returnWaitPick(' + row.ID + ');">返回</a>';
                if (row.IsApplyRefund == 0) {
                    html += ' | <a href="javascript:void(0);" onclick="printFinish(' + row.ID + ');">打印完毕</a>';
                } else {
                    html += ' | <a href="javascript:void(0);" class="unclick" style="color:#999; cursor:default;">打印完毕</a>';
                }
                return html;
            },
            sortable: false
        }
        ]],
        onLoadSuccess: function (data) {
            getApplyRefundCount();
            var deliveryExpressID = 0;
            $.each(data.rows, function (index, element) {
                if (index == 0) deliveryExpressID = element.DeliveryExpressID;
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
                $("#txtWaybillNo_" + index).blur(function () {
                    if ($(this).validatebox("isValid")) {
                        var oldValue = $("#spanWaybillNo_" + index).html();
                        var waybillNo = $(this).val();
                        var id = $("#hdnID_" + index).val();
                        var deliveryExpressID = $("#hdnDeliveryExpressID_" + index).val();
                        //有改变才更新
                        if (oldValue != waybillNo) {
                            var logisticsCode = ajaxGetLogisticsCode(deliveryExpressID);
                            var result = chkWaybillNoRules(logisticsCode, waybillNo);
                            if (result) {
                                ajaxEditWaybillNo(id, deliveryExpressID, waybillNo, index);
                            } else {
                                //运单号不符合规则，需要手动确认是否更新
                                $.messager.confirm('提示', "运单号不符合规则，确认保存吗？", function (r) {
                                    if (r) {
                                        ajaxEditWaybillNo(id, deliveryExpressID, waybillNo, index);
                                    } else {
                                        endEditWaybillNo(index, oldValue);
                                    }
                                });
                            }
                        } else {
                            endEditWaybillNo(index, waybillNo);
                        }
                    } else {
                        $(this).focus();
                    }
                });
            });
            var printBatchCode = $("#printBatch").combobox('getValue');
            if (printBatchCode != "" && printBatchCode != "0" && deliveryExpressID > 0) {
                var isHot = ajaxCheckIsHot(deliveryExpressID);
                if (isHot) {
                    $("#getWaybillNo").removeClass("unclick");
                    $("#setWaybillNo").addClass("unclick");
                } else {
                    $("#getWaybillNo").addClass("unclick");
                    $("#setWaybillNo").removeClass("unclick");
                }
            } else {
                $("#getWaybillNo").addClass("unclick");
                $("#setWaybillNo").addClass("unclick");
            }
            $("#grid").datagrid('clearSelections');
            DataGridNoData(this);
        }
    });
}
//返回
function returnWaitPick(id) {
    $.messager.confirm('提示', "返回操作会把出库单状态修改为待拣货，并且清除该出库单的打印批次和运单号，确认返回？", function (r) {
        if (r) {
            ajaxReturnWaitPick(id);
        }
    });
}
//获取运单号 热敏
function getWaybillNo() {
    if ($("#getWaybillNo").hasClass('unclick')) return false;
    var printBatchCode = $("#printBatch").combobox("getValue");
    if (printBatchCode == "" || printBatchCode == "0") {
        $.MsgBox.Alert("提示", "请选择一个批次号！");
    } else {
        $.MsgBox.Alert("提示", '获取运单号待实现！');
    }
}
//打印 0发货单、1拣货单、2快递单
function printOrder(id, printTemplateType) {
    var controllerName = '';
    var btnID = '';
    var title = '';
    var hasPrint = false;
    var hasRefund = false;
    switch (printTemplateType) {
        case 0:
            title = "发货单";
            controllerName = "PrintDeliveryDialog";
            btnID = "delivery_";
            break;
        case 1:
            title = "拣货单";
            controllerName = "PrintPickDialog";
            btnID = "pick_";
            break;
        case 2:
            title = "快递单";
            controllerName = "PrintExpressDialog";
            btnID = "express_";
            break;
    }
    var printBatchCode = $("#printBatch").combobox("getValue");
    if (undefined == id) {
        if (printBatchCode == "" || printBatchCode == "0") {
            $.MsgBox.Alert("提示", "请选择一个批次号！");
        } else {
            var rows = $("#grid").datagrid("getRows");
            if (rows.length > 0) {
                for (var i = 0; i < rows.length; i++) {
                    if ($("#" + btnID + rows[i].ID).hasClass("hasPrint")) {
                        hasPrint = true;
                    }
                    if (rows[i].IsApplyRefund > 0) {
                        hasRefund = true;
                    }
                }
                if (!hasRefund) {
                    //前端没检查到有申请退款，ajax后端再检查一次
                    hasRefund = ajaxCheckHasRefund(printBatchCode);
                }
                if (!hasRefund) {
                    var url = "/Warehouse/Outbound/" + controllerName + "?id=0&printBatchCode=" + printBatchCode + "&ram=" + Math.random();
                    if (!hasPrint) {
                        //前端没检查到已经打印，ajax后端再检查一次
                        hasPrint = ajaxCheckHasPrint(printBatchCode, printTemplateType);
                    }
                    if (hasPrint) {
                        $.messager.confirm('提示', "有出库单已经打印过" + title + "，再次打印可能导致重复发货，请确认！", function (r) {
                            if (r) {
                                showLocalWindow("打印" + title, url, 460, 260, true, false, false);
                            }
                        });
                    } else {
                        showLocalWindow("打印" + title, url, 460, 260, true, false, false);
                    }
                    
                } else {
                    $.MsgBox.Alert("提示", "申请退款的出库单不能进行此操作！");
                }
            } else {
                $.MsgBox.Alert("提示", "打印批次 " + printBatchCode + " 不存在！");
            }
        }
    } else {
        hasRefund = $("#hdnIsApplyRefund_" + id).val() == "1";
        hasPrint = $("#" + btnID + id).hasClass("hasPrint");
        if (!hasRefund) {
            //前端没检查到有申请退款，ajax后端再检查一次
            hasRefund = ajaxCheckIsApplyRefund(id);
        }
        if (!hasRefund) {
            var url = "/Warehouse/Outbound/" + controllerName + "?id=" + id + "&printBatchCode=" + printBatchCode + "&ram=" + Math.random();
            if (!hasPrint) {
                //前端没检查到已经打印，ajax后端再检查一次
                hasPrint = ajaxCheckIsPrint(id, printTemplateType);
            }
            if (hasPrint) {
                $.messager.confirm('提示', "出库单已经打印过" + title + "，再次打印可能导致重复发货，请确认！", function (r) {
                    if (r) {
                        showLocalWindow("打印" + title, url, 460, 260, true, false, false);
                    }
                });
            } else {
                showLocalWindow("打印" + title, url, 460, 260, true, false, false);
            }            
        }else{
            $.MsgBox.Alert("提示", "申请退款的出库单不能进行此操作！");
        }
    }
}
//保存运单号
function setWaybillNo() {
    if ($("#setWaybillNo").hasClass('unclick')) return false;
    var printBatchCode = $("#printBatch").combobox("getValue");
    if (printBatchCode == "" || printBatchCode == "0") {
        $.MsgBox.Alert("提示", "请选择一个批次号！");
    } else {
        var rows = $("#grid").datagrid("getRows");
        if (rows.length > 0) {
            var deliveryExpressID = rows[0].DeliveryExpressID;
            showLocalWindow("保存运单号", "/Warehouse/Outbound/SetWaybillNo?printBatchCode=" + printBatchCode + "&deliveryExpressID=" + deliveryExpressID + "&ram=" + Math.random(), 800, 500, true, false, false);
        } else {
            $.MsgBox.Alert("提示", "打印批次 " + printBatchCode + " 不存在！");
        }
    }
}
//开启运单号编辑
function startEditWaybillNo(index) {
    $("#spanWaybillNo_" + index).hide();
    $("#divWaybillNo_" + index).show();
    $.parser.parse($("#divWaybillNo_" + index));
    $("#txtWaybillNo_" + index).focus();
    $("#txtWaybillNo_" + index).select();
}
//结束运单号编辑
function endEditWaybillNo(index, waybillNo) {
    $("#divWaybillNo_" + index).hide();
    $("#spanWaybillNo_" + index).html(waybillNo);
    $("#txtWaybillNo_" + index).val(waybillNo);
    $("#spanWaybillNo_" + index).show();
}
//ajax 返回
function ajaxReturnWaitPick(ids){
    $.ajax({
        url: "/Warehouse/Outbound/ReturnWaitPick?ids=" + ids,
        type: "GET",
        async: false,
        cache: false,
        success: function (r) {
            var map = $.parseJSON(r);
            if (map.result == 1) {
                $.MsgBox.Alert("提示", "返回成功！", 1000);
                $('#refreshCurrentPage').click();
            } else {
                $.MsgBox.Alert("提示", map.message);
            }
        },
        error: function () {
            $.MsgBox.Alert("提示", "返回失败！");
        }
    });
}
//打印完毕
function printFinish(id) {
    var hasRefund = false;
    if (undefined == id) {
        var printBatchCode = $("#printBatch").combobox("getValue");
        if (printBatchCode == "" || printBatchCode == "0") {
            $.MsgBox.Alert("提示", "请选择一个批次号！");
        } else {
            var rows = $("#grid").datagrid("getRows");
            if (rows.length > 0) {
                for (var i = 0; i < rows.length; i++) {
                    if (rows[i].IsApplyRefund > 0) {
                        hasRefund = true;
                    }
                }
                if (!hasRefund) {
                    //前端没检查到有申请退款，ajax后端再检查一次
                    hasRefund = ajaxCheckHasRefund(printBatchCode);
                }
                if (!hasRefund) {
                    $.messager.confirm('提示', "此操作表示出库单已打印完毕，出库单将修改为待发货状态。确认操作吗？", function (r) {
                        if (r) {
                            ajaxPrintFinish(0, printBatchCode);
                        }
                    });
                } else {
                    $.MsgBox.Alert("提示", "申请退款的出库单不能进行此操作！");
                }
            } else {
                $.MsgBox.Alert("提示", "打印批次 " + printBatchCode + " 不存在！");
            }
        }
    } else {
        hasRefund = $("#hdnIsApplyRefund_" + id).val() == "1";
        if (!hasRefund) {
            //前端没检查到有申请退款，ajax后端再检查一次
            hasRefund = ajaxCheckIsApplyRefund(id);
        }
        if (!hasRefund) {
            $.messager.confirm('提示', "确认本出库单打印完毕？", function (r) {
                if (r) {
                    ajaxPrintFinish(id, "");
                }
            });
        } else {
            $.MsgBox.Alert("提示", "申请退款的出库单不能进行此操作！");
        }
    }
}
//ajax打印完毕
function ajaxPrintFinish(id, printBatchCode) {
    $.ajax({
        url: "/Warehouse/Outbound/PrintFinish?id=" + id + "&printBatchCode=" + printBatchCode,
        type: "GET",
        async: false,
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
            $.MsgBox.Alert("提示", "操作失败！");
        }
    });
}
//ajax检查打印批次是否有申请退款出库单
function ajaxCheckHasRefund(printBatchCode) {
    var hasRefund = false;
    $.ajax({
        url: "/Warehouse/Outbound/GetApplyRefundCount?printBatchCode=" + printBatchCode + "&status=" + status + "&ram=" + Math.random(),
        type: "GET",
        async: false,
        cache: false,
        success: function (r) {
            var map = $.parseJSON(r);
            if (map.appRefundCount > 0) {
                hasRefund = true;
            }
        },
        error: function () {
        }
    });
    return hasRefund;
}
//ajax检查打印批次是否有已经打印过的出库单
function ajaxCheckHasPrint(printBatchCode, printTemplateType) {
    var hasPrint = false;
    $.ajax({
        url: "/Warehouse/Outbound/GetPrintCount?printBatchCode=" + printBatchCode + "&printTemplateType=" + printTemplateType + "&status=" + status + "&ram=" + Math.random(),
        type: "GET",
        async: false,
        cache: false,
        success: function (r) {
            var map = $.parseJSON(r);
            if (map.printCount > 0) {
                hasPrint = true;
            }
        },
        error: function () {
        }
    });
    return hasPrint;
}
//ajax检查出库单是否申请退款
function ajaxCheckIsApplyRefund(id) {
    var isRefund = false;
    $.ajax({
        url: "/Warehouse/Outbound/CheckIsApplyRefund?id=" + id + "&ram=" + Math.random(),
        type: "GET",
        async: false,
        cache: false,
        success: function (r) {
            var map = $.parseJSON(r);
            if (map.isApplyRefund == 1) {
                isRefund = true;
            }
        },
        error: function () {
        }
    });
    return isRefund;
}
//ajax检查出库单是否已打印  0发货单、1拣货单、2快递单
function ajaxCheckIsPrint(id, printTemplateType) {
    var isPrint = false;
    $.ajax({
        url: "/Warehouse/Outbound/CheckIsPrint?id=" + id + "&printTemplateType=" + printTemplateType + "&ram=" + Math.random(),
        type: "GET",
        async: false,
        cache: false,
        success: function (r) {
            var map = $.parseJSON(r);
            if (map.isPrint == 1) {
                isPrint = true;
            }
        },
        error: function () {
        }
    });
    return isPrint;
}
//ajax 更换发货快递
function ajaxChangeDeliveryExpress(printBatchCode, deliveryExpressID) {
    $.ajax({
        url: "/Warehouse/Outbound/ChangeDeliveryExpress?printBatchCode=" + printBatchCode + "&deliveryExpressID=" + deliveryExpressID,
        type: "GET",
        async: false,
        cache: false,
        success: function (r) {
            var map = $.parseJSON(r);
            if (map.result == 1) {
                $.MsgBox.Alert("提示", "操作成功！", 1000);
                $("#refreshCurrentPage").click();
            } else {
                $.MsgBox.Alert("提示", map.message);
            }
        },
        error: function () {
            $.MsgBox.Alert("提示", "操作失败！");
        }
    });
}
//获取打印批次的发货快递否是热敏类型
function ajaxCheckIsHot(deliveryExpressID) {
    var isHot = false;
    $.ajax({
        url: '/Warehouse/Express/CheckIsHot?id=' + deliveryExpressID + '&ram=' + Math.random(),
        type: "GET",
        async: false,
        cache: false,
        success: function (r) {
            var map = $.parseJSON(r);
            if (map.result == 1) {
                isHot = true;
            }
        },
        error: function () {
            $.MsgBox.Alert("提示", "获取发货快递打印类型失败！", 1000);
        }
    });
    return isHot;
}
//ajax 编辑运单号
function ajaxEditWaybillNo(id, deliveryExpressID, waybillNo, index) {
    $.ajax({
        url: "/Warehouse/Outbound/EditWaybillNo?id=" + id + "&deliveryExpressID=" + deliveryExpressID + "&waybillNo=" + waybillNo,
        type: "GET",
        async: false,
        cache: false,
        success: function (r) {
            var map = $.parseJSON(r);
            if (map.result == 1) {
                $.MsgBox.Alert("提示", "保存成功！", 1000);
                endEditWaybillNo(index, waybillNo);
            } else {
                $.MsgBox.Alert("提示", map.message);
            }
        },
        error: function () {
            $.MsgBox.Alert("提示", "保存失败！");
        }
    });
}
//ajax 获取发货快递的物流代码
function ajaxGetLogisticsCode(deliveryExpressID) {
    var logisticsCode = '';
    $.ajax({
        url: "/Warehouse/Outbound/GetLogisticsCode?expressID=" + deliveryExpressID,
        type: "GET",
        async: false,
        cache: false,
        success: function (r) {
            logisticsCode = r;
        }
    });
    return logisticsCode;
}
