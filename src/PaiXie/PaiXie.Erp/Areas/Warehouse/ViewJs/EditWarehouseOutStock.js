//初始化
$(function () {
    //切换出库类型
    $('#BillType').combobox({
        onChange: function () {
            var billType = $("#BillType").combobox('getValue');
            if (billType == 20) {
                $('#gldh').show();
            }
            else {
                $('#gldh').hide();
                $('#hdnSourceID').val("0");
                $("#SourceNo").val("");
            }
        }
    });
});
//关联单据号判断   是否存在
$("#SourceNo").blur(function () {
    checkSourceNo(20)
});
function checkSourceNo(billType) {
    var result = 0;
    if (billType == 20) {
        var billNo = $("#SourceNo").val();
        if (billNo != "") {
            $.ajax({
                url: "/Warehouse/WarehouseOutStock/CheckSourceNo?ram=" + Math.random(),
                data: { "BillNo": billNo },
                async: false,
                cache: false,
                success: function (r) {
                    var map = $.parseJSON(r);
                    if (map.result == 1) {
                        $("#errorMsg").html("");
                        $("#hdnSourceID").val(map.sourceID);
                        result = 1;
                    }
                    else {
                        $("#errorMsg").html(map.message);
                        $("#SourceNo").focus();
                    }
                },
                error: function () {
                    $("#errorMsg").html("入库单号读取失败！");
                    $("#SourceNo").focus();
                }
            });
        } else {
            $("#errorMsg").html("入库单号不能为空！");
            $("#SourceNo").focus();
        }
    } else {
        $("#errorMsg").html("");
        result = 1;
    }
    return result;
}
//关闭当前窗口
$("#btnCancel").click(function () {
    parent.$('#localWin').window('close');
});
//保存出库单
$("#btnSave").click(function () {
    btnSave();
});
function btnSave() {
    var BillType = $("#BillType").combobox('getValue');
    if (BillType != "") {
        var result = checkSourceNo(BillType);
        if (result == 1) {
            $('#ff').form('submit', {
                url: "/Warehouse/WarehouseOutStock/SaveOutStock?ram=" + Math.random(),
                type: "POST",//使用get方法访问后台
                dataType: "json",
                onSubmit: function () {
                    var isValid = $(this).form('validate');
                    return isValid;	// 返回false终止表单提交
                },
                success: function (r) {
                    var map = $.parseJSON(r);
                    if (map.result == 1) {
                        $("#btnCancel").click();
                        parent.$("#btnSerach").click();
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
    else {
        $.MsgBox.Alert("提示", "请选择出库类型！");
    }
}