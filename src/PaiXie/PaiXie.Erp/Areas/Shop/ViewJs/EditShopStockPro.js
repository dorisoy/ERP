//返回
$("#btnClose").click(function () {
    location.href = "/shop/ShopStock/index";
});
//保存
$("#btnSave").click(function () {
    btnSave();
});
function btnSave() {
    $('#ff').form('submit', {
        url: "/shop/ShopStock/Save",
        type: "POST",//使用get方法访问后台
        dataType: "json",
        onSubmit: function () {
            var isValid = $(this).form('validate');
            return isValid;	// 返回false终止表单提交
        },
        success: function (r) {
            var map = $.parseJSON(r);
            if (map.result == 1) {
                //当前tab
                location.href = "/shop/ShopStock/index";
            }
            else {
                $.MsgBox.Alert("提示", map.message);
            }
        },
        error: function () {
            $.MsgBox.Alert("提示", "保存失败！");
        }
    });
}
//添加独享
$("#adddx").click(function () {
    showLocalWindow("添加独享库存", "/shop/ShopStock/AddDx?ProductsID=" + $("#ProductsID").val(), 500, 520, true, false, false);
});
//独享库存
function getdzkc(id) {
    showLocalWindow("独享库存", "/shop/ShopStock/AddDxRead?ProductsID=" + $("#ProductsID").val() + "&shopid=" + id, 500, 520, true, false, false);

}
$(function () {
    initTable();
});
function initTable() {
    $('#grid').datagrid({
        url: '/shop/ShopStock/search?ram=' + Math.random() + '&ProductsID=' + $('#ProductsID').val(),
        height: 400,
        width: 400,
        nowrap: true,
        autoRowHeight: false,
        striped: true,
        collapsible: true,
        pagination: false,
        fitColumns: true,
        fit: true, //datagrid自适应宽度
        border: false,
        pageSize: 15,
        pageList: [15, 20, 30],
        rownumbers: false,
        remoteSort: false,
        idField: 'ShopID',
        columns: [[
       // { field: 'ck', checkbox: true },   //选择
        { title: '主键', field: 'ShopID', width: 40, sortable: true, hidden: true },
        { title: '店铺名称', field: 'ShopName', width: 100, align: 'center', sortable: true },
        {
            title: '独占库存', field: 'xsnum', width: 100, align: 'left',
            formatter: function (value, row) {
                return '<a href="#" onclick=\'getdzkc(' + row.ShopID + ')\' style="margin-left:10px">' + row.xsnum + '</a>';
            },
            sortable: true
        },
          { title: '是否使用公用库存', field: 'IsSalePub', width: 100, align: 'center', sortable: true },
        {
            title: '操作', field: 'Permit', width: 100, align: 'left',
            formatter: function (value, row) {
                return '<a href="#" onclick=\'del(' + row.ShopID + ')\' style="margin-left:10px">删除</a>';
            },
            sortable: true
        }
        ]]
    });
}
//删除
function del(id) {
    $.messager.confirm('提示', "确定要删除吗", function (r) {
        if (r) {
            $.ajax({
                url: "/shop/ShopStock/Delete?shopid=" + id + "&ProductsID=" + $('#ProductsID').val(),
                type: "GET",
                cache: false,
                success: function (r) {
                    var map = $.parseJSON(r);
                    if (map.result == 1) {

                        initTable();
                    }
                    else {
                        $.MsgBox.Alert("提示", map.message);
                    }
                },
                error: function () {
                    $.MsgBox.Alert("提示", "删除失败！");
                }
            });
        }
    });
}
//刷新
$("#refresh").click(function () {
    initTable();
});