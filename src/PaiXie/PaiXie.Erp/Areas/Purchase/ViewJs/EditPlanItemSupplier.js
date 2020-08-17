//初始化
$(function () {
    initTable();
});

//加载列表
function initTable() {
    $('#grid').datagrid({
        url: '/Purchase/PlanItem/SearchSuppliers?productsSkuID=' + $("#hdnProductsSkuID").val() + '&ram=' + Math.random(),
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
        onSelect: function (rowIndex, rowData) {
            if (rowData.IsDefault == 0) {
                $.messager.confirm('提示', "是否设为默认方便以后系统自动选择供应商。<br>请点确定，设为默认？", function (r) {
                    if (r) {
                        updateSuppliersID($("#hdnPlanItemID").val(), rowData.SuppliersID, 1);
                    } else {
                        updateSuppliersID($("#hdnPlanItemID").val(), rowData.SuppliersID, 0);
                    }
                });
            } else {
                updateSuppliersID($("#hdnPlanItemID").val(), rowData.SuppliersID, 0);
            }
        },
        columns: [[
            { title: '主键', field: 'ID', width: 40, align: 'center', sortable: false, hidden: true },
			{
			    title: '<div style="text-align:center;">请点击选择供应商</div>', field: 'AliasName', width: 150, align: 'left',
			    formatter: function (value, row, index) {
			        var html = '&nbsp;&nbsp;&nbsp;&nbsp;';
			        if (row.IsDefault == 1) html = html + '<span style="color:#009900">[默认]</span> ';
			        html = html + value + '(￥<span style="color:#ff0000">' + row.PurchasePrice.toFixed(3) + '  ' + row.ArrivalCycle + '</span>天)'
			        return html;
			    },
			    sortable: false
			}
        ]],
        onLoadSuccess: function (data) {
            if (data.total == 0) {
                parent.$.MsgBox.Alert('提示', '该商品还没有关联供应商！<br>请先在<span style="color:#F00">供应商管理页面</span>把该商品添加到对应供应商商品');
                parent.$('#localWin').window('close');
            }
        }
    });
}

function updateSuppliersID(planItemID, suppliersID, isDefault) {
    $.ajax({
        url: "/Purchase/PlanItem/UpdateSuppliersID?PlanItemID=" + planItemID + "&suppliersID=" + suppliersID + "&productsSkuID=" + $("#hdnProductsSkuID").val() + "&isDefault=" + isDefault,
        type: "GET",
        cache: false,
        success: function (r) {
            var map = $.parseJSON(r);
            if (map.result == 1) {
                parent.$('#refreshCurrentPage').click();
                parent.$('#localWin').window('close');
            } else {
                $.MsgBox.Alert('提示', map.message);
            }
        },
        error: function () {
            $.MsgBox.Alert('提示', '操作失败！');
        }
    });
}