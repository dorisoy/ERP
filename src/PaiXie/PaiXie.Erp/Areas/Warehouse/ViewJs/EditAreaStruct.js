//初始化
$(function () {
    //检查父级是否已经有子级结构，顶级不限制，但是子级只能有一个
    $("#txtName").blur(checkName);
    $("#btnClose").click(function () {
        parent.$('#localWin').window('close');
    });
    $("#btnSave").click(btnSave);
});

function checkName() {
    var result = 0;
    if ($("#txtName").val() != "") {
        //检查父级是否已经有子级结构，顶级不限制，但是子级只能有一个
        $.ajax({
            url: "/Warehouse/AreaStruct/CheckName",
            data: { "id": $("#hdnID").val(), "name": $("#txtName").val(), "parentID": $("#ParentID").combotree("getValue") },
            async: false,
            cache: false,
            success: function (r) {
                var map = $.parseJSON(r);
                if (map.result == -1) {
                    $("#errorMessage").html(map.message);
                    $("#txtName").focus();
                } else {
                    result = 1;
                    $("#errorMessage").html("");
                }
            },
            error: function () {
                $("#errorMessage").html("读取结构名称失败！");
            }
        });
    }
    return result;
}

function btnSave() {
    var result = checkName();
    if (result == 1) {
        $('#ff').form('submit', {
            url: "/Warehouse/AreaStruct/Save",
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