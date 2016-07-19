$(".news-dropdown").mouseover(function () {
     if(this.id == "new1")
         $("#new11").show();
     else 
         $("#new22").show();

});

$(".news-dropdown").mouseout(function () {
    
      $(".submenu").hide();

});



