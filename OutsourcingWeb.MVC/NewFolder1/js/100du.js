
$(function (){

	// update文字弹性滑动
	(function (){
		var oDiv = $('.update');
		var oUl = oDiv.find('ul');
		var oLi = oUl.find('li');
		var iH = 0;
		var str = '';
		var oBtnUp = $('#updateUpBtn');
		var oBtnDown = $('#updateDownBtn');
		var iNow = 0;
		var timer = null;
		
		
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
			if ( Math.abs(iNow) > oLi.length-1 ) {
				iNow = 0;
			}
			if ( iNow > 0 ) {
				iNow = -(oLi.length-1);
			}
			oUl.stop().animate({ 'top': iH*iNow }, 2200, 'elasticOut');
		}
	})();
	
	
	
	
	
});