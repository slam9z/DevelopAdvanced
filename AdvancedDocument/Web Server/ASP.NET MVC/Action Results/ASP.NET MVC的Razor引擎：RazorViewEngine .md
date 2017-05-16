[ASP.NET MVC的Razor引擎：RazorViewEngine ](http://www.cnblogs.com/artech/archive/2012/09/07/razor-view-engine-04.html)

基于Web Form引擎的WebFormViewEngine和针对Razor引擎的RazorViewEngine都是抽象类型BuildManagerViewEngine的子类，
而后者又继承自VirtualPathProviderViewEngine。在这里我们仅仅对实现在RazorViewEngine中View获取的逻辑进行简单介绍。
由于Razor引擎下的View通过RazorView对象来表示，而RazorView通过View文件的虚拟路径来构建，所以RazorViewEngine的View获取机
制在于根据当前上下文找到与指定View名称相匹配的View文件（.cshtml或者.vbhtml文件），然后根据该 View文件的虚拟路径创建一个
RazorView对象并最终封装成ViewEngineResult对象返回。[本文已经同步到《How ASP.NET MVC Works?》中]

