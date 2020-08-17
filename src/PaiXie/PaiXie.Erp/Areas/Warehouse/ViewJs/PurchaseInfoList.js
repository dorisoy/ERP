$(function () {
    search(1);

    //初始化数据
    BindDictItem("Suppliers", "Suppliers");

    //选中特定值
    $("#Suppliers").combobox('setValue', '0');

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

//搜索
function search(pageNumber) {
    var strStatus = '';
    $("input[name='state']:checkbox").each(function () {
        if ($(this).attr("checked")) {
            strStatus += $(this).val() + ","
        }
    })
    strStatus = strStatus.substr(0, strStatus.length - 1);
    //得到用户输入的参数
    var queryData = {
        keyWordType: $("#keyWordType").combobox('getValue'),
        keyWord: $("#txtKeyWord").val(),
        suppliersID: $("#Suppliers").combobox('getValue'),
        status: strStatus
    }
    //将值传递给
    initTable(queryData, pageNumber);
}

//加载列表
function initTable(queryData, pageNumber) {
    $('#grid').datagrid({
        url: '/Warehouse/Purchase/Search?ram=' + Math.random(),
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
        columns: [[
			{ field: 'ck', width: 52, checkbox: true },   //选择
			{ title: '主键', field: 'ID', hidden: true },
			{
			    title: '采购单号', field: 'BillNo', width: 200, align: 'center',
			    formatter: function (value, row) {
			        var html = "";
			        html = '<a href="javascript:void(0);" onclick=\'manage("' + row.AliasName + '",' + row.ID + ')\'>' + value + '</a>';
			        if (row.PlanBillNo != null) {
			            html += '<br/><span style="color:#999;">(' + row.PlanBillNo + ')</span>';
			        }
			        return html;
			    },
			    sortable: true
			},
			{ title: '供应商名称', field: 'AliasName', width: 120, align: 'center', sortable: true },
			{ title: '采购数量', field: 'Num', width: 120, align: 'center', formatter: formatnum, sortable: true },
			{ title: '已入库数量', field: 'InStockNum', width: 120, align: 'center', formatter: formatnum, sortable: true },
			{ title: '入库单', field: 'InStockOrderCount', width: 100, align: 'center', sortable: true },
			{ title: '创建时间', field: 'CreateDate', width: 140, align: 'center', sortable: true },
			{ title: '创建人', field: 'CreatePerson', width: 120, align: 'center', sortable: true },
			{
			    title: '状态', field: 'Status', width: 100, align: 'center',
			    formatter: function (value, row, index) {
			        var status = "";
			        if (value == 10) {
			            status = '已确认';
			        }
			        else {
			            status = '已结束';
			        }
			        return status;
			    },
			    sortable: true
			},
			{
			    title: '操作', field: 'Permit', width: 150, align: 'center',
			    formatter: function (value, row, index) {
			        var html = "";
			        if (row.Status == 10) {
			            html = '<a href="javascript:void(0)" onclick=\'manage("' + row.AliasName + '",' + row.ID + ')\'>管理</a>';
			            if (row.InStockOrderCount != "0") {
			                html += ' | <a href="javascript:void(0)" onclick=\'end(' + row.ID + ')\'>结束</a>';
			            }
			        }
			        else {
			            html = '<a href="javascript:void(0)" onclick=\'manage("' + row.AliasName + '",' + row.ID + ')\'>查看</a>';
			        }
			        return html;
			    },
			    sortable: false
			}
        ]]
    });
}

function formatnum(val, row) {
    var str = String(val);
    var str = str.split('').reverse().join('').replace(/(\d{3})/g, '$1,').replace(/\,$/, '').split('').reverse().join('');
    return str;
}

//查看或管理
function manage(aliasName, purchaseID) {
    location.href = "/Warehouse/PurchaseItem/Index?aliasName=" + aliasName + "&purchaseID=" + purchaseID + "&ram=" + Math.random();
}

//结束操作
function end(purchaseID) {
    $.messager.confirm('提示', "结束后不能再从该采购单创建入入库单，确认结束吗？", function (r) {
        if (r) {
            $.ajax({
                url: "/Warehouse/Purchase/End?ids=" + purchaseID,
                type: "GET",
                cache: false,
                success: function (r) {
                    var map = $.parseJSON(r);
                    if (map.result == 1) {
                        $.MsgBox.Alert("提示", "操作成功！", 1000);
                        $('#refreshCurrentPage').click();
                    } else {
                        $.MsgBox.Alert("提示", map.message);
                    }
                },
                error: function () {
                    $.MsgBox.Alert("提示", "结束失败！");
                }
            });
        }
    });
}