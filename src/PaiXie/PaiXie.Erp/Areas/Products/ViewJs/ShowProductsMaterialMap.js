//初始化
$(function () {
    initTable();
    $('#back').click(function () {
        var status = $("#hdnStatus").val();
        location.href = '/Products/Products/Index?status=' + status;
    });
    $('#refresh').click(function () {
        initTable();
    }); 
});
//加载列表
function initTable() {
    $('#grid').datagrid({
        url: '/Products/Products/ShowProductsMaterialMapGroupList?id=' + $("#hdnProductsID").val(),
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
        onDblClickRow: function (rowIndex, rowData) {
            setProductsMaterialMap(rowData.ProductsSkuID);
        },
        columns: [[
			{ field: 'Saleprop', title: '销售属性', width: 80, align: 'center' },
			{ field: 'ProductsSkuCode', title: 'SKU码', width: 80, align: 'center' },
			{ field: 'ProductsMaterialMapCount', title: '关联物料数量', width: 80, align: 'center' },
			{
			    field: 'permit', title: '操作', width: 155, align: 'center',
			    formatter: function (value, row) {
			        return '<a href="javascript:void(0);" onclick=\'setProductsMaterialMap(' + row.ProductsSkuID + ');\'>设置</a>';
			    },
			    sortable: false
			}
        ]]
    });
}
function setProductsMaterialMap(sourceProductsSkuID) {
    showLocalWindow("关联物料", "/Products/Products/SetProductsMaterialMap?SourceProductsSkuID=" + sourceProductsSkuID, 700, 500, true, false, false);
}
