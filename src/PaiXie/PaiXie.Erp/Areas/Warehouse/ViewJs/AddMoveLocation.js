$(function () {
    $("#btnSave").click(function () {
        $('#ff').form('submit', {
            url: "/Warehouse/MoveLocation/Save",
            type: "POST",//使用get方法访问后台
            dataType: "json",
            onSubmit: function () {
                var isValid = $(this).form('validate');
                return isValid;	// 返回false终止表单提交
            },
            success: function (r) {
                var map = $.parseJSON(r);
                if (map.result == 1) {
                    parent.$("#btnSearch").click();
                    $('#btnCancel').click();
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
});