$(function () {
    //返回
    $("#back,#btnClose").click(function () {        
        var status = $("#hdnStatus").val();
        location.href = '/Products/Products/Index?status=' + status;
    });
    //保存
    $("#btnSave,#toolbarsave").click(function () {
        btnSave();
    });
    //增加商品属性
    $("#btnAddProductsSku").click(function () {
        addProductsSku();
    });
    //SKU销售价相同
    $("#chkSameSellingPrice").click(function () {
        sameSellingPrice(undefined);
    });
    //SKU成本价相同
    $("#chkSameCostPrice").click(function () {
        sameCostPrice(undefined);
    });
    txtChangeEvent();
    // 点击btn3控制 

    $("#btnShowRemark").click(function () {
        if ($("#divRemark").is(":hidden")) {
            $("#divRemark").show();
            $(this).html("隐藏商品详情");
        } else {
            $("#divRemark").hide();
            $(this).html("编辑商品详情");
        }
    });
    $("#file_upload").uploadify({
        queueID: "1",
        swf: '/Scripts/uploadify/uploadify.swf',
        uploader: '/Products/Products/Upload', // 上传文件，后台上传方法
        buttonImage: "/Content/images/uploadpic.jpg", // 按钮图片地址
        fileSizeLimit: '100000000KB', // 最大允许文件上传大小，这里是 10M
        fileTypeDesc: 'Image Files',
        fileTypeExts: '*.gif; *.jpg; *.png; *.jpeg',
        auto: true, // 自动上传
        formData: { ASPSESSID: $("#hdnSessionID").val(), AUTHID: $("#hdnAuthID").val() },
        uploadLimit: 20, // 允许文件上传个数
        queueSizeLimit: 5, // 允许同时文件上传个数
        width: 130, // 上传按钮的宽度
        height: 34, // 上传按钮的高度
        onSelect: function (file) { // 选择文件的事件
        },
        onUploadProgress: function (file, bytesUploaded, bytesTotal, totalBytesUploaded, totalBytesTotal) { // 上传文件的进度事件
        },
        onUploadSuccess: function (file, data, response) { // 上传文件成功之后的事件
            $('#jsimglist li.text').before('<li><a class="img60"><dfn></dfn><img src="' + data + '"></a><input type="hidden" name="SmallPic" value="' + data + '"></li>');
            $('#jsimglist li:not(.text)').bind('mouseenter', function () {
                $(this).addClass('hover').append('<i>删除</i>');
            }).bind('mouseleave', function () {
                $(this).removeClass('hover').find('i').remove();
            }).bind('click', function () {
                var isSuccess = true;
                var proID = parseInt($("#hdnProID").val(), 10);
                //添加商品时，直接删除图片，编辑商品需要保存才会删除图片
                if (proID == 0) {
                    isSuccess = deleteImg($(this).find('input').val());
                }
                if (isSuccess) {
                    $(this).remove();
                    var num = $('#jsimglist li:not(.text)').length;
                    $('#jsimglist li.text').text(num + '/6');
                    $('#file_upload').show();
                    $('#sfileupload').hide();
                    if (num == 0) $('#jsimglist').hide();
                }
            });
            $('#jsimglist').show();
            var num = $('#jsimglist li').length;
            num = num >= 7 ? 6 : num - 1;
            $('#jsimglist li.text').text(num + '/6')
            if (num >= 6) {
                $('#file_upload').hide();
                $('#sfileupload').show();
            } else {
                $('#file_upload').show();
                $('#sfileupload').hide();
            }
        }
    });

    var num = $('#jsimglist li:not(.text)').length;
    if (num > 0) {
        $('#jsimglist').show();
        num = num >= 7 ? 6 : num;
        $('#jsimglist li.text').text(num + '/6')
    }

    $('#jsimglist li:not(.text)').mouseenter(function () {
        $(this).addClass('hover').append('<i>删除</i>');
    }).mouseleave(function () {
        $(this).removeClass('hover').find('i').remove();
    }).click(function () {
        var isSuccess = true;
        var proID = parseInt($("#hdnProID").val(), 10);
        //添加商品时，直接删除图片，编辑商品需要保存才会删除图片
        if (proID == 0) {
            isSuccess = deleteImg($(this).find('input').val());
        }
        if (isSuccess) {
            $(this).remove();
            var num = $('#jsimglist li:not(.text)').length;
            $('#jsimglist li.text').text(num + '/6');
            $('#file_upload').show();
            $('#sfileupload').hide();
            if (num == 0) $('#jsimglist').hide();
        }
    });

    $("#IsHasUrl").click(function () {
        if ($(this).text() == "已有图片地址？") {
            $(this).text("没有图片地址？");
            $("#hasUrl").show();
            $("#noUrl").hide();
            $("#UrlType").val(1);
        } else {
            $(this).text("已有图片地址？");
            $("#hasUrl").hide();
            $("#noUrl").show();
            $("#UrlType").val(0);
        }
    });

    //提示说明
    $('#prompt1').tooltip({
        position: 'top',
        content: '输入商品属性，多个属性使用逗号隔开。例：红色，L',
        onShow: function () {
            $(this).tooltip('tip').css({
                width: "150px",
                backgroundColor: '#ffffea',
                borderColor: '#fdcb99',
                color: '#666666',
                padding: '10px',
                borderRadius: '0px'
            });
        }
    });
    $('#prompt2').tooltip({
        position: 'top',
        content: '商品SKU码是商品SKU的唯一标识，不能重复，通常使用SKU码与其它系统进行对接。可扫描商品SKU码确认对应商品（查找、校验、出入库等）',
        onShow: function () {
            $(this).tooltip('tip').css({
                width: "150px",
                backgroundColor: '#ffffea',
                borderColor: '#fdcb99',
                color: '#666666',
                padding: '10px',
                borderRadius: '0px'
            });
        }
    });
    $('#prompt3').tooltip({
        position: 'top',
        content: '可以使用国标码，也可以自定义，需要唯一。设置后可扫描商品条码确认对应商品（查找、校验、出入库等）',
        onShow: function () {
            $(this).tooltip('tip').css({
                width: "150px",
                backgroundColor: '#ffffea',
                borderColor: '#fdcb99',
                color: '#666666',
                padding: '10px',
                borderRadius: '0px'
            });
        }
    });
});

