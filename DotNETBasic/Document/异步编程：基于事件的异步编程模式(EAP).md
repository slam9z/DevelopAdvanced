##异步编程：基于事件的异步编程模式(EAP)

.NET2.0 中引入了：基于事件的异步编程模式(EAP，Event-based Asynchronous Pattern)。通过事件、AsyncOperationManager类和AsyncOperation类两个帮助器类实现如下功能：

1)   异步执行耗时的任务。

2)   获得进度报告和增量结果。

3)   支持耗时任务的取消。

4)   获得任务的结果值或异常信息。

5)   更复杂：支持同时执行多个异步操作、进度报告、增量结果、取消操作、返回结果值或异常信息。

对于相对简单的多线程应用程序，BackgroundWorker组件提供了一个简单的解决方案。对于更复杂的异步应用程序，可以考虑实现一个符合基于事件的异步模式的类。 


###EAP异步编程模型的优点

EAP是为Windows窗体开发人员创建的，其主要优点在于：

1.   EAP与Microsoft Visual Studio UI设计器进行了很好的集成。也就是说，可将大多数实现了EAP的类拖放到一个Visual Studio设计器平面上，然后双击事件名，让Visual Studio自动生成事件回调方法，并将方法同事件关联起来。

2.   EAP类在内部通过SynchronizationContext类，将应用程序模型映射到合适线程处理模型，以方便跨线程操作控件。


###AsyncOperationManager和AsyncOperation

AsyncOperationManager类和AsyncOperation类的API如下：


``` C#
// 为支持异步方法调用的类提供并发管理。此类不能被继承。

public static class AsyncOperationManager

{

    // 获取或设置用于异步操作的同步上下文。

    public static SynchronizationContext SynchronizationContext { get; set; }

 

    // 返回可用于对特定异步操作的持续时间进行跟踪的AsyncOperation对象。

    // 参数:userSuppliedState:

    //     一个对象，用于使一个客户端状态（如任务 ID）与一个特定异步操作相关联。

    public static AsyncOperation CreateOperation(object userSuppliedState)

    {

        return AsyncOperation.CreateOperation(userSuppliedState,SynchronizationContext);

    }

}

 

// 跟踪异步操作的生存期。

public sealed class AsyncOperation

{

    // 构造函数

    private AsyncOperation(object userSuppliedState, SynchronizationContext syncContext);

    internal static AsyncOperation CreateOperation(object userSuppliedState

                                            , SynchronizationContext syncContext);

 

    // 获取传递给构造函数的SynchronizationContext对象。

    public SynchronizationContext SynchronizationContext { get; }

    // 获取或设置用于唯一标识异步操作的对象。

    public object UserSuppliedState { get; }

 

    // 在各种应用程序模型适合的线程或上下文中调用委托。

    public void Post(SendOrPostCallback d, object arg);

    // 结束异步操作的生存期。

    public void OperationCompleted();

    // 效果同调用 Post() + OperationCompleted() 方法组合

    public void PostOperationCompleted(SendOrPostCallback d, object arg);

}
```


    先分析下这两个帮助器类：

1.   AsyncOperationManager是静态类。静态类是密封的，因此不可被继承。倘若从静态类继承会报错“静态类必须从 Object 派生”。（小常识，以前以为密封类就是 sealed 关键字）

2.   AsyncOperationManager为支持异步方法调用的类提供并发管理，该类可正常运行于 .NET Framework 支持的所有应用程序模式下。

3.   AsyncOperation实例提供对特定异步任务的生存期进行跟踪。可用来处理任务完成通知，还可用于在不终止异步操作的情况下发布进度报告和增量结果（这种不终止异步操作的处理是通过AsyncOperation的 Post() 方法实现）。

4.   AsyncOperation类有一个私有的构造函数和一个内部CreateOperation() 静态方法。由AsyncOperationManager类调用AsyncOperation.CreateOperation() 静态方法来创建AsyncOperation实例。

5.   AsyncOperation类是通过SynchronizationContext类来实现在各种应用程序的适当“线程或上下文”调用客户端事件处理程序。


    ```C#
    // 提供在各种同步模型中传播同步上下文的基本功能。

    public class SynchronizationContext

    {

        // 获取当前线程的同步上下文。

        public static SynchronizationContext Current { get; }

 

        // 当在派生类中重写时，响应操作已开始的通知。

        public virtual void OperationStarted();

        // 当在派生类中重写时，将异步消息调度到一个同步上下文。

        public virtual void Post(SendOrPostCallback d, object state);

        // 当在派生类中重写时，响应操作已完成的通知。

        public virtual void OperationCompleted();

        ……

    } 
    ```

    a)   在AsyncOperation构造函数中调用SynchronizationContext的OperationStarted() ；

    b)       在AsyncOperation的 Post() 方法中调用SynchronizationContext的Post() ；

    c)   在AsyncOperation的OperationCompleted()方法中调用SynchronizationContext的OperationCompleted()；

6.   SendOrPostCallback委托签名：

    // 表示在消息即将被调度到同步上下文时要调用的方法。

    public delegate void SendOrPostCallback(object state);


    

###基于事件的异步模式的特征

