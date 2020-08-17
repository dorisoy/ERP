$(function () {
    //售后原因
    BindDictItem("orderRefundReason", "OrderRefundReason", 0);
    //物流公司
    BindDictItem("logisticsID", "logistics");
    //返回
    $("#back,#btnClose").click(function () {
        location.href = '/Warehouse/OrderRefund/Index';
    });
    //保存
    $("#btnSave,#toolbarsave").click(function () {
        btnSave(0);
    });
    $("#btnReset").hide();
    $("#txtBillNo").focus();
    //回车搜索
    $("#txtBillNo").keyup(function (e) {
        var currKey = 0, e = e || event;
        currKey = e.keyCode || e.which || e.charCode;
        if (currKey == 13) {
            searchOutbound($("#txtBillNo").val());
        }
    });
    //确认搜索
    $("#btnConfirm").click(function () {        
        searchOutbound($("#txtBillNo").val());
    });
    //重置
    $("#btnReset").click(reset);
    //绑定售后类型下拉列表
    $('#orderRefundType').combobox({
        url: '/Warehouse/OrderRefund/GetOrderRefundTypeJson?hasSelectedDefault=1',
        valueField: 'Value',
        textField: 'Name',
        onLoadSuccess: function () {
            //默认退货退款
            $(this).combobox('select', 0);
        },
        onChange: function (n, o) {
            if (n == 1) {
                $("#tbItemInfo").hide();
                $(".title").hide();
                $("#tbRefundInfo").hide();
                $("#tdRefundFreight").hide();
                $('#txtRefundFreight').numberbox({ required: false });
                $("#txtWaybillNo").val("");
                $("#hdnExpressCompany").val("");
                $("#logisticsID").combobox("select", 0);
            } else {
                $("#tbItemInfo").show();
                $(".title").show();
                $("#tbRefundInfo").show();
                $("#tdRefundFreight").show();
                $('#txtRefundFreight').numberbox({ required: true });
            }
            $("input[id^='txtRefundNum_']").each(function (index, element) {
                $(element).val("");
            });
            $("#txtRefundAmount").numberbox("setValue", "");
            $("#txtRefundFreight").numberbox("setValue", "");
            $("#txtReturnFreight").numberbox("setValue", "");
        }
    });
    //绑定售后责任方下拉列表
    $('#orderRefundDuty').combobox({
        url: '/Warehouse/OrderRefund/GetOrderRefundDutyJson?hasSelectedDefault=1',
        valueField: 'Value',
        textField: 'Name',
        onLoadSuccess: function () { //数据加载完毕事件
            //默认买家
            $("#orderRefundDuty").combobox('select', 2);
        },
        onChange: function (n, o) {
            if (n == 4) {
                $('#txtDutyOther').show();
            } else {
                $("#txtDutyOther").hide();
            }
        }
    });
    $("#orderRefundReason").combobox({
        onChange: function (n, o) {
            $("#hdnReason").val($("#orderRefundReason").combobox("getText"));
        }
    });
    $("#logisticsID").combobox({
        onChange: function (n, o) {
            if (n == 0) {
                $("#hdnExpressCompany").val("");
            } else {
                $("#hdnExpressCompany").val($("#logisticsID").combobox("getText"));
            }
        }
    });
    $("#logisticsID").combobox("select", 0);
});
//查询出库单，如果是已取消或申请退款，则不查询商品
function searchOutbound(billNo) {
    if (billNo.trim() != "") {
        $("#spanMsg").html("");
        var refundReason = $('#orderRefundReason').combobox('getData');  //赋默认值
        if (refundReason.length > 0) {
            $("#orderRefundReason").combobox('select', refundReason[0].Value);
            $("#hdnReason").val(refundReason[0].Text);
        }
        $.ajax({
            url: "/Warehouse/OrderRefund/SearchOutbound?billNo=" + billNo,
            type: "GET",
            async: false,
            cache: false,
            success: function (r) {
                var map = $.parseJSON(r);
                var outboundList = map.outboundList;
                var warehouseConfig = map.warehouseConfig;
                if (outboundList.length > 0) {
                    if (outboundList.length == 1) {
                        if (outboundList[0].Status == $("#hdnDeliveryStatus").val()) {
                            $("#txtBillNo").attr("disabled", true);
                            $("#hdnOrderSource").val(outboundList[0].OrderSource);
                            $("#hdnShopID").val(outboundList[0].ShopID);
                            $("#hdnErpOrderCode").val(outboundList[0].ErpOrderCode);
                            $("#hdnOutOrderCode").val(outboundList[0].OutOrderCode);
                            $("#hdnOutboundBillNo").val(outboundList[0].BillNo);
                            $("#hdnBuyAddr").val(outboundList[0].BuyAddr);
                            $("#hdnBuyName").val(outboundList[0].BuyName);
                            $("#hdnBuyMtel").val(outboundList[0].BuyMtel);
                            $("#hdnBuyTel").val(outboundList[0].BuyTel);
                            $("#btnConfirm").hide();
                            $("#btnReset").show();
                            $("#tbOrderRefundInfo").show();
                            $("#tbRefundInfo").show();
                            if (warehouseConfig != null) {
                                $("#spanAddrInfo").html(warehouseConfig.ReceivePerson + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + warehouseConfig.ReceiveTel + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + warehouseConfig.ReceiveAddress + "(" + warehouseConfig.ReceivePostCode + ")");
                                $("#hdnReceivePerson").val(warehouseConfig.ReceivePerson);
                                $("#hdnReceiveTel").val(warehouseConfig.ReceiveTel);
                                $("#hdnReceiveAddress").val(warehouseConfig.ReceiveAddress);
                                $("#hdnReceivePostCode").val(warehouseConfig.ReceivePostCode);
                            }
                            searchPickItem(outboundList[0].ID);
                        } else {
                            showErrMsg("出库单 " + outboundList[0].BillNo + " 未发货！");
                        }
                    } else {
                        //查询出有多个出库单，需要手动选择售后哪单
                        $("#divManyOutbound").show();
                        var manyOutbound = '有多个出库单，请选择：';
                        for (var i = 0; i < outboundList.length; i++) {
                            if (outboundList[i].Status == $("#hdnDeliveryStatus").val()) {
                                manyOutbound += "<br/><a href=\"javascript:void(0);\" onclick=\"searchOutbound('" + outboundList[i].BillNo + "');\">" + (i + 1) + "、订单编号：" + outboundList[i].ErpOrderCode + "，出库单号：" + outboundList[i].BillNo + "</a>";
                            } else {
                                manyOutbound += "<br/><span style=\"color:#999;\">" + (i + 1) + "、订单编号：" + outboundList[i].ErpOrderCode + "，出库单号：" + outboundList[i].BillNo + "<font color='red'>(未发货)</font></span>";
                            }
                        }
                        $("#divManyOutbound").html(manyOutbound);
                    }
                } else {
                    showErrMsg("订单编号/出库单号未找到，或不属于本仓库！");
                }
            },
            error: function () {
                showErrMsg("读取出库单信息失败！");
            }
        });
    } else {
        showErrMsg("请输入订单编号/出库单号！");
    }
}

