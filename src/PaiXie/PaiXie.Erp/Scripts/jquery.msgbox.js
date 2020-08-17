/**
 *	jquery.msgbox 5.0 - 2010-04-17
 *
 *  Author: pwwang
 *  Website: pwwang.com
 *  Note: All the stuff written by pwwang
 *	      Feel free to do whatever you want with this file
 *        Please keep the distribution
 *
 **/
(function($) {

	$.msgbox = function(o) {

		if(typeof(o) == 'string'){ o = { content:o } }                          // 如果参数给出字符串， 则直接进行提示（text）
		var opts = o || {};                                                     // 用于接收参数
		
		opts.width             = o.width 		         || 360;                    // 提示框的宽度
		opts.height            = o.height 		       || 200,                    // 提示框的高度
		opts.autoClose         = o.autoClose 	       || 0;                      // 自动关闭的时间, 0则不会自动关闭
		opts.title 			       = o.title 		         || '提示',                 // 提示框标题
		opts.wrapperClass	     = o.wrapperClass      || 'msgbox_wrapper';       // 提示框外框class
		opts.titleClass 	     = o.titleClass 	     || 'msgbox_title';         // 提示框标题class
		opts.closeClass        = o.closeClass        || 'msgbox_close';         // 提示框关闭按钮class
		opts.titleWrapperClass = o.titleWrapperClass || 'msgbox_title_wrapper'; // 提示框标题行class
		opts.mainClass 		     = o.mainClass   	     || 'msgbox_main';          // 内容框class
		opts.bgClass 		       = o.bgClass 	         || 'msgbox_bg';            // 背景层class
		opts.buttonClass	     = o.buttonClass       || 'msgbox_button';        // 内容框中button的class
		opts.inputboxClass     = o.inputboxClass     || 'msgbox_inputbox';      // 内容框中input box的class
		opts.type 			       = o.type 		         || 'text';                 // 内容的类型
		// support:  text, url(=get), iframe, confirm, alert; confirm, alert is added in version 4.0, input added in V5.0
		opts.content 		       = o.content 	         || 'Hello, world!';        // 内容
		opts.onClose           = o.onClose           || function(){};          // 关闭时执行的事件 
		opts.closeImg	  	     = o.closeImg	         || '';                     // 关闭按钮图标, 空则显示"关闭"
		opts.bgOpacity		     = o.bgOpacity	       || 0.6;   			            // from 0 to 1  背景透明度
		opts.onAjaxed   	     = o.onAjaxed          || function(){};          // ajax执行完之后的事件 
		opts.onInputed         = o.onInputed         || function(){};
		opts.enableDrag        = typeof o.enableDrag != 'boolean' ? true : o.enableDrag; // 默认允许拖拽
		opts.bgAnimate         = typeof o.bgAnimate  != 'boolean' ? true : o.bgAnimate;  // 开启背景动画
		opts.boxAnimate        = typeof o.boxAnimate != 'boolean' ? true : o.boxAnimate; // 开启提示框动画
		
		var returnValue = false;        // 返回值, 用于confirm和input
		var relTop      = 0;            // 提示框离窗口上边的距离
		var relLeft     = 0;            // 提示框离窗口左边的距离, 用于页面滚动时保持窗口不动
		
		var $background = $("<div>")
			.attr('id', 'jMsgboxBg')    // 用来进行外部关闭
			.css({
				 'position'	: 'absolute',
				 'top'		: '0',
				 'left'		: '0',
				 'z-index'	: '9999',
				 'opacity'  : '0'
			})
			.addClass(opts.bgClass)
			.appendTo('body')
			.dblclick(closeMe)          // 双击关闭提示框
			.click(function(){          // 单击闪烁提示框
				flashTitle(0.5, 4, 80);
			});
			
		if(opts.bgAnimate)              // 背景渐变
			$background.animate({'opacity':opts.bgOpacity});
		else
			$background.css('opacity',opts.bgOpacity);

		var $wrapper = $("<div>")
			.attr('id', 'jMsgboxBox')   // 用来进行外部关闭
			.css({
				'width' 	: opts.width + 'px',
				'height' 	: opts.height + 'px',
				'position' 	: 'absolute',
				'z-index'	: '10000',
				'display'   : 'none'
			})
			.addClass(opts.wrapperClass)
			.appendTo('body');
		if(opts.boxAnimate)             // 展开提示框
			$wrapper.slideDown("slow"); 
		else
			$wrapper.css('display', '');
		
		var $titleWrapper = $('<ul><li>提示</li><li>关闭</li></ul>')
			.addClass(opts.titleWrapperClass)
			.appendTo($wrapper);
			
		var $titleLi = $("li:first", $titleWrapper)
			.html(opts.title)
			.addClass(opts.titleClass);
			
		var $closeLi = $titleLi.next()
			.addClass(opts.closeClass)
			.mousedown(closeMe)         // 关闭提示框
		if(opts.closeImg != '')
			$closeLi.html("<img src="+ opts.closeImg +" border=0 />"); // 载入关闭图标
						
		var $main = $(document.createElement("div"))
			.addClass(opts.mainClass)
			.appendTo($wrapper);
			
		$main.height( opts.height - $titleWrapper.outerHeight(true) - $main.outerHeight(true) + $main.height() ); // 计算内容框高度

		function closeMe(){
			if(opts.boxAnimate)
				$wrapper.slideUp('slow');
			else $wrapper.remove();
			if(opts.bgAnimate)
				$background.fadeOut();
			else $background.remove();
			opts.onClose(returnValue);				
		}
		
		function isVisible(){
			return	$background.is(":visible") &&
					$wrapper.is(":visible");
		}

		function autoCloseMe(autoClose){			
			if( autoClose > 0 && isVisible() ){ // prevent manually closing, 防止人为关闭后,计时器还在运行
				autoCloseStr = autoClose + " 秒后关闭 ...";
				$titleLi.html(opts.title + " &nbsp; " + autoCloseStr);		
				autoClose --;
				if( autoClose == 0 ) 
					closeMe();	
				setTimeout(function(){ autoCloseMe(autoClose) }, 1000);	
			}		
		}
		
		function resetPosition() {
			$background.css({
				 'width'	: document.documentElement.scrollWidth + 'px',
				 'height'	: document.documentElement.scrollHeight + 'px'
			});
			relLeft = ($(window).width() - opts.width)/2;
			relTop = ($(window).height() - opts.height)/2;
			fixBox();     // 定位初始位置
		}
		
		function flashTitle(opacity, times, interval, flag){ // 闪烁标题(模拟windows)
			if( times > 0 ) {
				flag = !flag;
				op = flag ? opacity : 1;
				$titleWrapper.css('opacity',op);	
				setTimeout(function(){ flashTitle(opacity, times-1, interval, flag) }, interval);
			}
		}
		
		function fixBox(){  // 定位box
			$wrapper.css({
				'top'		: $(window).scrollTop() + relTop + 'px',
				'left'	 	: $(window).scrollLeft() + relLeft + 'px'			 
			});	
		}
		
		function msgbox(){	// 按类型填充内容
			switch(opts.type){
				case 'input':
					$main.html(opts.content);
					var $inputbox = $("<input type='text' />")
						.appendTo($main)
						.addClass(opts.inputboxClass);
					var $buttonWrapper = $("<div>")
						.css({
							'text-align':'center',
							'padding':'15px 0'
						})
						.appendTo($main);
					var $yesButton = $("<input type=button value=' OK '>")
						.appendTo($buttonWrapper)
						.addClass(opts.buttonClass)
						.after(" &nbsp; &nbsp; ")
						.click(function(){
							opts.onInputed($inputbox.val());  // 返回输入的值
							closeMe();
						});
					var $noButton = $("<input type=button value=' Cancel '>")
						.appendTo($buttonWrapper)
						.addClass(opts.buttonClass)
						.click(closeMe);
					break;
				case 'alert':
					$main.html(opts.content);
					var $buttonWrapper = $("<div>")
						.css({
							'text-align':'center',
							'padding':'15px 0'
						})
						.appendTo($main);
					var $OKButton = $("<input type=button value=' OK '>")
						.appendTo($buttonWrapper)
						.addClass(opts.buttonClass)
						.click(closeMe);
					break;
				case 'confirm':
					$main.html(opts.content);
					//oneBox();
					var $buttonWrapper = $("<div>")
						.css({
							'text-align':'center',
							'padding':'15px 0'
						})
						.appendTo($main);
					var $yesButton = $("<input type=button value=' Yes '>")
						.appendTo($buttonWrapper)
						.addClass(opts.buttonClass)
						.after(" &nbsp; &nbsp; ")
						.click(function(){
							returnValue = true;
							closeMe();
						});
					var $noButton = $("<input type=button value=' No '>")
						.appendTo($buttonWrapper)
						.addClass(opts.buttonClass)
						.click(function(){
							returnValue = false;
							closeMe();
						});
					break;					
				case 'get':
				case 'ajax':
				case 'url':
					$main.text("Loading ...").load(
						opts.content,
						'',
						function(data){
							(opts.onAjaxed)(data);	
						}
					);
					break;			
				case 'iframe':
					$("<iframe frameborder=0 marginheight=0 marginwidth=0></iframe>")
						.appendTo($main)
						.attr({
							'width'			: '100%',
							'height'		: '100%',
							'scrolling' 	: 'auto',
							'src'			: opts.content
						});
					break;
				default:
					$main.html(opts.content);
			}	

		}
		
		resetPosition();
		
		$(window)
			.load(resetPosition)		// just in case user is changing size of page while loading
			.resize(resetPosition)
			.scroll(fixBox);

		msgbox();	//填充内容
		
		if( opts.autoClose > 0 )  // 自动关闭
			autoCloseMe(opts.autoClose);
			
		o.outClose = closeMe;   // 从外部执行关闭
		
		if( opts.enableDrag )
			$wrapper.Drags({  // 允许拖拽
				handler : $titleWrapper,
				onMove: function(){ $(window).unbind('scroll') },
				onDrop: function(){
					relTop = $wrapper.getCss('top') - $(window).scrollTop();
					relLeft = $wrapper.getCss('left') - $(window).scrollLeft();
					$(window).scroll(fixBox);
				}
			});
		
		return this;
	}	
	
	$.closemsgbox = function(o){
		o = o || window.document;
		if( o.constructor=='[object HTMLDocument]' )
			o = { document : o };
		var opts = o || {};
		opts.document   = o.document || window.document;                      // 关闭哪个页面的msgbox
		opts.bgAnimate  = typeof o.bgAnimate  == 'undefined' ? true : o.bgAnimate;     // 开启背景动画
		opts.boxAnimate = typeof o.boxAnimate == 'undefined' ? true : o.boxAnimate;    // 开启提示框动画
		opts.onClose   = o.onClose || function(){};
		
		//if(o.getElementById){
    var $wrapper = $(o.getElementById('jMsgboxBox'));
    var $background = $(o.getElementById('jMsgboxBg'));
		//} else {
    //  var $wrapper = $('#jMsgboxBox', opts.document);
    //  var $background = $('#jMsgboxBg', opts.document);
		//}
		if(opts.boxAnimate)
			$wrapper.slideUp('slow');
		else $wrapper.remove();
		if(opts.bgAnimate)
			$background.fadeOut();
		else $background.remove();
		opts.onClose();
	}
	
})(jQuery);
