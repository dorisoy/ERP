//初始化
$(function () {
    $('#txtCode').focus();
    $('#ReservoirArea').combobox({
        url: '/Warehouse/Location/GetDictTopLocation?ram=' + Math.random(),
        valueField: 'Value',
        textField: 'Text',
        onLoadSuccess: function () { //数据加载完毕事件
            $("#ReservoirArea").combobox('select', "0");
        },
        onChange: function () {
            initTable();
        }
    });
    $("#btnSearch").click(function () {
        btnSearch();
    });
    $("#txtCode").keyup(function (e) {
        var currKey = 0, e = e || event;
        currKey = e.keyCode || e.which || e.charCode;
        if (currKey == 13) {
            btnSearch();
        }
    });

    $('.numbox').live('keypress', isNumber);

    $("#btnSave").click(function () {
        btnSave();
    });

    $("#btnClose").click(function () {
        parent.$('#localWin').window('close');
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
//查询
function btnSearch() {
    var productsSkuCode = $("#txtCode").val();
    var sourceID = $("#hdnSourceID").val();
    if (productsSkuCode == "") {
        $.MsgBox.Alert("提示", "请输入商品SKU码！", 1000);
        $("#txtCode").focus();
        return false;
    }
    $.ajax({
        url: "/Warehouse/WarehouseOutStockProducts/SearchProductsSku",
        type: "POST",
        cache: false,
        data: { sourceID: sourceID, productsSkuCode: productsSkuCode },
        success: function (r) {
            var map = $.parseJSON(r);
            if (map.result == 1) {
                $('#txtCode').val('');
                $('#setlist,#infotitle,#infoshow,#buttonbox').show();
                $('#infotitle').text('商品名称：' + map.Name);
                $('#infoshow li:eq(0)').text('商品属性：' + map.Saleprop);
                $('#hdnProductsCode').val(map.Code);
                $('#hdnProductsID').val(map.ID);
                $('#hdnProductsNo').val(map.No);
                $('#hdnProductsName').val(map.Name);
                $('#hdnProductsSkuID').val(map.ProductsSkuID);
                $('#hdnProductsSkuCode').val(productsSkuCode);
                $('#hdnProductsSkuSaleprop').val(map.Saleprop);
                initTable();
            } else {
                $.MsgBox.Alert("提示", map.message);
                $('#setlist,#infotitle,#infoshow,#buttonbox').hide();
            }
        },
        error: function () {
            $.MsgBox.Alert("提示", "读取商品SKU码失败！");
        }
    });
}
//加载列表
function initTable() {
    $('#grid').datagrid({
        url: '/Warehouse/WarehouseOutStockProducts/SearchLocationProducts?ram=' + Math.random() + '&productsSkuID=' + $('#hdnProductsSkuID').val() + '&topLocationID=' + $("#ReservoirArea").combobox('getValue'),
        width: 650,
        height: 140,
        fitColumns: true,
        rownumbers: false,
        showFooter: true,
        columns: [[
			{ field: 'ID', hidden: true },
			{
			    field: 'LocationCode', title: '库位编码', width: 80,
			    formatter: function (value, row, index) {
			        var html = value + '<input type="hidden" value="' + row.LocationCode + '" id="LocationCode_' + index + '" name="LocationCode"/>';
			        html += '<input type="hidden" value="' + row.LocationID + '" id="LocationID_' + index + '" name="LocationID"/>';
			        return html;
			    },
			    align: 'center'
			},
            {
                field: 'ProductsBatchCode', title: '商品批次', width: 80,
                formatter: function (value, row, index) {
                    var html = value + '<input type="hidden" value="' + row.ProductsBatchCode + '" id="ProductsBatchCode_' + index + '" name="ProductsBatchCode"/>';
                    html += '<input type="hidden" value="' + row.ProductsBatchID + '" id="ProductsBatchID_' + index + '" name="ProductsBatchID"/>';
                    return html;
                },
                align: 'center'
            },
			{
			    field: 'KyNum', title: '当前库存<s class="prompt" id="Inventory"></s>', width: 80, align: 'center',
			    formatter: function (value, row, index) {
			        var html = value + '<input type="hidden" value="' + value + '" id="KyNum' + index + '" name="KyNum"/>';
			        return html;
			    }
			},
			{
			    field: 'OutNum', title: '出库数量', width: 80,
			    formatter: function (value, row) {
			        return '<input type="text" value="" class="inputextbox numbox" name="OutNum"  id="OutNum_' + row.ID + '"/>';
			    },
			    align: 'center'
			}
        ]],
        onLoadSuccess: function (data) {
            DataGridNoData(this);
        }
    });
}
//保存
function btnSave() {
    $('#ffpost').form('submit', {
        url: "/Warehouse/WarehouseOutStockProducts/Save?ram=" + Math.random(),
        type: "GET",
        dataType: "json",
        onSubmit: function () {
            var isValid = $(this).form('validate');
            var totalNum = 0;
            var kyNums = $("input[name='KyNum']");
            $("input[name='OutNum']").each(function (index, element) {
                var currOutNum = $(element).val();
                if (currOutNum != "") {
                    if ($.isNumeric(currOutNum)) {
                        if (Number(currOutNum) > 0) {
                            if (Number(currOutNum) <= Number($(kyNums[index]).val())) {
                                totalNum = totalNum + Number(currOutNum);
                            } else {
                                $.MsgBox.Alert("提示", "出库数量不能大于当前库存！", 1000);
                                $(element).focus();
                                isValid = false;
                                return false;
                            }
                        } else {
                            $.MsgBox.Alert("提示", "出库数量必须大于0！", 1000);
                            $(element).focus();
                            isValid = false;
                            return false;
                        }
                    } else {
                        $.MsgBox.Alert("提示", "出库数量必须是数字！", 1000);
                        $(element).focus();
                        isValid = false;
                        return false;
                    }
                }
            });
            if (totalNum == 0 && isValid) {
                $.MsgBox.Alert("提示", "请输入出库数量！");
                isValid = false;
            }
            return isValid;	// 返回false终止表单提交
        },
        success: function (r) {
            var map = $.parseJSON(r);
            if (map.result == 1) {
                parent.$("#lblOutStockNum").click();
                parent.$("#btnSearch").click();
                $('#setlist,#infotitle,#infoshow,#buttonbox').hide();
                $('#txtCode').focus();
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