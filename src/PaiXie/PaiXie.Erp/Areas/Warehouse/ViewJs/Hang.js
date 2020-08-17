$(function () {
    setTxtAreaFocus($("#txtHangRemark"));
    $('#btnClose').click(function () {
        parent.$('#localWin').window('close');
    });
    $('#btnSave').click(setHang);
});
function setHang() {
    var id = $("#hdnID").val();
    var hangRemark = $("#txtHangRemark").val().trim();
    if (hangRemark.length > 0) {
        $.ajax({
            url: "/Warehouse/Outbound/SetHang?type=0&id=" + id + "&hangRemark=" + encodeURIComponent(hangRemark),
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
                $.MsgBox.Alert("提示", "挂起失败！");
            }
        });
    } else {
        $.MsgBox.Alert("提示", "请输入挂起备注！");
    }
}