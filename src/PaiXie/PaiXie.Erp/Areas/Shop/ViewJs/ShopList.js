//初始化
$(function () {
  
    initTable();
    BindSerarchLickEvent();
});


function editshop(id) {
    location.href = "/shop/shops/Edit?id=" + id;
  }


//加载列表
function initTable(queryData) {

  

    $('#grid').datagrid({
        url: '/shop/shops/search?ram=' + Math.random(),
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
        remoteSort: false,
        idField: 'ID',
        queryParams: queryData,  //异步查询的参数
        onClickRow: function (rowIndex, rowData) {
            $('#hid').val(rowData.ID);
            if (rowData.IsEnable==1)
                $("#lbldelete").text("删除");
            else
                $("#lbldelete").text("启用");

        },
        //onDblClickRow: function (rowIndex, rowData) {
        //    location.href = "/shop/shops/Edit?id=" + rowData.ID;
        //   //  showMyWindow("店铺信息", "/shop/shops/Edit?id=" + rowData.ID, 700, 800, true, false, false);
        //},
        columns: [[
        { field: 'ck', checkbox: true },   //选择
        { title: '主键', field: 'ID', width: 40, sortable: true, hidden: true },
          { title: '店铺名称', field: 'Name', width: 100, align: 'center', sortable: true },
          //{
          //    title: '是否可用', field: 'IsEnable', width: 50, align: 'center',
          //    formatter: function (value, row) { return '<img src="/Content/images/' + ((value || '').toString() == "1" ? "checkmark.gif" : "checknomark.gif") + '"/>'; },
          //    sortable: true
          //},
          //{ title: '店铺类型', field: 'part1', align: 'center', width: 50, sortable: true },
         { title: '平台类型', field: 'part1', width: 120, sortable: true },

          { title: '负责人', field: 'ContactPerson', width: 120, sortable: true },

           { title: '联系电话', field: 'ContactTel', width: 120, sortable: true },


      { title: '添加时间', field: 'CreateDate', width: 120, sortable: true },
         {
             title: '操作', field: 'Permit', width: 155, align: 'center',
             formatter: function (value, row) {

                 return '<a href="#" onclick=\'editshop(' + row.ID + ')\'>编辑</a>';
             },
             sortable: false
         }
    
        
              
        ]]
    });
}
 function setRole(id)
{
    showMyWindow("角色信息", "/sys/User/userrole?UID=" + id, 700, 700, true, false, false);
}



//刷新
$("#refresh").click(function () {
  initTable();
});
//添加
$("#add").click(function () {
   // showMyWindow("用户信息", "/shop/shops/Edit?id=0", 700, 800, true, false, false);
    location.href = "/shop/shops/Edit?id=0" ;
});
//删除
$("#delete").click(function () {
    var id = $('#hid').val();
   // if (id != "0") {
        var rows = $("#grid").datagrid("getSelections");
        if (rows.length > 0) {
            var ids = [];
            for (var i = 0; i < rows.length; i++) {
                ids.push(rows[i].ID);
            }

        $.messager.confirm('提示', "确定要删除吗", function (r) {
            if (r) {
                $.ajax({
                    url: "/shop/shops/Delete?id=" +  ids.join(','),
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
       // showMsg("提示", "未选择！", true);
        $.MsgBox.Alert("提示", "未选择！", 1000);
    }
});
//绑定搜索按钮的的点击事件
function BindSerarchLickEvent() {
    //按条件进行查询数据，首先我们得到数据的值
    $("#btnSerach").click(function () {

 var ptlx=$("#ptlx").combobox('getValue');

 var pname = $("#ptmc").combobox('getValue');
      //  alert($("#ptmc").combobox('getValue'));
    
        //将值传递给

        var queryData = {
            Name: $("#Name").val()
           ,
      Type:ptlx,
        PlatformType: pname
        };


        initTable(queryData);
        return false;
    });
}