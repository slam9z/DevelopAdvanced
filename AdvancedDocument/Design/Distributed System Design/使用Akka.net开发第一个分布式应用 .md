[使用Akka.net开发第一个分布式应用 ](http://www.cnblogs.com/richieyang/p/4945905.html)

## 一、什么是Actor模型

Actor模型由Carl Hewitt于上世纪70年代早期提出并在Erlang语言中得到了广泛应用，目的是为了解决分布式编程中一系列问题。其主要特点如下：

* 系统由Actor构成 
* Actor之间完全独立 
* 消息传递是非阻塞和异步的 
* 所有消息发送都是并行的 

[基于消息传递的软件架构模型演变 ](http://www.cnblogs.com/richieyang/p/4907441.html)


## 三、响应式宣言

说到Akka.net不得不提到响应式宣言。随着互联网和软件行业的发展，早先的软件架构已经不适应社会发展的需求。
软件系统的架构应该具备：弹性、松耦合、可伸缩性，更加容易开发和维护，发生错误时能够自我容错。所以响应式系统的概念随之而来：

* Responsive：The system responds in a timely manner if at all possible（系统应尽可能的及时响应） 
* Resilient: The system stays responsive in the face of failure（系统在发生错误时任然能够及时响应） 
* Elastic: The system stays responsive under varying workload（系统在各种负载之下都能及时响应） 
* Message Driven: Reactive Systems rely on asynchronous message-passing to establish a boundary between components that ensures loose coupling, isolation, location transparency, and provides the means to delegate errors as messages.（反应式系统依赖于异步消息组件之间建立边界确保松耦合、隔离、位置透明并提供委托错误消息的手段，这句翻译的有点不够准确） 

![](http://images2015.cnblogs.com/blog/600216/201511/600216-20151107192559211-1922269278.png)

## 六、Actor结构

![](http://images2015.cnblogs.com/blog/600216/201511/600216-20151107193750461-319874135.png)
