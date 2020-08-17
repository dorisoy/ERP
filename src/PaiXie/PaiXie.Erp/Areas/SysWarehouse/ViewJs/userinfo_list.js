//初始化
$(function () {
 
    initTable();
    BindSerarchLickEvent();
 
    
});
//加载列表
function initTable(queryData) {
 
    $('#grid').datagrid({
        url: '/SysWarehouse/User/search?ram=' + Math.random(),
        height: $(document).height() * 0.8,
        width: function () { return document.body.clientWidth * 0.9 },
        nowrap: true,
        autoRowHeight: false,
        striped: true,
        collapsible: true,
        pagination: true,
        fitColumns: true,
        fit: true, //datagrid自适应宽度
        
        border: false,
        pageSize: 15,
        pageList: [15, 20, 30],
        rownumbers: false,
        sortName: 'ID',
        sortOrder: 'desc',
        remoteSort: false,
        idField: 'ID',
        queryParams: queryData,  //异步查询的参数
        onClickRow: function (rowIndex, rowData) {
            $('#issupper').val(rowData.IsSupper);
            $('#hid').val(rowData.ID);
        },
        onDblClickRow: function (rowIndex, rowData) {
            //if (rowData.IsSupper == 1) {
            //    $.MsgBox.Alert("提示", "超级管理员无法编辑！", 1000);
            //    return false;
            //}

            //if (!IsAuthJs('b0013')) {
            //    return false;
            //}
            //showLocalWindow("用户信息", "/SysWarehouse/User/Edit?id=" + rowData.ID, 700, 500, true, false, false);
        },
        columns: [[
       // { field: 'ck', checkbox: true },   //选择
        { title: '主键', field: 'ID', width: 40, sortable: true, hidden: true },
        { title: '用户代码', field: 'Code', width: 100, align: 'center', sortable: true },
         { title: '姓名', field: 'Name', width: 100, align: 'center', sortable: true },
          {
              title: '是否可用', field: 'IsEnable', width: 50, align: 'center',
              formatter: function (value, row) { return '<img src="/Content/images/' + ((value || '').toString() == "1" ? "checkmark.gif" : "checknomark.gif") + '"/>'; },
              sortable: true
          },
          { title: '登录次数', field: 'LoginCount', align: 'center', width: 50, sortable: true },
          { title: '最后登录时间', field: 'LastLoginDate', width: 120, sortable: true },
           { title: '创建人', field: 'cname', width: 100, align: 'center', sortable: true },
            { title: '创建时间', field: 'CreateDate', width: 120, sortable: true },
             { title: '修改人', field: 'uname', width: 100, align: 'center', sortable: true },
              { title: '修改时间', field: 'UpdateDate', width: 120, sortable: true },

        {
            title: '操作', field: 'Permit', width: 160, align: 'left',
            formatter: function (value, row) {
                if (row.IsSupper == 1) {
                    return '--';
                }
                else
                {
                    var str = '<a href="#" onclick=\'setRole(' + row.ID + ')\' style="margin-left:10px"><span class="icon icon-ok">&nbsp;</span>[设置角色]</a>';
                    str += '&nbsp;<a href="#" onclick=\'setEdit(' + row.ID + ')\' style="margin-left:10px"><span class="icon icon-ok">&nbsp;</span>[编辑]</a>';
                    return str;
                }
             
                   
              
              
            
            },
    sortable: true
}

              
        ]]
    });
}


function setEdit(id) {
  
    showLocalWindow("用户信息", "/SysWarehouse/User/Edit?ck=" + $('#ck').val() + "&id=" + id, 700, 500, true, false, false);




}




 function setRole(id)
{
  
     showLocalWindow("角色信息", "/SysWarehouse/User/userrole?UID=" + id, 700, 500, true, false, false);
}



//刷新
$("#refresh").click(function () {
 
  initTable();
});
//添加
$("#add").click(function () {
 
  
    showLocalWindow("用户信息", "/SysWarehouse/User/add?id=0", 700, 500, true, false, false);

});
//删除
$("#delete").click(function () {

    if ($('#issupper').val() == 1) {
        $.MsgBox.Alert("提示", "超级管理员无法编辑！", 1000);
        return false;
    }

  
    var id = $('#hid').val();
    if (id != "0") {
        $.messager.confirm('提示', "确定要删除吗", function (r) {
            if (r) {
                $.ajax({
                    url: "/SysWarehouse/User/Delete?id=" + id,
                    type: "GET",
                    cache: false,
                    success: function (r) {
                        var map = $.parseJSON(r);
                        if (map.result == 1) {
                            $('#hid').val("0");
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
    else {
        $.MsgBox.Alert("提示", "未选择！");
    }
});
//绑定搜索按钮的的点击事件
function BindSerarchLickEvent() {
    //按条件进行查询数据，首先我们得到数据的值
    $("#btnSerach").click(function () {
        //得到用户输入的参数
        var queryData = {
            Code: $("#Code").val()

        }
        //将值传递给
        initTable(queryData);
        return false;
    });
}