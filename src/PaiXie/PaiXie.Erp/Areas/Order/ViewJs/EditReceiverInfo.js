$(function () {
    $('#ProvinceID').combobox({
        onSelect: function () {
            var url = '/Warehouse/Warehouse/GetAreaJson?grade=1&pid=' + $('#ProvinceID').combobox('getValue');
            $('#CityID').combobox('reload', url);
            $('#CityID').combobox('select', 0);
            $('#DistrictID').combobox('select', 0);
            $('#Province').val($('#ProvinceID').combobox('getText'));
            $('#City').val('');
            $('#District').val('');
        }
    });
    $('#CityID').combobox({
        onSelect: function () {
            var url = '/Warehouse/Warehouse/GetAreaJson?grade=2&pid=' + $('#CityID').combobox('getValue');
            $('#DistrictID').combobox('reload', url);
            $('#DistrictID').combobox('select', 0);
            $('#District').val('');
            $('#City').val($('#CityID').combobox('getText'));
        }
    });
    $('#DistrictID').combobox({
        onSelect: function () {
            $('#District').val($('#DistrictID').combobox('getText'));
            var postCode = getPostCode($('#DistrictID').combobox('getValue'));
            $('#txtBuyPostCode').val(postCode);
        }
    });

    $("#btnSave").click(function () {
        btnSave();
    });

    $("#btnCancel").click(function () {
        parent.$('#localWin').window('close');
    });
});

function getPostCode(sysareaID) {
    var postCode = '';
    $.ajax({
        url: "/Warehouse/Address/GetPostCode?sysareaID=" + sysareaID,
        type: "GET",
        async: false,
        cache: false,
        success: function (r) {
            postCode = r;
        },
        error: function () {
            $.MsgBox.Alert("提示", "获取邮编失败！");
        }
    });
    return postCode;
}

function btnSave() {
    $('#ff').form('submit', {
        url: "/Order/Download/SaveReceiverInfo",
        type: "POST",
        dataType: "json",
        onSubmit: function () {
            var isValid = $(this).form('validate');
            if (isValid) {
                if ($("#txtBuyTel").val() == "" && $("#txtBuyMtel").val() == "") {
                    isValid = false;
                    $.MsgBox.Alert("提示", "电话和手机必填一个！");
                }
                else if($("#DistrictID").combobox("getValue") == "0")
                {
                    isValid = false;
                    $.MsgBox.Alert("提示", "发货地址不完善！");
                }
            }
            return isValid;
            
        },
        success: function (r) {
            var map = $.parseJSON(r);
            if (map.result == 1) {
                parent.$('#localWin').window('close');
                parent.$('#btnSearch').click();
            }
            else {
                $.MsgBox.Alert("提示", map.message);
            }
        },
        error: function () {
            $.MsgBox.Alert("提示", "确定失败！");
        }
    });
}