//初始化
$(function () {
    initTable();
    $('#back').click(function () {
        if ($('#kc').val() == "1") //返回分配库存
        {
            location.href = '/shop/ShopStock/Index';
        } else {
            var status = $("#hdnStatus").val();
            location.href = '/Products/Products/Index?status=' + status;
        }
    });

    $('#noun').click(function () {
        $.messager.alert({
            title: '名词解释',
            msg: '<b>库存：</b>可用库存+占用库存<br><b>可用库存：</b>系统库存中（包含预售可用），可以下单销售的库存<br><b>占用库存：</b>系统库存中（包含预售占用），被订单占用的库存<br><b>冻结库存：</b>实际发货库存中，被冻结的数量<br><b>预售可用：</b>预售库存中，还可以销售并下单的数量<br><b>预售占用：</b>预售库存中，已被订单占用的数量<br><b>备用库存：</b>仓库中备用库区的商品数量',
            width: 450,
            icon: '',
            style: {
                lineHeight: '24px'
            }
        });
    });
    $.extend($.messager.defaults, {
        ok: "我知道了"
    });
    $('#change').click(function () {
        if ($(this).attr('rel') != '1') {
            $(this).attr('rel', '1');
            $('#grid').datagrid('loadData', { total: 0, rows: [] });
            initTable();
        } else {
            $(this).attr('rel', '2');
            $('#grid').datagrid('loadData', { total: 0, rows: [] });
            $('#grid').datagrid({
                url: '/Products/Products/ShowWarehouseSkuList?id=' + $("#hdnProductsID").val() + '&ram=' + Math.random(),
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
                    {
                        field: 'WarehouseName', title: '仓库名称', width: 150, align: 'center',
                        formatter: function (value, row) {
                            var html = value;
                            if (row.ProductsStatus == 2) {
                                html += "<br/><span style=\"color:red;\">[已下架]</span>";
                            }
                            return html;
                        }
                    },
                    { field: 'Saleprop', title: '销售属性', width: 150, align: 'center' },
                    { field: 'ProductsSkuCode', title: 'SKU码', width: 80, align: 'center' },
                    { field: 'TotalNum', title: '库存', width: 80, align: 'center' },
                    { field: 'KyNum', title: '可用库存', width: 80, align: 'center' },
                    { field: 'ZyNum', title: '占用库存', width: 80, align: 'center' },
                    { field: 'DjNum', title: '冻结库存', width: 80, align: 'center' },
                    { field: 'YsNum', title: '预售可用', width: 80, align: 'center' },
                    { field: 'YsZyNum', title: '预售占用', width: 80, align: 'center' },
                    { field: 'ByNum', title: '备用库存', width: 80, align: 'center' }
                ]],
                onLoadSuccess: function (data) {
                    if (data.rows.length > 0) {
                        mergeCellsByField("grid", "WarehouseName");
                    }
                    DataGridNoData(this);
                }
            });
        }
        return false;
    });
});

//加载列表
function initTable() {
    $('#grid').datagrid({
        url: '/Products/Products/ShowSkuList?id=' + $("#hdnProductsID").val() + '&ram=' + Math.random(),
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
            { field: 'Saleprop', title: '销售属性', width: 150, align: 'center' },
            { field: 'ProductsSkuCode', title: 'SKU码', width: 150, align: 'center' },
            { field: 'TotalNum', title: '库存', width: 80, align: 'center' },
            {
                field: 'KyNum', title: '可用库存', width: 80, align: 'center', formatter: function (value, row) {
                    return value < 0 ? "<lable style='color:#ff0000;' title='下单后仓库库存变动导致超卖'>" + value + "</lable>" : value;
                }
            },
            { field: 'ZyNum', title: '占用库存', width: 80, align: 'center' },
            { field: 'DjNum', title: '冻结库存', width: 80, align: 'center' },
            { field: 'YsNum', title: '预售可用', width: 80, align: 'center' },
            { field: 'YsZyNum', title: '预售占用', width: 80, align: 'center' },
            { field: 'ByNum', title: '备用库存', width: 80, align: 'center' }
        ]]
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