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
});

function btnSearch() {
    var code = $("#txtCode").val();
    var warehouseCode = $("#hdnWarehouseCode").val();
    var suppliersID = $("#hdnSuppliersID").val();
    if (code == "") {
        $.MsgBox.Alert("提示", "请输入商品编码！", 1000);
        $("#txtCode").focus();
        return false;
    }
    $.ajax({
        url: "/Purchase/PurchaseItem/SearchProducts",
        type: "POST",
        cache: false,
        data: { warehouseCode: warehouseCode, code: code, suppliersID: suppliersID },
        success: function (r) {
            var map = $.parseJSON(r);
            if (map.result == 1) {
                $('#txtCode').val('');
                $('#setlist,#infotitle,#buttonbox').show();
                $('#infotitle').text('商品名称：' + map.Name);
                $('#hdnProductsCode').val(map.Code);
                $('#hdnProductsID').val(map.ID);
                $('#hdnProductsNo').val(map.No);
                $('#hdnProductsName').val(map.Name);
                initTable();
            } else {
                $.MsgBox.Alert("提示", map.message);
                $('#setlist,#infotitle,#buttonbox').hide();
            }
        },
        error: function () {
            $.MsgBox.Alert("提示", "读取商品编码失败！");
        }
    });
}

//加载列表
function initTable() {
    var warehouseCode = $("#hdnWarehouseCode").val();
    var productsID = $('#hdnProductsID').val();
    var suppliersID = $("#hdnSuppliersID").val();
    $('#grid').datagrid({
        url: '/Purchase/PurchaseItem/SearchProductsSku?warehouseCode=' + warehouseCode + '&productsID=' + productsID + "&suppliersID=" + suppliersID,
        width: 650,
        height: 250,
        fitColumns: true,
        rownumbers: false,
        showFooter: true,
        checkOnSelect: false,
        selectOnCheck: false,
        columns: [[
			{ field: 'ProductsSkuID', hidden: true },
			{ field: 'ProductsSkuSaleprop', title: '属性', width: 80, align: 'center' },
			{ field: 'ProductsSkuCode', title: '商品SKU码', width: 80, align: 'center' },
			{ field: 'KyNum', title: '当前库存', width: 80, align: 'center' },
            {
                field: 'Purchase', title: '采购价', width: 80, align: 'center',
                formatter: function (value, row) {
                    var html = "";
                    if (row.PurchasePrice != null) {
                        html = "<span style='color:#008c23;'>" + row.PurchasePrice.toFixed(3) + "</span>";
                    } else {
                        html = "<font style='color:#ff0000;'>未关联供应商</font>";
                    }
                    return html;
                }
            },
			{
			    field: 'Num', title: '采购数量', width: 80,
			    formatter: function (value, row) {
			        var html = '';
			        if (row.PurchasePrice != null) {
			            html = '<input type="text" value="" class="inputextbox" id="Num_' + row.ProductsSkuID + '"  name="Num" maxlength="7" />';
			        } else {
			            html = '<input type="text" value="" class="inputextbox" id="Num_' + row.ProductsSkuID + '"  name="Num" disabled />';
			            html = html + '<input type="hidden" id="Num_' + row.ProductsSkuID + '"  name="Num" value="0" />';
			        }
			        html = html + '<input type="hidden" id="hdnProductsSkuID_"' + row.ProductsSkuID + ' name="ProductsSkuID" value="' + row.ProductsSkuID + '" />';
			        html = html + '<input type="hidden" id="hdnProductsSkuSaleprop_"' + row.ProductsSkuID + ' name="ProductsSkuSaleprop" value="' + row.ProductsSkuSaleprop + '"/>';
			        html = html + '<input type="hidden" id="hdnProductsSkuCode_"' + row.ProductsSkuID + ' name="ProductsSkuCode" value="' + row.ProductsSkuCode + '"/>';
			        return html;
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
        url: "/Purchase/PurchaseItem/Save?warehouseCode=" + $("#hdnWarehouseCode").val(),
        type: "POST",
        dataType: "json",
        onSubmit: function () {
            var isValid = $(this).form('validate');
            if (isValid) {
                var totalNum = 0;
                $("input[name='Num']").each(function () {
                    if ($(this).val() != "") {
                        if ($.isNumeric($(this).val())) {
                            totalNum = totalNum + Number($(this).val());
                        } else {
                            $.MsgBox.Alert("提示", "采购数量必须是数字！", 1000);
                            $(this).focus();
                            return false;
                        }
                    }
                });
                if (totalNum == 0) {
                    $.MsgBox.Alert("提示", "请输入采购数量！");
                    isValid = false;
                }
            }
            return isValid;	// 返回false终止表单提交
        },
        success: function (r) {
            var map = $.parseJSON(r);
            if (map.result == 1) {
                parent.$("#lblNum").click();
                parent.$("#btnSearch").click();
                $('#setlist,#infotitle,#buttonbox').hide();
                $('#txtCode').focus();
            }
            else {
                $.MsgBox.Alert("提示", map.message);
            }
        },
        error: function () {
            $.MsgBox.Alert("提示", "添加失败！");
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