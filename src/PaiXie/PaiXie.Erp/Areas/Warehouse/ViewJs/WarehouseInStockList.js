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
    //刷新
    $("#refresh").click(function () {
        bindSerarchLickEvent(1);
    });
    //清空条件
    $('#btnReset').click(function () {
        window.location.reload(true);
    });
    //添加
    $("#add").click(function () {
        showLocalWindow("添加入库单", "/Warehouse/WarehouseInStock/AddInStock?id=0&ram=" + Math.random(), 550, 380, true, false, false);
    });
    $("#btnSerach").click(function () {
        bindSerarchLickEvent(1);
        return false;

    });
});
//加载列表
function initTable(queryData, pageNumber) {
    $('#grid').datagrid({
        url: '/Warehouse/WarehouseInStock/search?ram=' + Math.random(),
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
            title: '入库单号', field: 'Permit2', width: 120, align: 'center',
            formatter: function (value, row) {
                var html = '<a href="javascript:void(0);" onclick=\'GotoPro("' + row.BillNo + '",' + row.ID + ')\'>' + row.BillNo + '</a>';
               
                if (row.SourceNo == "" || row.SourceNo == null) { }
                else
                    html += '<br/>(' + row.SourceNo + ")";
                return html;
            }, sortable: true
        },
       { title: '入库类型', field: 'BillTypeName', width: 80, align: 'center', sortable: true },
         { title: '备注', field: 'Remark', width: 100, align: 'center', sortable: true },
          { title: '入库数量', field: 'OutInStockNum', width: 100, align: 'center', sortable: true },
             { title: '创建时间', field: 'CreateDate', width: 100, align: 'center', sortable: true },
                { title: '创建人', field: 'CreatePerson', width: 100, align: 'center', sortable: true },
                   { title: '状态', field: 'StatusName', width: 80, align: 'center', sortable: true },
        {
            title: '操作', field: 'Permit', width: 120, align: 'center',
            formatter: function (value, row) {
                var html = '';
                if (row.Status == 1) {
                    html = '<a href="javascript:void(0);" onclick=\'GotoPro("' + row.BillNo + '",' + row.ID + ');\'>管理商品</a>';
                    html += ' | <a href="javascript:void(0);" onclick=\'tj(' + row.ID + ');\' >提交</a>';
                    html += ' | <a href="javascript:void(0);" onclick=\'del("' + row.ID + '");\'>删除</a>';
                }
                else {
                    html = '<a href="javascript:void(0);" onclick=\'GotoPro("' + row.BillNo + '",' + row.ID + ');\'>查看</a>';
                }
                return html;
            },
            sortable: true
        }
        ]]
    });
}
//商品列表
function GotoPro(BillNo, id) {   
    location.href = "/Warehouse/WarehouseInStock/InStockProducts?ram=" + Math.random() + "&BillNo=" + BillNo + "&outInStockID=" + id;
}
//删除
function del(id) {
        $.messager.confirm('提示', "确定要删除吗", function (r) {
            if (r) {
                $.ajax({
                    url: "/Warehouse/WarehouseInStock/DelInStock?ram=" + Math.random() + "&ids=" + id,
                    type: "GET",
                    cache: false,
                    success: function (r) {
                        var map = $.parseJSON(r);
                        if (map.result == 1) {
                            var grid = $("#grid");
                            var options = grid.datagrid("getPager").data("pagination").options;
                            var currPageNumber = options.pageNumber;
                            BindSerarchLickEvent(currPageNumber);
                        }
                        else {
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
//提交
function tj(id) {
    $.messager.confirm('提示', "提交后仓库库存会进行相应变化。确认提交？", function (r) {
        if (r) {
            $.ajax({
                url: "/Warehouse/WarehouseInStock/tj?ram=" + Math.random()+"&id=" + id,
                type: "GET",
                cache: false,
                success: function (r) {
                    var map = $.parseJSON(r);
                    if (map.result == 1) {
                        var grid = $("#grid");
                        var options = grid.datagrid("getPager").data("pagination").options;
                        var currPageNumber = options.pageNumber;
                        bindSerarchLickEvent(currPageNumber);
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