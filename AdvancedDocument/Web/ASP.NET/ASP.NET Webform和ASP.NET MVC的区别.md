[ASP.NET Webform和ASP.NET MVC的区别 ](http://www.cnblogs.com/EthanCai/archive/2013/06/15/3137848.html)

##ASP.NET WebForm

ASP.NET Webform提供了一个类似于winform的事件响应GUI模型（event-driven GUI），隐藏了HTTP、HTML、JavaScript等细节，
将用户界面构建成一个服务器端的树结构控件（Control），每个控件通过ViewState保持自己的状态，并自动把客户端的js事件和服务器端的事件联系起来。
这种做法使得开发WinForm和WebForm程序具有相近的开发体验，填平WinForm开发（有状态、面向对象的）和WebForm开发（无状态、面向HTML的）之间的鸿沟。
这种设计在大型网站开发的时候，暴露出一系列弱点：

* ViewState可能过大。访问量非常大的情况下，viewstate占用的流量相当可观，这样直接影响页面传输速度。 

* Page Life Cycle：过于复杂。比如控件的Init事件在Page的Init事件之前执行，而Load事件是控件后执行。事件处理的事件 

* 缺乏对HTML的控制：一般每个控件负责自己的HTML输出，开发人员无法修改输出的HTML结构，除非自己写控件。而且HTML元素的ID比较复杂，也不便于js访问。 

* 界面设计和逻辑开发的关注分离做的不好：Webform开发是一个页面对应一个code-behind class，原本的目的是将展示和逻辑分离。

但是在实际开发的时候，面对某些需求，还是经常会在class中写一些控制界面展示的代码。 

* 对测试的支持不好：页面和code-behind class绑定在一起，无法单独对逻辑进行测试；不支持单元测试。 


##ASP.NET MVC

ASP.NET MVC的优点如下：

* 采用MVC架构：分离了关注点，比如开发Controller的时候，只需关注如何处理交互，从request中获得什么数据，业务逻辑交给Model处理，
还需要把哪些数据传给页面用于展示，如何展示交给View处理。 

* 更好的扩展性：ASP.NET MVC框架由一些列独立的组件构成，你可以轻松替换，如路由系统、the View Engine、the controller factory或者其它框架的组件。
 
* 更好的可测性：关注点的分离另外一个好处就是更好的可测性。

* 能够完全控制输出的HTML。 

* 强大的路由功能。 


参考
Pro ASP.NET MVC Framework 
http://www.asp.net/mvc
http://aspnetwebstack.codeplex.com/
ASP.NET Web Forms Weaknesses 
MVP模式 
Monorails 
http://yok.cnblogs.com/archive/2005/11/05/269383.html 
http://www.cnblogs.com/firstyi/category/108880.html 
Ruby on Rails 
http://www.cnblogs.com/hardrock/archive/2006/08/18/480668.html 
http://www.cnblogs.com/dahuzizyd/category/97947.html 