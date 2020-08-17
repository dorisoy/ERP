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
        showLocalWindow("添加模版", "/Warehouse/PrintTemplate/Edit?id=0", 480, 300, true, false, false);

    });
});

//加载列表
function initTable(pageNumber) {
    $('#grid').datagrid({
        url: '/Warehouse/PrintTemplate/Search?ram=' + Math.random(),
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
            { title: '模版名称', field: 'Name', width: 120, align: 'center', sortable: false },
            {title: '模版类型', field: 'TypeName', width: 80, align: 'center',sortable: false},
            {
                title: '状态', field: 'IsEnable', width: 80, align: 'center',
                formatter: function (value, row) {
                    return value == 0 ? '<span style="color:#ff0000;">禁用</span>' : '启用';
                },
                sortable: false
            },
            {
                title: '默认', field: 'IsDefault', width: 100, align: 'center',
                formatter: function (value, row) {
                    var isChecked = value == 1 ? "checked" : "";
                    var isDefault = value == 1 ? 0 : 1;
                    var html = '<input type="checkbox" id="chkIsDefault_' + row.ID + '" name="chkIsDefault" ' + isChecked + ' onclick="setIsDefault(' + row.TypeID + ',' + row.ID + ',' + isDefault + ')" value="' + value + '" />';
                    return html;
                },
                sortable: false
            },
            {
                title: '操作', field: 'Permit', width: 120, align: 'center',
                formatter: function (value, row) {
                    var html = "<a href=\"javascript:void(0);\" onclick=\"editPrintTemplate(" + row.ID + ");\">编辑</a>";
                    html += " | <a href=\"javascript:void(0);\" onclick=\"setPrintTemplate(" + row.ID + ");\">设置模版</a>";
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

//编辑模版
function editPrintTemplate(id) {
    showLocalWindow("编辑模版", "/Warehouse/PrintTemplate/Edit?id=" + id, 480, 300, true, false, false);
}

//设置模版
function setPrintTemplate(id) {
    location.href = "/Warehouse/PrintTemplate/SetPrintTemplate?id=" + id + "&ram=" + Math.random();
}

//启用禁用
function setIsEnable(id, isEnable) {
    var operateName = isEnable == 0 ? "禁用" : "启用";
    $.ajax({
        url: "/Warehouse/PrintTemplate/SetIsEnable?id=" + id + "&isEnable=" + isEnable,
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

//设置、取消 默认模版
function setIsDefault(typeID, id, isDefault) {
    var operateName = isDefault == 0 ? "取消默认" : "设置默认";
    $.ajax({
        url: "/Warehouse/PrintTemplate/SetIsDefault?typeID=" + typeID + "&id=" + id + "&isDefault=" + isDefault,
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

