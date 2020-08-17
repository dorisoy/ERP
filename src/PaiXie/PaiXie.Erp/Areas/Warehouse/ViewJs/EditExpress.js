//初始化
$(function () {
    $("#txtName").blur(checkName);
    $("#btnClose").click(function () {
        parent.$('#localWin').window('close');
    });
    $("#btnSave").click(btnSave);
});

function checkName() {
    var result = 0;
    if ($("#txtName").val() != "") {
        //快递名称是否已经存在于当前仓库
        $.ajax({
            url: "/Warehouse/Express/CheckName",
            type: "POST",
            data: { "id": $("#hdnID").val(), "name": $("#txtName").val() },
            async: false,
            cache: false,
            success: function (r) {
                var map = $.parseJSON(r);
                if (map.result == -1) {
                    $("#errorMessage").html("快递名称已存在！");
                    $("#txtName").focus();
                } else {
                    result = 1;
                    $("#errorMessage").html("");
                }
            },
            error: function () {
                $("#errorMessage").html("快递名称读取失败！");
            }
        });
    }
    return result;
}

function btnSave() {
    var result = checkName();
    if (result == 1) {
        var logisticsID = $("#logisticsID").combobox("getValue");
        if (logisticsID == 0) {
            $.MsgBox.Alert("提示", "请选择所属物流！");
            return false;
        }
        $('#ff').form('submit', {
            url: "/Warehouse/Express/Save",
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