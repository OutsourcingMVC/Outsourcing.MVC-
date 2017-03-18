// JavaScript Document
function mv(){
		var oSpan=document.getElementById('pan');
		var oBox=document.getElementById('box');
		var oUl=oBox.getElementsByTagName('ul')[0];
		var aLiUl=oUl.getElementsByTagName('li');
		var oOl=oBox.getElementsByTagName('ol')[0];
		var aLiOl=oOl.getElementsByTagName('li');
		var str=['111','222','精灵美女','古装美女']
		var num=0;
		
		oSpan.innerHTML=str[0];
		for(var i=0; i<aLiOl.length; i++){
			aLiOl[i].index=i;
			aLiOl[i].onclick=function(){
				for(var i=0; i<aLiOl.length; i++){
					aLiOl[i].className='';
					aLiUl[i].style.display='none';				
					aLiUl[i].style.fliter='alpha(opacity=0)';
					aLiUl[i].style.opacity=0;
					}
				this.className='active';
				aLiUl[this.index].style.display='block';
				startMove(aLiUl[this.index],{opacity:100});
				oSpan.innerHTML=str[this.index];
				}
			}
		};
		mv();
		
		
		
		
