//初始化
$(function () {
    initTable("", 1);
    $('#refreshCurrentPage').click(function () {
        var currPageNumber = getCurrPageNumber("grid");
        bindSerarchLickEvent(currPageNumber);
    });
    $('#back').click(function () {
        var status = $("#hdnStatus").val();
        location.href = '/Products/Products/Index?status=' + status;
    });
    $('#import').click(function () {
        importProducts(undefined);
    });
    $('#btnDownProducts').click(function () {
        downProducts();
    });
    //删除
    $("#del").click(function () {
        if ($(this).hasClass('unclick')) return false;
        var rows = $("#grid").datagrid("getSelections");
        if (rows.length > 0) {
            var ids = [];
            for (var i = 0; i < rows.length; i++) {
                ids.push(rows[i].ID);
            }
            $.messager.confirm('提示', "确认删除这 " + ids.length + " 件商品？", function (r) {
                if (r) {
                    $.ajax({
                        url: "/Shop/ShopProducts/Delete?ids=" + ids.join(','),
                        type: "GET",
                        cache: false,
                        success: function (r) {
                            var map = $.parseJSON(r);
                            if (map.result == 1) {
                                $.MsgBox.Alert("提示", "删除成功！", 1000);
                                $("#refreshCurrentPage").click();
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
            $.MsgBox.Alert("提示", "请选择商品！", 1000);
        }
    });
    $('#shop').combobox({
        url: '/Base/GetDictJson?dictTypeName=Shop',
        valueField: 'Value',
        textField: 'Text',
        onLoadSuccess: function () { //数据加载完毕事件
            $("#shop").combobox('select', "0");
        },
        onChange: function (n, o) {
            $('#productsStatus').combobox({
                url: '/Shop/ShopProducts/ProductsStatusList?shopID=' + $('#shop').combobox("getValue"),
                method: "get",
                valueField: 'VALUE',
                textField: 'TEXT'
            });
            if (o != "") {
                bindSerarchLickEvent(1);
            }
            if (n > 0) {
                $('body').everyTime('1s', 'task', getProcess);
            }
        }
    });
    
    $('#productsStatus').combobox({
        onChange: function (n, o) {
            if (o != "") {
                bindSerarchLickEvent(1);
            }
        }
    });
});
//绑定搜索的事件
function bindSerarchLickEvent(pageNumber) {
    //得到用户输入的参数
    var queryData = {
        shopID: $("#shop").combobox('getValue'),
        productsStatus: $("#productsStatus").combobox('getValue')
    }
    //将值传递给
    initTable(queryData, pageNumber);
}
//加载列表
function initTable(queryData, pageNumber) {
    $('#grid').datagrid({
        url: '/Shop/ShopProducts/Search?ram=' + Math.random(),
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
        remoteSort: false,
        idField: 'ID',
        queryParams: queryData,  //异步查询的参数
        onSelect: function (rowIndex, rowData) {
            showControl();
        },
        onUnselect: function (rowIndex, rowData) {
            showControl();
        },
        onDblClickRow: function (rowIndex, rowData) {
            editBrand(rowData.ID);
        },
        columns: [[
        { field: 'ck', checkbox: true },   //选择
        { title: '主键', field: 'ID', width: 40, align: 'center', sortable: true, hidden: true },
        { title: '商品编码', field: 'OuterId', width: 220, align: 'center', sortable: false },
         { title: '图片', field: 'ImgUrl', width: 120, align: 'center', formatter: formatimg, sortable: false },
         { title: '商品名称', field: 'ProTitle', width: 240, align: 'center', sortable: false },
         { title: '来源店铺', field: 'ShopName', width: 140, align: 'center', sortable: false },
         { title: '销售价', field: 'Price', width: 140, align: 'center', formatter: formatPrice, sortable: false },
         { title: '下载时间', field: 'CreateDate', width: 150, align: 'center', sortable: false },
          { title: '销售状态', field: 'ProductsStatus', width: 120, align: 'center', formatter: formatStatus, sortable: false },
        {
            title: '操作', field: 'Permit', width: 155, align: 'center',
            formatter: function (value, row) {
                return row.ErrorMessage != "" ? '<font style="color:#ff0000;">导入失败！</font><br/><font style="color:#1d74e0;">' + row.ErrorMessage + '</font>' : '<a href="javascript:void(0);" onclick=\'importProducts(' + row.ID + ')\'>导入系统</a>';
            },
            sortable: false
        }


        ]],
        onLoadSuccess: function (data) {
            //注册全选事件
            $('#grid').parent().find("div .datagrid-header-check").children("input[type='checkbox']").eq(0).click(function () {
                showControl();
            });
            $("#grid").datagrid('clearSelections');
            showControl();
            DataGridNoData(this);
        }
    });
}
//导入系统
function importProducts(productsID) {
    var ids = [];
    if (undefined == productsID) {
        var rows = $("#grid").datagrid("getSelections");
        for (var i = 0; i < rows.length; i++) {
            ids.push(rows[i].ID);
        }

    } else {
        ids.push(productsID);
    }
    var shopID = $("#shop").combobox('getValue');
    var productsStatus = $("#productsStatus").combobox('getValue');
    $.ajax({
        url: "/Shop/ShopProducts/ImportProducts?shopID=" + shopID + "&productsStatus=" + productsStatus + "&ids=" + ids.join(','),
        type: "GET",
        cache: false,
        success: function (r) {
            var map = $.parseJSON(r);
            if (map.result == 1) {
                $.MsgBox.Alert("提示", "导入成功！", 1000);
            } else {
                $.MsgBox.Alert("提示", map.message, 100000);
            }
            $("#refreshCurrentPage").click();
        },
        error: function () {
            $.MsgBox.Alert("提示", "导入失败！");
        }
    });
}

//下载商品
function downProducts() {
    var shopID = $("#shop").combobox('getValue');
    var productsStatus = $("#productsStatus").combobox('getValue');
    if (shopID > 0) {
        if (productsStatus > 0) {
            $("#btnDownProducts").attr("disabled", "disabled");
            $.ajax({
                url: "/Shop/ShopProducts/DownProducts?shopID=" + shopID + "&productsStatus=" + productsStatus,
                type: "GET",
                async: true,
                cache: false,
                success: function (r) {
                    $('body').everyTime('1s', 'task', getProcess);
                },
                error: function () {
                    $.MsgBox.Alert("提示", "下载失败！", 1000);
                }
            });
        } else {
            $.MsgBox.Alert("提示", "请选择商品类型！", 1000);

        }
    }else
    {
        $.MsgBox.Alert("提示", "请选择网店！", 1000);
    }
}

//获取下载进度
function getProcess() {
    var shopID = $("#shop").combobox('getValue');
    $.ajax({
        type: "GET",
        async: false,
        cache: false,
        url: "/Shop/ShopProducts/GetProcess?shopID=" + shopID,
        dataType: "json",
        success: function (data) {
            if (data.TotalCount > 0) {
                $('#spanProcessBar').html("<img src='../../Content/images/loading.gif' width='16px' height='16px'/>" + data.FinshCount + "/" + data.TotalCount);
                if (data.FinshCount >= data.TotalCount) {
                    $("#spanProcessBar").html("");
                    $("#btnDownProducts").removeAttr("disabled");
                    $('body').stopTime('task');
                    initTable("", 1);
                }
            } else {
                $('body').stopTime('task');
            }
        }
    });
}

//获取当前页数
function getCurrPageNumber(gridID) {
    var grid = $("#" + gridID);
    var options = grid.datagrid("getPager").data("pagination").options;
    var currPageNumber = options.pageNumber;
    return currPageNumber;
}

//按钮显示控制
function showControl() {
    var rows = $("#grid").datagrid("getSelections");
    if (rows.length <= 0) {
        //禁用删除
        $('#del').addClass('unclick');
    } else {
        //启用删除
        $('#del').removeClass('unclick');
    }
}

function formatPrice(val, row) {
    return '<span style="color:#ff0000;">' + val.toFixed(3) + '</span>';
}
function formatimg(val, row) {
    var firstSmallPic = "../../Upload/Products/NoImg.jpg";
    if (val != null && val != '') {
        firstSmallPic = val.split(',')[0];
    }
    return '<div class="d48"><dfn></dfn><img src=' + firstSmallPic + ' width=\'120px\' height=\'120px\'></div>';
}
function formatStatus(val, row) {
    return val == "1" ? "上架" : "下架";
}