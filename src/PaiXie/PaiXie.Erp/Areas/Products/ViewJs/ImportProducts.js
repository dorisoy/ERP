//初始化
$(function () {
    initTable();
    $('#back').click(function () {
        var status = $("#hdnStatus").val();
        location.href = '/Products/Products/Index?status=' + status;
    });

    $("#btnConfirm").click(function (ev) {
        ev.preventDefault();
        if (!validateExcel())
            return;
        $('#spanProcessBar').html("<img src='../../Content/images/loading.gif' width='16px' height='16px'/>");
        var isUpdate = false;
        if ($("#chkIsUpdate").attr("checked")) {
            isUpdate = true;
        }
        $.ajaxFileUpload({
            url: "/Products/Products/ImportProducts",
            type: 'post',
            data: { isUpdate: isUpdate },
            secureuri: false,
            fileElementId: "fileExcel",
            dataType: "json",
            success: function (data, status) {
                var map = $.parseJSON(data);
                $('#spanProcessBar').html("");
                if (map.result == 1) {
                    $('#message').html("提示：本次共导入SKU <font style='color:#ff0000;'>" + (map.successCount + map.errorCount) + " </font>件，<font style='color:#ff0000;'>" + map.successCount + " </font>件成功，<font style='color:#ff0000;'>" + map.errorCount + " </font>件异常。");
                    initTable();
                } else {
                    $.MsgBox.Alert("提示", map.message);
                }
            },
            error: function (data, status, e) {
                $('#spanProcessBar').html("");
                $.MsgBox.Alert("提示", "上传失败！");
            }
        });
    });
});
//加载列表
function initTable() {
    $('#grid').datagrid({
        url: '/Products/Products/ImportError?ram=' + Math.random(),
        height: $(document).height() * 0.8,
        width: function () { return document.body.clientWidth * 0.9 },
        nowrap: true,
        autoRowHeight: false,
        striped: true,
        collapsible: true,
        pagination: true,
        fitColumns: true,
        fit: true, //datagrid自适应宽度
        pageNumber: 1,
        border: false,
        pageSize: 15,
        pageList: [15, 20, 30],
        rownumbers: false,
        remoteSort: false,
        idField: 'ID',
        onClickRow: function (rowIndex, rowData) {
        },
        onDblClickRow: function (rowIndex, rowData) {
        },
        columns: [[
        { field: 'ck', checkbox: true },   //选择
        { title: '主键', field: 'ID', width: 40, align: 'center', sortable: true, hidden: true },
        { title: '商品编码', field: 'ProductsCode', width: 120, align: 'center', sortable: false },
         { title: '商品名称', field: 'ProductsTitle', width: 200, align: 'center', sortable: false },
         { title: '商品属性', field: 'Saleprop', width: 150, align: 'center', sortable: false },
         { title: '商品SKU码', field: 'ProductsSkuCode', width: 150, align: 'center', sortable: false },
         { title: '导入时间', field: 'CreateDate', width: 140, align: 'center', sortable: true },
        {
            title: '失败原因', field: 'Permit', width: 200, align: 'center',
            formatter: function (value, row) {
                return '<font style="color:#ff0000;">导入失败！</font><br/><font style="color:#1d74e0;">' + row.ErrorMessage.replace(/！,/g, "！<br/>"); + '</font>';
            },
            sortable: false
        }


        ]]
    });
}

function validateExcel() {
    var file = $("#fileExcel").val();
    if (file.length == 0) {
        $.MsgBox.Alert("提示", "请选择要上传的文件！");
        return false;
    }

    var index = file.lastIndexOf(".");
    if (index > -1) {
        //获取扩展名
        var extName = file.substring(index + 1).toLowerCase();
        if (extName == 'xls' || extName == 'xlsx') {
            return true;
        }
        else {
            $.MsgBox.Alert("提示", "只能上传 .xls或.xlsx类型的文件！");
            return false;

        }
    }
    else {
        $.MsgBox.Alert("提示", "只能上传 .xls或.xlsx类型的文件！");
        return false;
    }
}

function formatsale(val, row) {
    return '<span style="color:#ff0000;">' + val.toFixed(3) + '</span>';
}
function formatcost(val, row) {
    return '<span style="color:#008c23;">' + val.toFixed(3) + '</span>';
}