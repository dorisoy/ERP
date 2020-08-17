//初始化
$(function () {
    initTable("", 1);
    bindOnlineShop();

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

    //自动生成设置
    $('#autogeneration').click(function () {
        showLocalWindow("自动生成设置", "/Order/Download/Autogeneration?shopID=" + $("#shop").combobox('getValue') + "&shopName=" + $("#shop").combobox('getText'), 600, 500, true, false, false);
    });

    //快递设置
    $('#expressSet').click(function () {
        showLocalWindow("快递设置", "/Order/Download/ExpressSet?shopID=" + $("#shop").combobox('getValue') + "&shopName=" + $("#shop").combobox('getText'), 500, 400, true, false, false);
    });

    //批量生成
    $('#batchGenerate').click(function () {
        generate();
    });

    //批量删除
    $('#batchDelete').click(function () {
        del();
    });

    //清空条件
    $('#btnReset').click(function () {
        window.location.reload(true);
    });

    //下载订单
    $('#btnDownload').click(function () {
        download();
    });
});

//加载列表
function initTable(queryData, pageNumber) {
    $('#grid').datagrid({
        url: '/Order/Download/Search?ram=' + Math.random(),
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
                title: '订单编号', field: 'OutOrderCode', width: 300, align: 'center',
                formatter: function (value, row, index) {
                    var html = value + '<br/><span style=\"color:#999;\">(' + row.ErpOrderCode + ')</font>';
                    return html;
                },
                sortable: true
            },
			{ title: '下单时间', field: 'Created', width: 150, align: 'center', sortable: true },
			{ title: '商品金额', field: 'ProductsAmount', width: 200, align: 'center', formatter: function (value, row) { return '￥' + value.toFixed(3) }, sortable: true },
			{ title: '运费', field: 'Freight', width: 120, align: 'center', formatter: function (value, row) { return '￥' + value.toFixed(3) }, sortable: true },
			{
			    title: '收件人', field: 'BuyName', width: 150, align: 'center',
			    formatter: function (value, row, index) {
			        var content = "";
			        if (row.BuyMtel == "") {
			            content += row.BuyTel;
			        }
			        else {
			            content += row.BuyMtel;
			        }
			        if (row.BuyProvince != "") {
			            content += "<br/>" + row.BuyAddr
			        }
			        else {
			            content += "<br/>" + row.BuyAddr
			        }
			        var html = '<div class="ReceiverTooltip" title="' + content + '">' + value + '</div>';
			        html += '<a href="javascript:void(0)" onclick=\'showReceiverInfo(' + row.ID + ')\'>[修改]</a>';
			        return html;
			    },
			    sortable: true
			},
            {
                title: '订单商品', field: 'IsProductAddFin', width: 150, align: 'center',
                formatter: function (value, row) {
                    if (value == 1) {
                        return '<div style="background:url(../../Content/images/ord_right.png) no-repeat left center;background-size:16px 16px;width:80px;margin-left: auto;margin-right: auto;"><a href="javascript:void(0)" style="padding-left:5px" onclick=\'showOrderProducts(' + row.ShopID + ',' + row.ID + ',' + row.OutOrderCode + ')\'>添加完成</a></div>';
                    } else {
                        return '<div style="background:url(../../Content/images/ord_wrong.png) no-repeat left center;background-size:16px 16px;width:80px;margin-left: auto;margin-right: auto;"><a href="javascript:void(0)" style="padding-left:5px" onclick=\'showOrderProducts(' + row.ShopID + ',' + row.ID + ',' + row.OutOrderCode + ')\'>添加错误</a></div>';
                    }
                },
                sortable: true
            },
            {
                title: '发货物流', field: 'LogisticsName', width: 200, align: 'center',
                formatter: function (value, row) {
                    var html = "";
                    if (value == "" || value == null) {
                        html += '<a href="javascript:void(0)" style="color:red;" onclick=\'showChangeExpress(' + row.ErpOrderCode + ',' + row.LogisticsID + ')\'>未匹配物流</a>';
                    }
                    else {
                        html += '<a href="javascript:void(0)" onclick=\'showChangeExpress(' + row.ErpOrderCode + ',' + row.LogisticsID + ')\'>' + value + '</a>';
                    }
                    return html;
                },
                sortable: true
            },
            { title: '货到付款', field: 'IsCod', width: 100, align: 'center', formatter: function (value, row) { return (value == 0) ? "否" : "是" }, sortable: true },
            { title: '需要发票', field: 'IsNeedInvoice', width: 100, align: 'center', formatter: function (value, row) { return (value == 0) ? "否" : "是" }, sortable: true },
            {
                title: '买家留言', field: 'BuyMessage', width: 100, align: 'center',
                formatter: function (value, row) {
                    if (value == "" || value == null) {
                        return "-";
                    }
                    else {
                        return value;
                    }
                },
                sortable: true
            },
            {
                title: '卖家备注', field: 'SellerRemark', width: 100, align: 'center',
                formatter: function (value, row) {
                    if (value == "" || value == null) {
                        return "-";
                    }
                    else {
                        return value;
                    }
                },
                sortable: true
            },
            {
                title: '操作', field: 'Permit', width: 250, align: 'center',
                formatter: function (value, row) {
                    return '<a href="javascript:void(0)" onclick=\'generate(' + row.ID + ')\'>生成</a> | <a href="javascript:void(0)" onclick=\'del(' + row.ID + ')\'>删除</a>';
                },
                sortable: true
            }
        ]],
        onLoadSuccess: function (data) {
            $(".ReceiverTooltip").tooltip(
                {
                    position: 'top'
                }
            );
            DataGridNoData(this);
        }
    });
}

