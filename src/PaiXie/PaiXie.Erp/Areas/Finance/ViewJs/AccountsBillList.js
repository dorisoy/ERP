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

    //清空条件
    $('#btnReset').click(function () {
        window.location.reload(true);
    });
});

//绑定搜索按钮的的点击事件
function search(pageNumber) {
    var statusStr = "";
    $("input[name='status']:checked").each(function (index, element) {
        if (index == 0) {
            statusStr = $(element).val()
        } else {
            statusStr += "," + $(element).val();
        }
    });
    //得到用户输入的参数
    var queryData = {
        keyWordType: $("#keyWordType").combobox('getValue'),
        keyWord: $("#txtKeyWord").val(),
        paymentMethod: $("#paymentMethod").combotree('getValue'),
        billType: $("#billType").combobox('getValue'),
        dateType: $("#dateType").combobox('getValue'),
        startDate: $("#txtStartDate").datebox("getValue"),
        endDate:$("#txtEndDate").datebox("getValue"),
        status: statusStr
}
//将值传递给
initTable(queryData, pageNumber);
}

//加载列表
function initTable(queryData, pageNumber) {
    $('#grid').datagrid({
        url: '/Finance/AccountsBill/Search?ram=' + Math.random(),
        height: $(document).height() * 0.8,
        width: function () { return document.body.clientWidth * 0.9 },
        nowrap: false,
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
        queryParams: queryData,  //异步查询的参数
        columns: [[
        { field: 'ck', width: 52, checkbox: true },   //选择
        { title: '主键', field: 'ID', width: 40, align: 'center', sortable: true, hidden: true },
        { title: '单据号', field: 'BillNo', width: 200, align: 'center', sortable: false },
        {
            title: '类型', field: 'BillType', width: 80, align: 'center',
            formatter: function (value, row) {
                if (value == 150) {
                    return "收款";
                }
                else {
                    return "退款";
                }
            },
            sortable: false
        },
        { title: '创建时间', field: 'CreateDate', width: 100, align: 'center', sortable: false },
        {
            title: '付款方式', field: 'PaymentMethod', width: 100, align: 'center',
            formatter: function (value, row) {
                if (value == 0) {
                    return "在线支付";
                }
                else {
                    return "现金支付";
                }
            },
            sortable: false
        },
        { title: '付款金额(元)', field: 'Amount', width: 100, align: 'center', formatter: formatPrice, sortable: false },
        { title: '关联单号', field: 'AssociatedCode', width: 200, align: 'center', sortable: false },
        {
            title: '付款时间', field: 'PayDate', width: 100, align: 'center',
            formatter: function (value, row) {
                var html = '';
                if (value != "0001-01-01 00:00:00") {
                    html = value;
                } else {
                    html = '';
                }
                return html;
            },
            sortable: false
        },
        { title: '交易号', field: 'TradingNumber', width: 240, align: 'center', sortable: false },
        { title: '付款状态', field: 'StatusName', width: 100, align: 'center', sortable: false },
        {
            title: '操作', field: 'Permit', width: 155, align: 'center',
            formatter: function (value, row) {
                var html = '';
                if (row.Status == 0) {
                    html = '<a href="javascript:void(0);" onclick=\'EditPayInfo(\"' + row.BillNo + '\")\'>付款</a>';
                }
                else if (row.Status == 1) {
                    html = '<a href="javascript:void(0);" onclick=\'AuditPayInfo(\"' + row.BillNo + '\")\'>审核</a> | <a href="javascript:void(0);" onclick=\'EditPayInfo(\"' + row.BillNo + '\")\'>查看</a>';
                }
                else {
                    html = '<a href="javascript:void(0);" onclick=\'ShowPayInfo(\"' + row.BillNo + '\")\'>查看</a>';
                }
                return html;
            },
            sortable: false
        }
        ]],
        onLoadSuccess: function (data) {
            DataGridNoData(this);
        }
    });
}

function formatPrice(val, row) {
    return val.toFixed(3);
}

function ShowPayInfo(billNo) {
    showLocalWindow("付款信息", "/Finance/AccountsBill/ShowPayInfo?billNo=" + billNo, 500, 450, true, false, false);
}

function EditPayInfo(billNo) {
    showLocalWindow("付款", "/Finance/AccountsBill/EditPayInfo?billNo=" + billNo, 500, 520, true, false, false);
}

function AuditPayInfo(billNo) {
    $.ajax({
        url: "/Finance/AccountsBill/AuditPayInfo??billNo=" + billNo,
        type: "POST",
        cache: false,
        data: { billNo: billNo },
        success: function (r) {
            var map = $.parseJSON(r);
            if (map.result == 1) {
                $('#refreshCurrentPage').click();
            } else {
                $.MsgBox.Alert("提示", map.message);
            }
        },
        error: function () {
            $.MsgBox.Alert("提示", "审核失败！");
        }
    });
}