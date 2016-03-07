



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

###HttpClient


##.NET相应HTTP请求
