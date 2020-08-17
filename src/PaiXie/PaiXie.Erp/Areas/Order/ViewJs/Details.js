$(document).ready(function () {
    showControl();
    $('.tag li').click(function () {
        var id = $('.tag li').index($(this));
        switch (id) {
            case 0:
                location.href = "/Order/Details/Index?erpOrderCode=" + $("#hdnErpOrderCode").val();
                break;
            case 1:
                location.href = "/Order/Details/OrdItem?erpOrderCode=" + $("#hdnErpOrderCode").val();
                break;
            case 2:
                location.href = "/Order/Details/AccountsBill?erpOrderCode=" + $("#hdnErpOrderCode").val();
                break;
            case 3:
                location.href = "/Order/Details/Outbound?erpOrderCode=" + $("#hdnErpOrderCode").val();
                break;
                break;
            case 4:
                location.href = "/Order/Details/OrderRefund?erpOrderCode=" + $("#hdnErpOrderCode").val();
                break;
            case 5:
                location.href = "/Order/Details/Log?erpOrderCode=" + $("#hdnErpOrderCode").val();
                break;
        }
    });

    //添加优惠
    $('#addoffer').click(function () {
        if ($(this).hasClass('unclick')) return false;
        showLocalWindow('添加优惠', "/Order/AddOrder/AddDiscount?erpOrderCode=" + $("#hdnErpOrderCode").val(), 620, 450, true, false, false);
        $('#localWin').window({
            onClose: function () {
                var id = $('.tag li').index($(".current").eq(0));
                if (id == 1) {
                    window.location.reload(true);
                }
            }
        });
    });

    //添加商品
    $('#addpro').click(function () {
        showLocalWindow("订单商品", "/Order/AddOrder/AddProducts?erpOrderCode=" + $("#hdnErpOrderCode").val(), 720, 450, true, false, false);
        $('#localWin').window({
            onClose: function () {
                var id = $('.tag li').index($(".current").eq(0));
                if (id == 1) {
                    window.location.reload(true);
                }
            }
        });
    });

    //添加备注
    $('#addRemarks').click(function () {
        if ($(this).hasClass('unclick')) return false;
        showLocalWindow("备注", "/Order/Details/AddRemark?erpOrderCode=" + $("#hdnErpOrderCode").val(), 540, 350, true, false, false);
        $('#localWin').window({
            onClose: function () {
                var id = $('.tag li').index($(".current").eq(0));
                if (id == 0) {
                    window.location.reload(true);
                }
            }
        });
    });

    //挂起订单
    $('#pendingOrder').click(function () {
        if ($(this).hasClass('unclick')) return false;
        if ($("#pendingOrder").text() == "挂起订单") {
            showLocalWindow("挂起订单", "/Order/Details/Hang?erpOrderCode=" + $("#hdnErpOrderCode").val(), 520, 320, true, false, false);
            $('#localWin').window({
                onClose: function () {
                    var id = $('.tag li').index($(".current").eq(0));
                    if (id == 0) {
                        window.location.reload(true);
                    }
                }
            });
        }
        else {
            $.ajax({
                url: "/Order/Details/CancelHang",
                type: "POST",
                cache: false,
                data: { erpOrderCode: $("#hdnErpOrderCode").val() },
                success: function (r) {
                    var map = $.parseJSON(r);
                    if (map.result == 1) {
                        window.location.reload(true);
                    } else {
                        $.MsgBox.Alert("提示", map.message);
                    }
                },
                error: function () {
                    $.MsgBox.Alert("提示", "取消挂起失败！");
                }
            });
        }
    });

    //添加收款
    $('#addCollection').click(function () {
        if ($(this).hasClass('unclick')) return false;
        showLocalWindow("添加收款", "/Order/Details/AddCollection?erpOrderCode=" + $("#hdnErpOrderCode").val() + "&amount=" + $("#hdnAbnormalAmount").val(), 500, 350, true, false, false);
        $('#localWin').window({
            onClose: function () {
                var id = $('.tag li').index($(".current").eq(1));
                if (id == 0) {
                    window.location.reload(true);
                }
            }
        });
    });

    //添加退款
    $('#addRefund').click(function () {
        if ($(this).hasClass('unclick')) return false;
        showLocalWindow("添加退款", "/Order/Details/AddRefund?erpOrderCode=" + $("#hdnErpOrderCode").val() + "&amount=" + $("#hdnAbnormalAmount").val(), 500, 350, true, false, false);
        $('#localWin').window({
            onClose: function () {
                var id = $('.tag li').index($(".current").eq(1));
                if (id == 0) {
                    window.location.reload(true);
                }
            }
        });
    });

    //取消订单
    $('#cancelOrder').click(function () {
        if ($(this).hasClass('unclick')) return false;
        $.messager.confirm('提示', '取消后不能恢复，确认取消该订单吗？', function (r) {
            if (r) {
                $.ajax({
                    url: "/Order/Details/CancelOrder",
                    type: "GET",
                    cache: false,
                    data: { erpOrderCode: $("#hdnErpOrderCode").val() },
                    success: function (r) {
                        var map = $.parseJSON(r);
                        if (map.result == 1) {
                            $.MsgBox.Alert("提示", "取消成功！", 0);
                            var id = $('.tag li').index($(".current").eq(0));
                            if (id == 0) {
                                window.location.reload(true);
                            }
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
    });

    //分配仓库
    $('#distributionWarehouse').click(function () {
        if ($(this).hasClass('unclick')) return false;
        location.href = "/Order/DistributionWarehouse/Index?erpOrderCode=" + $("#hdnErpOrderCode").val();
    });
});

//按钮显示控制
function showControl() {
    var abnormalAmount = Number($("#hdnAbnormalAmount").val());
    if (abnormalAmount == 0) {
        //禁用添加收款
        $("#addCollection").addClass("unclick");
        //禁用添加退款
        $("#addRefund").addClass("unclick");
    }
    else {
        if (abnormalAmount > 0) {
            //禁用添加退款
            $("#addRefund").addClass("unclick");
        }
        else {
            //禁用添加收款
            $("#addCollection").addClass("unclick");
        }
    }

    if ($("#hdnIsHand").val() == "1") {
        $("#pendingOrder").text("取消挂起");
    }

    var orderStatus = $("#hdnOrderStatus").val();
    if (orderStatus != "10" && orderStatus != "20" && orderStatus != "30") {
        //禁用分配仓库
        $("#distributionWarehouse").addClass("unclick");
    }
}