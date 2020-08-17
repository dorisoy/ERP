$(function () {
    $('#txtCode').focus();
    $("#btnSearch").click(function () {
        btnSearch();
    });

    $('#txtCode').bind('keypress', function (e) {
        var currKey = 0, e = e || event;
        currKey = e.keyCode || e.which || e.charCode;
        if (currKey == 13) {
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

    $(":radio").change(function () {
        var rows = $("#grid").datagrid("getRows");
        for (var i = 0; i < rows.length; i++) {
            $('#grid').datagrid('refreshRow', i);
        }
    });

    $("#hdnErpOrderCode").val(parent.$("#hdnErpOrderCode").val());
});

function btnSearch() {
    var code = $("#txtCode").val();
    if (code == "") {
        $("#msg").text("请输入商品编码！");
        $("#txtCode").focus();
        return false;
    }
    $.ajax({
        url: "/Order/AddOrder/SearchProducts",
        type: "POST",
        cache: false,
        data: { code: code },
        success: function (r) {
            $("#msg").text("");
            var map = $.parseJSON(r);
            if (map.result == 1) {
                $('#txtCode').val('');
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
            $("#msg").text("查找商品编码失败！");
        }
    });
}

//加载列表
function initTable() {
    $('#grid').datagrid({
        url: '/Order/AddOrder/SearchProductsSku?productsID=' + $('#hdnProductsID').val() + '&erpOrderCode=' + $("#hdnErpOrderCode").val() + '&ram=' + Math.random(),
        //height: $(document).height() * 0.8,
        width: function () { return document.body.clientWidth * 0.9 },
        fitColumns: true,
        rownumbers: false,
        //showFooter: true,
        checkOnSelect: false,
        selectOnCheck: false,
        columns: [[
			{ field: 'ProductsSkuID', hidden: true },
			{ field: 'ProductsSkuSaleprop', title: '属性', width: 80, align: 'center' },
			{ field: 'ProductsSkuCode', title: '商品SKU码', width: 80, align: 'center' },
			{ field: 'KyNum', title: '可下单数量', width: 80, align: 'center' },
            {
                field: 'SellingPrice', title: '销售价', width: 80,
                formatter: function (value, row) {
                    if ($('input[name="AddType"]:checked ').val() == "0")
                        return "<span class='red'>￥</span>" + value.toFixed(3);
                    else {
                        return "<span class='red'>￥</span>0.000";
                    }
                },
                align: 'center'
            },
            { field: 'ProductsNum', title: '已下单', width: 80, align: 'center' },
			{
			    field: 'Num', title: '数量', width: 80,
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
        url: "/Order/AddOrder/SaveProducts",
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
                    $.MsgBox.Alert("提示", "请输入商品数量！");
                    isValid = false;
                }
            }
            return isValid;	// 返回false终止表单提交
        },
        success: function (r) {
            var map = $.parseJSON(r);
            if (map.result == 1) {
                $.MsgBox.Alert("提示", "添加成功！", 1000);
                $('#setlist,#infotitle,#buttonbox').hide();
                $('#txtCode').focus();
                $("#hdnErpOrderCode").val(map.erpOrderCode);
                $("#hdnID").val(map.id);
                if (parent.$("#hdnIsOrderDetails").val() != "1") {
                    parent.$("#hdnID").val(map.id);
                    parent.$("#hdnErpOrderCode").val(map.erpOrderCode);
                    parent.initTable();
                }
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
