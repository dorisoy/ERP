//根据淘宝接口提供的快递规则，判断运单号是否正确
function chkWaybillNoRules(logisticsCode, waybillNo) {
    logisticsCode = logisticsCode.toUpperCase();
    var rex = "";
    switch (logisticsCode) {
        case "EMS":
            rex = /^[A-Z]{2}[0-9]{9}[A-Z]{2}$|^(10|11)[0-9]{11}$|^(50|51)[0-9]{11}$/;
            break;
        case "EYB":
            rex = /^[A-Z]{2}[0-9]{9}[A-Z]{2}$|^(10|11)[0-9]{11}$|^(50|51)[0-9]{11}$/;
            break;
        case "EMSBZ":
            rex = /^[A-Z]{2}[0-9]{9}[A-Z]{2}$|^(10|11)[0-9]{11}$|^(50|51)[0-9]{11}$/;
            break;
        case "EMSJJ":
            rex = /^[A-Z]{2}[0-9]{9}[A-Z]{2}$|^(10|11)[0-9]{11}$|^(50|51)[0-9]{11}$/;
            break;
        case "POSTB"://邮政小包
            rex = /^[GA]{2}[0-9]{9}([2-5][0-9]|[1][1-9]|[6][0-5])$|^[99]{2}[0-9]{11}$/;
            break;
        case "STO":
            rex = /^(268|888|588|688|368|468|568|668|768|868|968)[0-9]{9}$|^(11|22|33|268|888|588|688|368|468|568|668|768|868|968)[0-9]{10}$|^(STO)[0-9]{10}$|^(33)[0-9]{11}$|^(44)[0-9]{11}$|^(55)[0-9]{11}$|^(66)[0-9]{11}$|^(77)[0-9]{11}$|^(88)[0-9]{11}$|^(99)[0-9]{11}$/;
            break;
        case "YTO":
            rex = /^[A-Za-z0-9]{2}[0-9]{10}$|^[A-Za-z0-9]{2}[0-9]{8}$|^(8800)[0-9]{14}$/;
            break;
        case "ZTO":
            rex = /^((751|358|618|680|778|768|688|689|618|828|988|118|888|571|518|010|628|205|880|717|718|728|738|761|762|763|701|757)[0-9]{9})$|^((2008|2010|8050|7518)[0-9]{8})$|^((36)[0-9]{10})$/;
            break;
        case "HTKY":
            rex = /^(A|B|C|D|E|H|0)(D|X|[0-9])(A|[0-9])[0-9]{10}$|^(21|22|23|24|25|26|27|28|29|30|31|32|33|34|35|37|38|39|61)[0-9]{10}$|^(369|639|649|630|360)[0-9]{9}$|^(50|51)[0-9]{12}$|^(7)[0-9]{13}$/;
            break;
        case "SF":
            rex = /^[0-9]{12}$/;
            break;
        case "ZJS":
            rex = /^[a-zA-Z0-9]{10}$|^(42)[0-9]{8}$/;
            break;
        case "YUNDA":
            rex = /^(10|11|12|13|14|15|16|17|19|18|50|55|58|80|88|66|31)[0-9]{11}$|^[0-9]{13}$/;
            break;
        case "TTKDEX":
            rex = /^[0-9]{12}$/;
            break;
        case "CRE":
            rex = /^K[0-9]{13}$|^K[0-9]{11}$/;
            break;
        case "DBL":
            rex = /^[0-9]{8,10}$/;
            break;
        case "QFKD":
            rex = /^[0-9]{12}$|^[0-9]{15}$/;
            break;
        case "FAST":
            rex = /^[0-9]{11,13}$|^(P330[0-9]{8})$|^(D[0-9]{11})$|^(319)[0-9]{11}$/;
            break;
        case "GTO":
            rex = /^(2|3|4|8|5|6|7)[0-9]{9}$/;
            break;
        case "UC":
            rex = /^VIP[0-9]{9}|V[0-9]{11}|[0-9]{12}$|^LBX[0-9]{15}-[2-9AZ]{1}-[1-9A-Z]{1}$|^(9001)[0-9]{8}$/;
            break;
    }
    if (rex == "") {
        return true;
    } else {
        return getValidateResult(waybillNo, rex);
    }
}
//根据运单号和正则表达式获取验证结果
function getValidateResult(waybillNo, rex) {
    var result = waybillNo.match(rex);
    if (result == null) {
        return false;
    } else {
        return true;
    }
}
/**
* 获取运单号验证码
* newCenterNumber 累加后的数字
* logisticsCode   物流代码 字母要大写
* preWaybillNo    上一个运单号(完整的，针对顺丰、宅急送有用)
*/
function getWaybillNoCode(newCenterNumber, logisticsCode, preWaybillNo) {
    newCenterNumber = newCenterNumber.toString();
    var newCenterNumberLength = newCenterNumber.length;
    var preWaybillNoLength = preWaybillNo.length;
    var waybillNoCode = "";
    if (logisticsCode == "EMS" || logisticsCode == "EYB" || logisticsCode == "EMSBZ" || logisticsCode == "EMSJJ") {
        //EMS必须是8位数字进行校验，包括最开始的0，长度不足有可能是开始位置的0被截取了
        for (var i = 0; i < 8 - newCenterNumberLength; i++) {
            newCenterNumber = "0" + newCenterNumber;
        }
        var num1 = Mid(newCenterNumber, 0, 1);
        var num2 = Mid(newCenterNumber, 1, 1);
        var num3 = Mid(newCenterNumber, 2, 1);
        var num4 = Mid(newCenterNumber, 3, 1);
        var num5 = Mid(newCenterNumber, 4, 1);
        var num6 = Mid(newCenterNumber, 5, 1);
        var num7 = Mid(newCenterNumber, 6, 1);
        var num8 = Mid(newCenterNumber, 7, 1);

        //EMS规则
        waybillNoCode = (8 * num1 + 6 * num2 + 4 * num3 + 2 * num4 + 3 * num5 + 5 * num6 + 9 * num7 + 7 * num8);
        waybillNoCode = 11 - (waybillNoCode) % (11);
        if (waybillNoCode == 10)
            waybillNoCode = 0;
        if (waybillNoCode == 11)
            waybillNoCode = 5;
    }

    if (logisticsCode == "SF") {
        //保证长度12
        for (var i = 0; i < 12 - preWaybillNoLength; i++) {
            preWaybillNo = "0" + preWaybillNo;
        }
        var num1 = Mid(preWaybillNo, 0, 1);
        var num2 = Mid(preWaybillNo, 1, 1);
        var num3 = Mid(preWaybillNo, 2, 1);
        var num4 = Mid(preWaybillNo, 3, 1);
        var num5 = Mid(preWaybillNo, 4, 1);
        var num6 = Mid(preWaybillNo, 5, 1);
        var num7 = Mid(preWaybillNo, 6, 1);
        var num8 = Mid(preWaybillNo, 7, 1);
        var num9 = Mid(preWaybillNo, 8, 1);
        var num10 = Mid(preWaybillNo, 9, 1);
        var num11 = Mid(preWaybillNo, 10, 1);
        var num12 = Mid(preWaybillNo, 11, 1);

        //保证长度11
        for (var i = 0; i < 11 - newCenterNumberLength; i++) {
            newCenterNumber = "0" + newCenterNumber;
        }
        var Nnum1 = Mid(newCenterNumber, 0, 1);
        var Nnum2 = Mid(newCenterNumber, 1, 1);
        var Nnum3 = Mid(newCenterNumber, 2, 1);
        var Nnum4 = Mid(newCenterNumber, 3, 1);
        var Nnum5 = Mid(newCenterNumber, 4, 1);
        var Nnum6 = Mid(newCenterNumber, 5, 1);
        var Nnum7 = Mid(newCenterNumber, 6, 1);
        var Nnum8 = Mid(newCenterNumber, 7, 1);
        var Nnum9 = Mid(newCenterNumber, 8, 1);
        var Nnum10 = Mid(newCenterNumber, 9, 1);
        var Nnum11 = Mid(newCenterNumber, 10, 1);
        var Nnum12 = 0;

        //顺丰规则
        if (Nnum9 - num9 == 1 && num9 % 2 == 1) {
            if (num12 - 8 >= 0) {
                Nnum12 = num12 - 8;
            } else {
                Nnum12 = num12 - 8 + 10;
            }
        } else if (Nnum9 - num9 == 1 && num9 % 2 == 0) {
            if (num12 - 7 >= 0) {
                Nnum12 = num12 - 7;
            } else {
                Nnum12 = num12 - 7 + 10;
            }
        } else {
            if ((num10 == 3 || num10 == 6) && num11 == 9) {
                if (num12 - 5 >= 0) {
                    Nnum12 = num12 - 5;
                } else {
                    Nnum12 = num12 - 5 + 10;
                }
            } else if (num11 == 9) {
                if (num12 - 4 >= 0) {
                    Nnum12 = num12 - 4;
                } else {
                    Nnum12 = num12 - 4 + 10;
                }
            } else {
                if (num12 - 1 >= 0) {
                    Nnum12 = num12 - 1;
                } else {
                    Nnum12 = num12 - 1 + 10;
                }
            }
        }
        waybillNoCode = Nnum12;
    }
    return waybillNoCode.toString();
}