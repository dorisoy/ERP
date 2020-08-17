//查看订单详情
function showOrderDetails(erpOrderCode) {
    var title = '订单编号' + erpOrderCode;
    var src = "/Order/Details/Index?erpOrderCode=" + erpOrderCode;
    var mid = "OrderDetails" + erpOrderCode;
    //拼接一个Iframe标签
    var str = '  <iframe id="frmWorkArea' + mid + '" data-options="fit:true" frameborder="0" style="  width: 100%; height: 100%; overflow: hidden;" src="' + src + '"></iframe>   ';
    //首先判断用户是否已经单击了此项，如果单击了直接获取焦点，负责打开
    var isExist = parent.$("#worktab").tabs('exists', title);
    if (!isExist) {
        parent.$('#worktab').tabs('add', { title: title, content: str, closable: true });
    }
    else {
        parent.$("#worktab").tabs('select', title);
        var targetTab = parent.$("#worktab").tabs("getSelected");
        parent.$('#worktab').tabs('update', {
            tab: targetTab,
            options: {
                content: str
            }
        });
    }
}
