function playSound(url) {
    var borswer = window.navigator.userAgent.toLowerCase();
    if (borswer.indexOf("ie") >= 0) {
        //IE内核浏览器
        var strEmbed = '<embed id="embedPlay" name="embedPlay" src="' + url + '" autostart="true" hidden="true" loop="false"></embed>';
        if ($("body").find("embed").length > 0) {
            $("body").find("embed").remove();
        }
        $("body").append(strEmbed);
        var embed = document.getElementById("embedPlay");

        //浏览器不支持 audio，则使用 embed 播放
        try {
            embed.volume = 100;
            embed.play();
        } catch (e) { }
    } else {
        //非IE内核浏览器
        var strAudio = '<audio id="audioPlay" src="' + url + '" hidden="true">';
        if ($("body").find("audio").length > 0) {
            $("body").find("audio").remove();
        }
        $("body").append(strAudio);
        var audio = document.getElementById("audioPlay");
        //浏览器支持 audion
        try {
            audio.play();
        } catch (e) { }
    }
}