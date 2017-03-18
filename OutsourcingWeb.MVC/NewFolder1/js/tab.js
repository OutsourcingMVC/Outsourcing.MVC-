// JavaScript Document
 	function tab(){
		var oTab=document.getElementById('new_tab');
		
		var aUl =oTab.getElementsByTagName('ul');
		
		var oTit=document.getElementById('tittle1');
		
		var aA = oTit.getElementsByTagName('a');
		//alert(aA )
		for( var i=0;i<aA.length; i++){
			aA[i].index=i;
			aA[i].onmouseover=function(){
				for( var i=0;i<aA.length; i++){
					aA[i].className = '';
					aUl[i].style.display='none';
				}
			 	this.className = 'active';
				aUl[this.index].style.display='block';
			}
		}
	}
	tab();