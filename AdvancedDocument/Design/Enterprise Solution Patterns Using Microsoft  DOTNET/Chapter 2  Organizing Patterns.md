[Chapter 2: Organizing Patterns](https://msdn.microsoft.com/en-us/library/ff647589.aspx)

>"Each pattern then depends both on the smaller patterns it contains, and on the larger patterns within
> which it is contained." - Christopher Alexander in The Timeless Way of Building


An innovation[创新] in one area of technology often fuels a breakthrough in another area. Radar[雷达] technology 
turned into a cooking device: the microwave oven. The Internet itself was originally designed as a military
 communications network with resilience against single points of attack and has since turned into the world's
 largest repository of knowledge. Similarly, patterns, originally applied to building and town architecture,
 were quickly embraced by the software development community as a means to describe complex software systems. 


Today there are dozens[一打] of patterns related to software with more emerging[出现] daily. This abundance of patterns creates
 a new set of challenges. How can a developer identify those patterns that are most relevant to the task at hand?
 Is the collection of patterns sufficient to describe complete solutions?


This chapter answers some of these questions by demonstrating how to:

* Identify relationships between patterns.
* Group patterns into clusters[簇].
* Identify patterns at various levels of abstraction. 
* Apply patterns to multiple aspects of a solution.
* Organize patterns into a frame.
* Use patterns to describe solutions concisely.

##Pattern of Patterns 

One reason the object-oriented programming community embraced patterns so emphatically is because patterns describe 
relationships. The base element of object-oriented programming is a class. However, a single class is not very
 meaningful apart from its relationship to other classes that make up the solution. Each pattern typically describes
 a cluster of classes, highlighting the relationships and interactions between them. Thus, patterns turn the sea of 
classes into a much more manageable collection of patterns.

Now that the number of available patterns easily exceeds the number of classes in an average application, 
you may suddenly find yourself in a sea of patterns. How can you make sense out of all these patterns? Again, 
the relationships between items appear to be the key. It is easy to see that some patterns are closely related to 
other patterns. For example, some patterns are refinements of others. Three-Tiered Distribution is a specific application
 of the concept of Tiered Distribution. Observer is frequently used to implement a part of the Model-View-Controller 
pattern. Page Controller describes the controller portion of Model-View-Controller in more detail. Implementing Page 
Controller in ASP.NET is an implementation of the Page Controller pattern using Microsoft ASP.NET.

To begin organizing patterns according to relationship, visualize a set of patterns as small circles (see Figure 1):

[Figure 1: A set of patterns]


If you draw a line between each pair of patterns that share some relationship, you get a picture like this: 

[Figure 2: Pattern relationships represented as lines]


The somewhat random collection of circles becomes a connected web of patterns. When you look at a pattern,
you can now identify closely related patterns and review those as well. You can also identify "neighborhoods" of
closely related patterns and see how they are related to other, more remote patterns.

##Pattern Clusters
 
Charting the relationships between patterns helps you navigate from one pattern to a set of related patterns.
 However, it does not yet tell you where to start. If you are building a Web application, should you read the
 Model-View-Controller pattern first or should you look at Page Cache instead? Should you look at a Broker as well?

Pattern clusters are groupings of patterns that relate to a specific subject area. For example, you can start with
 the Web Presentationcluster to find the patterns that are relevant to creating the front end of a Web application.
 Likewise, the Distributed Systems cluster contains patterns that are helpful in communicating with remote objects.
 Dividing the collection of patterns into clusters enables you to examine a group of patterns together. Although 
the pattern graph shows that two patterns are related, the cluster overview describes, in much more detail, how to
 combine the patterns to build actual solutions. Each cluster takes the reader on a guided tour through all the
 patterns within the cluster. Taking some inspiration from Christopher Alexander's world of town and building 
architecture, you can draw an analogy between a cluster and a city neighborhood. To stretch this analogy a little 
bit further, you can consider the cluster overview a neighborhood tour offered by the local tourism office.

[Figure 3: Pattern clusters]

This initial release of Enterprise Solution Patterns Using Microsoft .NET (ESP) identifies the five clusters shown in Table 1.

*Table 1: Enterprise Solution Patterns Clusters*

|---|---|
| Web Presentation | How do you create dynamic Web applications? |
| Deployment | How do you divide an application into layers and then deploy them onto a multi-tiered hardware infrastructure?|
| Distributed Systems | How do you communicate with objects that reside in different processes or different computers?|
| Performance and Reliability | How do you create a systems infrastructure that can meet critical operational requirements?|
| Services | How do you access services provided by other applications? How do you expose your application functionality as services to other applications? |


Chapters 3 through 7 describe these clusters in detail. 


##Different Levels of Abstraction 


Dividing patterns into clusters makes them more manageable. If you are building the front end of a Web application,
 start with the Web Presentation cluster, take the quick tour, and see what other patterns are related to this
 cluster. Keep in mind, though, that different people may be interested in different aspects of building a Web 
application, depending on the role they are playing or the stage of the project. A developer may be most interested
 in the most efficient implementation of the Page Controller pattern on the Microsoft .NET Framework, while an 
architect may be more interested in deciding whether to use a three-tiered or a four-tiered application architecture.

Level of abstraction, therefore, is a useful way to categorize patterns so that different user groups can find
 the patterns that correspond most closely to their area of interest. Dividing the patterns from general to
 more specific detail also helps you decide which patterns to consider first. You may want to think about how
 many tiers your application should have before you consider the intricacies of ASP.NET caching directives
 described in the Implementing Page Cache with ASP.NET pattern.

One way categorize the patterns is to divide the pattern graph into the three levels shown in Figure 4.

[Figure 4: Levels of abstraction]

This division largely coincides with the terminology used in some of the most influential books about software patterns.


###Architecture Patterns 


>"An architectural pattern expresses a fundamental structural organization schema for software systems. 
>It provides a set of predefined subsystems, specifies their responsibilities, and includes rules and
> guidelines for organizing the relationships between them." [Buschmann96]


ESP follows the Buschmann, et al. definition of architecture patterns. These patterns describe how to structure an
 application at the highest level. For example, the Layered Application pattern is an architecture pattern.


###Design Patterns 


>"A design pattern provides a scheme for refining[精炼] the subsystems or components of a software system, 
>or the relationships between them. It describes a commonly recurring structure of communicating components 
>that solves a general design problem within a particular context." [Gamma95]


Design patterns provide the next level of refinement, as described in the seminal work by Gamma, et al. Many of
 the iconic patterns, such as Model-View-Controller or Singleton, are in this layer.


###Implementation Patterns 


The patterns community refers to more detailed, programming-language-specific patterns as idioms[风格]. 
This definition works well for software patterns. However, the scope of this guide is not just software,
 but software-intensive systems, including the deployment of the software onto hardware processing 
nodes to provide a holistic business solution. Therefore, ESP modifies the definition of an idiom given in
 Pattern-Oriented Software Architecture (POSA) [Buschmann96] to reflect the broader scope and relabels these 
patterns as implementation patterns:


An implementation pattern is a low-level pattern specific to a particular platform. An implementation 
pattern describes how to implement particular aspects of components or the relationships between them,
 using the features of a given platform.


The ESP implementation patterns demonstrate how to implement design concepts using the.NET Framework.
 In some cases, the framework already incorporates the bulk of the work, making the developer's task easier.

>Note: Even though POSA [Buschmann96] defines idioms as patterns and The Timeless Way of Building
> [Alexander79] includes implementation patterns in his original pattern work, there is a debate 
>among some members of the pattern community as to whether implementation patterns are true patterns.
> Regardless of how they can be classified, they are very helpful when thinking about patterns, and 
>are therefore included in this guide. 

Dividing the collection of patterns into three levels of abstraction makes it easier for different user
 groups to identify patterns that relate to their fields of interest and expertise. The resulting model
 flows from high-level organization, through progressive refinement of subsystems and components, down 
to the implementation of these patterns using platform-specific technology.

##Viewpoints 

Although the levels of abstractions help to address different user groups, they do not reflect the fact that a
 software solution encompasses much more than code components. A holistic view of building an enterprise solution
 includes custom-developed software, platform software, hardware infrastructure, and the deployment of software onto
 hardware. Because of the stark differences between these areas, it makes sense to align the patterns with this nomenclature.

 
Keep in mind that these four areas describe different viewpoints of the same solution. Therefore, unlike the 
levels of refinement, these viewpoints do not describe a hierarchy, but simply provide four different ways of 
looking at the same thing. You can compare these viewpoints to different types of maps. One map of a region may
 depict traffic networks such as roads and freeways, while another map of the same area shows the topography. 
Still another map may show state and county borders. Each map has its own vocabulary. For example, lines in the
 topographical map represent elevations, while lines in the traffic map represent streets. Nevertheless, all maps
 describe the same subject: a specific geographic region.

Each viewpoint itself can also focus on different levels of abstraction. Therefore, ESP depicts the following 
viewpoints as vertical slices across the pattern graph: database, application, and infrastructure. There is often 
a significant gap between the application and infrastructure viewpoints. Concepts, abstractions, and skill sets are 
sufficiently different to warrant the insertion of a buffer between the two that helps to bridge the divide. This
 viewpoint is called the deployment viewpoint.

This line of reasoning results in the four viewpoints shown in Table 2.

*Table 2: Enterprise Solution Patterns Viewpoints*

|---|----|
| Database | The database view describes the persistent layer of the application. This view looks at such things as logical and physical schemas, database tables, relationships, and transactions.|
| Application | The application view focuses on the executable aspect of the solution. It includes such things as domain models, class diagrams, assemblies, and processes. |
| Deployment| The deployment view explicitly maps application concerns to infrastructure concerns (for example, processes to processors). |
| Infrastructure | The infrastructure view incorporates all of the hardware and networking equipment that is required to run the solution. |


Figure 5 overlays these viewpoints as vertical lines over the pattern graph and the levels of abstraction.

[Figure 5: Adding viewpoints]


For the sake of simplicity, Figure 5 does not show the cluster boundaries. However, the clusters, the layers of
 abstraction, and the viewpoints exist in parallel. They represent different ways to access the same set of patterns.

##The Pattern Frame 

The combination of three levels of refinement on the vertical axis and the four viewpoints on the horizontal axis
 results in a grid-like organization of the pattern graph. This arrangement, called the Pattern Frame, is shown in Figure 6.

[Figure 6: The Pattern Frame]

The Pattern Frame is included with each individual pattern description as a point of reference and as a navigational aid.


##Constraints 


The Pattern Frame organizes the collection of patterns into meaningful subcategories. For example, you can now focus
 on the design patterns of the Database view or on the implementation patterns of the Application view. 

However, software takes many forms. Today, software operates embedded systems such as pacemakers and 
telecommunications equipment, real-time systems such as antilock brakes, or in data warehousing systems
 constructed to analyze consumer buying behavior. Trying to address patterns related to all these flavors of 
software solutions would quickly enlarge the scope of any single book or pattern repository. ESP, therefore, 
constrains the patterns to enterprise business solutions. Because this term is somewhat nebulous, ESP identifies 
a small set of specific top-level architectural patterns, or root patterns, within the pattern graph. All other 
patterns in this collection adhere[附着] to the following constraints:

* Online[在线地] transaction processing (OLTP)
* Object-oriented
* Layered application
* Tiered distribution systems

OLTP systems are database subsystems that manage the processing of transactions. These subsystems ensure
 that each transaction is *atomic, consistent, isolated, and durable* (the so-called ACID properties).
 In practice, these applications often manipulate one or more relational databases that maintain the 
business state of the enterprise. In other words, these are the databases that keep track of the customers,
 orders, accounting, and so on. By identifying OLTP as a top-level constraint in the Pattern Frame, 
ESP excludes online analytical processing (OLAP) or simple flat file systems that do not support transactions.
 The online aspect of OLTP implies that these systems are reading or updating the database immediately in
 response to a change in business state, which excludes offline batch processing from consideration.


From the application viewpoint, the Pattern Frame is constrained by two patterns: 
Object-Oriented Application and Layered Application. Most, if not all, of the application viewpoint patterns 
depend on object-oriented concepts such as encapsulation[封装], polymorphism[多态性], and inheritance to successfully 
resolve their forces. Therefore, the Pattern Frame addresses only object-oriented applications and 
specifically does not address procedural applications.


Interesting enterprise applications are usually composed of a large number of objects and services
 that must collaborate to provide something of value to the business. To manage these collaborations,
 there must exist some high-level organization of the system. Most enterprise class systems use a 
layered approach to manage this complexity. As a result, the Pattern Frame addresses only applications
 that are designed as a set of layers and specifically excludes monolithic applications with little
 or no internal structure. 


From the infrastructure viewpoint, the model is constrained to a hardware infrastructure that supports
 distributing an application over a number of servers arranged into tiers. The tiered approach is 
commonly used for enterprise applications, because it has a relatively low startup cost and it supports 
a scaling out strategy where inexpensive servers can be added to the infrastructure to add incremental 
capability. Excluded from the model are solutions based on deploying applications to a single mainframe 
or large multiprocessor computer.


The deployment perspective is concerned with bridging the gap across the applications and infrastructure
 viewpoints. As a consequence, it does not have any constraints of its own, but operates within the 
constraints set by the application and infrastructure viewpoints. In other words, the highest-level
 deployment pattern is about mapping layered applications to a tiered distribution infrastructure and 
does not impose any additional constraints of its own.

Taken as a group, these four high-level constraints, or root constraints, help to narrow the patterns 
that are in scope for the remainder of this guide. Figure 7 shows the root constraints along the top of the Pattern Frame.

[Figure 7: Root constraints of the Pattern Frame]

Reducing the scope of the Pattern Frame makes it possible to focus on specific patterns and the relationships 
between them in more relevant detail.

##Pattlets 

The use of root constraints reduces the number of patterns to a manageable order of magnitude[巨大]. 
Nevertheless, elaborating on all patterns in the grid takes a significant amount of effort. 
Developing all patterns in isolation and then publishing "the ultimate[最后的] patterns guide" would 
counteract many benefits realized by the patterns communities. Patterns need to evolve as the 
collective understanding of them evolves. Patterns are not created by a single author, but are harvested 
from actual use in the software development community. Recognizing the evolutionary nature of patterns,
 the authors of this guide have published the subset of patterns included here to obtain feedback and 
start building a community.

Deferring patterns until later, however, leaves holes in the pattern graph, which could result in 
related patterns suddenly becoming disconnected. To preserve the integrity of the relationships inside
 the pattern graph, this guide includes the patterns that were not included in the first release as
 pattlets. Pattlets are actual patterns that have not yet been documented in detail. A pattlet describes
 a solution to a problem, but does not contain a detailed description of the context, problem, or forces
 that may impact the solution.

The concept of pattlets is also useful for referencing prior pattern works. The patterns community has 
been discovering and documenting software patterns for over a decade. It would be foolish to try to 
replicate these efforts. It would also be foolish, however, to require readers to purchase several 
other books as context for these patterns. Therefore, this guide includes a pattlet whenever it references
 a pattern that is described in an existing book about patterns. The pattlet includes the reference to 
the original work for those readers who would like to look at the complete pattern in more detail.

For a detailed list of all pattlets, see Appendix A.

##Pattern Language for Solutions 


The constrained Pattern Frame and the patterns it contains provide enough data points to begin using patterns
 to describe entire solutions. In fact, the quoting example from Chapter 1 can be described in terms of 
patterns. Recall that the requirements specified a Web-based quote application. Someone describing the 
architecture of the solution might say something like this:

Let's start by looking at the quote application at the architecture level of abstraction. From the 
application viewpoint, the quote application is an Object-Oriented Application that is logically 
structured as a Three-Layered Services Application. From the database viewpoint, the application 
is based on the OLTP processing model. From the infrastructure viewpoint, the hardware and network 
architecture are based on Four-Tiered Distribution, which calls for separate physical tiers for Web
 server and application server functionality. And finally, from the deployment viewpoint, the team 
has created a Deployment Plan to map components to servers, based on a Complex Web Application.

This concisely describes the architecture of the solution across all four of the viewpoints to anyone familiar
 with the referenced patterns. Continuing down one level of abstraction, you can see how someone might describe
 the design of the system:

From the application viewpoint, let's consider each layer of our Three-Layered Services Application separately.

The presentation layer is structured around a Web presentation framework based on Model-View-Controller (MVC). 
Although MVC provides a level of separation between business and presentation logic, each page contains a great
 deal of common logic. To eliminate this redundancy, we use a Page Controller to render common headers and 
footers and set a friendly display name for the user.


The business layer holds the Customer, Quote, Order, Line Item, and Inventory domain objects. The domain objects 
are realized using Table Module [Fowler03] because speed of development is a key requirement. The Complex Web
 Application Deployment Model calls for separate Web and application tiers. Therefore, the two tiers communicat
e through a Broker. Business entities, acting in the role of Data Transfer Objects [Fowler03], are used to 
encapsulate the information traveling between the two tiers. 

The data layer uses a Data Table Gateway [Fowler03] to access the OLTP database subsystem and a number of data
 access components to support the persistence requirements of the domain objects.


From the infrastructure viewpoint: to meet the operational requirements of the business, we build on the basic 
Four-Tiered Distribution model by adding Load-Balanced Cluster and Failover Cluster. Responding to a requirement
 calling for a high level of concurrent users, we added load balancing to our Web tier. To meet availability
 requirements, we added clustering to our database tier.

The description could continue on to describe the data and deployment viewpoints at the same level of abstraction.
 To continue, instead, down one more level of abstraction, you can see how someone might describe the implementation
 of the solution:

Let's look at the solution from the application viewpoint. The solution is built using Microsoft .NET technology. 
The presentation layer is based on the Web presentation framework that is built into ASP.NET. ASP.NET simplifies
 the implementation of Model-View-Controller with the built-in code-behind page feature. We use the built-in Page
 Controller mechanism in ASP.NET to implement our presentation logic. The domain objects in the business layer
 are managed .NET objects. Because the presentation layer and business layer are deployed on separate tiers, 
we use Implementing Broker with .NET Remoting Using Server-Activated Objects. Finally, the data layer is based 
on the ADO.NET classes within the .NET Framework to provide database access. The Table Modules and business entities
 are constructed using the DataSet component of ADO.NET. The remainder of the Data Access Components are provided
 by the Microsoft Application Blocks for .NET building block.

From the infrastructure viewpoint: Microsoft SQL Server, running in a failover cluster, is used for the OLTP 
database subsystem. Microsoft Network Load Balancing clusters provide load balancing between Web servers.

All of these conversations make frequent references to patterns. This can be daunting at first, but when you 
understand the patterns used, you realize that even this brief description gives you a detailed understanding 
about how the system works. Notice that you gained this understanding without having to wade through reams of
 documentation or step through endless lines of code. The communication benefits of patterns become clear 
if you imagine how much more work would be involved in describing the solution without using patterns. 

##Summary 

This chapter demonstrated how patterns provide a vocabulary to efficiently describe complex solutions without 
sacrificing detail. Effectively, the patterns form a new language with which architects and designers can
 communicate their thinking.

Because of the large number of patterns involved in building enterprise solutions, it can seem difficult to 
learn this new language. This guide structures the patterns into smaller, more closely related sets of patterns.
 This allows you to get started by using a smaller set of patterns, depending on your specific interest or the 
stage of the project.

This chapter introduced four mechanisms to help you navigate the patterns:

* Relationships. Relationships between patterns help you to identify patterns that are closely associated 
to the pattern you are using (for example, Page Controller focuses on the controller aspect of Model-View-Controller).

* Clusters. Clusters group patterns that belong to a common subject area (for example, Web Presentation).

* Levels of abstraction. Levels of abstraction allow you to describe concepts in a manner that is consistent 
with the level of detail of your discussion (for example, an architectural conversation).

* Viewpoints. Viewpoints help you select the vocabulary that is relevant to a team's particular role (for 
example, the infrastructure team).

These mechanisms are not meant to constrain your thinking, but instead are intended to make looking at 
complex systems easier. With practice, you will naturally switch between these mechanisms as you switch 
between roles, subject areas, and levels of detail.