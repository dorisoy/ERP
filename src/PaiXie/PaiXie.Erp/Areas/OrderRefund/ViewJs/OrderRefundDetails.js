//初始化
$(function () {
    //加载售后单信息和售后单商品
    initOrdRefundInfo();
    //退回商品
    $("#returnProduct").click(function () {
        showLocalWindow("退回商品", "/OrderRefund/OrderRefund/SetLogistics?ordRefundID=" + $("#hdnOrdRefundID").val(), 520, 320, true, false, false);
    });
    //确认收货
    $("#confirmReceive").click(function () {
        showLocalWindow("确认收货", "/OrderRefund/OrderRefund/ConfirmReceiveIndex?ordRefundID=" + $("#hdnOrdRefundID").val(), 820, 530, true, false, false);
    });
    //取消售后
    $("#cancelRefund").click(cancel);
    //刷新当前页
    $("#refreshCurrentPage").click(function () {
        initOrdRefundInfo();
    });
});

//加载售后单信息和售后单商品
function initOrdRefundInfo() {
    var billNo = $("#hdnBillNo").val();
    $.ajax({
        url: "/OrderRefund/OrderRefund/GetOrdRefundInfo?billNo=" + billNo,
        type: "GET",
        cache: false,
        success: function (r) {
            var map = $.parseJSON(r);
            if (map.ordRefund) {
                var ordRefund = map.ordRefund;
                $("#spanBillNo").html(ordRefund.BillNo);
                $("#hdnOrdRefundID").val(ordRefund.ID);
                $("#spanErpOrderCode").html(ordRefund.ErpOrderCode);
                $("#spanOutboundBillNo").html(ordRefund.OutboundBillNo);
                $("#spanStatus").html(map.statusName);
                switch (ordRefund.Status) {
                    case 10:
                        $("#returnProduct").show();
                        $("#confirmReceive").hide();
                        $("#cancelRefund").show();
                        break;
                    case 20:
                        $("#returnProduct").hide();
                        $("#confirmReceive").show();
                        $("#cancelRefund").show();
                        break;
                    case 30:
                        $("#returnProduct").hide();
                        $("#confirmReceive").show();
                        $("#cancelRefund").show();
                        $("#spanStatus").css("color", "red");
                        break;
                    case 40:
                        $("#returnProduct").hide();
                        $("#confirmReceive").hide();
                        $("#cancelRefund").hide();
                        $(".buttonList").hide();
                        break;
                    case 99:
                        $("#returnProduct").hide();
                        $("#confirmReceive").hide();
                        $("#cancelRefund").hide();
                        $(".buttonList").hide();
                        break;
                }
                $("#spanRefundType").html(map.refundTypeName);
                $("#spanDuty").html(map.dutyName);
                $("#spanRefundAmount").html("<lable style='color:red;'>￥</lable>" + ordRefund.RefundAmount.toFixed(3));
                $("#spanRefundFreight").html("<lable style='color:red;'>￥</lable>" + ordRefund.RefundFreight.toFixed(3));
                $("#spanRefundBillNo").html(ordRefund.RefundBillNo);
                $("#spanReason").html(ordRefund.Reason);
                $("#spanCreateDate").html(ordRefund.CreateDate);
                $("#spanCreatePerson").html(ordRefund.CreatePerson);
                $("#spanReasonDetail").html(ordRefund.ReasonDetail);
                //仅退款没有退货信息和商品
                if (ordRefund.RefundType == 0) {
                    $("#spanReceivePerson").html(ordRefund.ReceivePerson);
                    $("#spanReceiveTel").html(ordRefund.ReceiveTel);
                    if (ordRefund.ReceiveAddress != null) {
                        $("#spanReceiveAddress").html(ordRefund.ReceiveAddress + "(" + ordRefund.ReceivePostCode + ")");
                    }
                    $("#spanExpressCompany").html(ordRefund.ExpressCompany);
                    $("#spanWaybillNo").html(ordRefund.WaybillNo);
                    $("#spanReturnFreight").html("<lable style='color:red;'>￥</lable>" + ordRefund.ReturnFreight.toFixed(3));
                    if (ordRefund.SendBackDate == "0001-01-01 00:00:00") {
                        ordRefund.SendBackDate = '';
                    }
                    $("#spanSendBackDate").html(ordRefund.SendBackDate);
                    if (ordRefund.ReceiveDate == "0001-01-01 00:00:00") {
                        ordRefund.ReceiveDate = '';
                    }
                    $("#spanReceiveDate").html(ordRefund.ReceiveDate);
                    $("#spanReceiveRemark").html(ordRefund.ReceiveRemark);
                    $("#tbOrderRefundItem tr.dataTr").remove();
                    if (map.ordRefundItemList.length > 0) {
                        var ordRefundItemList = map.ordRefundItemList;
                        for (var i = 0; i < ordRefundItemList.length; i++) {
                            var ordRefundItem = ordRefundItemList[i];
                            var tr = $("<tr class=\"dataTr\"></tr>");
                            var td1 = $("<td>" + ordRefundItem.ProductsCode + "</td>")
                            td1.appendTo(tr);
                            var td2 = $("<td>" + ordRefundItem.ProductsName + "</td>")
                            td2.appendTo(tr);
                            var td3 = $("<td>" + ordRefundItem.ProductsSkuSaleprop + "</td>")
                            td3.appendTo(tr);
                            var td4 = $("<td>" + ordRefundItem.ProductsSkuCode + "</td>")
                            td4.appendTo(tr);
                            var td5 = $("<td>" + ordRefundItem.ProductsBatchCode + "</td>")
                            td5.appendTo(tr);
                            var td6 = $("<td>" + ordRefundItem.RefundNum + "</td>")
                            td6.appendTo(tr);
                            tr.insertAfter($("#tbOrderRefundItem").find("tr:last"));
                        }
                    } else {
                        $("<tr class=\"dataTr\"><td colspan=\"6\">没有数据</td><tr>").insertAfter($("#tbOrderRefundItem").find("tr:last"));
                    }
                    $(".dataTr").mouseover(function () {
                        $(this).css("background", "#e2e2e2");
                    }).mouseout(function () {
                        $(this).css("background", "#fff");
                    });
                } else {
                    $(".trRefundInfo").hide();
                    $("#tbOrderRefundItem").hide();
                }
            } else {
                $.MsgBox.Alert("提示", "售后单[" + billNo + "]不存在！");
            }
        },
        error: function () {
            $.MsgBox.Alert("提示", "获取售后单信息失败！");
        }
    });
}
//取消售后
function cancel() {
    $.messager.confirm('提示', "确认取消该售后单吗？", function (r) {
        if (r) {
            var billNo = $("#hdnBillNo").val();
            $.ajax({
                url: "/OrderRefund/OrderRefund/Cancel?billNo=" + billNo,
                type: "GET",
                cache: false,
                success: function (r) {
                    var map = $.parseJSON(r);
                    if (map.result == 1) {
                        $('#refreshCurrentPage').click();
                    } else {
                        $.MsgBox.Alert("提示", map.message);
                    }
                },
                error: function () {
                    $.MsgBox.Alert("提示", "取消售后失败！");
                }
            });
        }
    });
    
}