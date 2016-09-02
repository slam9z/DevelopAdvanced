##异步编程：IAsyncResult异步编程模型 (APM)



###IAsyncResult设计模式----规范概述

使用IAsyncResult设计模式的异步操作是通过名为 Begin*** 和 End*** 的两个方法来实现的，这两个方法分别指代开始和结束异步操作。例如，FileStream类提供BeginRead和EndRead方法来从文件异步读取字节。这两个方法实现了 Read 方法的异步版本。

在调用 Begin*** 后，应用程序可以继续在调用线程上执行指令，同时异步操作在另一个线程上执行。（如果有返回值还应调用 End*** 来获取操作的结果）。

1.  Begin*** 

    a)         Begin*** 方法带有该方法的同步版本签名中声明的任何参数。

    b)         Begin*** 方法签名中不包含任何输出参数。方法签名最后两个参数的规范是：第一个参数定义一个AsyncCallback委托，此委托引用在异步操作完成时调用的方法。第二个参数是一个用户定义的对象。此对象可用来向异步操作完成时为AsyncCallback委托方法传递应用程序特定的状态信息（eg：可通过此对象在委托中访问End*** 方法)。另外，这两个参数都可以传递null。

    c)         返回IAsyncResult对象。


    ``` C#
    // 表示异步操作的状态。

    [ComVisible(true)]

    public interface IAsyncResult

    {

        // 获取用户定义的对象，它限定或包含关于异步操作的信息。

        object AsyncState { get; }

        // 获取用于等待异步操作完成的System.Threading.WaitHandle，待异步操作完成时获得信号。

        WaitHandle AsyncWaitHandle { get; }

        // 获取一个值，该值指示异步操作是否同步完成。

        bool CompletedSynchronously { get; }

        // 获取一个值，该值指示异步操作是否已完成。

        bool IsCompleted { get; }

    }

 

    // 常用委托声明（我后面示例是使用了自定义的带ref参数的委托）

    public delegate void AsyncCallback(IAsyncResult ar) 

    ```


2.  End*** 

    a)         End***  方法可结束异步操作，如果调用 End*** 时，IAsyncResult对象表示的异步操作还未完成，则 End*** 将在异步操作完成之前阻塞调用线程。

    b)         End*** 方法的返回值与其同步副本的返回值类型相同。End*** 方法带有该方法同步版本的签名中声明的所有out 和 ref 参数以及由BeginInvoke返回的IAsyncResult，规范上 IAsyncResult 参数放最后。

                             i.              要想获得返回结果，必须调用的方法;

                           ii.              若带有out 和 ref 参数，实现上委托也要带有out 和 ref 参数，以便在回调中获得对应引用传参值做相应逻辑;

    3)         总是调用 End***() 方法，而且只调用一次

    以下理由都是针对“I/O限制”的异步操作提出。然而，对于计算限制的异步操作，尽管都是用户代码，但还是推荐遵守此规则。


    I/O限制的异步操作：比如像带FileOptions.Asynchronous标识的FileStream，其BeginRead()方法向Windows发送一个I/O请求包（I/O Request Packet，IRP）后方法不会阻塞线程而是立即返回，由Windows将IRP传送给适当的设备驱动程序，IRP中包含了为BeginRead()方法传入的回调函数，待硬件设备处理好IRP后，会将IRP的委托排队到CLR的线程池队列中。

    必须调用End***方法，否则会造成资源的泄露。有的开发人员写代码调用Begin***方法异步执行I/O限制后就不需要进行任何处理了，所以他们不关心End***方法的调用。但是，出于以下两个原因，End***方法是必须调用的：

    a)         在异步操作时，对于I/O限制操作，CLR会分配一些内部资源，操作完成时，CLR继续保留这些资源直至End***方法被调用。如果一直不调用End***，这些资源会直到进程终止时才会被回收。（End***方法设计中常常包含资源释放）

    b)         发起一个异步操作时，实际上并不知道该操作最终是成功还是失败（因为操作由硬件在执行）。要知道这一点，只能通过调用End***方法，检查它的返回值或者看它是否抛出异常。

    另外，需要注意的是I/O限制的异步操作完全不支持取消（因为操作由硬件执行），但可以设置一个标识，在完成时丢弃结果来模拟取消行为。

 
###一、基于IAsyncResult构造一个异步API


1.  执行异步调用后，若我们需要控制后续执行代码在异步操作执行完之后执行，可通过下面三种方式阻止其他工作：（当然我们不推荐你阻塞线程或轮询浪费CPU时间）

    a)         IAsyncResult的AsyncWaitHandle属性，待异步操作完成时获得信号。

    b)         通过IAsyncResult的IsCompleted属性进行轮询。

    c)         调用异步操作的 End*** 方法。


2.  执行异步调用后，若我们不需要阻止后续代码的执行，那么我们可以把异步执行操作后的响应放到回调中进行。（推荐使用无阻塞式回调模式）


 

###二、使用委托进行异步编程

对于委托，编译器会为我们生成同步调用方法“invoke”以及异步调用方法“BeginInvoke”和“EndInvoke”。对于异步调用方式，公共语言运行库 (CLR) 将对请求进行排队并立即返回到调用方，由线程池的线程调度目标方法并与提交请求的原始线程并行运行，为BeginInvoke()方法传入的回调方法也将在同一个线程上运行。

异步委托是快速为方法构建异步调用的方式，它基于IAsyncResult设计模式实现的异步调用，即，通过BeginInvoke返回IAsyncResult对象；通过EndInvoke获取结果值。

 

###三、多线程操作控件

2.         安全方式访问控件

原理：从一个线程封送调用并跨线程边界将其发送到另一个线程，并将调用插入到创建控件线程的消息队列中,当控件创建线程处理这个消息时,就会在自己的上下文中执行传入的方法。（此过程只有调用线程和创建控件线程，并没有创建新线程）

注意：从一个线程封送调用并跨线程边界将其发送到另一个线程会耗费大量的系统资源，所以应避免重复调用其他线程上的控件。

1)         使用BackgroundWork后台辅助线程控件方式（详见：基于事件的异步编程模式(EMP)）。

2)         结合TaskScheduler.FromCurrentSynchronizationContext() 和Task 实现。

3)         捕获线程上下文ExecuteContext，并调用ExeceteContext.Run()静态方法在指定的线程上下文中执行。（详见：执行上下文）

4)         使用Control类上提供的Invoke 和BeginInvoke方法。

5)         在WPF应用程序中可以通过WPF提供的Dispatcher对象提供的Invoke方法、BeginInvoke方法来完成跨线程工作。


**别人的加的线程可以调用外部的方法，为什么自己不可以呢**



[异步编程：IAsyncResult异步编程模型 (APM)](http://www.cnblogs.com/heyuquan/archive/2013/03/22/2976420.html)