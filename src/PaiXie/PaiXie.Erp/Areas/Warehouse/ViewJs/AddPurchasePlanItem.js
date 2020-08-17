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
    if (code == "") {
        $.MsgBox.Alert("提示", "请输入商品编码！", 1000);
        $("#txtCode").focus();
        return false;
    }
    $.ajax({
        url: "/Warehouse/PurchasePlanItem/SearchProducts",
        type: "POST",
        cache: false,
        data: { code: code },
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
        url: '/Warehouse/PurchasePlanItem/SearchProductsSku?productsID=' + $('#hdnProductsID').val() + '&ram=' + Math.random(),
        height: $(document).height() * 0.8,
        width: function () { return document.body.clientWidth * 0.9 },
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
			        html = '<input type="text" value="" class="inputextbox" id="Num_' + row.ProductsSkuID + '"  name="Num" maxlength="7" />';
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
        url: "/Warehouse/PurchasePlanItem/Save",
        type: "POST",
        dataType: "json",
        onSubmit: function () {
            var isValid = $(this).form('validate');
            if (isValid) {
                var totalNum = 0;
                $("input[name='Num']").each(function () {
                    if ($(this).val() != "") {
                        totalNum = totalNum + Number($(this).val());
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
