 
//唯一性判断
$("#Name").blur(function () {
    $.ajax({
        url: "/shop/shops/CheckName",
        data: { "Name": $("#Name").val(), "ID": $("#ID").val() },
        cache: false,
        success: function (r) {
            var map = $.parseJSON(r);
            if (map.result == -1) {
                $.MsgBox.Alert("提示", "店铺名称已存在！", 1000);
                $("#Name").focus();
            }
        },
        error: function () {
            $.MsgBox.Alert("提示", "读取失败！", 1000);
        }
    });
});
//选择不同平台类型
function change()
{
    var obj = document.getElementsByName("Typeradio");
    for (var i = 0; i < obj.length; i++) {
        if (obj[i].checked) {

            if (obj[i].value == "1") {
                $('#ptlxvisual').show();


                $('#ak').show();
                $('#ast').show();
                $('#asn').show();

                $('#mddz').hide();

            }
            else if (obj[i].value == "2") {
                $('#ptlxvisual').hide();


                $('#ak').show();
                $('#ast').show();
                $('#asn').show();

                $('#mddz').hide();
            }
            else {
                $('#ptlxvisual').hide();


                $('#ak').hide();
                $('#ast').hide();
                $('#asn').hide();

                $('#mddz').show();
            }
        }
    }

}
//关闭
$("#btnClose").click(function () {
    location.href = "/shop/shops/index";
});
//保存
$("#btnSave,#tsave").click(function () {
    btnSave();
});
function btnSave() {
    var obj = document.getElementsByName("Typeradio");
    for (var i = 0; i < obj.length; i++) {
        if (obj[i].checked) {
            $('#Type').val(obj[i].value);
        }
    }

    $('#StoreAddr').val($('#SelectF').combobox('getValue') + "," + $('#SelectW').combobox('getValue') + "," + $('#SelectL').combobox('getValue') + "," + $('#dadd').val());

    $('#ff').form('submit', {
        url: "/shop/shops/Save",
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
                var src = "/shop/shops/index?ram=" + Math.random();
                var current_tab = parent.$('#worktab').tabs('getSelected');
                parent.$('#worktab').tabs('update', {
                    tab: current_tab,
                    options: {
                        content: '  <iframe id="frmWorkArea0031" scrolling="no" data-options="fit:true" frameborder="0" style="  width: 100%; height: 100%; overflow: hidden;" src="' + src + '"></iframe>  '
                    }
                });
                parent.$('#win').window('close');
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