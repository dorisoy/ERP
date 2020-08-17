//初始化
$(function () {
    $("#txtName").focus();
    //品牌名称唯一性判断
    $("#txtName").blur(checkName);
    $("#btnClose").click(function () {
        parent.$('#localWin').window('close');
    });
    $("#btnSave").click(btnSave);
});

function checkName() {
    var result = 0;
    if ($("#txtName").val() != "") {
        $.ajax({
            url: "/Products/Brand/CheckName",
            data: { "name": $("#txtName").val(), "id": $("#ID").val() },
            async: false,
            cache: false,
            success: function (r) {
                var map = $.parseJSON(r);
                if (map.result == -1) {
                    $("#errorMessage").html("品牌名称已存在！");
                    $("#txtName").focus();
                } else {
                    result = 1;
                    $("#errorMessage").html("");
                }
            },
            error: function () {
                $("#errorMessage").html("品牌名称读取失败！");
            }
        });
    }
    return result;
}

function btnSave() {
    var result = checkName();
    if (result == 1) {
        $('#ff').form('submit', {
            url: "/Products/Brand/Save",
            type: "POST",//使用get方法访问后台
            dataType: "json",
            onSubmit: function () {
                var isValid = $(this).form('validate');
                return isValid;	// 返回false终止表单提交
            },
            success: function (r) {
                var map = $.parseJSON(r);
                if (map.result == 1) {
                    if ($("#ID").val() == "0") {
                        parent.$("#refresh").click();
                    } else {
                        parent.$("#refreshCurrentPage").click();
                    }
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
}