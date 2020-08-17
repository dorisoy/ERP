var gridID = "grid";
var status = $("#hdnStatus").val();
//初始化
$(function () {
    //初始化数据
    BindDictItem("Brand", "Brand");
    //选中特定值
    $("#Brand").combobox('setValue', '0');
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
        location.href = "/Products/Products/Index?status=" + status + "&ram=" + Math.random();
    });
    //添加商品
    $("#add").click(function () {
        location.href = "/Products/Products/Edit?id=0&status=" + status;
    });
    //下载商品
    $("#down").click(function () {
        location.href = "/Shop/ShopProducts/Down?status=" + status;
    });
    // 表格导入
    $("#import").click(function () {
        location.href = "/Products/Products/Import?status=" + status;
    });
    //更换分类
    $("#changeCategory").click(function () {
        if ($(this).hasClass('unclick')) return false;
        var rows = $("#" + gridID).datagrid("getSelections");
        if (rows.length > 0) {
            var ids = [];
            for (var i = 0; i < rows.length; i++) {
                ids.push(rows[i].ID);
            }
            showLocalWindow("更换分类", "/Products/Products/ChangeCategory?ids=" + ids.join(','), 500, 320, true, false, false);
        }
        else {
            $.MsgBox.Alert("提示", "请选择商品！");
        }
    });
    //更换品牌
    $("#changeBrand").click(function () {
        if ($(this).hasClass('unclick')) return false;
        var rows = $("#" + gridID).datagrid("getSelections");
        if (rows.length > 0) {
            var ids = [];
            for (var i = 0; i < rows.length; i++) {
                ids.push(rows[i].ID);
            }
            showLocalWindow("更换品牌", "/Products/Products/ChangeBrand?ids=" + ids.join(','), 500, 320, true, false, false);
        }
        else {
            $.MsgBox.Alert("提示", "请选择商品！");
        }
    });
    //导出
    $("#export").click(function () {
           
    });
    //上架
    $("#onSale").click(function () {
        if ($(this).hasClass('unclick')) return false;
        onSale();
    });
    //下架
    $("#offSale").click(function () {
        if ($(this).hasClass('unclick')) return false;
        offSale();
    });
    //删除
    $("#del").click(function () {
        if ($(this).hasClass('unclick')) return false;
        del();
    });

    $('#tt').tabs({
        onSelect: function (title, index) {
            switch (title) {
                case "销售中商品":
                    status = 1;
                    gridID = "grid";
                    bindSerarchLickEvent(1);
                    break;
                case "仓库中商品":
                    status = 2;
                    gridID = "gridOff";
                    bindSerarchLickEvent(1);
                    break;
            }
            $("#hdnStatus").val(status);
        }
    });
    if (status == 1) {
        bindSerarchLickEvent(1);
    } else {
        //选中仓库中商品选项卡
        $("#tt").tabs("select", 1);
    }
});

//上架
function onSale() {
    var rows = $("#" + gridID).datagrid("getSelections");
    if (rows.length > 0) {
        var ids = [];
        for (var i = 0; i < rows.length; i++) {
            ids.push(rows[i].ID);
        }
        $.messager.confirm('提示', "确认上架这 " + ids.length + " 件商品？", function (r) {
            if (r) {
                $.ajax({
                    url: "/Products/Products/OnSale?ids=" + ids.join(','),
                    type: "GET",
                    cache: false,
                    success: function (r) {
                        var map = $.parseJSON(r);
                        if (map.result == 1) {
                            $.MsgBox.Alert("提示", "上架成功！", 1000);
                            $('#refreshCurrentPage').click();
                        } else {
                            $.MsgBox.Alert("提示", map.message);
                        }
                    },
                    error: function () {
                        $.MsgBox.Alert("提示", "上架失败！");
                    }
                });
            }
        });
    }
    else {
        $.MsgBox.Alert("提示", "请选择商品！");
    }
}

//下架
function offSale() {
    var rows = $("#" + gridID).datagrid("getSelections");
    if (rows.length > 0) {
        var ids = [];
        for (var i = 0; i < rows.length; i++) {
            ids.push(rows[i].ID);
        }
        $.messager.confirm('提示', "管理端商品下架后，仓库端的对应商品也会下架，确认下架这 " + ids.length + " 件商品？", function (r) {
            if (r) {
                $.ajax({
                    url: "/Products/Products/OffSale?ids=" + ids.join(','),
                    type: "GET",
                    cache: false,
                    success: function (r) {
                        var map = $.parseJSON(r);
                        if (map.result == 1) {
                            $.MsgBox.Alert("提示", "下架成功！", 1000);
                            $('#refreshCurrentPage').click();
                        } else {
                            $.MsgBox.Alert("提示", map.message);
                        }
                    },
                    error: function () {
                        $.MsgBox.Alert("提示", "下架失败！");
                    }
                });
            }
        });
    }
    else {
        $.MsgBox.Alert("提示", "请选择商品！");
    }
}

