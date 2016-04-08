总结，本文主要讲了AppBuilder的Use方法的具体流程与扩展，三种扩展方法实际上都是对基础Use的封装，而基础Use方法总的来说可以接受四种middlewareObject

1. Delegate是最简单的，直接可以封装成三元组压入List
 
2. 有Initialize方法的类的实例，参数表第一项为一个AppFunc或者OwinMiddleware，只要其Invoke之后能返回一个Task就行，为了避免DynamicInvoke的弊端进行了一次封装，
 
3. 有Invoke方法的类的实例，参数表也需要汇聚到一个object[]中，这两种设计应该是有不同需求背景的，目前不知道究竟有什么不同
 
4. Type，这需要对这个类的构造方法进行封装，参考UseHandlerMiddleware的构造函数，第一个参数应该是一个AppFunc

[AppBuilder（一）Use汇总 ](http://www.cnblogs.com/hmxb/p/5299216.html)