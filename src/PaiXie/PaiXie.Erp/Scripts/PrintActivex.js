var CreatedOKLodop7766 = null;

function getLodop(oOBJECT, oEMBED, divIDStr) {
    /**************************
      本函数根据浏览器类型决定采用哪个页面元素作为Lodop对象：
      IE系列、IE内核系列的浏览器采用oOBJECT，
      其它浏览器(Firefox系列、Chrome系列、Opera系列、Safari系列等)采用oEMBED,
      如果页面没有相关对象元素，则新建一个或使用上次那个,避免重复生成。
      64位浏览器指向64位的安装程序install_lodop64.exe。
    **************************/
    var strHtmInstall = "<br><font color='#FF00FF'>打印控件未安装!点击这里<a href='../../Tool/install_lodop32.exe' target='_self'>执行安装</a>,安装后请刷新页面或重新进入。</font>";
    var strHtmUpdate = "<br><font color='#FF00FF'>打印控件需要升级!点击这里<a href='../../Tool/install_lodop32.exe' target='_self'>执行升级</a>,升级后请重新进入。</font>";
    var strHtm64_Install = "<br><font color='#FF00FF'>打印控件未安装!点击这里<a href='../../Tool/install_lodop64.exe' target='_self'>执行安装</a>,安装后请刷新页面或重新进入。</font>";
    var strHtm64_Update = "<br><font color='#FF00FF'>打印控件需要升级!点击这里<a href='../../Tool/install_lodop64.exe' target='_self'>执行升级</a>,升级后请重新进入。</font>";
    var strHtmFireFox = "<br><br><font color='#FF00FF'>（注意：如曾安装过Lodop旧版附件npActiveXPLugin,请在【工具】->【附加组件】->【扩展】中先卸它）</font>";
    var strHtmChrome = "<br><br><font color='#FF00FF'>(如果此前正常，仅因浏览器升级或重安装而出问题，需重新执行以上安装）</font>";
    var LODOP;
    try {
        //=====判断浏览器类型:===============
        var isIE = (navigator.userAgent.indexOf('MSIE') >= 0) || (navigator.userAgent.indexOf('Trident') >= 0);
        var is64IE = isIE && (navigator.userAgent.indexOf('x64') >= 0);
        //=====如果页面有Lodop就直接使用，没有则新建:==========
        if (oOBJECT != undefined || oEMBED != undefined) {
            if (isIE)
                LODOP = oOBJECT;
            else
                LODOP = oEMBED;
        } else {
            if (CreatedOKLodop7766 == null) {
                LODOP = document.createElement("object");
                LODOP.setAttribute("width", 0);
                LODOP.setAttribute("height", 0);
                LODOP.setAttribute("style", "position:absolute;left:0px;top:-100px;width:0px;height:0px;");
                if (isIE) LODOP.setAttribute("classid", "clsid:2105C259-1E0C-4534-8141-A753534CB4CA");
                else LODOP.setAttribute("type", "application/x-print-lodop");
                $("#" + divIDStr).appendChild(LODOP);
                CreatedOKLodop7766 = LODOP;
            } else
                LODOP = CreatedOKLodop7766;
        };
        //=====判断Lodop插件是否安装过，没有安装或版本过低就提示下载安装:==========
        if ((LODOP == null) || (typeof (LODOP.VERSION) == "undefined")) {
            if (navigator.userAgent.indexOf('Chrome') >= 0)
                $("#" + divIDStr).html(strHtmChrome + $("#" + divIDStr).html());
            if (navigator.userAgent.indexOf('Firefox') >= 0)
                $("#" + divIDStr).html(strHtmFireFox + $("#" + divIDStr).html());
            if (is64IE) $("#" + divIDStr).html(strHtm64_Install); else
                if (isIE) $("#" + divIDStr).html(strHtmInstall); else
                    $("#" + divIDStr).html(strHtmInstall + $("#" + divIDStr).html());
            return LODOP;
        } else
            if (LODOP.VERSION < "6.2.0.2") {
                if (is64IE) $("#" + divIDStr).html(strHtm64_Update); else
                    if (isIE) $("#" + divIDStr).html(strHtmUpdate); else
                        $("#" + divIDStr).html(strHtmUpdate + $("#" + divIDStr).html());
                return LODOP;
            };
        //=====如下空白位置适合调用统一功能(如注册码、语言选择等):====	     
        LODOP.SET_LICENSES("", "394101451001069811011355115108", "", "");
        //============================================================	     
        return LODOP;
    } catch (err) {
        if (is64IE)
            $("#" + divIDStr).html(strHtm64_Install + $("#" + divIDStr).html()); else
            $("#" + divIDStr).html(strHtmInstall + $("#" + divIDStr).html());
        return LODOP;
    };
}

