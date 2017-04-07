[Querying an inline style property in JQuery](http://stackoverflow.com/questions/5563806/querying-an-inline-style-property-in-jquery)


```
$('.inputBox', this).each(function(index) {
   var stylestemp = $(this).attr('style').split(';');
   var styles = {};
   var c = '';
   for (var x = 0, l = stylestemp.length; x < l; x++) {
     c = stylestemp[x].split(':');
     styles[$.trim(c[0])] = $.trim(c[1]);
   }
   widthArray[widthArray.length] = styles.width;
});
``