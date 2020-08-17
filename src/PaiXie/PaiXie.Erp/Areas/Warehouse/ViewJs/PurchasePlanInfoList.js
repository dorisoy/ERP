//初始化
$(function () {
    search(1);

    //刷新
    $('#refresh').click(function () {
        search(1);
    });

    //刷新当前页
    $('#refreshCurrentPage').click(function () {
        var grid = $("#grid");
        var options = grid.datagrid("getPager").data("pagination").options;
        var currPageNumber = options.pageNumber;
        search(currPageNumber);
    });

    //搜索
    $('#btnSearch').click(function () {
        search(1);
    });

    //添加
    $('#add').click(function () {
        showLocalWindow("添加采购计划单", "/Warehouse/PurchasePlan/Add", 620, 250, true, false, false);
    });

    $('#btnReset').click(function () {
        window.location.reload(true);
    });

});

//加载列表
function initTable(queryData, pageNumber) {
    $('#grid').datagrid({
        url: '/Warehouse/PurchasePlan/Search?ram=' + Math.random(),
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
        queryParams: queryData,  //异步查询的参数
        onSelect: function (rowIndex, rowData) {
        },
        onUnselect: function (rowIndex, rowData) {
        },
        columns: [[
			{ field: 'ck', width: 50, checkbox: true },   //选择
			{ title: '', field: 'ID', hidden: true },
			{
			    title: '采购计划单号', field: 'BillNo', width: 250, align: 'center',
			    formatter: function (value, row) {
			        if (row.Status == 0) {
			            return '<a href="javascript:void(0)" onclick=\'manage(' + row.ID + ')\'>' + value + '</a>';
			        } else {
			            return '<a href="javascript:void(0)" onclick=\'showend(' + row.ID + ')\'>' + value + '</a>';
			        }
			    },
			    sortable: true
			},
			{ title: '计划单名称', field: 'Name', width: 250, align: 'center', sortable: true },
			{ title: '计划数量', field: 'Num', width: 100, align: 'center', formatter: formatnum, sortable: true },
			{ title: '创建时间', field: 'CreateDate', width: 200, align: 'center', sortable: true },
			{ title: '创建人', field: 'CreatePerson', width: 100, align: 'center', sortable: true },
			{
			    title: '状态', field: 'Status', width: 150, align: 'center',
			    formatter: function (value, row, index) {
			        if (row.Status == 0) {
			            return '未提交';
			        } else if (row.Status == 10) {
			            return '已提交';
			        } else {
			            return '已结束';
			        }
			    },
			    sortable: true
			},
			{
			    title: '操作', field: 'Permit', width: 150, align: 'center',
			    formatter: function (value, row, index) {
			        if (row.Status == 0) {
			            return '<a href="javascript:void(0)" onclick=\'manage(' + row.ID + ')\'>管理商品</a> | <a href="javascript:void(0)" onclick=\'Submit(' + row.ID + ')\'>提交</a> | <a href="javascript:void(0)" onclick=\'del(' + row.ID + ')\'>删除</a>';
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
    var str = '';
    $("input[name='status']:checkbox").each(function () {
        if ($(this).attr("checked")) {
            str += $(this).val() + ","
        }
    })
    str = str.substr(0, str.length - 1);
    //得到用户输入的参数
    var queryData = {
        keyWordType: $("#keyWordType").combobox('getValue'),
        keyWord: $("#txtKeyWord").val(),
        status: str
    }
    //将值传递给
    initTable(queryData, pageNumber);
}

function formatnum(val, row) {
    var str = String(val);
    var str = str.split('').reverse().join('').replace(/(\d{3})/g, '$1,').replace(/\,$/, '').split('').reverse().join('');
    return str;
}

//管理商品
function manage(planID) {
    location.href = "/Warehouse/PurchasePlanItem/Index?planID=" + planID;
}

//查看商品
function showend(planID) {
    location.href = "/Warehouse/PurchasePlanItem/Index?planID=" + planID;
}

//删除操作
function del(id) {
    parent.$.messager.confirm('提示', "确认删除？", function (r) {
        if (r) {
            $.ajax({
                url: "/Warehouse/PurchasePlan/Delete?id=" + id,
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

//提交操作
function Submit(id) {
    parent.$.messager.confirm('提示', "确认提交？", function (r) {
        if (r) {
            $.ajax({
                url: "/Warehouse/PurchasePlan/Submit?id=" + id,
                type: "GET",
                cache: false,
                success: function (r) {
                    var map = $.parseJSON(r);
                    if (map.result == 1) {
                        $.MsgBox.Alert("提示", map.message, 1000);
                        $('#refreshCurrentPage').click();
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
}