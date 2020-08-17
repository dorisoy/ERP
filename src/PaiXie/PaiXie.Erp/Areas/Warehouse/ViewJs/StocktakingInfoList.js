//初始化
$(function () {
    initTable("", 1);

    //刷新
    $('#refresh').click(function () {
        search(1);
    });

    //添加
    $('#add').click(function () {
        showLocalWindow("添加盘点记录", "/Warehouse/Stocktaking/Add", 600, 380, true, false, false);
    });
});

//加载列表
function initTable(queryData, pageNumber) {
    $('#grid').datagrid({
        url: '/Warehouse/Stocktaking/Search?ram=' + Math.random(),
        height: '100%',
        width: '100%',
        nowrap: false,
        autoRowHeight: false,
        striped: true,
        collapsible: true,
        pagination: true,
        fitColumns: true,
        pageNumber: pageNumber,
        fit: true, //datagrid自适应宽度
        fitColumn: false, //列自适应宽度
        border: false,
        pageSize: 15,
        pageList: [15, 20, 30],
        rownumbers: false,
        remoteSort: false,
        columns: [[
			{ field: 'ck', width: 52, checkbox: true },   //选择
			{ title: '', field: 'ID', hidden: true },
			{
			    title: '盘点记录号', field: 'BillNo', width: 250, align: 'center',
			    formatter: function (value, row) {
			        if (row.Status == 0) {
			            return '<a href="javascript:void(0)" onclick=\'show(' + row.ID + ')\'>' + value + '</a>';
			        } else {
			            return '<a href="javascript:void(0)" onclick=\'showend(' + row.ID + ')\'>' + value + '</a>';
			        }
			    },
			    sortable: true
			},
			{ title: '盘点备注', field: 'Remark', width: 150, align: 'center', sortable: true },
			{ title: '盘点库区', field: 'LocationName', width: 150, align: 'center', sortable: true },
			{ title: '创建时间', field: 'CreateDate', width: 200, align: 'center', sortable: true },
			{ title: '确认时间', field: 'ConfirmDate', width: 200, align: 'center', sortable: true },
			{ title: '创建人', field: 'CreatePerson', width: 100, align: 'center', sortable: true },
			{
			    title: '状态', field: 'Status', width: 100, align: 'center',
			    formatter: function (value, row, index) {
			        if (row.Status == 0) {
			            return '未确认';
			        } else if (row.State == 10) {
			            return '待审核';
			        } else {
			            return '已审核';
			        }
			    },
			    sortable: true
			},
			{
			    title: '操作', field: 'Permit', width: 150, align: 'center',
			    formatter: function (value, row, index) {
			        if (row.Status == 0) {
			            return '<a href="javascript:void(0)" onclick=\'show(' + row.ID + ')\'>管理</a> | <a href="javascript:void(0)" onclick=\'submit(' + row.ID + ')\'>确认</a> | <a href="javascript:void(0)" onclick=\'del(' + row.ID + ')\'>删除</a>';
			        } else {
			            return '<a href="javascript:void(0)" onclick=\'showend(' + row.ID + ')\'>查看</a>';
			        }
			    },
			    sortable: false
			}
        ]]
    });
}

//搜索
function search(pageNumber) {
    var queryData = {};
    initTable(queryData, pageNumber);
}

//盘点商品列表
function show(stocktakingID) {
    location.href = "/Warehouse/StocktakingItem/Index?stocktakingID=" + stocktakingID;
}

//盘点商品列表（已确认）
function showend(stocktakingID) {
    location.href = "/Warehouse/StocktakingItem/Index?stocktakingID=" + stocktakingID;
}

//删除操作
function del(id) {
    parent.$.messager.confirm('提示', "确认删除？", function (r) {
        if (r) {
            $.ajax({
                url: "/Warehouse/Stocktaking/Delete?id=" + id,
                type: "GET",
                cache: false,
                success: function (r) {
                    var map = $.parseJSON(r);
                    if (map.result == 1) {
                        $.MsgBox.Alert("提示", map.message, 1000);
                        search(1);
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

//确认
function submit(id) {
    parent.$.messager.confirm('提示', "确认后仓库库存会进行相应变化。是否确认？", function (r) {
        if (r) {
            $.ajax({
                url: "/Warehouse/Stocktaking/Submit?id=" + id,
                type: "GET",
                cache: false,
                success: function (r) {
                    var map = $.parseJSON(r);
                    if (map.result == 1) {
                        parent.$.messager.alert("提示", "操作成功", "");
                        $('#refresh').click();
                    } else {
                        parent.$.messager.alert("提示", map.message, "error");
                    }
                },
                error: function () {
                    parent.$.messager.alert("提示", "确认失败！", "error");
                }
            });
        }
    });
}