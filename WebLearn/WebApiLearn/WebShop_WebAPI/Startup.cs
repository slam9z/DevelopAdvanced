using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;
using System.Web.Http;
using WebShop.App_Start;
using System.Web.Mvc;
using WebShopCommon.Dal;
using System.Configuration;
using System.Threading.Tasks;
using System.Diagnostics;

[assembly: OwinStartup(typeof(WebShop.Startup))]

namespace WebShop
{

	using AppFunc = Func<IDictionary<string, object>, Task>;

	public partial class Startup
	{

		public void Configuration(IAppBuilder app)
		{
			AreaRegistration.RegisterAllAreas();

			// Configure web api
			//var config = new HttpConfiguration();
			//WebApiConfig.Register(config);

			//config.EnsureInitialized();


			DbConfig.ConnectionString = ConfigurationManager.ConnectionStrings["DbContext"].ConnectionString;
			WebApiConfig.Register(GlobalConfiguration.Configuration);
			//FilterConfig.RegisterGlobalFilters(GlobalConfiguration.Configuration);
			GlobalConfiguration.Configuration.EnsureInitialized();


			app.Use(
			(context, next) =>
			{
				PrintMessage("FuncMiddleWare1");
				UpdateInvokeCount(context.Environment);
				return next.Invoke();
			}
			);

			Func<AppFunc, AppFunc> func = DelegateMiddleWare;
			app.Use(func);

			app.Use(new InvokeMiddleWare());
			app.Use(new InitializeMiddleWare());
			app.Use(typeof(TypeMiddleWare));

			//app.Use(
			//(context, next) =>
			//{
			//	PrintMessage("FuncMiddleWare2");
			//	UpdateInvokeCount(context.Environment);
			//	return next.Invoke();
			//}
			//);

		}

		public AppFunc DelegateMiddleWare(AppFunc next)
		{
			PrintMessage("DelegateMiddleWare");
			return next;
		}

		public class InitializeMiddleWare
		{
			AppFunc _next;
			OwinMiddleware _middleware;
			public void Initialize()
			{
				Console.Write("InitializeMiddleWare");
			}

			public void Initialize(AppFunc next)
			{
				PrintMessage("InitializeMiddleWare 2");
				_next = next;
			}

			/// <summary>
			/// 两种都可以,优先选择AppFunc  ，Microsoft.Owin.Infrastructure.AppFuncTransition会进行装换。
			/// </summary>
			/// <param name="middleware"></param>
			public void Initialize(OwinMiddleware middleware)
			{
				PrintMessage("InitializeMiddleWare 2");
				_middleware = middleware;
			}

			public Task Invoke(IDictionary<string, object> environment)
			{
				PrintMessage("InitializeMiddleWare invoke");
				UpdateInvokeCount(environment);
				if (_next != null)
				{
					return _next.Invoke(environment);
				}

				var context = new OwinContext(environment);
				return _middleware.Invoke(context);

			}

		}

		/// <summary>
		/// 无法进行处理
		/// </summary>
		public class InvokeMiddleWare
		{
			public void Invoke()
			{
				PrintMessage("InvokeMiddleWare");
			}

			//public void Invoke(object middleWare)
			//{
			//	Console.Write("InvokeMiddleWare 2");
			//}

			//可能在IIS环境下不可用?
			//Microsoft.Owin.Host.SystemWeb.IntegratedPipeline.IntegratedPipelineContext  DefaultAppInvoked
			public AppFunc Invoke(AppFunc next)
			{
				PrintMessage("InvokeMiddleWare 3");
				return next;
			}

			public OwinMiddleware Invoke(OwinMiddleware middleware)
			{
				PrintMessage("InvokeMiddleWare 3");
				return middleware;
			}

		}
		public class TypeMiddleWare
		{
			private AppFunc _next;
			OwinMiddleware _middleware;
			public TypeMiddleWare()
			{
				PrintMessage("TypeMiddleWare");
			}

			//public TypeMiddleWare(object next)
			//{
			//	_next = next;
			//	PrintMessage("TypeMiddleWare ctor");
			//}

			//public TypeMiddleWare(AppFunc next)
			//{
			//	_next = next;
			//	PrintMessage("TypeMiddleWare ctor");
			//}

			public TypeMiddleWare(OwinMiddleware middleware)
			{
				_middleware = middleware;
				PrintMessage("TypeMiddleWare ctor");
			}
			//	{Microsoft.Owin.Host.SystemWeb.CallEnvironment.AspNetDictionary}
			public Task Invoke(IDictionary<string, object> environment)
			{
				PrintMessage("TypeMiddleWare invoke");
				UpdateInvokeCount(environment);
				if (_next != null)
				{
					return _next.Invoke(environment);
				}
				var context = new OwinContext(environment);
				return _middleware.Invoke(context);
			}
		}

		public static void PrintMessage(string msg)
		{
			Debug.WriteLine(msg);
		}

		const string InvokeCount = "InvokeCount";
		public static void UpdateInvokeCount(IDictionary<string, object> environment)
		{
			IDictionary<string, string[]> responseHeaders =
		   (IDictionary<string, string[]>)environment["owin.ResponseHeaders"];

			int number;
			if (responseHeaders.ContainsKey(InvokeCount))
			{
				number = Convert.ToInt32(responseHeaders[InvokeCount][0]) + 1;
			}
			else
			{
				number = 1;
			}
			responseHeaders[InvokeCount] = new string[] { number.ToString() };

			PrintMessage(string.Format("InvokeCount {0}", number));
		}

	}
}
