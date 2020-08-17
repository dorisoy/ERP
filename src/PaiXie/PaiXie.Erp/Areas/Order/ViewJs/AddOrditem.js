$(function () {
    $('#txtProductsSkuCode').focus();

    $("#btnSearch").click(function () {
        btnSearch();
    });

    $('#txtProductsCode,#txtProductsSkuCode').bind('keypress', function (event) {
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

    if ($("#txtProductsSkuCode").val() != "") {
        $("#btnSearch").click();
    }
});

function btnSearch() {
    var productsCode = $("#txtProductsCode").val();
    var productsSkuCode = $("#txtProductsSkuCode").val();
    if (productsCode == "" && productsSkuCode == "") {
        if (productsCode == "") {
            $("#txtProductsCode").focus();
        }
        else {
            $("#txtCode").focus();
        }
        $("#msg").text("请输入商品编码或商品SKU码！");
        return false;
    }
    $.ajax({
        url: "/Order/Download/SearchProducts",
        type: "POST",
        cache: false,
        data: { productsCode: productsCode, productsSkuCode: productsSkuCode },
        success: function (r) {
            $("#msg").text("");
            var map = $.parseJSON(r);
            if (map.result == 1) {
                $('#setlist,#infotitle,#buttonbox').show();
                $('#infotitle').text('商品名称：' + map.productsName);
                $('#hdnProductsID').val(map.productsID);
                initTable();
            } else {
                $("#msg").text(map.message);
                $('#setlist,#infotitle,#buttonbox').hide();
            }
        },
        error: function () {
            $("#msg").text("查找商品失败！");
        }
    });
}

//加载列表
function initTable() {
    $('#grid').datagrid({
        url: '/Order/Download/SearchProductsSku?productsID=' + $('#hdnProductsID').val() + '&productsSkuCode=' + $('#txtProductsSkuCode').val() + '&ram=' + Math.random(),
        height: $(document).height() * 0.8,
        width: 600,
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
			    field: 'Num', title: '数量', width: 80,
			    formatter: function (value, row) {
			        var html = '';
			        html = '<input type="text" value="" class="inputextbox" id="Num_' + row.ProductsSkuID + '"  name="Num" maxlength="7" />';
			        html = html + '<input type="hidden" id="hdnProductsSkuID_"' + row.ProductsSkuID + ' name="ProductsSkuID" value="' + row.ProductsSkuID + '" />';
			        html = html + '<input type="hidden" id="hdnProductsSkuCode_"' + row.ProductsSkuID + ' name="ProductsSkuCode" value="' + row.ProductsSkuCode + '"/>';
			        return html;
			    },
			    align: 'center'
			}
        ]]
    });
}

function btnSave() {
    $('#ffpost').form('submit', {
        url: "/Order/Download/SaveProducts",
        type: "POST",
        dataType: "json",
        onSubmit: function () {
            var isValid = $(this).form('validate');
            if (isValid) {
                var totalNum = 0;
                var count = 0;
                $("input[name='Num']").each(function () {
                    if ($(this).val() != "") {
                        totalNum = totalNum + Number($(this).val());
                        count++;
                    }
                });
                if (totalNum == 0) {
                    $.MsgBox.Alert("提示", "请输入商品数量！");
                    isValid = false;
                }
                if (count > 1) {
                    $.MsgBox.Alert("提示", "只能添加一个SKU信息！");
                    isValid = false;
                }
            }
            return isValid;
        },
        success: function (r) {
            var map = $.parseJSON(r);
            if (map.result == 1) {
                parent.$('#localWin').window('close');
                parent.initTable();
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
