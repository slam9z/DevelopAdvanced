*AppFunc��Func<IDictionary<string, object>, Task>�� OwinMiddleware��ͨ�õģ���Microsoft.Owin.Infrastructure.AppFuncTransition��OwinMiddlewareTransition�����װ��*


�ܽᣬ������Ҫ����AppBuilder��Use�����ľ�����������չ��������չ����ʵ���϶��ǶԻ���Use�ķ�װ��������Use�����ܵ���˵���Խ�������middlewareObject

1. Delegate����򵥵ģ�ֱ�ӿ��Է�װ����Ԫ��ѹ��List
 
2. ��Initialize���������ʵ�����������һ��Ϊһ��AppFunc����OwinMiddleware��ֻҪ��Invoke֮���ܷ���һ��Task���У�Ϊ�˱���DynamicInvoke�ı׶˽�����һ�η�װ��
 
3. ��Invoke���������ʵ����������Ҳ��Ҫ��۵�һ��object[]�У����������Ӧ�����в�ͬ���󱳾��ģ�Ŀǰ��֪��������ʲô��ͬ

    ���캯����һ��ΪNext������

    ``` C#

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

    ```
 
4. Type������Ҫ�������Ĺ��췽�����з�װ���ο�UseHandlerMiddleware�Ĺ��캯������һ������Ӧ����һ��AppFunc

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
 
