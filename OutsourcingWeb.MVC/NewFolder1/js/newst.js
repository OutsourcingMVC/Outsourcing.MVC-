// ActionScript Document
(function (){
		var oDiv = $('.update');
		var oUl = oDiv.find('ul');
		var iH = 0;
		var arrData = [
			{ 'name':'萱萱', 'time':4, 'title':'那些灿烂华美的瞬间', 'url':'http://www.miaov.com/2013/' },
			{ 'name':'畅畅', 'time':5, 'title':'广东3天抓获涉黄疑犯', 'url':'http://www.miaov.com/2013/#curriculum' },
			{ 'name':'萱萱', 'time':6, 'title':'国台办回应王郁琦', 'url':'http://www.miaov.com/2013/#about' },
			{ 'name':'畅畅', 'time':7, 'title':'那些灿烂华美的瞬间', 'url':'http://www.miaov.com/2013/#message' },
			{ 'name':'萱萱', 'time':8, 'title':'那些灿烂华美的瞬间', 'url':'http://www.miaov.com/2013/' },
			{ 'name':'畅畅', 'time':9, 'title':'广东3天抓获涉黄疑犯', 'url':'http://www.miaov.com/2013/#curriculum' },
			{ 'name':'萱萱', 'time':10, 'title':'国台办回应王郁琦', 'url':'http://www.miaov.com/2013/#about' },
			{ 'name':'畅畅', 'time':11, 'title':'那些灿烂华美的瞬间', 'url':'http://www.miaov.com/2013/#message' }
		];
		var str = '';
		var oBtnUp = $('#updateUpBtn');
		var oBtnDown = $('#updateDownBtn');
		var iNow = 0;
		var timer = null;
		
		for ( var i=0; i<arrData.length; i++ ) {
			str += '<li><a href="'+ arrData[i].url +'"><strong>'+ arrData[i].name +'</strong> <span>'+ arrData[i].time +'分钟前</span> 写了一篇新文章：'+ arrData[i].title +'…</a></li>';
		}
		oUl.html( str );
		
		iH = oUl.find('li').height();

		oBtnUp.click(function (){
			doMove(-1);
		});
		oBtnDown.click(function (){
			doMove(1);
		});
		
		oDiv.hover(function (){
			clearInterval( timer );
		}, autoPlay);
		
		function autoPlay() {
			timer = setInterval(function () {
				doMove(-1);
			}, 3500);
		}
		autoPlay();
		
		function doMove( num ) {
			iNow += num;
			if ( Math.abs(iNow) > arrData.length-1 ) {
				iNow = 0;
			}
			if ( iNow > 0 ) {
				iNow = -(arrData.length-1);
			}
			oUl.stop().animate({ 'top': iH*iNow }, 2200, 'elasticOut');
		}
	})();