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
        var options = grid.datagrid("getPager").data("pagination").options;
        var currPageNumber = options.pageNumber;
        initTable(currPageNumber);
    });
    //添加
    $("#add").click(function () {
        showLocalWindow("品牌信息", "/Products/Brand/Edit?id=0", 550, 380, true, false, false);

    });
    //删除
    $("#del").click(function () {
        if ($(this).hasClass('unclick')) return false;
        var rows = $("#grid").datagrid("getSelections");
        if (rows.length > 0) {
            var ids = [];
            for (var i = 0; i < rows.length; i++) {
                ids.push(rows[i].ID);
            }
            $.messager.confirm('提示', "确认删除所选品牌？", function (r) {
                if (r) {
                    $.ajax({
                        url: "/Products/Brand/Delete?ids=" + ids.join(','),
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
            $.MsgBox.Alert("提示", "请选择品牌！");
        }
    });
});
//加载列表
function initTable(pageNumber) {
    $('#grid').datagrid({
        url: '/Products/Brand/Search?ram=' + Math.random(),
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
            editBrand(rowData.ID);
        },
        columns: [[
        { field: 'ck', checkbox: true },   //选择
        { title: '主键', field: 'ID', width: 40, align: 'center', sortable: true, hidden: true },
        { title: '品牌名称', field: 'Name', width: 240, align: 'center', sortable: false },
         { title: '代码', field: 'Code', width: 200, align: 'center', sortable: false },

          { title: '备注', field: 'Remark', width: 420, align: 'center', sortable: false },
        {
            title: '操作', field: 'Permit', width: 155, align: 'center',
            formatter: function (value, row) {

                return '<a href="#" onclick=\'editBrand(' + row.ID + ')\'>编辑</a>';
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

//按钮显示控制
function showControl() {
    var rows = $("#grid").datagrid("getSelections");
    if (rows.length <= 0) {
        //禁用删除
        $('#del').addClass('unclick');
    } else {
        //启用删除
        $('#del').removeClass('unclick');
    }
}


function editBrand(id) {
    showLocalWindow("品牌信息", "/Products/Brand/Edit?id=" + id, 550, 380, true, false, false);
}