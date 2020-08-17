//初始化
$(function () {

  $.MsgBox.Alert("提示", "用户名已存在！");
    //  $.MsgBox.Confirm("提示", "用户名已存在",'aa');
    initTable();
    BindSerarchLickEvent();
});
//加载列表
function initTable(queryData) {
    $('#grid').datagrid({
        url: '/Demo/Student/search',
        height: $(document).height() * 0.8,
        width: function () { return document.body.clientWidth * 0.9 },
        nowrap: true,
        autoRowHeight: false,
        striped: true,
        collapsible: true,
        pagination: true,
        fitColumns: true,
        pageNumber: 1,
        fit: true, //datagrid自适应宽度       
        border: false,
        pageSize: 5,
        pageList: [5, 20, 30],
        rownumbers: false,
        sortName: 'ID',
        sortOrder: 'asc',
        remoteSort: false,
        idField: 'ID',
        queryParams: queryData,  //异步查询的参数
        onClickRow: function (rowIndex, rowData) {
            $('#hid').val(rowData.ID);
        },//单击
        onDblClickRow: function (rowIndex, rowData) {
            location.href = "/Demo/Student/edit?id=" + rowData.ID;
        },//双击
        columns: [[
        { field: 'ck', checkbox: true },   //选择
        { title: '主键', field: 'ID', width: 40, sortable: true, hidden: true },
        { title: '姓名', field: 'SstuNmae', width: 100, sortable: true },
         { title: '成绩', field: 'Score', width: 100, sortable: true },
          { title: '入学时间', field: 'CreTime', width: 100, sortable: true },
          {
              title: '班级', field: 'ClassName', width: 100,
              formatter: function (value, row) { return '<a href="https://www.baidu.com/">sasasa </a>'; },

              sortable: true
          },
         { title: '备注', field: 'Remark', width: 100, sortable: true }
        ]]
    });
}
//刷新
$("#refresh").click(function () {
    // initTable();
    //获取当前页码 
    var grid = $('#grid');
    var options = grid.datagrid('getPager').data("pagination").options;
    var curr = options.pageNumber;
    alert(curr);
    //var total = options.total;
    //var max = Math.ceil(total / options.pageSize);
});
//添加
$("#add").click(function () {
    location.href = "/Demo/student/edit?id=0";
});
//删除
$("#delete").click(function () {
    //批量操作
    var rows = $("#grid").datagrid("getSelections");
        if (rows.length > 0) {
            var ids = [];
            for (var i = 0; i < rows.length; i++) {
                ids.push(rows[i].ID);
            }
            alert(ids.join(','));
        }
        else {
            showMsg("提示", "请选择！", true);         
        }
   


   //单个操作
    ////var id = $('#hid').val();
    ////if (id != "0") {
    ////    $.messager.confirm('提示', "确定要删除吗", function (r) {
    ////        if (r) {
    ////            $.ajax({
    ////                url: "/Demo/student/delete?id=" + id,
    ////                type: "GET",
    ////                cache: false,
    ////                success: function (date) {
    ////                    if (date == "OK")
    ////                        initTable();
    ////                    else
    ////                        showMsg("提示", date, true);
    ////                },
    ////                error: function () {
    ////                    showMsg("提示", "删除失败！", true);
    ////                }
    ////            });
    ////        }
    ////    });
    ////}
    ////else {
    ////    showMsg("提示", "未选择！", true);
    ////}
});
//绑定搜索按钮的的点击事件
function BindSerarchLickEvent() {
    //按条件进行查询数据，首先我们得到数据的值
    $("#btnSerach").click(function () {
        //得到用户输入的参数
        var queryData = {
            SstuNmae: $("#txtName").val()
        }
        //将值传递给
        initTable(queryData);
        return false;
    });
}