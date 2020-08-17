//初始化
var status = 1;
$(function () {
    initTable("", 1);
    initTable2("", 1);

    $('#tabs').tabs({
        onSelect: function (title, index) {
            switch (title) {
                case "系统日志":
                    status = 1;

                    BindSerarchLickEvent(1);
                    break;
                case "登录日志":
                    status = 2;

                    BindSerarchLickEvent(1);
                    break;
            }
        }
    });

    //搜索
    $('#btnSearch').click(function () {
        BindSerarchLickEvent(1);
    });



});



//加载列表
function initTable(queryData, pageNumber) {

    $('#grid').datagrid({
        url: '/SysWarehouse/logs/hislogsearch?ram=' + Math.random(),
        height: $(document).height() * 0.8,
        width: function () { return document.body.clientWidth * 0.9 },
        nowrap: true,
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
        { title: '主键', field: 'ID', width: 40, sortable: true, hidden: true, align: 'center' },
        { title: '用户代码', field: 'UserCode', width: 100, sortable: true, align: 'center' },
         { title: '姓名', field: 'UserName', width: 100, sortable: true, align: 'center' },
          { title: '主机名', field: 'HostName', width: 100, sortable: true, align: 'center' },
           { title: '主机IP', field: 'HostIP', width: 100, sortable: true, align: 'center' },
            { title: '登录地址', field: 'LoginCity', width: 100, sortable: true, align: 'center' },
             { title: '登录时间', field: 'LoginDate', width: 100, sortable: true, align: 'left' }
        ]]
    });
}


//加载列表
function initTable2(queryData, pageNumber) {

    $('#grid2').datagrid({
        url: '/SysWarehouse/logs/syslogsearch?ram=' + Math.random(),
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
        { title: '主键', field: 'ID', width: 40, sortable: true, hidden: true, align: 'center' },
        { title: '时间', field: 'Date', width: 100, sortable: true, align: 'center' },
         { title: '用户代码', field: 'UserCode', width: 80, sortable: true, align: 'center' },

           { title: '操作对象', field: 'Target', width: 80, sortable: true, align: 'center' },

             { title: '操作', field: 'ButtonName', width: 80, sortable: true, align: 'center' },

        { title: '操作内容', field: 'Message', width: 200, sortable: true, align: 'center' }

        ,
           {
               title: '操作', field: 'Permit', width: 55, align: 'center',
               formatter: function (value, row) {

                   return '<a href="#" onclick=\'seeMessage(' + row.ID + ')\'>查看</a>';
               },
               sortable: false
           }

        ]]
    });
}




//刷新
$("#refresh").click(function () {

    var grid;
    if (status == 1) {
        //将值传递给
        grid = $("#grid2")
    }
    else {
        grid = $("#grid")
    }

    var options = grid.datagrid("getPager").data("pagination").options;
    var currPageNumber = options.pageNumber;
    BindSerarchLickEvent(currPageNumber);

});
//绑定搜索按钮的的点击事件
function BindSerarchLickEvent(pageNumber) {
    //得到用户输入的参数
    var queryData = {
        keyWordType: $("#keyWordType").combobox('getValue'),
        keyWord: $("#txtKeyWord").val(),
        StartDate: $('#txtStartDate').datetimebox('getValue'),
        EndDate: $('#txtEndDate').datetimebox('getValue')
    }
    if (status == 1) {
        //将值传递给
        initTable2(queryData, pageNumber);
    }
    else {
        initTable(queryData, pageNumber);
    }

}

function seeMessage(id) {

    showLocalWindow("操作内容", "/SysWarehouse/logs/SeeMessage?id=" + id, 500, 300, true, false, false);
}