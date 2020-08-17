$(function () {
    $('#txtCode').focus();
    $("#btnSearch").click(function () {
        btnSearch();
    });
    $('#txtCode').bind('keypress', function (event) {
        if (event.keyCode == "13") {
            btnSearch();
        }
    });

    $('.inputextbox').live('keypress', isNumber);
  
    $("#btnSave").click(function () {
        btnSave();
    });

    $("#btnClose").click(function () {
        parent.$('#localWin').window('close');
    });
    if ($("#hdnIsEdit").val() == 1) {
        $('#setlist,#buttonbox').show();
        initTable();
    }
});

function btnSearch() {
    var code = $("#txtCode").val();
    var suppliersID = $("#hdnSuppliersID").val();
    if (code == "") {
        $.MsgBox.Alert("提示", "请输入商品编码！", function () {
            $("#txtCode").focus();
        });
        return false;
    }
    $.ajax({
        url: "/Purchase/SuppliersItem/SearchProducts",
        type: "POST",
        cache: false,
        data: { code: code, suppliersID: suppliersID },
        success: function (r) {
            var map = $.parseJSON(r);
            if (map.result == 1) {
                $('#txtCode').val('');
                $('#setlist,#infotitle,#buttonbox').show();
                $('#infotitle').html('商品名称: ' + map.Name + '<br>商品编码: ' + map.Code + '<br><span class="pl2em">单位: ' + map.Unit + '</span><span class="pl2em">重量: ' + map.Weight.toFixed(3) + 'g</span><span class="pl2em">保质期: ' + map.ShelfLife + '天</span>');
                $('#hdnProductsCode').val(map.Code);
                $('#hdnProductsID').val(map.ID);
                $('#hdnProductsNo').val(map.No);
                $('#hdnProductsName').val(map.Name);
                initTable();
            } else {
                $.MsgBox.Alert("提示", map.message, function () {
                    $('#txtCode').select();
                    $('#setlist,#infotitle,#buttonbox').hide();
                });
            }
        },
        error: function () {
            $.MsgBox.Alert("提示", "读取商品编码失败！");
        }
    });
}

//加载列表
function initTable() {
    var productsID = $('#hdnProductsID').val();
    var suppliersID = $("#hdnSuppliersID").val();
    $('#grid').datagrid({
        url: '/Purchase/SuppliersItem/SearchProductsSku?productsID=' + productsID + '&suppliersID=' + suppliersID,
        width: 650,
        height: 190,
        fitColumns: true,
        rownumbers: false,
        showFooter: true,
        checkOnSelect: false,
        selectOnCheck: false,
        columns: [[
			{ field: 'ProductsSkuID', hidden: true },
			{ field: 'ProductsSkuSaleprop', title: '属性', width: 200, align: 'center' },
			{ field: 'ProductsSkuCode', title: '商品SKU码', width: 200, align: 'center' },
			{
			    field: 'PurchasePrice', title: '采购价 <input type="checkbox" id="theSame" name="theSame" value="" /><label for="theSame">相同</label>', width: 150,
			    formatter: function (value, row) {
			        var arrivalCycle = $("#txtArrivalCycle").numberbox("getValue");
			        if (arrivalCycle == "" || row.ArrivalCycle > arrivalCycle) {
			            $("#txtArrivalCycle").numberbox("setValue", row.ArrivalCycle);
			        }
			        var html = '<input type="text" value="' + value.toFixed(3) + '" class="inputextbox" id="PurchasePrice_' + row.ProductsSkuID + '"  name="PurchasePrice" style="width:120px;"/>';
			        html = html + '<input type="hidden" id="hdnProductsSkuID_"' + row.ProductsSkuID + ' name="ProductsSkuID" value="' + row.ProductsSkuID + '" />';
			        html = html + '<input type="hidden" id="hdnProductsSkuSaleprop_"' + row.ProductsSkuID + ' name="ProductsSkuSaleprop" value="' + row.ProductsSkuSaleprop + '"/>';
			        html = html + '<input type="hidden" id="hdnProductsSkuCode_"' + row.ProductsSkuID + ' name="ProductsSkuCode" value="' + row.ProductsSkuCode + '"/>';
			        return html;
			    },
			    align: 'center'
			}
        ]],
        onLoadSuccess: function (data) {
            //SKU采购价相同
            $("#theSame").click(function () {
                samePurchasePrice(undefined);
            });
            txtChangeEvent();
            $("input[name='PurchasePrice']").each(function () {
                $(this).blur(function () {
                    var value = $(this).val();
                    if (value != "") {
                        var rex = /^[0-9]\d{0,13}(\.\d{0,3})?$/;
                        if (!rex.test(value)) {
                            $.MsgBox.Alert("提示", "采购价必须是数字 ，且最多只能有三位小数！", 1000);
                            $(this).val('');
                            $(this).focus();
                        }
                    }
                });
            });
            DataGridNoData(this);
        }
    });
}

function btnSave() {
    $('#ffpost').form('submit', {
        url: "/Purchase/SuppliersItem/Save",
        type: "POST",
        dataType: "json",
        onSubmit: function () {
            var isValid = $(this).form('validate');
            if (isValid) {
                $("input[name='PurchasePrice']").each(function () {
                    if ($(this).val() == "") {
                        $.MsgBox.Alert("提示", "采购价未输入！", 1000);
                        $(this).focus();
                        isValid = false;
                        return false;
                    }
                });
            }
            return isValid;	// 返回false终止表单提交
        },
        success: function (r) {
            var map = $.parseJSON(r);
            if (map.result == 1) {
                var isEdit = $("#hdnIsEdit").val();
                parent.$("#btnSearch").click();
                if (isEdit == 1) {
                    $("#btnClose").click();
                } else {
                    parent.$("#lblProductsCount").click();
                    $('#setlist,#infotitle,#buttonbox').hide();
                    $('#txtArrivalCycle').val('');
                    $('#txtCode').focus();
                }
            } else {
                $.MsgBox.Alert("提示", map.message);
            }
        },
        error: function () {
            $.MsgBox.Alert("提示", "保存失败！");
        }
    });
}

function txtChangeEvent() {
    $('input[id^="PurchasePrice_"]').each(function () {
        $(this).change(function () {
            samePurchasePrice($(this).val());
        });
    });
}
function samePurchasePrice(firstPurchasePrice) {
    if ($("#theSame").attr("checked")) {
        $('input[id^="PurchasePrice_"]').each(function (i) {
            if (undefined == firstPurchasePrice) {
                firstPurchasePrice = $(this).val();
            }
            $(this).val(firstPurchasePrice);
        });
    }
}
function isNumber(e) {
    if ($.browser.msie) {
        if (((event.keyCode > 47) && (event.keyCode < 58)) ||
			  (event.keyCode == 8) || (event.keyCode == 46)) {
            return true;
        } else {
            return false;
        }
    } else {
        if (((e.which > 47) && (e.which < 58)) ||
			  (e.which == 8) || (event.keyCode == 46)) {
            return true;
        } else {
            return false;
        }
    }
}