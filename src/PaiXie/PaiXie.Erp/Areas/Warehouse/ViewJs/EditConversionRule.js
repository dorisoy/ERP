//初始化
$(function () {
    $('.jsdel').live('click', function () {
        $(this).parent().parent().remove();
        if ($(this).prev().val() == "0") {
            $("#btnSelectCustomer").show();
        }
    });

    $('#btnSelectCustomer').live('click', function () {
        showLocalWindow("添加转换商品", "/Warehouse/ConversionRule/AddProducts?location=Left", 600, 250, true, false, false);
    });

    $('#btnSelectCustomerRight').live('click', function () {
        showLocalWindow("添加转换商品", "/Warehouse/ConversionRule/AddProducts?location=Right", 600, 250, true, false, false);
    });

    $('#btnSave').click(function () { btnSave(); return false });

    $('#btnCancel').click(function () { window.location.href = "/Warehouse/ConversionRule/Index"; });

    $("#btnSaveProducs").click(function () {
        btnSaveProducs();
    });

    var location = $("#hdnLocation").val();
    if (location == "Left") {
        $("#txtNum").numberbox('setValue', "1").combobox('disable');
    }
    if ($("#TransformationLeft tr").length > 0) {
        $("#btnSelectCustomer").hide();
    }
});

//删除转换商品
function DeleteTr(obj) {
    $(obj).parent().parent().remove();
}

//添加转换规则
function btnSave() {
    if ($("#TransformationLeft tr").length == 0 || $("#TransformationRight tr").length == 0) {
        $.MsgBox.Alert("提示", "请添加转换商品！");
        return false;
    }

    $('#ff').form('submit', {
        url: "/Warehouse/ConversionRule/Save",
        type: "POST",
        dataType: "json",
        onSubmit: function () {
            var isValid = $(this).form('validate');
            return isValid;
        },
        success: function (r) {
            var map = $.parseJSON(r);
            if (map.result == 1) {
                $.MsgBox.Alert("提示", map.message, 1000);
                setTimeout(function t() { window.location.reload(); }, 1000);
            } else {
                $.MsgBox.Alert("提示", map.message);
            }
        },
        error: function () {
            $.MsgBox.Alert("提示", "保存失败！");
        }
    });
}

//添加转换商品
function btnSaveProducs() {
    var isValid = $('#ff').form('validate');
    if (!isValid) return false;
    var code = $("#txtCode").val();
    var num = $("#txtNum").val();
    var flag = true;
    parent.$("input[name='ProductsSkuCode']").each(function () {
        if ($(this).val() == code) {
            $("#msg").text("商品已添加！");
            flag = false;
        }
    });
    if (flag) {
        $.ajax({
            url: "/Warehouse/ConversionRule/SaveProducts",
            data: { "Code": code, "Num": num },
            type: "GET",
            cache: false,
            success: function (r) {
                var map = $.parseJSON(r);
                if (map.result == 1) {
                    var location = $("#hdnLocation").val();
                    parent.$('#ff').contents().find('#Transformation' + location).append('<tr><td width="50%">' + $('#txtCode').val() + '<input type="hidden" name="ProductsSkuCode" value="' + $('#txtCode').val() + '"></td><td width="25%">' + $('#txtNum').val() + '<input type="hidden" name="Num" value="' + $('#txtNum').val() + '"></td><td width="25%"><input type="hidden" name="ConversionWay" value="' + (location == "Left" ? "0" : "1") + '"><a href="javascript:void(0)" class="red jsdel">×</a></td></tr>');
                    parent.$('#localWin').window('close');
                    if (location == "Left") {
                        parent.$("#btnSelectCustomer").hide();
                    }
                }
                else {
                    $("#msg").text(map.message);
                }
            },
            error: function () {
                $("#msg").text("添加失败！");
            }
        });
    }
}
