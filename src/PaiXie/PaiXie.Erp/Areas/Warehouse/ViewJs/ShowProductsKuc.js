//初始化
$(function () {
    initTable();

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
});

//加载列表
function initTable(queryData) {
    $('#grid').datagrid({
        url: '/Warehouse/WarehouseProducts/ShowSkuKuc?id=' + $("#hdnProductsID").val(),
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
            { field: 'KyNum', title: '可用库存', width: 80, align: 'center' },
            { field: 'ZyNum', title: '占用库存', width: 80, align: 'center' },
            { field: 'DjNum', title: '冻结库存', width: 80, align: 'center' },
            { field: 'YsNum', title: '预售可用', width: 80, align: 'center' },
            { field: 'YsZyNum', title: '预售占用', width: 80, align: 'center' },
            { field: 'ByNum', title: '备用库存', width: 80, align: 'center' }
        ]]
    });
}