//初始化
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
    //添加
    $('#add').click(function () {
        showLocalWindow("添加移位单", "/Warehouse/MoveLocation/Add", 600, 285, true, false, false);
    });
    //搜索
    $('#btnSearch').click(function () {
        bindSerarchLickEvent(1);
    });
    //清空条件
    $('#btnReset').click(function () {
        window.location.reload(true);
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
        url: '/Warehouse/MoveLocation/Search?ram=' + Math.random(),
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
        sortName: 'ID',
        sortOrder: 'desc',
        queryParams: queryData,  //异步查询的参数
        remoteSort: false,
        idField: 'ID',
        columns: [[
			{ field: 'ck', width: 52, checkbox: true },   //选择
			{ title: '', field: 'ID', hidden: true },
			{
			    title: '移位单号', field: 'BillNo', width: 200, align: 'center',
			    formatter: function (value, row) {
			        return '<a href="javascript:void(0);" onclick=\'manage(' + row.ID + ')\'>' + value + '</a>';
			    },
			    sortable: true
			},
			{ title: '说明', field: 'Remark', width: 220, align: 'center',
			formatter: function (value, row, index) {
			    var html = '<div id="remarkDetail_' + index + '" class="easyui-panel easyui-tooltip" title="' + value + '">' + value + '</div>';
			    return html;
			}, sortable: true },
			{ title: '移位数量', field: 'Num', width: 150, align: 'center', sortable: true },
			{ title: '创建时间', field: 'CreateDate', width: 180, align: 'center', sortable: true },
			{ title: '创建人', field: 'CreUserName', width: 100, align: 'center', sortable: true },
			{ title: '状态', field: 'StatusName', width: 100, align: 'center', sortable: true },
			{
			    title: '操作', field: 'Permit', width: 150, align: 'center',
			    formatter: function (value, row, index) {
			        var html = '';
			        var htmlEnd = '';
			        var opearTxt = '';
			        if (row.Status == 0) {
			            opearTxt = '管理';
			            if (Number(row.Num) > 0) {
			                htmlEnd = ' | <a href="javascript:void(0);" onclick=\'Confirm(' + row.ID + ')\'>确认</a>';
			            }
			            htmlEnd += ' | <a href="javascript:void(0);" onclick=\'del(' + row.ID + ')\'>删除</a>';
			        } else {
			            opearTxt = '查看';
			        }
			        html = '<a href="javascript:void(0);" onclick=\'manage(' + row.ID + ')\'>' + opearTxt + '</a>' + htmlEnd;
			        return html;
			    },
			    sortable: false
			}
        ]],
        onLoadSuccess: function (data) {
            //提示说明
            $.each(data.rows, function (index, element) {
                $('#remarkDetail_' + index).tooltip({
                    position: 'top',
                    onShow: function () {
                        $(this).tooltip('tip').css({
                            backgroundColor: '#ffffea',
                            borderColor: '#fdcb99',
                            color: '#666666',
                            padding: '10px',
                            borderRadius: '0px'
                        });
                    }
                });
            });
            DataGridNoData(this);
        }
    });
}

//管理、查看
function manage(id) {
    location.href = "/Warehouse/MoveLocationItem/Index?moveLocationID=" + id;
}
//删除操作
function del(id) {
    $.messager.confirm('提示', "确认删除？", function (r) {
        if (r) {
            $.ajax({
                url: "/Warehouse/MoveLocation/Delete?ids=" + id,
                type: "GET",
                cache: false,
                success: function (r) {
                    var map = $.parseJSON(r);
                    if (map.result == 1) {
                        $('#refreshCurrentPage').click();
                    } else {
                        $.MsgBox.Alert("提示", map.message, 1000);
                    }
                },
                error: function () {
                    $.MsgBox.Alert("提示", "删除失败！", 1000);
                }
            });
        }
    });
}
//确认
function Confirm(id) {
    $.messager.confirm('提示', "确认后仓库库存会进行相应变化。是否确认？", function (r) {
        if (r) {
            $.ajax({
                url: "/Warehouse/MoveLocation/Confirm?id=" + id,
                type: "GET",
                cache: false,
                success: function (r) {
                    var map = $.parseJSON(r);
                    if (map.result == 1) {
                        $('#refreshCurrentPage').click();
                    } else {
                        $.MsgBox.Alert("提示", map.message);
                    }
                },
                error: function () {
                    $.MsgBox.Alert("提示", "确认失败！");
                }
            });
        }
    });
}

