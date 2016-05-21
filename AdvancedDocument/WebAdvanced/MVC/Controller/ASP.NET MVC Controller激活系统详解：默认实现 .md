[ASP.NET MVC Controller激活系统详解：默认实现 ](http://www.cnblogs.com/artech/archive/2012/03/31/controller-activation-02.html)

##一、Controller类型的解析


##二、 Controller类型的缓存

为了避免通过遍历所有程序集对目标Controller类型的解析，ASP.NET MVC对解析出来的Controller类型进行了缓存以提升性能。与针对用于Area注册的AreaRegistration类型的缓存类似，
Controller激活系统同样采用基于文件的缓存策略，而用于保存Controller类型列表的名为MVC-ControllerTypeCache.xml的文件保存在ASP.NET的临时目录下面