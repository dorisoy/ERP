$(function () {
    var aa = $("input[name='IsEnable']:checkbox");
    aa.bind("click", function () {
        if ($(this).attr("checked")) {
            $("#IsEnable").val(1);
        }
        else {
            $("#IsEnable").val(0);
        }
    })
});
//唯一性判断
$("#Code").blur(checkCode);
$("#btnClose").click(function () {
    parent.$('#localWin').window('close');
});
$("#btnSave").click(function () {
    btnSave();
});
function btnSave() {
    var result = checkCode();
    if (result == 1) {
        $('#ff').form('submit', {
            url: "/sys/dict/Savecode?ram=" + Math.random(),
            type: "POST",//使用get方法访问后台
            dataType: "json",
            onSubmit: function () {
                var isValid = $(this).form('validate');
                return isValid;	// 返回false终止表单提交
            },
            success: function (r) {
                var map = $.parseJSON(r);
                if (map.result == 1) {
                    parent.$("#refreshcode").click();
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

function checkCode() {
    var result = 0;
    if ($("#Code").val() == "0") {
        $("#lblcode").text("代码不能为0！");
        $("#Code").focus();
    } else {
        $.ajax({
            url: "/sys/dict/CheckCodes",
            data: { "code": $("#Code").val(), "ID": $("#ID").val() },
            async: false,
            cache: false,
            success: function (r) {
                var map = $.parseJSON(r);
                if (map.result == -1) {
                    $("#lblcode").text("代码已存在！");
                    $("#Code").focus();
                } else {
                    result = 1;
                    $("#lblcode").text("");
                }
            },
            error: function () {
                $.MsgBox.Alert("提示", "读取代码失败！", 1000);
            }
        });
    }
    return result;
}