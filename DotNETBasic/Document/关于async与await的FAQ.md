##关于async与await的FAQ

###概念概述

1. 从哪能获得关于”async”和”await”主题的优秀资源？

    通常，你能在Visual Studio Async主题中找到很多资源（eg：文章、视频、博客等等）。2011年10月份的MSDN杂志包含了三篇介绍”async”和”await”主题的优秀文章。如果你阅读，我推荐你阅读顺序依次为：


    1) [《通过新的 Visual Studio Async CTP 更轻松地进行异步编程》](http://msdn.microsoft.com/zh-cn/magazine/hh456401.aspx)

    2) [《通过 Await 暂停和播放》](http://msdn.microsoft.com/zh-cn/magazine/hh456403.aspx)

    3) [《了解 Async 和 Await 的成本》](http://msdn.microsoft.com/zh-cn/magazine/hh456402.aspx)

    .NET团队博客同样也有”async”和”await”主题的优秀资源：[《Async in .NET4.5: 值得期待》](http://blogs.msdn.com/b/dotnet/archive/2012/04/03/async-in-4-5-worth-the-await.aspx)

2. 为什么需要编译器帮助我们完成异步编程？

    Anders Hejlsberg’s在2011 微软Build大会上花了1个小时来帮我们说明为什么编译器在这里真的有用，视频：[《C#和Visual Basic未来的发展方向》](http://channel9.msdn.com/events/BUILD/BUILD2011/TOOL-816T)。简而言之，传统的异步编程模型（APM或EAP）要求你手写大量代码（eg：连续传递委托、回调）来实现，并且这些代码会导致语句控制流混乱颠倒。通过.NET4.5提供的新的编程模型（TAP），你可以像在写同步代码一样使用常规的顺序控制流结合并行任务及”async”和”await”关键字来完成异步编程，编译器在后台应用必要的转换以使用回调方式来避免阻塞线程。

3. 通过Task.Run() 将同步方法包装成异步任务是否真的有益处？

    这取决于你的目标，你为什么要异步调用方法。如果你的目标只是想将当前任务切换到另一个线程执行，比如，保证UI线程的响应能力，那么肯定有益。如果你的目标是为了提高可扩展性，那么使用Task.Run() 包装成异步调用将没有任何实际意义。更多信息，请看《我是否应该公开同步方法对应的异步方法API？》。通过Task.Run() 你可以很轻松的实现从UI线程分担工作到另一个工作线程，且可协调后台线程一旦完成工作就返回到UI线程。（这里说的可扩展性就如当增加cpu时，Task.Run()并不会增加程序的并行效率，因为他只相当于启动了一个线程执行任务，倘若使用Parallel.For就具有更好的可扩展性。什么是系统的可扩展性？）


###“async”关键字

1. 将关键字”async”应用到方法上的作用是什么？

    当你用关键字”async”标记一个方法时，即告诉了编译器两件事：

    1)     你告诉编译器，想在方法内部使用”await”关键字（只有标记了”async”关键字的方法或lambda表达式才能使用”await”关键字）。这样做后，编译器会将方法转化为包含状态机的方法（类似的还有yield的工作原理，请看 《C#稳固基础：传统遍历与迭代器》 ），编译后的方法可以在await处挂起并且在await标记的任务完成后异步唤醒。

    2)     你告诉编译器，方法的结果或任何可能发生的异常都将作为返回类型返回。如果方法返回Task或Task<TResult>，这意味着任何结果值或任何在方法内部未处理的异常都将存储在返回的Task中。如果方法返回void，这意味着任何异常会被传播到调用者上下文。

    a)     async void函数只能在UI Event回调中使用。

    b)     async void函数中一定要用try-catch捕获所有异常，否则会很容易导致程序崩溃。另外需要特别注意lambda表达式，

    如：（List<T> 只有 public void ForEach(Action<T> action); 重载）

    ``` C#
    Enumerable.Range(0, 3).ToList().ForEach(async (i) => { throw new Exception(); }); 
    ```

    这段代码就隐式生成了async void 函数，直接导致了程序的crash。

    不过好在，编译器是优先考虑生成 async Task 形式的匿名函数的。即如下两个重载，编译器是使用ForEach(Func<T, Task> action);重载生成async Task 函数。

    ``` C#
    public void ForEach(Action<T> action);

    public void ForEach(Func<T, Task> action); 
    ```

    c)    注册TaskScheduler.UnobservedTaskException事件，记录Task中未处理异常信息，方便分析及错误定位。

