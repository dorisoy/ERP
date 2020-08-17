//初始化
$(function () {
    $("#btnSave").click(function () {
        save();
    });
    $("#btnClose").click(function () {
        parent.$('#localWin').window('close');
    });
    //全选、反选
    $("#chkAll").click(function () {
        if ($(this).attr("checked")) {
            $("input[name='chkID']").each(function (index, element) {
                enableTxtWaybillNo(index);
                $(element).attr("checked", true);
            });
        } else {
            $("input[name='chkID']").each(function (index, element) {
                disabledTxtWaybillNo(index);
                $(element).attr("checked", false);
            });
        }
    });
    //单个选中、单个取消选中
    $("input[name='chkID']").each(function (index, element) {
        $(element).click(function () {
            if (!$(element).attr("checked")) {
                //取消全选
                disabledTxtWaybillNo(index);
                $("#chkAll").attr("checked", false);
            } else {
                enableTxtWaybillNo(index);
                var unCheckedBoxs = $("input[name='chkID']").not("input:checked");
                if (unCheckedBoxs.length == 0) {
                    $("#chkAll").attr("checked", true);
                }
            }
        });
    });
    //验证运单号是否符合规则
    $('input[id^="txtWaybillNo_"]').each(function (index, element) {
        $(element).blur(function () {
                var logisticsCode = $("#hdnLogisticsCode").val().toUpperCase();
                var waybillNo = $.trim($(element).val());
                var msg = '';
                var isBreak = false;
                if (waybillNo != "") {
                    if (isCode(waybillNo)) {
                        var result = chkWaybillNoRules(logisticsCode, waybillNo);
                        if (!result) {
                            msg = "运单号不符合规则！";
                        }

                    } else {
                        msg = "请输入英文或数字！";
                        isBreak = true;
                    }
                }
                showErrMsg(index, msg);
                if (isBreak) {
                    clearSetFocus($(element));
                }
        });
    });
    //连号
    $('#chkContinuous').click(waybillNoContinuous);
});
//运单号连号
function waybillNoContinuous() {
    var waybillNos = $('input[id^="txtWaybillNo_"]').not("[readonly]");
    if (waybillNos.size() == 0) {
        $.MsgBox.Alert("提示", "请选择要连号的出库单！");
        return false;
    }
    if (!$("#chkContinuous").attr("checked")) {
        waybillNos.each(function (index, element) {
            if (index > 0) {
                $(element).val('');
                var rowIndex = $(element).attr("index");
                showErrMsg(rowIndex, "");
            }
        });
        return;
    }
    var firstCheckedRowIndex = $(waybillNos[0]).attr("index");
    var waybillNo_0 = $.trim($(waybillNos[0]).val());
    if (waybillNo_0.length == 0) {
        showErrMsg(firstCheckedRowIndex, "请输入运单号！");
        clearSetFocus($(waybillNos[0]));
        return false;
    }
    if (!isCode(waybillNo_0)) {
        showErrMsg(firstCheckedRowIndex, "请输入英文或数字！");
        clearSetFocus($(waybillNos[0]));
        return false;
    }
    var logisticsCode = $("#hdnLogisticsCode").val().toUpperCase();
    if (!chkWaybillNoRules(logisticsCode, waybillNo_0)) {
        showErrMsg(firstCheckedRowIndex, "运单号不符合规则！");
        clearSetFocus($(waybillNos[0]));
        return false;
    }
    var x = 0;
    var y = 0;
    //开头有字母的单号，包括0（如：EM、EM0）
    for (var i = 0; i < waybillNo_0.length; i++) {
        if (isNum(Mid(waybillNo_0, i, 1)) && Mid(waybillNo_0, i, 1) != "0") {
            break;
        }
        x++;
    }
    //结束有字母的单号
    for (var i = waybillNo_0.length - 1; i >= 0; i--) {
        if (isNum(Mid(waybillNo_0, i, 1))) {
            break;
        }
        y++;
    }
    //EMS前后两位原字母现在改数字
    if (logisticsCode == "EMS" || logisticsCode == "EYB" || logisticsCode == "EMSBZ" || logisticsCode == "EMSJJ") {
        x = 2;
        y = 2;
    }

    //开始字母
    var beginLetter = Left(waybillNo_0, x);
    if (beginLetter == null) beginLetter = "";
    //结束字母
    var endLetter = Right(waybillNo_0, y);
    if (endLetter == null) endLetter = "";

    //EMS、顺丰、宅急送 最后一位数字是校验码，要去掉，然后动态生成
    if (logisticsCode == "EMS" || logisticsCode == "EYB" || logisticsCode == "SF") {
        y++;
    }

    //中间连续的数字
    var centerNumber = Mid(waybillNo_0, x, waybillNo_0.length - x - y);
    if (isNum(centerNumber) == false) {                
        showErrMsg(firstCheckedRowIndex, "运单号数字必须大于0！");
        clearSetFocus($(waybillNos[0]));
        return false;
    }
    centerNumber = Number(centerNumber);
    //记录原长度，跟自动累加后的长度对比
    var oldCenterNumberLength = centerNumber.toString().length;
    for (var i = 1; waybillNos.length > i; i++) {
        centerNumber += 1;
        var centerNumberLength = centerNumber.toString().length;
        if (oldCenterNumberLength < centerNumberLength) {
            //位数进了，去掉0，例如单号：EM099999990CS，则 beginLetter = EM0，下一个单号为：EM100000003CS，则 beginLetter = EM
            if (Right(beginLetter, 1) == "0") {
                beginLetter = Left(beginLetter, beginLetter.length - 1);
            }
            oldCenterNumberLength = centerNumberLength;
        }

        //开始字母 + 累加后的数字 + 验证码 + 结束字母
        var newCenterNumber = centerNumber;
        //由于原快递号中间有数字会去掉以0开头的字符，这个补回0
        if (oldCenterNumberLength > newCenterNumber.toString().length) {
            newCenterNumber = Left("0000000", oldCenterNumberLength - centerNumber.toString().length) + centerNumber;
        }
        $(waybillNos[i]).val(beginLetter + newCenterNumber.toString() + getWaybillNoCode(newCenterNumber, logisticsCode, $(waybillNos[i - 1]).val()) + endLetter);
    }
}
//LEFT，RIGHT，MID函数
function Left(str, len) {
    if (len > 0) {
        return str.substring(0, len);
    } else {
        return null;
    }
}

