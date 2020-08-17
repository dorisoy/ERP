//初始化
$(function () {
  
    var outInStockID = $("#hdnOutInStockID").val();
   
    $("#file_upload").uploadify({
        queueID: "1",
        swf: '/Scripts/uploadify/uploadify.swf',
        uploader: '/Warehouse/WarehouseInStock/ImportInStock?outInStockID=' + outInStockID, // 上传文件，后台上传方法
        buttonImage: "/Content/images/uploadpic.jpg", // 按钮图片地址
        fileSizeLimit: '200000000KB', // 最大允许文件上传大小，这里是 20M
        fileTypeDesc: 'Excel Files',
        fileTypeExts: '*.xls;*.xlsx',
        auto: true, // 自动上传
        formData: { ASPSESSID: $("#hdnSessionID").val(), AUTHID: $("#hdnAuthID").val() },
        uploadLimit: 1, // 允许文件上传个数
        queueSizeLimit: 1, // 允许同时文件上传个数
        width: 130, // 上传按钮的宽度
        height: 34, // 上传按钮的高度
        onSelect: function (file) { // 选择文件的事件
            $('#spanProcessBar').html("<img src='../../Content/images/loading.gif' width='16px' height='16px' /> 导入中...");
        },
        onUploadProgress: function (file, bytesUploaded, bytesTotal, totalBytesUploaded, totalBytesTotal) { // 上传文件的进度事件
        },
        onUploadSuccess: function (file, data, response) { // 上传文件成功之后的事件
            var map = $.parseJSON(data);
            if (map.result == 1) {
                $('#spanProcessBar').html("导入成功！");
            } else {
               // $('#file_upload').uploadifyCancel(1);
                $('#spanProcessBar').html("<font style='color:#ff0000;'>" + map.message + "</font>");
                setTimeout(function () { location.href = location.href; }, 1000);
               
            }
            setTimeout(function () {             
               //入库数量
                $.ajax({
                    url: "/Warehouse/WarehouseInStock/getProductsNum?ram=" + Math.random() + "&outInStockID=" + outInStockID,
                    type: "GET",
                    cache: false,
                    success: function (r) {
                       
                        parent.$("#rksl").text(r);
                        
                    },
                    error: function () {
                      
                    }
                });
                parent.$("#btnSearch").click();
                parent.$('#localWin').window('close')
            }, 3000);
        }
    });
});