[Content Negotiation in ASP.NET Web API](http://www.asp.net/web-api/overview/formats-and-model-binding/content-negotiation)    

The primary mechanism for content negotiation in HTTP are these request headers:

* Accept: Which media types are acceptable for the response, such as “application/json,” “application/xml,” or a custom media type such as "application/vnd.example+xml"
* Accept-Charset: Which character sets are acceptable, such as UTF-8 or ISO 8859-1.
* Accept-Encoding: Which content encodings are acceptable, such as gzip.
* Accept-Language: The preferred natural language, such as “en-us”.

##Serialization

1. If a Web API controller returns a resource as CLR type, the pipeline serializes the return value and writes it into the HTTP response body. 


2. A controller can also return an HttpResponseMessage object. To specify a CLR object for the response body, call the CreateResponse extension method:

    ``` C#
    public HttpResponseMessage GetProduct(int id)
    {
        var item = _products.FirstOrDefault(p => p.ID == id);
        if (item == null)
        {
            throw new HttpResponseException(HttpStatusCode.NotFound);
        }
        return Request.CreateResponse(HttpStatusCode.OK, product);
    }

    ```




##How Content Negotiation Works

``` C#

public HttpResponseMessage GetProduct(int id)
{
    var product = new Product() 
        { Id = id, Name = "Gizmo", Category = "Widgets", Price = 1.99M };

    IContentNegotiator negotiator = this.Configuration.Services.GetContentNegotiator();

    ContentNegotiationResult result = negotiator.Negotiate(
        typeof(Product), this.Request, this.Configuration.Formatters);
    if (result == null)
    {
        var response = new HttpResponseMessage(HttpStatusCode.NotAcceptable);
        throw new HttpResponseException(response));
    }

    return new HttpResponseMessage()
    {
        Content = new ObjectContent<Product>(
            product,		        // What we are serializing 
            result.Formatter,           // The media formatter
            result.MediaType.MediaType  // The MIME type
        )
    };
}

```


