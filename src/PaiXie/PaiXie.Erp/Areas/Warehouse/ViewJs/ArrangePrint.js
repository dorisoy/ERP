//初始化
$(function () {
    //绑定快递下拉列表
    $('#express').combobox({
        url: '/Warehouse/Express/JsonTree',
        valueField: 'Value',
        textField: 'Text',
        onLoadSuccess: function () { //数据加载完毕事件
            $("#express").combobox('select', "0");
        }
    });
    $("#btnSave").click(function () {
        btnSave();
    });

    $("#btnClose").click(function () {
        parent.$('#localWin').window('close');
    });
});

//保存
function btnSave() {
    var ids = $("#hdnOutboundIDs").val();
    var deliveryID = $("#express").combobox("getValue");
    $('#ff').form('submit', {
        url: "/Warehouse/Outbound/SavePrintBatch?ids=" + ids + "&deliveryID=" + deliveryID + "&ram=" + Math.random(),
        type: "GET",
        dataType: "json",
        onSubmit: function () {
            var isValid = $(this).form('validate');
            if (isValid) {
                if (deliveryID == 0) {
                    parent.$.MsgBox.Alert("提示", "请选择打印快递！");
                    isValid = false;
                }
            }
            return isValid;	// 返回false终止表单提交
        },
        success: function (r) {
            var map = $.parseJSON(r);
            if (map.result == 1) {
                parent.$.MsgBox.Alert("提示", "安排成功！", 1000);
                $("#btnClose").click();
                parent.$("#btnReset").click();
            } else {
                parent.$.MsgBox.Alert("提示", map.message);
            }
        },
        error: function () {
            parent.$.MsgBox.Alert("提示", "安排打印失败！");
        }
    });
}