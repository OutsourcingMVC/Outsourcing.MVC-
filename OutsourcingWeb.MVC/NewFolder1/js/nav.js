// JavaScript Document
function nav(){
	  var oList = document.getElementById('list');
	  var aH2 = oList.getElementsByTagName('h2');
	  var aU = oList.getElementsByTagName('ul');
	  var i = 0;
	  var aLi = null;
	  var arr = [];

	  for(i=0;i<aH2.length;i++){
		  aH2[i].index = i;
		  aH2[i].onclick = function(){
			  for(var i=0;i<aH2.length;i++){
				  if(i != this.index){
					  aH2[i].className = '';
					  aU[i].style.display = 'none';
				  }
				  
			  }

			  if(aH2[this.index].className == '')
			  {
				   aH2[this.index].className = 'active';
				   aU[this.index].style.display = 'block';
			  }
			  else
			  {
				   aH2[this.index].className = '';
				   aU[this.index].style.display = 'none';
			  }
			  
		  }
	  }

	  for(i=0;i<aU.length;i++){
		 var aLi = aU[i].getElementsByTagName('li');
		 for(j=0;j<aLi.length;j++)
		 {
			 arr.push(aLi[j]);
		 }
	  }

	  for(i=0;i<arr.length;i++){
		  arr[i].onclick = function(){
			  for(i=0;i<arr.length;i++){
				   arr[i].className = '';
			  }
			  this.className = 'hover';
		  }
	  }
  }
  nav();

