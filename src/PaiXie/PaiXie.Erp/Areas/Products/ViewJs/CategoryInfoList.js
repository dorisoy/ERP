//初始化
$(function () {
    initTable(1);
    //刷新
    $("#refresh").click(function () {
        initTable(1);
    });
    //刷新当前页
    $('#refreshCurrentPage').click(function () {
        var grid = $("#grid");
        var options = grid.treegrid("getPager").data("pagination").options;
        var currPageNumber = options.pageNumber;
        initTable(currPageNumber);
    });
    //添加
    $("#add").click(function () {
        showLocalWindow("添加分类", "/Products/Category/Edit?id=0&parentID=0", 450, 270, true, false, false);

    });
    //删除
    $("#del").click(function(){
        if ($(this).hasClass('unclick')) return false;
        var ids = [];
        $("input[name='check']:checkbox").each(function () {
            if ($(this).attr("checked")) {
                ids.push($(this).val());
            }
        });
        if (ids.length > 0) {
            $.messager.confirm('提示', "确认删除所选分类？", function (r) {
                if (r) {
                    $.ajax({
                        url: "/Products/Category/Delete?ids=" + ids.join(','),
                        type: "GET",
                        cache: false,
                        success: function (r) {
                            var map = $.parseJSON(r);
                            if (map.result == 1) {
                                $.MsgBox.Alert("提示", "删除成功！", 1000);
                                $("#refreshCurrentPage").click();
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
        else {
            $.MsgBox.Alert("提示", "请选择分类！");
        }
    });
});
//加载列表
function initTable(pageNumber) {
    $('#grid').treegrid({
        url: '/Products/Category/Search?ram=' + Math.random(),
        height: '100%',
        width: '100%',
        idField: 'id',
        treeField: 'text',
        animate: true,
        rownumbers: false,
        pageNumber: pageNumber,
        pagination: true,
        onDblClickRow: function (node) {//双击事件  
            editCategory(node.id);
        },
        onSelect: function (node) {
            $("#check_" + node.id).attr("checked", true);
            set_power_status("check_" + node.id);
            showControl();
        },
        onUnselect: function (node) {
            $("#check_" + node.id).attr("checked", false);
            set_power_status("check_" + node.id);
            showControl();
        },
        columns: [[
            //{ field: 'ck', checkbox: true },   //选择
            { title: '主键', field: 'ID', width: 40, align: 'center', sortable: true, hidden: true },
            {
                title: '分类名称', field: 'text', formatter: function (value, rowData, rowIndex) {
                    return "<input type=\"checkbox\" id=\"check_" + rowData.id + "\" name=\"check\" value=\"" + rowData.id + "\">" + " " + rowData.text;
                }, width: '60%'
            },
            {
                title: '操作', field: 'Permit', width: '40%', align: 'center',
                formatter: function (value, row) {

                    return '<a href="#" onclick=\'addSubCategory(' + row.id + ')\'>添加子类</a>&nbsp;|&nbsp;<a href="#" onclick=\'editCategory(' + row.id + ')\'>编辑</a>';
                },
                sortable: false
            }
        ]],
        onLoadSuccess: function (data) {
            $("#grid").treegrid('clearSelections');
            showControl();
        }
    });
}
function editCategory(id) {
    showLocalWindow("编辑分类", "/Products/Category/Edit?id=" + id + "&parentID=0", 450, 270, true, false, false);
}

function addSubCategory(parentID) {
    showLocalWindow("添加子类", "/Products/Category/Edit?id=0&parentID=" + parentID, 450, 270, true, false, false);
}

//按钮显示控制
function showControl() {
    var ids = [];
    $("input[name='check']:checkbox").each(function () {
        if ($(this).attr("checked")) {
            ids.push($(this).val());
        }
    });
    if (ids.length <= 0) {
        //禁用删除
        $('#del').addClass('unclick');
    } else {
        //启用删除
        $('#del').removeClass('unclick');
    }
}

function set_power_status(id) {

        show(id.replace('check_', ''));



    }

function show(checkid) {
        try {
            var s = '#check_' + checkid;
            //  alert( $(s).attr("id"));
            //   alert($(s)[0].checked);
            /*选子节点*/
            var nodes = $("#grid").treegrid("getChildren", checkid);
            for (i = 0; i < nodes.length; i++) {
                $(('#check_' + nodes[i].id))[0].checked = $(s)[0].checked;

            }
            //选上级节点
            if (!$(s)[0].checked) {
                var parent = $("#grid").treegrid("getParent", checkid);
                $(('#check_' + parent.id))[0].checked = false;
                while (parent) {
                    parent = $("#grid").treegrid("getParent", parent.id);
                    $(('#check_' + parent.id))[0].checked = false;
                }
            } else {
                var parent = $("#grid").treegrid("getParent", checkid);
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
                    parent = $("#grid").treegrid("getParent", parent.id);
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
        } catch (e) {

        }
    }