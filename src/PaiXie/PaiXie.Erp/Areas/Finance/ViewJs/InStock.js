var gridID = "grid";
var status = 1;
//初始化
$(function () {
    initTable("", 1);
    //刷新
    $('#refresh').click(function () {
        bindSerarchLickEvent(1);
    });
    //刷新当前页
    $('#refreshCurrentPage').click(function () {
        var grid = $("#" + gridID);
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
    $('#tt').tabs({
        onSelect: function (title, index) {
            switch (title) {
                case "入库审核":
                    status = 1;
                    $("#status").val(status);
                    gridID = "grid";
                    bindSerarchLickEvent(1);
                    break;
                case "出库审核":
                    status = 2;
                    $("#status").val(status);
                    gridID = "gridout";
                    bindSerarchLickEvent(1);
                    break;
                case "盘点审核":
                    status = 3;
                    $("#status").val(status);
                    gridID = "gridpd";
                    bindSerarchLickEvent(1);
                    break;
            }
        }
    });
});

//绑定搜索按钮的的点击事件
function bindSerarchLickEvent(pageNumber) {
   
    //得到用户输入的参数
    var queryData = {
        status: $("#status").combobox('getValue'),
        txtbillno: $("#txtbillno").val(),
        startDate: $("#startDate").datebox('getValue'),
        endDate: $("#endDate").datebox('getValue')
    }
    //将值传递给
    initTable(queryData, pageNumber);
}

//入库单   加载列表
function initTable(queryData, pageNumber) {
    var msg = "入库";
    if (status == 1)
    { msg = "入库"; }
    else
        if (status == 2)
        { msg = "出库"; }

    $("#" + gridID).datagrid({
        url: '/Finance/FinancialAudit/search?type=' + gridID + '&ram=' + Math.random(),
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
            title: msg+'单号', field: 'Permit2', width: 120, align: 'center',
            formatter: function (value, row) {
                var html = '<a href="javascript:void(0);" >' + row.BillNo + '</a>';

                if (row.SourceNo == "" || row.SourceNo == null) { }
                else
                    html += '<br/>(' + row.SourceNo + ")";
                return html;
            }, sortable: true
        },
       { title: msg+'类型', field: 'BillTypeName', width: 80, align: 'center', sortable: true },
         { title: '备注', field: 'Remark', width: 100, align: 'center', sortable: true },
          { title: msg + '数量', field: 'OutInStockNum', width: 100, align: 'center', sortable: true },
             { title: '确认时间', field: 'ConfirmDate', width: 100, align: 'center', sortable: true },
                { title: '操作人员', field: 'CreUserName', width: 100, align: 'center', sortable: true },
                   { title: '状态', field: 'StatusName', width: 80, align: 'center', sortable: true },
        {
            title: '操作', field: 'Permit', width: 120, align: 'center',
            formatter: function (value, row) {
                var html = '';
                if (row.IsAuditPrice == 0) {
                    html = '<a href="javascript:void(0);" onclick=\'Gotodetail(' + row.ID + ',' + row.BillType + ');\'>核对入库价</a>';
                    html += ' <br/> <a href="javascript:void(0);" onclick=\'shenhe(' + row.ID + ');\' >审核完毕</a>';
                   
                }
                else {
                    html = '--';
                }
                return html;
            },
            sortable: true
        }
        ]]
    });
}



//审核
function shenhe(id) {
    $.messager.confirm('提示', "确认对该单据进行审核？", function (r) {
        if (r) {
            $.ajax({
                url: "/Finance/FinancialAudit/OutInStockshenhe?ram=" + Math.random() + "&id=" + id,
                type: "GET",
                cache: false,
                success: function (r) {
                    var map = $.parseJSON(r);
                    if (map.result == 1) {
                        $('#refreshCurrentPage').click();
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
//核对入库价
function Gotodetail(id, type) {
    if (type == 10 || type == 30) //入库明细
    {
        location.href = "/Finance/FinancialAudit/InStock?id=" + id;
    }
    else  //出库明细
    {
        location.href = "/Finance/FinancialAudit/OutStock?id=" + id;
    }
    
}

