



##HTTP协议

[HTTP协议详解CSDN](http://blog.csdn.net/gueter/article/details/1524447)


##HTTP请求.NET实现

###WebRequest(HttpWebRequest)

	WebRequest Request for URI. request/response model 

* RequestHeaders
* UserAgent
* Create(Uri)
* BeginGetRequestStream与EndGetRequestStream(IAsyncResult)或者或者GetRequestStream

	获取body的写入流
``` C#
  var asyncResult = request.BeginGetRequestStream((ar) =>
                    {
                        try
                        {
                            var currentRequest = (HttpWebRequest)ar.AsyncState;
                            using (var outputStream = currentRequest.EndGetRequestStream(ar))
                            {
                                outputStream.Write(content, 0, content.Length);
                            }
								ProcessStringResponse(item, currentRequest, succeededCallBack, failCallBack, userState);
                        }
                        catch (Exception wexp)
                        {
                            }
                        }
                    }, request);
```

* BeginGetResponse与EndGetResponse 或者GetResponse 

``` C#
		var resAsyncResult = request.BeginGetResponse((resar) =>
            {
                try
                {
                    var currentRequest = (HttpWebRequest)resar.AsyncState;
                    var response = (HttpWebResponse)currentRequest.EndGetResponse(resar);
                    item.ResponseStatusCode = response.StatusCode;
                    string strResponse = string.Empty;
                    using (var inputStream = response.GetResponseStream())
                    {
                        var reader = new StreamReader(inputStream, Encoding.UTF8);
                        strResponse = reader.ReadToEnd();
                    }
                    item.ResponseContent = strResponse;
                    item.DeserializeOutputs();
                    if (succeededCallBack != null)
                    {
                        succeededCallBack.Invoke(item, false, userState);
                    }
					  if (item.CanBeCached && Cache != null)
                    {
                        Cache[item.CacheKey] = strResponse;
                    }
                    _logger.Debug(string.Format("Get Response with URL:{0}, Method:{1},Got a strResponse:{2} .", request.RequestUri, item.Method, strResponse));
                }
                catch (Exception wexp)
                {
                    _logger.Error(string.Format("Get Response with URL:{0}, Method:{1},Got a exception:{2} .", request.RequestUri, item.Method, wexp));
                }
            }, request);
        }
```

* upload file(form)

	ContentType Multipart/form-data与boundary,boundary是为了支持同时上传多个文件的。

``` C#
	const string end = "\r\n";
	const string twoHyphens = "--";
	var boundary = string.Format("--------------WI{0}", Guid.NewGuid().ToString("N"));
	var contentType = string.Format("multipart/form-data; boundary={0}", boundary);
	var contentDisposition = "Content-Disposition: form-data; name=\"" + fileFormName + "\"; filename=\"" + uploadedFileName + "\"";
	var twoHyphensbytes = Encoding.UTF8.GetBytes(twoHyphens);
	var boundaryBytes = Encoding.UTF8.GetBytes(boundary);
	var endBytes = Encoding.UTF8.GetBytes(end);
	var contentDispositionbytes = Encoding.UTF8.GetBytes(contentDisposition);
	var contentTypebytes = Encoding.UTF8.GetBytes("Content-Type: application/octet-stream");
```

[Multipart/form-data POST文件上传详解](http://blog.csdn.net/xiaojianpitt/article/details/6856536)



[WebRequest类](https://msdn.microsoft.com/zh-cn/library/system.net.webrequest(v=vs.110).aspx)

[HttpWebRequest类](https://msdn.microsoft.com/zh-cn/library/system.net.httpwebrequest(v=vs.110).aspx)


###WebClient
	
	只是用来处理Uri资源的。

* OpenRead(Uri)
* OpenWrite(Uri,Method)
* UploadFile和UploadData

[WebClient类](https://msdn.microsoft.com/zh-cn/library/system.net.webclient(v=vs.110).aspx)

[WebClient 用法小结](http://www.cnblogs.com/hfliyi/archive/2012/08/21/2649892.html)

###HttpClient

	Windows.Web.Http与System.Net.Http有一套相同的API。

	HttpClient优势

	(1) 可以在HttpClient实例上配置扩展，设置默认的头部，取消未完成的的请求和设置

	(2) HttpClient有自己的连接池

	(3) HttpClient 不与特定的服务器绑定，可以访问任何Http请求

	(4) HttpClient采用异步请求处理（更加先进的异步）

	(5) 有拦截功能


* HttpResponseMessage
	
	所有方法都返回这个对象
	
	* Content(.NET是HttpContent WinRt是IHttpContent)
	* StautsCode 
	* ReasonPhrase	

[HttpResponseMessage类](https://msdn.microsoft.com/zh-cn/library/system.net.http.httpresponsemessage(v=vs.118).aspx)

* HttpRequestMessage

	* Content
	* Method 
	* RequestUri	
	* Headers(get only)
	* Properties	

这些类封装的还不错，基本上将请求都包括了。属性也十分好理解

[HttprequestMessage类](https://msdn.microsoft.com/zh-cn/library/system.net.http.httprequestmessage(v=vs.118).aspx)

* HttpContent(abstract class)

	* CopyToAsync(Stream)
	* ReadAsByteArrayAsync  
	* ReadAsStreamAsync  
	* ReadAsStringAsync 



``` C#
System.Object 
  System.Net.Http.HttpContent
    System.Net.Http.ByteArrayContent
    System.Net.Http.MultipartContent
		System.Net.Http.MultipartFormDataContent 
    System.Net.Http.StreamContent
```

```C#
public static async Task<string> Upload(byte[] image)
{
     using (var client = new HttpClient())
     {
         using (var content =
             new MultipartFormDataContent("Upload----" + DateTime.Now.ToString(CultureInfo.InvariantCulture)))
         {
             content.Add(new StreamContent(new MemoryStream(image)), "bilddatei", "upload.jpg");
              using (
                 var message =
                     await client.PostAsync("http://www.directupload.net/index.php?mode=upload", content))
              {
                  var input = await message.Content.ReadAsStringAsync();
                  return !string.IsNullOrWhiteSpace(input) ? Regex.Match(input, @"http://\w*\.directupload\.net/images/\d*/\w*\.[a-z]{3}").Value : null;
              }
          }
     }
}
```
上面的3个重要的要素都很齐全。

[HttpContent类](https://msdn.microsoft.com/en-us/library/system.net.http.httpcontent(v=vs.118).aspx)

[C# HttpClient 4.5 multipart/form-data upload](http://stackoverflow.com/questions/16416601/c-sharp-httpclient-4-5-multipart-form-data-upload)

* IHttpContent(Windows.Web.Http)

	* BufferAllAsync  Serialize the HTTP content into memory as an asynchronous operation. 
	* ReadAsBufferAsync  Serialize the HTTP content to a buffer as an asynchronous operation. 
	* ReadAsInputStreamAsync  Serialize the HTTP content and return an input stream that represents the content as an asynchronous operation. 
	* ReadAsStringAsync  Serialize the HTTP content to a String as an asynchronous operation. 
	* TryComputeLength  Determines whether the HTTP content has a valid length in bytes. 
	* WriteToStreamAsync  Write the HTTP content to an output stream as an asynchronous operation. 

```
IHttpContent - A base interface for developers to create their own content objects. It represents an HTTP entity body and content headers. This interface has methods that get and set the actual content data. It also provides properties that get and set content related headers.
• HttpBufferContent - HTTP content that uses a buffer.
• HttpFormUrlEncodedContent - HTTP content that uses name/value tuples encoded with the application/x-www-form-urlencoded MIME type.
• HttpMultipartContent - HTTP content that uses multipart/* MIME type.
• HttpMultipartFormDataContent - HTTP content that uses the encoded multipart/form-data MIME type.
• HttpStreamContent - HTTP content that uses a stream. This content type is used by the HTTP methods to receive data and HTTP methods to upload data.
• HttpStringContent - HTTP content that uses a string. 
```

[IHttpContent接口](https://msdn.microsoft.com/en-us/library/windows/apps/windows.web.http.ihttpcontent.aspx)

* DeleteAsync(requestUri)
* GetAsync(requestUri)还有许多其它具体方法
* PostAsync(requestUri,content)
* PutAsync(Uri requestUri,HttpContent content)
* SendAsync(HttpRequestMessage request) (.NET)
* SendRequestAsync(HttpRequestMessage)  （Winrt）



	Send方法因为可以指定Method所以应该可以实现Delete Get Post Put的功能。这种API的设计还不错。

* Download file

``` C#
	var uri = new Uri(url);
	var httpClient = new HttpClient();
	{
		HttpResponseMessage response = await httpClient.GetAsync(uri);
		using (Stream responseStream = (await response.Content.ReadAsInputStreamAsync()).AsStreamForRead())
		{
			var fcache = GetCache(cache);
			await fcache.SaveFileToCache(responseStream, fileName);
			return Path.Combine(GetCachePath(cache), fileName);
		}
	}
```

*Upload file

``` C#
	HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post,new Uri(url));
	stream.Seek(0, SeekOrigin.Begin);
	var streamContent = new HttpStreamContent(stream.AsInputStream());
	request.Content = streamContent;
	request.Content.Headers.ContentType = new HttpMediaTypeHeaderValue(contentType);
	HttpClient client = new HttpClient();
	try
	{
		client.DefaultRequestHeaders.Add("AccessToken", accessToken);
		var response = await client.SendRequestAsync(request);
		var responseContent = response.Content;
		var responseString = await response.Content.ReadAsStringAsync();
	}
	catch (Exception ex)
    {
	}
```

* HttpClinet类在.NET与Winrt之间的差异
	
	* Content在Winrt是IHttpContent的接口.NET是HttpCotent抽象类。

	* .NET使用HttpMessageHandler，构造函数的参数，Winrt使用IHttpFilter为参数，虽然类名称不一样但是作用也是一样。
HttpMessageHandler(The HTTP handler stack to use for sending requests. )，SendAsync的方法可以做拦截处理。
IHttpFilter接口的SendRequestAsync方法

[IHttpFilter interface](https://msdn.microsoft.com/zh-cn/library/windows/apps/windows.web.http.filters.ihttpfilter.aspx)
[HttpMessageHandler](https://msdn.microsoft.com/en-us/library/system.net.http.httpmessagehandler(v=vs.118).aspx)
	

里面介绍了HttpMessageHandler的用法。
[ASP.NET MVC Web API 学习笔记----HttpClient简介](http://www.cnblogs.com/qingyuan/archive/2012/11/08/2760034.html)

[Calling a Web API From a .NET Client in ASP.NET Web API 2](http://www.asp.net/web-api/overview/advanced/calling-a-web-api-from-a-net-client)

[HttpClinet类-.NET](https://msdn.microsoft.com/en-us/library/system.net.http.httpclient(v=vs.118).aspx)

[HttpClinet类-WinRt](https://msdn.microsoft.com/zh-cn/library/windows/apps/windows.web.http.aspx)


##.NET响应HTTP请求

	这个可能不算这个范畴，主要是Web服务端，以后再考虑这个问题吧
