[RabbitMQ](http://www.cnblogs.com/fanqiang/archive/2010/10/26/1861964.html)


Introduction:RabbitMQ is a complete and highly reliable enterprise messaging system based on the emerging 
AMQP standard. It is licensed under the open source Mozilla Public License and has a platform-neutral
 distribution, plus platform-specific packages and bundles for easy installation. 

RabbitMQ 是一个由 Erlang 写成的 实现，AMQP的出现其实也是应了广大人民群众的需求，虽然在同步消息通讯的世界里有很多公
开标准（如 COBAR 的 IIOP ，或者是 SOAP 等），但是在异步消息处理中却不是这样，只有大企业有一些商业实现（如微软的 MSMQ，
IBM 的 Websphere MQ 等），因此，在 2006 年的 6 月，Cisco 、Redhat、iMatix 等联合制定了 AMQP 的公开标准。反正现在这
个世道通常都是小公司拥抱标准，大企业自己搞一套标准，不过公开标准总还是对大众有利的。

RabbitMQ 是由 LShift 提供的一个 AMQP 的开源实现，由以高性能、健壮以及 Scalability 出名的 Erlang 写成，因此也是继承
了这些优点。不过，也许还有一些人对 Messaging 到底是干什么有些疑问，这里的消息和 XMPP 里通常是不一样的，比如一个大型系

统里，各个组件之间的相互交流，因此通常 QMQP 里对实时性、健壮性、容错性等各方面的要求都要比 XMPP 要高一些。AMQP 里主要
要说两个组件：Exchange 和 Queue （在 AMQP 1.0 里还会有变动），如下图所示，绿色的 X 就是 Exchange ，红色的是 Queue ，
这两者都在 Server 端，又称作 Broker ，这部分是 RabbitMQ 实现的，而蓝色的则是客户端，通常有 Producer 和 Consumer 
两种类型：

要使用 RabbitMQ 这个 Broker 非常简单，只要把 server 运行起来就可以了，当然 RabbitMQ 还提供了工具供服务器管理以及认证等，
不过作为一个简单的示例，可以不考虑这些，下载代码，然后运行即可（当然，要事先安装好 Erlang ，并且还需要 Python 以及 Python
的 json 支持，用于生成一部分代码）：
hg clone http://hg.rabbitmq.com/rabbitmq-codegen hg clone http://hg.rabbitmq.com/rabbitmq-server cd 

rabbitmq-server make run然后不管是 Producer 还是 Consumer 都是这个 Broker 的客户端，当然可以按照协议的定义手工来
做这个客户端，但是大家通常都更喜欢直接用 API ，这样可以把精力集中在系统的逻辑上，而不是琐碎的网络通讯、编码解码以及

同步等东西。正好 RabbitMQ 的客户端 API 是非常丰富的，除了 C/C++ ，其他流行的语言基本上都能找到好用的 API ，参见它的
官方列表。我搜索了一下，发现目前官方正在开发一个 C++ 的客户端 API ，但是没有任何文档，所以也不知道完成情况如何，不过
大致也可以理解，用 C/C++ 来做这样的模块，要处理网络、线程等东西的话，做到跨平台就很麻烦了，不仅库开发起来很难，大概开
发出来的库也用起来不太方便。