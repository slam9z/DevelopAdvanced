[jQuery.param()](https://api.jquery.com/jQuery.param/)


Description: Create a serialized representation of an array, a plain object, or a jQuery 
object suitable for use in a URL query string or Ajax request. In case a jQuery object is 
passed, it should contain input elements with name/value properties.


```js
var params = { width:1680, height:1050 };
var str = jQuery.param( params );

//width=1680&height=1050
```