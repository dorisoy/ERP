//初始化
$(function () {
    $("#txtCode").blur(checkCode);
    $("#btnClose").click(function () {
        parent.$('#localWin').window('close');
    });
    $("#btnSave").click(btnSave);
});

function checkCode() {
    var result = 1;
    if ($("#txtCode").val() != "") {
        //检查库位代码是否已经存在
        $.ajax({
            url: "/Warehouse/Location/CheckCode",
            type: "POST",
            data: { "id": $("#hdnID").val(), "code": $("#txtCode").val() },
            async: false,
            cache: false,
            success: function (r) {
                var map = $.parseJSON(r);
                if (map.result == -1) {
                    result = 0;
                    $("#errorMessage").html("库位编码已存在！");
                    $("#txtCode").focus();
                } else {
                    $("#errorMessage").html("");
                }
            },
            error: function () {
                result = 0;
                $("#errorMessage").html("读取库位代码失败！");
            }
        });
    }
    return result;
}

function btnSave() {
    var result = checkCode();
    if (result == 1) {
        $('#ff').form('submit', {
            url: "/Warehouse/Location/SaveChild",
            type: "POST",//使用get方法访问后台
            dataType: "json",
            onSubmit: function () {
                var isValid = $(this).form('validate');
                return isValid;	// 返回false终止表单提交
            },
            success: function (r) {
                var map = $.parseJSON(r);
                if (map.result == 1) {
                    if ($("#hdnID").val() == "0") {
                        parent.$("#refresh").click();
                    }
                    else {
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