//搜索
function search(pageNumber) {
    //得到用户输入的参数
    var queryData = {
        keyWordType: $("#keyWordType").combobox('getValue'),
        keyWord: $("#txtKeyWord").val(),
        isCod: $("#isCod").combobox('getValue'),
        isNeedInvoice: $("#isNeedInvoice").combobox('getValue'),
        isRemark: $("#isRemark").combobox('getValue'),
        shopID: $("#shop").combobox('getValue'),
    }
    //将值传递给
    initTable(queryData, pageNumber);
}

//绑定线上店铺
function bindOnlineShop() {
    $.post("/Order/Download/GetOnlineShopJson", {}, function (dt) {
        $("#shop").combobox({
            valueField: "Value",
            textField: "Text",
            data: eval(dt),
            onLoadSuccess: function () {
                $('#shop').combobox('select', eval(dt)[0].Value); //默认选中第一个  
            },
            onChange: function (n, o) {
                $('#btnSearch').click();
                if (n > 0) {
                    $('body').everyTime('1s', 'task', DownloadProgress);
                    showTaskMsg($("#shop").combobox('getValue'));
                }
            }
        });
    });
}

//下载订单
function download() {
    var shopID = $("#shop").combobox('getValue');
    var dateType = $("#dateType").combobox('getValue');
    var startDate = $("#txtStartDate").datebox("getValue");
    var endDate = $("#txtEndDate").datebox("getValue");
    $.ajax({
        url: "/Order/Download/DownOrder?shopID=" + shopID + "&dateType=" + dateType + "&startDate=" + startDate + "&endDate=" + endDate,
        type: "GET",
        cache: false,
        success: function (r) {
            $('#spanProcessBar').html("<img src='../../Content/images/loading.gif' width='16px' height='16px'/>");
            $('body').everyTime('1s', 'task', DownloadProgress);
        },
        error: function () {
            $.MsgBox.Alert("提示", "下载失败！");
        }
    });
}

//下载进度
function DownloadProgress() {
    var shopID = $("#shop").combobox('getValue');
    setTimeout(function () {
        $.ajax({
            type: "GET",
            async: false,
            cache: false,
            url: "/Order/Download/DownloadProgress?shopID=" + shopID,
            dataType: "json",
            success: function (data) {
                if (data.FinshCount >= data.TotalCount) {
                    if ($("#spanProcessBar").html() != "") {
                        $("#spanProcessBar").html('<div style="background:url(../../Content/images/ord_right.png) no-repeat left center;background-size:16px 16px;width:100px;margin-left: auto;margin-right: auto;"><font style="padding-left:20px">下载完成！</font></div>');
                    }
                    $("#btnDownload").removeAttr("disabled");
                    $('body').stopTime('task');
                    showTaskMsg(shopID);
                    setTimeout(function () { $("#spanProcessBar").html(""); }, 2000);
                }
                else {
                    $('#spanProcessBar').html("<img src='../../Content/images/loading.gif' width='16px' height='16px'/>&nbsp;" + data.FinshCount + "/" + data.TotalCount);
                }
            }
        });
    }, 1000);
}