function Right(str, len) {
    if (str.length - len >= 0 && str.length >= 0 && str.length - len <= str.length) {
        return str.substring(str.length - len, str.length);
    } else {
        return null;
    }
}

function Mid(str, starIndex, len) {
    if (str.length >= 0) {
        return str.substr(starIndex, len);
    } else {
        return null;
    }
}
clearSetFocus = function (obj) {
    var t = $(obj).val();
    $(obj).val("").focus().val(t);
}
//保存运单号
function save() {
    var chkIDs = $("input[name='chkID']:checked");
    if (chkIDs.length > 0) {
        var ids = [];
        var waybillNos = [];
        var deliveryExpressID = $("#hdnDeliveryExpressID").val();
        for (var i = 0; i < chkIDs.length; i++) {
            var index = $(chkIDs[i]).attr("index");
            var id = $(chkIDs[i]).val();
            var waybillNo = $.trim($("#txtWaybillNo_" + index).val());
            if (waybillNo != "") {
                if (isCode(waybillNo)) {
                    ids.push(id);
                    waybillNos.push(waybillNo);
                } else {
                    showErrMsg(index, "请输入英文或数字！")
                    clearSetFocus($("#txtWaybillNo_" + index));
                    return false;
                }
            }
            else {
                showErrMsg(index, "请输入运单号！");
                clearSetFocus($("#txtWaybillNo_" + index));
                return false;
            }
        }
        $.ajax({
            url: "/Warehouse/Outbound/SaveWaybillNo",
            type: "POST",
            async: false,
            cache: false,
            data: { ids: ids.join(","), waybillNos: waybillNos.join(","), deliveryExpressID: deliveryExpressID },
            success: function (r) {
                var map = $.parseJSON(r);
                if (map.result == 1) {
                    parent.$('#refreshCurrentPage').click();
                    $('#btnClose').click();
                } else {
                    $.MsgBox.Alert("提示", map.message);
                }
            },
            error: function () {
                $.MsgBox.Alert("提示", "保存失败！");
            }
        });
    } else {
        $.MsgBox.Alert("提示", "请选择要保存的出库单！");
    }
}
//显示错误消息
function showErrMsg(rowIndex, errorMsg) {
    $("#divErrorMsg_" + rowIndex).html(errorMsg);
}
//禁用运单号输入框
function disabledTxtWaybillNo(rowIndex) {
    $("#txtWaybillNo_" + rowIndex).attr("readonly", true);
    $("#txtWaybillNo_" + rowIndex).css("background-color", "#f6f6f6");
    showErrMsg(rowIndex, "");
}
//启用运单号输入框
function enableTxtWaybillNo(rowIndex) {
    $("#txtWaybillNo_" + rowIndex).removeAttr("readonly");
    $("#txtWaybillNo_" + rowIndex).css("background-color", "#fff");
}
// 英文或数字
function isCode(str) {
    var rex = /^[A-Za-z0-9]+$/;
    var result = str.match(rex);
    if (result == null) {
        return false;
    } else {
        return true;
    }
}
//是否0-9
function isNum(num) {
    var rex = /[0-9]/;
    var result = num.match(rex);
    if (result == null) {
        return false;
    } else {
        return true;
    }
}