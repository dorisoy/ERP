$(function () {
    $("#btnSave").click(function () {
        $('#ff').form('submit', {
            url: "/Order/Download/SaveAutogeneration",
            type: "POST",
            dataType: "json",
            onSubmit: function () {
                var isValid = $(this).form('validate');
                return isValid;
            },
            success: function (r) {
                var map = $.parseJSON(r);
                var map = $.parseJSON(r);
                if (map.result == 1) {
                    parent.$("#refreshCurrentPage").click();
                    parent.$('#localWin').window('close');
                }
                else {
                    $.MsgBox.Alert("提示", map.message);
                }
            },
            error: function () {
                $.MsgBox.Alert("提示", "设置失败！");
            }
        });
    });

    $("#btnCancel").click(function () {
        parent.$('#localWin').window('close');
    });
});