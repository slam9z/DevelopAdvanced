*AppFunc（Func<IDictionary<string, object>, Task>与 OwinMiddleware是通用的，由Microsoft.Owin.Infrastructure.AppFuncTransition与OwinMiddlewareTransition会进行装换*


总结，本文主要讲了AppBuilder的Use方法的具体流程与扩展，三种扩展方法实际上都是对基础Use的封装，而基础Use方法总的来说可以接受四种middlewareObject

1. Delegate是最简单的，直接可以封装成三元组压入List
 
2. 有Initialize方法的类的实例，参数表第一项为一个AppFunc或者OwinMiddleware，只要其Invoke之后能返回一个Task就行，为了避免DynamicInvoke的弊端进行了一次封装，
 
3. 有Invoke方法的类的实例，参数表也需要汇聚到一个object[]中，这两种设计应该是有不同需求背景的，目前不知道究竟有什么不同

构造函数第一个为Next参数。

''' C#
using System.Threading.Tasks;

namespace Microsoft.Owin
{
    /// <summary>
    /// An abstract base class for a standard middleware pattern.
    /// </summary>
    public abstract class OwinMiddleware
    {
        /// <summary>
        /// Instantiates the middleware with an optional pointer to the next component.
        /// </summary>
        /// <param name="next"></param>
        protected OwinMiddleware(OwinMiddleware next)
        {
            Next = next;
        }

        /// <summary>
        /// The optional next component.
        /// </summary>
        protected OwinMiddleware Next { get; set; }

        /// <summary>
        /// Process an individual request.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public abstract Task Invoke(IOwinContext context);
    }
}

'''
 
4. Type，这需要对这个类的构造方法进行封装，参考UseHandlerMiddleware的构造函数，第一个参数应该是一个AppFunc

```C#
namespace Microsoft.Owin.Extensions
{
    using AppFunc = Func<IDictionary<string, object>, Task>;

    /// <summary>
    /// Represents a middleware for executing in-line function middleware.
    /// </summary>
    public class UseHandlerMiddleware
    {
        private readonly AppFunc _next;
        private readonly Func<IOwinContext, Task> _handler;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Microsoft.Owin.Extensions.UseHandlerMiddleware" /> class.
        /// </summary>
        /// <param name="next">The pointer to next middleware.</param>
        /// <param name="handler">A function that handles all requests.</param>
        public UseHandlerMiddleware(AppFunc next, Func<IOwinContext, Task> handler)
        {
            if (handler == null)
            {
                throw new ArgumentNullException("handler");
            }
            _next = next;
            _handler = handler;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Microsoft.Owin.Extensions.UseHandlerMiddleware" /> class.
        /// </summary>
        /// <param name="next">The pointer to next middleware.</param>
        /// <param name="handler">A function that handles the request or calls the given next function.</param>
        public UseHandlerMiddleware(AppFunc next, Func<IOwinContext, Func<Task>, Task> handler)
        {
            if (handler == null)
            {
                throw new ArgumentNullException("handler");
            }
            _next = next;
            _handler = context => handler.Invoke(context, () => _next(context.Environment));
        }

        /// <summary>
        /// Invokes the handler for processing the request.
        /// </summary>
        /// <param name="environment">The OWIN context.</param>
        /// <returns>The <see cref="T:System.Threading.Tasks.Task" /> object that represents the request operation.</returns>
        public Task Invoke(IDictionary<string, object> environment)
        {
            IOwinContext context = new OwinContext(environment);
            return _handler.Invoke(context);
        }
    }
}

```
 
