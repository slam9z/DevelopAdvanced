



##HTTP协议

[HTTP协议详解CSDN](http://blog.csdn.net/gueter/article/details/1524447)


##HTTP请求.NET实现

###WebRequest(HttpWebRequest)


* RequestHeaders
* UserAgent
* BeginGetRequestStream与EndGetRequestStream  IAsyncResult

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

* BeginGetResponse与EndGetResponse 

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



###WebClient

###HttpClient


##.NET相应HTTP请求
