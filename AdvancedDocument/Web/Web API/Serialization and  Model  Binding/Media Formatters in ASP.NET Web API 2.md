[Media Formatters in ASP.NET Web API 2](http://www.asp.net/web-api/overview/formats-and-model-binding/media-formatters)  


## Internet Media Types

A media type, also called a MIME type, identifies the format of a piece of data.
 In HTTP, media types describe the format of the message body. A media type consists of two strings,
 a type and a subtype. For example:

* text/html
* image/png
* application/json

When an HTTP message contains an entity-body, the Content-Type header specifies the format of the message body. 
This tells the receiver how to parse the contents of the message body.

For example, if an HTTP response contains a PNG image, the response might have the following headers.

```
HTTP/1.1 200 OK
Content-Length: 95267
Content-Type: image/png
```

When the client sends a request message, it can include an Accept header. The Accept header tells the server which media type(s) 
the client wants from the server. For example:

```
Accept: text/html,application/xhtml+xml,application/xml
```

This header tells the server that the client wants either HTML, XHTML, or XML. 

The media type determines how Web API serializes and deserializes the HTTP message body. 
Web API has built-in support for XML, JSON, BSON, and form-urlencoded data,
 and you can support additional media types by writing a media formatter.

To create a media formatter, derive from one of these classes:

* MediaTypeFormatter. This class uses asynchronous read and write methods.
* BufferedMediaTypeFormatter. This class derives from MediaTypeFormatter but uses sychronous read/write methods.

Deriving from BufferedMediaTypeFormatter is simpler, because there is no asynchronous code,
 but it also means the calling thread can block during I/O. 
 

##Example: Creating a CSV Media Formatter


##Adding a Media Formatter to the Web API Pipeline

To add a media type formatter to the Web API pipeline, use the Formatters property on the HttpConfiguration object.

``` C#
public static void ConfigureApis(HttpConfiguration config)
{
    config.Formatters.Add(new ProductCsvFormatter()); 
}
```

##Character Encodings


