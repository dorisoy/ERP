$(function () {
    initTable("", 1);
    //刷新
    $('#refresh').click(function () {
        bindSerarchLickEvent(1);
    });
    //刷新当前页
    $('#refreshCurrentPage').click(function () {
        var grid = $("#grid");
        var options = grid.datagrid("getPager").data("pagination").options;
        var currPageNumber = options.pageNumber;
        bindSerarchLickEvent(currPageNumber);
    });
    //搜索
    $('#btnSearch').click(function () {
        bindSerarchLickEvent(1);
    });
    //清空条件
    $('#btnReset').click(function () {
        window.location.reload(true);
    });
    //添加
    $('#add').click(function () {
        showLocalWindow("添加供应商", "/Purchase/Suppliers/Edit?id=0", 620, 460, true, false, false);
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
            $.messager.confirm('提示', "如果与供应商有进行采购，删除可能会影响数据，请谨慎操作！", function (r) {
                if (r) {
                    $.ajax({
                        url: "/Purchase/Suppliers/Delete?ids=" + ids.join(','),
                        type: "GET",
                        cache: false,
                        success: function (r) {
                            var map = $.parseJSON(r);
                            if (map.result == 1) {
                                $.MsgBox.Alert("提示", "删除成功！", 1000);
                                $('#refreshCurrentPage').click();
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
            $.MsgBox.Alert("提示", "请选择供应商！", 1000);
        }
    });
});

//绑定搜索按钮的的点击事件
function bindSerarchLickEvent(pageNumber) {
    //得到用户输入的参数
    var queryData = {
        keyWordType: $("#keyWordType").combobox('getValue'),
        keyWord: $("#txtKeyWord").val()
    }
    //将值传递给
    initTable(queryData, pageNumber);
}
//加载列表
function initTable(queryData, pageNumber) {
    $('#grid').datagrid({
        url: '/Purchase/Suppliers/Search?ram=' + Math.random(),
        height: $(document).height() * 0.8,
        width: function () { return document.body.clientWidth * 0.9 },
        nowrap: false,
        autoRowHeight: false,
        striped: true,
        collapsible: true,
        pagination: true,
        fitColumns: true,
        pageNumber: pageNumber,
        fit: true, //datagrid自适应宽度
        border: false,
        pageSize: 15,
        pageList: [15, 20, 30],
        rownumbers: false,
        remoteSort: false,
        idField: 'ID',
        queryParams: queryData,  //异步查询的参数
        onSelect: function (rowIndex, rowData) {
            showControl();
        },
        onUnselect: function (rowIndex, rowData) {
            showControl();
        },
        columns: [[
			{ field: 'ck', width: 52, checkbox: true },   //选择
			{ title: '主键ID', field: 'ID', hidden: true },
			{
			    title: '供应商名称', field: 'Name', width: 200, align: 'center',
			    formatter: function (value, row, index) {
			        return value + '<br/>（' + row.AliasName + '）';
			    },
			    sortable: false
			},
			{ title: '联系人', field: 'ContactPerson', width: 100, align: 'center', sortable: false },
			{ title: '电话', field: 'Tel', width: 150, align: 'center', sortable: false },
			{ title: '传真', field: 'Fax', width: 150, align: 'center', sortable: false },
			{ title: '邮件', field: 'Email', width: 120, align: 'center', sortable: false },
			{ title: '供应商品数', field: 'ProductsCount', width: 100, align: 'center', sortable: false },
			{
			    title: '操作', field: 'Permit', width: 150, align: 'center',
			    formatter: function (value, row, index) {
			        return ' <a href="javascript:void(0);" onclick=\'modify(' + row.ID + ')\'>修改</a> | <a href="javascript:void(0);" onclick=\'manage(' + row.ID + ')\'>管理</a>';
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

//控制按钮是否可用
function showControl() {
    var rows = $("#grid").datagrid("getSelections");
    var ids = [];
    for (var i = 0; i < rows.length; i++) {
        ids.push(rows[i].ID);
    }
    if (ids.length <= 0) {
        //禁用删除
        $("#del").addClass("unclick");
    } else {
        //启用删除
        $('#del').removeClass('unclick');
    }
}
//查看或管理
function manage(suppliersID) {
    location.href = "/Purchase/SuppliersItem/Index?suppliersID=" + suppliersID + "&ram=" + Math.random();
}
//修改
function modify(suppliersID) {
    showLocalWindow("供应商资料", "/Purchase/Suppliers/Edit?id=" + suppliersID, 620, 460, true, false, false);
}