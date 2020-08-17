$(function () {
    $("#btnSave").click(function () {
        $('#ff').form('submit', {
            url: "/Warehouse/PurchasePlan/Save",
            type: "POST",
            dataType: "json",
            onSubmit: function () {
                var isValid = $(this).form('validate');
                return isValid;
            },
            success: function (r) {
                var map = $.parseJSON(r);
                if (map.result == 1) {
                    $.MsgBox.Alert("提示", map.message,1000);
                    parent.$('#btnSearch').click();
                    parent.$('#localWin').window('close');
                }
                else {
                    $.MsgBox.Alert("提示", map.message);
                }
            },
            error: function () {
                $.MsgBox.Alert("提示", "添加失败！");
            }
        });
    });
});