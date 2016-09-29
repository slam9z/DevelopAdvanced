[Chapter 1: Patterns for Building Enterprise Solutions](https://msdn.microsoft.com/en-us/library/ff648303.aspx)


>"A complex system that works is invariably found to have evolved from a simple system that worked...A complex
> system designed from scratch never works and cannot be patched up to make it work. You have to start over 
>with a working simple system." - John Gall in Systemantics: How Systems Really Work and How They Fail


Enterprise class business solutions, the kind that companies bet their business on, are often extremely complex 
and must perform well against high expectations[期望]. Not only must they be highly available and scalable in the face
 of unpredictable[难以预料的] usage, they must also be malleable[可塑的] and predictable[可预见的] in response to rapidly
changing  business requirements. 

The best solutions are those composed of a set of smaller, simple mechanisms that solve simple problems reliably
 and effectively. During the process of building larger and more complex systems, these simple mechanisms combine
 to evolve the larger system.

Knowledge of these simple mechanisms does not come easy. It usually resides[存在] in the minds of experienced 
developers and architects and is an important part of the tacit knowledge they bring to projects.

This guide captures the knowledge of seasoned developers and presents it in the form of a patterns catalog.
 Each pattern contains a simple, proven[proven] mechanism that solves a small problem effectively. Although you
 can understand and apply each pattern individually, you often combine these patterns to build complex systems.

Patterns are useful to developers and architects because they:

* Document simple mechanisms that work.
* Provide a common vocabulary[词汇] and taxonomy[分类学] for developers and architects.
* Enable solutions to be described concisely as combinations of patterns.
* Enable reuse of architecture, design, and implementation decisions.

This chapter introduces the notion of a pattern, explains how a pattern documents simple, proven mechanisms,
 and shows how collections of patterns provide a common language for developers and architects. 
To illustrate these concepts, this chapter applies abbreviated versions of actual patterns to real-life development situations. 

##Patterns Document Simple Mechanisms 

A pattern describes a recurring problem that occurs in a given context and, based on a set of guiding forces,
 recommends a solution. The solution is usually a simple mechanism, a collaboration between two or more classes, 
objects, services, processes, threads, components, or nodes that work together to resolve the problem identified 
in the pattern. 

>Note: Although the underlying mechanisms described in these patterns are conceptually simple, in practice 
>their implementation can become quite complex. The implementation requires skill and judgment to tailor 
>general patterns to fit specific circumstances. In addition, the pattern examples in this chapter are highly
> abbreviated for the purpose of introduction; the actual patterns in subsequent chapters are much more detailed.

Consider the following example:

You are building a quote application, which contains a class that is responsible for managing all of the quotes
 in the system. It is important that all quotes interact with one and only one instance of this class. How do
 you structure your design so that only one instance of this class is accessible within the application?


A simple solution to this problem is to create a QuoteManager class with a private constructor so that no
 other class can instantiate it. This class contains a static instance of QuoteManager that is returned with
 a static method named GetInstance(). The code looks something like this:
 
```sql
public class QuoteManager
{
     //NOTE: For single threaded applications only
     private static QuoteManager _Instance = null;
     private QuoteManager() {}
     public static QuoteManager GetInstance() 
     {
          if (_Instance==null) 
          {
               _Instance = new QuoteManager ();
          }
          return _Instance;
     }
     
     //... functions provided by QuoteManager 
}
```
 
It is likely that you have solved problems like this in a similar manner, as many other developers have. In fact,
 pattern writers on the lookout for recurring problems and solutions have observed this kind of implementation 
frequently, distilled the common solution, and documented the problem-solution pair as the Singleton pattern [Gamma95].


##Patterns as Problem-Solution Pairs 


Notice that the Singleton pattern does not mention a Quote or QuoteManager class. Instead, the pattern 
looks something like the following abbreviated example.

[Figure 1: Singleton pattern, abbreviated]

Comparing the abbreviated pattern example in Figure 1 with the QuoteManager source code illustrates the difference
 between the pattern, which is a generalized problem-solution pair, and the application of the pattern, which is
 a very specific solution to a very specific problem. The solution, at a pattern level, is a simple, yet elegant,
 collaboration between several classes. The general collaboration in the pattern applies specifically to the QuoteManager
 class, which provides the mechanism that controls instantiations in the quote application. Clearly, you can apply 
the same pattern to countless applications by modifying the pattern slightly to suit specific local requirements.


Written patterns provide an effective way to document simple and proven mechanisms. Patterns are written in a 
specific format, which is useful as a container for complex ideas. These patterns exist in the minds of developers, 
and their code, long before they are documented and given pattern names. At some point, pattern writers discove
r these patterns from actual implementations and generalize them so they can be applied to other applications.

Although pattern writers usually provide implementation code examples within these generalized patterns, it is 
important to understand that there are many other correct ways to implement these patterns. The key here is to
 understand the guidance within the pattern and then customize it to your particular situation. For example, 
if you are familiar with the Singleton pattern, you probably noticed that the code example is based on the [Gamma95]
 implementation. This implementation is used here because it is the most popular example and requires the least
 explanation for the purposes of this introduction to patterns. However, an implementation of Singleton optimized
 for the C# language would look quite different, and while these two implementations differ significantly, both would be correct. 

##Patterns at Different Levels 


Patterns exist at many different levels of abstraction. Consider another example, this time at a higher level of 
abstraction than the level of source code:

You are designing a Web-based quote application containing a great deal of business and presentation logic, 
which, in turn, depends on numerous platform software components to provide a suitable execution environment.
 How do you organize your system at a high level to be flexible, loosely coupled, and yet highly cohesive[使内聚的]?

One solution to this problem involves organizing your system into a series of layers, with each layer containing
 elements at roughly the same level of abstraction. You then identify the dependencies in each layer and decide
 on either a strict or a relaxed layering strategy. Next, you decide if you are going to create a custom layering
 scheme or adopt a layering scheme previously documented by others. In this case, let's say you decide to use a 
well-known layering strategy: one layer each for presentation, business logic, and data access. Figure 2 shows 
how your layering scheme might look.

[Figure 2: Quote application layers]


If you always design systems this way, then you employ this pattern already, independent of any generalized
 pattern. Even so, there are many reasons why you might want to understand the patterns that underpin this 
design approach. You may be curious about why systems frequently are built this way, or you may be looking for more 
optimal approaches to problems that this pattern does not quite resolve. In either case, it is worth examining the 
patterns and mechanisms at work here.

Using layers as a high-level organizing approach is a well-established pattern described in the Layers pattern 
[Buschmann96]. Figure 3 shows an abbreviated version of this pattern.

[Figure 3: Layers pattern, abbreviated]

This simple strategy for organizing applications helps to solve two challenges in software development:
 the management of dependencies and the need for exchangeable components. Building applications without
 a well-considered strategy for dependency management leads to brittle and fragile components, which are 
difficult and expensive to maintain, extend, and substitute.

The mechanisms at work inside the Layers pattern are more subtle[不明显的] than those of the Singleton.
 For Layers, the first collaboration is at design time between classes, because the layered organization 
localizes the effects of source code changes and prevents the changes from rippling[涟漪] throughout the entire 
system. The second collaboration is at runtime, when relatively independent components within a layer become 
exchangeable with other components, again isolating the rest of the system from impact.


Although the Layers pattern is general enough to apply to areas such as network protocols, platform software, 
and virtual machines, it does not resolve certain specific forces that are present in enterprise-class business
 solutions. For example, in addition to managing complexity by decomposition (the essential problem solved by Layers),
 business solution developers also need to organize for effective reuse of business logic and conserve valuable 
connections to expensive resources such as databases. One way to solve this problem is by using the Three-Layered
 Application pattern. Figure 4 shows the abbreviated description of this pattern.

[Figure 4: Three-Layered Application, abbreviated]


Again, there is a difference between the pattern (Three-Layered Application) and the application of the pattern
 (quote application layering model). The pattern is a generalized problem-solution pair on the topic of application 
organization. In contrast, the application of the pattern solves a very specific problem by creating specific layers,
 each layer resolving very specific requirements.

###Simple Refinement[精炼] 


Notice that Three-Layered Application is really a simple refinement of Layers;the context, forces, and solution
 identified in Layers still apply to Three-Layered Application,but not the other way around. That is, the Layers 
pattern constrains Three-Layered Application, and the Three-Layered Application pattern refines the Layers pattern.
 This pattern relationship is useful to manage complexity. After you understand one pattern, you must only 
understand the incremental differences between the initial pattern and patterns that refine it. Another example
, this time in the area of Web services, should help to illustrate the concept of refinement:


You built a quote application for a successful enterprise that is rapidly expanding. Now you want to extend the 
application by exposing your quote engine to business partners and integrating additional partner services 
(such as shipping) into the quote application. How do you structure your business application to provide and consume 
services? 

One solution to this problem is to extend Three-Layered Application by adding additional service-related 
responsibilities to each layer. The business layer adds the responsibility for providing a simplified set of
 operations to client applications through Service Interfaces. The responsibilities of the data access layer
 broaden beyond database and host integration to include communication with other service providers. This 
additional functionality in the data access layer is encapsulated in Service Gateway components, which are 
responsible for connecting to services (both synchronously and asynchronously), managing basic conversational
 state with the service, and notifying business process components of significant service-related events.

The Three-Layered Services Application (Figure 5) captures this problem-solution pair.

[Figure 5: Three-Layered Services Application, abbreviated]

Applying the Three-Layered Services Application pattern to the quote application example results in the following model.

[Figure 6: Three-Layered Services Application applied to the quote application]

Notice the relationships between these patterns (see Figure 7). Layers introduces a fundamental strategy for organizing 
a software application. Three-Layered Application refines this idea and constrains it to business systems that require
 business logic reuse, flexible deployment, and efficient use of connections. Three-Layered Services Application refines
 Three-Layered Application and extends the design to provide and consume granular elements of data and logic from highly
 variable sources. 

[Figure 7: Refinement of related patterns]

Adding additional types of components to specific layers is not the only way to manage this growing complexity. As 
complexity warrants, designers often create additional layers within the application to handle this responsibility. 
For example, some designers move Service Interfaces into a separate layer. Other designers separate the business layer
 into a domain layer and an application layer. In any case, you sometimes see these three layers expanded to four,
 five, or even six layers as designers use this pattern in response to complex requirements. Conversely, the Layers
 pattern was also used in the relatively simpler days of client-server applications, when two-layered applications
 were the standard.

When grouped together, these Layers variations form a cluster of patterns (see Figure 8) that visually represents common
 approaches to application layering. Clustering, used in this context, simply means a logical grouping of some set of
 similar patterns. This notion of a cluster is quite useful for expanding the view of patterns to encompass an entire
 solution, and for identifying clusters of patterns that address similar concerns in the solution space. Chapter 2,
 "Organizing Patterns," discusses clusters in more detail.

[Figure 8: A cluster of patterns]

##Common Vocabulary[词汇量]

While considering the Singleton, Layers, Three-Layered Application, and Layered Services Application patterns, you 
probably noticed that patterns also provide a powerful vocabulary for communicating software architecture and design
 ideas. Understanding a pattern not only communicates the knowledge and experience embedded within the pattern but 
also provides a unique, and hopefully evocative[引起记忆的], name that serves as shorthand for evaluating and describing software
 design choices.

For example, when designing an application a developer might say, "I think the pricing engine should be implemented 
as a Singleton and exposed through a Service Interface." If another developer understands these patterns, he or she
 would have a very detailed idea of the design implications under discussion. If the developer did not understand
 the patterns, he or she could look them up in a catalog and learn the mechanisms, and perhaps even learn some additional 
patterns along the way. 

Patterns have a natural taxonomy. If you look at enough patterns and their relationships, you begin to see sets of ordered 
groups and categories at different levels of abstraction. For example, the Singleton pattern example was at a lower level 
of abstraction than the Layers pattern, but the Layers pattern had a set of related patterns that refined it in one way or 
another. Chapter 2 further expands and refines this taxonomy.

Over time, developers discover and describe new patterns, thus extending the community body of knowledge in this area.
 In addition, as you start to understand patterns and the relationships between patterns, you can describe entire 
solutions in terms of patterns.

##Concise[简明的] Solution Description 


In this guide, the term solution has two very distinct meanings: 

first, to indicate[指出] part of a pattern itself, as in a  problem-solution pair contained within a context; 
second, to indicate a business solution. When the term business solution is used, it refers to a software-intensive system
 that is designed to meet a specific set of functional and operational business  requirements. A software-intensive system 
implies that you are not just concerned with software; you must deploy this software  onto hardware processing nodes to 
provide a holistic technology solution. Further, the software under consideration includes
 both custom-developed software and purchased software infrastructure and platform components, all of which you integrate together.

##Summary 

This chapter introduced the concept of a pattern, explained how patterns document simple, proven mechanisms, and showed 
how patterns provide a common language for developers and architects. Chapter 2 explains how to organize your thinking 
about patterns, and how to use patterns to describe entire solutions concisely.