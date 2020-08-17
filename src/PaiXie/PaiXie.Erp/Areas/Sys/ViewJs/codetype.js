//初始化
$(function () {
    initTable();  //父类
    loadtreegrid();//子类
});
// 父类 搜索
$("#btnSave").click(function () {
    initTable();
});
//父类
function initTable() {
    $('#tree').tree({
        url: '/sys/dict/JsonTreeSyscodeType?name=' + $('#txtname').val() + '&ram=' + Math.random(),
        checkbox: false,
        onClick: function (node) {
            $('#codetype').val(node.id);
            $('#codetypename').val(node.text);
            $('#lblpname').text(node.text);
            $("#code").val("0");
            $("#test").treegrid("unselectAll");
            loadtreegrid();
        }
    });
}
$("#codeadd").click(function () {
    var id = $('#codetype').val();
    if (id != "") {
        showLocalWindow("字典", "/sys/dict/Code?ram=" + Math.random() + "&codetype=" + $('#codetype').val() + "&pcode=" + $('#code').val(), 700, 500, true, false, false);
    }
    else {
        $.MsgBox.Alert("提示", "未选择字典类别！", 1000);
    }
});
$("#codetypemanger").click(function () {
    showLocalWindow("字典类型", "/sys/dict/CodeType?codetype=&ram=" + Math.random(), 700, 500, true, false, false);
});
$("#codetypeedit").click(function () {
    var id = $('#codetype').val();
    if (id != "") {
        showLocalWindow("字典类型", "/sys/dict/CodeType?ram=" + Math.random() + "&codetype=" + $("#codetype").val(), 700, 500, true, false, false);
    }
    else {
        $.MsgBox.Alert("提示", "未选择！", 1000);
    }
});
//刷新
$("#refresh").click(function () {
    //  initTable();
    location.reload();


});
$("#refreshcode").click(function () {
    loadtreegrid();
});
//删除code
$("#codedelete").click(function () {



    var rows = $("#test").treegrid("getSelections");
    if (rows.length > 0) {
        var ids = [];
        for (var i = 0; i < rows.length; i++) {
            ids.push(rows[i].id);
        }
        // alert(ids.join(','));


        $.messager.confirm('确认', "确定要禁用/启用吗", function (r) {
            if (r) {
                $.ajax({
                    url: "/sys/dict/Deletecode?id=" + ids.join(','),
                    type: "GET",
                    cache: false,
                    success: function (r) {
                        var map = $.parseJSON(r);
                        if (map.result == 1) {
                            loadtreegrid();
                            $('#code').val("0");
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
    }
    else {
        $.MsgBox.Alert("提示", "未选择！", 1000);
    }



    return;


    //var id = $('#code').val();   
    //if (id != "0") {
    //    $.messager.confirm('确认', "确定要禁用/启用吗", function (r) {
    //        if (r) {
    //            $.ajax({
    //                url: "/sys/dict/Deletecode?id=" + id,
    //                type: "GET",
    //                cache: false,
    //                success: function (r) {
    //                    var map = $.parseJSON(r);
    //                    if (map.result == 1) {
    //                        loadtreegrid();
    //                        $('#code').val("0");
    //                    }
    //                    else {
    //                        $.MsgBox.Alert("提示", map.message);
    //                    }
    //                },
    //                error: function () {
    //                    $.MsgBox.Alert("提示", "操作失败！");
    //                }
    //            });
    //        }
    //    });
    //}
    //else {
    //    $.MsgBox.Alert("提示", "未选择！", 1000);
    //}
});
//删除
$("#delete").click(function () {
    var id = $('#codetype').val();
    if (id != "") {
        $.messager.confirm('确认', "确定要删除吗", function (r) {
            if (r) {
                $.ajax({
                    url: "/sys/dict/Delete?id=" + id,
                    type: "GET",
                    cache: false,
                    success: function (r) {
                        var map = $.parseJSON(r);
                        if (map.result == 1) {
                            initTable();
                            $('#codetype').val("");
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
        $.MsgBox.Alert("提示", "未选择！", 1000);
    }
});
function loadtreegrid() {
    $('#test').treegrid({
        url: "/sys/dict/Gettreegrid?ram=" + Math.random() + "&CodeType=" + $('#codetype').val(),
        height: $(document).height() * 0.9,
        width: '100%',// function () { return document.body.clientWidth * 0.8 },
        idField: 'id',
        treeField: 'text',
        animate: "true",
        singleSelect: false,
        rownumbers: false,
        pagination: true,
        onDblClickRow: function (node) {//单击事件  
            //  alert(node.id);
            //   showLocalWindow("字典", "/sys/dict/Code?code=" + node.id + "&type=" + $('#codetype').val() + "&pcode=" + $('#code').val(), 700, 500, true, false, false);
        },
        onSelect: function (node) {//单击事件  
            $('#code').val(node.id);
        },
        columns: [[
              { field: 'ck', checkbox: true, width: '20%', align: 'center' },   //选择
             { field: 'id', title: '代码', width: '20%', align: 'center' },
             { title: '项目值', field: 'text', width: '20%', align: 'center' },
          {
              title: '有效', field: 'Permit', width: '10%', align: 'center',
              formatter: function (value, row) { return '<img src="/Content/images/' + ((row.attributes.IsEnable).toString() == "1" ? "checkmark.gif" : "checknomark.gif") + '"/>'; },
              sortable: true
          },
           {
               title: '备注', field: 'Permit2', width: '20%', align: 'center',
               formatter: function (value, row)
               { return row.attributes.Description.toString(); }

           }
          ,

       {
           title: '操作', field: 'Permit3', width: '15%', align: 'center',
           formatter: function (value, row) {

               return '<a href="#" onclick=\'editcode("' + row.id + '")\'>编辑</a>';
           },
           sortable: false
       }


        ]]
    });
}



function editcode(id) {
    //   alert(id);
    showLocalWindow("字典", "/sys/dict/Code?code=" + id + "&type=" + $('#codetype').val() + "&pcode=" + $('#code').val(), 700, 500, true, false, false);



}
