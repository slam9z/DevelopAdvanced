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

* 编程模型
    * 同步
    * AsyncCallback Delegate and State Object

       AsyncCallback 

    * Event-based Asynchronous Pattern

        SocketAsyncEventArgs,Completed事件是必须的。这种编程感觉更加难理解


[Using an AsyncCallback Delegate and State Object](https://msdn.microsoft.com/zh-cn/library/ms228978(v=vs.110).aspx)

[Event-based Asynchronous Pattern (EAP)](https://msdn.microsoft.com/zh-cn/library/ms228969(v=vs.110).aspx)


* Accept方法
    
Socket Accept()  
bool AcceptAsync(SocketAsyncEventArgs)  
IAsyncResult BeginAccept(AsyncCallback,object state)  
EndAccept(IAsyncResult)

* Receive

* Send
   
* Shutdown(SocketShutdown)

* Close()

* Connect(EndPoint)


[C#Socket 异步代码示例](http://www.cnblogs.com/klvk/archive/2011/07/05/2098632.html)

[C# Socket简单例子（服务器与客户端通信）同步](http://blog.csdn.net/andrew_wx/article/details/6629721)

进阶，对具体实现由更深入的解释，实际使用需要理解这些。  
[深入探析c# Socket](http://www.cnblogs.com/tianzhiliang/archive/2010/09/08/1821623.html)

[C# SocketAsyncEventArgs High Performance Socket Code](http://www.codeproject.com/Articles/83102/C-SocketAsyncEventArgs-High-Performance-Socket-Cod)

##Tcp

###Tcp协议

####首部格式

![格式图释](http://pic002.cnblogs.com/images/2012/387401/2012070916030558.png)

####数据单位

TCP 传送的数据单位协议是 TCP 报文段(segment)

 
####特点

TCP 是面向连接的传输层协议  
每一条 TCP 连接只能有两个端点(endpoint),每一条 TCP 连接只能是点对点的（一对一）  
TCP 提供可靠交付的服务  
TCP 提供全双工通信  
面向字节流  

####具体实现

说明:

* TCP 连接的每一端都必须设有两个窗口 一个发送窗口和一个接收窗口
* TCP 可靠传输机制用字节的序号进行控制.TCP 所有的确认都是基于序号而不是基于报文段
* TCP 两端的四个窗口经常处于动态变化之中
* TCP连接的往返时间 RTT 也不是固定不变的.需要使用特定的算法估算较为合理的重传时间


####运输连接

三个阶段:

* 连接建立
    3次握手 （SYN，ACK,ack，seq）  ACK(Acknowledgement) SYN(握手信号)
![建立Tcp连接](http://pic002.cnblogs.com/images/2012/387401/2012070916204517.jpg)
* 数据传送
* 连接释放 (Fin,seq,ACK,ack)
    4次
![释放Tcp连接](http://pic002.cnblogs.com/images/2012/387401/2012070916205749.jpg)


####拥塞处理相关概念

#####拥塞窗口:

含义:

拥塞窗口的大小取决于网络的拥塞程度,并且动态地在变化.发送方让自己的发送窗口等于拥塞窗口.如再考虑到接收方的接收能力,则发送窗口还可能小于拥塞窗口


发送方控制拥塞窗口的原则:

只要网络没有出现拥塞,拥塞窗口就再增大一些,以便把更多的分组发送出去.但只要网络出现拥塞,拥塞窗口就减小一些,以减少注入到网络中的分组数

####TCP 的有限状态机

![TCP 的有限状态机](http://pic002.cnblogs.com/images/2012/387401/2012070916285990.png)

[(传输层)TCP协议](http://www.cnblogs.com/kzloser/articles/2582957.html)

 
###IP协议 

IP协议是TCP/IP协议簇中的核心协议，也是TCP/IP的载体。所有的TCP，UDP，ICMP及IGMP数据都以IP数据报格式传输。
IP提供不可靠的，无连接的数据传送服务。

![IP包头](http://p.blog.csdn.net/images/p_blog_csdn_net/houdong/e265a35bb710429bba4ab4b09c5f745d.jpg)

[IP协议](http://blog.csdn.net/houdong/article/details/1505798)


####IP路由选择

直接交付与间接交付

间接交付怎么进行的？还是未完全搞懂


如果目的主机与源主机直接相连（点对点）或都在一个共享网络上（以太网），那么IP数据报就直接送达到目的主机上。否则，主机把数据报发到网关（路由器），由路由器来转发该数据报。
IP路由选择是逐跳的进行的。IP并不知道到达任何目的的完整路径。所有的IP路由选择只为数据报传送提供下一站路由器的IP地址。它假定下一站路由器比发送数据报的主机更接近目的，并且下一站路由器与该主机是直接相连的。

IP路由选择主要完成以下功能：

⑴搜索路由表，寻找与目的IP地址完全匹配的条目。

⑵如果⑴失败，则寻找与目的网络号匹配的条目。

⑶如果⑴和⑵都失败，则寻找默认路路由。如果找到，则把报文发送给该条目指定的下一站路由器。如果未找到，则丢弃数据报并向源发送ICMP不可达。

[路由选择](http://www.cnblogs.com/chenny7/p/3988374.html)




*了解这些底层协议，也可以开阔自己的编程思维！要想更清楚的了解，可能还是要看书啊！Wireshark工具了解网络层*

###OSI(Open System Interconnection)模型

* 物理层
   
    物理层处于OSI七层模型的最低端，它的主要任务是将比特流与电子信号进行转换。 0,1 Bit流

* 数据链路层

    数据链路层处于OSI七层模型的第二层，它定义了通过通信介质相互连接的设备之间，数据传输的规范。
    在数据链路层中，数据不再以0、1序列的形式存在，它们被分割为一个一个的“帧”，然后再进行传输。

    * MAC地址
        MAC地址是被烧录到网卡ROM中的一串数字，长度为48比特，它在世界范围内唯一(不考虑虚拟机自定义MAC地址)。

    * 分组交换
        分组交换是指将较大的数据分割为若干个较小的数据，然后依次发送。使用分组交换的原因是不同的数据链路有各自的最大传输单元(MTU: Maximum Transmission Unit)。

    * 以太网帧

        ![](http://cc.cocimg.com/api/uploads/20160219/1455851677534297.png)

    * 交换机
        交换机是一种在数据链路层工作的网络设备，它有多个端口，可以连接不同的设备。交换机根据每个帧中的目标MAC地址决定向哪个端口发送数据，此时它需要参考“转发表”
    
        ![](http://cc.cocimg.com/api/uploads/20160219/1455851751361202.png)

* 网络层

    IP协议处于OSI参考模型的第三层——网络层，网络层的主要作用是实现终端节点间的通信。IP协议是网络层的一个重要协议，网络层中还有ARP(获取MAC地址)和ICMP协议(数据发送异常通知)

    * IP地址
        IP地址是一种在网络层用于识别通信对端信息的地址。

        从功能上看，IP地址由两部分组成：网络标识和主机标识。

        网络标识用于区分不同的网段，相同段内的主机必须拥有相同的网络表示，不同段内的主机不能拥有相同的网络标识。

        主机标识用于区分同一网段下不同的主机，它不能在同一网段内重复出现。

        32位IP地址被分为两部分，到底前多少位是网络标识呢？一般有两种方法表示：IP地址分类、子网掩码

    * IP分类

        IP地址分为四个级别，分别为A类、B类、C类和D类。分类的依据是IP地址的前四位：

        A类IP地址是第一位为“0”的地址。A类IP地址的前8位是网络标识，用十进制标识的话0.0.0.0-127.0.0.0是A类IP地址的理论范围。另外我们还可以得知，A类IP地址最多只有128个(实际上是126个，下文不赘述)，每个网段内主机上限为2的24次方，也就是16，777，214个。

        B类IP地址是前两位为“10“的地址。B类IP地址的前16位是网络标识，用十进制标识的话128.0.0.0-191.255.0.0是B类IP地址的范围。B类IP地址的主机标记长度为16位，因此一个网段内可容纳主机地址上限为65534个。

        C类IP地址是前三位为“110”的地址。C类IP地址的前24位是网络标识，用十进制标识的话192.0.0.0-239.255.255.0是C类IP地址的范围。C类地址的后8位是主机标识，共容纳254个主机地址。

        D类IP地址是前四位为“1110”的地址。D类IP地址的网络标识长32位，没有主机标识，因此常用于多播。


    * 子网掩码

        IP地址总长度32位，它能表示的主机数量有限，大约在43亿左右。而IP地址分类更是造成了极大的浪费，A、B类地址一共也就一万多个，而世界上包含主机数量超过254的网段显然不止这么点。

        我们知道IP地址分类的本质是区分网络标识和主机标识，另一种更加灵活、细粒度的区分方法是使用子网掩码。

        子网掩码长度也是32位，由一段连续的1和一段连续的0组成。1的长度就表示网络标识的长度。以IP地址172.20.100.52为例，它本来是一个B类IP地址(前16位是网络标识)，但通过子网掩码，它可以是前26为为网络标识的

    * 路由控制

        路由控制(Routing)是指将分组数据发送到目标地址的功能，这个功能一般由路由器完成。(不要与家里用的小型无线路由器混为一谈)

        ![](http://cc.cocimg.com/api/uploads/20160219/1455852332402589.png)

    * DNS 解析
        ![](http://cc.cocimg.com/api/uploads/20160219/1455863583783234.png)

    * ARP 协议
        ![](http://cc.cocimg.com/api/uploads/20160219/1455863615816282.png)


    * NAT 和 NAPT 技术
        NAT (Network Address Translator) 是一种用于将局域网中的私有地址转换成全局 IP 地址的技术。
        ![NAT工作原理](http://cc.cocimg.com/api/uploads/20160219/1455863663557104.png)

        如果外部地址要访问内部地址可以做到吗?

        附加端口区分
        ![NAPT工作原理](http://cc.cocimg.com/api/uploads/20160219/1455863693297644.png)

        [NAT原理与NAT穿越](http://www.cnblogs.com/bo083/articles/2170189.html)

    [TCP/IP：数据链路层、IP协议以及IP协议相关技术](http://www.cocoachina.com/programmer/20160225/15363.html) 

###Tcp .NET实现

####TcpClient

* Connect

    与Socket一样
* NetworkStream GetStream

    然后 Read Write  CanRead

    接收数据搞不懂啊！竟然不是事件模型。

* NetworkStream
        

####TcpListener

* Start
* TcpClient  AcceptTcpClient

    然后与客户端一样

    建立连接的目标就是接收数据，然后读取数据。

    如果不是事件驱动，用同一个连接双方怎么知道对方有发送新的数据呢？好像读写完就关闭了。使用循环！

``` C#
private void HandleClientComm(object client)
{
    TcpClient tcpClient = (TcpClient)client;
    NetworkStream clientStream = tcpClient.GetStream();

    byte[] message = new byte[4096];
    int bytesRead;

    while (true)
    {
        bytesRead = 0;

        try
        {
            //blocks until a client sends a message
            bytesRead = clientStream.Read(message, 0, 4096);
        }
        catch
        {
            //a socket error has occured
            break;
        }

        if (bytesRead == 0)
        {
            //the client has disconnected from the server
            break;
        }

        //message has successfully been received
        ASCIIEncoding encoder = new ASCIIEncoding();
        System.Diagnostics.Debug.WriteLine(encoder.GetString(message, 0, bytesRead));
    }

    tcpClient.Close();
}
```

    [c#网络编程使用tcpListener和tcpClient](http://ilewen.com/questions/514)