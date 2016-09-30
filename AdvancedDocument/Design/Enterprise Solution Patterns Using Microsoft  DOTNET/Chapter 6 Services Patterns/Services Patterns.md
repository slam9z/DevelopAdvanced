[Services Patterns](https://msdn.microsoft.com/en-us/library/ff648490.aspx)


>Abstractions enable us to assign behavior and data to discrete chunks of software that interact at runtime.
> In well-architected systems, the sum of these interactions forms a coherent executable intelligence, 
>which provides tangible business value to the enterprise. 

The previous chapter introduced patterns for distributing a single application across multiple processing nodes using
 instance-based collaboration and systems separated by near links. Near links, as you may recall, are reliable links that
 connect distributed systems residing in the same trust zone and within the same enterprise; near links do not require 
interoperability. Far links are all other links, including links that span the Internet. This chapter is primarily concerned 
with systems that are connected by far links and that use service-based collaboration.

When building distributed systems characterized by near links and instance based collaboration, the developing organization
 usually has full control over all components involved in the solution. However, many large enterprise applications contain 
systems separated by far links and have to interact with preexisting systems that are usually not under control of the developing 
organization. For example, an order management system may use credit scoring functionality implemented in a pre-existing 
system or a sales tax calculation service provided by an external service provider. As a result, complex solutions are likely 
to have to interact with functions that are controlled by outside organizations and must be used as is. 

This chapter focuses on collaboration between applications and external services. To describe how Web services provide an
 interoperable environment that facilitates such collaboration, the chapter overview addresses the following topics:

* Basic collaboration concepts
* Web services 
* Patterns for service-based collaboration using Web services

##Collaboration Concepts 

Classes, objects, components, and interfaces are the basic building blocks of modern software. Some of these elements 
encapsulate problem domains, while others provide system infrastructure and technical architecture. Each building 
block provides a useful function, but the real power lies in the composition of individual elements into a collaborative 
solution that provides tangible business value to an enterprise (or a web of connected enterprises). To enable this level 
of collaboration, software elements must adhere to agreed-upon organizing principles and must expose standard interfaces
 to each other. Where components are dissimilar, one element must be adapted to the other, or both must be adapted to an
 agreed upon standard. 

###Service-Based Collaboration 


Chapter 5 introduced the notion of instance-based and service-based collaboration, highlighting the strengths and 
weaknesses of each approach. Service-based collaboration works well in scenarios where the consuming application 
does not have any control over the remote services or has to interoperate with solutions developed on top of different
 programming languages or platforms.


Service-based interfaces expose a single instance of an interface that provides a service to potential consumers. 
In the context of Web services, Microsoft defines a software service as a "discrete unit of application logic 
that exposes message-based interfaces suitable for being accessed across a network." [Microsoft02-2]    

A service does not depend on the process that invokes it; it is self-contained and context-independent.
 This allows any potential consumer on the network to access the service. Services are well-defined by means
 of a contract that specifies the format for requests to the service and the format of the associated replies.

Although not necessarily message based, the notion of creating a set of logically grouped services was used in
 application development before the advent of distributed applications. For example, operating systems provide
 services to all applications running on the operating system. The Microsoft Windows GDI library, for instance,
 provides graphical services, and the Open Database Connectivity (ODBC) API exposes database access services. 
And just as abstracting some of the core capabilities of an operating system in to a set of services helped simplify
 application programming models, identifying core business capabilities of an enterprise and encapsulating them as a
 set of interoperable services helps to simplify collaborations with partners outside the corporate firewall.


###Service-Oriented Architecture 


Service-oriented architectures (SOAs) apply the concept of a service to distributed enterprise applications. 
In an SOA, each application exposes high-level business functions as services to be consumed by other applications.
 Because of the expanded scope and complexity of these service-oriented solutions, a service-oriented architecture must
 provide additional functions beyond the capability to invoke a remote service. The most important of these functions include:

* Making services locatable[? ocate 确定…的准确地点] at runtime. It is easy for a stand-alone application to locate an operating
 system service such as a GDI call; it is implemented in a local dynamic-link library named gdi32.dll. 
However, enterprise services can be distributed across many computers, networks, or facilities. Some of 
these services may change locations because they are tied to existing applications. Therefore, locating a 
service in a distributed, service-oriented architecture can be a complex task.

* Making service and consumer agree on a common format. After the correct service is found, the consuming
 application must be able to dynamically determine which protocol to use to access the service, how to format
 a request, and what type of response to expect. Because services can be implemented in a variety of languages
 and platforms, getting the service and the consumer to agree on a common format can also be a challenging task.

###Service Contracts 


When one method calls another inside an application, the method signature defines the "understanding" between the
 method and the caller, for example the number and types of parameters passed into the method and returned on its 
completion. Method calls can embody their understanding in a simple method signature because the caller and the 
method make a number of implicit assumptions; for example, that both methods execute inside the same process and
 share the same memory space; that both methods use the same programming language; and that execution returns to 
the calling method once the called method is complete. In the world of distributed SOAs, many of these assumptions
 are no longer valid and need to be spelled out explicitly in a service contract.

The service contract must specify the implementation of the communication channel connecting the Service Consumers
 with the Service Provider Applications, such as the network protocol. The service contract must also specify wha
t kinds of messages the service can consume or produce, described by means of a detailed schema for each message 
involved in the interaction.

Figure 1 shows the elements of a service-oriented architecture.

[Figure 1: Invocation of a service in an SOA]

A single service may need to support multiple contracts. For instance, service consumers within the same
 organization may want to interact with the service through a series of relatively fine-grained messages and
 may be granted access to sensitive functionality. Service consumers external to the organization may want to
 interact with the service in a more coarse-grained manner for performance reasons, and will not be granted 
access to sensitive functionality.

The following steps are required to invoke a remote service:

1. Discovery: A service consumer (any application wanting to access a service) queries the service repository, 
which provides the location of the desired service.
2. Negotiation[协商]: The service consumer and the service provider agree on a communications format specified by the service contract.
3. Invocation: The service consumer invokes the service.

##Web Services 

Web services provide a standards-based implementation of an SOA. Web services define a suite of technologies and protocols
 that greatly simplify creating solutions based on a set of collaborating applications. Among the many technologies and
 principles associated with Web services, two features are key:

* A communication contract between the service provider and service consumer 
* Interoperability 

###Communication Contract 


A protocol stack is commonly used as a metaphor for communication between systems, the most well-known incarnation
being the Open Systems Interconnect (OSI) layer model. A protocol stack describes communication as a set of layered 
services on both sides of the communication, with "higher" layers using the services of the lower layers. For example,
 an application protocol such as FTP or HTTP can use a TCP/IP transport protocol, which in turn uses an Ethernet card 
to move bits and bytes over the connection. 

A communication contract defines all layers of this protocol stack in detail. As an example, the telephone system 
provides the hardware layer as voice communication in a certain frequency range (400 - 4000 Hz) and MTDF dialing. 
However, it does two participants little good if they use compatible telephony hardware but speak different languages. 
If you have ever received a phone call from a confused international caller or a fax machine, you know that the 
communication can fail even if the communication layer works beautifully.


The Web services contract works similarly. It needs to address two primary aspects of the communication:

* A common communications channel
* Data representation and message schemas

####Common Communications Channel 


In order for the applications to communicate, they must use compatible protocols. TCP/IP has become the default 
core communication protocol stack. Most, if not all, operating systems provide built-in TCP/IP functionality. 
Any application that uses a properly configured TCP/IP stack can communicate with other applications that use 
TCP/IP stacks on the same local network.

Figure 2 shows the communication channel between two applications with compatible protocol stacks.

[Figure 2: Communication channel and TCP/IP protocol stacks]


As an example, Application A may make a request for a Web service exposed by Application B.
 Application A's protocol stack breaks down the application-level request into one or more low-level data 
packets to be streamed across the network. Application B's protocol stack translates the packets back into 
an application-level call to the service. The service reply undergoes an equivalent process.


The ubiquity of the TCP/IP protocol stack makes it the ideal foundation for interoperable communications. 
However, TCP/IP is a low-level protocol and does not define the content of any messages between the applications.
 Like a phone line, it provides a channel for communication but does not specify a common language to be spoken. 

HTTP is a protocol layered on top of TCP/IP that provides the most basic conventions for making a request 
to an external resource. The simplicity of HTTP has helped it gain wide support as the protocol used to 
transport information across the Internet and across corporate firewalls. Thus, HTTP has become both a 
blessing and a curse. Its universal use makes it ideal for routing messages, but its capability to permeate 
most corporate firewalls concerns many IT security administrators.

With the addition of HTTP, the resulting protocol stack now appears as shown in Figure 3.

[Figure 3: Communication protocol stacks with addition of HTTP]

###Data Representation and Message Schemas 

The second part of the communication contract deals with what is being passed across the communications channel,
 comparable to the language spoken across a telephone line. This part of the contract needs to define three things: 

* The data representation format
* The message schema
* The binding of messages to services

###Data Representation Format


If applications are to successfully communicate with each other, they must all agree to a common set of data 
definitions for data passed over the connection. The Web services protocol stack already constrains the 
data passed back and forth to be in a textual format. But what does that textual data represent? Does it 
represent a serialized object? An array of integers? Or an XML document?

There are basically two ways to provide information about what the data represents: provide an external
 description or use self-describing data.

An external description defines the schema of the data in some form external to the data itself. 
The interface definition languages (IDL) used by Distributed RPC, DCOM, or CORBA technologies are examples 
of languages designed to write external descriptions. This external approach to data description limits
 interoperability because all computers involved in the collaboration must have access to the external description.

Self-describing data includes an embedded description of the data with the data itself. Using this approach
 enhances interoperability because the data can be parsed without having to consult an external description of the data.


Web services use XML as the data representation format. XML provides the following advantages:

* It is text based and therefore compatible with the communication channel
* It is an industry standard with broad industry and user support
* It is self-describing
* It is interoperable

XML parsers exist for virtually all platforms, and many development tools are available to ease the development
 of XML applications

###Message Schema

The message is the fundamental unit of communication in Web services and therefore it is imperative that 
all collaborating parties have a precise understanding of the message contents.

The communication contract must specify all the request messages associated with a service and any associated
 response messages. Then the contents for each message must be specified. This task typically involves 
identifying the data elements of each message, specifying the data type of the elements, and specifying 
any constraints associated with the types or between types.

SOAP divides a message into two sections: an optional header and a mandatory body. The header contains
 information associated with the communication and services infrastructure. The body contains the business-oriented
 content of the message while the header contains metadata.

###Message Binding


After the messages have been defined, they must be associated with a communication channel. In addition to HTTP,
 SOAP can work with other text-based communication protocols such as SMTP. As a result, a service may support more
 than one communication protocol.

Web services use Web Services Description Language (WSDL) to provide a detailed specification of all the messages
 supported by a service. In turn, WSDL uses the XML Schema Definition (XSD) standard to document the internal 
structure of each message and any constraints on any of the message elements.

WSDL groups messages into operations. An operation is the logical unit of interaction with a service, which is defined
 as a request message and any associated response messages.

Finally, WSDL binds operations to one or more protocols, such as HTTP and SMTP, and then groups these bound operation
 together in a service. In addition to specifying the operations associated with a service, the WSDL service 
specification also documents the communication channel-specific address of the service. For instance, the service
 specification would document the URL to identify a service that is exposed through SOAP over HTTP.

###Interoperability[互用性] 


Interoperability was a major factor in the previous discussion of communication channels and message 
descriptions. Several other features of Web services significantly aid interoperability.

###Open Standards 

One of the key disadvantages of using a traditional approach to distributed communication is that applications
 are dependent on proprietary communication technology, protocols, and data formats.

Web services are entirely based on a set of widely-supported, platform-independent, open standards. As a result, 
virtually every major platform has one or more implementations of the Web services protocol stack. This
 significantly reduces the effort and cost associated with implementing and deploying solutions based on 
collaborating applications.

###Service Repository 


The final piece of the Web services puzzle is service discovery. How does a service consumer application find
 the services it needs to collaborate with? The answer is to provide a federated service repository that contains
 descriptions of the services and associate these descriptions with various metadata elements that are useful 
for identifying particular services. For instance, the service repository should return pointers to services 
based on several different criteria such as developing organization, hosting organization, industry type, and
 business process supported. 

Using a service repository significantly reduces coupling between the service provider and service consumer.
 The reduction results from the consumer only needing a reference to the service, rather than hard-coding all
 the details needed to access the service within the service consumer. This allows the service provider to c
hange many pieces of the communications contract without requiring any changes to the service consumer. T
he provider only needs to update the registry. Applications that make full use of the UDDI specification 
will automatically use the new settings the next time they access the service.

###UDDI 

The Universal Discovery, Description, and Integration (UDDI) specification solves the service discovery problem
 for Web services. Interoperability was one of the primary goals of UDDI, so it is not surprising that UDDI uses
 many of the technologies and protocols already discussed in this chapter.

At its core, UDDI is simply a repository containing links to WSDL service descriptions. UDDI defines several XML
 descriptions of various metadata that may be associated with a service. These descriptions include, information
 about the organization providing the service, the business process supported by the service, and the service 
type. Finally, UDDI exposes its functionality as a set of SOAP services. 

##Patterns Overview 

The patterns in this chapter describe how to structure a custom-developed solution in a service-oriented 
environment. Specifically, these patterns enable you to:

* Expose application functionality as a service
* Encapsulate[简述] the details of consuming services that are exposed by other applications

Figure 4 shows the relationship between a service gateway, a service interface, and the implementation of the service.

[Figure 4: Service elements]

As you design service oriented systems, it is helpful to separate the elements that are responsible for 
application business logic from those elements responsible for communicating with services and participating
 in service contracts. Separating these elements furthers the general design objective of separation of concerns,
 and improves maintainability, flexibility, and testability. 

The Service Interface pattern provides guidance on structuring the service provider portion of the contract.
 It discusses using a service interface component that encapsulates the details of communicating with a particular 
set of service consumers and invokes a service implementation component that performs the actual business logic
 associated with the service Implementing Service Interface in .NET then provides a concrete example of creating
 a service interface component using the .NET framework.

The Service Gatewaydesign pattern provides guidance for implementing the service consumer portion of the contract. 
It discusses using a service agent component that encapsulates all the low-level details of communicating with 
the service and exposes an interface that is optimized for the use of the other components within the service 
consumer application. Implementing Service Gateway in .NET then provides a concrete example of creating a Service 
Gateway component using the .NET Framework.

##Services Patterns 

Table 1 lists the patterns in the Services patterns cluster, along with the problem statements and associated 
implementations that serve as a roadmap to the patterns. 

Table 1: Service Patterns

| | | |
|--|---|--|
| Service Interface | How do you make pieces of your application's functionality available to other applications, while ensuring that the interface mechanics are decoupled from the application logic? |Implementing Service Interface in .NET|
| Service Gateway | How do you decouple the details of fulfilling the contract responsibilities defined by the service from the rest of your application? | Implementing Service Gateway in .NET |