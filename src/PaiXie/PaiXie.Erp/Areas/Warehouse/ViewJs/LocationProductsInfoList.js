//初始化
$(function () {
    initTable("", 1);

    //刷新
    $('#refresh,#showEmptyLocation').click(function () {
        BindSerarchLickEvent(1);
    });
    //刷新当前页
    $("#refreshCurrentPage").click(function () {
        var grid = $("#grid");
        var options = grid.datagrid("getPager").data("pagination").options;
        var currPageNumber = options.pageNumber;
        BindSerarchLickEvent(currPageNumber);
    });
    //添加库位
    $('#add').click(function () {
        showLocalWindow("添加库位", "/Warehouse/Location/EditChild?parentID=" + $("#hdnParentID").val() + "&id=0", 620, 250, true, false, false);
    });
    //导入
    $('#import').click(function () {
        var parentID = $("#hdnParentID").val();
        showLocalWindow("导入库位", "/Warehouse/Location/Import?parentID=" + parentID, 400, 180, true, false, false);
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
                if (rows[i].ZkNum > 0) ispronum = true;
            }
            if (ispronum) {
                $.MsgBox.Alert("提示", "库位中还有商品，不能删除！");
                return false;
            }
            $.messager.confirm('提示', "确认删除这 " + ids.length + " 个库位吗？", function (r) {
                if (r) {
                    $.ajax({
                        url: "/Warehouse/Location/DeleteChild?ids=" + ids.join(','),
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
    //导出
    $('#export').click(function () {
        //var rows = $("#grid").datagrid("getSelections");
        //if (rows.length > 0) {
        //    var ids = [];
        //    for (var i = 0; i < rows.length; i++) {
        //        ids.push(rows[i].ID);
        //    }
        //} else {
        //    rows = $("#grid").datagrid("getRows");
        //    var ids = [];
        //    for (var i = 0; i < rows.length; i++) {
        //        ids.push(rows[i].ID);
        //    }
        //}
        //$.ajax({
        //    url: "json-management-export.html?ids=" + ids.join(','),
        //    type: "GET",
        //    cache: false,
        //    success: function (r) {
        //        var map = $.parseJSON(r);
        //        if (map.result == 1) {
        //            parent.$.messager.alert("提示", "导出成功", "");
        //        } else {
        //            parent.$.messager.alert("提示", map.message, "error");
        //        }
        //    },
        //    error: function () {
        //        parent.$.messager.alert("提示", "导出失败！", "error");
        //    }
        //});
        //return false;
    });
    //搜索
    $('#btnSearch').click(function () {
        BindSerarchLickEvent(1);
    });
    //清空条件
    $('#btnReset').click(function () {
        window.location.reload(true);
    });
    //初始化数据
    $('#Brand').combobox({
        url: "/Warehouse/Products/GetWarehouseBrandJson",
        valueField: 'Value',
        textField: 'Text',
        onLoadSuccess: function (data) { //数据加载完毕事件
            $("#Brand").combobox('select', 0);
        }
    });
});


//加载列表
function initTable(queryData, pageNumber) {
    $('#grid').datagrid({
        url: '/Warehouse/LocationProducts/Search?parentID=' + $("#hdnParentID").val() + '&ram=' + Math.random(),
        height: '100%',
        width: '100%',
        nowrap: false,
        autoRowHeight: false,
        striped: true,
        collapsible: true,
        pagination: true,
        fitColumns: true,
        fit: true, //datagrid自适应宽度
        fitColumn: false, //列自适应宽度
        pageNumber: pageNumber,
        border: false,
        pageSize: 15,
        pageList: [15, 20, 30],
        rownumbers: false,
        remoteSort: false,
        queryParams: queryData,  //异步查询的参数
        onDblClickRow: function (rowIndex, rowData) {
            editChildLocation(rowData.ID);
        },
        onSelect: function (rowIndex, rowData) {
            showControl();
        },
        onUnselect: function (rowIndex, rowData) {
            showControl();
        },
        columns: [[
			{ field: 'ck', width: 52, checkbox: true },   //选择
			{ title: '', field: 'ID', hidden: true },
			{
			    title: '库位编码', field: 'Code', width: 200, align: 'center',
			    formatter: function (value, row) {
			        return '<a href="javascript:void(0);" onclick=\"showLocationProducts(' + row.ID + ')\">' + value + '</a>';
			    },
			    sortable: true
			},
			{ title: '库位名称', field: 'Name', width: 180, align: 'center', sortable: true },
			{ title: '商品名称', field: 'ProductsName', width: 300, align: 'center', sortable: true },
			{ title: '商品编码', field: 'ProductsCode', width: 150, align: 'center', sortable: true },
			{ title: '属性', field: 'Saleprop', width: 150, align: 'center', sortable: false },
			{ title: 'SKU码', field: 'ProductsSkuCode', width: 150, align: 'center', sortable: true },
			{ title: '数量', field: 'ZkNum', width: 100, align: 'center', sortable: false },
			{
			    title: '操作', field: 'Permit', width: 100, align: 'center',
			    formatter: function (value, row) {
			        return '<a href="javascript:void(0);" onclick=\'editChildLocation(' + row.ID + ')\'>编辑</a>';
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

//绑定搜索按钮的的点击事件
function BindSerarchLickEvent(pageNumber) {
    //得到用户输入的参数
    var queryData = {
        keyWordType: $("#keyWordType").combobox('getValue'),
        keyWord: $("#txtKeyWord").val(),
        categoryID: $("#Category").combotree('getValue'),
        brandID: $("#Brand").combobox('getValue'),
        showEmptyLocation: $("#showEmptyLocation").attr("checked") ? "1" : "0"
    }
    //将值传递给
    initTable(queryData, pageNumber);
}

function showLocationProducts(id) {
    location.href = "/Warehouse/Location/Show?id=" + id;
}
function editChildLocation(id) {
    showLocalWindow("编辑库位", "/Warehouse/Location/EditChild?parentID=" + $("#hdnParentID").val() + "&id=" + id, 620, 250, true, false, false);
}

//按钮显示控制
function showControl() {
    var rows = $("#grid").datagrid("getSelections");
    var ids = [];
    var ispronum = false;
    for (var i = 0; i < rows.length; i++) {
        ids.push(rows[i].ID);
        if (rows[i].ZkNum > 0) ispronum = true;
    }
    if (ids.length <= 0 || ispronum) {
        //禁用删除
        $('#del').addClass('unclick');
    } else {
        //启用删除
        $('#del').removeClass('unclick');
    }
}