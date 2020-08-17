//初始化
$(function () {
    initTable();
    $("#back").click(function () {
        if (typeof ($("#hdnProductsSkuID").val()) != "undefined") {
            location.href = "/Warehouse/ConversionRule/Index";
        }
        else
        {
            location.href = "/Warehouse/LocationProducts/Index?parentID=" + $("#hdnParentID").val();
        }
    });
});

function initTable() {
    var strUrl = "";
    var columns = "";
    if (typeof($("#hdnProductsSkuID").val())!="undefined") {
        strUrl = "/Warehouse/ConversionRule/ShowSkuList?locationID=" + $("#hdnLocationID").val() + "&productsSkuID=" + $("#hdnProductsSkuID").val() + "&ram=" + Math.random();
        columns = "[[" +
                       "{ field: 'ID', title: '', hidden: true }," +
                       "{ field: 'ProductsCode', title: '商品编码', width: 150, align: 'center' }," +
                       "{ field: 'ProductsName', title: '商品名称', width: 150, align: 'center' }," +
                       "{ field: 'Saleprop', title: '属性', width: 150, align: 'center' }," +
                       "{ field: 'ProductsSkuCode', title: 'SKU码', width: 150, align: 'center' }," +
                       "{ field: 'ProductsBatchCode', title: '入库批次', width: 150, align: 'center' }," +
                       "{ field: 'ZkNum', title: '数量', width: 100, align: 'center' }" +
                  "]]";
    }
    else {
        strUrl = "/Warehouse/Location/ShowSkuList?id=" + $("#hdnLocationID").val() + "&ram=" + Math.random();
        columns = "[[" +
                       "{ field: 'ID', title: '', hidden: true },"+
                       "{ field: 'ProductsCode', title: '商品编码', width: 150, align: 'center' },"+
                       "{ field: 'ProductsName', title: '商品名称', width: 150, align: 'center' },"+
                       "{ field: 'Saleprop', title: '属性', width: 150, align: 'center' },"+
                       "{ field: 'ProductsSkuCode', title: 'SKU码', width: 150, align: 'center' },"+
                       "{ field: 'ProductsBatchCode', title: '入库批次', width: 150, align: 'center' },"+
                       "{ field: 'ZkNum', title: '库位数量', width: 100, align: 'center' },"+
                       "{ field: 'ZyNum', title: '占用数量', width: 100, align: 'center' },"+
                       "{ field: 'DjNum', title: '冻结数量', width: 100, align: 'center' }" +
                  "]]";
    }

    $('#grid').datagrid({
        url: strUrl,
        height: '100%',
        width: '100%',
        nowrap: false,
        autoRowHeight: false,
        striped: true,
        collapsible: true,
        pagination: true,
        fitColumns: true,
        fit: true, //datagrid自适应宽度
        fitColumn: false, //列自适应宽度
        border: false,
        pageSize: 15,
        pageList: [15, 20, 30],
        rownumbers: false,
        remoteSort: false,
        showFooter: false,
        columns: eval('(' + columns + ')')
    });
}