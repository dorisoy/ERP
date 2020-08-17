//初始化
$(function () {
    initTable(1);
    //刷新
    $("#refresh").click(function () {
        initTable(1);
    });
    //刷新当前页
    $("#refreshCurrentPage").click(function () {
        var grid = $("#grid");
        var options = grid.treegrid("getPager").data("pagination").options;
        var currPageNumber = options.pageNumber;
        initTable(currPageNumber);
    });
    //添加
    $("#add").click(function () {
        location.href = "/Warehouse/Location/Edit?id=0";
    });
    //删除
    $("#del").click(function () {
        if ($(this).hasClass('unclick')) return false;
        var rows = $("#grid").datagrid("getSelections");
        var ispronum = false;
        if (rows.length > 0) {
            var ids = [];
            for (var i = 0; i < rows.length; i++) {
                ids.push(rows[i].ID);
                if (rows[i].ProductsNum > 0) ispronum = true;
            }
            if (ispronum) {
                $.MsgBox.Alert("提示", "库区中还有商品，不能删除！");
                return false;
            }
            $.messager.confirm('提示', "确认删除这 " + ids.length + " 个库区吗？", function (r) {
                if (r) {
                    $.ajax({
                        url: "/Warehouse/Location/Delete?ids=" + ids.join(','),
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
            $.MsgBox.Alert("提示", "请选择库区！");
        }
    });
});
//加载列表
function initTable(pageNumber) {
    $('#grid').datagrid({
        url: '/Warehouse/Location/Search?ram=' + Math.random(),
        height: $(document).height() * 0.8,
        width: function () { return document.body.clientWidth * 0.9 },
        nowrap: true,
        autoRowHeight: false,
        striped: true,
        collapsible: true,
        pagination: true,
        fitColumns: true,
        fit: true, //datagrid自适应宽度
        pageNumber: pageNumber,
        border: false,
        pageSize: 15,
        pageList: [15, 20, 30],
        rownumbers: false,
        remoteSort: false,
        idField: 'ID',
        onSelect: function (rowIndex, rowData) {
            showControl();
        },
        onUnselect: function (rowIndex, rowData) {
            showControl();
        },
        onDblClickRow: function (rowIndex, rowData) {
            editLocation(rowData.ID);
        },
        columns: [[
        { field: 'ck', width: 52, checkbox: true },   //选择
        { title: '主键', field: 'ID', hidden: true },
        { title: '库区名称', field: 'Name', width: 200, align: 'center', sortable: true },
        { title: '代码', field: 'Code', width: 120, align: 'center', sortable: false },
        { title: '类型', field: 'TypeName', width: 100, align: 'center', sortable: false },
        { title: '库位数量', field: 'LocationNum', width: 100, align: 'center', sortable: false },
        { title: '商品数量', field: 'ProductsNum', width: 100, align: 'center', sortable: false },
        { title: '操作', field: 'Permit', width: 100, align: 'center',
            formatter: function (value, row) {
                return '<a href="javascript:void(0);" onclick=\'showLocation(' + row.ID + ',"' + row.Name + '")\'>管理</a> | <a href="javascript:void(0);" onclick=\'editLocation(' + row.ID + ')\'>编辑</a>';
            },
            sortable: false
        }
        ]],
        onLoadSuccess: function (data) {
            //注册全选事件
            $('#grid').parent().find("div .datagrid-header-check").children("input[type='checkbox']").eq(0).click(function () {
                showControl();
            });
            $("#grid").datagrid('clearSelections');
            showControl();
            DataGridNoData(this);
        }
    });
}

function showLocation(id, title) {
    var src = "/Warehouse/LocationProducts/Index?parentID=" + id + "&ram=" + Math.random();
    //var re = $(this).attr("re");
    var mid = "LocationProducts" + id;
    //拼接一个Iframe标签
    var str = '  <iframe id="frmWorkArea' + mid + '" data-options="fit:true" frameborder="0" style="  width: 100%; height: 100%; overflow: hidden;" src="' + src + '"></iframe>   '
    //首先判断用户是否已经单击了此项，如果单击了直接获取焦点，负责打开
    var isExist = parent.$("#worktab").tabs('exists', title);
    if (!isExist) {
        parent.$('#worktab').tabs('add', { title: title, content: str, closable: true });
    }
    else {
        //如果存在则获取焦点
        parent.$("#worktab").tabs('select', title);
    }
}

function editLocation(id) {
    location.href = "/Warehouse/Location/Edit?id=" + id;
}

//按钮显示控制
function showControl() {
    var rows = $("#grid").datagrid("getSelections");
    var ids = [];
    var ispronum = false;
    for (var i = 0; i < rows.length; i++) {
        ids.push(rows[i].ID);
        if (rows[i].ProductsNum > 0) ispronum = true;
    }
    if (ids.length <= 0 || ispronum) {
        //禁用删除
        $('#del').addClass('unclick');
    } else {
        //启用删除
        $('#del').removeClass('unclick');
    }
}