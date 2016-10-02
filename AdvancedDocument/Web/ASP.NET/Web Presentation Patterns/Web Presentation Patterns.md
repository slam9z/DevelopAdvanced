[Web Presentation Patterns](https://msdn.microsoft.com/en-us/library/ff650511.aspx)

##Presentation Patterns

*This content is outdated and is no longer being maintained. 
It is provided as a courtesy for individuals who are still using these technologies.
This page may contain URLs that were valid when originally published, but now link to sites or pages that no longer exist.
Please see the [patterns & practices guidance](https://msdn.microsoft.com/en-us/library/ff921345.aspx) for the most current information.*


公司技术不更新我也办法，找到一个更好的解决方案，不管是什么技术，只要用都把它弄明白用好。
技术用好了，如果确实符合公司现状其实差别就不大。


>"An architect's first work is apt to be spare and clean. He knows he doesn't know what he is doing, 
>so he does it carefully and with great restraint. As he designs the first work, 
>frill after frill and embellishment after embellishment occur to him. These get stored away to be used 'next time'...
>This second system is the most dangerous system a man ever designs...
>The general tendency is to over-design the second system using all the ideas and frills that were cautiously sidetracked on the first one."
> - Frederick P. Brooks, Jr. in The Mythical Man Month, 1972


The first systems on the Web were simply linked static HTML pages that enabled document sharing between distributed teams.
 As user adoption increased, dynamic Web pages that responded to user input became common. Early dynamic pages were typically
 written as Common Gateway Interface (CGI) scripts. These CGI scripts not only contained the business logic for deciding what
 to display in response to user input, they also generated the presentation HTML. As demand for more complex logic increased, 
so did the demand for richer and more engaging presentations. This increased complexity strained the CGI programming model. 

Soon page-based development (for example, ASP and JSP) emerged. This allowed developers to embed script directly into HTML pages, 
thus simplifying the programming model. As these embedded script applications became more complex, developers wanted to separate 
out business logic from presentation logic at the page level. In response, tag libraries with helper objects and code-behind page
 strategies emerged. Elaborate frameworks appeared, which offered dynamically configurable site navigation and command dispatchers, 
all at the cost of additional complexity. Given the wide range of Web presentation options now available, how do you choose
 the appropriate Web presentation design strategy for your application?


##Complexity and Redundancy (复杂和多余)

Unfortunately, there is no single design strategy that is right for all situations. This is due to the competing needs 
in software design to eliminate excessive redundancy and excessive complexity. 

You can start with a simple page that contains embedded script, and soon the business logic is repeated across files, 
making the system difficult to maintain and extend. You can move this logic into a set of collaborating components to 
eliminate redundancy, but doing so adds complexity to the solution. Instead, you can start off with a framework that
 offers tag libraries, dynamic configuration, and command dispatchers, but although this eliminates redundant code,
 it adds a great deal of complexity to the system, often unnecessarily. 

Adding complexity obscures your intentions, making the system more difficult for other developers to understand. 
The added complexity also makes the system harder to maintain and extend, thereby increasing the total cost of ownership. 
If this added complexity is carefully considered and reserved for meeting current requirements, it can be worthwhile.
 Extra complexity is sometimes added based on speculation that it might be needed someday, rather than based on current requirements.
 This can clutter code with unnecessary abstractions that impede understanding and your ability to deliver a working system today.

So, again, how do you wade through the choices to arrive at an appropriate Web presentation design strategy for your application? 

First, it is important to understand the key Web application design issues, possible solutions, and the associated tradeoffs.
 This chapter gives developers a strong head start down that path. In the process, you will become familiar with options, 
assess tradeoffs, and then pick the least complex solution that meets the application's requirements. Think carefully before 
choosing a more complex solution that supports possible future change scenarios over a simpler solution that meets today's
 requirements. Sometimes the extra cost is justified, but quite often it is not.



##Patterns Overview 

This patterns cluster starts off simply with *Model-View-Controller* (MVC),
 a long-standing pattern that has stood the test of time when it comes to separating business logic from presentation logic. 
Although this pattern is not new [Buschmann96], this collection presents it in a simplified form that is tailored for building b
usiness solutions, not for building user interface frameworks for rich clients. 
The pattern is written first at the design level, and is then mapped to a 
platform implementation named [Implementing Model-View-Controller in ASP.NET](https://msdn.microsoft.com/en-us/library/ff647462.aspx). 


Figure 1 shows the Web Presentation patterns cluster.

![]()

Figure 1: Web Presentation patterns cluster(群集)


The implementation of MVC with Microsoft ASP.NET starts with an example of a simple system, written on a single page, with 
application logic embedded in the presentation elements. As complexity grows, the code-behind feature of ASP.NET is used
 to separate the presentation code (view) from the model-controller code. This works well, until requirements drive you 
to consider reusing the model code, without the controller, to avoid redundancy within your application. At this point, 
independent models are created to abstract the business logic, and the code-behind feature is used to adapt the model to
 the view code. The implementation then finishes off with a discussion about the testing implications of this MVC approach.


So far, the use of the Model-View-Controller pattern has focused on the model and the view; the controller plays a relatively minor role. 
In fact, the controller at work in this pattern is really the implicit controller in ASP.NET. It is responsible for sensing user 
events (requests and postbacks) and wiring those events to the appropriate system response, which in this case is the events in t
he code-behind page.


In dynamic Web-based applications, many common tasks are repeated during each page request, such as user authentication,
 validation, request parameter extraction, and presentation-related database lookups. Left unmanaged, these tasks can quickly
 lead to unnecessary code duplication. Because these tasks have everything to do with sensing user events and determining the 
proper system response, the logical place to put this behavior is in the controller.


###More Powerful Controllers 


The next pattern in this cluster is *Page Controller*, which is a refinement of Model-View-Controller and is appropriate for
 the next level of complexity. This pattern uses a controller at the page scope, accepts input from the page request, 
invokes the requested actions on the model, and then determines the correct view to use for the resulting page. Duplicate logic,
 such as validation, is moved into a base controller class.


Implementing Page Controller with ASP.NET illustrates the power of the ASP.NET built-in page controller 
functionality with a common look-and-feel example. The implementation also uses the Template Method [Gamma95] pattern, 
in conjunction with Page Controller, to define the skeleton of an algorithm in an operation, deferring some of those steps to subclasses.

As more complexity is added to the application, eventually the page controlleraccumulates a great deal of logic in the base class, 
a problem which is often solved by deepening the page controller inheritance hierarchy. Given enough complexity, 
both of these factors lead to code that is hard to maintain and extend. Also, certain applications need dynamic configuration
 of navigation maps, which would potentially span multiple page controllers. When this level of complexity occurs, 
it is time to consider Front Controller.


*Front Controller*, the next pattern in this catalog, is also a refinement(精炼) of Model-View-Controller. 
In a front controller, all of the requests are channeled through a single, usually, two-part controller. 
The first part of the controller is the handler, and the second part is a hierarchy of Commands [Gamma95]. 
The commands themselves are part of the controller and represent specific actions that the controller triggers. 
After the action has executed, the command chooses which view to use to render the page. Usually, this controller 
framework is built to use a configuration file that maps requests to actions, and is therefore easy to change after 
it is built. The tradeoff, of course, is in the level of complexity inherent in this design.



###Filters and Caching 


The last two patterns in this cluster involve filters and caching.

*Intercepting Filter* offers a solution to the problem of how to implement common preprocessing and post-processing of the HTTP request.
 An Intercepting Filter is an ideal place to perform common tasks that are not application-specific, such as security checks, 
logging, compression, encoding, and decoding. Intercepting filters are typically concerned with performing one particular task. 
If multiple tasks execute against the HTTP request, multiple filters are chained together. Implementing Intercepting Filter 
in ASP.NET Using HTTP Module highlights the ease with which you can implement this pattern in ASP.NET.


*Page Cache* deals with increasing the scalability and performance of Web applications by keeping a copy of often-used dynamic
 Web pages that are expensive to create. After the page is initially created, the copy is sent in response to future requests. 
Page Cache also discusses several key cache design factors such as cache refresh, data freshness, and cache granularity.
 Implementing Page Cache in ASP.NET Using Absolute Expiration demonstrates the built-in page cache functionality of ASP.NET.

##Web Presentation Patterns 

The following table lists the patterns included in the Web Presentation patterns cluster. The patterns are arranged so
 that later patterns build on earlier patterns. This implies a progression from more general patterns (such as Model-View-Controller)
 to more specific patterns (such as Intercepting Filter). 

Table 1: Web Presentation Patterns


##Web Presentation Patterns 

###[Model-View-Controller](https://msdn.microsoft.com/en-us/library/ff649643.aspx)

How do you modularize(模块化) the user interface functionality of a Web application so that you can easily modify the individual(与众不同的) parts?

[Implementing Model-View-Controller in ASP.NET](https://msdn.microsoft.com/en-us/library/ff647462.aspx)


###[Page Controller](https://msdn.microsoft.com/en-us/library/ff649595.aspx)

How do you best structure the controller for moderately complex Web applications so that you can achieve reuse and flexibility 
while avoiding code duplication?

[Implementing Page Controller in ASP.NET](https://msdn.microsoft.com/en-us/library/ff649415.aspx)


###[Front Controller](https://msdn.microsoft.com/en-us/library/ff648617.aspx)

How do you best structure the controller for very complex Web applications so that you can achieve reuse and 
flexibility while avoiding code duplication?

[Implementing Front Controller in ASP.NET Using HTTP Handler](https://msdn.microsoft.com/en-us/library/ff647590.aspx)

###[Intercepting Filter](https://msdn.microsoft.com/en-us/library/ff647251.aspx)

How do you implement common pre- and post-processing steps around Web page requests?

[Implementing Intercepting Filter in ASP.NET Using HTTP Module](https://msdn.microsoft.com/en-us/library/ff649096.aspx)

###[Page Cache](https://msdn.microsoft.com/en-us/library/ff648482.aspx)

How do you improve the response time of dynamically generated Web pages that are requested frequently but consume a large 
amount of system resources to construct?

[Implementing Page Cache in ASP.NET Using Absolute Expiration](https://msdn.microsoft.com/en-us/library/ff649217.aspx)

###[Observer](https://msdn.microsoft.com/en-us/library/ff649896.aspx)

How can an object notify other objects of state changes without being dependent on their classes?

[Implementing Observer in .NET](https://msdn.microsoft.com/en-us/library/ff648108.aspx)



