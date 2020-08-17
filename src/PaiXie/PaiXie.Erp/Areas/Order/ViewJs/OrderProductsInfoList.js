//初始化
$(function () {
    initTable();
});

//加载列表
function initTable() {
    $('#gridOuter').datagrid({
        url: '/Order/Download/ShowOrdOuterItem?ram=' + Math.random(),
        fitColumns: true,
        queryParams: { ordouterID: $("#hdnOrdouterID").val() },
        columns: [[
            { title: '商品名称', field: 'ProductsName', width: 200, align: 'center', sortable: true },
            { title: '属性', field: 'ProductsSkuSaleprop', width: 200, align: 'center', sortable: true },
            { title: '商品SKU码', field: 'ProductsSkuCode', width: 100, align: 'center', sortable: true },
            { title: '销售单价', field: 'Payment', width: 100, align: 'center', formatter: function (value, row) { return '￥' + value.toFixed(3) }, sortable: true },
            { title: '销售数量', field: 'ProductsNum', width: 100, align: 'center', sortable: true },
            {
                title: '添加状态', field: 'IsProductAddFin', width: 100, align: 'center',
                formatter: function (value, row) {
                    if (value == 0) {
                        return row.ProductAddMsg;
                    } else if(value == 1){
                        return row.ProductAddMsg;
                    }
                    else
                    {
                        return "已删除";
                    }
                },
                sortable: true
            },
            {
                title: '操作', field: 'Permit', width: 150, align: 'center',
                formatter: function (value, row) {
                    if (row.IsProductAddFin == 0) {
                        if (row.IsRefund == 0) {
                            return '<a href="javascript:void(0)" onclick=\'addProducts(' + row.ID + ')\'>添加关联商品</a>';
                        }
                        else {
                            return '<a href="javascript:void(0)" onclick=\'del(1,' + row.ID + ')\'>删除</a> | <a href="javascript:void(0)" onclick=\'addProducts (' + row.ID + ')\'>添加商品</a>';
                        }
                    }
                },
                sortable: true
            }
        ]],
        onLoadSuccess: function (data) {
            DataGridNoData(this);
        }
    });

    $('#grid').datagrid({
        url: '/Order/Download/ShowOrdItem?ram=' + Math.random(),
        fitColumns: true,
        queryParams: { ordouterID: $("#hdnOrdouterID").val() },
        queryParams: { shopID: $("#hdnShopID").val(), outOrderCode: $("#hdnOutOrderCode").val() },
        columns: [[
            { title: '商品编码', field: 'ProductsCode', width: 100, align: 'center', sortable: true },
            { title: '商品名称', field: 'ProductsName', width: 200, align: 'center', sortable: true },
            { title: '属性', field: 'ProductsSkuSaleprop', width: 200, align: 'center', sortable: true },
            { title: '商品SKU码', field: 'ProductsSkuCode', width: 100, align: 'center', sortable: true },
            { title: '销售单价', field: 'ActualSellingPrice', width: 100, align: 'center', formatter: function (value, row) { return '￥' + value.toFixed(3) }, sortable: true },
            { title: '销售数量', field: 'ProductsNum', width: 100, align: 'center', sortable: true },
            {
                title: '操作', field: 'Permit', width: 100, align: 'center',
                formatter: function (value, row) {
                            return '<a href="javascript:void(0)" onclick=\'del(2,' + row.ID + ')\'>删除</a>';
                },
                sortable: true
            }
        ]]
    });
}

function addProducts(ordouterItemID) {
    showLocalWindow("添加商品", "/Order/Download/AddOrdItem?ordouterItemID=" + ordouterItemID, 650, 450, true, false, false);
}

function del(type,id)
{
    var url = "";
    if(type == 1)
    {
        url = "/Order/Download/DelOuterItem";
    }
    else
    {
        url = "/Order/Download/DelItem";
    }

    $.ajax({
        url: url,
        type: "POST",
        cache: false,
        data: { id: id },
        success: function (r) {
            var map = $.parseJSON(r);
            if (map.result == 1) {
                initTable();
            } else {
                $.MsgBox.Alert("提示", map.message);
            }
        },
        error: function () {
            $.MsgBox.Alert("提示", "删除失败！");
        }
    });
}
