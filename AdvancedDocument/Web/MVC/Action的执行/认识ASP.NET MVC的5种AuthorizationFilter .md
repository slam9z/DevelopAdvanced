[认识ASP.NET MVC的5种AuthorizationFilter ](http://www.cnblogs.com/artech/archive/2012/07/02/AuthorizationFilter.html)

##一、IAuthorizationFilter

``` C#
public interface IAuthorizationFilter
{    
    void OnAuthorization(AuthorizationContext filterContext);
}
```

下面的都继承IAuthorizationFilter接口

##二、AuthorizeAttribute

##三、RequireHttpsAttribute 

##四、ValidateInputAttribute


##五、ValidateAntiForgeryTokenAttribute

##、ChildActionOnlyAttribute

如果我们希望定义在Controol中的方法能以子Action的形式在某个View中被调用，这样的调用一般用于生成组成整个View的某个部分的HTML，
我们可以在方法上应用ChildActionOnlyAttribute特性
