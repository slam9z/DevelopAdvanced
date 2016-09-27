[Intercepting Filter](https://msdn.microsoft.com/en-us/library/ff647251.aspx)

Context 
Anyone who has built a Web application from scratch realizes that it requires bit more housekeeping work than building an internal client-server application. First, you have to deal with the HTTP and all its quirks such as HTTP headers, multi-part forms, the statelessness of HTTP, character set encoding schemes, Multipurpose Internet Mail Extensions (MIME) types, and URL rewriting. On top of that, you have to deal with security measures such as Secure Sockets Layer (SSL) and user authentication. In many situations, the list continues on to include such items as client browser detection or user activity logging. 
Web application server frameworks perform many of these tasks for you, but sometimes you need additional control, or you need to insert your own processing steps before or after the application processes the Web page request.
Problem 
How do you implement common pre- and post-processing steps around Web page requests?
Forces 
There are many ways to approach this problem, so you will need to consider what forces and tradeoffs are involved:
It is common practice to separate lower-level functions, such as dealing with HTTP headers, cookies, or character encoding, from the application logic. This enables you to test and reuse the application logic in other environments that may not use a Web client. 
Pre-processing and post-processing features may change at a different pace than application functionality. After you have the character set encoding module working, you are less likely to change it than the code that deals with rendering the HTML page. Therefore, a separation of concerns helps to limit the propagation of changes. 
Many pre-processing and post-processing tasks are common to all Web pages. You should, therefore, try to implement these functions in a central location to avoid code duplication. 
Many of the lower-level functions are not dependent on each other. For example, browser detection and character encoding detection are two independent functions. To maximize reuse, you should encapsulate these functions in a set of composable modules. This enables you to add or remove modules without affecting existing modules.
In many instances, it is very beneficial to be able to add or remove modules at deployment time rather than at compile-time. For example, you may deploy the character encoding detection module only in the international deployment of the software, but not in the local deployment. Or, you may have a free Web site for anonymous users to which you want to add an authentication module that requires users to sign in. This ability to add or remove modules at deployment time without having to make code changes is often called deployment-time composability.
Because lower-level functions are executed for every single page request, performance is critical. This means two things: do as little as possible and do it efficiently. You do not want to overload these common functions with unnecessary features or decision points, but you do want to minimize access to slower, external resources such as databases. Therefore, you should make each processing step as compact and as efficient as possible.
You may even consider implementing some of these functions in a different programming language, for example a language that is very efficient at processing character streams (such as C++). On the other hand, using a different language may preclude you from using some of the useful features that the application framework provides (for example, automated memory management and object pooling). Either way, it is a benefit to be able to detach preprocessing from the main application so that you have the choice of using a different programming language if necessary.
After you create these pre-processing and post-processing functions, you want to be able to reuse them in other Web applications. You want to structure them so that you can reuse one module in another environment without depending on the other modules. You also want to be able to combine existing modules with new modules without having to make any code changes.
Solution 
Create a chain of composable filters to implement common pre-processing and post-processing tasks during a Web page request.

Figure 1: Chain of composable filters

The filters form a series of independent modules that can be chained together to execute a set of common processing steps before the page request is passed to the controller object. Because the individual filters implement identical interfaces, they do not have explicit dependencies on each other. Therefore, new filters can be added without affecting existing filters. You can even add filters at deployment time by instantiating them dynamically based on a configuration file.

As much as possible, you should design the individual filters in such as way that they make no assumptions about the presence of other filters. This maintains the composability; that is, the ability to add, remove, or rearrange filters. Also, some frameworks that implement the Intercepting Filter pattern do not guarantee the order in which the filters are executed. If you find that you have strong interdependencies between multiple filters, a regular method with calls to helper classes may be the better choice because it guarantees to preserve the constraints in the filters sequence.

In some contexts, the term Intercepting Filter is associated with a specific implementation using the Decorator pattern [Gamma95]. The solution described here takes a bit more abstract view and considers different implementation options of the Intercepting Filter concept.
Filter Chain 


A straightforward implementation of Intercepting Filter is a filter chain that iterates through a list of all filters. The Web request handler executes the filter chain before passing control to the application logic (see Figure 2).

Figure 2: Intercepting Filter class diagram

When the Web server receives a page request, Request Handler passes control to the FilterChain object first. This object maintains a list of all filters and calls each filter in sequence. FilterChain can read the sequence of filters from a configuration file to achieve deployment-time composability. Each filter has the chance to modify the incoming request. For example, it can modify the URL or add header fields to be used by the application. After all filters have been executed, Request Handler passes control to the controller, which executes the application functionality (see Figure 3). 

Figure 3: Intercepting Filter sequence diagram

One of the key benefits of this design is that filters are self-contained components without any direct dependency on the other filters or the controller, because FilterChain invokes each filter. Therefore, a filter does not have to hold a reference to the next filter. The handler passes a context into each filter on which the filter operates. The filter can manipulate the context, for example, by adding information or redirecting the request.
Decorator 


An interesting alternative implementation to the Intercepting Filter pattern uses the Decorator pattern around a Front Controller. Decorator wraps an object in such a way that it provides the same interface as the original object. As a result, the wrapping is transparent to any other object that references the original object. Because the interface of the original object and wrapper are identical, you can add additional wrappers around the wrapper to create a chain of wrappers that is very similar to a filter chain. Inside each wrapper, you can perform pre-processing and post-processing functions.

Figures 4 and 5 show how this concept can be used to implement Intercepting Filter. Each filter implements the Controller interface. It also holds a reference to the next object that implements the Controller interface, which could be either the actual controller (concreteController) or another filter. Even though the filters call each other directly, there is no direct dependency between the filters, because each filter only references the Controller interface instead of the next filter class. 

Figure 4: Decorator class diagram

Before the filter passes control to the next filter, it has the opportunity to perform pre-processing tasks. Likewise, after the rest of the chain is finished processing the request, the filter has an opportunity to perform post-processing tasks.

Figure 5: Decorator sequence diagram

The Decorator approach avoids the need for a FilterChain class that iterates over the filters. Also, the request handler is now completely unaware of the existence of the filters. As far as the request handler is concerned, it simply calls the controller by using the Controller interface. This approach usually appears more elegant to hardcore object-oriented developers, but it can be a bit more difficult to figure out what is going on by looking at the code. The Decorator approach relates to the Filter Chain approach much as a linked list relates to an array with an iterator.

Even though the object instances have references to each other, you can still compose the chain at runtime. You can instantiate each filter passing along a reference to the Controller interface of the next filter object in the chain. That way, you can build the filter chain dynamically from back to front.
Event-Driven Filters 


In an ideal world, you would design the individual filters in such a way that they were not dependent on the sequence in which they were executed, but the real world rarely works that way. Even if you manage to design the filters independently, they will end up replicating a lot of functionality. For example, each filter that has to analyze the HTTP headers (for example, to do browser detection and extract cookies) will have to parse the headers, extract the header element names, and perform some action on them. It would be much easier if the framework could do some of this work and pass along a collection of all header elements, validated and indexed by element name. This would make the filter development easier and less error-prone, but then all filters would depend on this common header parsing function. This would not be a problem unless a filter had to access the HTTP request stream before any header parsing occurred (maybe because you wanted to manipulate or rearrange some header information).

If you want to provide additional base functionality, but still allow filters to be plugged into the request stream, you must define multiple filter chains. Each chain is then executed before or after the framework completes a processing step. For example, you can have a filter chain that is executed before any header parsing occurs and have a second filter chain that is executed after the headers are parsed (see Figure 6). If you take this concept to its logical conclusion, you can define a whole series of events. You can let the filter decide which event it wants to attach to, based on what function it performs and what services it needs from the framework. 

Figure 6: Event-driven intercepting filters

This model shares some similarities to the event model described in the Observer pattern. In both cases, objects can "subscribe" to events without the original object being dependent on the observers. The object has no dependencies on any specific observers because it calls the observers through an abstract interface. The key difference between Intercepting Filter and Observer lies in the fact that the observer generally does not modify the source object; it "observes" passively what is going on in the source object. The purpose of Intercepting Filter, on the other hand, is to intercept and modify the context in which it is called.

Figure 6 also illustrates very well how each filter intercepts the sequence of events inside the Web server framework, hence the name Intercepting Filter.
Variations 
In most cases, filters are passive in the sense that they manipulate the context, but do not affect flow of execution. In the case of a filter intercepting a Web request, however, you often must design filters so that they redirect the request to a different page. For example, an authentication filter may redirect the request to an error page or to the logon page if the authentication fails.
To illustrate how these filters affect the flow of the Web request, Figure 7 shows the sequence of a typical filter scenario, in which the intercepting filterdoes not intervene in the message flow.

Figure 7: Intercepting filter that does not intervene in the message flow 

Figure 8 shows an alternate sequence in which Filter One redirects the flow to a different page based on the type of request. 

Figure 8: Intercepting filter that redirects the message flow

In this scenario, no page is rendered, but a redirect header (HTTP response 302) is produced and is returned to the client. This header causes the client to issue a new request to the URL specified in the redirect header. Because this type of redirection requires a second request from the client browser, it is often referred to as client-side redirect. The main disadvantage is that the client browser has to issue two requests to retrieve the page. This slows down the page display and can also lead to complications with bookmarking, because the client will bookmark the redirected URL, which is generally not good.

Server-side redirects, on the other hand, forward the request to a different page without requiring a roundtrip to the client. They accomplish this by returning control to the httpRunTime object, which calls a different Page Controller directly, passing along the request context. The transfer happens internally in the server without any involvement of the client. As a result, you do not have to repeat any common preprocessing of the request. 

Server-side redirects are used in two common scenarios: URL manipulation can be used in Intercepting Filter to allow clients to use virtual URLs to pass parameters to the application. For example, a filter can convert http://example.com/clientabc into the URL http://www.example.com/start.aspx?Client=clientabc. This manipulation provides a level of indirection that lets the client bookmark a virtual URL that is not affected by internal changes to the application (for example, the migration from .asp to .aspx files). The other common technique that uses server-side redirection is the use of a Front Controller. The Front Controller processes all page requests in a central component and then passes control to the appropriate command. Front Controllers are useful for Web applications with dynamically configurable navigation paths.
Example 
Because intercepting filtersare such a common need when processing Web requests, most Web frameworks provide mechanisms for the application developer to hook intercepting filters into the request-response process.
The Microsoft Windows platform provides two distinct mechanisms:
The server running Internet Information Services (IIS) provides ISAPI filters. ISAPI filters are low-level constructs that are called before any other processing is performed. As a result, ISAPI filters have a high degree of control over the processing of the request. ISAPI filters are ideal for low-level functions such as URL manipulations. Unfortunately, ISAPI filters should be written in C++ and do not have access to any of the functions incorporated into the Microsoft .NET Framework.
The .NET Framework provides the HTTPModule interface. Using a configuration file, filters that implement this interface can be attached to a series of events defined by the framework. For more detail, see Implementing Intercepting Filter in ASP.NET Using HTTP Module.
Resulting Context 
The Intercepting Filter pattern results in the following benefits and liabilities:
Benefits 

Separation of concerns. The logic contained in the filters is decoupled from the application logic. Therefore, the application code is not affected when low-level features change (for example, if you move from HTTP to HTTPS or if you migrate session management from URL rewriting to hidden form fields).
Flexibility. The filters are independent of one another. As a result, you can chain together any combination of filters without having to make code changes to any filter.
Central configuration. Due to the composability of filters, you can use a single configuration file to load the filter chain. Rather than working with a lot of source code, you can modify a single configuration file to determine the list of filters to be inserted into the request processing.
Deployment-time composability. Intercepting Filter chains can be constructed at runtime based on configuration files. As a result, you can change the sequence of filters during deployment without having to modify code.
Reuse. Because the filters are not dependent on their operating environment, except for the context on which they operate, individual filters can be reused in other Web applications.
Liabilities 

Order dependency. Intercepting filters have no explicit dependencies on any other filter. However, filters may make assumptions about the context that is passed to them. For example, some filters may expect certain processing to have occurred before they are invoked. Consider these implicit dependencies when you configure the filter chain. Some frameworks may not guarantee the order of execution across filters. If the program requires a strict sequence, a hard-coded method call may be a better solution than a dynamic filter chain.
Shared state. Filters have no explicit mechanism for sharing state information with one another except to manipulate the context. This is also true for passing information from a filter to the controller. For example, if the filter analyzes the browser type based on header-field values, there is no simple way to pass this information to the application controller. The most common way is to add a fake header field to the request context that contains the filter output. The controller can then extract the fake header field and make a decision based on its value. Unfortunately, you lose any compile-time checking or type safety between the filter and the controller. This is the downside of loose coupling. 
Intercepting Filter vs. Controller 


Because the intercepting filters are executed right before and after the controller, sometimes it may be difficult to determine whether to implement functionality inside the intercepting filteror inside the controller. The following criteria provide some guidelines when making this decision:
Filters are better suited to dealing with low-level, transport-related functions such as character-set decoding, decompression, session validation, client-browser type recognition, and traffic logging. These types of operations tend to be well-encapsulated, efficient, and stateless. Therefore, it is easy to chain these operations together without one operation having to pass state information to the other.
True application functionality that interacts with the model is better taken care of inside the controller or a helper of the controller. These types of functions typically do not possess the kind of composability that filters require.
In most cases. the processing inside a filter is not dependent on the state of the application; it is executed no matter what. Even though the page controller may contain common functionality, it is best to maintain the opportunity to override the behavior on a case-by-case basis. The controller is better suited to this task than is a chain of filters.
Many filter implementations (for example, IIS ISAPI filters) execute at a lower layer inside the application server. This gives filters a great deal of control (not much happens before the filter is invoked), but prevents them from accessing many features that the application layer provides, such as session management.
Because filters are executed for every Web page request, performance is critical. As a result, the framework may limit the choice of implementation language. For example, most ISAPI filters are implemented in a compiled language such as C++. You would not want to have to code complex application logic in C++, if you can have the convenience of coding these pieces in the Microsoft Visual Basic development system or in the Microsoft Visual C# development tool with full access to the .NET Framework.
Related Patterns 
For more information, refer to the following related patterns:
Intercepting Filter is commonly used in combination with Front Controller. [Alur01] and [Fowler03] describe the relationship between the two patterns in detail.
Decorator [Gamma95]. Intercepting filters can be considered decorators around a front controller.
Acknowledgments 
[Alur01] Alur, Crupi, and Malks. Core J2EE Patterns: Best Practices and Design Strategies. Prentice-Hall, 2001.
[Fowler03] Fowler, Martin. Patterns of Enterprise Application Architecture. Addison-Wesley, 2003.
[Gamma95] Gamma, Helm, Johnson, and Vlissides. Design Patterns: Elements of Reusable Object-Oriented Software. Addison-Wesley, 1995.
[Buschmann96] Buschmann, Frank, et al. Pattern-Oriented Software Architecture, Vol 1. Wiley & Sons, 1996.
[Schmidt00] Schmidt, et al. Pattern-Oriented Software Architecture, Vol 2. Wiley & Sons, 2000.