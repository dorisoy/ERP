//初始化
$(function () {
    $("#btnClose").click(function () {
        parent.$('#localWin').window('close');
    });
    $("#btnSave").click(btnSave);
});
function btnSave() {
    $('#ff').form('submit', {
        url: "/Products/Products/SaveBrand?ids=" + $("#productsIDs").val() + "&BrandID=" + $("#Brand").combobox('getValue'),
        type: "POST",//使用get方法访问后台
        dataType: "json",
        onSubmit: function () {
            var isValid = $(this).form('validate');
            return isValid;	// 返回false终止表单提交
        },
        success: function (r) {
            var map = $.parseJSON(r);
            if (map.result == 1) {
                parent.$("#refreshCurrentPage").click();
                $("#btnClose").click();
            }
            else {
                $.MsgBox.Alert("提示", map.message);
            }
        },
        error: function () {
            $.MsgBox.Alert("提示", "保存失败！");
        }
    });
}