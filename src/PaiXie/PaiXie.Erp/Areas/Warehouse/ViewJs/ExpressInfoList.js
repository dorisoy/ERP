//初始化
$(function () {
    initTable(1);
    //刷新
    $("#refresh").click(function () {
        initTable(1);
    });
    //刷新当前页
    $("#refreshCurrentPage").click(function () {
        var grid = $("#grid");
        var options = grid.treegrid("getPager").data("pagination").options;
        var currPageNumber = options.pageNumber;
        initTable(currPageNumber);
    });
    //添加
    $("#add").click(function () {
        showLocalWindow("添加快递", "/Warehouse/Express/Edit?id=0", 480, 350, true, false, false);

    });
});

//加载列表
function initTable(pageNumber) {
    $('#grid').datagrid({
        url: '/Warehouse/Express/Search?ram=' + Math.random(),
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
        columns: [[
            { title: '主键', field: 'ID', width: 40, align: 'center', sortable: false, hidden: true },
            { title: '快递名称', field: 'Name', width: 120, align: 'center', sortable: false },
            { title: '所属物流', field: 'LogisticsName', width: 120, align: 'center', sortable: false },
            { title: '物流代码', field: 'LogisticsCode', width: 120, align: 'center', sortable: false },
            { title: '打印类型', field: 'PrinterTypeName', width: 80, align: 'center', sortable: false },
            {
                title: '状态', field: 'IsEnable', width: 80, align: 'center',
                formatter: function (value, row) {
                    return value == 0 ? '<span style="color:#ff0000;">禁用</span>' : '启用';
                },
                sortable: false
            },
            {
                title: '打印模版', field: 'PrintTemplate', width: 100, align: 'center',
                formatter: function (value, row) {
                    var html = "<a href=\"javascript:void(0);\" onclick=\"setExpressPrintTemplate(" + row.ID + ",'" + row.LogisticsCode + "','" + row.LogisticsName + "'," + row.PrinterType + ",'" + row.Name + "');\">设置快递打印模版</a>";
                    return html;
                },
                sortable: false
            },
            {
                title: '操作', field: 'Permit', width: 120, align: 'center',
                formatter: function (value, row) {
                    var html = "<a href=\"javascript:void(0);\" onclick=\"editExpress(" + row.ID + ");\">编辑</a>";
                    html += " | <a href=\"javascript:void(0);\" onclick=\"setExpressPrice(" + row.ID + ",'" + row.LogisticsCode + "','" + row.LogisticsName + "'," + row.PrinterType + ",'" + row.Name + "');\">设置运费</a>";
                    if (row.IsEnable == 0) {
                        html += " | <a href=\"javascript:void(0);\" onclick=\"setIsEnable(" + row.ID + ",1);\">启用</a>";
                    } else {
                        html += " | <a href=\"javascript:void(0);\" onclick=\"setIsEnable(" + row.ID + ",0);\">禁用</a>";
                    }
                    return html;
                },
                sortable: false
            }
        ]],
        onLoadSuccess: function (data) {
            $("#grid").treegrid('clearSelections');
            DataGridNoData(this);
        }
    });
}

//设置快递打印模版
function setExpressPrintTemplate(id, logisticsCode, logisticsName, printerType, name) {
    //var title = name + "打印模版设计";
    //var src = "/Warehouse/Express/SetExpressPrintTemplate?id=" + id + "&logisticsCode=" + logisticsCode + "&printerType=" + printerType + "&ram=" + Math.random();
    ////var re = $(this).attr("re");
    //var mid = "SetExpressPrintTemplate" + id;
    ////拼接一个Iframe标签
    //var str = '  <iframe id="frmWorkArea' + mid + '" data-options="fit:true" frameborder="0" style="  width: 100%; height: 100%; overflow: hidden;" src="' + src + '"></iframe>   '
    ////首先判断用户是否已经单击了此项，如果单击了直接获取焦点，负责打开
    //var isExist = parent.$("#worktab").tabs('exists', title);
    //if (!isExist) {
    //    parent.$('#worktab').tabs('add', { title: title, content: str, closable: true });
    //}
    //else {
    //    //如果存在则获取焦点
    //    parent.$("#worktab").tabs('select', title);
    //}
    location.href = "/Warehouse/Express/SetExpressPrintTemplate?id=" + id + "&logisticsCode=" + logisticsCode + "&logisticsName=" + encodeURIComponent(logisticsName) + "&printerType=" + printerType + "&ram=" + Math.random();
}

//编辑快递
function editExpress(id) {
    showLocalWindow("编辑快递", "/Warehouse/Express/Edit?id=" + id, 480, 350, true, false, false);
}
//设置运费
function setExpressPrice(id, logisticsCode, logisticsName, printerType, name) {
    location.href = "/Warehouse/Express/SetExpressPrice?id=" + id + "&logisticsCode=" + logisticsCode + "&logisticsName=" + encodeURIComponent(logisticsName) + "&printerType=" + printerType + "&ram=" + Math.random();
}
//启用禁用
function setIsEnable(id, isEnable) {
    var operateName = isEnable == 0 ? "禁用" : "启用";
    $.ajax({
        url: "/Warehouse/Express/SetIsEnable?id=" + id + "&isEnable=" + isEnable,
        type: "GET",
        cache: false,
        success: function (r) {
            var map = $.parseJSON(r);
            if (map.result == 1) {
                $.MsgBox.Alert("提示", operateName + "成功！", 1000);
                $('#refreshCurrentPage').click();
            } else {
                $.MsgBox.Alert("提示", map.message);
            }
        },
        error: function () {
            $.MsgBox.Alert("提示", operateName + "失败！");
        }
    });
}