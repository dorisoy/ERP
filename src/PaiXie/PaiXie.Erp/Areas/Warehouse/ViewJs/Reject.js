$(function () {
    setTxtAreaFocus($("#txtRejectRemark"));
    $('#btnClose').click(function () {
        parent.$('#localWin').window('close');
    });
    $('#btnSave').click(setReject);
});
function setReject() {
    var ids = $("#hdnIDs").val();
    var rejectRemark = $("#txtRejectRemark").val().trim();
    if (rejectRemark.length > 0) {
        $.ajax({
            url: "/Warehouse/Outbound/SetReject?ids=" + ids + "&rejectRemark=" + encodeURIComponent(rejectRemark),
            type: "GET",
            cache: false,
            success: function (r) {
                var map = $.parseJSON(r);
                if (map.result == 1) {
                    $('#btnClose').click();
                    parent.$('#refreshCurrentPage').click();
                } else {
                    $.MsgBox.Alert("提示", map.message);
                }
            },
            error: function () {
                $.MsgBox.Alert("提示", "驳回失败！");
            }
        });
    } else {
        $.MsgBox.Alert("提示", "请输入驳回备注！");
    }
}