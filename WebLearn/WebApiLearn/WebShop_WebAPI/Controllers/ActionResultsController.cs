using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using WebShopCommon.Models;

namespace WebShop.Controllers
{

	public class TextResult : IHttpActionResult
	{
		string _value;
		HttpRequestMessage _request;

		public TextResult(string value, HttpRequestMessage request)
		{
			_value = value;
			_request = request;
		}
		public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
		{
			var response = new HttpResponseMessage()
			{
				Content = new StringContent(_value),
				RequestMessage = _request
			};
			return Task.FromResult(response);
		}
	}


	public class ActionResultsController : ApiController
	{
		/// <summary>
		/// 实际返回405，而不是204 (No Content). Get方法为405，Post为204.
		/// </summary>
		public void PostData()
		{
		}


//HTTP/1.1 200 OK
//Cache-Control: max-age=1200
//Content-Length: 10
//Content-Type: text/plain; charset=utf-16
//Server: Microsoft-IIS/10.0
//InvokeCount: 3
//X-AspNet-Version: 4.0.30319
//X-SourceFiles: =?UTF-8?B? RjpcU291cmNlXERldmVsb3BBZHZhbmNlZFxXZWJMZWFyblxXZWJBcGlMZWFyblxXZWJTaG9wX1dlYkFQSVxhcGlcQWN0aW9uUmVzdWx0c1xHZXREYXRh?=
//X-Powered-By: ASP.NET
//Date: Fri, 08 Apr 2016 09:57:36 GMT

//hello

		/// <summary>
		/// 一个与方法名相同的text文件,内容是hello
		/// </summary>
		/// <returns></returns>
		public HttpResponseMessage GetData()
		{
			//HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, "value");

			HttpResponseMessage response = new HttpResponseMessage();
			response.Content = new StringContent("hello", Encoding.Unicode);
			response.RequestMessage = Request;
			//response.Headers.CacheControl = new CacheControlHeaderValue()
			//{
			//	MaxAge = TimeSpan.FromMinutes(20)
			//};
			return response;
		}


//HTTP/1.1 200 OK
//Cache-Control: no-cache
//Pragma: no-cache
//Content-Length: 5
//Content-Type: text/plain; charset=utf-8
//Expires: -1
//Server: Microsoft-IIS/10.0
//InvokeCount: 3
//X-AspNet-Version: 4.0.30319
//X-SourceFiles: =?UTF-8?B? RjpcU291cmNlXERldmVsb3BBZHZhbmNlZFxXZWJMZWFyblxXZWJBcGlMZWFyblxXZWJTaG9wX1dlYkFQSVxhcGlcQWN0aW9uUmVzdWx0c1xHZXRUZXh0?=
//X-Powered-By: ASP.NET
//Date: Fri, 08 Apr 2016 10:04:11 GMT

//hello

		/// <summary>
		/// 显示在网页上
		/// </summary>
		/// <returns></returns>
		public IHttpActionResult GetText()
		{
			return new TextResult("hello", Request);
		}


		public Account GetAccount()
		{
			return new Account() { UserName = "Test" };
		}

	}
}
