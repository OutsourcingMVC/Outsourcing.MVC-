// JavaScript Document
function Player(buttons,scrollContent,hoverClass,timeout){
	hoverClass = hoverClass|| 'hover';
	timeout = timeout || 3000;
	this.buttons = buttons;
	this.scrollContent=scrollContent;
	this.hoverClass = hoverClass;
	this.timeout = timeout;
	
	this.curItem=buttons[0];
	this.curItem.index=0;
	var _this=this;
	for(var i=0;i<this.buttons.length;i++){
		this.buttons[i].onmouseover=function(){
			_this.stop();
			_this.invoke(this.index);
		};
		this.buttons[i].onmouseout=function(){
			_this.start();
		};
		this.buttons[i].index=i;
	}
	this.invoke(0);
}
Player.prototype={
		start:function(){
			this.stop();
			var _this = this;
			this.interval=setInterval(function(){
				_this.next();
			},this.timeout);
		},
		stop:function(){
			clearInterval(this.interval);
		},
		invoke:function(n){
			
			this.curItem=this.buttons[n];
			this.curItem.index=n;
			
			//现将所有样式回复如初
			this.recoverButtonsClass();
			
			this.scrollContent.style.top=(-n*350)+"px";
			this.curItem.className = this.hoverClass;
		},
		next:function(){
			var nextIndex = this.curItem.index+1;
			if(nextIndex>=this.buttons.length){
				nextIndex=0;
			}
			this.invoke(nextIndex);
		},
		recoverButtonsClass:function(){
			for(var i=0;i<this.buttons.length;i++){
				this.buttons[i].className="";
			}
		}
};
window.onload=function(){
	var buttons = getByClass("buttons",document.body)[0].getElementsByTagName("LI");
	var scrollContent = getByClass("scrollContent",document.body)[0];
	var player = new Player(buttons,scrollContent,undefined,3000);
	player.start();//开始播放
//	player.invoke(3);//切换播放
	//player.stop();//停止播放
};
/*
* var div = document.getElementsByClassName("scrollFrame");
* 该方法不支持IE9以下的浏览器
*/

function getByClass(className,context){
	context = context || document;
	if( context.getElementsByClassName ){
		return context.getElementsByClassName(className);
	}
	var nodes = context.getElementsByTagName("*"),
	    ret = [];
	for(var i=0;i<nodes.length;i++){
		if(hasClass(nodes[i],className))ret.push(nodes[i]);
	}
	return ret;
};

function hasClass(node,className){
	var names = node.className.split(/\s+/);
	for(var i=0;i<names.length;i++){
		if(names[i]==className)return true;
	}
	return false;
}
function animate(o,start,alter,dur,fx){
	var curTime=0;
	var t = setInterval(function(){
		if(curTime>=dur)clearTimeout(t);
		for(var i in start){
			o.style[i]=fx(start[i],alter[i],curTime,dur)+"px";
		}
		curTime+=3000;
	},3000);
}

function Quad(start,alter,curTime,dur){
	return start+Math.pow(curTime/dur,2)*alter;
}
