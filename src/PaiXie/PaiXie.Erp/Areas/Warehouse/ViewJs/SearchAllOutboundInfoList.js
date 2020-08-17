//初始化
$(function () {
    $("#txtKeyWord").focus();
    $("#txtKeyWord").keyup(function (e) {
        var currKey = 0, e = e || event;
        currKey = e.keyCode || e.which || e.charCode;
        if (currKey == 13) {
            bindSerarchLickEvent(1);
        }
    });
    //清空条件
    $('#btnReset').click(function () {
        window.location.reload(true);
    });
    //搜索
    $('#btnSearch').click(function () {
        bindSerarchLickEvent(1);
    });
});

//绑定搜索按钮的的点击事件
function bindSerarchLickEvent(pageNumber) {
    //得到用户输入的参数
    var queryData = {
        keyWordType: $("#keyWordType").combobox('getValue'),
        keyWord: $("#txtKeyWord").val()
    }
    if (queryData.keyWord.trim() != "" && queryData.keyWordType.trim() != "") {
        //将值传递给
        initTable(queryData, pageNumber);
    } else {
        $.MsgBox.Alert("提示", "请输入关键字！");
    }
    return false;
}
//加载列表
function initTable(queryData, pageNumber) {
    $('#grid').datagrid({
        url: '/Warehouse/Outbound/SearchAll?ram=' + Math.random(),
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
        //{ field: 'ck', checkbox: true },   //选择
        {
            title: '主键', field: 'ID', width: 10,
            formatter: function (value, row, index) {
                var html = '<input type="hidden" id="hdnID_' + index + '" value="' + row.ID + '" />';
                html += '<input type="hidden" id="hdnDeliveryExpressID_' + index + '" value="' + row.DeliveryExpressID + '" />';
                return html;
            },
            sortable: true, hidden: true
        },
        {
            title: '出库单号', field: 'BillNo', width: 150, align: 'center',
            formatter: function (value, row) {
                var html = '<a href="javascript:void(0);" onclick=\'showOutboundDetail("' + value + '")\'>' + value + '</a>';
                html += '<br/><span style="color:#999;">(' + row.ShopName + ')</span>';
                return html;
            }, sortable: true
        },
        {
            title: '订单编号', field: 'ErpOrderCode', width: 120, align: 'center',
            formatter: function (value, row, index) {
                var html = value;
                html += '<br/><span style="color:#999;">(' + row.BuyName + ')</span>';
                if (row.IsApplyRefund == 1) {
                    html += '<br/><span style="color:#ff0000;">[申请退款]</span>';
                    html += '<input type="hidden" id="hdnIsApplyRefund_' + row.ID + '" value="' + row.IsApplyRefund + '" />';
                }
                return html;
            },
            sortable: true
        },
        { title: '商品数量', field: 'ProductsNum', width: 60, align: 'center', sortable: true },
        {
            title: '发货快递', field: 'ExpressName', width: 140, align: 'center',
            formatter: function (value, row) {
                var html = '';
                if (row.DeliveryExpressID > 0) {
                    html += row.DeliveryExpressName + "<br/>";
                }
                if (row.ExpressID != row.DeliveryExpressID) {
                    html += '<span style="color:#999;" title="下单选择快递">（' + row.ExpressName + '）</span>';
                }
                return html;
            },
            sortable: false
        },
        {
            title: '运单号', field: 'WaybillNo', width: 130, align: 'center',
            formatter: function (value, row, index) {
                var html = '<span id="spanWaybillNo_' + index + '">' + (value == null ? "" : value) + '</span>';
                html += '<div id="divWaybillNo_' + index + '" style="display:none;"><input type="text" id="txtWaybillNo_' + index + '" class="easyui-validatebox" data-options="validType:[\'code\',\'length[1,20]\'],height:30" value="' + value + '" style="width:120px; text-align:center;" /></div>';
                html += '<span id="spanErrorMsg" style="color:red;"></span>';
                return html;
            },
            sortable: true
        },
        {
            title: '货到付款', field: 'IsCod', width: 60, align: 'center',
            formatter: function (value, row) {
                return value == 1 ? "是" : "否";
            },
            sortable: false
        },
        {
            title: '需要发票', field: 'IsNeedInvoice', width: 60, align: 'center',
            formatter: function (value, row) {
                return value == 1 ? '<span style="color:#ff0000;">是</span>' : '否';
            },
            sortable: false
        },
        {
            title: '留言备注', field: 'messageRemark', width: 60, align: 'center',
            formatter: function (value, row, index) {
                var html = '';
                if (row.BuyMessage != '' || row.SellerRemark != '') {
                    var contentText = '';
                    if (row.BuyMessage != '') {
                        contentText += '买家留言：' + row.BuyMessage;
                    }
                    if (row.SellerRemark != '') {
                        if (contentText != '') {
                            contentText += '<br/>';
                        }
                        contentText += '卖家备注：' + row.SellerRemark;
                    }
                    html += '<div id="messageRemark_' + index + '" class="easyui-panel easyui-tooltip" title="' + contentText + '" style="color:#265cff;">有</div>';
                } else {
                    html += '<div>无</div>';
                }
                if (row.IsHang == 1) {
                    html += '<div id="hangRemark_' + index + '" class="easyui-panel easyui-tooltip" title="挂起备注：' + row.HangRemark + '" style="color:#ff0000;">[挂起]</div>';
                }
                return html;
            },
            sortable: false
        },
        { title: '出库单状态', field: 'StatusName', width: 80, align: 'center', sortable: true },
        {
            title: '操作', field: 'Permit', width: 140, align: 'center',
            formatter: function (value, row) {
                var html = '<a href="javascript:void(0);" onclick=\'showOutboundDetail("' + row.BillNo + '")\'>查看详情</a>';
                return html;
            },
            sortable: false
        }
        ]],
        onLoadSuccess: function (data) {
            $.each(data.rows, function (index, element) {
                //留言备注
                $('#messageRemark_' + index).tooltip({
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
                //挂起备注
                $('#hangRemark_' + index).tooltip({
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
            $("#grid").datagrid('clearSelections');
            DataGridNoData(this);
        }
    });
}