2. 被”async”关键字标记的方法的调用都会强制转变为异步方式吗？

    不会，当你调用一个标记了”async”关键字的方法，它会在当前线程以同步的方式开始运行。所以，如果你有一个同步方法，它返回void并且你做的所有改变只是将其标记的”async”，这个方法调用依然是同步的。返回值为Task或Task<TResult>也一样。

    方法用”async”关键字标记不会影响方法是同步还是异步运行并完成，而是，它使方法可被分割成多个片段，其中一些片段可能异步运行，这样这个方法可能异步完成。这些片段界限就出现在方法内部显示使用”await”关键字的位置处。所以，如果在标记了”async”的方法中没有显示使用”await”，那么该方法只有一个片段，并且将以同步方式运行并完成。

3. “async”关键字会导致调用方法被排队到ThreadPool吗？会创建一个新的线程吗？

    全都不会，”async”关键字指示编译器在方法内部可能会使用”await”关键字，这样该方法就可以在await处挂起并且在await标记的任务完成后异步唤醒。这也是为什么编译器在编译”async” 标记的方法时，方法内部没有使用”await”会出现警告的原因（warning CS4014: 由于不等待此调用，因此会在此调用完成前继续执行当前方法。请考虑向此调用的结果应用"await"运算符）。

4. ”async”关键字能标记任何方法吗？

    不能，只有返回类型为void、Task或Task<TResult>的方法才能用”async”标记。并且，并不是所有返回类型满足上面条件的方法都能用”async”标记。如下，我们不允许使用”async”标记方法：

    1)  在程序的入口方法（eg：Main()），不允许。当你正在await的任务还未完成，但执行已经返回给方法的调用者了。Eg：Main()，这将退出Main()，直接导致退出程序。

    2)  在方法包含如下特性时，不允许。

    *  [MethodImpl(MethodImplOptions.Synchronized)]

    为什么这是不允许的，详细请看[《What’s New for Parallelism in .NET 4.5 Beta》](http://blogs.msdn.com/b/pfxteam/archive/2012/02/29/10274035.aspx)。此特性将方法标记为同步类似于使用lock/SyncLock同步基元包裹整个方法体。

    * [SecurityCritical]和[SecuritySafeCritical]   (Critical：关键)

      编译器在编译一个”async”标记的方法，原方法体实际上最终被编译到新生成的MoveNext()方法中，但是其标记的特性依然存在。这意味着特性如[SecurityCritical]不会正常工作。

    3) 在包含ref或out参数的方法中，不允许。调用者期望方法同步调用完成时能确保设置参数值，但是标记为”async”的方法可能不能保证立刻设置参数值直到异步调用完成。

    4) Lambda被用作表达式树时，不允许。异步lambda表达式不能被转换为表达式树。

