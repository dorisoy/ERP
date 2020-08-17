var _area = "";
$(function () {


    $.getJSON("http://api.map.baidu.com/location/ip?ak=F454f8a5efe5e577997931cc01de3974&callback=?", function (d) {
        _area = d.content.address;

    });
});


function EnterPress(e) {
          var e = e || window.event;
           
          if (e.keyCode == 13) {
              $('#login').click();
          }
      }

//判断显示用户登录的时候显示整个页面
if (window.parent.window != window) {
    window.top.location.href = "/Home/Index";
}
      
$("#login").click(function () {
  
    var postData = {
        UserCode: $("#txtUid").val(),
        yzm: $("#txtyzm").val(),
        UserPassword: $("#txtPwd").val(),
        Area: _area,
        checkme: $("#checkme").attr("checked")
    };
    $.ajax({
        url: "/Login/DoAction",
        type: "GET",
        cache: false,
        async: true,
        data: postData,
        dataType: "text",
        success: function (result) {
            if (result == "OK") {
                $("#errmsg").text("登录成功，请稍候...");
                window.location.href = "/Home/Index";
            } else {
                $("#errmsg").text(result);
                ClickRemoveChangeCode();
            }
        },
        error: function () {
            $("#errmsg").text("登录失败！");
            ClickRemoveChangeCode();
        }
    });

});
       
        

$("#reset").click(function () {
    //window.location.href = "/Login/Index";
    $("#txtUid").val("");
    $("#txtPwd").val("");
});