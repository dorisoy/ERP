 $(function () {
       var aa = $("input[name='IsEnable']:checkbox");
       aa.bind("click", function () {

           if ($(this).attr("checked")) {
               $("#IsEnable").val(1);
           }
           else {
               $("#IsEnable").val(0);
           }
       })
   });
//用户名唯一性判断
$("#Code").blur(function () {
    $.ajax({
        url: "/sys/User/CheckUserCode",
        data: { "UserCode": $("#Code").val(), "ID": $("#ID").val() },
        cache: false,
        success: function (r) {
            var map = $.parseJSON(r);
            if (map.result == -1) {
               // alert("用户名已存在");
                $.MsgBox.Alert("提示", "用户名已存在！", 1000);
                $("#Code").focus();
            }
        },
        error: function () {
           // alert("读取失败");
            $.MsgBox.Alert("提示", "读取失败！", 1000);
        }
    });
});
$("#btnClose").click(function () {
    // parent.$('#win').window('close');
    location.href = "/shop/ShopStock/index";
});
$("#btnSave").click(function () {
    btnSave();
});
function btnSave() {
    $('#ff').form('submit', {
        url: "/shop/ShopStock/Save",
        type: "POST",//使用get方法访问后台
        dataType: "json",
        onSubmit: function () {
            var isValid = $(this).form('validate');
            return isValid;	// 返回false终止表单提交
        },
        success: function (r) {
            var map = $.parseJSON(r);
            if (map.result == 1) {
                //当前tab
                location.href = "/shop/ShopStock/index";
            }
            else {
                $.MsgBox.Alert("提示", map.message);
            }
        },
        error: function () {
            $.MsgBox.Alert("提示", "保存失败！");
        }
    });
}