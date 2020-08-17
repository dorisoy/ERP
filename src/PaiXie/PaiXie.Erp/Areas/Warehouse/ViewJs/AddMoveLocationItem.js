//初始化
$(function () {
    $('#txtCode').focus();
    //移出库区
    $('#ReservoirArea').combobox({
        url: '/Warehouse/Location/GetDictTopLocation?ram=' + Math.random(),
        valueField: 'Value',
        textField: 'Text',
        onLoadSuccess: function () { //数据加载完毕事件
            $("#ReservoirArea").combobox('select', "0");
        },
        onChange: function (n, o) {
            var topLocationID = $("#ReservoirArea").combobox('getValue');
            bindOutLocation(topLocationID);
        }
    });

    //移入库区
    $('#InReservoirArea').combobox({
        url: '/Warehouse/Location/GetDictTopLocation?ram=' + Math.random(),
        valueField: 'Value',
        textField: 'Text',
        onLoadSuccess: function () { //数据加载完毕事件
            $("#InReservoirArea").combobox('select', "0");
        },
        onChange: function (n, o) {
            var topLocationID = $("#InReservoirArea").combobox('getValue');
            bindInLocation(topLocationID);
        }
    });
    $("#btnSearch").click(function () {
        btnSearch();
    });
    $('#txtCode').bind('keypress', function (event) {
        if (event.keyCode == "13") {
            btnSearch();
        }
    });

    $('.numbox').live('keypress', isNumber);

    $("#btnSave").click(function () {
        btnSave();
    });

    $("#btnCancel").click(function () {
        parent.$('#localWin').window('close');
    });
});
//绑定移出库位
function bindOutLocation(topLocationID) {
    $('#Location').combobox({
        url: '/Warehouse/Location/GetDictLocation?topLocationID=' + topLocationID + '&ram=' + Math.random(),
        valueField: 'Value',
        textField: 'Text',
        onLoadSuccess: function (n, o) { //数据加载完毕事件
            $("#Location").combobox('select', "0");
        },
        onChange: function () {
            initTable();
        }
    });
}
//绑定移入库位
function bindInLocation(topLocationID) {
    $('#InLocation').combobox({
        url: '/Warehouse/Location/GetDictLocation?topLocationID=' + topLocationID + '&ram=' + Math.random(),
        valueField: 'Value',
        textField: 'Text',
        onLoadSuccess: function (n, o) { //数据加载完毕事件
            $("#InLocation").combobox('select', "0");
            $("#hdnInLocationCode").val('');
        },
        onChange: function (n, o) {
            var locationCode = $('#InLocation').combobox("getText");
            if (locationCode == '请选择库位') {
                locationCode = '';
            }
            $("#hdnInLocationCode").val(locationCode);
        }
    });
}

