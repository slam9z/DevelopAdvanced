[Implementing Intercepting Filter in ASP.NET Using HTTP Module](https://msdn.microsoft.com/en-us/library/ff649096.aspx)


Context 
You are building a Web application in Microsoft ASP.NET with many different types of requests. Some requests are forwarded to the appropriate page, and others must be logged or modified in some way before being processed.
Implementation Strategy 
The ASP.NET implementation of the Intercepting Filter pattern is an example of the event-driven filters described in the pattern. ASP.NET provides a series of events during request processing that your application can hook into. These events guarantee the state of the request. Individual filters are implemented with an HTTP module. An HTTP module is a class that implements the IHttpModule interface and determines when the filter should be called. ASP.NET includes a set of HTTP modules that can be used by your application. For example, SessionStateModule is provided by ASP.NET to supply session state services to an application. You can create your own custom HTTP modules to filter the request or response as needed by your application. 
The general process for writing a custom HTTP module is: 
Implement the IHttpModule interface. 
Handle the Init method and register for the events you need. 
Handle the events. 
Optionally, implement the Dispose method if you have to do cleanup. 
Register the module in the web.config file. 
Events 


The following lists show the events that, when raised during the processing of a request, can be intercepted using ASP.NET. All events are listed in the order in which they occur. 

The first list shows the events that are raised before the request is processed: 
BeginRequest. This event signals a new request; it is guaranteed to be raised on each request.
AuthenticateRequest. This event signals that the configured authentication mechanism has authenticated the request. Attaching to this event guarantees your filter that the request has been authenticated.
AuthorizeRequest. Like AuthenticateRequest, this event signals that the request is now one step further down the chain and has been authorized. 
ResolveRequestCache. The output cache module uses this event to short-circuit the processing of requests that have been cached.
AcquireRequestState. This event signals that individual request state should be obtained.
PreRequestHandlerExecute. This event signals that the request handler is about to execute. This is the last event you can participate in before the HTTP handler for this request is called.

The next list shows the events that are raised after the request is processed. The events are listed in the order in which they occur:
PostRequestHandlerExecute. This event signals that the HTTP handler has finished processing the request.
ReleaseRequestState. This event signals that the request state should be stored because the application is finished with the request.
UpdateRequestCache. This event signals that code processing is complete and the file is ready to be added to the ASP.NET cache.
EndRequest. This event signals that all processing has finished for the request. This is the last event called when the application ends.

In addition, the following three per-request events can fire in a nondeterministic order:
PreSendRequestHeaders. This event signals that HTTP headers are about to be sent to the client. This provides an opportunity to add, remove, or modify the headers before they are sent.
PreSendRequestContent. This event signals that content is about to be sent to the client. This provides an opportunity to modify the content before it is sent.
Error. This event signals an unhandled exception.

The following example demonstrates how a request is intercepted after it has been authenticated by the ASP.NET runtime. When the example module, called UserLogger, is initialized, it connects a member function, called OnAuthenticate, to the AuthenticateRequest event. Every time a new request is authenticated, the OnAuthenticate function is called. In this example, the OnAuthenticate function logs the name of the authenticated user to the Intercepting Filter Pattern application event log. 
 

using System;
using System.Web;
using System.Security.Principal;
using System.Diagnostics;

public class UserLogModule : IHttpModule
{
   private HttpApplication httpApp;

   public void Init(HttpApplication httpApp)
   {
      this.httpApp = httpApp;
      httpApp.AuthenticateRequest += new EventHandler(OnAuthentication);
   }

   void OnAuthentication(object sender, EventArgs a)
   {
      HttpApplication application = (HttpApplication)sender;
      HttpResponse response = application.Context.Response;

      WindowsIdentity identity = 
         (WindowsIdentity)application.Context.User.Identity;

      LogUser(identity.Name);
   }

   private void LogUser(String name)
   {
      EventLog log = new EventLog();
      log.Source = "Intercepting Filter Pattern";
      log.WriteEntry(name,EventLogEntryType.Information);
   }

   public void Dispose()
   {}
}
 
The example module must be added to the web.config file so that the ASP.NET runtime recognizes the module. The following is the configuration file that changes for the UserLogModule example module:
 

<httpModules>
      <add name="UserLogModule" type="UserLogModule, ifilter" />
</httpModules>
 
Examples 
The following are examples of intercepting filters that are built into Microsoft .NET:
DefaultAuthenticationModule. This filter ensures that an Authentication object is present in the HttpContext object. 
FileAuthorizationModule. This filter verifies that the remote user has Microsoft Windows NT permissions to access the file requested. 
FormsAuthenticationModule. This filter enables ASP.NET applications to use forms authentication.
PassportAuthenticationModule. This filter provides a wrapper around PassportAuthentication services for Passport authentication. 
SessionStateModule. This filter provides session-state services for an application.
UrlAuthorizationModule. This filter provides URL-based authorization services for allowing or denying access to specified URLs.
WindowsAuthenticationModule. This filter enables ASP.NET applications to use Microsoft Windows or Internet Information Services (IIS) authentication.
Testing Considerations 
Testing the HTTP modules without the ASP.NET runtime is not possible. Therefore, a slightly different implementation strategy must be employed to separate as much of the functionality as possible from the class that implements the IHttpModule interface. In the previous example, the code that logs the user name does not require the ASP.NET runtime. This functionality can be placed in its own class, called UserLog, which is independent of ASP.NET. The UserLogAdapter class, which implements the IHttpModule interface, can use the UserLog class. This enables other classes to use the UserLog class and also enables you to test it without the ASP.NET environment. The following is the same functionality as described previously, but implemented in a way that allows the logging functionality to be tested without the ASP.NET runtime: 
 

using System;
using System.Diagnostics;

public class UserLog
{
   public static void Write(String name)
   {
      EventLog log = new EventLog();
      log.Source = "Intercepting Filter Pattern";
      log.WriteEntry(name,EventLogEntryType.Information);
   }
}

using System;
using System.Web;
using System.Security.Principal;

public class UserLogAdapter
{
   private HttpApplication httpApp;

   public void Init(HttpApplication httpApp)
   {
      this.httpApp = httpApp;
      httpApp.AuthenticateRequest += new EventHandler(OnAuthentication);
   }

   void OnAuthentication(object sender, EventArgs a)
   {
      HttpApplication application = (HttpApplication)sender;
      HttpResponse response = application.Context.Response;

      WindowsIdentity identity = 
         (WindowsIdentity)application.Context.User.Identity;

      UserLog.Write(identity.Name);
   }

   public void Dispose()
   {}
}
 
Resulting Context 
The implementation of the Intercepting Filter pattern results in the following benefits and liabilities:
Benefits 

Uses event-driven filters. The ASP.NET runtime provides numerous events, which enable the programmer to hook into the right place to add their functionality. This is beneficial, because they can assume the current state of the request based on the event. For example, if the event is AuthenticateRequest, you can assume that the request is authenticated prior to your filter being called. 
Enables flexible configuration. The modules are added or removed by editing the web.config file. The source code does not have to be changed, and the ASP.NET runtime does not have to be restarted. 
Alleviates order dependency. One of the liabilities of Intercepting Filter is that filters should not be order-dependent. Because the ASP.NET implementation uses events, it alleviates the problem by using events to indicate that certain processing has occurred. 
Liabilities 


Testing of classes that implement the IHttpModule interface is difficult or impossible without testing the full ASP.NET runtime. 
Related Patterns 
For more information, see Adapter [Gamma95]. The Adapter pattern was used in "Testing Considerations" to help isolate the core functionality and to enhance testability. 
Acknowledgments 
[Gamma95] Gamma, Helm, Johnson, and Vlissides. Design Patterns: Elements of Reusable Object-Oriented Software. Addison-Wesley, 1995.