//查询出库单拣货明细
function searchPickItem(id) {
    $.ajax({
        url: "/Warehouse/OrderRefund/SearchPickItem?id=" + id,
        type: "GET",
        async: false,
        cache: false,
        success: function (r) {
            var pickItemList = $.parseJSON(r);
            if (pickItemList.length > 0) {
                //查询到商品才显示
                $(".title").show();
                $("#tbItemInfo").show();
                $(".dataTr").each(function (index, element) {
                    $(element).remove();
                });
                for (var i = 0; i < pickItemList.length; i++) {
                    var html = '';
                    html += "<input type=\"hidden\" id=\"hdnOrdItemID_" + pickItemList[i].OrdItemID + "\" name=\"OrdItemID\" value=\"" + pickItemList[i].OrdItemID + "\" />";
                    html += "<input type=\"hidden\" id=\"hdnProductsID_" + pickItemList[i].OrdItemID + "\" name=\"ProductsID\" value=\"" + pickItemList[i].ProductsID + "\" />";
                    html += "<input type=\"hidden\" id=\"hdnProductsCode_" + pickItemList[i].OrdItemID + "\" name=\"ProductsCode\" value=\"" + pickItemList[i].ProductsCode + "\" />";
                    html += "<input type=\"hidden\" id=\"hdnProductsName_" + pickItemList[i].OrdItemID + "\" name=\"ProductsName\" value=\"" + pickItemList[i].ProductsName + "\" />";
                    html += "<input type=\"hidden\" id=\"hdnProductsNo_" + pickItemList[i].OrdItemID + "\" name=\"ProductsNo\" value=\"" + pickItemList[i].ProductsNo + "\" />";
                    html += "<input type=\"hidden\" id=\"hdnProductsWeight_" + pickItemList[i].OrdItemID + "\" name=\"ProductsWeight\" value=\"" + pickItemList[i].ProductsWeight + "\" />";
                    html += "<input type=\"hidden\" id=\"hdnProductsSkuID_" + pickItemList[i].OrdItemID + "\" name=\"ProductsSkuID\" value=\"" + pickItemList[i].ProductsSkuID + "\" />";
                    html += "<input type=\"hidden\" id=\"hdnProductsSkuCode_" + pickItemList[i].OrdItemID + "\" name=\"ProductsSkuCode\" value=\"" + pickItemList[i].ProductsSkuCode + "\" />";
                    html += "<input type=\"hidden\" id=\"hdnProductsSkuSaleprop_" + pickItemList[i].OrdItemID + "\" name=\"ProductsSkuSaleprop\" value=\"" + pickItemList[i].ProductsSkuSaleprop + "\" />";
                    html += "<input type=\"hidden\" id=\"hdnProductsBatchID_" + pickItemList[i].OrdItemID + "\" name=\"ProductsBatchID\" value=\"" + pickItemList[i].ProductsBatchID + "\" />";
                    html += "<input type=\"hidden\" id=\"hdnProductsBatchCode_" + pickItemList[i].OrdItemID + "\" name=\"ProductsBatchCode\" value=\"" + pickItemList[i].ProductsBatchCode + "\" />";
                    html += "<input type=\"hidden\" id=\"hdnProductsNum_" + pickItemList[i].OrdItemID + "\" name=\"ProductsNum\" value=\"" + pickItemList[i].ProductsNum + "\" />";
                    html += "<input type=\"hidden\" id=\"hdnHasRefunNum_" + pickItemList[i].OrdItemID + "\" name=\"HasRefunNum\" value=\"" + pickItemList[i].HasRefunNum + "\" />";
                    html += "<input type=\"hidden\" id=\"hdnActualSellingPrice_" + pickItemList[i].OrdItemID + "\" name=\"ActualSellingPrice\" value=\"" + pickItemList[i].ActualSellingPrice + "\" />";
                    var tr = $("<tr id=\"dataTr_" + pickItemList[i].OrdItemID + "\" class=\"dataTr\" orditemid=\"" + pickItemList[i].OrdItemID + "\"></tr>");
                    var td1 = $("<td>" + pickItemList[i].ProductsCode + html + "</td>");
                    td1.appendTo(tr);
                    var td2 = $("<td>" + pickItemList[i].ProductsName + "</td>");
                    td2.appendTo(tr);
                    var td3 = $("<td>" + pickItemList[i].ProductsSkuSaleprop + "</td>");
                    td3.appendTo(tr);
                    var td4 = $("<td>" + pickItemList[i].ProductsSkuCode + "</td>");
                    td4.appendTo(tr);
                    var td5 = $("<td><lable style='color:red;'>￥</lable>" + pickItemList[i].ActualSellingPrice.toFixed(3) + "</td>");
                    td5.appendTo(tr);
                    var td6 = $("<td>" + pickItemList[i].ProductsBatchCode + "</td>");
                    td6.appendTo(tr);
                    var hasRefundNumHtml = '';
                    if (pickItemList[i].HasRefunNum > 0) hasRefundNumHtml = '<br/><font style="color:red;">已售后：' + pickItemList[i].HasRefunNum + '</font>';
                    var td7 = $("<td>" + pickItemList[i].ProductsNum + hasRefundNumHtml + "</td>");
                    td7.appendTo(tr);
                    var td8 = $("<td><input type=\"text\" id=\"txtRefundNum_" + pickItemList[i].OrdItemID + "\" name=\"RefundNum\" class=\"inputextbox\" orditemid=\"" + pickItemList[i].OrdItemID + "\" onkeyup=\"checkInputNum(" + pickItemList[i].OrdItemID + ")\" style=\"width:65px; text-align:center;\" autocomplete=\"Off\" /></td>");
                    td8.appendTo(tr);
                    tr.appendTo($("#tbItemInfo"));
                }
                $.parser.parse($("#tbItemInfo"));
                $(".dataTr").mouseover(function () {
                    $(this).css("background", "#e2e2e2");
                }).mouseout(function () {
                    $(this).css("background", "#fff");
                });
            } else {
                showErrMsg("未找到出库单拣货明细！");
            }
        },
        error: function () {
            showErrMsg("读取出库单拣货明细信息失败！");
        }
    });
}
//显示错误消息
function showErrMsg(errMsg) {
    $("#spanMsg").html(errMsg);
    $("#txtBillNo").focus();
}
//检查输入数量并更新退款金额
function checkInputNum(id) {
    var num = $("#txtRefundNum_" + id).val().replace(/\D/g, '');
    $("#txtRefundNum_" + id).val(num);
    var canRefundNum = Number($("#hdnProductsNum_" + id).val()) - Number($("#hdnHasRefunNum_" + id).val());
    if (Number($("#txtRefundNum_" + id).val()) > canRefundNum) {
        $("#txtRefundNum_" + id).val("");
        $.MsgBox.Alert("提示", "超过了可售后数量！");
    }
    updateAmount();
}
//更新退款金额
function updateAmount() {
    var totalRefundAmount = 0;
    $("input[id^='txtRefundNum_']").each(function (index, element) {
        if ($.isNumeric($(this).val())) {
            var ordItemID = $(this).attr("orditemid");
            totalRefundAmount += Number($(this).val()) * parseFloat($("#hdnActualSellingPrice_" + ordItemID).val());
        }
    });
    $("#txtRefundAmount").numberbox("setValue", totalRefundAmount);
}
//保存售后
function btnSave(i) {
    $('#ff').form('submit', {
        url: "/Warehouse/OrderRefund/Save",
        type: "POST",//使用get方法访问后台
        dataType: "json",
        onSubmit: function () {
            var isValid = $(this).form('validate');
            if (isValid) {
                if ($('#orderRefundType').combobox('getValue') == 1) {
                    if (i == 0) {
                        isValid = false;
                        $.messager.confirm('提示', "仅退款的售后状态会自动生成一条退款单。确认添加吗？", function (r) {
                            if (r) {
                                btnSave(1);
                            }
                        });
                    }
                } else {

                    var totalRefundNum = 0;
                    $("input[name='RefundNum']").each(function (index, element) {
                        if ($(this).val() != "") {
                            totalRefundNum += Number($(this).val());
                        }
                    });
                    if (totalRefundNum == 0) {
                        $.MsgBox.Alert("提示", "请输入售后数量！");
                        isValid = false;
                    }
                    if (isValid) {
                        var expressCompany = $("#hdnExpressCompany").val().trim();
                        var waybillNo = $("#txtWaybillNo").val().trim();
                        var returnFreight = $("#txtReturnFreight").numberbox("getValue");
                        //寄回信息 要么三个都不填写，要么都填写
                        isValid = (expressCompany == "" && waybillNo == "" && returnFreight == "") || (expressCompany != "" && waybillNo != "" && returnFreight != "");
                        if (!isValid) {
                            $.MsgBox.Alert("提示", "寄回信息未填写完整！");
                        }
                    }
                }
            }
            return isValid;	// 返回false终止表单提交
        },
        success: function (r) {
            var map = $.parseJSON(r);
            if (map.result == 1) {
                $.MsgBox.Alert("提示", "保存成功！", 1000);
                reset();
            } else {
                $.MsgBox.Alert("提示", map.message);
            }
        },
        error: function () {
            $.MsgBox.Alert("提示", "保存失败！");
        }
    });
}
//重置
function reset() {
    $("#txtBillNo").removeAttr("disabled");
    $("#txtBillNo").val("");
    $("#txtBillNo").focus();
    $("#btnReset").hide();
    $("#btnConfirm").show();
    $("#spanMsg").html("");
    $('#orderRefundType').combobox("setValue", 0);
    $("#tbOrderRefundInfo").hide();
    $("#logisticsID").combobox("select", 0);
    $("#hdnExpressCompany").val("");
    $("#txtWaybillNo").val("");
    $("#txtReturnFreight").numberbox("setValue", "");
    $("#orderRefundDuty").combobox('select', 2);
    $("#txtDutyOther").val("");
    $("#txtReasonDetail").val("");
    $("#txtRefundAmount").numberbox("setValue", "");
    $("#txtRefundFreight").numberbox("setValue", "");
    $('#txtRefundFreight').numberbox({ required: true });
}