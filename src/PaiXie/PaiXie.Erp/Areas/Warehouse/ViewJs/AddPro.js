var code = '';
$(document).ready(function () {
    $('#Code').textbox('textbox').keydown(function (e) {
        if (e.keyCode == 13) {
            btnSearch();
        }
    });
    code = $('#Code').val();
    if ($('#infotitle').text() != '') $('#infotitle,#infoshow,#buttonbox').show();
    //查找
    $("#btnSearch").click(function () {
        btnSearch();
    });
    $('.numbox').live('keypress', isNumber);
    $("#btnSave").click(function () {
        $('#ffcode').val(code);
        btnSave();
    });
    $("#btnCancel").click(function () {
        parent.$('#localWin').window('close');
    });
    $('#ReservoirArea').combobox({
        onChange: function () {
            var kqid = $("#ReservoirArea").combobox('getValue');
            initTable();
        }
    });
    $('#Inventory').tooltip({
        position: 'top',
        content: '本库位上该商品的库存',
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
//加载列表
function initTable() {
    $('#grid').datagrid({
        url: '/Warehouse/WarehouseInStock/SKUStockNum?ram=' + Math.random() + '&skucode=' + code + '&kqid=' + $("#ReservoirArea").combobox('getValue'),
        width: 650,
        height: 140,
        fitColumns: true,
        rownumbers: false,
        showFooter: true,
        columns: [[
			{ field: 'ID', hidden: true },
			{
			    field: 'LibraryCode', title: ' 库位编码', width: 80,
			    formatter: function (value, row) {
			       
			        if (value == "null")
			        {
			            value = '';
			        }
			        if (row.IsEdit == 1)
			        {
			            return '<input type="text"   readonly="readonly"   value="' + value + '" class="inputextbox" name="LibraryCode"  id="LibraryCode[' + row.ID + ']"/>';

			        }
			        else
			        {
			            return '<input type="text"    value="' + value + '" class="inputextbox" name="LibraryCode"  id="LibraryCode[' + row.ID + ']"/>';

			        }
			        },
			    align: 'center'
			},
			{ field: 'Inventory', title: '当前库存<s class="prompt" id="Inventory"></s>', width: 80, align: 'center' },
			{
			    field: 'StorageNum', title: '入库数量', width: 80,
			    formatter: function (value, row) {
			        return '<input type="text" value="' + value + '" class="inputextbox numbox" name="StorageNum"  id="StorageNum[' + row.ID + ']"/>';
			    },
			    align: 'center'
			}
        ]],
        onLoadSuccess: function (data) {
            if (data.total == 0) {
                //   $('#setlist').hide();
            } else {
                //  $('#setlist').css("display", "block");
                //   $('#setlist').show();
            }
            DataGridNoData(this);
        }
    });
}
//查询
function btnSearch() {
    $('#ff').form('submit', {
        url: "/Warehouse/WarehouseInStock/SkuSearch?BillNo=" + $('#BillNo').val() + "&ram=" + Math.random(),
        type: "GET",
        dataType: "json",
        onSubmit: function () {
            var isValid = $(this).form('validate');
            return isValid;	// 返回false终止表单提交
        },
        success: function (r) {
            var map = $.parseJSON(r);
            if (map.result == 1) {
                code = $('#Code').val();
                $('#ff input').val('');
                $('#setlist,#infotitle,#infoshow,#buttonbox').show();
                $('#infotitle').text('商品名称：' + map.Name);
                $('#infoshow li:eq(0)').text('商品属性：' + map.Attribute);
              
                $('#PurchasePrice').val(map.PurchasePrice);
                $('#PurchasePrice').next().find('input').val(map.PurchasePrice);
                $("#PurchasePrice").numberbox('setValue', map.PurchasePrice);
                $('#ProductionDate').val();
                $('#ProductionDate').next().find('input').val();
                initTable();
            } else {
                $.MsgBox.Alert("提示", map.message);
            }
        },
        error: function () {
            $.MsgBox.Alert("提示", "操作失败,请重试或刷新！", 1000);
        }
    });
}



//保存
function btnSave() {
    $('#ffpost').form('submit', {
        url: "/Warehouse/WarehouseInStock/PutSKUStockSave?ram=" + Math.random(),
        type: "GET",
        dataType: "json",
        onSubmit: function () {
            var isValid = $(this).form('validate');
            return isValid;	// 返回false终止表单提交
        },
        success: function (r) {
            var map = $.parseJSON(r);
            if (map.result == 1) {
             //   alert($('#BillNo').val());
                //入库数量
                $.ajax({
                    url: "/Warehouse/WarehouseInStock/getProductsNum?ram=" + Math.random() + "&BillNo=" + $('#BillNo').val(),
                    type: "GET",
                    cache: false,
                    success: function (r) {
                      //  alert(r);
                        parent.$("#rksl").text("入库数量:"+r);
                    },
                    error: function () {
                    }
                });
                parent.$("#btnSearch").click();
                $.MsgBox.Alert("提示", "添加成功！", 1000);
                setTimeout("gotonew();", 2000);    //等待2s执行一次             
            } else {
                $.MsgBox.Alert("提示", map.message);
            }
        },
        error: function () {
            $.MsgBox.Alert("提示", "保存失败！");
        }
    });
}
//数字格式化
function isNumber(e) {
    if ($.browser.msie) {
        if (((event.keyCode > 47) && (event.keyCode < 58)) ||
			  (event.keyCode == 8)) {
            return true;
        } else {
            return false;
        }
    } else {
        if (((e.which > 47) && (e.which < 58)) ||
			  (e.which == 8)) {
            return true;
        } else {
            return false;
        }
    }
}
//跳转  重新添加
function gotonew() {
    location.href = "/Warehouse/WarehouseInStock/AddPro?BillNo=" + $('#BillNo').val();
}