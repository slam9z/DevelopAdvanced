[MVC、MVP以及Model2[上篇]](http://www.cnblogs.com/artech/archive/2012/03/08/mvc-01.html)

## 一、自治视图

说到自治视图，可能很多人会感到模式，但是我想很多人（尤其是.NET开发人员）可能经常在采用这种模式来设计我们的应用。
Windows Forms和ASP.NET Web Forms虽然分别属于GUI和Web开发框架，但是它们都采用了事件驱动的开发方式。
所有与UI相关的逻辑都可以定义在针对视图（Windows Form或者Web Form）的后台代码（Code Behind）中，并最终注册到视图本身或者视图元素（控件）的相应事件上。


## 二、MVC模式

它将构成一个人机交互应用涉及到的功能分为Model、Controller和View三部分，三者各自具有的基本职责或者功能范围如下：

* Model：是对应用状态和业务功能的封装，可以看成是同时包含数据和行为的领域模型（Domain Model）。Model接受Controller的请求执行相应的业务功能，
并在状态改变的时候通知View。 

* View：实现可视化界面的呈现，捕捉最终用户的交互操作（比如鼠标和键盘操作）。 

* Controller：View捕获到用户交互操作后会直接转发给Controller，后者完成相应的UI逻辑。如果需要涉及业务功能的调用，Controller会直接调用Model。
在完成UI处理之后，Controller会根据需要控制原View或者创建新的View对用户交互操作予以响应。 

![](http://images.cnblogs.com/cnblogs_com/artech/201203/201203081758064689.png)


## 三、多层架构中的MVC

我看到很多人将MVC和所谓的“三层架构”进行比较，其实两者并没有什么可比性，MVC更不是分别对应着UI、业务逻辑和数据存取三个层次。
不过两者也不能说完全没有关系，我们现在就来讨论这个问题。

Trygve M. H. Reenskau当时提出MVC的时候实际上将其作为构建整个GUI应用的架构模式，而Model维护着整个应用的状态并实现了所有的业务逻辑，
它更多地体现为一个领域模型。而对于多层架构来说（比如我们经常提及的三层架构），MVC是被当成是UI呈现层（Presentation Layer）的设计模式，
而Model则更多地体现为访问业务层的入口（Gateway）。如果采用面向服务的设计，将业务功能定义成相应服务并通过接口（契约）的形式暴露出来，
这里的Model甚至还可以表示成进行服务调用的代理。


[MVC、MVP以及Model2[下篇]](http://www.cnblogs.com/artech/archive/2012/03/08/mvc-02.html)


## 一、 MVP

MVP中的M和V对应中MVC的Model和View，而P（Presenter）则自然代替了MVC中的Controller。

![](http://images.cnblogs.com/cnblogs_com/artech/201203/201203082136518106.png)


Martin Folwer将MVP可分为PV（Passive View）和SoC（Supervising Controller）两种模式。

### PV与SoC

*之前老大的部件式编程有点像MVP，但是接口太烂的，与控件耦合*

如果我们为该View定义一个接口IEmployeeSearchView，我们不能像如下的代码所示将上述这两个控件直接以属性的形式暴露出来。
针对数据绑定对控件类型的选择属于View的内部细节（比如说针对部门列表的显示，我们可以选择DropDownList也可以选择ListBox），
不能体现在表示用于抽象View的接口中。在另一方面，理想情况下定义在Presenter中的UI处理逻辑应该是与具体的技术平台无关的，
如果在接口中涉及到了控件类型，这无疑将Presenter也具体的技术平台绑定在了一起。



``` C#
public interface IEmployeeSearchView
{
    DropDownList Departments { get;}
    GridView Employees { get; }
}


public interface IEmployeeSearchView
{
    IEnumerable<string> Departments { set; }
    string SelectedDepartment { get; }
    IEnumerable<Employee> Employees { set; }
}
```

缺点：

* 对于一些复杂的富客户端（Rich Client）View来说，接口成员将会变得很多，这无疑会提升编程所需的代码量。

* 从另一方讲，由于Presenter需要在控件级别对View进行细粒度的控制，这无疑会提供Presenter本身的复杂度，往往会使原本简单的逻辑复杂化，
在这种情况下我们往往采用SoC模式。


### View和Presenter交互的规则（针对SoC模式）

View和Presenter之间的交互是整个MVP的核心，能够正确地应用MVP模式来架构我们的应用极大地取决于能够正确地处理View和Presenter两者之间的关系。
在由Model、View和Presenter组成的三角关系的核心不是View而是Presenter，Presenter不是View调用Model的中介，而是最终决定如何响应用户交互行为的决策者。

View本身仅仅实现单纯的、独立的UI处理逻辑，它处理的数据应该是Presenter实时推送给它的，所以View尽可能不维护数据状态。定义在IView的接口最好只包含方法，而避免属性的定义，
Presenter所需的关于View的状态应该在接收到View发送的用户交互请求的时候一次得到，而不需要通过View的属性去获取。


## 二、Model2

![](http://images.cnblogs.com/cnblogs_com/artech/201203/20120308214247597.png)

Model 2种一个HTTP请求的目标是Controller中的某个Action，后者体现为定义在Controller类型中的某个方法，
所以对请求的处理最终体现在对Controller对象的激活和对Action方法的执行。一般来说，Controller、Action以及作为Action方法的部分参数
（针对HTTP-GET）可以直接通过请求的URL解析出来。

如上图所示，我们通过一个拦截器（Interceptor）对抵达Web服务器的HTTP请求进行拦截。一般的Web应用框架都提供了针对这样一种拦截机制，
对于ASP.NET来说，我们可以以HttpModule的形式来定义这么一个拦截器。拦截器根据请求解析出目标Controller和对应的Action，
Controller被激活之后Action方法被执行。对于需要传入Action方法的输入参数，则来源于请求地址或/和Post的数据。

在Controller的Action方法被执行过程中，它可以调用Model获取或者改变其状态。在Action方法执行的最后阶段会选择相应的View，
绑定在View上的数据来源Model或者基于显示要求进行得简单逻辑计算，我们有时候它们成为VM（View Model），即基于View的Model（MVC中的Model是与UI无关的）。
生成的View最终写入HTTP回复并最终呈现在用户的浏览器中。

和MVP一样，Model 2完全隔断了View和Model之间的联系。Controller作为支配者地位在Model 2体现尤为明显，
用户交互请求不再由View报告给Controller（Presenter），而是由拦截器直接转发给Controller。Controller不仅仅决定着Model的调用，
还决定了View的选择和生成。ASP.NET MVC就是基于Model 2模式设计的。

## 三、ASP.NETMVC与Model2


由于ASP.NET MVC只有View Model，所以ASP.NET MVC应用框架本社实际上仅仅关于View和Controller，真正的Model以及Model和Controller之间的交互体现在我们如何来设计Controller。
我个人觉得将用于构建ASP.NET MVC的MVC模式成为M(Model)-V(View)-VM(View Model)-C(Controller)也许更为准确。




