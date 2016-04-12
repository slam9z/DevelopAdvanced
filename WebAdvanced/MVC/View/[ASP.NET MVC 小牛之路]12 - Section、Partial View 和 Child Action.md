[[ASP.NET MVC 小牛之路]12 - Section、Partial View 和 Child Action](http://www.cnblogs.com/willick/p/3410855.html)

给View添加动态内容的方式可归纳为下面几种：

* Inline code，小的代码片段，如 if 和 foreach 语句。
* Html helper方法，用来生成单个或多个HTML元素，如view model、ViewBag等。
* Section，在指定的位置插入创建好的一部分内容。
* Partial view，存在于一个单独的视图文件中，作为子内容可在多个视图中共享。
* Child action，相当于一个包含了业务逻辑的UI组件。当使用child action时，它调用 controller 中的 action 来返回一个view，并将结果插入到输出流中。


##Section

Razor视图引擎支持将View中的一部分内容分离出来，以便在需要的地方重复利用，减少了代码的冗余。
下面来演示如何使用Section。

注意，section只能在当前View或它的Layout中被调用。@RenderSection方法没有找到参数指定的section会抛异常，
如果不确定section是否存在，则需要指定第二个参数的值为falsefalse，如下：

``` Html
@RenderSection("scripts", false) 
```

我们还可以通过 IsSectionDefined 方法来判断一个section是否被定义或在当前View中是否能调用得到，如：

``` Html
@if (IsSectionDefined("Footer")) { 
    @RenderSection("Footer") 
} else { 
    <h4>This is the default footer</h4>    
} 
```


##Partial View

Partial view（分部视图）是将部分 Razor 和 Html 标签放在一个独立的视图文件中，以便在不同的地方重复利用。

继续为此添加一个 List.cshtml 视图，并通过@Html.Partial方法来调用我们要呈现的分部视图，如下：

``` Html
@{
    ViewBag.Title = "List";
    Layout = null;
}
<h3>This is the /Views/Home/List.cshtml View</h3>
@Html.Partial("MyPartial")
```

视图引擎将按照规定的顺序先后在 /Views/Home 和 /Views/Shared 文件夹下查找 MyPartial 视图。


##Child Action

Child action 和 Patial view 类似，也是在应用程序的不同地方可以重复利用相同的子内容。
不同的是，它是通过调用 controller 中的 action 方法来呈现子内容的，并且一般包含了业务的处理。
任何 action 都可以作为子 action 


在 List.cshtml 视图中添加如下代码来调用 Time action 方法 ：

``` Html
@Html.Action("Time")
```