function txtChangeEvent() {
    $('input[id^="txtSellingPrice_"]').each(function () {
        $(this).numberbox({
            onChange: function (newValue, oldValue) {
                sameSellingPrice(newValue);
            }
        });
    });
    $('input[id^="txtCostPrice_"]').each(function () {
        $(this).numberbox({
            onChange: function (newValue, oldValue) {
                sameCostPrice(newValue);
            }
        });
    });
}

function addProductsSku() {
    var SellingPrice = "0.000";
    var CostPrice = "0.000";
    var rows = $(".dataTr").length + 1;
    if (rows > 0) {
        if ($("#chkSameSellingPrice").attr("checked")) {
            var txtSellingPrice = $("input[name='SellingPrice']")
            SellingPrice = $(txtSellingPrice[0]).val();
        }
        if ($("#chkSameCostPrice").attr("checked")) {
            var txtSellingPrice = $("input[name='CostPrice']")
            CostPrice = $(txtSellingPrice[0]).val();
        }
    }
    var tr = $("<tr id=\"dataTr_" + rows + "\" class=\"dataTr\"></tr>");
    var td1 = $("<th><input type=\"hidden\" id=\"ID_" + rows + "\" name=\"ID\" value=\"0\"></th>");
    td1.appendTo(tr);
    var td2 = $("<td><input type=\"text\" id=\"txtSaleprop_" + rows + "\" name=\"Saleprop\" value=\"\" class=\"easyui-validatebox\" style=\"width:190px;\" data-options=\"required:true,validType:'length[1,20]'\"></td>");
    td2.appendTo(tr);
    var td3 = $("<td><input type=\"text\" id=\"txtCode_" + rows + "\"  name=\"Code\" value=\"\" class=\"easyui-validatebox\" style=\"width:150px;\" data-options=\"required:true,validType:['code','length[1,20]']\"></td>");
    td3.appendTo(tr);
    var td4 = $("<td><input type=\"text\" id=\"txtBarCode_" + rows + "\" name=\"BarCode\" value=\"\" class=\"easyui-validatebox\" style=\"width:150px;\" data-options=\"validType:['code','length[0,20]']\"></td>");
    td4.appendTo(tr);
    var td5 = $("<td><input type=\"text\" id=\"txtSellingPrice_" + rows + "\" name=\"SellingPrice\" value=\"" + SellingPrice + "\" class=\"easyui-numberbox\" style=\"width:150px;\" min=\"0\" max=\"9999999.999\" missingmessage=\"只能输入0~9999999.999之间的数字\" data-options=\"required:true,validType:\'number\',precision:3,height:30,onChange:function(newValue, oldValue){sameSellingPrice(newValue);}\"></td>");
    td5.appendTo(tr);
    var td6 = $("<td><input type=\"text\" id=\"txtCostPrice_" + rows + "\" name=\"CostPrice\" value=\"" + CostPrice + "\" class=\"easyui-numberbox\" style=\"width:150px;\" min=\"0\" max=\"9999999.999\" missingmessage=\"只能输入0~9999999.999之间的数字\" data-options=\"required:true,validType:\'number\',precision:3,height:30,onChange:function(newValue, oldValue){sameCostPrice(newValue);}\"></td>");
    td6.appendTo(tr);
    var td7 = $("<td><a href='javascript:void(0);' class='red' onclick='deleteProductsSku(" + rows + ")' title=\"删除SKU\">×</a></td>")
    td7.appendTo(tr);
    tr.insertBefore($("#sampleTr"));
    $.parser.parse(tr);
}

