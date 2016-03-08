##Socket


###Socket原理

    Socket是建立在各种底层传输层上的一个编程抽象接口，门面模式的设计模式。
    
    Socket概念与方法
    
* Type(Tcp ,Udp 等)
* EndPoint(IP and port)
* 客户端方法
    
    Connet  Write  Read  Close

* 服务端方法
    
    Bind   Listen   Accept  Read  Write  Close


[揭开Socket编程的面纱](http://goodcandle.cnblogs.com/archive/2005/12/10/294652.aspx)   

[Network socket-Wiki](https://en.wikipedia.org/wiki/Network_socket)

###Socket .NET实现

####Socket

    Namespace：System.Net.Sockets

这常用的Tcp类型ipv4  
``` C#  
    // create the socket
    Socket listenSocket = new Socket(AddressFamily.InterNetwork, 
                                        SocketType.Stream,
                                        ProtocolType.Tcp);
    // bind the listening socket to the port
    IPAddress hostIP = (Dns.Resolve(IPAddress.Any.ToString())).AddressList[0];
    IPEndPoint ep = new IPEndPoint(hostIP, port);
    listenSocket.Bind(ep); 
    // start listening
    listenSocket.Listen(backlog);
```

基本方法

* Bind(EndPoint)
* Listen(Int32)
最大连接数
* Accept方法
    
包含3种编程模型

Socket Accept()
bool AcceptAsync(SocketAsyncEventArgs)
IAsyncResult BeginAccept(AsyncCallback,object state)

    



[C#Socket 异步代码示例](http://www.cnblogs.com/klvk/archive/2011/07/05/2098632.html)

[C# Socket简单例子（服务器与客户端通信）同步](http://blog.csdn.net/andrew_wx/article/details/6629721)

##Tcp

###Tcp协议

###Tcp .NET实现

