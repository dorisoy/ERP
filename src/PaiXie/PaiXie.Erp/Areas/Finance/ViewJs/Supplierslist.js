//初始化
$(function () {
    BindDictItem("Suppliers", "Suppliers"); 
    $("#Suppliers").combobox('setValue', '0');
    initTable("", 1);
  
});
//搜索
$('#btnSerach').click(function () {
    BindSerarchLickEvent(1);
});
//刷新当前页
$('#refreshCurrentPage').click(function () {
    var grid = $("#grid");
    var options = grid.datagrid("getPager").data("pagination").options;
    var currPageNumber = options.pageNumber;
    BindSerarchLickEvent(currPageNumber);
});

//加载列表
function initTable(queryData, pageNumber) {
    $('#grid').datagrid({
        url: '/Finance/Suppliers/search?ram=' + Math.random(),
        height: $(document).height() * 0.8,
        width: function () { return document.body.clientWidth * 0.9 },
        nowrap: true,
        autoRowHeight: false,
        pageNumber: pageNumber,
        striped: true,
        collapsible: true,
        pagination: true,
        fitColumns: true,
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
        { field: 'ck', checkbox: true },   //选择
        { title: '主键', field: 'ID', width: 40, sortable: true, hidden: true },
        {
            title: '出/入库号', field: 'cr', width: 100, align: 'center',
            formatter: function (value, row, index) {

             
                if (row.BillType == "10") {
                    return '<a href="javascript:void(0)" >' + row.BillNo + '</a>';
                } else {
                    return '<a href="javascript:void(0)" >' + row.BillNo + '</a><br/>(' + row.SourceNo + ')';
                }
            },
            sortable: true
        },
         { title: '类型', field: 'BillTypename', width: 100, align: 'center', sortable: true },
          { title: '供应商', field: 'Suppliersname', align: 'center', width: 50, sortable: true },
          { title: '仓库', field: 'Warehousename', width: 120, sortable: true },
           { title: '备注', field: 'Remark', width: 100, align: 'center', sortable: true },
            { title: '数量', field: 'totalnum', width: 120, sortable: true },
             { title: '总金额', field: 'totalPrice', width: 100, align: 'center', sortable: true },
              { title: '确认时间', field: 'ConfirmDate', width: 120, sortable: true },
              { title: '核对状态', field: 'shenheName', width: 120, sortable: true },
               { title: '结算状态', field: 'SettlementName', width: 120, sortable: true }
        ]]
    });
}


//刷新
$("#refresh").click(function () {
    BindSerarchLickEvent(1);
});
//供应商结算
$("#add").click(function () {
    var rows = $("#grid").datagrid("getSelections");  
    if (rows.length > 0) {
        var ids = [];
        for (var i = 0; i < rows.length; i++) {
            ids.push(rows[i].ID);
        } 
        $.ajax({
            url: "/Finance/Suppliers/SuppliersCheck?ram=" + Math.random() + "&ids=" + ids.join(','),
            type: "GET",
            cache: false,
            success: function (r) {
                var map = $.parseJSON(r);
                if (map.result == 1) {
                    showLocalWindow("供应商结算", "/Finance/Suppliers/Edit?ids=" + ids.join(','), 500, 420, true, false, false);
                }
                else {
                    $.MsgBox.Alert("提示", map.message);
                }
            },
            error: function () {
                $.MsgBox.Alert("提示", "检查失败！");
            }
        });
     }
    else {
        $.MsgBox.Alert("提示", "请选择单据！");
    }   
});

//绑定搜索按钮的的点击事件
function BindSerarchLickEvent(pageNumber) {
    //按条件进行查询数据，首先我们得到数据的值
  //  $("#btnSerach").click(function () {
        //得到用户输入的参数
        var queryData = {
            suppliersID: $("#Suppliers").combobox('getValue'),
            billtype: $("#billtype").combobox('getValue'),
            Status: $("#Status").combobox('getValue'),
            Settlement: $("#Settlement").combobox('getValue')
        }
        //将值传递给
        initTable(queryData, pageNumber);
     //   return false;
   // });
}