//初始化
$(function () {
    //绑定店铺下拉列表
    BindDictItem("shop", "Shop");
    $('#shop').combobox("setValue", '0');
    //绑定售后类型下拉列表
    $('#orderRefundType').combobox({
        url: '/OrderRefund/OrderRefund/GetOrderRefundTypeJson',
        valueField: 'Value',
        textField: 'Name',
        onLoadSuccess: function () { //数据加载完毕事件
            $(this).combobox('select', "-1");
        }
    });
    //绑定售后责任方下拉列表
    $('#orderRefundDuty').combobox({
        url: '/OrderRefund/OrderRefund/GetOrderRefundDutyJson',
        valueField: 'Value',
        textField: 'Name',
        onLoadSuccess: function () { //数据加载完毕事件
            $(this).combobox('select', "-1");
        }
    });
    bindSerarchLickEvent(1);
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
    //添加售后
    $('#add').click(function () {
        location.href = "/OrderRefund/OrderRefund/Add";
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
        shopID: $("#shop").combobox('getValue'),
        refundType: $("#orderRefundType").combobox('getValue'),
        duty: $("#orderRefundDuty").combobox('getValue'),
        statusStr: statusStr
    }
    //将值传递给
    initTable(queryData, pageNumber);
}

//加载列表
function initTable(queryData, pageNumber) {
    $('#grid').datagrid({
        url: '/OrderRefund/OrderRefund/Search?ram=' + Math.random(),
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
        queryParams: queryData,  //异步查询的参数
        remoteSort: false,
        idField: 'ID',
        columns: [[
			{ field: 'ck', width: 52, checkbox: true },   //选择
			{ title: '', field: 'ID', hidden: true },
			{
			    title: '售后单号', field: 'BillNo', width: 200, align: 'center',
			    formatter: function (value, row) {
			        return "<a href=\"javascript:void(0);\" onclick=\"showOrderRefundDetails('" + value + "')\">" + value + "</a>";
			    },
			    sortable: false
			},
			{ title: '订单号', field: 'ErpOrderCode', width: 220, align: 'center', sortable: false },
			{ title: '售后类型', field: 'RefundTypeName', width: 150, align: 'center', sortable: false },
            { title: '责任方', field: 'DutyName', width: 150, align: 'center', sortable: false },
            {
                title: '退金额', field: 'RefundAmount', width: 150, align: 'center',
                formatter: function (value, row) {
                    return value.toFixed(3);
                },
                sortable: false
            },
            {
                title: '退运费', field: 'RefundFreight', width: 150, align: 'center',
                formatter:function(value,row){
                    return value.toFixed(3);
                },
                sortable: false
            },
			{ title: '创建时间', field: 'CreateDate', width: 180, align: 'center', sortable: true },
			{
			    title: '状态', field: 'StatusName', width: 100, align: 'center',
			    formatter: function (value, row, index) {
			        var html = value;
			        if (row.Status == 30) {
			            html = "<lable style='color:red;'>" + value + "</lable>";
			        }
			        return html;
			    },
			    sortable: false
			},
			{
			    title: '操作', field: 'Permit', width: 150, align: 'center',
			    formatter: function (value, row, index) {
			        return "<a href=\"javascript:void(0);\" onclick=\"showOrderRefundDetails('" + row.BillNo + "')\">查看详情</a>";
			    },
			    sortable: false
			}
        ]],
        onLoadSuccess: function (data) {
            DataGridNoData(this);
        }
    });
}

//查看详情
function showOrderRefundDetails(billNo) {
    var title = '售后单' + billNo;
    var src = "/OrderRefund/OrderRefund/Details?billNo=" + billNo;
    //var re = $(this).attr("re");
    var mid = "OrderRefundDetails" + billNo;
    //拼接一个Iframe标签
    var str = '  <iframe id="frmWorkArea' + mid + '" data-options="fit:true" frameborder="0" style="  width: 100%; height: 100%; overflow: hidden;" src="' + src + '"></iframe>   ';
    //首先判断用户是否已经单击了此项，如果单击了直接获取焦点，负责打开
    var isExist = parent.$("#worktab").tabs('exists', title);
    if (!isExist) {
        parent.$('#worktab').tabs('add', { title: title, content: str, closable: true });
    }
    else {
        parent.$("#worktab").tabs('select', title);
        var targetTab = parent.$("#worktab").tabs("getSelected");
        parent.$('#worktab').tabs('update', {
            tab: targetTab,
            options: {
                content: str
            }
        });
    }
}