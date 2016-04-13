[Bundling and Minification](http://www.asp.net/mvc/overview/performance/bundling-and-minification)

[.NET/ASP.NET 4.5 Bundle组件—捆绑、缩小静态文件 ](http://blog.csdn.net/wangqingpei557/article/details/11477749)

##Bundle

* Include 
    添加文件

* Path
    虚拟目录

* Using a CDN



##ScriptBundle与StyleBundle

系统实现的两张静态资源处理。    

##Styles.Render与Scripts.Render

静态类，用来处理Render Bundle资源。

##扩展自定义类型静态文件



##BundleTable与BundleCollection


###EnableOptimizations
要想你的捆绑起效果需要在注册的时候加上一段：BundleTable.EnableOptimizations = true;代码，意思是说开启捆绑，
如果不开启捆绑则默认在调试环境里将不起效果，因为System.Web.Optimization使用了默认捆绑策略，如果是在Debug模式下，将不启用捆绑，如果你人为的设置了将覆盖默认设置；


##BundleModule

在PostResolveRequestCache事件进行处理的

###客户端缓存相关

我们通过Pragma: no-cache头也能看出来了；

那么我们得出结论，所有Bundle出来的文件都不可能直接缓存在浏览器中，每次都会带上Cache段If-Modified-Since去验证服务器的文件版本；
刚好这里我们可以跟动态输出的静态文件地址的后面的参数对上了；

/Content/css?v=ZPnWVRT3c0yyrVDPmI-xkJuhBdJfQsL3A0K5C9WTOk01

这个链接后面的v参数是表示当前Bundle后虚拟文件的版本，如果我们在服务器上把文件修改了之后那么这个文件的If-Modified-Since验证就失败了，
会生成新的版本号作为连接的参数；
