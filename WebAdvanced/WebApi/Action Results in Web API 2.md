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
