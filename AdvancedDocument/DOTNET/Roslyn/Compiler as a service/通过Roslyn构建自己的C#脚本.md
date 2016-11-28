[通过Roslyn构建自己的C#脚本](http://www.cnblogs.com/TianFang/archive/2012/02/14/2351817.html)

通过Roslyn构建自己的C#脚本
在下一代的C#中，一个重要的特性就是"Compiler as a Service"，简单的讲，就是就是将编译器开放为一种可在代码中调用的服务。最近使用了一下微软放出的Project Roslyn CTP版，感觉还是非常强大的。 
要在自己的代码中执行C#脚本，首先进行如下几步准备工作。 
在微软的网站下载Roslyn CTP版并安装 
在工程中添加Roslyn.Compilers.dll和Roslyn.Compilers.CSharp.dll的引用 
在代码中增加如下命名空间的引用。
using Roslyn.Scripting;
using Roslyn.Scripting.CSharp; 
经典的HelloWorld 
首先还是以经典的Hello World来开始介绍如何执行脚本吧。 
    static void Main(string[] args)
    {
        var scriptEngine = new ScriptEngine();
        scriptEngine.Execute("System.Console.WriteLine(\"hello world\")");
    } 
从上述代码中可以看出，执行一个脚本还是比较简单的，只要创建一个ScriptEngine对象，然后就可以通过ScriptEngine.Execute()函数执行自己的脚本了。 
如果我们要获取脚本的返回值，也是很容易的。 
    var result = scriptEngine.Execute<int>("3+2*5");
    Console.WriteLine(result); 
在会话中执行脚本 
很多时候，我们无法一次执行所有的脚本，而是像shell中那样输入一句执行一句的。假如我们执行如下代码： 
    var scriptEngine = new ScriptEngine();
    scriptEngine.Execute("var i = 3;");
    var result = scriptEngine.Execute("i * 2"); 
得到的并不是我们想要的结果6，而是一个异常：(1,1): error CS0103: The name 'i' does not exist in the current context。 
究其原因，是因为ScriptEngine.Execute()函数每次都是在一个单独的上下文中执行的，并不会和前面的语句产生关联。如果我们要在ScriptEngine.Execute()函数中添加Session参数，以标明其是在同一个会话中的。正确方式如下： 
    var scriptEngine = new ScriptEngine();
    var session = Session.Create();
    scriptEngine.Execute("var i = 3;", session);
    var result = scriptEngine.Execute("i * 2", session); 
在脚本和程序中共享数据 
我们在执行脚本时，除了获取脚本的输出外，许多时候需要设置脚本的输入，要设置输入的方式也有许多。最直接的方式拼接脚本但这么做的效率和可维护性是十分差的。另外也可以通过传统的IPC通信机制——文件、Socket等方式，这种方式一来比较麻烦，二来对于复杂的对象来说，还牵涉到序列化，也是非常不便。 
Roslyn提供了一个更为简单有效的解决办法：在会话中传入一个宿主对象，会话中的脚本程序也能访问宿主对象的各成员变量。 
还是举一个简单的例子吧： 
    namespace Host
    {
        public class HostObject
        {
            public string State = "Hello";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var hostReference = new Roslyn.Compilers.AssemblyFileReference(typeof(Host.HostObject).Assembly.Location);
            var engine = new ScriptEngine(references: new[] { hostReference }, importedNamespaces: new[] { "System" });
            var host = new Host.HostObject();
            var session = Session.Create(host);

            var result = engine.Execute<string>("State + State", session);
            Console.WriteLine(result);

            host.State = "Go Go hello ";
            result = engine.Execute<string>("State + State", session);
            Console.WriteLine(result);
        }
    } 
这里首先创建了一个HostObject类型的宿主对象host，再由它创建会话。这样就将host对象的成员变量State嵌入了脚本中，在脚本和程序中都能共享State变量了。