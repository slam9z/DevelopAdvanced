[jQuery.ajax()](http://api.jquery.com/jQuery.ajax/)

###contentType (default: 'application/x-www-form-urlencoded; charset=UTF-8')
Type: Boolean or String 
When sending data to the server, use this content type. Default is "application/x-www-form-urlencoded;
charset=UTF-8", which is fine for most cases. If you explicitly pass in a content-type to $.ajax(), 
then it is always sent to the server (even if no data is sent). As of jQuery 1.6 you can pass false to
tell jQuery to not set any content type header. Note: The W3C XMLHttpRequest specification dictates
that the charset is always UTF-8; specifying another charset will not force the browser to change 
the encoding.

>Note: For cross-domain requests, setting the content type to anything other than application/x-www-form-urlencoded,
>multipart/form-data, or text/plain will trigger the browser to send a preflight OPTIONS request to the server.

###dataType (default: Intelligent Guess (xml, json, script, or html))
Type: String 
The type of data that you're expecting back from the server. If none is specified, jQuery will try to infer it
 based on the MIME type of the response (an XML MIME type will yield XML, in 1.4 JSON will yield a JavaScript 
 object, in 1.4 script will execute the script, and anything else will be returned as a string). The available
  types (and the result passed as the first argument to your success callback) are: 

* "xml": Returns a XML document that can be processed via jQuery.
* "html": Returns HTML as plain text; included script tags are evaluated when inserted in the DOM.
* "script": Evaluates the response as JavaScript and returns it as plain text. Disables caching by appending 
a query string parameter, _=[TIMESTAMP], to the URL unless the cache option is set to true. Note: This will
 turn POSTs into GETs for remote-domain requests.

* "json": Evaluates the response as JSON and returns a JavaScript object. Cross-domain "json" requests a
re converted to "jsonp" unless the request includes jsonp: false in its request options. The JSON data 
is parsed in a strict manner; any malformed JSON is rejected and a parse error is thrown. As * of jQuery 1.9, 
an empty response is also rejected; the server should return a response of null or {} instead. (See json.org 
for more information on proper JSON formatting.)

* "jsonp": Loads in a JSON block using JSONP. Adds an extra "?callback=?" to the end of your URL to specify 
the callback. Disables caching by appending a query string parameter, "_=[TIMESTAMP]", to the URL unless
 the cache option is set to true.

* "text": A plain text string.

multiple, space-separated values: As of jQuery 1.5, jQuery can convert a dataType from what it received
 in the Content-Type header to what you require. For example, if you want a text response to be treated as 
 XML, use "text xml" for the dataType. You can also make a JSONP request, have it received  as text, and 
 interpreted by jQuery as XML: "jsonp text xml". Similarly, a shorthand string such as "jsonp xml" will 
 first attempt to convert from jsonp to xml, and, failing that, convert from jsonp to text, and then from
  text to xml.