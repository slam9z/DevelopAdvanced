[JSON string to JS object](http://stackoverflow.com/questions/2257117/json-string-to-js-object)



Some modern browsers have support for parsing JSON into a native object:

```js
var var1 = '{"cols": [{"i" ....... 66}]}';
var result = JSON.parse(var1);
```

For the browsers that don't support it, you can download json2.js from json.org for safe parsing of a JSON object. The script will check for native JSON support and if it doesn't exist, provide the JSON global object instead. If the faster, native object is available it will just exit the script leaving it intact. You must, however, provide valid JSON or it will throw an error — you can check the validity of your JSON with http://jslint.com or http://jsonlint.com.
