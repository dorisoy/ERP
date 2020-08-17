//初始化
$(function () {
    loadtreegrid();
});
$("#btnClose").click(function () {
    parent.$('#win').window('close');
});
$("#btnSave").click(function () {

    var idList = "";
    $("input:checked").each(function () {
        var id = $(this).attr("id");
        id = id.replace('check_', '');
        idList += id + ',';
    })
    if (idList != "") {
        idList = idList.replace('check_', '');
    }


    if (idList != "") {

                $.ajax({
                    url: "/Warehouse/Warehouse/Setwarea?wid=" + $("#wid").val() + "&mids=" + idList,
                    type: "GET",
                    cache: false,
                    success: function (r) {
                        var map = $.parseJSON(r);
                        if (map.result == 1) {
                            parent.$('#win').window('close');
                        }
                        else //showMsg("提示", "设置失败！", true);
                            $.MsgBox.Alert("提示", "设置失败！", 1000);
                    },
                    error: function () {
                       // showMsg("提示", "设置失败！", true);
                        $.MsgBox.Alert("提示", "设置失败！", 1000);
                    }
                });


    }
    else {
     //   $.messager.alert("提示", "请选择", "error");
        $.MsgBox.Alert("提示", "请选择！", 1000);
    }
});

function loadtreegrid() {
    $('#test').treegrid({
        url: "/Warehouse/Warehouse/Gettreegrid?wid=" + $('#wid').val(),
        idField: 'id',
        treeField: 'text',
        animate: "true",
        rownumbers: false,
        pagination: false,
        height: $(document).height() * 0.75,
        width: function () { return document.body.clientWidth * 0.9 },
        columns: [[
            {
                //" + (row.checked == 1 ? 'checked' : '') + " 
                title: '地区列表', field: 'text', formatter: function (value, rowData, rowIndex) {
                    return "<input  " + (rowData.attributes.attr == 1 ? 'checked' : '') + "   onclick=\"set_power_status('check_" + rowData.id + "');\" type=\"checkbox\" id=\"check_" + rowData.id + "\">" + " " + rowData.text;
                }, width: 180
            }
         
        ]]
    });
    
}

function set_power_status(id) {

    show(id.replace('check_', ''));



}

function show(checkid) {
    var s = '#check_' + checkid;
    //  alert( $(s).attr("id"));
    //   alert($(s)[0].checked);
    /*选子节点*/
    var nodes = $("#test").treegrid("getChildren", checkid);
    for (i = 0; i < nodes.length; i++) {
        $(('#check_' + nodes[i].id))[0].checked = $(s)[0].checked;

    }
    //选上级节点
    if (!$(s)[0].checked) {
        var parent = $("#test").treegrid("getParent", checkid);
        $(('#check_' + parent.id))[0].checked = false;
        while (parent) {
            parent = $("#test").treegrid("getParent", parent.id);
            $(('#check_' + parent.id))[0].checked = false;
        }
    } else {
        var parent = $("#test").treegrid("getParent", checkid);
        var flag = true;
        var sons = parent.sondata.split(',');
        for (j = 0; j < sons.length; j++) {
            if (!$(('#check_' + sons[j]))[0].checked) {
                flag = false;
                break;
            }
        }
        if (flag)
            $(('#check_' + parent.id))[0].checked = true;
        while (flag) {
            parent = $("#test").treegrid("getParent", parent.id);
            if (parent) {
                sons = parent.sondata.split(',');
                for (j = 0; j < sons.length; j++) {
                    if (!$(('#check_' + sons[j]))[0].checked) {
                        flag = false;
                        break;
                    }
                }
            }
            if (flag)
                $(('#check_' + parent.id))[0].checked = true;
        }
    }
}
