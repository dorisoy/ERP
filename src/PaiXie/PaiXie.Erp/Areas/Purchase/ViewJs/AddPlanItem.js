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
    if (code == "") {
        $.MsgBox.Alert("提示", "请输入商品编码！", 1000);
        $("#txtCode").focus();
        return false;
    }
    $.ajax({
        url: "/Purchase/PlanItem/SearchProducts",
        type: "POST",
        cache: false,
        data: { warehouseCode: warehouseCode, code: code },
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
    $('#grid').datagrid({
        url: '/Purchase/PlanItem/SearchProductsSku?warehouseCode=' + $("#hdnWarehouseCode").val() + '&productsID=' + $('#hdnProductsID').val(),
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
			    field: 'Num', title: '计划采购数量', width: 80,
			    formatter: function (value, row) {
			        var html = '';
			        if (row.SuppliersItemInfoList.length > 0) {
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
			},
			{
			    field: 'SuppliersItemInfoList', title: '供应商 <input type="checkbox" id="theSame"><label for="theSame">相同</label>', width: 160,
			    formatter: function (value, row, index) {
			        var suppliersID = 0;
			        var html = '<div align="left">';
			        if (value.length > 0) {
			            $.each(value, function (index, item) {
			                html = html + '<input type="radio" id="Suppliers_' + row.ProductsSkuID + item.SuppliersID + '" value="' + item.SuppliersID + '" name="SuppliersID_' + row.ProductsSkuID + '"';
			                if (index == 0) {
			                    html = html + 'checked="checked"';
			                    suppliersID = item.SuppliersID;
			                }
			                html = html + ' /><label for="Suppliers_' + row.ProductsSkuID + item.SuppliersID + '">' + item.AliasName + '(￥' + item.PurchasePrice.toFixed(3) + '  ' + item.ArrivalCycle + '天)</label><br />';
			            });
			        } else {
			            html = html + "&nbsp;&nbsp;<font style='color:#ff0000;'>未关联供应商</font>";
			        }
			        html = html + '<input type="hidden" id="SuppliersID_' + row.ProductsSkuID + '"  name="SuppliersID" value="' + suppliersID + '" />';
			        html = html + "</div>";
			        return html;
			    },
			    align: 'center'
			}
        ]],
        onLoadSuccess: function (data) {
            //供应商相同
            $("#theSame").click(function () {
                sameSuppliers(undefined);
            });
            $("input[name^='SuppliersID_']").each(function () {
                $(this).click(function () {
                    setSuppliersValue($(this).attr("name"), $(this).val());
                    sameSuppliers($(this).val());
                });
            });
            DataGridNoData(this);
        }
    });
}
function sameSuppliers(firstSuppliers) {
    if ($("#theSame").attr("checked")) {
        $('input[id^="Suppliers_"]').each(function (i) {
            if ($(this).attr("checked")) {
                if (undefined == firstSuppliers) {
                    firstSuppliers = $(this).val();
                }
                $('input[id^="Suppliers_"][value="' + firstSuppliers + '"]').each(function () {
                    $(this).attr("checked", true);
                    setSuppliersValue($(this).attr("name"), firstSuppliers);
                });
            }
        });
    }
}
function setSuppliersValue(id, value) {
    $("#" + id).val(value);
}

function btnSave() {
    $('#ffpost').form('submit', {
        url: "/Purchase/PlanItem/Save?warehouseCode=" + $("#hdnWarehouseCode").val(),
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
                            $.MsgBox.Alert("提示", "计划采购数量必须是数字！", 1000);
                            $(this).focus();
                            return false;
                        }
                    }
                });
                if (totalNum == 0) {
                    $.MsgBox.Alert("提示", "请输入计划采购数量！");
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
