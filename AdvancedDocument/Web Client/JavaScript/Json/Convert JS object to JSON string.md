[Convert JS object to JSON string](http://stackoverflow.com/questions/4162749/convert-js-object-to-json-string)

##Question


If I defined an object in JS with:
var j={"name":"binchen"};
How can I convert the object to JSON? The output string should be:
'{"name":"binchen"}'

##Answer

Modern browsers (IE8, FF3, Chrome etc.) have native JSON support built in (Same API as with JSON2).
So as long you're not dealing with IE6/7 you can do it just as easily as that:

```
var j={"name":"binchen"};
JSON.stringify(j); // '{"name":"binchen"}'
```

But to add support for the oldie's, you should also include the json2 script