1.   基于事件的异步模式可以采用多种形式，具体取决于某个特定类支持操作的复杂程度：

    1)   最简单的类可能只有一个 ***Async方法和一个对应的 ***Completed 事件，以及这些方法的同步版本。

    2)   复杂的类可能有若干个 ***Async方法，每种方法都有一个对应的 ***Completed 事件，以及这些方法的同步版本。

    3)   更复杂的类还可能为每个异步方法支持取消（CancelAsync()方法）、进度报告和增量结果（ReportProgress() 方法+ProgressChanged事件）。

    4)   如果您的类支持多个异步方法，每个异步方法返回不同类型的数据，您应该：

    a)   将您的增量结果报告与您的进度报告分开。

    b)   使用适当的EventArgs为每个异步方法定义一个单独的 ***ProgressChanged事件以处理该方法的增量结果数据。

    5)   如果类不支持多个并发调用，请考虑公开IsBusy属性。

    6)   如要异步操作的同步版本中有 Out 和 Ref 参数，它们应做为对应 ***CompletedEventArgs的一部分，



2.   如果你的组件要支持多个异步耗时的任务并行执行。那么：

    1)   为***Async方法多添加一个userState对象参数（此参数应当始终是***Async方法签名中的最后一个参数），用于跟踪各个操作的生存期。

    2)   注意要在你构建的异步类中维护一个userState对象的集合。使用 lock 区域保护此集合，因为各种调用都会在此集合中添加和移除userState对象。

    3)   在***Async方法开始时调用AsyncOperationManager.CreateOperation并传入userState对象，为每个异步任务创建AsyncOperation对象，userState存储在AsyncOperation的UserSuppliedState属性中。在构建的异步类中使用该属性标识取消的操作，并传递给CompletedEventArgs和ProgressChangedEventArgs参数的UserState属性来标识当前引发进度或完成事件的特定异步任务。

    4)   当对应于此userState对象的任务引发完成事件时，你构建的异步类应将AsyncCompletedEventArgs.UserState对象从集合中删除。

3.   异常处理

    EAP的错误处理和系统的其余部分不一致。首先，异常不会抛出。在你的事件处理方法中，必须查询AsyncCompletedEventArgs的Exception属性，看它是不是null。如果不是null，就必须使用if语句判断Exception派生对象的类型，而不是使用catch块。 

    另外，如果你的代码忽略错误，那么不会发生未处理的异常，错误会变得未被检测到，应用程序将继续运行，其结果不可预知。 


###BackgroundWorker组件

System.ComponentModel命名空间的BackgroundWorker组件为我们提供了一个简单的多线程应用解决方案，它允许你在单独的线程上运行耗时操作而不会导致用户界面的阻塞。但是，要注意它同一时刻只能运行一个异步耗时操作（使用IsBusy属性判定），并且不能跨AppDomain边界进行封送处理（不能在多个AppDomain中执行多线程操作）。


``` C#
public class BackgroundWorker : Component

{

    public BackgroundWorker();

 

    // 获取一个值，指示应用程序是否已请求取消后台操作。

    public bool CancellationPending { get; }

    // 获取一个值，指示BackgroundWorker是否正在运行异步操作。

    public bool IsBusy { get; }

    // 获取或设置一个值，该值指示BackgroundWorker能否报告进度更新。

    public bool WorkerReportsProgress { get; set; }

    // 获取或设置一个值，该值指示BackgroundWorker是否支持异步取消。

    public bool WorkerSupportsCancellation { get; set; }

 

    // 调用RunWorkerAsync() 时发生。

    public event DoWorkEventHandlerDoWork;

    // 调用ReportProgress(System.Int32) 时发生。

    public event ProgressChangedEventHandlerProgressChanged;

    // 当后台操作已完成、被取消或引发异常时发生。

    public event RunWorkerCompletedEventHandlerRunWorkerCompleted;

 

    // 请求取消挂起的后台操作。

    public void CancelAsync();

    // 引发ProgressChanged事件。percentProgress:范围从 0% 到 100%

    public void ReportProgress(int percentProgress);

    // userState:传递到RunWorkerAsync(System.Object) 的状态对象。

    public void ReportProgress(int percentProgress, object userState);

    // 开始执行后台操作。

    public void RunWorkerAsync();

    // 开始执行后台操作。argument:传递给DoWork事件的DoWorkEventArgs参数。

    public void RunWorkerAsync(object argument);

}
```

6)   确保在DoWork事件处理程序中不操作任何用户界面对象。而应该通过ProgressChanged和RunWorkerCompleted事件与用户界面进行通信。

因为RunWorkerAsync() 是通过委托的BeginInvoke() 引发的DoWork事件，即DoWork事件的执行线程已不是创建控件的线程（我在《异步编程：异步编程模型 (APM)》中介绍了几种夸线程访问控件的方式）。而ProgressChanged和RunWorkerCompleted事件是通过帮助器类AsyncOperation的 Post() 方法使其调用发生在合适的“线程或上下文”中。


[异步编程：基于事件的异步编程模式(EAP)](http://www.cnblogs.com/heyuquan/archive/2013/04/01/2993085.html)