//初始化
$(function () {
    initTable(1);
    //刷新
    $("#refresh").click(function () {
        initTable(1);
    });
});

$("#refreshCurrentPage").click(function () {
    initTable(1);
});

//加载列表
function initTable(pageNumber) {
    $('#grid').treegrid({
        url: '/Sys/SetArea/Search?ram=' + Math.random(),
        height: '100%',
        width: '100%',
        idField: 'id',
        treeField: 'text',
        animate: true,
        rownumbers: false,
        pageNumber: pageNumber,
        pagination: true,
        pageSize: 15,
        pageList: [15, 20, 30],
        columns: [[
            //{ field: 'ck', checkbox: true },   //选择
            { title: '主键', field: 'ID', width: 40, align: 'center', sortable: true, hidden: true },

             { title: '区域名称', field: 'text', width: '60%', align: 'left', sortable: true, hidden: false },


       
            {
                title: '操作', field: 'Permit', width: '40%', align: 'center',
                formatter: function (value, rowData) {
                    var html = "";
                  console.info(rowData);
                    if (rowData.attributes.attr == "0")
                    {
                        html +=  '<a href="javascript:void(0);" onclick=\'add(' + rowData.id + ',0)\' >添加市区<a> |';
                        html += '<a href="javascript:void(0);" onclick=\'edit(' + rowData.id + ',0)\'  >编辑<a> ';

                    }
                    else
                        if (rowData.attributes.attr == "1") {
                            html += '<a href="javascript:void(0);" onclick=\'add(' + rowData.id + ',1)\'  >添加区县<a> |';
                            html += '<a href="javascript:void(0);" onclick=\'edit(' + rowData.id + ',1)\' >编辑<a> |';
                            html += '<a href="javascript:void(0);" onclick=\'del(' + rowData.id + ',1)\'  >删除<a> ';

                        }
                        else if (rowData.attributes.attr == "2") {
                            html += '<a href="javascript:void(0);" onclick=\'edit(' + rowData.id + ',2)\'  >编辑<a> |';
                            html += '<a href="javascript:void(0);" onclick=\'del(' + rowData.id + ',2)\'  >删除<a> ';

                        }

                   


                    return html;
                },
                sortable: false
            }
        ]]
      
    });
}

function del(id, level) {

    $.messager.confirm('提示', "确认删除？", function (r) {
        if (r) {
            $.ajax({
                url: "/Sys/SetArea/Delete?id=" + id + "&level=" + level,
                type: "GET",
                cache: false,
                success: function (r) {
                    var map = $.parseJSON(r);
                    if (map.result == 1) {
                        $.MsgBox.Alert("提示", "删除成功！", 1000);
                        initTable(1);
                    } else {
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


function add(id, level) {
    showLocalWindow("添加区域", "/Sys/SetArea/Edit?id=" + id + "&level=-1", 450, 270, true, false, false);

}


function edit(id, level) {
    showLocalWindow("编辑区域", "/Sys/SetArea/Edit?id=" + id + "&level=" + level, 450, 270, true, false, false);

}


//恢复初始化设置
$("#add").click(function () {
 
    $.messager.confirm('提示', "确认恢复初始化设置？", function (r) {
            if (r) {
                $.ajax({
                    url: '/Sys/SetArea/initArea?ram=' + Math.random(),
                    type: "GET",
                    cache: false,
                    success: function (r) {
                        var map = $.parseJSON(r);
                        if (map.result == 1) {
                            $.MsgBox.Alert("提示", "操作成功！", 1000);
                            $("#refreshCurrentPage").click();
                        } else {
                            $.MsgBox.Alert("提示", map.message);
                        }
                    },
                    error: function () {
                        $.MsgBox.Alert("提示", "操作失败！");
                    }
                });
            }
        });
  
});