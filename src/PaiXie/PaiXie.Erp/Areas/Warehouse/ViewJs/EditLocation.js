//初始化
$(function () {
    //返回
    $("#back,#btnClose").click(function () {
        location.href = '/Warehouse/Location/Index';
    });
    if ($("#hdnID").val() == 0) {
        //获取子结构
        $('#WarehouseAreaStruct').combobox({
            url: "/Warehouse/AreaStruct/JsonTop",
            valueField: 'Value',
            textField: 'Text',
            onSelect: function (node) {
                $(".structTr").remove();
                if (Number(node.Value) > 0) {
                    $.ajax({
                        url: "/Warehouse/AreaStruct/GetChildWarehouseAreaStruct?parentID=" + node.Value,
                        type: "GET",
                        async: false,
                        cache: false,
                        success: function (r) {
                            //var struct = [{ "Name": "排", "Code": "P" }, { "Name": "组", "Code": "Z" }, { "Name": "层", "Code": "C" }, { "Name": "位", "Code": "W" }];
                            var struct = $.parseJSON(r);
                            $.each(struct, function (index, item) {
                                var currName = item.Name;
                                var currCode = item.Code == null ? "" : item.Code;
                                var parentName = "";
                                if (index == 0) {
                                    parentName = "一共";
                                } else {
                                    parentName = "每" + struct[index - 1].Name;
                                }
                                var tr = $("<tr class='structTr'></tr>");
                                var th = $("<th></th>");
                                th.appendTo(tr);
                                var td = "<td colspan='3'><b>" + currName + "：</b>";
                                td += parentName + " &nbsp;<input type=\"text\" id=\"txtStructCount_" + index + "\" name=\"StructCount\" value=\"\" style=\"width:100px\" class=\"easyui-numberbox\" min=\"1\" max=\"10000\" missingmessage=\"只能输入1~10000之间的数字\" data-options=\"required:true,validType:'number',height:30\">&nbsp;<input type=\"hidden\" id=\"hdnStructName_" + index + "\" name=\"StructName\" value=\"" + currName + "\" />" + currName;
                                td += "&nbsp;&nbsp;&nbsp;&nbsp;";
                                td += "代码：<input type=\"text\" id=\"txtStructCode_" + index + "\" name=\"StructCode\" value=\"" + currCode + "\" style=\"width:100px\" class=\"easyui-validatebox\" data-options=\"required:true,validType:['code','length[1,4]']\">";
                                td += "<span class=\"f12c\">(用于自动生成库位编码)</span></td>";
                                $(td).appendTo(tr);
                                tr.insertBefore($("#lastTr"));
                                $.parser.parse(tr);
                            });

                        },
                        error: function () {
                            $.MsgBox.Alert("提示", "读取库区结构失败！", 1000);
                        }

                    });
                }
            }
        });
    }
    //检查库区代码
    $("#txtCode").blur(checkCode);
    //保存
    $("#btnSave").click(btnSave);
});

function checkCode() {
    var result = 1;
    if ($("#txtCode").val() != "") {
        //检查库区代码是否已经存在
        $.ajax({
            url: "/Warehouse/Location/CheckCode",
            type: "POST",
            data: { "id": $("#hdnID").val(), "code": $("#txtCode").val() },
            async: false,
            cache: false,
            success: function (r) {
                var map = $.parseJSON(r);
                if (map.result == -1) {
                    result = 0;
                    $("#errorMessage").html("库区代码已存在！");
                    $("#txtCode").focus();
                } else {
                    $("#errorMessage").html("");
                }
            },
            error: function () {
                result = 0;
                $("#errorMessage").html("读取库区代码失败！");
            }
        });
    }
    return result;
}

function btnSave() {
    $("#loading").show();
    var result = checkCode();
    if (result == 1) {
        $('#ff').form('submit', {
            url: "/Warehouse/Location/Save",
            type: "POST",//使用get方法访问后台
            dataType: "json",
            onSubmit: function () {
                var isValid = $(this).form('validate');
                if (isValid) {
                    var totalCount = 1;
                    $("input[name='StructCount']").each(function (index, element) {
                        totalCount = totalCount * Number($(element).val());
                    });
                    if (totalCount > 10000) {
                        $.MsgBox.Alert("提示", "库位总数不能超过10000个！");
                        isValid = false;
                    }
                }
                return isValid;	// 返回false终止表单提交
            },
            success: function (r) {
                var map = $.parseJSON(r);
                if (map.result == 1) {
                    $("#loading").hide();
                    $("#back").click();
                } else {
                    $.MsgBox.Alert("提示", map.message);
                }
            },
            error: function () {
                $.MsgBox.Alert("提示", "保存失败！");
            }
        });
    }
}