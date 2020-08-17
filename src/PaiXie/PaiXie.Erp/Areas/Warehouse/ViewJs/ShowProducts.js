//初始化
$(function () {
    initTable();
    $('.tag li').click(function () {
        var id = $('.tag li').index($(this));
        $('.tag li').removeClass('current');
        $(this).addClass('current');
        $('.tagcont').hide();
        $('.tagcont:eq(' + id + ')').show();
    });
});
$.fn.extend({
    resizeDataGrid: function () {
        $(this).datagrid('resize', {
            height: '100%',
            width: '100%'
        });
    }
});

function initTable() {
    $('#grid').datagrid({
        url: '/Warehouse/Products/ShowSkuList?id=' + $("#hdnProductsID").val(),
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
        columns:
        [[
            { title: '商品属性', field: 'Saleprop', width: 100, align: 'center', sortable: false },
            { title: '商品SKU码', field: 'ProductsSkuCode', width: 100, align: 'center', sortable: false },
            { title: '商品条码', field: 'ProductsSkuBarCode', width: 100, align: 'center', sortable: false },
            { title: '销售价', field: 'ProductsSkuSellingPrice', width: 100, align: 'center', sortable: false },
            { title: '成本价', field: 'ProductsSkuCostPrice', width: 100, align: 'center', sortable: false },
            { title: '关联物料', field: 'FromProductsSkuCode', width: 100, formatter: function (value, row) { if (value != "" && value != null) return value + "(1:" + row.FromNum + ")" }, align: 'center', sortable: false }
        ]],
        onLoadSuccess: function (data) {
            if (data.rows.length > 0) {
                mergeCellsByField("grid", "Saleprop,ProductsSkuCode,ProductsSkuBarCode,ProductsSkuSellingPrice,ProductsSkuCostPrice");
            }
            DataGridNoData(this);
        }
    });
}
function mergeCellsByField(tableID, colList) {
    var ColArray = colList.split(",");
    var tTable = $("#" + tableID);
    var TableRowCnts = tTable.datagrid("getRows").length;
    var tmpA;
    var tmpB;
    var PerTxt = "";
    var CurTxt = "";
    var alertStr = "";
    for (j = ColArray.length - 1; j >= 0; j--) {
        PerTxt = "";
        tmpA = 1;
        tmpB = 0;

        for (i = 0; i <= TableRowCnts; i++) {
            if (i == TableRowCnts) {
                CurTxt = "";
            }
            else {
                CurTxt = tTable.datagrid("getRows")[i][ColArray[j]];
            }
            if (PerTxt == CurTxt) {
                tmpA += 1;
            }
            else {
                tmpB += tmpA;

                tTable.datagrid("mergeCells", {
                    index: i - tmpA,
                    field: ColArray[j],　　//合并字段
                    rowspan: tmpA,
                    colspan: null
                });
                tTable.datagrid("mergeCells", { //根据ColArray[j]进行合并
                    index: i - tmpA,
                    field: "Ideparture",
                    rowspan: tmpA,
                    colspan: null
                });

                tmpA = 1;
            }
            PerTxt = CurTxt;
        }
    }
}