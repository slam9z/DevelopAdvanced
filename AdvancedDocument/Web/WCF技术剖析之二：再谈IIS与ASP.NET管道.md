[WCF技术剖析之二：再谈IIS与ASP.NET管道 ](http://www.cnblogs.com/artech/archive/2009/06/20/1507165.html)

## IIS 5.x与ASP.NET

我们先来看看IIS 5.x是如何处理基于ASP.NET资源（比如.aspx,.asmx等）请求的，整个过程基本上可以通过图1体现。 

IIS 5.x运行在进程InetInfo.exe中，在该进程中一个最重要的服务就是名为*World Wide Web Publishing Service（简称W3SVC）*的Windows Service。
W3SVC的主要功能包括HTTP请求的监听、工作进程的管理以及配置管理（通过从Metabase中加载相关配置信息）等。 

当检测到某个HTTP Request后，先根据扩展名判断请求的是否是静态资源（比如.html,.img,.txt,.xml等），如果是则直接将文件内容以HTTP Response的形式返回
。如果是动态资源（比如.aspx,asp,php等等），则通过扩展名从IIS的脚本影射（Script Map）找到相应的ISAPI Dll。 

 
![](http://images.cnblogs.com/cnblogs_com/artech/WindowsLiveWriter/IISASP.NET_457/clip_image002_thumb.jpg)
  
图1 IIS 5.x与ASP.NET 


*ISAPI是Internet服务器API（Internet Server Application Programming Interface）*的缩写，是一套本地的（Native）Win32 API，具有较高的执行性能,
是IIS和其他动态Web应用或者平台之间的纽带。比如ASP ISAPI桥接IIS与ASP，而ASP.NET ISAPI则连接着IIS与ASP.NET。ISPAI定义在一个Dll中，
ASP.NET ISAPI对应的Dll为Aspnet_isapi.dll,你可以在目录“%windir%\Microsoft.NET\Framework\{version no}\”中找到该Dll。 

ISAPI支持ISAPI扩展（ISAPI Extension）和ISAPI筛选（ISAPI Filter），前者是真正处理HTTP请求的接口，
后者则可以在HTTP请求真正被处理之前查看、修改、转发或者拒绝请求，比如IIS可以利用ISAPI筛选进行请求的验证（Authentication）。 

如果我们请求的是一个基于ASP.NET的资源类型，比如：.aspx Web Page、 .asmx Web Service或者.svc WCF Service等，Aspnet_isapi.dll会被加载，
ASP.NET ISAPI扩展会创建ASP.NET的工作进程（如果该进程尚未启动），对于IIS 5.x来说，该工作进程为aspnet.exe。IIS进程与工作进程之间通过命名管道（Named Pipes）进程通信，以获得最好的性能。 

在工作进程初始化过程中，.NET 运行时（CLR）被加载，从而构建了一个托管的环境。对于某个Web应用的初次请求，CLR会为其创建一个AppDomain。
在此AppDomain中，HTTP运行时（HTTP Runtime）被加载并用以创建相应的应用。对于寄宿于IIS 5.x的所有Web 应用都运行在同一个进程（*工作进程Aspnet_wp.exe*）
的不同AppDomain中。 

## IIS 6与ASP.NET

通过上面的介绍，我们可以看出IIS 5.x至少存在着如下两个方面的不足： 

* ISAPI Dll被加载到InetInfo.exe进程中，它和工作进程之间是一种典型的跨进程通信方式，尽管采用性能最好的命名管道，但是仍然会带来性能的瓶颈； 
* 所有的ASP.NET应用，运行在相同的进程（aspnet_wp.exe）中的不同的应用程序域（AppDomain）中，基于应用程序域的隔离级别不能从根本上解决一个应用程序对
另一个程序的影响，在更多的时候，我们需要不同的Web应用运行在不同的进程中。

在IIS 6.0中，为了解决第一个问题，ISAPI.dll被直接加载到工作进程中。为了解决第2个问题，引入了应用程序池（Application Pool）的机制。

HTTP协议栈（HTTP Protocol Stack，HTTP.SYS）。HTTP.SYS运行在Windows的内核模式（Kernel Mode）下，作为驱动程序而存在。
它是Windows 2003的TCP/IP网络子系统的一部分，从结构上，它属于TCP之上的一个网络驱动程序。严格地说，HTTP.SYS已经不属于IIS的范畴了，
所以HTTP.SYS的配置信息并不保存在IIS的元数据库（Metabase），而是定义在注册表中。HTTP.SYS的注册表项位于下面的路径中：
HKEY_LOCAL_MACHINE/SYSTEM/CurrentControlSet/Services/HTTP。HTTP.SYS能够带来如下的好处： 

* 持续监听：由于HTTP.SYS是一个网络驱动程序，始终处于运行状态，对于用户的HTTP请求，能够及时作出反应； 
* 更好的稳定性：HTTP.SYS运行在操作系统内核模式下，并不执行任何用户代码，所以其本身不会受到Web应用、工作进程和IIS进程的影响； 
* 内核模式下数据缓存：如果某个资源被频繁请求，HTTP.SYS会把响应的内容进行缓存，缓存的内容可以直接响应后续的请求。由于这是基于内核模式的缓存，
不存在内核模式和用户模式的切换，响应速度将得到极大的改进。

![](http://images.cnblogs.com/cnblogs_com/artech/WindowsLiveWriter/IISASP.NET_457/clip_image004_thumb.jpg)

图2 IIS 6与ASP.NET 

## IIS 7.0与ASP.NET

IIS 7.0对请求的监听和分发机制上又进行了革新性的改进，主要体现在对于*Windows进程激活服务（Windows Process Activation Service，WAS）*的引入，
将原来（IIS 6.0）W3SVC承载的部分功能分流给了WAS。具体来说，通过上面的介绍，我们知道对于IIS 6.0来说，W3SVC主要承载着三大功能：
 
* HTTP请求接收：接收HTTP.SYS监听到的HTTP请求； 
* 配置管理：从元数据库（Metabase）中加载配置信息对相关组件进行配置； 
* 进程管理：创建、回收、监控工作进程。

在IIS 7.0，后两组功能被移入WAS中，接收HTTP请求的任务依然落在W3SVC头上。WAS的引入为IIS 7.0一项前所未有的特性：同时处理HTTP和非HTTP请求。
在WAS中，通过一个重要的接口：监听器适配器接口（Listener Adapter Interface）抽象出不同协议监听器监听到的请求。至于IIS下的监听器，
除了基于网络驱动的HTTP.SYS提供HTTP请求监听功能外，WCF提供了3种类型的监听器：TCP监听器、命名管道（Named Pipes）监听器和MSMQ监听器，
分别提供了基于TCP、命名管道和MSMQ传输协议的监听功能。

![](http://images.cnblogs.com/cnblogs_com/artech/WindowsLiveWriter/IISASP.NET_457/clip_image008_2.jpg)
图4 IIS 7与ASP.NET




## ASP.NET集成 

从上面对IIS 5.x和IIS 6.0的介绍中，我们不难发现这一点，IIS与ASP.NET是两个相互独立的管道（Pipeline），
在各自管辖范围内，它们各自具有自己的一套机制对HTTP请求进行处理。两个管道通过ISAPI实现“联通”：IIS是第一道屏障，
当对HTTP请求进行必要的前期处理（比如身份验证等）后，通过ISAPI将请求分发给ASP.NET管道。
当ASP.NET在自身管道范围内完成对HTTP请求的处理后，处理后的结果再返回到IIS，IIS对其进行后期处理（比如日志记录、压缩等），最终生成HTTP响应（HTTP Response）。
从另一个角度讲，IIS运行在非托管的环境中，而ASP.NET管道则是托管的，从这个意义上讲，ISAPI还是连接非托管环境和托管环境的纽带。
图5反映了IIS 6.0与ASP.NET之间的桥接关系。 

![](http://images.cnblogs.com/cnblogs_com/artech/WindowsLiveWriter/IISASP.NET_457/clip_image012_2.jpg)

图5 基于IIS 6.0与ASP.NET双管道设计 

IIS 5.x和IIS 6.0下把两个管道进行隔离至少带来了下面一些局限与不足： 

* 相同操作的重复执行：IIS与ASP.NET之间具有一些重复的操作，比如身份验证； 

* 动态文件与静态文件处理的不一致：因为只有基于ASP.NET的动态文件（比如.aspx、.asmx、.svc等等）的HTTP请求才能通过ASP.NET ISAPI进入ASP.NET管道，
而对于一些静态文件（比如.html、.xml、.img等）的请求，则由IIS直接响应，那么ASP.NET管道中的一些功能将不能用于这些基于静态文件的请求，
比如，我们希望通过Forms认证应用于基于图片文件的请求； 

* IIS难以扩展：对于IIS的扩展基本上就体现在自定义ISAPI，但是对于大部分人来说，这不是一件容易的事情。因为ISAPI是基于Win32的非托管的API，并非一种面向应用的编程接口。
通常我们希望的是诸如定义ASP.NET的HttpModule和HttpHandler一样，通过托管代码的方式来扩展IIS。

在IIS 7.0中，实现了两者的集成。对于集成模式下的IIS 7.0，我们获得如下的好处。 

* 允许我们通过本地代码（Native Code）和托管代码（Managed Code）两种方式定义IIS Module，这些IIS Module注册到IIS中形成一个通用的请求处理管道。
由这些IIS Module组成的这个管道能够处理所有的请求，不论请求基于怎样的资源类型。比如，可以将FormsAuthenticationModule提供的Forms认证应用到基于.aspx，
CGI和静态文件的请求。 

* 将ASP.NET提供的一些强大的功能应用到原来难以企及的地方，比如将ASP.NET的URL重写功能置于身份验证之前； 
* 采用相同的方式去实现、配置、检测和支持一些服务器特性（Feature），比如Module、Handler映射、错误定制配置（Custom Error Configuration）等。

![](http://images.cnblogs.com/cnblogs_com/artech/WindowsLiveWriter/IISASP.NET_457/clip_image014_thumb.jpg)
图6 基于IIS 7.0与ASP.NET集成管道设计 


## ASP.NET管道

如果HTTP.SYS接收到的HTTP请求是对该Web应用的第一次访问，当成功加载了运行时后，会通过AppDomainFactory为该Web应用创建一个应用程序域（AppDomain）。
随后，一个特殊的运行时IsapiRuntime被加载。IsapiRuntime定义在程序集System.Web中，对应的命名空间为System.Web.Hosting。IsapiRuntime会接管该HTTP请求。 

IsapiRuntime会首先创建一个IsapiWorkerRequest对象，用于封装当前的HTTP请求，并将该IsapiWorkerRequest对象传递给ASP.NET运行时：HttpRuntime，
从此时起，HTTP请求正式进入了ASP.NET管道。根据IsapiWorkerRequest对象，HttpRuntime会创建用于表示当前HTTP请求的上下文（Context）对象：HttpContext。 

随着HttpContext被成功创建，HttpRuntime会利用HttpApplicationFactory创建新的或者获取现有的HttpApplication对象。
实际上，ASP.NET维护着一个HttpApplication对象池，HttpApplicationFactory从池中选取可用的HttpApplication用户处理HTTP请求，处理完毕后将其释放到对象池中。
HttpApplicationFactory负责处理当前的HTTP请求。 

在HttpApplication初始化过程中，会根据配置文件加载并初始化相应的HttpModule对象。对于HttpApplication来说，在它处理HTTP请求的不同的阶段会触发不同的事件
（Event），而HttpModule的意义在于通过注册HttpApplication的相应的事件，将所需的操作注入整个HTTP请求的处理流程。ASP.NET的很多功能，
比如身份验证、授权、缓存等，都是通过相应的HttpModule实现的。 

而最终完成对HTTP请求的处理实现在另一个重要的对象中：HttpHandler。对于不同的资源类型，具有不同的HttpHandler。
比如.aspx页对应的HttpHandler为System.Web.UI.Page，WCF的.svc文件对应的HttpHandler为System.ServiceModel.Activation.HttpHandler。
上面整个处理流程如图7所示。 

![](http://images.cnblogs.com/cnblogs_com/artech/WindowsLiveWriter/IISASP.NET_457/clip_image016_thumb.jpg)

图7 ASP.NET 处理管道 


### HttpApplication 

HttpApplication是整个ASP.NET基础架构的核心，它负责处理分发给它的HTTP请求。由于一个HttpApplication对象在某个时刻只能处理一个请求
，只有完成对某个请求的处理后，HttpApplication才能用于后续的请求的处理。所以，ASP.NET采用对象池的机制来创建或者获取HttpApplication对象。
具体来讲，当第一个请求抵达的时候，ASP.NET会一次创建多个HttpApplication对象，并将其置于池中，选择其中一个对象来处理该请求。当处理完毕，HttpApplication不会被回收，而是释放到池中。对于后续的请求，空闲的HttpApplication对象会从池中取出，如果池中所有的HttpApplication对象都处于繁忙的状态，ASP.NET会创建新的HttpApplication对象。 

HttpApplication处理请求的整个生命周期是一个相对复杂的过程，在该过程的不同阶段会触发相应的事件。我们可以注册相应的事件，
将我们的处理逻辑注入到HttpApplication处理请求的某个阶段。我们接下来介绍的HttpModule就是通过HttpApplication事件注册的机制实现相应的功能的。
表1按照实现的先后顺利列出了HttpApplication在处理每一个请求时触发的事件名称。


对于一个ASP.NET应用来说，HttpApplication派生于global.asax文件，我们可以通过创建global.asax文件对HttpApplication的请求处理行为进行定制。
global.asax采用一种很直接的方式实现了这样的功能，这种方式既不是我们常用的方法重写（Method Overriding）或者事件注册，而是直接采用方法名匹配。
在global.asax中，我们按照这样的方法命名规则进行事件注册：Application_{Event Name}。
比如Application_BeginRequest方法用于处理HttpApplication的BeginRequest事件。

### HttpModule 

ASP.NET为创建各种.NET Web应用提供了强大的平台，它拥有一个具有高度可扩展性的引擎，并且能够处理对于不同资源类型的请求。

``` C#
public interface IHttpModule
{
    void Dispose();
    void Init(HttpApplication context);
}
```

ASP.NET提供的很多基础构件（Infrastructure）功能都是通过相应的HttpModule实现的，下面类列出了一些典型的HttpModule: 

* OutputCacheModule：实现了输出缓存（Output Caching）的功能； 
* SessionStateModule：在无状态的HTTP协议上实现了基于会话（Session）的状态； 
* WindowsAuthenticationModule + FormsAuthenticationModule + PassportAuthentication- Module：实现了3种典型的身份认证方式：
Windows认证、Forms认证和Passport认证； 
* UrlAuthorizationModule + FileAuthorizationModule：实现了基于Uri和文件ACL（Access Control List）的授权。


### HttpHandler 

如果说HttpModule相当于IIS的ISAPI Filter的话，我们可以说HttpHandler则相当于IIS的ISAPI Extension，HttpHandler在ASP.NET中扮演请求的最终处理者的角色。
对于不同资源类型的请求，ASP.NET会加载不同的Handler来处理，也就是说.aspx page与.asmx web service对应的Handler是不同的。 

所有的HttpHandler都实现了接口IHttpHandler。下面是IHttpHandler的定义，方法ProcessRequest提供了处理请求的实现。 

``` C#
public interface IHttpHandler
{
    void ProcessRequest(HttpContext context);
    bool IsReusable { get; }
}
```