5. 是否有任何约定，这时应该使用”async”标记方法？

    有，基于任务的异步编程模型(TAP)是完全专注于怎样实现异步方法，这个方法返回Task或Task<TResult>。这包括(但不限于)使用”async”和”await”关键字实现的方法。想要深入TAP，请看[《基于任务的异步编程模型》](http://www.microsoft.com/en-us/download/details.aspx?id=19957)文档。

6. “async”标记的方法创建的Tasks是否需要调用”Start()”？

    不需要，TAP方法返回的Tasks是已经正在操作的任务。你不仅不需要调用”Start()”，而且如果你尝试也会失败。更多细节，请看[《.NET4.X 并行任务中Task.Start()的FAQ》](http://www.cnblogs.com/heyuquan/archive/2013/02/01/2888312.html) 。

7. “async”标记的方法创建的Tasks是否需要调用”Dispose()”？

    不需要，一般来说，你不需要 Dispose() 任何任务。请看[《.NET4.X并行任务Task需要释放吗？》](http://www.cnblogs.com/heyuquan/archive/2013/02/28/2937701.html)。

8. “async”是如何关联到当前SynchronizationContext？

    对于”async” 标记的方法，如果返回Task或Task<TResult>，则没有方法级的SynchronizationContext交互；对于”async” 标记的方法，如果返回void，则有一个隐藏的SynchronizationContext交互。

    当一个”async void”方法被调用，方法调用的开端将捕获当前SynchronizationContext(“捕获”在这表示访问它并且将其存储)。如果这里有一个非空的SynchronizationContext，将会影响两件事：（前提：”async void”）

    1)     在方法调用的开始将导致调用捕获SynchronizationContext.OperationStarted()方法，并且在完成方法的执行时(无论是同步还是异步)将导致调用捕获SynchronizationContext.OprationCompleted()方法。这给上下文引用计数未完成异步操作提供时机点。如果TAP方法返回Task或Task<TResult>，调用者可通过返回的Task做到同样的跟踪。

    2)     如果这个方法是因为未处理的异常导致方法完成，那么这个异常将会提交给捕获的SynchronizationContext。这给上下文一个处理错误的时机点。如果TAP方法返回Task或Task<TResult>，调用者可通过返回的Task得到异常信息。

    当调用”async void”方法时如果没有SynchronizationContext，没有上下文被捕获，然后也不会调用OperaionStarted/OperationCompleted方法。在这种情况下，如果存在一个未处理过的异常在ThreadPool上传播，那么这会采取线程池线程默认行为，即导致进程被终止。

 

###“await”关键字

1. “await”关键字做了什么

    “await”关键字告诉编译器在”async”标记的方法中插入一个可能的挂起/唤醒点。

    逻辑上，这意味着当你写”await someObject;”时，编译器将生成代码来检查someObject代表的操作是否已经完成。如果已经完成，则从await标记的唤醒点处继续开始同步执行；如果没有完成，将为等待的someObject生成一个continue委托，当someObject代表的操作完成的时候调用continue委托。这个continue委托将控制权重新返回到”async”方法对应的await唤醒点处。

    返回到await唤醒点处后，不管等待的someObject是否已经经完成，任何结果都可从Task中提取，或者如果someObject操作失败，发生的任何异常随Task一起返回或返回给SynchronizationContext。

    在代码中，意味着当你写：

    await someObject;

    编译器会生成一个包含 MoveNext 方法的状态机类：


    ``` C#
    private class FooAsyncStateMachine : IAsyncStateMachine

    { 

        // Member fields for preserving “locals” and other necessary     state 

        int $state; 

        TaskAwaiter $awaiter; 

        … 

        public void MoveNext() 

        { 

            // Jump table to get back to the right statement upon         resumption 

            switch (this.$state) 

            { 

                … 

            case 2: goto Label2; 

                … 

            } 

            … 

            // Expansion of “await someObject;” 

            this.$awaiter = someObject.GetAwaiter(); 

            if (!this.$awaiter.IsCompleted) 

            { 

                this.$state = 2; 

                this.$awaiter.OnCompleted(MoveNext); 

                return; 

                Label2: 

            } 

            this.$awaiter.GetResult(); 

            … 

        } 

    } 
    ```

    在实例 someObject上使用这些成员来检查该对象是否已完成（通过 IsCompleted），如果未完成，则挂接一个续体（通过 OnCompleted），当所等待实例最终完成时，系统将再次调用 MoveNext 方法，完成后，来自该操作的任何异常将得到传播或作为结果返回（通过 GetResult），并跳转至上次执行中断的位置。

2. 什么是”awaitables”？什么是”awaiters”？

    虽然Task和Task<TResult>是两个非常普遍的等待类型(“awaitable”)，但这并不表示只有这两个的等待类型。

    “awaitable”可以是任何类型，它必须公开一个GetAwaiter() 方法并且返回有效的”awaiter”。这个GetAwaiter() 可能是一个实例方法（eg:Task或Task<TResult>的实例方法），或者可能是一个扩展方法。

    “awaiter”是”awaitable”对象的GetAwaiter()方法返回的符合特定的模式的类型。”awaiter”必须实现System.Runtime.CompilerServices.INotifyCompletion接口（，并可选的实现System.Runtime.CompilerServices.ICriticalNotifyCompletion接口）。除了提供一个INotifyCompletion接口的OnCompleted方法实现（，可选提供ICriticalNotifyCompletion接口的UnsafeCompleted方法实现），还必须提供一个名为IsCompleted的Boolean属性以及一个无参的GetResult()方法。GetResult()返回void，如果”awaitable”代表一个void返回操作，或者它返回一个TResult，如果”awaitable”代表一个TResult返回操作。

    几种方法来实现自定义的”awaitable” 谈论，请看[《await anything》](http://blogs.msdn.com/b/pfxteam/archive/2011/01/13/10115642.aspx)。也能针对特殊的情景实现自定义”awaitable”，请看[《Advanced APM Consumption in Async Methods》](http://blogs.msdn.com/b/pfxteam/archive/2012/01/23/10259822.aspx)和[《Awaiting Socket Operations》](http://blogs.msdn.com/b/pfxteam/archive/2011/12/15/10248293.aspx)。

3. 哪些地方不能使用”await”？

    1) 在未标记”async”的方法或lambda表达式中，不能使用”await”。”async”关键字告诉编译器其标记的方法内部可以使用”await”。（更详细，请看《Asynchrony in C# 5 Part Six: Whither async?》）

    2) 在属性的getter或setter访问器中，不能使用”await”。属性的意义是快速的返回给调用者，因此不期望使用异步，异步是专门为潜在的长时间运作的操作。如果你必须在你的属性中使用异步，你可以通过实现异步方法然后在你的属性中调用。

    3) 在lock/SyncLock块中，不能使用”await”。关于谈论为什么不允许，以及SemaphoreSlim.WaitAsync（哪一个能用于此情况的等待），请看《What’s New for Parallelism in .NET 4.5 Beta》。你还可以阅读如下文章，关于如何构建各种自定义异步同步基元：

    a)  [构建Async同步基元，Part 1 AsyncManualResetEvent](http://www.cnblogs.com/heyuquan/archive/2012/12/27/2835481.html)
       
    b)  [构建Async同步基元，Part 2 AsyncAutoResetEvent](http://www.cnblogs.com/heyuquan/archive/2013/01/14/2860112.html)
       
    c)  [构建Async同步基元，Part 3 AsyncCountdownEvent](http://www.cnblogs.com/heyuquan/archive/2013/01/15/2860631.html)
       
    d)  [构建Async同步基元，Part 4 AsyncBarrier](http://www.cnblogs.com/heyuquan/archive/2013/01/15/2861605.html)
       
    e)  [构建Async同步基元，Part 5 AsyncSemaphore](http://www.cnblogs.com/heyuquan/archive/2013/01/16/2862191.html)
       
    f)  [构建Async同步基元，Part 6 AsyncLock](http://www.cnblogs.com/heyuquan/archive/2013/01/16/2863094.html)
       
    g)  [构建Async同步基元，Part 7 AsyncReaderWriterLock](http://www.cnblogs.com/heyuquan/archive/2013/01/18/2865850.html)

    4)  在unsafe区域中，不能使用”await”。注意，你能在标记”async”的方法内部使用”unsafe”关键字，但是你不能在unsafe区域中使用”await”。

    5)  在catch块和finally块中，不能使用”await”。你能在try块中使用”await”，不管它是否有相关的catch块和finally块，但是你不能在catch块或finally块中使用”await”。这样做会破坏CLR的异常处理。

    6)  LINQ中大部分查询语法中，不能使用”await”。”await”可能只用于查询表达式中的第一个集合表达式的”from”子句或在集合表达式中的”join”子句。

