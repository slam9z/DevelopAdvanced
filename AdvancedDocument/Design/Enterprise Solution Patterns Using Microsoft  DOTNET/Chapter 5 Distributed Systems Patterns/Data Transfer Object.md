[Data Transfer Object](https://msdn.microsoft.com/en-us/library/ff649585.aspx)

##Context 

You are designing a distributed application, and to satisfy[满足] a single client request, you find yourself making 
multiple calls to a remote interface, which increases the response time beyond acceptable levels. 

##Problem 

How do you preserve the simple semantics of a procedure call interface without being subject to the latency[潜伏]
 issues inherent in remote communication?

##Forces 

When communicating with a remote object, consider the following tradeoffs:

* Remote calls (those that have to cross the network) are slow. Although many remote invocation frameworks 
can hide the complexities of making a remote call, they cannot eliminate the steps that are required for 
the communication to take place. For example, the remote object location has to be looked up, and a connection
 to the remote computer has to be made before the data can be serialized into a byte stream, possibly encrypted,
 and then transmitted to the remote computer. 

* When considering the performance of networks, you have to look at both latency and throughput[接待人数]. In simplified 
terms, latency describes the time that passes before the first byte of data reaches the destination. Throughput
 describes how many bytes of data are sent across the network within a certain time period (for example, 1 second).
 In modern IP routing-based networks (for example, the Internet), latency can be a bigger factor than throughput.
 That means it may take almost the same amount of time to transmit 10 bytes of data as it takes to transmit 1,000
 bytes of data. This effect is particularly pronounced when using connectionless protocols such as HTTP. Faster 
networks can often increase the throughput, but latency is much more difficult to reduce.

* When designing an object interface, good practices are to hide much of the information inside an object and to
 provide a set of fine-grained methods for accessing and manipulating that information. Fine-grained means that
 each method should be responsible for a single, fairly small, and atomic piece of functionality. This approach 
simplifies programming and provides better abstraction from the object internals, thereby increasing potential 
for reuse. This must be balanced against the fact that using finer-grained methods implies invoking more methods
 to perform a high-level task. Typically, the overhead of these extra function calls is acceptable when the methods
 are invoked within the same process; however, the overhead can become severe when these methods are invoked across
 process and network boundaries. 

* The best way to avoid latency issues that are inherent in remote calls is to make fewer calls and to pass more data
 with each call. One way to accomplish this is to declare the remote method with a long list of parameters. This
 allows the client to pass more information to the remote component in a single call. Doing so makes programming 
against this interface error-prone, however, because arguments are likely to call parameters of the external method
 solely by position in the call statement. For example, if a remote method accepts 10 string parameters, it is easy
 for the developer to pass arguments in the wrong order. The compiler will not be able to detect such a mistake. 

* A long parameter list does not help return more information from the remote call to the client because most programming
 languages limit the return type of a method call to a single parameter. Coincidentally, the return is often when the most
 data is transmitted. For example, many user interfaces transmit a small amount of information but expect a large result 
set in return.

##Solution 

Create a data transfer object (DTO) that holds all data that is required for the remote call. Modify the remote
 method signature to accept the DTO as the single parameter and to return a single DTO parameter to the client.
 After the calling application receives the DTO and stores it as a local object, the application can make a series
 of individual procedure calls to the DTO without incurring the overhead of remote calls. Martin Fowler describes 
this pattern in Patterns of Enterprise Application Architecture [Fowler03].

The following figure shows how a client application makes a sequence of remote calls to retrieve the various elements
 of a customer name. 

[Figure 1: Remote calls without a DTO]

A DTO allows the remote object to return the whole customer name to the client in a single remote call. In this
 example, doing so would reduce the number of calls from four to one. Instead of making multiple remote calls,
 the client makes a single call and then interacts with the DTO locally (see Figure 2).

[Figure 2: Reducing the number of calls by using a DTO]

A DTO is a simple container for a set of aggregated data that needs to be transferred across a process or n
etwork boundary. It should contain no business logic and limit its behavior to activities such as internal 
consistency checking and basic validation. Be careful not to make the DTO depend on any new classes as a result
 of implementing these methods. 


When designing a data transfer object, you have two primary choices: use a generic collection or create a custom 
object with explicit getter and setter methods.


*A generic collection* has the advantage that you only need a single class to fit any data transfer purpose
 throughout the whole application. Furthermore, collection classes (for example, simple arrays or hashmaps) 
are built into almost all language libraries, so you do not have to code new classes at all. The main drawback
 of using collection objects for DTOs is that the client has to access fields inside the collection either by
 position index (in the case of a simple array) or by element name (in the case of a keyed collection). Also, 
collections store items of the same type (usually the most generic Object type), which can lead to subtle but
 fatal coding errors that cannot be detected at compile time.

*Creating custom classes* for each DTO provides strongly-typed objects that the client application can access exactl
y like any other object, so they provide compile-time checking and support code editor features such as Microsoft
 IntelliSense technology. The main drawback is that you could end up having to code a large number of these classes
 if your application makes a lot of remote calls.


A number of options try to combine the benefits of the two approaches. The first is code generation that generates
 the source code for custom DTO classes off existing metadata, such as an Extensible Markup Language (XML) schema.
 The second approach is to provide a more powerful collection that is generic but stores relationship and data
 type information along with the raw data. The Microsoft ADO.NET DataSet supports both approaches (see Implementing
 Data Transfer Object in .NET with a DataSet).

Now that you have a DTO class, you need to populate it with data. In most instances, data inside a DTO is derived 
from more than one domain object. Because the DTO has no behavior, it cannot extract the data from the domain objects. 
This is fine, because keeping the DTO unaware of the domain objects enables you to reuse the DTO in different contexts
. Likewise, you do not want the domain objects to know about the DTO because that may mean that changing the DTO would
 require changing code in the domain logic, which would lead to a maintenance nightmare. 

The best solution is to use the Assembler pattern [Fowler03], which creates DTOs from business objects and vice versa.
 Assembler is a specialized instance of the Mapper pattern also mentioned in Patterns of Enterprise Application
 Architecture [Fowler03].

[Figure 3: Using an Assembler to load data into the DTO]

The key characteristic of Assembler is that the DTO and the domain object do not depend upon each other. This decouples 
the two objects. The downside is that Assembler depends on both the DTO and the domain object. Any change to these 
classes may result in having to change the Assembler class.

##Example 

See Implementing Data Transfer Object in .NET with a DataSet.

##Testing Considerations 

DTOs are simple objects that should not contain any business logic that would require testing. You do, however,
 need to test data aggregation for each DTO. Depending on your serialization mechanism, testing may or may not be 
required for each DTO. If serialization is part of the framework, you need to test only one DTO. If not, use a generic 
reflection mechanism so that you do not need to test the serialization of each DTO. 

DTOs also contribute to the testability of remote functions. Having the results of a remote method available in an
 object instance makes it easy to pass this data to a test module or to compare it with the desired outcome.

##Security Considerations 

Ideally, data obtained from untrusted sources, such as user input from a Web page, should be cleansed and validated 
before being placed into a DTO. Doing so enables you to consider the data in the DTO relatively safe, which simplifies
 future interactions with the DTO. 

The security credentials of the processes and associated users receiving the DTO are also important to consider.
 DTOs often contain a large amount of information that is assembled from many different sources. Are all users 
of the DTO authorized to access all the information contained within it? The best way to ensure that users are
 authorized is to populate the DTO with only the specific data that is authorized by the users' security credentials. 
Try to avoid making the DTO responsible for its own security. This increases the number of dependences the DTO 
has on other classes, which means these classes must be deployed to all nodes on which the DTO is used. It also 
spreads the security functionality across more classes, which increases security risk and negatively affects 
flexibility and maintainability.

##Resulting Context 

Data Transfer Object results in the following benefits and liabilities:

###Benefits 

* Reduced number of remote calls. By transmitting more data in a single remote call, the application can reduce the
 number of remote calls.

* Improved performance. Remote calls can slow an application drastically. Reducing the number of calls is one of 
the best ways to improve performance. In most scenarios, a remote call carrying a larger amount of data takes 
virtually the same time as a call that carries only a small amount of data.

* Hidden internals. Passing more data back and forth in a single call also more effectively hides the internals
 of a remote application behind a coarse-grained interface. This is the main motivation behind the Remote 
Facade pattern [Fowler03].

* Discovery of business objects. In some cases, defining a DTO can help in the discovery of meaningful business
 objects. When creating custom classes to serve as DTOs, you often notice groupings of elements that are presented
 to a user or another system as a cohesive set of information. Often these groupings serve as useful prototypes
 for objects that describe the business domain that the application deals with. 

* Testability. Encapsulating all parameters in a serializable object can improve testability. For example, you 
could read DTOs from an XML file and call remote functions to test them. Likewise, it would be easy to serialize
 the results back into XML format and compare the XML document to the desired outcome without having to create 
lengthy comparison scripts.

###Liabilities 

* Possible class explosion. If you chose to use strongly-typed DTOs, you may have to create one (or two, 
if you consider the return value) DTO for each remote method. Even in a coarse-grained interface, this could
 lead to a large number of classes. It can be hard to code and manage this number of classes. Using automatic
 code generation can alleviate some of this problem.

* Additional computation. The act of translating from one data format on the server to a byte stream that 
can be transported across the network and back into an object format inside the client application can introduc
e a fair amount of overhead. Typically, you aggregate the data from multiple sources into the single DTO on
 the server. To improve efficiency of remote calls across the network, you have to perform additional computation
 on either end to aggregate and serialize information. 

* Additional coding effort. Passing parameters to a method can be done in a single line. Using a DTO requires 
instantiating a new object and calling setters and getters for each parameter. This code can be tedious to write.

##Related Patterns 

For more information, see the following related patterns:

Remote Facade. The Data Transfer Object pattern is typically used in conjunction with a coarse-grained Remote Facade 
to reduce the number of remote calls.

Mapper [Fowler03]. A Mapper is the recommended technique to load the DTO with data elements from the domain objects.

Value Object. Some books refer to Data Transfer Object as Value Object. This usage is no longer considered correct. 
For more information, see Patterns of Enterprise Application Architecture [Fowler03].

##Acknowledgments 

[Fowler03] Fowler, Martin. Patterns of Enterprise Application Architecture. Addison-Wesley, 2003.