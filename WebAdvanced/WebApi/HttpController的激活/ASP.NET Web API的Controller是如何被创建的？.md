[ASP.NET Web API的Controller是如何被创建的？ ](http://www.cnblogs.com/artech/p/http-controller-how-to-activate.html)

##一、程序集的解析

在ASP.NET Web API的HttpController激活系统中，AssembliesResolver为目标HttpController类型解析提供候选的程序集。
换句话说，候选HttpController类型的选择范围仅限于定义在由AssembliesResolver提供的程序集中的所有实现了IHttpController接口的类型
。所有的AssembliesResolver均实现了接口IAssembliesResolver，该接口定义在命名空间“System.Web.Http.Dispatcher”下，
如果未作特别说明，本节新引入的类型均定义在此命名空间下。如下面的代码片断所示，
IAssembliesResolver接口中仅仅定义了一个唯一的GetAssemblies方法，该方法返回的正是提供的程序集列表。

``` C#
public interface IAssembliesResolver
{
    ICollection<Assembly> GetAssemblies();
}
```

默认使用的AssembliesResolver类型为DefaultAssembliesResolver。如下面的代码片断所示，
DefaultAssembliesResolver在实现的GetAssemblies方法中直接返回当前应用程序域加载的所有程序集列表。

``` C#
public class DefaultAssembliesResolver : IAssembliesResolver
{
    public virtual ICollection<Assembly> GetAssemblies()
    {
        return AppDomain.CurrentDomain.GetAssemblies().ToList<Assembly>();
    }
}
```

DefaultAssembliesResolver是默认使用的AssembliesResolver，那么默认的AssembliesResolver类型在ASP.NET Web API是如何确定的呢？要回答这个问题，需要涉及到另一个重要的类型ServicesContainer，它定义在命名空间“System.Web.Http.Controllers”下。

由于DefaultAssembliesResolver在为HttpController类型解析提供的程序集仅限于当前应用程序域已经加载的程序集，如果目标HttpController定义在尚未加载的程序集中，我们不得不预先加载它们。但是这样的问题只会发生在Self Host寄宿模式下，如果采用Web Host寄宿模式则无此困扰，原因在于后者默认使用的是另一个AssembliesResolver类型。
我们知道在Web Host寄宿模式下用于配置ASP.NET Web API消息处理管道的是通过类型GlobalConfiguration的静态只读属性Configuration返回的HttpConfiguration对象。
从如下的代码片断我们可以发现，当GlobalConfiguration的Configuration属性被第一次访问的时候，在ServicesContainer中注册的AssembliesResolver会被替换成一个类型为WebHostAssembliesResolver的对象。

``` C#
public static class GlobalConfiguration
{
    //其他成员
    static GlobalConfiguration()
    {
        _configuration = new Lazy<HttpConfiguration>(delegate 
        {
            HttpConfiguration configuration = new HttpConfiguration( new HostedHttpRouteCollection(RouteTable.Routes));
            configuration.Services.Replace(typeof(IAssembliesResolver), new WebHostAssembliesResolver());        
            //其他操作
            return configuration;
        });
    //其他操作
    }
 
    public static HttpConfiguration Configuration
    {
        get
        {
            return _configuration.Value;
        }
    }
}
```

WebHostAssembliesResolver是一个定义在程序集“System.Web.Http.WebHost.dll”中的内部类型。
从如下的代码片断可以看出WebHostAssembliesResolver在实现的GetAssemblies方法中直接通过调用BuildManager
的GetReferencedAssemblies方法来获取最终提供的程序集。

``` C#
internal sealed class WebHostAssembliesResolver : IAssembliesResolver
{
    ICollection<Assembly> IAssembliesResolver.GetAssemblies()
    {
        return BuildManager.GetReferencedAssemblies().OfType<Assembly>().ToList<Assembly>();
    }
}
```


##二、HttpController类型的解析

注册在当前ServicesContainer上的AssembliesResolver对象为HttpController类型的解析提供了可供选择的程序集，
真正用于解析HttpController类型的是一个名为HttpControllerTypeResolver的对象。所有的HttpControllerTypeResolver类型均实现了接口IHttpControllerTypeResolver，
如下面的代码片断所示，定义其中的唯一方法GetControllerTypes借助于提供的AssembliesResolver解析出所有的HttpController类型。

``` C#
public interface IHttpControllerTypeResolver
{
    ICollection<Type> GetControllerTypes(IAssembliesResolver assembliesResolver);
}
```

与AssembliesResolver注册方式类似，默认使用的HttpControllerTypeResolver同样是注册在当前HttpConfiguration的ServicesContainer对象上。我们可以通过ServicesContainer具有如下定义的扩展方法GetHttpControllerTypeResolver得到这个注册的HttpControllerTypeResolver对象。



我们同样可以通过HttpConfiguration默认采用的DefaultServices的构造函数得到默认注册的HttpControllerTypeResolver对象的类型。
如下面的代码片断所示，这个默认注册的HttpControllerTypeResolver是一个类型为DefaultHttpControllerTypeResolver的对象。

``` C#
public class DefaultServices : ServicesContainer
{
    //其他成员
    public DefaultServices(HttpConfiguration configuration)
    {
        //其他操作
        this.SetSingle<IHttpControllerTypeResolver>(new DefaultHttpControllerTypeResolver());
    }
}
```

###1、DefaultHttpControllerTypeResolver

如下面的代码片断所示， DefaultHttpControllerTypeResolver具有一个Predicate<Type>类型的只读属性IsControllerTypePredicate，
返回的委托对象用于判断指定的类型是否是一个有效的HttpController类型。



###2、HttpController类型的缓存

由于针对所有HttpController类型的解析需要大量使用到反射，这是一个相对耗时的过程，所以ASP.NET Web API会对解析出来的HttpController类型进行缓存。
具体的缓存实现在具有如下定义的HttpControllerTypeCache类型中，这是一个定义在程序集“System.Web.Http.dll”中的内部类型。

``` C#
internal sealed class HttpControllerTypeCache
{
    //其他成员   
    internal Dictionary<string, ILookup<string, Type>> Cache { get; }
}
```

缓存的HttpController类型通过只读属性Cache获取，这是一个类型为Dictionary<string, ILookup<string, Type>>的字典对象。该字典的Key表示HttpController的名称（HttpController类型名称去除“Controller”后缀），
其Value返回的ILookup<string, Type>对象包含一组具有相同名称的HttpController类型列表，自身的Key表示HttpController类型的命名空间。

##三、目标HttpController的选择

AssembliesResolver仅仅是将所有合法的HttpController类型解析出来，针对具体的调用请求，系统必须从中选择一个与当前请求匹配的HttpController类型出来。
HttpController的选择通过HttpControllerSelector对象来完成，所有的HttpControllerSelector类型均实现了具有如下定义的接口IHttpControllerSelector。

``` C#
public interface IHttpControllerSelector
{
    IDictionary<string, HttpControllerDescriptor> GetControllerMapping();
    HttpControllerDescriptor SelectController(HttpRequestMessage request);
}
```

如上面的代码片断所示，该接口中定义了GetControllerMapping和SelectController两个方法。GetControllerMapping返回一个描述所有HttpController类型的HttpControllerDescriptor对象与对应的HttpController名称之间的映射关系。针对请求的HttpController选择实现在SelectController方法中，它返回描述目标HttpController的HttpControllerDescriptor对象。

###1、DefaultHttpControllerSelector

默认使用HttpControllerSelector依然注册到当前的ServicesContainer对象中，我们可以调用ServicesContainer如下所示的扩展方法GetHttpControllerSelector得到注册的HttpControllerSelector对象。




如下面的代码片断所示，DefaultHttpControllerSelector不仅仅实现了IHttpControllerSelector接口中定义的两个方法，还定义了另一个名为GetControllerName方法，我们可以调用此方法根据指定HttpRequestMessage对象得到该请求访问的目标HttpController的名称。

``` C#
public class DefaultHttpControllerSelector : IHttpControllerSelector
{
    public DefaultHttpControllerSelector(HttpConfiguration configuration);
 
    public virtual IDictionary<string, HttpControllerDescriptor> GetControllerMapping();
    public virtual HttpControllerDescriptor SelectController(HttpRequestMessage request);
 
    public virtual string GetControllerName(HttpRequestMessage request);
}
```

###2、获取目标HttpController的名称

如果采用Web Host寄宿模式，消息管道的缔造者HttpControllerHandler在根据当前HTTP上下文创建用于表示请求的HttpRequestMessage对象后，
会将ASP.NET路由系统解析当前请求得到的RouteData对象转换成HttpRouteData对象并添加到HttpRequestMessage的属性字典中。
对于Self Host寄宿模式来说，处于消息处理管道末端的HttpRoutingDispatcher会利用ASP.NET Web API的路由系统对当前请求进行路由解析并直接得到封装了路由数据的HttpRouteData对象，
此HttpRouteData同样会被添加到表示当前请求的HttpRequestMessage对象的属性字典之中。

由于被附加到当前请求的HttpRouteData已经包含了目标HttpController的名称（对应的变量名为“controller”），所以我们可以从HttpRequestMessage中直接获取目标HttpController的名称。如下面的代码片断所示，DefaultHttpControllerSelector的GetControllerName方法也是按照这样的逻辑从指定的HttpMessageMessage中提取目标HttpController的名称。

``` c#
public class DefaultHttpControllerSelector : IHttpControllerSelector
{  
    //其他成员
    public virtual string GetControllerName(HttpRequestMessage request)
    {      
        IHttpRouteData routeData = request.GetRouteData();
        if (routeData == null)
        {
            return null;
         }
         string str = null;
         routeData.Values.TryGetValue<string>("controller", out str);
         return str;
     }    
 }
```

###3、建立HttpController名称与HttpControllerDescriptor之间的映射

DefaultHttpControllerSelector 的GetControllerMapping方法会返回一个类型为IDictionary<string, HttpControllerDescriptor>的字典，它包含了描述所有HttpController的HttpControllerDescriptor对象与对应HttpController名称之间的映射关系。



GetControllerMapping方法的实现逻辑其实很简单。如上面的代码片断所示，DefaultHttpControllerSelector具有一个HttpControllerTypeCache类型的只读字段，通过它可以得到HttpController类型与名称之间的关系，
GetControllerMapping方法只需要根据HttpController类型生成对应的HttpControllerDescriptor对象即可。但是有个问题必须要考虑，由于同名的HttpController类型可能定义在不同的命名空间下，而且这里所指的“HttpController名称”是不区分大小写的，所以一个HttpController名称可能对应着多个HttpController类型，这也是为何HttpControllerTypeCache缓存的数据是一个类型为Dictionary<string, ILookup<string, Type>>的字典对象的原因。

###4、根据请求选择HttpController

其实HttpControllerSelector的终极目标还是根据请求实现对目标HttpController的选择，这体现在它的SelectController方法上。对于默认注册的DefaultHttpControllerSelector来说，其SelectController方法的实现逻辑非常简单，它只需要调用GetControllerName方法从给定的HttpRequestMessage提取目标HttpController的名称，然后根据此名称从GetControllerMapping方法的返回值中提取对应的HttpControllerDescriptor对象即可。
实现在SelectController方法中针对请求的HttpController选择机制虽然简单，但是针对几种特殊情况的处理机制我们不应该忽视。

首先，如果调用GetControllerName方法返回的HttpController名称为Null或者是一个空字符串，意味着ASP.NET路由系统（针对Web Host寄宿模式）或者ASP.NET Web API路由系统（针对Self Host寄宿模式）在对请求的解析过程中并没有得到表示目标HttpController名称的路由变量。
这种情况下DefaultHttpControllerSelector会直接抛出一个响应状态为HttpStatusCode.NotFound的HttpResponseException异常，客户端自然就会接收到一个状态为“404, Not Found”的响应。

其次，如果在调用GetControllerMapping方法返回的字典中并没有一个与目标HttpController名称相匹配的HttpControllerDescriptor对象，通过上面的分析我们知道如下两种情况会导致这样的问题。
* 在通过AssembliesResolver提供的程序集中并不曾定义这么一个有效的HttpController类型。 
* 在通过AssembliesResolver提供的程序集中定义了多个同名的HttpController类型，可能是多个HttpController类型在不区分大小写情况下同名，或者是完全同名的多个HttpController类型定义在不同的命名空间下。 

这两种情况下自然不能通过GetControllerMapping方法返回的字典对象来判断，但是却可以通过用于缓存HttpController类型的HttpControllerTypeCache对象来判断。对于第一种情况，DefaultHttpControllerSelector依然会抛出一个响应状态为HttpStatusCode.NotFound的HttpResponseException异常。在第二种情况下，它会抛出一个InvalidOperationException异常，并提示“具有多个匹配的HttpController”。

##四、HttpController的创建

通过上面的介绍我们知道利用注册的HttpControllerSelector对象可以根据表示当前请求的HttpRequestMessage得到描述目标HttpController的HttpControllerDescriptor对象。
在前面介绍HttpControllerDescriptor的时候我们提到过它自身就具有创建对应HttpController的能力。
HttpControllerDescriptor创建被描述HttpController的能力体现在它的CreateController方法上。接下来我们就来着重介绍实现在这个CreateController方法中的HttpController创建机制。



###1、HttpControllerActivator

针对请求对目标HttpController的激活机制最终落实到一个名为HttpControllerActivator的对象上，所有的HttpControllerActivator类型均实现了IHttpControllerActivator接口。
如下面的代码片断所示，定义其中的唯一方法Create会根据表示当前请求的HttpRequestMessage对象、描述目标HttpController的HttpControllerDescriptor对象以及目标HttpController的类型来创建对应的HttpController对象。

``` c#
public interface IHttpControllerActivator
{
    IHttpController Create(HttpRequestMessage request, HttpControllerDescriptor controllerDescriptor, Type controllerType);
}
```

我们已经知道了像这样的“标准化组件”一定是注册到当前ServicesContainer上被HttpController激活系统使用的。
我们可以通过ServicesContainer具有如下定义的扩展方法GetHttpControllerActivator直接获取注册的HttpControllerActivator对象。



###2、DefaultHttpControllerActivator

我们照例利用通过DefaultServices的构造函数定义分析出默认注册的HttpControllerActivator是个怎样的对象。如下面的代码片断所示，当DefaultServices被初始化的时候它会创建并注册一个类型为DefaultHttpControllerActivator对象。



接下来我们就来分析一下在DefaultHttpControllerActivator类型的Create方法中是如何激活目标HttpController实例的，不过要真正了解实现在DefaultHttpControllerActivator的HttpController激活机制之前，我们需要认识另一个名为DependencyResolver的对象。

###3、DependencyResolver

说到DependencyResolver，我们又不得不谈到IoC的概念。我们知道IoC常和另一个术语“依赖注入（DI，Dependency Injection）”联系在一起。通过IoC容器激活的对象可能具有针对其他对象的依赖，而且被依赖的对象可能具有针对另一个对象的依赖，所以IoC容器需要在提供所需对象之前帮助我们解决这些依赖。从命名也可以看出来，
这里介绍DependencyResolver与依赖注入有关，我们可以将它视为ASP.NET Web API内部使用的IoC容器。所有的DependencyResolver实现了具有如下定义的接口IDependencyResolver，它定义在命名空间“System.Web.Http.Dependencies”下。这个接口的定义有点特别，
它具有唯一个返回类型为IDependencyScope的BeginScope方法，IDependencyResolver接口本身同时也继承IDependencyScope这个接口，并且这两个接口又都继承自IDisposable接口。

``` c#
public interface IDependencyResolver : IDependencyScope, IDisposable
{
    IDependencyScope BeginScope();
}
 
public interface IDependencyScope : IDisposable
{
    object GetService(Type serviceType);
    IEnumerable<object> GetServices(Type serviceType);
}
```

通过DependencyResolver的BeginScope方法创建的IDependencyScope对象可以视为一个用于激活目标对象的上下文，我们可以通过调用它的GetService和GetServices方法根据指定的“服务接口类型”获取对应的服务实例。由于IDependencyScope继承自IDisposable，所以与此上下文关联的资源释放工作可以通过实现的Dispose方法来完成。

与上面我们介绍的那些“标准化组件”不同，默认使用的DependencyResolver并未注册到当前的ServicesContainer对象上，而是直接注册到了当前HttpConfiguration上面。如下面的代码片断所示，当前使用的DependencyResolver直接通过HttpConfiguration的DependencyResolver属性来获取和设置。


从上面的代码片断我们还可以看出默认注册到HttpConfiguration上的DependencyResolver是通过类型EmptyResolver的静态属性Instance返回的EmptyResolver对象。EmptyResolver是一个定义在程序集“System.Web.Http.dll”中的内部类型，其成员定义如下。之所以将它如此命名，
原因在于它仅仅是一个“空”的IoC容器。它的BeginScope返回的是它自身，GetService和GetServices方法分别返回Null和一个空对象集合，
Dispose方法也没有任何资源释放工作要做。



###4、HttpRequestMessage中的DependencyResolver

虽然当前使用的DependencyResolver是注册到当前HttpConfiguration上的，但是我们可以直接从表示当前请求的HttpRequestMessage对象中获取由它创建的DependencyScope对象。如下面的代码片断所示，HttpRequestMessage具有一个返回类型为IDependencyScope接口的扩展方法GetDependencyScope。


public static class HttpRequestMessageExtensions
{
    //其他成员    
    public static IDependencyScope GetDependencyScope(this HttpRequestMessage request);
}

其实这个扩展方法实现逻辑很简单，因为DependencyScope对象也存放于HttpRequestMessage的属性字典中。如果此DependencyScope对象尚未添加，该方法则会通过当前的HttpConfiguration得到注册的DependencyResolver对象，然后利用它创建一个新的DependencyScope对象并添加到HttpRequestMessage对象的属性字典中，后续过程如果需要使用到此DependencyScope就可以直接从HttpRequestMessage中提取了。

###5、DependencyResolver在DefaultHttpControllerActivator中的应用

在对DependencyResolver有了基本了解后，我们再来讨论DefaultHttpControllerActivator的Create方法是如何根据当前请求来激活目标HttpController对象的。其实实现机制非常简单
，DefaultHttpControllerActivator先通过调用表示当前请求的HttpRequestMessage对象的扩展方法GetDependencyScope得到通过当前DependencyResolver创建的DependencyScope对象，
然后将目标HttpController的类型作为参数调用其GetService方法。如果该方法返回一个具体的HttpController对象，该对象就是Create方法的返回值，否则直接根据目标HttpController的类型进行反射创建一个HttpController对象并返回。如下所示的代码片断基本上体现了DefaultHttpControllerActivator的HttpController激活机制。

``` c#
public class DefaultHttpControllerActivator : IHttpControllerActivator
{
    public IHttpController Create(HttpRequestMessage request, HttpControllerDescriptor controllerDescriptor, Type controllerType)
    {
        IDependencyScope depedencyScope = request.GetDependencyScope();
        object httpController = depedencyScope.GetService(controllerType)?? Activator.CreateInstance(controllerType);
        return httpController as IHttpController;
    }
}
```

由于默认请求下注册到当前HttpConfiguration上的DependencyResolver是一个EmptyResolver对象，它的GetService方法总是返回Null，所以默认情况下对HttpController的激活总是利用针对目标HttpController类型的反射实现的。关于HttpController的激活，我还想强调一点，
在默认情况下解析出来的所有HttpController类型会被缓存，创建的用于描述HttpController的HttpControllerDescriptor对象也会被缓存，但是HttpController激活系统并不会对创建的HttpController对象实施缓存。
换言之，对于多个针对相同的HttpController类型的请求来说，最终被激活的HttpController实例都是不同的。