//提示信息
function showTaskMsg(shopid) {
    $.ajax({
        url: "/Order/Download/ShowTaskMsg?shopid=" + shopid,
        type: "GET",
        cache: false,
        success: function (r) {
            var map = $.parseJSON(r);
            if (map.TaskStatus > 0) {
                $('#lblNum').html(map.FinshCount);
                $('#lblDate').html("更新时间：" + map.UpdateDate);
            }
        }
    });
}

//订单商品
function showOrderProducts(shopID, ordouterID, outOrderCode) {
    showLocalWindow("订单商品", "/Order/Download/OrderProducts?shopID=" + shopID + "&ordouterID=" + ordouterID + "&outOrderCode=" + outOrderCode, 850, 580, true, false, false);
    $('#localWin').window({
        onClose: function () {
            $('#refreshCurrentPage').click();
        }
    });
}

//发货物流
function showChangeExpress(erpOrderCode, logisticsID) {
    showLocalWindow("选择发货物流", "/Order/Download/ChangeExpress?erpOrderCode=" + erpOrderCode + "&logisticsID=" + logisticsID, 400, 300, true, false, false);
}

//收货人信息
function showReceiverInfo(ordouterID) {
    showLocalWindow("收货人信息", "/Order/Download/ReceiverInfo?ordouterID=" + ordouterID, 670, 470, true, false, false);
}

//生成订单
function generate(id) {
    if (id > 0) {
        var flag = false;
        $.ajax({
            url: "/Order/Download/GetRefundItemList?id=" + id,
            type: "GET",
            cache: false,
            async: false,
            success: function (r) {
                var map = $.parseJSON(r);
                if (map.length == 0) flag = true;
            }
        });

        if (!flag) {
            $.messager.confirm('提示', "确认生成这 " + ids.length + " 单？", function (r) {
                if (r) {
                    flag = r;
                }
            });
        }
        if (flag) {
            $.ajax({
                url: "/Order/Download/Generate?id=" + id,
                type: "GET",
                cache: false,
                success: function (r) {
                    var map = $.parseJSON(r);
                    if (map.result == 1) {
                        $.MsgBox.Alert("提示", "生成成功！", 1000);
                        $('#refreshCurrentPage').click();
                    } else {
                        $.MsgBox.Alert("提示", map.message);
                    }
                },
                error: function () {
                    $.MsgBox.Alert("提示", "生成失败！");
                }
            });
        }
    }
    else {
        var rows = $("#grid").datagrid("getSelections");
        if (rows.length > 0) {
            var ids = [];
            for (var i = 0; i < rows.length; i++) {
                ids.push(rows[i].ID);
            }
            if (ids.length > 0)
                $.messager.confirm('提示', "确认生成这 " + ids.length + " 单？", function (r) {
                    if (r) {
                        $.ajax({
                            url: "/Order/Download/BatchGenerate?ids=" + ids.join(','),
                            type: "GET",
                            cache: false,
                            success: function (r) {
                                var map = $.parseJSON(r);
                                if (map.result == 1) {
                                    $.MsgBox.Alert("提示", map.message);
                                    $('#refreshCurrentPage').click();
                                } else {
                                    $.MsgBox.Alert("提示", map.message);
                                }
                            },
                            error: function () {
                                $.MsgBox.Alert("提示", "生成失败！");
                            }
                        });
                    }
                });
        }
        else {
            $.MsgBox.Alert("提示", "请选择订单！");
        }
    }
}

//删除
function del(id) {
    if (id > 0) {
        $.ajax({
            url: "/Order/Download/Delete?id=" + id,
            type: "POST",
            cache: false,
            data: { id: id },
            success: function (r) {
                var map = $.parseJSON(r);
                if (map.result == 1) {
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
    else
    {
        var rows = $("#grid").datagrid("getSelections");
        if (rows.length > 0) {
            var ids = [];
            for (var i = 0; i < rows.length; i++) {
                ids.push(rows[i].ID);
            }
            if (ids.length > 0)
                $.messager.confirm('提示', "确认删除这 " + ids.length + " 单？", function (r) {
                    if (r) {
                        $.ajax({
                            url: "/Order/Download/BatchDelete?ids=" + ids.join(','),
                            type: "GET",
                            cache: false,
                            success: function (r) {
                                var map = $.parseJSON(r);
                                if (map.result == 1) {
                                    $.MsgBox.Alert("提示", map.message);
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
        else {
            $.MsgBox.Alert("提示", "请选择订单！");
        }
    }
}