//删除
function del() {
    var rows = $("#" + gridID).datagrid("getSelections");
    if (rows.length > 0) {
        var ids = [];
        for (var i = 0; i < rows.length; i++) {
            ids.push(rows[i].ID);
        }
        $.messager.confirm('提示', "确认删除这 " + ids.length + " 件商品？", function (r) {
            if (r) {
                $.ajax({
                    url: "/Products/Products/Delete?ids=" + ids.join(','),
                    type: "GET",
                    cache: false,
                    success: function (r) {
                        var map = $.parseJSON(r);
                        if (map.result == 1) {
                            $.MsgBox.Alert("提示", "删除成功！", 1000);
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
        $.MsgBox.Alert("提示", "请选择商品！");
    }
}

//绑定搜索按钮的的点击事件
function bindSerarchLickEvent(pageNumber) {
    //得到用户输入的参数
    var queryData = {
        keyWordType: $("#keyWordType").combobox('getValue'),
        keyWord: $("#txtKeyWord").val(),
        categoryID: $("#Category").combotree('getValue'),
        brandID: $("#Brand").combobox('getValue'),
        status: status
    }
    //将值传递给
    initTable(queryData, pageNumber);
}

//加载列表
function initTable(queryData, pageNumber) {
    $('#' + gridID).datagrid({
        url: '/Products/Products/Search?ram=' + Math.random(),
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
            editProducts(rowData.ID);
        },
        columns: [[
        { field: 'ck', width: 52, checkbox: true },   //选择
        { title: '主键', field: 'ID', width: 40, align: 'center', sortable: true, hidden: true },
        { title: '商品编码', field: 'Code', width: 120, align: 'center', sortable: false },
         {
             title: '图片', field: 'SmallPic', width: 110, align: 'center', formatter: formatimg, sortable: false
         },

          { title: '商品名称', field: 'Name', width: 200, align: 'center', sortable: false },
          { title: '分类', field: 'CategoryName', width: 100, align: 'center', sortable: false },
          { title: '品牌', field: 'BrandName', width: 100, align: 'center', sortable: false },
          { title: '销售价', field: 'SellingPrice', width: 100, formatter: formatsale, align: 'center', sortable: false },
          { title: '成本价', field: 'CostPrice', width: 100, formatter: formatcost, align: 'center', sortable: false },
          { title: '库存数量', field: 'Num', width: 100, align: 'center',
              formatter: function (value, row) {
                  return (value < 0 ? "<lable style='color:#ff0000;' title='下单后仓库库存变动导致超卖'>" + value + "</lable>" : value) + '<br/><a href="javascript:void(0);" onclick=\'showProducts(' + row.ID + ')\'>查看</a>';
              }, sortable: false
          },
          { title: '创建时间', field: 'CreateDate', width: 150, align: 'center', sortable: true },
        {
            title: '操作', field: 'Permit', width: 155, align: 'center',
            formatter: function (value, row) {
                var html = '<a href="javascript:void(0);" onclick=\'editProducts(' + row.ID + ')\'>编辑</a>';
                var htmlEnd = '';
                if (row.SaleType == 1) {
                    htmlEnd = ' | <a href="javascript:void(0);" onclick=\'setProductsMaterialMap(' + row.ID + ')\'>关联物料</a>';
                }
                return html + htmlEnd;
            },
            sortable: false
        }


        ]],
        onLoadSuccess: function (data) {
            //注册全选事件
            $('#' + gridID).parent().find("div .datagrid-header-check").children("input[type='checkbox']").eq(0).click(function () {
                showControl();
            });
            $("#" + gridID).datagrid('clearSelections');
            showControl();
            DataGridNoData(this);
        }
    });
}

function editProducts(id) {
    location.href = "/Products/Products/Edit?id=" + id + "&status=" + status;
}

function setProductsMaterialMap(id) {
    location.href = "/Products/Products/ShowProductsMaterialMap?id=" + id + "&status=" + status;
}

function showProducts(id) {
    location.href = "/Products/Products/show?id=" + id + "&status=" + status;
}

//按钮显示控制
function showControl() {
    var rows = $("#" + gridID).datagrid("getSelections");
    if (rows.length <= 0) {
        //禁用上架
        $("#onSale").addClass("unclick");
        //禁用下架
        $("#offSale").addClass("unclick");
        //禁用更换分类
        $("#changeCategory").addClass("unclick");
        //禁用更换品牌
        $("#changeBrand").addClass("unclick");
        //禁用删除
        $("#del").addClass("unclick");
    } else {
        if (status == "2") {
            //仓库中启用上架
            $('#onSale').removeClass('unclick');
        } else {
            //销售中启用下架
            $('#offSale').removeClass('unclick');
        }
        //启用更换分类
        $('#changeCategory').removeClass('unclick');
        //启用更换品牌
        $('#changeBrand').removeClass('unclick');
        //启用删除
        $('#del').removeClass('unclick');
    }
}

function formatsale(val, row) {
    return '<span style="color:#ff0000;">' + val.toFixed(3) + '</span>';
}
function formatcost(val, row) {
    return '<span style="color:#008c23;">' + val.toFixed(3) + '</span>';
}
function formatimg(val, row) {
    var firstSmallPic = "../../Upload/Products/NoImg.jpg";
    if (val != null && val != '') {
        firstSmallPic = val.split(',')[0];
    }
    return '<div class="d48"><dfn></dfn><img src=' + firstSmallPic + ' width=\'120px\' height=\'120px\'></div>';
}