//初始化
$(function () {

    initTable();
    BindSerarchLickEvent();
    //初始化数据
    BindDictItem("Brand", "Brand");
    //选中特定值
    $("#Brand").combobox('setValue', '0');
});

//绑定搜索按钮的的点击事件
function bindSerarchLickEvent() {
    //得到用户输入的参数
    var queryData = {
        keyWordType: $("#keyWordType").combobox('getValue'),
        keyWord: $("#txtKeyWord").val(),
        categoryID: $("#Category").combotree('getValue'),
        brandID: $("#Brand").combobox('getValue'),
        status: status
    }
    //将值传递给
    initTable(queryData);
}

//搜索
$('#btnSearch').click(function () {
    bindSerarchLickEvent(1);
});
//清空条件
$('#btnReset').click(function () {
    window.location.reload(true);
});
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

function showProducts(id) {
    location.href = "/Products/Products/show?kc=1&id=" + id;
}

function Distribution(id) {
    location.href = "/shop/ShopStock/SetStockPro?sid=" + id;
}


//加载列表
function initTable(queryData) {

    $('#grid').datagrid({
        url: '/Products/Products/Search?kc=1&ram=' + Math.random(),
        height: $(document).height() * 0.8,
        width: function () { return document.body.clientWidth * 0.9 },
        nowrap: false,
        autoRowHeight: false,
        striped: true,
        collapsible: true,
        pagination: true,
        fitColumns: true,
        // pageNumber: pageNumber,
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
        { title: '主键', field: 'ID', width: 40, align: 'center', sortable: true, hidden: true },
        { title: '商品编码', field: 'Code', width: 120, align: 'center', sortable: false },
         {
             title: '图片', field: 'SmallPic', width: 120, align: 'center', formatter: formatimg, sortable: false
         },

          { title: '商品名称', field: 'Name', width: 200, align: 'center', sortable: false },
          { title: '分类', field: 'CategoryName', width: 100, align: 'center', sortable: false },
          { title: '品牌', field: 'BrandName', width: 100, align: 'center', sortable: false },
          { title: '销售价', field: 'SellingPrice', width: 100, formatter: formatsale, align: 'center', sortable: false },
          { title: '成本价', field: 'CostPrice', width: 100, formatter: formatcost, align: 'center', sortable: false },
          {
              title: '库存数量', field: 'Num', width: 100, align: 'center',
              formatter: function (value, row) {
                  return value + '<br/><a href="javascript:void(0);" onclick=\'showProducts(' + row.ID + ')\'>查看</a>';
              }, sortable: false
          },
          { title: '添加时间', field: 'CreateDate', width: 140, align: 'center', sortable: true },
        {
            title: '操作', field: 'Permit', width: 155, align: 'center',
            formatter: function (value, row) {
                return '<a href="javascript:void(0);" onclick=\'Distribution(' + row.ID + ')\'>分配库存</a>&nbsp;';
            },
            sortable: false
        }


        ]]

    });
}
function setRole(id) {
    showMyWindow("角色信息", "/sys/User/userrole?UID=" + id, 700, 500, true, false, false);
}



//刷新
$("#refresh").click(function () {
    //   $('#txtName').val(22);
    initTable();
});
//添加
$("#add").click(function () {
    location.href = "/shop/ShopStock/SetStock?ram=" + Math.random();
});
//删除
$("#delete").click(function () {
    var id = $('#hid').val();
    if (id != "0") {
        $.messager.confirm('提示', "确定要删除吗", function (r) {
            if (r) {
                $.ajax({
                    url: "/sys/User/Delete?id=" + id,
                    type: "GET",
                    cache: false,
                    success: function (r) {
                        var map = $.parseJSON(r);
                        if (map.result == 1) {
                            $('#hid').val("0");
                            initTable();
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
    else {
        // showMsg("提示", "未选择！", true);
        $.MsgBox.Alert("提示", "未选择！", 1000);
    }
});
//绑定搜索按钮的的点击事件
function BindSerarchLickEvent() {
    //按条件进行查询数据，首先我们得到数据的值
    $("#btnSerach").click(function () {
        //得到用户输入的参数
        var queryData = {
            Code: $("#Code").val()

        }
        //将值传递给
        initTable(queryData);
        return false;
    });
}