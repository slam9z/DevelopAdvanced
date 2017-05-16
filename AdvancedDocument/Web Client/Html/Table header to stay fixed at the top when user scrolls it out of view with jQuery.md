[Table header to stay fixed at the top when user scrolls it out of view with jQuery](http://stackoverflow.com/questions/4709390/table-header-to-stay-fixed-at-the-top-when-user-scrolls-it-out-of-view-with-jque)

##answer

You would do something like this by tapping into the scroll event handler on window, and using another 
table with a fixed position to show the header at the top of the page.

HTML:

```html
<table id="header-fixed"></table>
```

CSS:

```css
#header-fixed {
    position: fixed;
    top: 0px; display:none;
    background-color:white;
}
```

JavaScript:

```js
var tableOffset = $("#table-1").offset().top;
var $header = $("#table-1 > thead");
var $fixedHeader = $("#header-fixed").append($header.clone());

$(window).bind("scroll", function() {
    var offset = $(this).scrollTop();
    
    if (offset >= tableOffset && $fixedHeader.is(":hidden")) {
        $fixedHeader.show();
        
        $.each($header.find('tr > th'), function(ind,val){
          var original_width = $(val).width();
          $($fixedHeader.find('tr > th')[ind]).width(original_width);
        });
    }
    else if (offset < tableOffset) {
        $fixedHeader.hide();
    }
});
```

This will show the table head when the user scrolls down far enough to hide the original table head.
 It will hide again when the user has scrolled the page up far enough again.

Working example: http://jsfiddle.net/noahkoch/wLcjh/1/