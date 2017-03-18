// JavaScript Document


function mv()
{
	var oBox=document.getElementById('box');
	var oUl=oBox.getElementsByTagName('ul')[0];
		var aLiUl=oUl.getElementsByTagName('li');
		var oBtn=document.getElementById('btn')
		var aLiOl=oBtn.getElementsByTagName('li');
		var oneHeight=aLiUl[0].offsetHeight;
		var iNow=0;
		var iNow2=0;
		
		
		var timer=null;
		for(var i=0; i<aLiOl.length; i++){
			aLiOl[i].index=i;
			aLiOl[i].onclick=function(){
				for(var i=0; i<aLiOl.length; i++){
					aLiOl[i].className='';
					}
				this.className='active1';
					startMove(oUl, {top:-this.index*oneHeight});
					iNow=this.index; 
					iNow2=this.index;
				}
			}
			
			
			timer=setInterval(toRun, 2000);
			oBox.onmouseover=function(){
				clearInterval(timer);
				}
			oBox.onmouseout=function(){
				timer=setInterval(toRun, 2000);
				}	
				
			
			 function toRun(){
				 if(iNow==0)
				 {
					aLiUl[0].style.position='static';
					oUl.style.top=0;
					iNow2=0; 
				 }
				 if(iNow==aLiOl.length-1)
				 {
					iNow=0; 
					aLiUl[0].style.position='relative';
					aLiUl[0].style.top=aLiUl.length*oneHeight+'px';
					
				 }
				else(iNow++);
				iNow2++;
				
				
				for(var i=0; i<aLiOl.length; i++){
					aLiOl[i].className='';
					}
					
					aLiOl[iNow].className='active1';
					startMove(oUl, {top:-iNow2*oneHeight});
				}

}