4. “await task;”和”task.Wait”效果一样吗？

    不。

    “task.Wait()”是一个同步，可能阻塞的调用。它不会立刻返回到Wait()的调用者，直到这个任务进入最终状态，这意味着已进入RanToCompletion，Faulted，或Canceled完成状态。相比之下，”await task;”告诉编译器在”async”标记的方法内部插入一个隐藏的挂起/唤醒点，这样，如果等待的task没有完成，异步方法也会立马返回给调用者，当等待的任务完成时唤醒它从隐藏点处继续执行。当”await task;”会导致比较多应用程序无响应或死锁的情况下使用“task.Wait()”。更多信息请看《Await, and UI, and deadlocks! Oh my!》。

    当你使用”async”和”await”时，还有其他一些潜在缺陷。Eg：

    1) [避免传递lambda表达式的潜在缺陷](http://blogs.msdn.com/b/pfxteam/archive/2012/02/08/10265476.aspx)

    2) [保证”async”方法不要被释放](http://blogs.msdn.com/b/pfxteam/archive/2011/10/02/10219048.aspx)

    3) [不要忘记完成你的任务](http://blogs.msdn.com/b/pfxteam/archive/2011/10/02/10218999.aspx)

    4) [使用”await”依然可能存在死锁？](http://blogs.msdn.com/b/pfxteam/archive/2012/04/12/10293249.aspx)

5. “task.Result”与”task.GetAwaiter().GetResult()”之间存在功能区别吗？

    存在。但仅仅在任务以非成功状态完成的情况下。如果task是以RanToCompletion状态完成，那么这两个语句是等价的。然而，如果task是以Faulted或Canceled状态完成，task.Result将传播一个或多个异常封装而成的AggregateException对象；而”task.GetAwaiter().GetResult()”将直接传播异常(如果有多个任务，它只会传播其中一个)。关于为什么会存在这个差异，请看[《.NET4.5中任务的异常处理》](http://blogs.msdn.com/b/pfxteam/archive/2011/09/28/10217876.aspx)。

6. “await”是如何关联到当前SynchronizationContext？

    这完全取决于被等待的类型。对于给定的”awaitable”，编译器生成的代码最终会调用”awaiter”的OnCompleted()方法，并且传递将执行的continue委托。编译器生成的代码对SynchronizationContext一无所知，仅仅依赖当等待的操作完成时调用OnCompleted()方法时所提供的委托。这就是OnCompleted()方法，它负责确保委托在”正确的地方”被调用，”正确的地方”完全由”awaiter”决定。

    正在等待的任务(由Task和Task<TResult>的GetAwaiter方法分别返回的TaskAwaiter和TaskAwaiter<TResult>类型)的默认行为是在挂起前捕获当前的SynchronizationContext，然后等待task的完成，如果能捕获到当前的SynchronzationContext，调用continue委托将控制权返回到SynchronizationContext中。所以，例如，如果你在应用程序的UI线程上执行”await task;”，如果当前SynchronizationContext非空则将调用OnCompleted()，并且在任务完成时，将使用UI的SynchronizationContext传播continue委托返回到UI线程。

    当你等待一个任务，如果没有当前SynchronizationContext，那么系统会检查当前的TaskScheduler，如果有，当task完成时将使用TaskScheduler调度continue委托。

    如果SynchronizationContext和TaskScheduler都没有，无法迫使continue委托返回到原来的上下文，或者你使用”await task.ConfigureAwait(false)代替”await task;”，然后continue委托不会迫使返回到原来上下文并且将允许在系统认为合适的地方继续运行。这通常意味着要么以同步方式运行continue委托，无论等待的task在哪完成；要么使用ThreadPool中的线程运行continue委托。

7. 在控制台程序中能使用”await”吗？

    当然能。但你不能在Main()方法中使用”await”，因为入口点不能被标记为”async”。相反，你能在控制台应用程序的其他方法中使用”await”。如果你在Main()中调用这些方法，你可以同步等待(而不是异步等待)他们的完成。Eg：

    你还可以使用自定义的SynchronizationContext或TaskScheduler来实现相似的功能，更多信息请看：

    1)  [Await, SynchronizationContext, and Console Apps: Part 1](http://blogs.msdn.com/b/pfxteam/archive/2012/01/20/10259049.aspx)

    2)  [Await, SynchronizationContext, and Console Apps: Part 2](http://blogs.msdn.com/b/pfxteam/archive/2012/01/21/10259307.aspx)

    3)  [Await, SynchronizationContext, and Console Apps: Part 3](http://blogs.msdn.com/b/pfxteam/archive/2012/02/02/10263555.aspx)

8. “await”能和异步编程模型模式(APM)或基于事件的异步编程模式(EAP)一起使用吗？

    当然能，你可以为你的异步操作实现一个自定义的”awaitable”，或者将你现有的异步操作转化为现有的”awaitable”，像task或task<TResult>。示例如下：

    1)  [Tasks and the APM Pattern](http://blogs.msdn.com/b/pfxteam/archive/2009/06/09/9716439.aspx)

    2)  [Tasks and the Event-based Asynchronous Pattern](http://blogs.msdn.com/b/pfxteam/archive/2009/06/19/9791857.aspx)

    3)  [Advanced APM Consumption in Async Methods](http://blogs.msdn.com/b/pfxteam/archive/2012/01/23/10259822.aspx)

    4)  [Implementing a SynchronizationContext.SendAsync method](http://blogs.msdn.com/b/pfxteam/archive/2012/01/20/10259082.aspx)

    5)  [Awaiting Socket Operations](http://blogs.msdn.com/b/pfxteam/archive/2011/12/15/10248293.aspx)

    6)  [await anything](http://blogs.msdn.com/b/pfxteam/archive/2011/01/13/10115642.aspx)

    7)  [The Nature of TaskCompletionSource<TResult>](http://blogs.msdn.com/b/pfxteam/archive/2009/06/02/9685804.aspx)

9. 编译器对async/await生成的代码是否能高效异步执行？

    大多数情况下，是的。因为大量的生成代码已经被编译器所优化并且.NET Framework也为生成代码建立依赖关系。要了解更多信息，包括使用async/await的最小化开销的最佳实践等。请看

    1) [.NET4.5对TPL的性能提升](http://blogs.msdn.com/b/pfxteam/archive/2011/11/10/10235962.aspx)

    2) [2012年MVP峰会上的“The Zen of Async”](http://blogs.msdn.com/b/pfxteam/archive/2012/03/03/10277034.aspx)

    3) [《了解 Async 和 Await 的成本》](http://msdn.microsoft.com/zh-cn/magazine/hh456402.aspx)

 

 

原文：http://blogs.msdn.com/b/pfxteam/archive/2012/04/12/async-await-faq.aspx

作者：[Stephen Toub - MSFT](https://social.msdn.microsoft.com/profile/stephen%20toub%20-%20msft/)



[关于async与await的FAQ](http://www.cnblogs.com/heyuquan/archive/2012/11/30/2795859.html)
