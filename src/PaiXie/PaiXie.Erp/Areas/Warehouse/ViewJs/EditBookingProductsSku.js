$(function () {
    if ($("#hdnProductsID").val() != "0") {
        $("#formnum").show();
        initTable({ productsID: $("#hdnProductsID").val() });
    }

    $('.presalenum').live('keyup', function () {
        $(this).val($(this).val().replace(/\D/g, ''));

        var num = 0;
        $('.presalenum').each(function (index, element) {
            num = num + Number($(element).val());
        });
        $('#presalenum').text(num);
        $('#tspanum').text(num);
    });

    //搜索商品
    $("#btnSearch").click(function () {
        var flag = true;
        var ProductsCode = $("#txtProductsCode").val();
        $.ajax({
            url: "/Warehouse/BookingProductsSku/CheackProductsCode?productsCode=" + ProductsCode,
            type: "GET",
            cache: false,
            async: false,
            success: function (r) {
                var map = $.parseJSON(r);
                if (map.result == 0) {
                    $.MsgBox.Alert("提示", map.message);
                    flag = false;
                }
            },
            error: function () {
                $.MsgBox.Alert("提示", "搜索失败！");
            }
        });

        if (flag) {
            location.href = "/Warehouse/BookingProductsSku/Add?productsCode=" + $("#txtProductsCode").val();
        }
    });

    //添加预售
    $("#btnSave").click(function () {
        $('#formnum').form('submit', {
            url: "/Warehouse/BookingProductsSku/AddSave",
            type: "POST",
            dataType: "json",
            onSubmit: function () {
                var isValid = $(this).form('validate');
                if (Number($('#presalenum').text()) == 0)
                {
                    $.MsgBox.Alert("提示", "预售数量不能为0！");
                    isValid = false;
                }
                return isValid;
            },
            success: function (r) {
                var map = $.parseJSON(r);
                if (map.result == 1) {
                    $.MsgBox.Alert("提示", map.message, 1000);
                    setTimeout(function t() { location.href = "/Warehouse/BookingProductsSku/Add"; }, 1000);
                    
                } else {
                    $.MsgBox.Alert("提示", map.message);
                }
            },
            error: function () {
                $.MsgBox.Alert("提示", "修改失败！");
            }
        });
    });
    
    //修改预售
    $('#presaleNumModify').live('click', function () {
        if ($(this).text() == '[修改]') {
            $(this).text('[保存]');
            $('.panum,#tpanum').hide();
            $('.presalenum,#tspanum').show();
        } else {
            var istrue = false;
            $('.presalenum').each(function (index, element) {
                if (Number($(element).val()) < Number($(element).parent().parent().parent().find('.occupancy').text())) {
                    istrue = true;
                }
            });
            if (istrue) {
                $.MsgBox.Alert("提示", "修改的数量不能小于预售占用数量！");
                return false;
            }
            $('#formnum').form('submit', {
                url: "/Warehouse/BookingProductsSku/EditSave",
                type: "post",
                dataType: "json",
                data: $('#formnum').serialize(),
                success: function (r) {
                    var map = $.parseJSON(r);
                    if (map.result == 1) {
                        $.MsgBox.Alert("提示", map.message, 1000);
                        initTable({ productsID: $("#hdnProductsID").val() });
                    } else {
                        $.MsgBox.Alert("提示", map.message);
                    }
                },
                error: function () {
                    $.MsgBox.Alert("提示", "修改失败！");
                }
            });
        }

    });

    //取消
    $("#btnCancel").click(function () {
        location.href = "/Warehouse/BookingProductsSku/Add?ram=" + Math.random();
    });
});

//加载列表
function initTable(queryData) {
    var columns = "";
    var pageName = $("#hdnPageName").val();
    if (pageName == "Add") {
        columns = "[[" +
                     "{ field: 'Saleprop', title: '销售属性', width: 50, align: 'center' },"+
			         "{ field: 'Code', title: 'SKU码', width: 50, align: 'center' },"+
                     "{ field: 'BookingNum', title: '预售数量', width: 50, align: 'center', formatter: function (value, row, index) { if (row.Code != '汇总') { return '<input type=\"hidden\" name=\"ProductsSkuID\" value=\"' + row.ID + '\" /><input type=\"text\" name=\"BookingNum\" value=\"0\" class=\"presalenum\">'; } else { return '<span id=\"presalenum\">0</span>'; } } }"+
                   "]]";
    }
    else {
        columns = "[[" +
                     "{ field: 'Saleprop', title: '销售属性', width: 50, align: 'center' }," +
			         "{ field: 'Code', title: 'SKU码', width: 50, align: 'center' }," +
			         "{ field: 'BookingNum', title: '预售数量<a href=\"javascript:void(0)\" rel=\"1\" id=\"presaleNumModify\" class=\"yy\">[修改]</a>', width: 50, formatter: formatsale, align: 'center' }," +
			         "{ field: 'KyNum', title: '预售可用', width: 50, align: 'center' }," +
			         "{ field: 'ZyNum', title: '预售占用', width: 50, formatter: formatoccupancy, align: 'center' }," +
			         "{ field: 'CdNum', title: '冲抵数量', width: 50, align: 'center' }" +
                  "]]";
    }

    $('#grid').datagrid({
        url: '/Warehouse/BookingProductsSku/BookingNum?ram=' + Math.random(),
        width: '100%',
        height: $(document).height() - 255,
        fitColumns: true,
        rownumbers: false,
        showFooter: false,
        queryParams: queryData,  //异步查询的参数
        columns: eval('(' + columns + ')')
    });
}

function formatsale(val, row, index) {
    if (row.Code != '汇总') {
        return '<span class="panum">' + val + '</span><input type=\"hidden\" name=\"ProductsSkuID\" value=\"' + row.ID + '\" /><input type="text" name="BookingNum" value="' + val + '" class="presalenum ndis">';
    } else {
        return '<span id="tpanum">' + val + '</span><span id="tspanum" class="ndis">' + val + '</span>';
    }
}

function formatoccupancy(val, row) {
    return '<span class="occupancy">' + val + '</span>';
}