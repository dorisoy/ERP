var str = '1,2,';
//初始化
$(function () {
    bindSerarchLickEvent(1);
    var aa = $("input[name='state']:checkbox");
    aa.bind("click", function () {
        if ($(this).attr("checked")) {
            str += $(this).val() + ","
        }
        else {
            str = str.replace($(this).val() + ",", "");
        }
    });
    //刷新当前页
    $('#refreshCurrentPage').click(function () {
        var grid = $("#grid");
        var options = grid.datagrid("getPager").data("pagination").options;
        var currPageNumber = options.pageNumber;
        bindSerarchLickEvent(currPageNumber);
    });
    //清空条件
    $('#btnReset').click(function () {
        window.location.reload(true);
    });
    //刷新
    $("#refresh").click(function () {
        bindSerarchLickEvent(1);
    });
    //添加
    $("#add").click(function () {
        showLocalWindow("添加出库单", "/Warehouse/WarehouseOutStock/Add?id=0", 550, 380, true, false, false);
    });
    //搜索
    $("#btnSerach").click(function () {
        bindSerarchLickEvent(1);
    });
});
//绑定搜索按钮的的点击事件
function bindSerarchLickEvent(pageNumber) {
    //得到用户输入的参数
    var queryData = {
        keyWordType: $("#keyWordType").combobox('getValue'),
        keyWord: $("#txtKeyWord").val(),
        state: str
    }
    //将值传递给
    initTable(queryData, pageNumber);
    return false;

}
//加载列表
function initTable(queryData, pageNumber) {
    $('#grid').datagrid({
        url: '/Warehouse/WarehouseOutStock/Search?ram=' + Math.random(),
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
        sortName: 'ID',
        sortOrder: 'desc',
        remoteSort: false,
        idField: 'ID',
        queryParams: queryData,  //异步查询的参数
        columns: [[
       // { field: 'ck', checkbox: true },   //选择
        { title: '主键', field: 'ID', width: 40, sortable: true, hidden: true },
        {
            title: '出库单号', field: 'Permit2', width: 120, align: 'center',
            formatter: function (value, row) {
                var html = '<a href="javascript:void(0);" onclick=\'manage("' + row.ID + '")\'>' + row.BillNo + '</a>';
                if (row.SourceNo == "" || row.SourceNo == null) {
                }
                else {
                    html += '<br/>(' + row.SourceNo + ")";
                }
                return html;
            }, sortable: true
        },
       { title: '出库类型', field: 'BillTypeName', width: 80, align: 'center', sortable: true },
       { title: '备注', field: 'Remark', width: 100, align: 'center', sortable: true },
       { title: '出库数量', field: 'OutInStockNum', width: 100, align: 'center', sortable: true },
       { title: '创建时间', field: 'CreateDate', width: 100, align: 'center', sortable: true },
       { title: '创建人', field: 'CreatePerson', width: 100, align: 'center', sortable: true },
       { title: '状态', field: 'StatusName', width: 80, align: 'center', sortable: true },
       {
           title: '操作', field: 'Permit', width: 120, align: 'center',
           formatter: function (value, row) {
               var html = '';
               if (row.Status == 1) {
                   html = '<a href="javascript:void(0);" onclick=\'manage("' + row.ID + '");\'>管理商品</a>';
                   if (Number(row.OutInStockNum) > 0) {
                       html += ' | <a href="javascript:void(0);" onclick=\'submit(' + row.ID + ');\' >提交</a>';
                   }
                   html += ' | <a href="javascript:void(0);" onclick=\'del("' + row.ID + '");\'>删除</a>';
               }
               else {
                   html = '<a href="javascript:void(0);" onclick=\'manage("' + row.ID + '");\'>查看</a>';
               }
               return html;
           },
           sortable: true
       }
        ]]
    });
}

//商品列表
function manage(id) {
    location.href = "/Warehouse/WarehouseOutStockProducts/Index?ram=" + Math.random() + "&outInStockID=" + id;
}

//删除
function del(id) {
        $.messager.confirm('提示', "确定要删除吗？", function (r) {
            if (r) {
                $.ajax({
                    url: "/Warehouse/WarehouseOutStock/DelOutStock?ram=" + Math.random() + "&ids=" + id,
                    type: "GET",
                    cache: false,
                    success: function (r) {
                        var map = $.parseJSON(r);
                        if (map.result == 1) {
                            $("#refreshCurrentPage").click();
                        }
                        else {
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
//提交
function submit(id) {
    $.messager.confirm('提示', "提交后仓库库存会进行相应变化。确认提交？", function (r) {
        if (r) {
            $.ajax({
                url: "/Warehouse/WarehouseOutStock/Submit?ram=" + Math.random() + "&id=" + id,
                type: "GET",
                cache: false,
                success: function (r) {
                    var map = $.parseJSON(r);
                    if (map.result == 1) {
                        $("#refreshCurrentPage").click();
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