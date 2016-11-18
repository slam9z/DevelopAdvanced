##What can RabbitMQ do for you?

*Messaging enables software applications to connect and scale. Applications can connect to each other, 
as components of a larger application, or to user devices and data. Messaging is asynchronous, decoupling
applications by separating sending and receiving data.*

You may be thinking of data delivery, non-blocking operations or push notifications. Or you want to use 
publish / subscribe, asynchronous processing, or work queues. All these are patterns, and they form part of messaging. 

RabbitMQ is a messaging broker - an intermediary for messaging. It gives your applications a common platform 
to send and receive messages, and your messages a safe place to live until received. 

##Feature Highlights

###Reliability

RabbitMQ offers a variety of features to let you trade off performance with reliability, including persistence, 
delivery acknowledgements, publisher confirms, and high availability. 

###Flexible Routing

Messages are routed through exchanges before arriving at queues. RabbitMQ features several built-in exchange 
types for typical routing logic. For more complex routing you can bind exchanges together or even write your 
own exchange type as a plugin. 

###Clustering

Several RabbitMQ servers on a local network can be clustered together, forming a single logical broker. 

###Federation

For servers that need to be more loosely and unreliably connected than clustering allows, RabbitMQ offers a
federation model. 

###Highly Available Queues

Queues can be mirrored across several machines in a cluster, ensuring that even in the event of hardware failure 
your messages are safe. 

###Multi-protocol

RabbitMQ supports messaging over a variety of messaging protocols. 

###Many Clients

There are RabbitMQ clients for almost any language you can think of. 

###Management UI

RabbitMQ ships with an easy-to use management UI that allows you to monitor and control every aspect of 
your message broker. 

###Tracing

If your messaging system is misbehaving, RabbitMQ offers tracing support to let you find out what's going on. 

###Plugin System

RabbitMQ ships with a variety of plugins extending it in different ways, and you can also write your own. 

##And Also...

###Commercial Support

Commercial support, training and consulting are available from Pivotal. 

###Large Community

There's a large community around RabbitMQ, producing all sorts of clients, plugins, guides, etc. Join our
 mailing list to get involved! 