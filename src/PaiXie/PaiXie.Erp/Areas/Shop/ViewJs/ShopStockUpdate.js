var t1;
//初始化
$(function () {
    initTable();
    //切换店铺
    $('#shop').combobox({
        onChange: function () {
            var shopid = $("#shop").combobox('getValue');
            // 提示信息
         
            $('#lblupdatestatus').html('0/0');
            
            showmsg(shopid);
            BindSerarchLickEvent();
        }
    });
});
//提示信息
function showmsg(shopid) {
    $.ajax({
        url: "/Shop/ShopStockUpdate/GetUpdateInfo?shopid=" + shopid,
        type: "GET",
        cache: false,
        success: function (r) {
            var map = $.parseJSON(r);
            if (map.result == 1) {
                $('#lblpronum').html(map.pronum);
                $('#lblskunum').html(map.skunum);
                $('#lblsuess').html(map.sucesssku);
                $('#lblerror').html(map.errorsku);
                $('#lbltime').html(map.updatetime);
            }
        },
        error: function () {
        }
    });
}
//刷新
$('#refresh').click(function () {
    BindSerarchLickEvent();
});
//加载列表
function initTable(queryData) {
    $('#grid').datagrid({
        url: '/Shop/ShopProducts/ShopStockUpdateSearch?ram=' + Math.random() + "&shopid=" + $("#shop").combobox('getValue'),
        height: $(document).height() * 0.8,
        width: function () { return document.body.clientWidth * 0.9 },
        nowrap: false,
        autoRowHeight: false,
        striped: true,
        collapsible: true,
        pagination: true,
        fitColumns: true,
        fit: true, //datagrid自适应宽度
        border: false,
        pageSize: 15,
        pageList: [15, 20, 30],
        rownumbers: false,
        remoteSort: false,
        idField: 'ID',
        queryParams: queryData,  //异步查询的参数
        columns: [[
       // { field: 'ck', checkbox: true },   //选择
        { title: '主键', field: 'ID', width: 40, sortable: true, hidden: true },
        { title: '商品编码', field: 'OuterId', width: 80, align: 'center', sortable: true },
            { title: '商品名称', field: 'ProTitle', width: 100, align: 'center', sortable: true },
       { title: '来源店铺', field: 'ShopName', width: 80, align: 'center', sortable: true },

          { title: '更新时间', field: 'UpdateTime', width: 100, align: 'center',
          formatter: function (value, row) {
              if(row.UpdateTime=="9999-01-01 00:00:00") 
              {
                  return '--';
              }
              else
              {
                  return   row.UpdateTime;
              }
          },
          
          
          sortable: true },


     

         {
               title: '更新状态', field: 'UpdateStatus', width: 100, align: 'center',
               formatter: function (value, row) {
                   var statusstr = row.UpdateStatus;
                   if (statusstr == '0')
                       statusstr = '--';
                   return statusstr + '<br/>' + row.ErrorMsg;
               },
               sortable: true
           },
      {
          title: '操作', field: 'Permit', width: 60, align: 'center',
          formatter: function (value, row) {
              return '<a href="javascript:void(0);" onclick=\'update(' + row.ShopID + ',' + row.OuterId + ')\'>更新</a>&nbsp;';
          },
          sortable: false
      }
        ]]
    });
}
//搜索
$("#btnSerach").click(function () {
    BindSerarchLickEvent();
});
//绑定搜索的事件
function BindSerarchLickEvent() {
    //得到用户输入的参数
    var queryData = {
        OuterId: $("#OuterId").val(),
        ProNo: $("#ProNo").val(),
        ProSKU: $("#ProSKU").val()
    }
    //将值传递给
    initTable(queryData);
}
//更新库存  按店铺
$('#btnupdate').click(function () {
  
    var shopid = $("#shop").combobox('getValue');
    if (shopid == 0)
        $.MsgBox.Alert("提示", "未选择店铺！", 1000);
    else {
        $.ajax({
            url: "/Shop/ShopStockUpdate/StockUpdate?shopid=" + shopid,
            type: "GET",
            cache: false,
            async: false,
            success: function (r) {
                var map = $.parseJSON(r);
                if (map.result == 1) {
                    //进度
                  //  initTable();
                    //   $.MsgBox.Alert("提示", "操作完成！", 1000);
                    t1 = setInterval(f, 100);
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
//进度
function f() {
    var shopid = $("#shop").combobox('getValue')
    $.ajax({
        url: "/Shop/ShopStockUpdate/StockUpdatepross?shopid=" + shopid,
        type: "GET",
        async: true,
        cache: false,
        success: function (r) {
            var map = $.parseJSON(r);
            if (map.result == 1) {
             
                $('#lblupdatestatus').html("<img src='../../Content/images/loading.gif' width='16px' height='16px'/>" + map.message);
             //   $('#lblupdatestatus').html(map.message);
            } else if (map.result == 99) {
                $.MsgBox.Alert("提示", "操作完成！", 1000);
              
                $('#lblupdatestatus').html(map.message);
                //去掉定时器
                window.clearInterval(t1);
                initTable();
                showmsg(shopid);
            }
        },
        error: function () {
        }
    });
}
//每个商品更新  
function update(shopid, OuterId) {
    $.ajax({
        url: "/Shop/ShopStockUpdate/StockUpdate?shopid=" + shopid + "&OuterId=" + OuterId,
        type: "GET",
        cache: false,
        success: function (r) {
            var map = $.parseJSON(r);
            if (map.result == 1) {
                initTable();
                $.MsgBox.Alert("提示", "操作完成！", 1000);
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