//初始化
$(function () {
    initTable();

    //商品SKU码是否存在判断
    $("#txtProductsSkuCode").blur(checkProductsSkuCode);

    $("#btnSave").click(btnSave);
});

function checkProductsSkuCode() {
    var result = 1;
    if ($("#txtProductsSkuCode").val() != "") {
        $.ajax({
            url: "/Products/Products/CheckProductsSkuCode",
            data: { "sourceProductsSkuID": $("#hdnSourceProductsSkuID").val(), "productsSkuCode": $("#txtProductsSkuCode").val() },
            cache: false,
            success: function (r) {
                var map = $.parseJSON(r);
                if (map.result == 0) {
                    result = 0;
                    $("#errorMessage").html(map.message);
                    $("#txtProductsSkuCode").focus();
                }
                $("#hdnFromProductsSkuID").val(map.fromProductsSkuID);
            },
            error: function () {
                result = 0;
                $.MsgBox.Alert("提示", "读取商品SKU码失败！");
            }
        });
    }
    return result;
}

function btnSave() {
    var result = checkProductsSkuCode();
    if (result == 1) {
        $('#ff').form('submit', {
            url: "/Products/Products/AddProductsMaterialMap",
            type: "POST",//使用get方法访问后台
            dataType: "json",
            onSubmit: function () {
                var isValid = $(this).form('validate');
                return isValid;	// 返回false终止表单提交
            },
            success: function (r) {
                var map = $.parseJSON(r);
                if (map.result == 1) {
                    $('#txtProductsSkuCode').val('');
                    $('#txtFromNum').val('');
                    initTable();
                    parent.$('#refresh').click();
                } else {
                    $.MsgBox.Alert("提示", map.message);
                }
            },
            error: function () {
                $.MsgBox.Alert("提示", "添加失败！");
            }
        });
    }
}

function initTable() {
    $('#grid').datagrid({
        url: '/Products/Products/GetProductsSkuMaterialMapList?sourceProductsSkuID=' + $("#hdnSourceProductsSkuID").val(),
        height: $(document).height() * 0.8,
        width: function () { return document.body.clientWidth * 0.9 },
        nowrap: false,
        autoRowHeight: false,
        striped: true,
        collapsible: true,
        pagination: false,
        fitColumns: true,
        fit: true, //datagrid自适应宽度
        border: false,
        rownumbers: false,
        showFooter: false,
        columns: [[
            { field: 'SmallPic', title: '商品名称', width: 150, formatter: formatimg, align: 'center' },
            { field: 'Saleprop', title: '属性', width: 80, align: 'center' },
            { field: 'ProductsSkuCode', title: '商品SKU码', width: 80, align: 'center' },
            {
                field: 'FromNum', title: '关联比例', width: 80, formatter: function (value, row) {
                    return '1:' + value;
                }, align: 'center'
            },
            {
                field: 'Permit', title: '操作', width: 50, align: 'center',
                formatter: function (value, row) {
                    return '<a href="javascript:void(0);" style="color:#ff0000;" onclick=\'delProductsMaterialMap(' + row.ID + ')\'>X</a>';
                },
                sortable: false
            }
        ]],
        onLoadSuccess: function (data) {
            $("#grid").datagrid('clearSelections');
            DataGridNoData(this);
        }
    });
}

function formatimg(val, row) {
    var firstSmallPic = "../../Upload/Products/NoImg.jpg";
    if (val != null && val != '') {
        firstSmallPic = val.split(',')[0];
    }
    return '<table><tr><td><div class="d48"><dfn></dfn><img src=' + firstSmallPic + '></div></td><td>' + row.ProductsName + '</td></tr></table>';
}

function delProductsMaterialMap(productsMaterialMapID) {
    $.ajax({
        url: '/Products/Products/DelProductsMaterialMap?productsMaterialMapID=' + productsMaterialMapID,
        type: 'GET',
        cache: false,
        success: function (r) {
            var map = $.parseJSON(r);
            if (map.result == 1) {
                $('#grid').datagrid('reload');
                parent.$('#refresh').click();
            } else {
                $.MsgBox.Alert("提示", map.message);
            }
        },
        error: function (data) {
            $.MsgBox.Alert("提示", "删除失败！");
        }
    });
}
