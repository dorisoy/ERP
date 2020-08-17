//初始化
$(function () {
    initTable();
});
//加载列表
function initTable() {
    var queryData = {
        IsEnable: 1
    }
    $('#grid').datagrid({
        url: '/sys/role/search?ram=' + Math.random()+'&UCode=' + $('#UCode').val(),     
        nowrap: true,
        autoRowHeight: false,
        striped: true,
        collapsible: true,
        pagination: true,
        fitColumns: true,
        fit: true, //datagrid自适应宽度
        
        border: false,
        pageSize: 5,
        pageList: [5, 10, 15],
        rownumbers: false,
        remoteSort: false,
        idField: 'ID',
        queryParams: queryData,  //异步查询的参数
      
        columns: [[
     { field: 'ck', checkbox: true },   //选择
        { title: '主键', field: 'ID', width: 40, sortable: true, hidden: true },
        { title: '角色代码', field: 'Code', width: 100, sortable: true },
         { title: '名称', field: 'Name', width: 100, sortable: true }
        

        ]],
        onLoadSuccess: function (data) {
            var rowData = data.rows;
            $.each(rowData, function (index, value) {
                if (value.part1 =="1")
                        $('#grid').datagrid('selectRow', index);
            });
            DataGridNoData(this);
        }

    });
}

$("#btnClose").click(function () {
   
    parent.$('#localWin').window('close');
});

$("#btnSave").click(function () {
    var rows = $("#grid").datagrid("getSelections");
    if (rows.length > 0) {
    
                var ids = [];
                for (var i = 0; i < rows.length; i++) {
                    ids.push(rows[i].Code);
                }   
                $.ajax({
                    url: "/sys/role/SetUserRole?ucode=" + $("#UCode").val() + "&rids=" + ids.join(','),
                    type: "GET",
                    cache: false,
                    success: function (r) {
                        var map = $.parseJSON(r);
                        if (map.result == 1) {
                            $("#btnClose").click();
                        }
                        else {
                            $.MsgBox.Alert("提示", map.message);
                        }
                    },
                    error: function () {
                        $.MsgBox.Alert("提示", "设置失败！");
                    }
                });


    }
    else {
    
        $.MsgBox.Alert("提示", "请选择角色！");
    }
});