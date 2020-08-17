$(function () {
    //搜索商品
    $("#btnSearchProducts").click(function () {
        var flag = true;
        var productsCode = $("#txtProductsCode").val();
        var billNo = $("#hdnBillNo").val();
        $.ajax({
            url: "/Warehouse/PurchasePlan/CheackProductsCode?productsCode=" + productsCode + "&billNo=" + billNo,
            type: "GET",
            cache: false,
            async: false,
            success: function (r) {
                var map = $.parseJSON(r);
                if (map.result == 0) {
                    $.messager.alert('提示', map.message, 'err');
                    flag = false;
                }
            },
            error: function () {
                showMsg("提示", "查找失败！", false);
            }
        });

        if (flag)
            location.href = "/Warehouse/PurchasePlan/AddProducts?productsCode=" + productsCode + "&billNo=" + billNo;
    });

    //添加商品
    $("#btnSaveProducts").click(function () {
        $('#ff').form('submit', {
            url: "/Warehouse/PurchasePlan/AddProductsSave",
            type: "POST",
            dataType: "json",
            onSubmit: function () {
                var isValid = $(this).form('validate');
                return isValid;
            },
            success: function (r) {
                var map = $.parseJSON(r);
                if (map.result == 1) {
                    $.messager.alert('提示', map.message, 'info');
                    location.href = "/Warehouse/PurchasePlan/AddProducts";
                }
                else {
                    $.messager.alert('提示', map.message, 'err');
                }
            },
            error: function () {
                $.messager.alert('提示', "修改失败", 'err');
            }
        });
    });
});