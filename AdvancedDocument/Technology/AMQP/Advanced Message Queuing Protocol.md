术语：AMQP:Advanced Message Queuing Protocol，高级消息队列协议，是应用层协议的一个开放标准，为面向消息
的中间件设计。AMQP的主要特征是面向消息、队列、路由（包括点对点和发布/订阅）、可靠性、安全性要求很严格。

AMQP在消息提供者和客户端的行为进行了强制规定，使得不同卖商之间真正实现了互操作能力。JMS是早期消息中间件进行标
准化的一个尝试，它仅仅是在API级进行了规范，离创建互操作能力还差很远。

与JMS不同，AMQP是一个Wire级的协议，它描述了在网络上传输的数据的格式，以字节为流。因此任何遵守此数据格式的工具
，其创建和解释消息，都能 与其他兼容工具进行互操作。

AMQP规范的版本：0-8        是2006年6月发布0-9        于2006年12月发布0-9-1     于2008年11月发布0-10      
于2009年下半年发布1.0 draft  （文档还是草案） 
 
AMQP的实现有：

* OpenAMQAMQP的开源实现，用C语言编写，运行于Linux、AIX、Solaris、Windows、OpenVMS。
* Apache QpidApache的开源项目，支持C++、Ruby、Java、JMS、Python和.NET。
* Redhat Enterprise MRG实现了AMQP的最新版本0-10，提供了丰富的特征集，比如完全管理、联合、Active-Active集群，
有Web控制台，还有许多企业级特征，客 户端支持C++、Ruby、Java、JMS、Python和.NET。
* RabbitMQ一个独立的开源实现，服务器端用Erlang语言编写，支持多种客户端，如：Python、Ruby、.NET、Java、JMS、C、
PHP、 ActionScript、XMPP、STOMP等，支持AJAX。RabbitMQ发布在Ubuntu、FreeBSD平台。
* AMQP InfrastructureLinux下，包括Broker、管理工具、Agent和客户端。
* ?MQ一个高性能的消息平台，在分布式消息网络可作为兼容AMQP的Broker节点，绑定了多种语言，包括Python、C、C++、Lisp、Ruby等。
* Zyre是一个Broker，实现了RestMS协议和AMQP协议，提供了RESTful HTTP访问网络AMQP的能力。 

