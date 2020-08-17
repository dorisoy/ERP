//初始化
$(function () {
    $('#Category').combotree({
        onSelect: function (node) {
            //返回树对象
            var tree = $(this).tree;
            //选中的节点是否为叶子节点,如果不是叶子节点,清除选中
            var isLeaf = tree('isLeaf', node.target);
            if (!isLeaf) {
                //清除选中
                $.MsgBox.Alert("提示", "不能选择父节点！");
                $('#Category').combotree('clear');
            }
        }
    });
    $("#btnClose").click(function () {
        parent.$('#localWin').window('close');
    });
    $("#btnSave").click(btnSave);
});
function btnSave() {
    $('#ff').form('submit', {
        url: "/Products/Products/SaveCategory?ids=" + $("#productsIDs").val() + "&categoryID=" + $("#Category").combotree("getValue"),
        type: "POST",//使用get方法访问后台
        dataType: "json",
        onSubmit: function () {
            var isValid = $(this).form('validate');
            return isValid;	// 返回false终止表单提交
        },
        success: function (r) {
            var map = $.parseJSON(r);
            if (map.result == 1) {
                parent.$("#refreshCurrentPage").click();
                $("#btnClose").click();
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