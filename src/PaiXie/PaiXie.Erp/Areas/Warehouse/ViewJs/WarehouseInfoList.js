//初始化
$(function () {
    initTable();
    BindSerarchLickEvent();
});
//加载列表
function initTable(queryData) {
 
    $('#grid').datagrid({
        url: '/Warehouse/Warehouse/search?ram=' + Math.random(),
        height: $(document).height() * 0.8,
        width: function () { return document.body.clientWidth * 0.9 },
        nowrap: false,
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
        },
        //onDblClickRow: function (rowIndex, rowData) {
        //   // location.href = "/sys/sys/EditUser?id=" + rowData.ID;
        //    showMyWindow("仓库信息", "/Warehouse/Warehouse/Edit?id=" + rowData.ID, 700, 800, true, false, false);
        //},
        columns: [[
       // { field: 'ck', checkbox: true },   //选择
        { title: '主键', field: 'ID', width: 40, sortable: true, hidden: true },
      { title: '仓库名称', field: 'Name', width: 100, align: 'center', sortable: true },

      { title: '授权品牌', field: 'part1', width: 100, align: 'center', sortable: true },

      { title: '备注', field: 'Remark', width: 100, align: 'center', sortable: true },


        {
              title: '是否可用', field: 'IsEnable', width: 50, align: 'center',
              formatter: function (value, row) { return '<img src="/Content/images/' + ((value || '').toString() == "1" ? "checkmark.gif" : "checknomark.gif") + '"/>'; },
              sortable: true
          },
       
        {
            title: '操作', field: 'Permit', width: 200, align: 'center',
            formatter: function (value, row) {
                var aa=   '<a href="/home/index?wid='+ row.Code +'" target="_blank"  style="margin-left:10px"><span class="icon icon-ok">&nbsp;</span>[进入仓库]</a>';
                aa += '<a href="javascript:void(0);" onclick="editshow(' + row.ID + ')" style="margin-left:10px"><span class="icon icon-ok">&nbsp;</span>[修改]</a>';
                aa += '<a href="javascript:void(0);" onclick="editwarea(' + row.ID + ')" style="margin-left:10px"><span class="icon icon-ok">&nbsp;</span>[设置配送区域]</a>';

                aa += '<a href="javascript:void(0);" onclick="editstatus(' + row.ID + ','+row.IsEnable+')" style="margin-left:10px"><span class="icon icon-ok">&nbsp;</span>[' + ((row.IsEnable).toString() == "1" ? "禁用" : "启用") + ']</a>';

                return aa;
                   
                   
              
              
            
            },
    sortable: true
}

              
        ]]
    });
}




function editstatus(rid, status) {

    var str = "确认禁用仓库？";
    if (status == 0)
    {
        str = "确认启用仓库？";
    }
  
    $.messager.confirm('提示', str, function (r) {
            if (r) {
              

                $.ajax({
                    url: "/Warehouse/Warehouse/IsEnablewarehouse?wid=" + rid + "&sf=1",
                    type: "GET",
                    cache: false,
                    success: function (r) {
                        var map = $.parseJSON(r);
                        if (map.result == 1) {
                            $.MsgBox.Alert("提示", "操作成功！", 1000);
                            initTable();
                        }
                        else {
                            $.MsgBox.Alert("提示", map.message);
                        }
                    },
                    error: function () {
                        $.MsgBox.Alert("提示", "操作失败！");
                    }
                });


            }

        });
 //   }
   
    }


function editshow(rid)
{
    location.href = "/Warehouse/Warehouse/Edit?id=" + rid;
  //  showMyWindow("仓库信息", "/Warehouse/Warehouse/Edit?id=" + rid, 700, 800, true, false, false);
}



function editwarea(rid) {
    showLocalWindow("设置配送区域", "/Warehouse/Warehouse/SelectArea?id=" + rid, 900, 630, true, false, false);

   // showLocalWindow("选择地区", "/Warehouse/Express/SelectArea?warehouseExpressPriceID=0&rowIndex=0", 900, 550, true, false, false);


}



 function setRole(id)
{
     showMyWindow("角色信息", "/Warehouse/Warehouse/userrole?UID=" + id, 700, 500, true, false, false);
}



//刷新
$("#refresh").click(function () {
   //   $('#txtName').val(22);
  initTable();
});
//添加
$("#add").click(function () {
  //  location.href = "/sys/sys/EditUser?id=0";
  //  showMyWindow("仓库信息", "/Warehouse/Warehouse/Edit?id=0", 700, 800, true, false, false);
    location.href = "/Warehouse/Warehouse/Edit?id=0";
});
//删除
$("#delete").click(function () {
    var id = $('#hid').val();
    if (id != "0") {
        $.messager.confirm('提示', "确定要删除吗", function (r) {
            if (r) {
                $.ajax({
                    url: "/Warehouse/Warehouse/Delete?id=" + id,
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