//初始化打印任务 0发货单 1拣货单 2快递单
function initPrintTask(LODOP, taskName, width, height, printTemplateType) {
    //新初始化一个任务
    var orient = 0;
    if (printTemplateType = 2) {
        //快递单
        LODOP.PRINT_INIT(taskName);
        orient = 1;
        fontSize = 12;
    } else {
        //发货单和拣货单
        LODOP.PRINT_INITA("0mm", "0mm", width + "mm", height + "mm", taskName);
        if (height == "0.0") {
            //自适应高度，这时height是纸张底边的空白高 5毫米
            height = "5.0";
            orient = 3;
        }
        else {
            //方向不定，由操作者自行选择或按打印机缺省设置
            orient = 0;
        }
        fontSize = 9.75;
    }
    LODOP.SET_PRINT_PAGESIZE(orient, width + "mm", height + "mm", "");
    LODOP.SET_PRINT_STYLE("FontSize", fontSize);
    //设置打印份数是1份
    LODOP.SET_PRINT_COPIES(1);
}
//根据打印模版类型获取默认打印机 快递单传快递ID，拣货单和发货单传打印模版ID 0发货单 1拣货单 2快递单
function getDefaultPrinterName(deliveryExpressID, printTemplateID, printTemplateType) {
    var printerName = '';
    $.ajax({
        url: "/Warehouse/Outbound/GetDefaultPrinterName?deliveryExpressID=" + deliveryExpressID + "&printTemplateID=" + printTemplateID + "&printTemplateType=" + printTemplateType,
        type: "GET",
        async: false,
        cache: false,
        success: function (r) {
            printerName = r;
        }
    });
    return printerName;
}
//绑定打印模版 0发货单 1拣货单
function bindPrintTemplate(LODOP, printTemplateComboboxID, printerComboboxID, printTemplateType) {
    $("#" + printTemplateComboboxID).combobox({
        url: '/Warehouse/PrintTemplate/JsonTree?printTemplateType=' + printTemplateType,
        valueField: 'Value',
        textField: 'Text',
        onLoadSuccess: function (data) { //数据加载完毕事件
            $.each(data, function (index, element) {
                if (element.IsChecked == 1) {
                    $("#" + printTemplateComboboxID).combobox('select', element.Value);
                }
            });
        },
        onChange: function (n, o) {
            //获取打印模版默认打印机，并选中
            var deliveryExpressID = 0;
            var defaultPrinterName = getDefaultPrinterName(deliveryExpressID, n, printTemplateType);
            if (defaultPrinterName != "") {
                //如果选择的打印机无效
                if (!LODOP.SET_PRINTER_INDEXA(defaultPrinterName)) {
                    
                }
            }
            $("#" + printerComboboxID).combobox("setValue", defaultPrinterName);
        }
    });
}
//绑定打印机 默认打印机未设置就传 ""
function bindPrinterName(LODOP, printerComboboxID, defaultPrinterName) {
    try {
        var data = [];
        data.push({ "text": "—请选择—", "value": "" });
        var COUNT = LODOP.GET_PRINTER_COUNT();
        for (var i = 0; i < COUNT; i++) {
            var Printer_Name = LODOP.GET_PRINTER_NAME(i);
            data.push({ "text": Printer_Name, "value": Printer_Name });
        }
        if (defaultPrinterName != "") {
            //如果选择的打印机无效
            if (!LODOP.SET_PRINTER_INDEXA(defaultPrinterName)) {
                data.push({ "text": defaultPrinterName, "value": defaultPrinterName });
            }
        }
        $("#" + printerComboboxID).combobox("loadData", data);
        $("#" + printerComboboxID).combobox("setValue", defaultPrinterName);
    } catch (e) { }
}
//根据打印模版类型获取json打印数据 0发货单 1拣货单 2快递单
function getPrintData(id, printBatchCode, deliveryExpressID, printTemplateID, printTemplateType) {
    var jsonData = null;
    var url = "/Warehouse/Outbound/GetPrintData?id=" + id + "&printBatchCode=" + printBatchCode + "&deliveryExpressID=" + deliveryExpressID + "&printTemplateID=" + printTemplateID + "&printTemplateType=" + printTemplateType;
    $.ajax({
        url: url,
        type: "GET",
        async: false,
        cache: false,
        dataType: "json",
        success: function (data) {
            jsonData = data;
        }
    });
    return jsonData;
}
//根据打印模版类型 打印或预览 type0直接打印 1预览 printTemplateType 0发货单 1拣货单 2快递单
function print(id, printBatchCode, deliveryExpressID, printTemplateID, LODOP, printerName, printTemplateType, type) {
    if (printerName == "") {
        $.MsgBox.Alert("提示", "请选择打印机！");
        return false;
    }
    var jsonData = getPrintData(id, printBatchCode, deliveryExpressID, printTemplateID, printTemplateType);
    if (jsonData != null) {
        try {
            var width = 0;
            var height = 0;            
            var printPageCount = 0;
            var groupCount = 50;
            var title = '';
            switch (printTemplateType) {
                case "0":
                    title = "发货单";
                    width = jsonData.PrintTemplateInfo.Width;
                    height = jsonData.PrintTemplateInfo.Height;
                    printPageCount = jsonData.OutboundList.length;
                    break;
                case "1":
                    title = "拣货单";
                    width = jsonData.PrintTemplateInfo.Width;
                    height = jsonData.PrintTemplateInfo.Height;
                    printPageCount = 1;
                    groupCount = 1;
                    break;
                case "2":
                    title = "快递单";
                    width = jsonData.ExpressInfo.Width;
                    height = jsonData.ExpressInfo.Height;
                    printPageCount = jsonData.OutboundList.length;
                    break;
            }
            if (title == '') return false;
            initPrintTask(LODOP, "打印_" + title + Math.random(), width, height, printTemplateType);
            var ids = [];
            for (var i = 0; i < printPageCount; i++) {
                var tempContent = ''
                switch (printTemplateType) {
                    case "0":
                        ids.push(jsonData.OutboundList[i].ID);
                        tempContent = jsonData.PrintTemplateInfo.TemplateContent.replace(/\"/g, "$quot;").replace(/\\/g, "$fxg;");
                        tempContent = prepareDeliveryTempContent(tempContent, jsonData.CurrentTime, jsonData.CurrentDate, jsonData.OutboundList[i], i + 1, jsonData.PrintTemplateInfo.SecondPageOffset);
                        break;
                    case "1":
                        ids = jsonData.outboundIDs;
                        tempContent = jsonData.PrintTemplateInfo.TemplateContent.replace(/\"/g, "$quot;").replace(/\\/g, "$fxg;");
                        tempContent = preparePickTempContent(tempContent, jsonData, jsonData.PrintTemplateInfo.SecondPageOffset)
                        break;
                    case "2":
                        ids.push(jsonData.OutboundList[i].ID);
                        tempContent = jsonData.ExpressInfo.TemplateContent.replace(/\"/g, "$quot;").replace(/\\/g, "$fxg;");
                        tempContent = prepareExpressTempContent(tempContent, jsonData.CurrentTime, jsonData.CurrentDate, jsonData.OutboundList[i], jsonData.SendInfo);
                        break;
                }
                //过滤回车和换行
                tempContent = tempContent.replace(/\r\n/g, "");
                //过滤转义反斜杠 '\'
                tempContent = tempContent.replace(/\\/g, "\\\\");
                //过滤 引号 ""，有时地址会有引号，导致JS出错
                tempContent = tempContent.replace(/\"/g, "");
                //还原tempContent的引号
                tempContent = tempContent.replace(/\$quot;/g, "\"").replace(/\$fxg;/g, "\\");
                LODOP.NewPage();
                eval(tempContent);
                var currentOrderCount = i + 1;
                var isLastOrder = currentOrderCount == printPageCount;
                if (currentOrderCount % groupCount == 0 || isLastOrder) {
                    if (!LODOP.SET_PRINTER_INDEX(printerName)) {
                        return false;
                    }
                    if (type == 0) {
                        LODOP.PRINT();
                        setPrintDate(ids.join(","), printTemplateType);
                        parent.$("#refreshCurrentPage").click();
                        ids = [];
                    } else {
                        LODOP.SET_PRINT_MODE("AUTO_CLOSE_PREWINDOW", 1);
                        var result = LODOP.PREVIEW();
                        if (result == 0) {
                            return false;
                        } else {
                            type = 0;
                            setPrintDate(ids.join(","), printTemplateType);
                            parent.$("#refreshCurrentPage").click();
                            ids = [];
                        }
                    }
                    if (i < printPageCount - 1) {
                        initPrintTask(LODOP, "打印_" + title + Math.random(), width, height, printTemplateType);
                    }
                }
            }
        } catch (e) {
            $.MsgBox.Alert("提示", "请先下载安装打印控件！");
        }
    } else {
        $.MsgBox.Alert("提示", "获取打印数据失败！");
    }
}
//将快递单模版中的标签替换成数据
function prepareExpressTempContent(tempContent, currentTime, currentDate, outbound, sendInfo) {
    tempContent = tempContent.replace(/\$当前时间/g, currentTime);
    tempContent = tempContent.replace(/\$寄件日期/g, currentDate);
    tempContent = tempContent.replace(/\$系统订单号/g, outbound.ErpOrderCode);
    tempContent = tempContent.replace(/\$外部订单号/g, nullToEmpty(outbound.OutOrderCode));
    tempContent = tempContent.replace(/\$出库单号/g, outbound.BillNo);
    tempContent = tempContent.replace(/\$收件人手机/g, nullToEmpty(outbound.BuyMtel));
    tempContent = tempContent.replace(/\$收件人电话/g, nullToEmpty(outbound.BuyTel));
    tempContent = tempContent.replace(/\$收件人姓名/g, nullToEmpty(outbound.BuyName));
    tempContent = tempContent.replace(/\$收件人地址/g, nullToEmpty(outbound.BuyAddr));
    tempContent = tempContent.replace(/\$收件人邮编/g, nullToEmpty(outbound.BuyPostCode));
    tempContent = tempContent.replace(/\$收件人地址-省/g, nullToEmpty(outbound.Province));
    tempContent = tempContent.replace(/\$收件人地址-市/g, nullToEmpty(outbound.City));
    tempContent = tempContent.replace(/\$收件人地址-区/g, nullToEmpty(outbound.District));
    tempContent = tempContent.replace(/\$寄件人电话/g, nullToEmpty(sendInfo.SendTel));
    tempContent = tempContent.replace(/\$寄件人姓名/g, nullToEmpty(sendInfo.SendPerson));
    tempContent = tempContent.replace(/\$寄件人地址/g, nullToEmpty(sendInfo.SendAddress));
    tempContent = tempContent.replace(/\$寄件人地址-省/g, nullToEmpty(sendInfo.SendProvince));
    tempContent = tempContent.replace(/\$寄件人地址-市/g, nullToEmpty(sendInfo.SendCity));
    tempContent = tempContent.replace(/\$寄件人地址-区/g, nullToEmpty(sendInfo.SendDistrict));
    tempContent = tempContent.replace(/\$买家留言/g, nullToEmpty(outbound.BuyMessage));
    tempContent = tempContent.replace(/\$卖家备注/g, nullToEmpty(outbound.SellerRemark));
    tempContent = tempContent.replace(/\$店铺名称/g, nullToEmpty(outbound.ShopName));
    tempContent = tempContent.replace(/\$买家会员名/g, nullToEmpty(outbound.BuyNickName));
    tempContent = tempContent.replace(/\$商品明细/g, nullToEmpty(outbound.ProList));
    tempContent = tempContent.replace(/\$商品数量/g, outbound.ProductsNum);
    tempContent = tempContent.replace(/\$商品金额/g, outbound.ProductsAmount);
    tempContent = tempContent.replace(/\$包裹重量/g, outbound.TotalWeight);
    if (outbound.IsCod == 1) {
        tempContent = tempContent.replace(/\$货到付款金额\(小写\)/g, outbound.UncollectedeAmount);
        tempContent = tempContent.replace(/\$货到付款金额\(大写\)/g, outbound.UncollectedeAmountChinese);
    } else {
        tempContent = tempContent.replace(/\$货到付款金额\(小写\)/g, "");
        tempContent = tempContent.replace(/\$货到付款金额\(大写\)/g, "");
    }
    return tempContent;
}
//将发货单模版中的标签替换成数据
function prepareDeliveryTempContent(tempContent, currentTime, currentDate, outbound, sortNo, secondPageOffset) {
    tempContent = tempContent.replace(/\$序号/g, sortNo);
    tempContent = tempContent.replace(/\$当前时间/g, currentTime);
    tempContent = tempContent.replace(/\$发货日期/g, currentDate);
    tempContent = tempContent.replace(/\$店铺名称/g, nullToEmpty(outbound.ShopName));
    tempContent = tempContent.replace(/\$买家会员名/g, nullToEmpty(outbound.BuyNickName));
    tempContent = tempContent.replace(/\$系统订单号/g, outbound.ErpOrderCode);
    tempContent = tempContent.replace(/\$外部订单号/g, nullToEmpty(outbound.OutOrderCode));
    tempContent = tempContent.replace(/\$出库单号/g, outbound.BillNo);
    tempContent = tempContent.replace(/\$运单号/g, nullToEmpty(outbound.WaybillNo));
    tempContent = tempContent.replace(/\$收件人手机/g, nullToEmpty(outbound.BuyMtel));
    tempContent = tempContent.replace(/\$收件人电话/g, nullToEmpty(outbound.BuyTel));
    tempContent = tempContent.replace(/\$收件人姓名/g, nullToEmpty(outbound.BuyName));
    tempContent = tempContent.replace(/\$收件人地址/g, nullToEmpty(outbound.BuyAddr));
    tempContent = tempContent.replace(/\$买家留言/g, nullToEmpty(outbound.BuyMessage));
    tempContent = tempContent.replace(/\$卖家备注/g, nullToEmpty(outbound.SellerRemark));
    tempContent = tempContent.replace(/\$发票信息/g, nullToEmpty(outbound.InvoiceInfo));
    tempContent = tempContent.replace(/\$商品明细打印区域/g, nullToEmpty(outbound.ProList));
    tempContent = tempContent.replace(/\$SecondPageOffset\$quot;\);/g, "$quot;); LODOP.SET_PRINT_STYLEA(0,$quot;TableHeightScope$quot;,1);LODOP.SET_PRINT_STYLEA(0, $quot;Top2Offset$quot;, $quot;" + secondPageOffset + "mm$quot;);");
    tempContent = tempContent.replace(/\$商品数量/g, outbound.ProductsNum);
    tempContent = tempContent.replace(/\$商品金额/g, outbound.ProductsAmount);
    tempContent = tempContent.replace(/\$商品优惠/g, outbound.OrderDiscount);
    tempContent = tempContent.replace(/\$实收运费/g, outbound.Freight);
    tempContent = tempContent.replace(/\$实收金额/g, outbound.RealAmount);
    tempContent = tempContent.replace(/\$运单号条码/g, nullToEmpty(outbound.WaybillNo));
    tempContent = tempContent.replace(/\$运单号二维码/g, nullToEmpty(outbound.WaybillNo));
    tempContent = tempContent.replace(/\$系统订单号条码/g, outbound.ErpOrderCode);
    tempContent = tempContent.replace(/\$系统订单号二维码/g, outbound.ErpOrderCode);
    tempContent = tempContent.replace(/\$外部订单号条码/g, nullToEmpty(outbound.OutOrderCode));
    tempContent = tempContent.replace(/\$外部订单号二维码/g, nullToEmpty(outbound.OutOrderCode));
    tempContent = tempContent.replace(/\$出库单号条码/g, outbound.BillNo);
    tempContent = tempContent.replace(/\$出库单号二维码/g, outbound.BillNo);
    if (outbound.IsCod == 1) {
        tempContent = tempContent.replace(/\$货到付款金额\(小写\)/g, outbound.UncollectedeAmount);
        tempContent = tempContent.replace(/\$货到付款金额\(大写\)/g, outbound.UncollectedeAmountChinese);
    } else {
        tempContent = tempContent.replace(/\$货到付款金额\(小写\)/g, "");
        tempContent = tempContent.replace(/\$货到付款金额\(大写\)/g, "");
    }
    return tempContent;
}
//将拣货单模版中的标签替换成数据
function preparePickTempContent(tempContent, jsonData, secondPageOffset) {
    tempContent = tempContent.replace(/\$当前时间/g, jsonData.CurrentTime);
    tempContent = tempContent.replace(/\$当前日期/g, jsonData.CurrentDate);
    tempContent = tempContent.replace(/\$打印批次/g, nullToEmpty(jsonData.PrintBatchCode));
    tempContent = tempContent.replace(/\$出库单数量/g, jsonData.outboundIDs.length);
    tempContent = tempContent.replace(/\$商品数量/g, jsonData.ProductsNum);
    tempContent = tempContent.replace(/\$商品明细打印区域/g, nullToEmpty(jsonData.ProList));
    tempContent = tempContent.replace(/\$SecondPageOffset\$quot;\);/g, "$quot;); LODOP.SET_PRINT_STYLEA(0,$quot;TableHeightScope$quot;,1);LODOP.SET_PRINT_STYLEA(0, $quot;Top2Offset$quot;, $quot;" + secondPageOffset + "mm$quot;);");
    return tempContent;
}
//根据打印模版类型设置打印时间 0发货单 1拣货单 2快递单
function setPrintDate(ids, printTemplateType) {
    $.ajax({
        url: "/Warehouse/Outbound/SetPrintDate?ids=" + ids + "&printTemplateType=" + printTemplateType,
        type: "GET",
        async: false,
        cache: false,
        success: function (r) {
            var map = $.parseJSON(r);
            if (map.result != 1) {
                $.MsgBox.Alert("提示", map.message);
            }
        }
    });
}
//防止打印出null或者undefined
function nullToEmpty(obj) {
    if (null == obj || undefined == obj) {
        return '';
    } else {
        return obj;
    }
}