[Action Results in Web API 2](http://www.asp.net/web-api/overview/getting-started-with-aspnet-web-api/action-results)

A Web API controller action can return any of the following:

1. void
2. HttpResponseMessage
3. IHttpActionResult
4. Some other type 

Depending on which of these is returned, Web API uses a different mechanism to create the HTTP response. 

| Return type | How Web API creates the response |
| ------------- | ------------- |
| void | Return empty 204 (No Content)  |
| HttpResponseMessage | Convert directly to an HTTP response message.  | 
|  IHttpActionResult | Call ExecuteAsync to create an HttpResponseMessage, then convert to an HTTP response message. | 
|  Other type Write | the serialized return value into the response body; return 200 (OK). | 

The rest of this topic describes each option in more detail.


###void

Get请求返回405(Method Not Allowed)，Post请求返回204(No Content).
Get方法返回void 没有任何意义。

###HttpResponseMessage

###IHttpActionResult

The IHttpActionResult interface was introducted in Web API 2. Essentially, 
it defines an HttpResponseMessage factory. Here are some advantages of using the IHttpActionResult interface:

* Simplifies unit testing your controllers. 
* Moves common logic for creating HTTP responses into separate classes.
* Makes the intent of the controller action clearer, by hiding the low-level details of constructing the response. 


###Other Return Types

For all other return types, Web API uses a [media formatter](http://www.asp.net/web-api/overview/formats-and-model-binding/media-formatters) 
to serialize the return value. Web API writes the serialized value into the response body.
The response status code is 200 (OK).

A disadvantage of this approach is that you cannot directly return an error code, such as 404.
However, you can throw an HttpResponseException for error codes.
For more information, see [Exception Handling in ASP.NET Web API](http://www.asp.net/web-api/overview/web-api-routing-and-actions/exception-handling).

Web API uses the Accept header in the request to choose the formatter