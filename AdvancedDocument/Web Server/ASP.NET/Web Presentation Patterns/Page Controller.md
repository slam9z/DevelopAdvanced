﻿[Page Controller](https://msdn.microsoft.com/en-us/library/ff649595.aspx)

##Context 

You decided to use the Model-View-Controller (MVC) pattern to separate the user interface components of your dynamic Web application
 from the business logic. The application you are building constructs the Web pages dynamically, but the navigation between the 
pages is mostly static.


##Problem 

How do you best structure the controller for moderately complex Web applications so that you can achieve reuse and 
flexibility while avoiding code duplication?

##Forces 

The following forces act on a system within this context and must be reconciled[使和解] as you consider a solution to the problem:

* The MVC pattern often focuses primarily on the separation between the model and the view, while paying less attention to
 the controller. In many rich-client scenarios, the separation between controller and view is less critical and is often 
omitted [Fowler03]. In a thin-client application, however, view and controller are inherently separated because the 
presentation occurs in the client browser, whereas the controller is part of the server-side application. The controller, 
therefore, warrants[保证] a closer examination[检验].

* In dynamic Web applications, multiple user actions can lead to different controller logic, followed by the same page 
presentation. For example, in a simple Web-based e-mail application, both sending a message and deleting a message from 
the inbox is likely to return the user to the (refreshed) inbox page. Although the same page is rendered after either 
activity, the application must perform a different action, based on the previous page and the button the user clicked.

* The code that renders most dynamic Web pages involves very similar steps: verifying user authentication, extracting 
the page parameters from the query string or the form fields, gathering session information, retrieving data from a data 
source, rendering the dynamic portion of the page, and adding applicable headers and footers. This can lead to a significant 
amount of code duplication.

* Scripted server pages (such as ASP.NET) may be easy to create, but can introduce a number of disadvantages as the 
application grows. Scripted pages provide poor separation between controller and the view, which reduces the opportunities 
for reuse. For example, if multiple actions lead to the same page, it is difficult to reuse the display code across multiple
 controllers, because it is intertwined with the controller code. Scripted server pages that intersperse business logic
 and presentation logic are also more difficult to test and debug. Finally, developing scripted server pages requires 
expertise both in developing business logic and in rendering visually appealing and efficient HTML pages; these two skill
 sets are rarely possessed by a single person. Due to these considerations, it makes sense to minimize the scripted 
server-page code and develop business logic in actual classes.

* As described in the MVC pattern, testing user interface code tends to be time-consuming and tedious[冗长的]. If you can separate
 user-interface-specific code from the actual business logic, testing the business logic becomes simpler and more repeatable.
 This is true not only for the presentation portion, but also for the controller part of an application.

* Common appearance and navigation tend to improve usability and brand recognition[识别] of a Web application. However, common 
appearance can lead to repetitive presentation code, especially if the code is embedded inside scripted server pages. 
Therefore, you need a mechanism for improving reuse of presentation logic across pages.


##Solution 

Use the Page Controller pattern to accept input from the page request, invoke the requested actions on the model, 
and determine the correct view to use for the resulting page. Separate the dispatching logic from any view-related code.
 Where appropriate, create a common base class for all page controllers to avoid code duplication and increase consistency 
and testability. Figure 1 shows how the page controller relates to the model and view.

[Figure 1: Page Controller structure]

The page controller receives a page request, extracts any relevant data, invokes any updates to the model, and forwards the
 request to the view. The view in turn depends on the model for retrieval of data to be displayed. Defining a separate page 
controller isolates the model from the specifics of the Web request - for example, session management, or the use of query
 strings or hidden form fields to pass parameters to the page. In this basic form, you create a controller for every link 
in the Web application. This keeps the controllers simple, because you only have to concern yourself with one action at a time. 


Creating a separate controller for each Web page (or action) can lead to significant code duplication. Therefore, you should
 create a BaseController class to incorporate common functions such as validating parameters (see Figure 2). Each individual 
page controller can inherit this common functionality from BaseController. In addition to inheriting from a common base class,
 you can also define a set of helper classes that the controllers can call to perform common functions.

[Figure 2: Using BaseController to eliminate code duplication]


This approach works well if most pages are similar and you can pull the common functions into a single base class. 
The more page variations you have, the more levels you may have to inject into the inheritance tree. Let's say that all 
pages parse parameters, but only pages that display lists retrieve data from the database, while pages that require data 
entry update the model, rather than retrieve data. You could now introduce two new base classes, ListController and 
DataEntryController, that both inherit from BaseController. The list pages could then inherit from ListController, 
and the data entry pages could inherit from DataEntryController. Although this approach may work well in this simple 
example, the inheritance tree can get rather deep and complicated if you are dealing with a real-life businesses application.
 You may be tempted to add conditional logic into the base classes to accommodate some of the variants, but doing so 
violates the principles of encapsulation and makes the base classes a notorious bottleneck for any changes to the system.
 Therefore, you should consider using helper classes or the Front Controller pattern as your application becomes more complex.


Using a page controller for a Web application is such a common need that most Web application frameworks provide a 
default implementation of the page controller. Most frameworks incorporate the page controller in the form of a server
 page (for example, ASP, JSP, and PHP). Server pages actually combine the functions of view and controller and do not 
provide the desired separation between the presentation code and the controller code. Unfortunately, some of the 
frameworks make it very easy to blend together view-related code with controller-related code and make it difficult
 to properly separate the controller logic. As a result, the Page Controller approach has developed a bad reputation 
with many developers. Now, many developers associate Page Controller with bad design and Front Controller with good design.
 This perception, in fact, resulted from a specific (faulty) implementation choice; both Page Controller and the Front
 Controller are perfectly viable architectural choices.


Therefore, it is preferable to separate the controller logic into separate classes that can be called from the server page.
 The ASP.NET page framework provides an excellent mechanism for achieving this separation, called code-behind classes
. (See Implementing Page Controller in ASP.NET). 


##Variants 


In most cases, the page controller is dependent on the specifics of an HTTP-based Web request. As a result, the page 
controllercode usually contains references to HTTP headers, query strings, form fields, multipart form requests, and so forth. 
This makes it very hard to test the controller code outside the Web application framework. The only option is to test the 
controller by simulating HTTP requests and parsing the results. This type of testing is both time-consuming and error prone.
 Therefore, to improve testability you could separate the Web-dependent and the Web-independent code into two separate 
classes (see Figure 3). 

[Figure 3: Separating the Web-dependent and Web-independent code]

In this example, AspNetController encapsulates all dependencies on the application framework (ASP.NET). For example, 
it can extract all incoming parameters from the Web request and pass it to BaseController in a way that is independent 
from the Web interface (for example, in a collection). This approach not only improves testability, but enables you 
to reuse the controller code with other user interfaces, for example a rich-client interface or a custom scripting language.


The downside of this approach is the additional overhead. You now have an additional class, and each request has to
 go through a translation before it can be serviced. Therefore, you should keep the environment-specific part of the
 controller as thin as possible and consider the tradeoffs between reduced dependencies and more efficient development and execution.

##Example 

See Implementing Page Controller in ASP.NET.

##Resulting Context 

Using the Page Controller pattern results in a number of benefits and liabilities.

###Benefits 

* Simplicity[质朴]. Because each dynamic Web page is handled by a specific controller, the controllers have to deal with
 only a limited scope and can remain simple. Because each page controller deals with only a single page, Page Controller
 is particularly well-suited for Web applications with simple navigation.

* Built-in framework features. In its most basic form, the controller is already built into most Web application platforms.
 For example, if the user clicks a link in a Web page that leads to a dynamic page generated by an ASP.NET script, the 
Web server analyzes the URL associated with the link and executes the associated ASP.NET page. In effect, the ASP.NET 
page is the controller for the action taken by the user. The ASP.NET page framework also provides code-behind classes 
to execute controller code. Code-behind classes provide better separation between the controller and the view and also 
allow you to create a controller base class that incorporates common functionality across all controllers. For an example,
 see Implementing Page Controller in ASP.NET.

* Increased reuse. Creating a controller base class reduces code duplication and enables you to reuse common code across
 page controllers. You can reuse code by implementing recurring logic in the base class. This logic is then automatically
 inherited by all concrete Page Controller objects. If the implementation of the logic varies from page to page, you can 
still use Template Method and implement the basic execution structure in the base class; the implementation of specific 
substeps may vary from page to page.

* Expandability[扩展性]. You can expand a page controller quite easily by using helper classes. If the logic inside the controller 
becomes too complex, you can delegate some of the logic to helper classes. Helper classes also provide another mechanism 
for reuse, besides inheritance.

* Separation of developer responsibilities. Using a Page Controller class helps separate responsibilities among members
 of the development team. The developer of the controller must be familiar with the domain model and the business logic 
implemented by the application. The designer of the view, on the other hand, can focus on the presentation style of the results.

###Liabilities 

Due to its simplicity, Page Controller is the default implementation for most dynamic Web applications. However, 
you should be aware of the following limitations:

* One controller per page. The key constraint of Page Controller is that you create one controller for each Web page.
 This works well for applications with a static set of pages and a simple navigation path. Some more complex applications
require dynamic configuration of pages and navigation maps between them. Spreading this logic across many page controllers
 would make the application hard to maintain, even if some of the logic could be pulled into the base controller.
 In addition, the built-in features of the Web framework may reduce the amount of flexibility you have in naming URL
s and resource paths (even though you can compensate for some of this with low-level mechanisms like ISAPI filters). 
In these scenarios, consider using Front Controller that intercepts all Web requests and forwards the request to the 
appropriate handler, based on configurable rules.

* Deep inheritance trees. Inheritance seems to be one of the most loved and most hated features of object-oriented 
programming. Using inheritance alone to reuse common functionality may lead to inflexible[僵化的] inheritance hierarchies. 
For more detail, see Implementing Page Controller in ASP.NET.

* Dependency on the Web framework. In the basic form, the page controller still depends on the Web application 
environment and cannot be tested independently. You can use a wrapper mechanism to decouple the Web-dependent 
part, but doing so requires an additional level of indirection.

##Testing Considerations 

Because Page Controller is dependent on specifics of the Web application framework (for example, query strings and HTTP headers), 
you cannot instantiate and test the controller classes outside the Web framework. If you want to run a suite of automated unit 
tests on the controller class, you would have to start the Web application server for each test case. You would then have to 
submit HTTP requests in a format that executes the desired function. This configuration introduces many dependencies and side 
effects into the test. To improve testability, consider separating the business logic (including controller logic as it becomes 
more complex) from the Web-dependent code.

##Related Patterns 

For more information, see the following related patterns:

Intercepting Filter. This pattern is another construct to implement recurring functionality inside a Web application.
 The Web server framework can pass each request through a configurable chain of filters before passing it to the controller.
 Filters tend to deal with lower-level functions such as decoding, authentication, and session management, whereas
 Page Controller deals with application functionality. Filters also are not usually page-specific.

Front Controller. This pattern is a more complex, but also more powerful alternative to Page Controller. Front Controller
 defines a single controller for all page requests, which enables it to make navigational decisions that span multiple pages.
 
Model-View-Controller. Page Controller is an implementation variant of the controller portion of MVC.

##Acknowledgments 

[Fowler03] Fowler, Martin. Patterns of Enterprise Application Architecture. Addison-Wesley, 2003.

[Gamma95] Gamma, Helm, Johnson, and Vlissides. Design Patterns: Element of Reusable Object-Oriented Software. Addison-Wesley, 1995.