[ASP.NET MVC路由扩展：路由映射 ](http://www.cnblogs.com/artech/archive/2012/03/26/mvc-routing-01.html)

##一、基本路由映射

由于ASP.NET MVC的路由注册与具体的物理文件无关，所以MapRoute方法中*并没有一个表示文件路径的physicalFile参数*。
与直接定义在RouteCollectionExtensions中的Ignore和MapPageRoute方法不同的是，
*表示默认变量的参数defaults和基于正则表达式的变量约束的参数constraints都不再是一个RouteValueDictionary对象，而是一个普通的object。*

##二、 实例演示：注册路由映射与查看路由信息


##三、基于Area的路由映射

###AreaRegistration与AreaRegistrationContext


AreaRegistration定义了两个抽象的静态RegisterAllAreas方法重载，参数state用于传递给具体AreaRegistration的数据。
当RegisterAllArea方法执行的时候，它先遍历通过BuildManager的静态方法GetReferencedAssemblies方法得到的编译Web应用所使用的程序集，
通过反射得到所有实现了接口IController的类型，并通过反射创建相应的AreaRegistration对象。对于每个AreaRegistration对象，
一个AreaRegistrationContext对象被创建出来并作为参数调用它们的RegisterArea方法。

``` C#
public abstract class AreaRegistration
{    
    public static void RegisterAllAreas();
    public static void RegisterAllAreas(object state);
 
    public abstract void RegisterArea(AreaRegistrationContext context);
    public abstract string AreaName { get; }
}
```

###AreaRegistration的缓存

###实例演示：查看基于Area路由信息


