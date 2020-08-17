$(function () {
    $("#btnSave").click(function () {
        btnSave();
    });
});

function btnSave() {
    $('#ff').form('submit', {
        url: "/Order/Download/SaveExpress",
        type: "POST",
        dataType: "json",
        onSubmit: function () {
        },
        success: function (r) {
            var map = $.parseJSON(r);
            if (map.result == 1) {
                parent.$('#localWin').window('close');
                parent.$('#refreshCurrentPage').click();
            }
            else {
                $.MsgBox.Alert("提示", map.message);
            }
        },
        error: function () {
            $.MsgBox.Alert("提示", "更改发货物流失败！");
        }
    });
}