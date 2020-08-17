$(function () {
    $('#checkbox').click(function () {
        proportionalConversion();
    });
    proportionalConversion();

    $('#TransformationLeft .textbox-text').live('blur', function () {
        var text = $(this).val();
        if ($('#checkbox').attr('checked') || $('#checkbox').attr('checked') == 'checked') {
            $('#TransformationRight .textbox-text').each(function (index, element) {
                var zhblNum = $(this).parent().next().val();
                if (!isNaN(parseInt(text))) {
                    $(this).val(parseInt(text) * parseInt(zhblNum));
                    $(this).next().val(parseInt(text) * parseInt(zhblNum));
                }
                else
                {
                    $(this).val(text);
                    $(this).next().val(text);
                }
            });
        }
    });

    $('#PermitTransformation').click(function () {
        $(this).find('span').toggleClass('off');
        if ($(this).find('span').hasClass('off')) {
            $('#transleft').text('转出数量');
            $('#transright').text('转入数量');
            $("#hdnPermitTransformation").val("1");
        } else {
            $('#transright').text('转出数量');
            $('#transleft').text('转入数量');
            $("#hdnPermitTransformation").val("0");
        }
        return false;
    });


    $('#btnSave').click(function () { btnSave(); return false });

    $('#btnCancel').click(function () {
        parent.$('#win').window('close');
        location.href = "/Warehouse/ConversionRule/Index";
    });
});

function btnSave() {
    $('#ff').form('submit', {
        url: "/Warehouse/ConversionRule/SaveConversion?ram=" + Math.random(),
        type: "post",
        dataType: "json",
        data: $('#ff').serialize(),
        onSubmit: function () {
            var isValid = $(this).form('validate');
            var istrue = true;
            if ($('#PermitTransformation span').hasClass('off')) {
                $('#TransformationLeft tr').each(function (index, element) {
                    if (Number($(element).find('td:eq(2) .textbox-value').val()) > Number($(element).find('td:eq(1)').text())) {
                        $.MsgBox.Alert("提示", "商品转出数量必须小于中转仓数量！", 1000);
                        istrue = false;
                    }
                    if (isNaN(Number($(element).find('td:eq(2) .textbox-value').val())) || Number($(element).find('td:eq(2) .textbox-value').val()) == 0) {
                        $.MsgBox.Alert("提示", "转换数量不能小于0！", 1000);
                        istrue = false;
                    }
                });
            } else {
                $('#TransformationRight tr').each(function (index, element) {
                    if (Number($(element).find('td:eq(2) .textbox-value').val()) > Number($(element).find('td:eq(1)').text())) {
                        istrue = false;
                    }
                    if (isNaN(Number($(element).find('td:eq(2) .textbox-value').val())) || Number($(element).find('td:eq(2) .textbox-value').val()) == 0) {
                        $.MsgBox.Alert("提示", "转换数量不能小于0！", 1000);
                        istrue = false;
                    }
                });
            }
            if (!istrue) $.MsgBox.Alert("提示", "商品转出数量必须小于中转仓数量！", 1000);
            return (isValid && istrue);
        },
        success: function (r) {
            var map = $.parseJSON(r);
            if (map.result == 1) {
                var dlg = $.MsgBox.Alert("提示", "转换成功！", 1000);
                setTimeout(function () {
                    var src = "/Warehouse/ConversionRule/Index";
                    var current_tab = parent.$('#worktab').tabs('getSelected');
                    parent.$('#worktab').tabs('update', {
                        tab: current_tab,
                        options: {
                            content: '<iframe id="frmWorkArea00130" scrolling="no" data-options="fit:true" frameborder="0" style="  width: 100%; height: 100%; overflow: hidden;" src="' + src + '"></iframe>  '
                        }
                    });
                    parent.$('#win').window('close');
                }, 1000);
            } else {
                $.MsgBox.Alert("提示", map.message);
            }
        },
        error: function () {
            $.MsgBox.Alert("提示", "转换失败！");
        }
    });
}

function proportionalConversion() {
    $("#TransformationRight .textbox-text").each(function (index, element) {
        if ($('#checkbox').attr('checked') || $('#checkbox').attr('checked') == 'checked') {
            $(this).attr("disabled", true);
        }
        else {
            $(this).removeAttr("disabled");
        }

    });
    if ($('#checkbox').attr('checked') || $('#checkbox').attr('checked') == 'checked') {
        $("#TransformationLeft .textbox-text").blur();
    }
}