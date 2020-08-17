//初始化
$(function () {
    //加载出库单基本信息和出库单商品
    initOutboundInfo();
    //挂起
    $("#btnHang").click(function () {
        setHang(0);
    });
    //取消挂起
    $("#btnCancelHang").click(function () {
        setHang(1);
    });
    //取消出库单
    //$("#btnCancel").click(cancel);
    //刷新当前页
    $("#refreshCurrentPage").click(function () {
        initOutboundInfo();
    });
    //选项卡
    $('#tt').tabs({
        onSelect: function (title, index) {
            switch (title) {
                case "基本信息":
                    initOutboundInfo();
                    break;
                case "出库单日志":
                    initOutboundLog();
                    break;
            }
        }
    });
});

//加载出库单基本信息和出库单商品
function initOutboundInfo() {
    var billNo = $("#hdnBillNo").val();
    $.ajax({
        url: "/Warehouse/Outbound/GetOutboundInfo?billNo=" + billNo,
        type: "GET",
        cache: false,
        success: function (r) {
            var map = $.parseJSON(r);
            if (map.outbound) {
                var outbound = map.outbound;
                $("#hdnID").val(outbound.ID);
                $("#spanBillNo").html(outbound.BillNo);
                $("#spanErpOrderCode").html(outbound.ErpOrderCode);
                $("#spanOutOrderCode").html(outbound.OutOrderCode);
                $("#spanStatusName").html(map.statusName);
                if (outbound.Status != 30 && outbound.Status != 99) {
                    if (outbound.IsHang == 1) {
                        $("#btnCancelHang").show();
                        $("#btnHang").hide();
                        $("#spanIsHang").html("<div id=\"hangRemark\" class=\"easyui-panel easyui-tooltip\" title=\"挂起备注：" + outbound.HangRemark + "\" style=\"color:#ff0000; width:35px;\">[挂起]</div>");
                        //挂起备注
                        $('#hangRemark').tooltip({
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
                    } else {
                        $("#btnHang").show();
                        $("#btnCancelHang").hide();
                        $("#spanIsHang").html("");
                    }
                } else {
                    $(".buttonList").hide();
                    $("#btnHang").hide();
                    $("#btnCancelHang").hide();
                }
                $("#spanIsWaitPurchase").html(outbound.IsWaitPurchase == 1 ? "是" : "否");
                $("#spanCreateDate").html(outbound.CreateDate);
                $("#spanIsNeedInvoice").html(outbound.IsNeedInvoice == 1 ? "是" : "否");
                $("#spanInvoiceInfo").html(outbound.InvoiceInfo);
                $("#spanBuyName").html(outbound.BuyName);
                $("#spanBuyTel").html(outbound.BuyTel);
                $("#spanBuyMtel").html(outbound.BuyMtel);
                $("#spanBuyAddr").html(outbound.BuyAddr);
                $("#spanExpressName").html(map.expressName);
                $("#spanWaybillNo").html(outbound.WaybillNo);
                $("#spanFreight").html("<lable style='color:red;'>￥</lable>" + outbound.Freight.toFixed(3));
                if (outbound.DeliveryDate == "0001-01-01 00:00:00") {
                    outbound.DeliveryDate = '';
                }
                $("#spanDeliveryDate").html(outbound.DeliveryDate);
                if (outbound.ExpectedDeliDate != "0001-01-01 00:00:00") {
                    outbound.ExpectedDeliDate = outbound.ExpectedDeliDate.split(' ')[0];
                } else {
                    outbound.ExpectedDeliDate = '';
                }
                $("#spanExpectedDeliDate").html(outbound.ExpectedDeliDate);
                $("#tbPickItem tr.dataTr").remove();
                if (map.pickItemList.length > 0) {
                    var pickItemList = map.pickItemList;
                    for (var i = 0; i < pickItemList.length; i++) {
                        var pickItem = pickItemList[i];
                        var tr = $("<tr class=\"dataTr\"></tr>");
                        var td1 = $("<td>" + pickItem.ProductsCode + "</td>")
                        td1.appendTo(tr);
                        var td2 = $("<td>" + pickItem.ProductsName + (pickItem.LocationID == 0 ? "<span style=\"color:red;\">[预售]</span>" : "") + "</td>")
                        td2.appendTo(tr);
                        var td3 = $("<td>" + pickItem.ProductsSkuSaleprop + "</td>")
                        td3.appendTo(tr);
                        var td4 = $("<td>" + pickItem.ProductsSkuCode + "</td>")
                        td4.appendTo(tr);
                        var td5 = $("<td>" + pickItem.ProductsUnit + "</td>")
                        td5.appendTo(tr);
                        var td6 = $("<td>" + pickItem.Num + "</td>")
                        td6.appendTo(tr);
                        var td7 = $("<td>" + pickItem.ProductsWeight.toFixed(3) + "</td>")
                        td7.appendTo(tr);
                        var td8 = $("<td>" + (pickItem.LocationCode == null || pickItem.LocationCode == "" ? "－" : pickItem.LocationCode) + "</td>")
                        td8.appendTo(tr);
                        var td9 = $("<td>" + (pickItem.ProductsBatchCode == null || pickItem.ProductsBatchCode == "" ? "－" : pickItem.ProductsBatchCode) + "</td>")
                        td9.appendTo(tr);
                        //var html = '－';
                        //if (outbound.Status == 0) {
                            //html = "<a href=\"javascript:void(0);\" onclick=\"reject(" + pickItem.OrditemID + ");\">驳回</a>";
                        //}
                        //var td10 = $("<td>" + html + "</td>")
                        //td10.appendTo(tr);
                        tr.insertAfter($("#tbPickItem").find("tr:last"));
                    }
                }else{
                    $("<tr class=\"dataTr\"><td colspan=\"9\">没有数据</td><tr>").insertAfter($("#tbPickItem").find("tr:last"));
                }
            } else {
                $.MsgBox.Alert("提示", "出库单[" + billNo + "]不存在！");
            }
        },
        error: function () {
            $.MsgBox.Alert("提示", "获取出库单信息失败！");
        }
    });
}
//加载出库单日志
function initOutboundLog() {
    var billNo = $("#hdnBillNo").val();
    $.ajax({
        url: "/Warehouse/Outbound/GetOutboundLog?billNo=" + billNo,
        type: "GET",
        cache: false,
        success: function (r) {
            var map = $.parseJSON(r);
            $("#tbOutboundLog tr.dataTr").remove();
            if (map.ordLogList.length > 0) {
                for (var i = 0; i < map.ordLogList.length; i++) {
                    var ordLog = map.ordLogList[i];
                    var tr = $("<tr class=\"dataTr\"></tr>");
                    var td1 = $("<td>" + ordLog.CreateDate + "</td>")
                    td1.appendTo(tr);
                    var td2 = $("<td style=\"text-align:left;\">" + ordLog.Message + "</td>")
                    td2.appendTo(tr);
                    var td3 = $("<td>" + ordLog.UserCode + "<span style=\"color:#999;\" >(" + ordLog.UserName + ")</span></td>")
                    td3.appendTo(tr);
                    tr.insertAfter($("#tbOutboundLog").find("tr:last"));
                }
            } else {
                $("<tr class=\"dataTr\"><td colspan=\"3\">没有数据</td><tr>").insertAfter($("#tbOutboundLog").find("tr:last"));
            }
        },
        error: function () {
            $.MsgBox.Alert("提示", "获取出库单日志失败！");
        }
    });
}

//挂起、取消挂起
function setHang(type) {
    var id = $("#hdnID").val();
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
                    //$('#refreshCurrentPage').click();
                    $("#btnHang").show();
                    $("#btnCancelHang").hide();
                    $("#spanIsHang").html("");
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
//驳回商品
function reject(ordItemID) {
    $.MsgBox.Alert("提示", "驳回商品待实现！");
}