﻿//初始化
$(function () {
    //初始化数据
    BindDictItem("Warehouse", "Warehouse");
    BindDictItem("Suppliers", "Suppliers");
    //选中特定值
    $("#Warehouse").combobox('setValue', '0');
    $("#Suppliers").combobox('setValue', '0');
    $("#btnSave").click(function () {
        btnSave();
    });
    //关闭
    $("#btnClose").click(function () {
        parent.$('#localWin').window('close');
    });
});

function btnSave() {
    $('#ff').form('submit', {
        url: "/Purchase/Purchase/Save",
        type: "POST",
        dataType: "json",
        onSubmit: function () {
            var isValid = $(this).form('validate');
            if (isValid) {
                var warehouceCode = $("#Warehouse").combobox('getValue');
                if (warehouceCode == "0") {
                    $.MsgBox.Alert("提示", "请选择仓库！");
                    isValid = false;
                }
            }
            if (isValid) {
                var suppliersID = $("#Suppliers").combobox('getValue');
                if (suppliersID == "0") {
                    $.MsgBox.Alert("提示", "请选择供应商！");
                    isValid = false;
                }
            }
            return isValid;	// 返回false终止表单提交
        },
        success: function (r) {
            var map = $.parseJSON(r);
            if (map.result == 1) {
                parent.$("#refresh").click();
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