[Distributed Systems Patterns](https://msdn.microsoft.com/en-us/library/ff648311.aspx)



In today's connected world, enterprise applications increasingly run distributed across multiple servers, connect to
 remote data sources and Web services, and are accessible over the Internet. Distributed computing is powerful, 
but it is not without challenges. Networks are inherently unreliable, and communicating with remote servers is slow 
when compared to local interprocess communication. In addition, running a program simultaneously across multiple 
computers can introduce a host of concurrency and synchronization issues.

##Instance-Based vs. Service-Based Collaboration[协作] 

//.NET Romoting  vs Web Service or Web API
 
Distributed computing can be based on two distinct architectural styles, according to Business Component Factory [Herzum00]:

* Instance-based collaboration
* Service-based collaboration

*Instance-based collaboration* extends the model of object-oriented computing across network boundaries. A component
 can instantiate[用具体例证说明] remote object instances, pass references to these remote objects around, invoke methods on the remote 
objects, and de-allocate them. The advantage of this approach is that the same object-oriented programming model used 
inside the application applies to the distributed components. Most runtime platforms incorporate support for instance-based
 collaboration so that a developer has to make no (or few) special provisions to access a remote object versus a local object.
 This simplifies developing a distributed solution tremendously, often to the point where previously co-located objects 
can be distributed during deployment time without requiring any code changes to the application. Instance-based collaboration
 also gives the consumer of a remote object fine-grained control over the lifetime of the remote object, allowing more
 efficient usage of remote resources.


The ease-of-use of instance-based collaboration, however, comes at the expense of a complex interaction model 
and tight coupling between consumer and provider. Instance-based interaction requires a specific instance of a
 remote object to be addressable over the network, introducing the complexities of lifetime and instance management
 into the communications protocol. For this reason, most platforms that support instance-based collaboration do not
 provide interoperability with other platforms.

*Service-based collaboration* addresses some of these challenges by exposing only a "manager-like" or 
"coordinator-like" interface to potential consumers. Consumers can invoke a method on this interface but 
they do not have lifetime control over any remote objects This simplifies the interaction tremendously and enables
 the use of standard protocols that support interoperability across platforms. 

However, service-based collaborations do not provide the continuity of using an object-oriented programming model 
for both local and remote objects. This means that you have to track the state of a conversation between objects
 explicitly, something you did not have to worry about when using instance-based collaboration. Also, while 
standards-based protocols improve interoperability, they require the application to convert application-internal 
data types into a common format that is understood by each communicating endpoint, which may involve additional 
transformation logic.

##Near Links vs. Far Links 

Another way to think about distributed systems is to consider each system as a collection of processing nodes
 connected by links. The nodes represent actual server machines while the links represent the network that connects 
these machines together. The links within the system fall within two classifications: near links or far links.

Near links are those that reside in the same trust zone, within the same enterprise, are connected reliably and do 
not require interoperability[互用性]. Far links include all other links and include any link that spans the Internet.

If your distributed system spans near links only, then instance-based collaboration may be optimal. Using 
instance-based collaboration, you can expand the power of object-oriented development across machine boundaries
 while taking advantage of your platform infrastructure to optimize speed, navigate type systems, and handle 
marshalling details for you. Technology choices here would include .NET remoting and Enterprise Services within
 the Microsoft .NET Framework.

On the other hand, if your distributed system spans far links, then service-based collaboration is usually a
 better choice. Interacting with a service that offers up a "coordinator-like" interface allows the service to 
be responsible for implementation and shields the user of the service from implementation details. Service 
interfaces often return messages, which offer less coupling than remote procedure calls. The best messages are
 those that contain both a header and a body, which allow the receiver to act upon the message autonomously. 
Technology choices here would include capabilities such as Web services. 

The balance of this chapter will describe patterns usually associated with instance-based collaboration and
 near links. Patterns usually associated with service-based collaborations and far links are further described 
in Chapter 6, "Services Patterns."

##Distributed Computing Challenges 

The core of a distributed architecture is the ability to invoke a method on an object or communicate with services
 that reside in a different process and possibly on a different computer. Although this does not sound difficult, 
you must address a surprisingly long list of issues:

* How do you instantiate a remote object?

* If you want to invoke a method on an existing object, how do you obtain a reference to this object?

* Network protocols transport only byte streams, not objects. How can you invoke a method over a byte stream?

* What about security? Can anyone invoke a method on the remote object?

* Most networks are inherently unreliable. What happens if the remote object is unreachable? What if the remote object
 receives the method invocation but cannot send the response because of network problems?

* Calling a remote object can be much slower than invoking a local method. Do you want to invoke the remote method
 asynchronously so that you can continue processing locally while the remote object processes the request?

The list of questions continues. Fortunately, the features in the .NET Framework take care of most of these 
issues, allowing developers to create distributed applications without having to deal with many of the nasty
 details. These features make remote invocation almost transparent to the programmer, at least at the syntactic
 level. This simplicity can be deceiving, however, because developers still must understand some of the underlying
 principles of remote communication to write robust and efficient distributed applications. The Distributed Systems
 patterns cluster helps developers make informed design decisions when implementing distributed applications.

##Using Layered Application 

* The secret to creating an easy-to-use infrastructure for distributed systems is Layered Application. 
A distributed services layer relies on lower layers, such as the TCP/IP stacks and socket communication 
layers, but hides the details of these layers from upper layers that contain the application and business
 logic layers. This arrangement allows the application developer to work at a higher level of abstraction 
without having to worry about such details as TCP/IP packets and network byte ordering. It also allows lower 
layers to be replaced without any impact on the upper layers. For example, you can switch to a different 
transport protocol (for example HTTP instead of straight TCP/IP) without changing code at the application layer.

* One way to make remote invocation easy for developers is to use a Proxy [Gamma95]. A proxy is a local stand-in
 object with which the client object communicates. When the client creates an instance of the remote object,
 the infrastructure creates a proxy object that looks to the client exactly like the remote type. When the
 client invokes a method on that proxy object, the proxy invokes the remoting infrastructure. The remoting
 infrastructure routes the request to the server process, invokes the server object, and returns the result
 to the client proxy, which passes the result to the client object. Because all of this happens behind the 
scenes, the client object may be completely unaware that the other object resides on a different computer. 
This not only makes developing distributed applications easier, it also allows you to distribute objects after 
the program has been developed while only minimally changing the application code.


##Patterns Overview 

The Distributed Systems patterns cluster focuses on two primary concepts: remote invocation and coarse-grained interfaces.

[Figure 1: Patterns in the Distributed Systems cluster]

###Remote Invocation 


The Broker pattern describes how to locate a remote object and invoke one of its methods without introducing the
 complexities[复杂性] of communicating over a network into the application. This pattern establishes the basis for most
 distributed architectures, including .NET remoting. 


One of the guiding principles of the .NET Framework is to simplify complex programming tasks without taking control
 away from the programmer. In accordance with this principle, .NET remoting allows the developer to choose from
 a number of remoting models, as described in the following paragraphs.

###Local Copy 


The simplest remoting model involves passing a copy of an object by value to the client. Any subsequent method 
invocations on this object are truly local calls. This model avoids many of the complications inherent in distributed
 computing but has a number of shortcomings. First, computing is not really distributed because you are running a 
local copy of an object in your own process space. Second, any updates you make to the object's state are lost because 
they occur only locally. Finally, an object is usually remote because it requires a remote resource or because the
 provider of the remote object wants to protect access to its internals. Copying the object instance to the local
 process not only defeats both of these goals but also adds the overhead of shipping a complete object over a remote
 channel. Because of these limitations, the only application of object copying that this chapter discusses is the 
Data Transfer Object pattern.


###Server-Activated Objects 


Invoking the methods directly on the remote object is a better model than working on a local copy. However, you can 
invoke a method on a remote object only if you have a reference to it. Obtaining a reference to the remote object 
requires the object to be instantiated first. The client asks the server for an instance of the object, and the server
 returns a reference to a remote instance. This works well if the remote object can be viewed as a service. 
For example, consider a service that verifies credit card numbers. The client object submits a credit card number and 
receives a positive or negative response, depending on the customer's spending (and payment) habits. In this case,
 you are not really concerned with the instance of the remote object. You submit some data, receive a result, 
and move on. This is a good example of a stateless service, a service in which each request leaves the object in the
 same state that it was in before.

Not all remote object collaborations follow this model, though. Sometimes you want to call the remote object to
 retrieve some data that you can then access in subsequent remote calls. You must be sure that you call the same
 object instance during subsequent calls. Furthermore, when you are finished examining the data, you would like 
the object to be deallocated to save memory on the server. With server-activated objects, you do not have this 
level of control over object instances. Server-activated objects offer a choice of only two alternatives for 
lifetime instance management: 

* Create a new instance of the object for each call.
* Use only a single instance of the remote object for all clients (effectively making the object a Singleton).

Neither of these options fits the example where you want to access the same remote instance for a few function
 calls and then let the garbage collector have it. 

###Client-Activated Objects 


Client-activated objects give the client control over the lifetime of the remote objects. The client can instantiate 
a remote object almost as it would instantiate a local object, and the garbage collector removes the remote objects 
after the client removes all references to the object instance. This level of control comes at a price, though.
 To use client activation, you must copy the assembly available to the client process. This contradicts the idea that
 a variety of clients should be able to access the remote objects without further setup requirements.

You can have the best of both worlds, though, by creating a server-activated object that is a factory object for server
 objects. This factory object creates instances of other objects. The factory itself is stateless; therefore, you can 
easily implement it as a server-activated singleton. All client requests then share the same instance of the factory.
 Because the factory object runs remotely, all objects it instantiates are remote objects, but the client can determine
 when and where to instantiate them.


###Coarse-Grained[‎粗粒度‎] Interfaces 


Invoking a method across process and network boundaries is significantly slower than invoking a method on an object 
in the same operating system process. 

Many object oriented design practices typically lead to designing objects with fine-grained interfaces. These objects 
may have many fields with associated getters and setters and many methods, each of which encapsulates a small and 
cohesive piece of functionality. Because of this fine-grained nature, many methods must be called to achieve a
 desired result. This fine-grained interface approach is ideal for stand-alone applications because it supports 
many desirable application characteristics such as maintainability, reusability, and testability.

Working with an object that exposes a fine-grained interface can greatly impede application performance, 
because a fine-grained interface requires many method calls across process and network boundaries. To improve 
performance, remote objects must expose a more coarse-grained interface. A coarse-grained interface is one that
 exposes a relatively small set of self-contained methods. Each method typically represents a high-level piece
 of functionality such as Place Order or Update Customer. These methods are considered self-contained because
 all the data that a method needs is passed in as a parameter to the method. 

##Data Transfer Object 


The Data Transfer Object pattern applies the coarse-grained interface concept to the problem of passing data
 between components that are separated by process and network boundaries. It suggests replacing many parameters 
with one object that holds all the data that a remote method requires. The same technique also works quite well
 for data that the remote method returns. 


There are several options for implementing a data transfer object (DTO). One technique is to define a separate
 class for each different type of DTO that the solution needs. These classes usually have a strongly typed 
public field (or property) for each data element they contain. To transfer these objects across networks or 
process boundaries, these classes are serialized. The serialized object is marshaled across the boundary and
 then reconstituted on the receiving side. Performance and type safety are the key benefits to this approach.
 This approach has the least amount of marshaling overhead, and the strongly typed fields of the DTO ensure 
that type errors are caught at compile time rather than at run time. The downside to this approach is that a
 new class is created for each DTO. If a solution requires a large number of DTOs, the effort associated with
 writing and maintaining these classes can be significant.


A second technique for creating a DTO is to use a generic container class for holding the data. A common implementation 
of this approach is to use something like the ADO.NET DataSet as the generic container class. This approach requires
 two extra translations. The first translation on the sending side converts the application data into a form that 
is suitable for use by the DataSet. The second translation happens on the receiving side when the data is extracted 
from the DataSet for use in the client application. These extra translations can impede performance in some applications.
 Lack of type safety is another disadvantage of this approach. If a customer object is put into a DataSet on the sending 
side, attempting to extract an order object on the receiving side results in a run-time error. The main advantage to this 
approach is that no extra classes must be written, tested, or maintained.

ADO.NET offers a third alternative, the typed DataSet. ADO.NET provides a mechanism that automatically generates a 
type-safe wrapper around a DataSet. This approach has the same potential performance issues as the DataSet approach
 but allows the application to benefit from the advantages of type safety, without requiring the developer to develop,
 test, and maintain a separate class for each DTO. 

##Distributed Systems Patterns 

Table 1 lists the patterns included in the Distributed Systems patterns cluster, along with the problem statements 
and associated implementations that serve as a roadmap to the patterns.

Table 1: Distributed Systems Patterns

|   |   |
|----|---|
| Broker| How can you structure a distributed system so that application developers do not have to deal with the details of remote communication?|
| Data Transfer Object | How do you preserve the simple semantics of a procedure call interface without being subject to the latency issues inherent in remote communication? |
| Singleton | How do you make an instance of an object globally available and guarantee that only one instance of the class is created? |


> Note: The scope for this pattern cluster does not currently include message-oriented middleware, integration of multiple
>  applications, or service-oriented architectures. These topics are extremely important and are part of the overall 
> pattern language, but do not appear in this initial release.