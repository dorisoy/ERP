$(function () {
    $('#btnClose').click(function () {
        parent.$('#localWin').window('close');
    });
    $('#btnSave').click(ConfirmSplit);
});
function ConfirmSplit() {
    var id = $("#hdnID").val();
    $.ajax({
        url: "/Warehouse/Outbound/SplitOutbound?id=" + id,
        type: "GET",
        cache: false,
        success: function (r) {
            var map = $.parseJSON(r);
            if (map.result == 1) {
                parent.$('#refreshCurrentPage').click();
                $('#btnClose').click();
            } else {
                $.MsgBox.Alert("提示", map.message);
            }
        },
        error: function () {
            $.MsgBox.Alert("提示", "拆分失败！");
        }
    });
}