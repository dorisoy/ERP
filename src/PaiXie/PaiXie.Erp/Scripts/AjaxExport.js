; (function ($) {
    $.extend($, {
        AjaxExport: function (options) {
            var options = $.extend({
                url: "",
                type: "post",
                dataType: "json",
                cache: false,
                data:{},
                divExportId: 'localWin',
                title: "导出报表",
                fileTitle: "导出报表",
                beforeSend: function () {
                    $('#' + options.divExportId).window({
                        title: options.title,
                        width: 400,
                        height: 150,
                        content: '',
                        modal: true,
                        minimizable: false,
                        maximizable: false,
                        shadow: false,
                        cache: false,
                        closed: false,
                        collapsible: false,
                        resizable: false,
                        loadingMessage: '正在加载数据，请稍等片刻......'
                    });
                },
                success: function (data) {
                    window.$check_File_Exist;
                    templates.find(".hdnFileName").val(data.fileName);
                    templates.find(".hdnFileMapPath").val(data.fileMapPath);
                    templates.find(".hdnDownTaskId").val(data.downTaskId);
                }
            }, options);

            ////导出模板
            var templates = $(
                            + '<div style="height:150px;">'
                            + '<div class="showFin" style="display:none; height:100%;">'
                            + '<div style="padding-top:40px;">'
                            + '<div style="float:left;margin:-10px 0px 0px 60px; width:45px;"><img src="../../Content/images/icon_victory.gif" border="0" /></div>'
                            + '<a href="javascript:window.$get_DownLoad_File(\'' + options.divExportId + '\');" style="text-decoration:none;"><label class="lblFileTitle"></label>文件已生成，点击这里下载！</a>'
                            + '</div>'
                            + '<input type="hidden" class="hdnFileName" value="" />'
                            + '<input type="hidden" class="hdnFileMapPath" value="" />'
                            + '<input type="hidden" class="hdnDownTaskId" value="" />'
                            + '<iframe src="" class="iframeDownLoadFile" style="display: none">'
                            + '</iframe>'
                            + '</div>'
                            + '<div class="showWait" style="height:100%;padding-top:35px;">'
                            + '<div style="float:left;margin:-8px 0px 0px 50px; width:45px;"><img src="../../Content/images/loading.gif" border="0" /></div>数据正在生成，请稍等。。。 (已生成<label class="lbl_Task_Progress">0</label>条)'
                            + '</div>'
                            + '</div>'
                            );
            $('#' + options.divExportId).html("");
            templates.find(".lblFileTitle").html(options.fileTitle);
            templates.find(".showFin").hide();
            templates.find(".showWait").show();
            $('#' + options.divExportId).prepend(templates);
            $.ajax(options);
            window.clearInterval(window.$check_File_Exist);
            window.$check_File_Exist = window.setInterval('window.$ajax_check_File_Exist();', 1500);

            ////显示进度
            if (typeof window.$ajax_check_File_Exist == 'undefined') {
                window.$ajax_check_File_Exist = function () {
                    var _FileMapPath = $('#' + options.divExportId).find(".hdnFileMapPath").val();; //注意：需要把反斜杠替换为双反斜杠
                    var _DownTaskId = $('#' + options.divExportId).find(".hdnDownTaskId").val();;
                    $.ajax({
                        type: "POST",
                        url: "/Export/CheckFileExist?r=" + Math.random(),
                        data: { fileMapPath: _FileMapPath, downTaskId: _DownTaskId },
                        success: function (msg) {
                            if (msg.toString() == "-1") {
                                window.clearInterval(window.$check_File_Exist);
                                window.$get_DownLoad_File(options.divExportId);
                            } else {
                                var divExport = $("#" + options.divExportId);
                                if (divExport.is(":visible")) {
                                    $('#' + options.divExportId).find(".lbl_Task_Progress").text(msg);
                                }
                                else {
                                    window.clearInterval(window.$check_File_Exist);
                                    window.$check_File_Exist = undefined; //重新定义为undefined，下次可重新启动定时器
                                }
                            }
                        }
                    });
                }
            }

            ////下载文件
            if (typeof window.$get_DownLoad_File == 'undefined') {
                window.$get_DownLoad_File = function (divExportId) {
                    var divExport = $("#" + divExportId);
                    if (divExport.is(":visible")) {
                        divExport.find(".showFin").show();
                        divExport.find(".showWait").hide();
                        var FileName = divExport.find(".hdnFileName").val();
                        if (FileName.length > 0) {
                            divExport.find(".iframeDownLoadFile").attr("src", "/Export/Download?fileName=" + encodeURIComponent(FileName) + "&t=" + Math.random());
                            window.$check_File_Exist = undefined;
                        }
                    }
                };
            }
        }
    });

})(jQuery);
