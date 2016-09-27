[Front Controller](https://msdn.microsoft.com/en-us/library/ff648617.aspx)

##Context 

You have decided to use the Model-View-Controller(MVC) pattern to separate the user interface logic from the business logic 
of your dynamic Web application. You have reviewed the Page Controller pattern, but your page controller classes have
 complicated logic, are part of a deep inheritance hierarchy, or your application determines the navigation between pages 
dynamically based on configurable rules.

##Problem 

How do you best structure the controller for very complex Web applications so that you can achieve reuse and flexibility while avoiding code duplication? 

##Forces 

The following are specific aspects of the forces from Model-View-Controller that apply to the Front Controller pattern. 

* If common logic is replicated in different views in the system, you need to centralize[集权控制] this logic to reduce the amount 
of code duplication. Removing the duplicated code is critical to improving the overall maintainability[可维修性] of the system.

* The retrieval[取回] of data is also best handled in one location. A series of views that use the same data from the database 
is a good example. It is better to implement the retrieval of this data in one place as opposed to having each view retrieve
 the data and duplicate the database access code. 

* As described in MVC, testing user interface code tends to be time-consuming and tedious. Separating the individual roles
 enhances overall testability. This is true not only for the model code, which was described in MVC, but also applies
 to the controller code. 

The following forces might persuade[说服] you to use Front Controller as opposed to Page Controller: 

* A common implementation of Page Controller involves creating a base class for behavior shared among individual pages.
 However, over time these base classes can grow with code that is not common to all pages. It requires discipline to 
periodically refactor this base class to ensure that only common behavior is included. For example, you do not want a 
page to examine a request and decide (based on request parameters) to transfer control to a different page, because 
this type of decision is more specific to a particular function, rather than common among all the pages. 

* To avoid adding excessive[过分的] conditional logic in the base class, you could create a deeper inheritance hierarchy to 
remove the conditional logic. For example, in an application that has three functional areas, it might be useful to 
have a single base class that has common functionality for the application. There might also be another class for each
 functional area, which inherits from the overall application base class. This type of structure, at first glance,
 is straightforward, but often leads to a very brittle design and implementation, and to a morass of code. 

* The Page Controller solution describes a single object per logical page. This solution breaks down when you need to 
control or coordinate processing across multiple pages. For example, suppose that you have complex configurable navigation,
 which is stored in XML, in your Web application. When a request comes in, the application must look up where to go next,
 based on its current state. 

* Because Page Controller is implemented with a single object per logical page, it is difficult to consistently apply a 
particular action across all the pages in a Web application. Security, for example, is best implemented in a coordinated
 fashion. Having security handled by each view or page controller object is problematic because it can be inconsistently
 applied and can lead to security breaches. An additional solution to this problem is also discussed in Intercepting Filter.
 
* The association of the URL to the particular controller object can be constraining for Web applications. For example, 
suppose your site has a wizard-like interface for gathering information. This wizard consists of a number of mandatory 
pages and a number of optional pages based on user input. When implemented with Page Controller, the optional pages would
 have to be implemented with conditional logic in the base class to select the next page. 

##Solution
 
Front Controller solves the decentralization problem present in Page Controller by channeling all requests through a 
single controller. The controller itself is usually implemented in two parts: a handler and a hierarchy of commands (see Figure 1). 

[Figure 1: Front Controller structure]

The handler has two responsibilities: 

* Retrieve parameters. The handler receives the HTTP Post or Get request from the Web server and retrieves relevant
 parameters from the request. 

* Select commands. The handler uses the parameters from the request first to choose the correct command and then to 
transfers control to the command for processing. 

Figure 2 shows these two responsibilities. 

[Figure 2: Front Controller, typical scenario]


The commands themselves are also part of the controller. The commands represent the specific actions as described
 in the Command pattern [Gamma95]. Representing commands as individual objects allows the controller to interact with 
all commands in a generic way, as opposed to invoking specific methods on a common command class. After the command 
object completes the action, the command chooses which view to use to render the page. 

##Example 

See Implementing Front Controller in ASP.NET Using HTTPHandler.

##Resulting Context

Using the Front Controller pattern results in the following benefits and liabilities:

###Benefits 

* Centralized[集中] control. Front Controller coordinates all of the requests that are made to the Web application.
 The solution describes using a single controller instead of the distributed model used in Page Controller. This 
single controller is in the perfect location to enforce application-wide policies, such as security and usage tracking. 

* Thread-safety. Because each request involves creating a new command object, the command objects themselves do 
not need to be thread safe. This means that you avoid the issues of thread safety in the command classes. This does
 not mean that you can avoid threading issues altogether, though, because the code that the commands act upon, the 
model code, still must be thread safe [Fowler03].

* Configurability. Only one front controller needs to be configured into the Web server; the handler does the rest of
 the dispatching. This simplifies the configuration of the Web server. Some Web servers are awkward to configure.
 Using dynamic commands enables you to add new commands without changing anything [Fowler03]. 

###Liabilities 

* Performance considerations. Front Controller is a single controller that handles all requests for the Web application. 
Of the two parts, the handler should be examined closely for performance problems, because the handler determines the type
 of command that performs the request. If the handler must perform a database query or a query of an XML document to make 
the decision, performance could be very slow as a result.

* Increased complexity. Front Controller is more complicated than Page Controller. It often involves replacing the built-in
 controller with a custom built Front Controller. Implementing this solution increases the maintenance costs and the time i
t takes for developers to orient themselves to the solution. 

##Testing Considerations 

Removing business logic from the views simplifies the testing of the views, because you can then test the views independent
 from the controller. As described in the Page Controller pattern, testing the controller may be hindered by the fact that 
the controller contains code that makes it dependent on the HTTP run-time environment. This dependency may be resolved by 
using a two-stage Web handler as described in Martin Fowler's book, Patterns for Enterprise Application Architecture [Fowler03], 
and in the Page Controller pattern. The controller is separated into two parts: a Web handler and a dispatcher. The Web handler
 retrieves data from the Web request and passes it to the dispatcher in a way that the dispatcher does not depend on the Web
 server framework (for example, in a generic collection object). This allows for the dispatcher to be tested without the Web
 server framework being present.

##Related Patterns 

For more information, see the following related patterns:

Intercepting Filter. This pattern describes another way to implement recurring functionality inside a Web application. 
Intercepting Filter works by passing each request through a configurable chain of filters prior to passing control over 
to the controller. Filters tend to deal with lower-level functions such as decoding, authorization, authentication, and 
session management whereas Front Controller and Page Controller deal with application-level functionality. Another aspect 
of filter is that they are usually stateless. For example, when a user gets to authorization, the Web server has to 
authenticate the session. If the user is authenticated the process continues on. If not, the user is redirected elsewhere.
 One advantage of Intercepting Filter is that, in most implementations, the pages themselves do not have to be modified 
to add additional functionality. 

Page Controller. This pattern is a simpler alternative to Front Controller. Page Controller has a single controller object
 per page as opposed to the single object for all requests. Page Controller is a more appropriate starting point for most
 applications. Only when the need arises should you turn to Front Controller.

##Acknowledgments 

[Alur01] Alur, Crupi, and Marks. Core J2EE Patterns: Best Practices and Design Strategies. Prentice Hall, 2001.
[Fowler03] Fowler, Martin. Patterns of Enterprise Application Architecture. Addison-Wesley, 2003.
[Gamma95] Gamma, Helm, Johnson, and Vlissides. Design Patterns: Elements of Reusable Object-Oriented Software. Addison-Wesley, 1995.


