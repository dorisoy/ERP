//初始化
$(function () {
    initTable();
});
//加载列表
function initTable() {
    $('#grid').datagrid({
        url: '/sys/Whouse/search?ram='+ Math.random(),
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
        columns: [[
       // { field: 'ck', checkbox: true },   //选择
        { title: '主键', field: 'ID', width: 40, sortable: true, hidden: true },
  { title: '物流名称', field: 'Name', width: 120, sortable: true, align: 'center' },
        { title: '物流代码', field: 'Code', width: 120, sortable: true,align:'center' },
       
           { title: '官方地址', field: 'WebUrl', width: 250, sortable: true, align: 'center' },
              {
              title: '是否可用', field: 'IsEnable', width: 100,
              formatter: function (value, row) { return '<img src="/Content/images/' + ((value || '').toString() == "1" ? "checkmark.gif" : "checknomark.gif") + '"/>'; },
              sortable: true, align: 'center'
          },
          {
              title: '操作', field: 'Permit', width: 100, align: 'center',
              formatter: function (value, row) {
                  var html = '<a href="#" onclick=\'edit(' + row.ID + ')\'>编辑</a>';
                  if (row.IsEnable == 1)
                  {
                      html += ' | <a href="#" onclick=\'IsEnable(' + row.ID + ')\'>禁用</a>';
               
                  }
                  else
                  {
                      html += ' | <a href="#" onclick=\'IsEnable(' + row.ID + ')\'>启用</a>';

                  }
                  return html;
              },
              sortable: true
          }
        ]]
    });
}
function edit(id) {
    showLocalWindow("物流信息", "/sys/Whouse/Edit?id="+id, 470, 350, true, false, false);
}
//刷新
$("#refresh").click(function () {
  initTable();
});
//添加
$("#add").click(function () {
    showLocalWindow("物流信息", "/sys/Whouse/Edit?id=0", 470, 350, true, false, false);

});
//禁用  启用
function IsEnable(id) {
  
        $.messager.confirm('提示', "确定要禁用/启用吗", function (r) {
            if (r) {
                $.ajax({
                    url: "/sys/Whouse/Delete?id=" + id,
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

$("#btnSerach").click(function () {
    initTable();
});