function getStyle(obj, attr)
	{
		if(obj.currentStyle)
		{
			return obj.currentStyle[attr];	
		}
		else
		{
			return getComputedStyle(obj, false)[attr];
		}	
	}
	var timer=null;
	function startMove(obj, json, fn)
	{
		clearInterval(obj.timer);
		obj.timer=setInterval(function(){
			bStop=true;
			for(var attr in json)
			{
				var iCur=0;
				
				if(iCur!=json[attr])
				{
					bStop=false;
				}
				if(attr=='opacity')
				{
					iCur=parseInt(parseFloat(getStyle(obj, attr))*100);
				}
				else
				{
					iCur=parseInt(getStyle(obj, attr));
				}
				
				var iSpeed=(json[attr]-iCur)/8;
				iSpeed=iSpeed>0?Math.ceil(iSpeed):Math.floor(iSpeed);
				
				if(attr=='opacity')
				{
					obj.style.filter='alpha(opacity:'+(iCur+iSpeed)+')';
					obj.style.opacity=(iCur+iSpeed)/100;
				}
				else
				{
					obj.style[attr]=iCur+iSpeed+'px';	
				}		
			}
			if(bStop)
			{
				clearInterval(timer);
					if(fn)
					{
						fn();
					}		
			}
		},30)	
	}
