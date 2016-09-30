[Broker](https://msdn.microsoft.com/en-us/library/ff648096.aspx)


##Context 

Many complex software systems run on multiple processors or distributed computers. There are a number of reasons to 
distribute software across computers, for example:

* A distributed system can take advantage of the computing power of multiple CPUs or a cluster of low-cost computers.

* Certain software may only be available on specific computers.

* Parts of the software may have to run on different network segments due to security considerations.

* Some services may be provided by business partners and may only be accessed over the Internet.

However, implementing a distributed system is not easy because you have to deal with issues such as concurrency,
 cross-platform connectivity, and unreliable network connections.

##Problem 

How can you structure a distributed system so that application developers don't have to concern 
themselves with the details of remote communication? 

##Forces 

The following forces must be reconciled as you build a distributed system:

* Although distributed systems provide a lot of advantages, they also tend to introduce significant complexity
 into the software system. Physical and logic boundaries exist between processes or computers running on the 
same network. To have objects running on different processes or computers communicating with each other across 
these boundaries, you have to deal with issues such as communications, encoding, and security. If you mix these 
implementation details with the application code, a simple change in the communications infrastructure could
 lead to significant code changes.

* The distribution of the system often occurs after development is complete. For example, software may be 
distributed across multiple servers to increase processing power. You would not want to change the application
 code at this late a stage in the life cycle.

* The details of cross-process communication can be quite tedious[冗长的]. You have to deal with TCP/IP sockets, 
marshaling and unmarshaling, serialization, timeouts, and many other challenges. Therefore, it makes sense 
to have a special team focus on the infrastructure so that the application developers do not have to learn 
about remote communications.

* To maintain the flexibility of being able to move components to different locations at deployment time, 
you must avoid hard-coding the location of specific components.

##Solution 

Use the Broker pattern to hide the implementation details of remote service invocation by encapsulating them into a 
layer other than the business component itself [Buschmann96].

This layer provides an interface to the client that allows it to invoke methods just as it would invoke any local
 interface. However, the methods inside the client interface trigger services to be performed on remote objects. This 
is transparent to the client because the remote service object implements the same interface. This pattern refers to the
 business component that initiates the remote service invocation as the client, and the component that responds to the
 remote service invocation as the server. 

Figure 1 shows the static structure of a simple example without any distribution. The client invokes the performFunctionA 
method on the server directly. This is possible only if the server objects reside on the same computer as the client objects.

[Figure 1: Structure with no distribution]

Figure 2 shows the static structure when distribution is implemented.

[Figure 2: Structure with distribution]

The ServiceInterface is a necessary abstraction that makes distribution possible by providing the contract about the 
service that the server is going to provide without exposing the implementation details on the server side. When 
implementing the distribution, client and server proxies would be added to handle all the "plumbing"[自来水管道] for sending 
a method invocation and its parameters across the network to the server and then sending the response back to the 
client. The proxies would do all the marshaling and unmarshaling of data, security control, transfer channel 
configuration, and any other additional work. The client would simply invoke the performFunctionA method on the 
client proxy as if it were a local call because the client proxy actually implements the ServerInterface. The code 
change to the client would be minimal and thus you could develop your whole business domain model without any knowledge
 about the distribution nature of the system. Any change to the way remote service invocation is implemented would be
 limited to within the proxy classes, and would not have any impact on the domain model. Figure 3 shows one scenario of
 the interactions between these components.

[Figure 3: Behavior with distribution]


###Server Look-Up 


The Broker solution addresses most of the problems described previously. However, because the client proxy 
communicates with the server proxy directly, the client must be able to find the location of the server at 
compile time. This means that you cannot change or move the server to a different location at run time. To 
overcome this limitation, you need to avoid exposing the exact location of the server. Instead, you deploy 
a new component, the broker component, at a well-known location and then expose that location to the client. 
The broker component is then responsible for locating the server for the client. The broker component also
 implements a repository for adding and removing server components, which makes it possible to add, remove,
 or exchange server components at run time. Figure 4 shows the static structure with the broker component involved.


This type of function is often called a naming service. Looking up remote objects is a common requirement in 
enterprise computing. Therefore, a number of platforms implement a naming service, for example, Microsoft 
uses the Active Directory directory service.


[Figure 4: Broker structure with server look-up]

The broker is hosted at a well-known location that should not change very often. Any server that is activated
 and ready to receive requests would register itself with the broker so that the next time a client asks the broker
 for this type of server, the broker would be able to use it. This could also increase the performance and availability 
of the system, because it enables you to have multiple identical server components that run and serve multiple 
clients at the same time. This mechanism is sometimes called load balancing. Figure 5 shows a sample interaction 
scenario between these components.

[Figure 5: Broker behavior with server look-up]

###Broker as Intermediary[中间人] 


In the previous scenario, the broker is only responsible for locating the server for the client. The client 
obtains the location of the server from the broker and then communicates with the server directly without 
any involvement of the broker. In some situations, however, direct communication between client and server 
is not desirable. For example, for security reasons, you may want to host all the servers in your company's 
private network, which is behind a firewall, and only allow access to them from the broker. In this case, 
you must have the broker forward all the requests and responses between the server and the client instead 
of having them talk to each other directly. Figure 6 shows a revised static structure of this model.

[Figure 6: Structure of Broker serving as intermediary]

Figure 7 shows the interaction diagram with the broker serving as a messenger between the client and the server.
 This example also shows how the communication between the client and the server can be asynchronous (note the open 
arrowhead on the sendRequest call).

There are also situations when the client must make a series of method invocations on the same server to complete
 one long and complex business transaction. In these cases, the server must maintain the state between client calls. 
The broker must then make sure that all server calls that a client makes inside an atomic session are routed to 
the exact same server component.

[Figure 7: Behavior of Broker serving as intermediary]


##Example 
The Broker pattern and its variants are implemented in many distributed system frameworks.
 See Implementing Broker in .NET Remoting Using Server-Activated Objects and 
Implementing Broker in .NET Remoting Using Client-Activated Objects.

##Resulting Context 

The Broker pattern has many of the benefits and liabilities of the Layered Application pattern.

###Benefits 


Broker provides the following benefits:

* Isolation. Separating all the communication-related code into its own layer isolates it from the application.
 You can decide to run the application distributed or all on one computer without having to change any application code.

* Simplicity. Encapsulating complex communication logic into a separate layer breaks down the problem space.
 The engineers coding the broker do not have to concern themselves with arcane user requirements and business 
logic, and the application developers do not have to concern themselves with multicast protocols and TCP/IP routing.

* Flexibility[灵活性]. Encapsulating functions in a layer allows you to swap this layer with a different
 implementation. For example, you can switch from DCOM to .NET remoting to standard Web services without
 affecting the application code.

###Liabilities 


Unfortunately, layers of abstraction can harm performance. The basic rule is that the more information you have, 
the better you can optimize. Using a separate broker layer may hide details about how the application uses the 
lower layer, which may in turn prevent the lower layer from performing specific optimizations. For example, when
 you use TCP/IP, the routing protocol has no idea what is being routed. Therefore, it is hard to decide that a
 packet containing a video stream, for instance, should have routing priority over a packet containing a junk e-mail. 

##Security Considerations 

Server components that contain sensitive business data are often located in a company's private network,
 protected behind a firewall. The broker component then sits in a perimeter network (also known as demilitarized 
zone, DMZ, or screened subnet), which is a small network inserted as a neutral zone between a company's private
 network and the outside public network. Access to the server components is only allowed from the perimeter [边界]
network and not from the outside public network. This extra layer prevents outside users from getting direct access to a server.

##Related Patterns 

For more information about Broker, see the following related patterns:

Implementing Broker with .NET Remoting Using Server-Activated Objects and Implementing Broker with .NET Remoting
 Using Client-Activated Objects describe two implementation strategies for the Broker pattern.

Data Transfer Object.

Remote Proxy. The ClientProxy object described in this pattern follows a variant of the Proxy pattern 
described in [Gamma95, Buschmann96], which is called Remote Proxy.

##Acknowledgments 

[Buschmann96] Buschmann, Frank, et al. Pattern-Oriented Software Architecture. John Wiley & Sons Ltd, 1996.
[Fowler03] Fowler, Martin. Patterns of Enterprise Application Architecture. Addison-Wesley, 2003.
[Gamma95] Gamma, Helm, Johnson, and Vlissides. Design Patterns: Elements of Reusable Object-Oriented Software. Addison-Wesley, 1995.