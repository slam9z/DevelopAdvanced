[How ASP.NET MVC Works? ](http://www.cnblogs.com/artech/archive/2012/04/10/how-mvc-works.html)

##一、ASP.NET + MVC   


[IIS与ASP.NET管道 ](http://www.cnblogs.com/artech/archive/2009/06/20/1507165.html)

[MVC、MVP以及Model2[上篇] ]()
[MVC、MVP以及Model2[下篇] ]()

ASP.NET MVC是如何运行的[1]: 建立在“伪”MVC框架上的Web应用 
ASP.NET MVC是如何运行的[2]: URL路由 
ASP.NET MVC是如何运行的[3]: Controller的激活 
ASP.NET MVC是如何运行的[4]: Action的执行 

##二、URL 路由

ASP.NET的路由系统：URL与物理文件的分离 
ASP.NET的路由系统：路由映射 
ASP.NET的路由系统：根据路由规则生成URL 

[ASP.NET MVC路由扩展：路由映射 ]()
[ASP.NET MVC路由扩展：链接和URL的生成 ]()

[ASP.NET路由系统实现原理：HttpHandler的动态映射]() 

[在ASP.NET MVC中通过URL路由实现对多语言的支持]()

##三、Controller的激活


ASP.NET MVC Controller激活系统详解：总体设计 
ASP.NET MVC Controller激活系统详解：默认实现 
ASP.NET MVC Controller激活系统详解：IoC的应用[上篇] 
ASP.NET MVC Controller激活系统详解：IoC的应用[下篇]

##四、Model元数据的提供


ASP.NET MVC Model元数据及其定制：初识Model元数据 
ASP.NET MVC Model元数据及其定制：Model元数据的定制 
ASP.NET MVC Model元数据及其定制：一个重要的接口IMetadataAware 

ASP.NET MVC的Model元数据与Model模板：预定义模板 
ASP.NET MVC的Model元数据与Model模板：模板的获取与执行策略 
ASP.NET MVC的Model元数据与Model模板：将ListControl引入ASP.NET MVC 

ASP.NET MVC的Model元数据提供机制的实现

##五、Model的绑定


ASP.NET MVC三个重要的描述对象：ControllerDescriptor 
ASP.NET MVC三个重要的描述对象：ActionDescriptor 
ASP.NET MVC三个重要的描述对象：ControllerDescriptor与ActionDescriptor的创建机制 
ASP.NET MVC三个重要的描述对象：ParameterDescriptor 

ASP.NET MVC以ValueProvider为核心的值提供系统: NameValueCollectionValueProvider 
ASP.NET MVC以ValueProvider为核心的值提供系统: DictionaryValueProvider 
ASP.NET MVC以ValueProvider为核心的值提供系统: ValueProviderFactory 

ASP.NET MVC的ModelBinder及其提供机制 

通过实例模拟ASP.NET MVC的Model绑定的机制：简单类型+复杂类型 
通过实例模拟ASP.NET MVC的Model绑定的机制：数组 
通过实例模拟ASP.NET MVC的Model绑定的机制：集合+字典

##六、Model验证


ASP.NET MVC以ModelValidator为核心的Model验证体系: ModelValidator 
ASP.NET MVC以ModelValidator为核心的Model验证体系: ModelValidatorProvider 
ASP.NET MVC以ModelValidator为核心的Model验证体系: ModelValidatorProviders 

ASP.NET MVC基于标注特性的Model验证：ValidationAttribute 
ASP.NET MVC基于标注特性的Model验证：DataAnnotationsModelValidator 
ASP.NET MVC基于标注特性的Model验证：DataAnnotationsModelValidatorProvider 
ASP.NET MVC基于标注特性的Model验证：将ValidationAttribute应用到参数上 
ASP.NET MVC基于标注特性的Model验证：一个Model，多种验证规则 

ASP.NET MVC的客户端验证：jQuery的验证 
ASP.NET MVC的客户端验证：jQuery验证在Model验证中的实现 
ASP.NET MVC的客户端验证：自定义验证

##七、Action的执行


ASP.NET MVC下的异步Action的定义和执行原理

ASP.NET MVC涉及到的5个同步与异步，你是否傻傻分不清楚？[上篇] 
ASP.NET MVC涉及到的5个同步与异步，你是否傻傻分不清楚？[下篇]

深入探讨ASP.NET MVC的筛选器 
认识ASP.NET MVC的5种AuthorizationFilter 
ASP.NET MVC中的ActionFilter是如何执行的？

ASP.NET MVC集成EntLib实现“自动化”异常处理[实例篇] 
ASP.NET MVC集成EntLib实现“自动化”异常处理[实现篇]

##八、View的呈现


[了解ASP.NET MVC几种ActionResult的本质：EmptyResult & ContentResult ]()
[了解ASP.NET MVC几种ActionResult的本质：FileResult ]()
了解ASP.NET MVC几种ActionResult的本质：JavaScriptResult & JsonResult 
了解ASP.NET MVC几种ActionResult的本质：HttpStatusCodeResult & RedirectResult/RedirectToRouteResult

[ASP.NET MVC的View是如何被呈现出来的？[设计篇]](http://www.cnblogs.com/artech/archive/2012/08/22/view-engine-01.html) 
[ASP.NET MVC的View是如何被呈现出来的？[实例篇]](http://www.cnblogs.com/artech/archive/2012/08/23/view-engine-02.html)

ASP.NET MVC的Razor引擎：View编译原理 
ASP.NET MVC的Razor引擎：RazorView 
ASP.NET MVC的Razor引擎：IoC在View激活过程中的应用 
ASP.NET MVC的Razor引擎：RazorViewEngine