function btnSearch() {
    var productsSkuCode = $("#txtCode").val();
    if (productsSkuCode == "") {
        $.MsgBox.Alert("提示", "请输入商品SKU码！", 1000);
        $("#txtCode").focus();
        return false;
    }
    $.ajax({
        url: "/Warehouse/MoveLocationItem/SearchProductsSku",
        type: "POST",
        cache: false,
        data: { productsSkuCode: productsSkuCode },
        success: function (r) {
            var map = $.parseJSON(r);
            if (map.result == 1) {
                $('#txtCode').val('');
                $('#setlist,#infotitle,#infoshow,#InLocationshow,#buttonbox').show();
                $('#infotitle').text('商品名称：' + map.Name);
                $('#infoshow li:eq(0)').text('商品属性：' + map.Saleprop);
                $('#hdnProductsCode').val(map.Code);
                $('#hdnProductsID').val(map.ID);
                $('#hdnProductsNo').val(map.No);
                $('#hdnProductsName').val(map.Name);
                $('#hdnProductsSkuID').val(map.ProductsSkuID);
                $('#hdnProductsSkuCode').val(productsSkuCode);
                $('#hdnProductsSkuSaleprop').val(map.Saleprop);
                initTable();
            } else {
                $.MsgBox.Alert("提示", map.message);
                $('#setlist,#infotitle,#infoshow,#InLocationshow,#buttonbox').hide();
            }
        },
        error: function () {
            $.MsgBox.Alert("提示", "读取商品SKU码失败！", 1000);
        }
    });
}
//加载列表
function initTable() {
    var topLocationID = $("#ReservoirArea").combobox('getValue');
    var locationID = $("#Location").combobox('getValue');
    if (locationID == "请选择库位") {
        locationID = 0;
    }
    var productsSkuID = $('#hdnProductsSkuID').val();
    $('#grid').datagrid({
        url: '/Warehouse/MoveLocationItem/SearchLocationProducts?productsSkuID=' + productsSkuID + '&topLocationID=' + topLocationID + '&locationID=' + locationID + '&ram=' + Math.random(),
        width: 650,
        height: 140,
        fitColumns: true,
        rownumbers: false,
        showFooter: true,
        columns: [[
			{ field: 'ID', hidden: true },
            {
                field: 'LocationCode', title: '库位编码', width: 100,
                formatter: function (value, row) {
                    var html = value;
                    html += '<input type="hidden" value="' + row.LocationID + '" id="hdnOutLocationID_' + row.ID + '"  name="OutLocationID"/>';
                    html += '<input type="hidden" value="' + value + '" id="hdnOutLocationCode_' + row.ID + '"  name="OutLocationCode"/>';
                    return html;
                },
                align: 'center'
            },
            {
                field: 'ProductsBatchCode', title: '商品批次', width: 155,
                formatter: function (value, row) {
                    var html = value;
                    html += '<input type="hidden" value="' + row.ProductsBatchID + '" id="hdnProductsBatchID_' + row.ID + '"  name="ProductsBatchID"/>';
                    html += '<input type="hidden" value="' + value + '" id="hdnProductsBatchCode_' + row.ID + '"  name="ProductsBatchCode"/>';
                    return html;
                },
                align: 'center'
            },
			{
			    field: 'KyNum', title: '当前库存', width: 80,
			    formatter: function (value, row, index) {
			        var html = value + '<input type="hidden" value="' + value + '" id="KyNum' + index + '" name="KyNum"/>';
			        return html;
			    },
			    align: 'center'
			},
			{
			    field: 'MoveNum', title: '移出数量', width: 80,
			    formatter: function (value, row) {
			        return '<input type="text" value="" class="inputextbox numbox" id="MoveNum_' + row.ID + '"  name="MoveNum"/>';
			    },
			    align: 'center'
			}
        ]],
        onLoadSuccess: function (data) {
            DataGridNoData(this);
        }
    });
}
function btnSave() {
    $('#ffpost').form('submit', {
        url: "/Warehouse/MoveLocationItem/Save",
        type: "POST",
        dataType: "json",
        onSubmit: function () {
            var isValid = $(this).form('validate');
            var totalNum = 0;
            var kyNums = $("input[name='KyNum']");
            $("input[name='MoveNum']").each(function (index, element) {
                var currMoveNum = $(element).val();
                if (currMoveNum != "") {
                    if ($.isNumeric(currMoveNum)) {
                        if (Number(currMoveNum) > 0) {
                            if (Number(currMoveNum) <= Number($(kyNums[index]).val())) {
                                totalNum = totalNum + Number(currMoveNum);
                            } else {
                                $.MsgBox.Alert("提示", "移出数量不能大于当前库存！", 1000);
                                $(element).focus();
                                isValid = false;
                                return false;
                            }
                        } else {
                            $.MsgBox.Alert("提示", "移出数量必须大于0！", 1000);
                            $(element).focus();
                            isValid = false;
                            return false;
                        }
                    } else {
                        $.MsgBox.Alert("提示", "移出数量必须是数字！", 1000);
                        $(element).focus();
                        isValid = false;
                        return false;
                    }
                }
            });
            if (totalNum == 0 && isValid) {
                $.MsgBox.Alert("提示", "请输入移出数量！");
                isValid = false;
            }
            if (isValid) {
                var locationID = $('#InLocation').combobox("getValue");
                if (locationID == '0' || locationID == '' || locationID == '请选择库位') {
                    $.MsgBox.Alert("提示", "请选择移入库位！");
                    isValid = false;
                }
            }
            return isValid;	// 返回false终止表单提交
        },
        success: function (r) {
            var map = $.parseJSON(r);
            if (map.result == 1) {
                parent.$("#lblMoveNum").click();
                parent.$("#btnSearch").click();
                $('#setlist,#infotitle,#infoshow,#InLocationshow,#buttonbox').hide();
                $('#txtCode').focus();
            } else {
                $.MsgBox.Alert("提示", map.message);
            }   
        },
        error: function () {
            $.MsgBox.Alert("提示", "保存失败！");
        }
    });
}
function isNumber(e) {
    if ($.browser.msie) {
        if (((event.keyCode > 47) && (event.keyCode < 58)) ||
			  (event.keyCode == 8)) {
            return true;
        } else {
            return false;
        }
    } else {
        if (((e.which > 47) && (e.which < 58)) ||
			  (e.which == 8)) {
            return true;
        } else {
            return false;
        }
    }
}
