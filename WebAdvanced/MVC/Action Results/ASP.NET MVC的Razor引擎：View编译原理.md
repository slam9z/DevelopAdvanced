[ASP.NET MVC的Razor引擎：View编译原理 ](http://www.cnblogs.com/artech/archive/2012/09/04/razor-view-engine-01.html)

和ASP.NET 传统的编译方式一样，针对View的编译默认是基于目录的，也就是说同一个目录下的多个View文件被编译到同一个程序集

输出结果至少可以反映三个问题：

* ASP.NET MVC对View文件进行动态编译生成的类型名称基于View文件的虚拟路径（比如文件路径为“~/Views/Foo/Action1.cshtml”的View对应的类型为“ASP._Page_Views_foo_Action1_cshtml”）。 
* ASP.NET MVC是按照目录进行编译的（“~/Views/Foo/”下的两个View文件最终都被编译到程序集“App_Web_j04xtjsy”中）。 
* 程序集按需加载，即第一次访问“~/View/Foo/”目录下的View并不会加载针对“~/View/Bar/”目录的程序集（实际上此时该程序集尚未生成）。 

``` C#
[Dynamic(new bool[] { false, true })]
public class _Page_Views_Foo_Action1_cshtml : WebViewPage<object>
{    
    public override void Execute()
    {
        this.WriteLiteral("<div>当前View类型：</div>\r\n<div>");
        this.Write(base.GetType().AssemblyQualifiedName);
        this.WriteLiteral("</div><br/>\r\n<div>当前加载的View程序集：</div>\r\n");
        this.Write(base.Html.ListViewAssemblies());
    }
     
    protected global_asax ApplicationInstance
    {
        get
        {
            return (global_asax) this.Context.ApplicationInstance;
        }
    }
}

```
