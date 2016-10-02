[浅谈ASP.NET的Postback ](http://www.cnblogs.com/artech/archive/2007/04/06/702658.html)

这篇Blog的主旨就是从方法调用的角度讲述整个程序运行的过程：从HTML 被Render到Client端，到用户Click某个按钮，输入被Postback到Server端，
并触发两个Event，执行Event Handler打印出相关的Message。



然后我们来看看Server如何处理这个Postback，关于Web Page的生命周期在这里就不详细介绍了。
Server端通过__EVENTTARGET这个hidden field的值找到对应的Server端的Control，
通过Reflection确定该Control是否实现了System.Web.UI.IPostBackEventHandler Interface。
如果该Control确实实现了该Interface，那么调用Page的RaisePostBackEvent方法，这是一个Virtual的方法，可以被Override。我们来看该方法的定义。

```cs
[EditorBrowsable(EditorBrowsableState.Advanced)]
protected virtual void RaisePostBackEvent(IPostBackEventHandler sourceControl, string eventArgument)
{
    sourceControl.RaisePostBackEvent(eventArgument);
}
```

我们可以看到该方法直接调用该sourceControl的RaisePostBackEvent，并传入一个eventArgument参数，
在这个例子中sourceControl就是__EVENTTARGET对应的Web Control：Button2，eventArgument就是__EVENTTARGET对应的值：一个空字符串。
Button2的类型是System.Web.UI.WebControls.Button。我们来看看System.Web.UI.WebControls.Button中的RaisePostBackEvent方法是如何定义的：

```cs
protected virtual void RaisePostBackEvent(string eventArgument)
{
    base.ValidateEvent(this.UniqueID, eventArgument);
    if (this.CausesValidation)
    {
        this.Page.Validate(this.ValidationGroup);
    }
    this.OnClick(EventArgs.Empty);
    this.OnCommand(new CommandEventArgs(this.CommandName, this.CommandArgument));
}
```