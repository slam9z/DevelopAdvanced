##深入探究 WinRT 和 await

首先，我们先来审视一下没有 await 的情况。

###基础知识回顾

WinRT 中的所有异步功能全部源自同一个接口：IAsyncInfo。

``` C#
public interface IAsyncInfo
{
    AsyncStatus Status { get; }
    HResult ErrorCode { get; }
    uint Id { get; }

    void Cancel();
    void Close();
}
```

WinRT 中的每个异步操作都需要实施此接口，该接口可提供执行异步操作所需的基本功能，查询其标识和状态，并请求其取消。但该特定接口缺少对异步操作来说无疑是至关重要的功能：当操作完成时，通过回调通知监听器。该功能有意地划分到了四个依赖 IAsyncInfo 的其他接口中，而 WinRT 中的每个异步操作都需要实施以下四个接口之一：

``` C#
public interface IAsyncAction : IAsyncInfo
{
    AsyncActionCompletedHandler Completed { get; set; }
    void GetResults();
}

public interface IAsyncOperation<TResult> : IAsyncInfo
{
    AsyncOperationCompletedHandler<TResult> Completed { get; set; }
    TResult GetResults();
}

public interface IAsyncActionWithProgress<TProgress> : IAsyncInfo
{
    AsyncActionWithProgressCompletedHandler<TProgress> Completed { get; set; }
    AsyncActionProgressHandler<TProgress> Progress { get; set; }
    void GetResults();
}

public interface IAsyncOperationWithProgress<TResult, TProgress> : IAsyncInfo
{
    AsyncOperationWithProgressCompletedHandler<TResult, TProgress> Completed { get; set; }
    AsyncOperationProgressHandler<TResult, TProgress> Progress { get; set; }
    TResult GetResults();
}
```



这四个接口支持带有/不带结果，以及带有/不带进度报告的全部组合。所有这些接口都会公开一个 Completed 属性，该属性可以设置为在操作完成时调用的委派。您只能设置一次该委派，而如果该委派在操作完成后设置，它将通过处理操作完成和分配委派之间先后顺序的实施，立即加入计划或得到调用。

###编译器转换

**其实理解await还蛮简单的，在不需要await的时候写一次调用，然后再思考await代码生成的代码，实际效果是等效的**


###转换为任务

TaskCompletionSource

WindowsRuntimeSystemExtensions的AsTask方法

###直接等待 WinRT 异步操作


###自定义等待行为

如前所述，TaskAwaiter 和 TaskAwaiter<TResult> 可提供满足编译器对等待程序的期待所需的全部成员：

``` C#
bool IsCompleted { get; }
void OnCompleted(Action continuation);
TResult GetResult(); //returns void on TaskAwaiter
```

其中最有趣的成员为 OnCompleted，它将在所等待的操作完成时，负责调用续体委派。OnCompleted 提供特殊封送行为，以确保续体委派在正确的位置执行。

在默认情况下，当任务等待程序的 OnCompleted 得到调用时，会通知当前的 SynchronizationContext，SynchronizationContext 是代码执行环境的抽象表示。在 Metro 风格应用程序的 UI 线程中，SynchronizationContext.Current 会返回一个内部 WinRTSynchronizationContext 类型的实例。SynchronizationContext 会提供一个虚拟的 Post 方法，该方法会接受一个委派，并在上下文的适当位置执行该委派；WinRTSynchronizationContext 封装了一个 CoreDispatcher，并使用其 RunAsync 将委派异步调用回 UI 线程（我们稍早前已在本博文中手动实施了该功能）。当所等待的任务完成时，委派将作为 Post 传递给 OnCompleted，以便在调用 OnCompleted 时捕获的当前 SynchronizationContext 中执行。正是该机制允许您在 UI 逻辑中使用 await 编写代码，而无需担心是否能封送回正确的线程：任务的等待程序将为您处理妥当。

当然，在某些情况下，您可能不希望执行这种默认的封送行为。此类情况多在库中出现：许多类型的库不关心操作 UI 控件及运行其自身的线程，因此从性能的角度来考虑，避免与跨线程封送相关的开销将不无裨益。为了适应希望禁用这种默认封送行为的代码，Task 和 Task<TResult> 提供了 ConfigureAwait 方法。ConfigureAwait 接受布尔值 continueOnCapturedContext 参数：传递 True 表示使用默认行为，传递 False 表示系统不需要将委派的调用强制封送回原始上下文，而是在系统认为适当的任意位置执行该委派。

因此，如果您希望在不强迫剩余的代码返回 UI 线程执行的情况下等待某个 WinRT 操作，请编写以下任一代码作为替换：

``` C#
await SomeMethodAsync();

//或者：


await SomeMethodAsync().AsTask();

//您可以编写：


await SomeMethodAsync().AsTask()
                      .ConfigureAwait(continueOnCapturedContext:false);

//或仅编写：


await SomeMethodAsync().AsTask().ConfigureAwait(false);
```

###何时使用 AsTask


[深入探究 WinRT 和 await](http://blogs.msdn.com/b/windowsappdev_cn/archive/2012/04/30/winrt-await.aspx)