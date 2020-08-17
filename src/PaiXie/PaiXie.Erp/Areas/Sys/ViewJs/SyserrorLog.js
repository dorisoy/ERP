$(function () {
    bindSerarchLickEvent(1);
    //刷新
    $("#refresh").click(function () {
        bindSerarchLickEvent(1);
    });
    //搜索
    $('#btnSearch').click(function () {
        bindSerarchLickEvent(1);
    });
    //清空条件
    $('#btnReset').click(function () {
        location.reload(true);
    });
});
//绑定搜索按钮的的点击事件
function bindSerarchLickEvent(pageNumber) {
    //得到用户输入的参数
    var queryData = {
        keyWordType: $("#keyWordType").combobox('getValue'),
        keyWord: $("#txtKeyWord").val(),
        StartDate: $('#txtStartDate').datetimebox('getValue'),
        EndDate: $('#txtEndDate').datetimebox('getValue')
    }
    initTable(queryData, pageNumber);
}
//加载列表
function initTable(queryData, pageNumber) {
    $('#grid').datagrid({
        url: '/sys/logs/SyserrorLogSearch?ram=' + Math.random(),
        height: $(document).height() * 0.8,
        width: function () { return document.body.clientWidth * 0.9 },
        nowrap: false,
        autoRowHeight: false,
        striped: true,
        collapsible: true,
        pagination: true,
        fitColumns: true,
        fit: true, //datagrid自适应宽度
        pageNumber: pageNumber,
        border: false,
        pageSize: 15,
        pageList: [15, 20, 30],
        rownumbers: false,
        remoteSort: false,
        idField: 'ID',
        queryParams: queryData,  //异步查询的参数      
        columns: [[
       // { field: 'ck', checkbox: true },   //选择
        { title: '主键', field: 'ID', width: 40, sortable: false, hidden: true, align: 'center' },
        { title: '错误地址', field: 'ErrorUrl', width: 150, sortable: false, align: 'center' },
        { title: '错误信息', field: 'FriendlyMessage', width: 150, sortable: false, align: 'center' },
        { title: '错误详情', field: 'StackTrace', width: 250, sortable: false, align: 'center' },
        { title: '错误备注', field: 'Remark', width: 100, sortable: false, align: 'center' },
        { title: '创建人', field: 'CreatePerson', width: 100, sortable: true, align: 'center' },
        { title: '创建时间', field: 'CreateDate', width: 120, sortable: true, align: 'center' }
        ]]
    });
}