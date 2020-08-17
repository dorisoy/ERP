$(function () {
    $("#btnSave").click(function () {
        if ($("input[name='LocationID']:checked").length == 0) {
            $.MsgBox.Alert("提示", "请选择盘点库区！");
            return false;
        }
        $('#ff').form('submit', {
            url: "/Warehouse/Stocktaking/Save",
            type: "POST",
            dataType: "json",
            onSubmit: function () {
                var isValid = $(this).form('validate');
                return isValid;
            },
            success: function (r) {
                var map = $.parseJSON(r);
                if (map.result == 1) {
                    parent.search(1);
                    $("#btnCancel").click();
                } else {
                    $.MsgBox.Alert("提示", map.message);
                }
            },
            error: function () {
                $.MsgBox.Alert("提示", "添加失败！");
            }
        });
    });

    $("#btnCancel").click(function () {
        parent.$('#localWin').window('close');
    });

    $("input[name='LocationID']").change(function () {
        $(this).next().prop('checked',$(this).prop('checked'));
    });
});