function deleteProductsSku(rowIndex) {
    var productsSkuID = $("#ID_" + rowIndex).val();
    if (parseInt(productsSkuID) > 0) {
        //删除SKU
        $.messager.confirm('提示', "确定要删除吗？", function (r) {
            if (r) {
                $.ajax({
                    url: "/Products/Products/DeleteProductsSku?productsSkuID=" + productsSkuID,
                    type: "GET",
                    cache: false,
                    success: function (r) {
                        var map = $.parseJSON(r);
                        if (map.result == 1) {
                            $("#dataTr_" + rowIndex).remove();
                        } else {
                            $.MsgBox.Alert("提示", map.message);
                        }
                    },
                    error: function () {
                        $.MsgBox.Alert("提示", "删除失败！");
                    }
                });
            }
        });
    } else {
        $("#dataTr_" + rowIndex).remove();
    }
}

function deleteImg(smallPic) {
    var isSuccess = false;
    $.ajax({
        url: "/Products/Products/DeleteImg?smallPic=" + smallPic,
        async: false,
        type: "GET",
        cache: false,
        success: function (r) {
            var map = $.parseJSON(r);
            if (map.result == 1) {
                isSuccess = true;
            } else {
                isSuccess = false;
                $.MsgBox.Alert("提示", map.message);
            }
        },
        error: function () {
            isSuccess = false;
            $.MsgBox.Alert("提示", "删除失败！");
        }
    });
    return isSuccess;
}

function btnSave() {
    $("#Remark").val(editor.document.getBody().getHtml());
    $('#ff').form('submit', {
        url: "/Products/Products/Save",
        type: "POST",//使用get方法访问后台
        dataType: "json",
        onSubmit: function () {
            var isValid = $(this).form('validate');
            if (isValid){
                var SaleType = $("input[name='SaleType']");
                if (!($(SaleType[0]).attr("checked") || $(SaleType[1]).attr("checked"))) {
                    $.MsgBox.Alert("提示", "请选择类型！");
                    isValid = false;
                }
            }
            return isValid;	// 返回false终止表单提交
        },
        success: function (r) {
            var map = $.parseJSON(r);
            if (map.result == 1) {
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

function sameSellingPrice(firstSellingPrice) {
    if ($("#chkSameSellingPrice").attr("checked")) {
        $('input[id^="txtSellingPrice_"]').each(function (i) {
            if (undefined == firstSellingPrice) {
                firstSellingPrice = $(this).numberbox("getValue");
            }
            $(this).numberbox("setValue", firstSellingPrice);
        });
    }
}

function sameCostPrice(firstCostPrice) {
    if ($("#chkSameCostPrice").attr("checked")) {
        $('input[id^="txtCostPrice_"]').each(function (i) {
            if (undefined == firstCostPrice) {
                firstCostPrice = $(this).numberbox("getValue");
            }
            $(this).numberbox("setValue", firstCostPrice);
        });
    }
}