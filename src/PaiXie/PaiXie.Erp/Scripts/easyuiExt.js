//confirm 
function Confirm(msg) {
    //$.messager.confirm('提示', msg, function (r) {
    //    if (r) {
    //        eval(control.toString().slice(11));
    //    }
    //});

    $.messager.confirm('提示', msg, function (r) {
             if (r){   
                   alert('ok');   
                 }   
      });  

    return false;


}

//load
function Load() {
    $("<div class=\"datagrid-mask\"></div>").css({ display: "block", width: "100%", height: $(window).height() }).appendTo("body");
    $("<div class=\"datagrid-mask-msg\"></div>").html("正在运行，请稍候。。。").appendTo("body").css({ display: "block", left: ($(document.body).outerWidth(true) - 190) / 2, top: ($(window).height() - 45) / 2 });
}

//display Load
function dispalyLoad() {
    $(".datagrid-mask").remove();
    $(".datagrid-mask-msg").remove();
}

//弹出提醒框alert
function showMsg(title, msg, isAlert) {
    if (isAlert !== undefined && isAlert) {
        $.messager.alert(title, msg);
    } else {
        $.messager.show({
            title: title,
            msg: msg,
            showType: 'show'
        });
    }
}

//删除确认confirm
function deleteConfirm() {
    return showConfirm('温馨提示', '确定要删除吗?');
}

//弹出确认框confirm
//  showConfirm("11", "22", sf); sf函数名
function showConfirm(title, msg, callback) {
    $.messager.confirm(title, msg, function (r) {
        if (r) {
            if (jQuery.isFunction(callback))
                callback.call();
        }
    });
}

//进度条
function showProcess(isShow, title, msg) {
    if (!isShow) {
        $.messager.progress('close');
        return;
    }
    var win = $.messager.progress({
        title: title,
        msg: msg
    });
}

//弹出框体window
// showMyWindow("test", "/Demo/Student/PopShowContent?id=1888", 600, 400, true, false, false);    
function showMyWindow(title, href, width, height, modal, minimizable, maximizable) {

    parent.$('#win').window({

        title: title,

        width: width === undefined ? 600 : width,

        height: height === undefined ? 400 : height,

        content: '<iframe scrolling="auto" frameborder="0"  src="' + href + '" style="width:100%;height:100%;"></iframe>',

        //        href: href === undefined ? null : href,

        modal: modal === undefined ? true : modal,

        minimizable: minimizable === undefined ? false : minimizable,

        maximizable: maximizable === undefined ? false : maximizable,

        shadow: false,

        cache: false,

        closed: false,

        collapsible: false,

        resizable: false,

        loadingMessage: '正在加载数据，请稍等片刻......'
        //onClose:function(){
        //    var o=$('iframe选择器').contents().find('iframe加载的页面对象选择器')

        //    $('父页容器选择器').html(o.html()或者o.val())//o.val()是输入控件，o.html()是非输入控件

        //}

    });

}

function showLocalWindow(title, href, width, height, modal, minimizable, maximizable) {

    $('#localWin').window({

        title: title,

        width: width === undefined ? 600 : width,

        height: height === undefined ? 400 : height,

        content: '<iframe scrolling="auto" frameborder="0"  src="' + href + '" style="width:100%;height:100%;"></iframe>',

        //        href: href === undefined ? null : href,

        modal: modal === undefined ? true : modal,

        minimizable: minimizable === undefined ? false : minimizable,

        maximizable: maximizable === undefined ? false : maximizable,

        shadow: false,

        cache: false,

        closed: false,

        collapsible: false,

        resizable: false,
        top: ($(window).height() - height) * 0.1,
        left: ($(window).width() - width) * 0.5,
        loadingMessage: '正在加载数据，请稍等片刻......'
        //onClose:function(){
        //    var o=$('iframe选择器').contents().find('iframe加载的页面对象选择器')

        //    $('父页容器选择器').html(o.html()或者o.val())//o.val()是输入控件，o.html()是非输入控件

        //}

    });

}

function UpdatePwdwin(title, href, width, height, modal, minimizable, maximizable) {

    $('#win').window({

        title: title,

        width: width === undefined ? 600 : width,

        height: height === undefined ? 400 : height,

        content: '<iframe scrolling="auto" frameborder="0"  src="' + href + '" style="width:100%;height:100%;"></iframe>',

        //        href: href === undefined ? null : href,

        modal: modal === undefined ? true : modal,

        minimizable: minimizable === undefined ? false : minimizable,

        maximizable: maximizable === undefined ? false : maximizable,

        shadow: false,

        cache: false,

        closed: false,

        collapsible: false,

        resizable: false,

        loadingMessage: '正在加载数据，请稍等片刻......'
        //onClose:function(){
        //    var o=$('iframe选择器').contents().find('iframe加载的页面对象选择器')

        //    $('父页容器选择器').html(o.html()或者o.val())//o.val()是输入控件，o.html()是非输入控件

        //}

    });

}



//关闭弹出框体 window
function closeMyWindow() {

    parent.$('#win').window('close');

}