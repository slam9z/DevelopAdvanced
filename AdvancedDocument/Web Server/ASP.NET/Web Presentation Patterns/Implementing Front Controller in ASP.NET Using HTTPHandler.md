[Implementing Front Controller in ASP.NET Using HTTPHandler](https://msdn.microsoft.com/en-us/library/ff647590.aspx)


##Context
 
You are building a Web application in ASP.NET. You have evaluated the alternative designs described in Page Controller
 and Front Controller and have determined that there is sufficient complexity in your application to warrant implementing
 Front Controller. 

##Background 

An example is helpful to explain how to implement Front Controller in ASP.NET and the value provided by centralizing
 all control through a single controller object, as long as the example is complex enough to demonstrate the issues 
you will encounter when implementing the pattern. 

>Note: Because Page Controller is built into ASP.NET, the additional effort required to implement Front Controller 
>rather than Page Controller is very large. In fact, you must build the whole framework for Front Controller. You
> should do so only if your application warrants that amount of complexity. Otherwise, review Page Controller to
> determine whether it is sufficient. 


The following example builds on the solution described in Implementing Page Controller in ASP.NET. That solution
 describes two different pages. The pages inherit from a common base class, which is responsible for adding the
 site header to each page. The implementation is a common choice for Page Controller when you want to share b
ehavior between pages. The following is the BasePage class from the Page Controller example:
 
```cs
using System;
using System.Web.UI;
using System.Web.UI.WebControls;

public class BasePage : Page
{
   protected Label eMail;
   protected Label siteName;

   virtual protected void PageLoadEvent(object sender, System.EventArgs e)
   {}
   
   protected void Page_Load(object sender, System.EventArgs e)
   {
      if(!IsPostBack)
      {
         string name = Context.User.Identity.Name;

         eMail.Text = DatabaseGateway.RetrieveAddress(name);
         siteName.Text = "Micro-site";

         PageLoadEvent(sender, e);
      }
   }


   #region Web Form Designer generated code
   override protected void OnInit(EventArgs e)
   {
      //
      // CODEGEN: This call is required by the ASP.NET Web Form Designer.
      //
      InitializeComponent();
      base.OnInit(e);
   }
      
   /// <summary>
   /// Required method for Designer support - do not modify
   /// the contents of this method with the code editor.
   /// </summary>
   private void InitializeComponent()
   {    
      this.Load += new System.EventHandler(this.Page_Load);

   }
   #endregion
} 
```

The Page_Load function is called every time the page is being loaded. It retrieves the e-mail address from the DatabaseGateway 
class (shown in Implementing Page Controller in ASP.NET), sets some labels with the data, and then calls PageLoadEvent for
 specialized processing of each page. 

One of the criteria for choosing Front Controller instead of Page Controller is when you have excessive conditional logic in
 the base class. This example does not use conditional logic in the base class. Therefore, based on this criterion alone,
 there is no need to implement Front Controller. 


###Changing Requirements 


The previous example works very well for its intended purpose. However, it is overly simplistic and not representative of 
most Web applications. To better approximate the overall complexity of such applications, the requirements for this example 
call for different headers on the pages, depending on the URL and query parameters. 

This example creates two sites: a Micro-site and a Macro-site. Each site consults a different database to retrieve the e-mail 
address contained in the header. The pages themselves remain unchanged; only the header content is different. In this example,
 most of the implementation is the same as the previous example. The only class that must be modified is BasePage. 
 
```cs
using System;
using System.Web.UI;
using System.Web.UI.WebControls;

public class BasePage : Page
{
   protected Label eMail;
   protected Label siteName;

   virtual protected void PageLoadEvent(object sender, System.EventArgs e)
   {}
   
   protected void Page_Load(object sender, System.EventArgs e)
   {
      if(!IsPostBack)
      {
         string site = Request["site"];

         if(site != null && site.Equals("macro"))
            LoadMacroHeader();
         else
            LoadMicroHeader();

         PageLoadEvent(sender, e);
      }
   }

   private void LoadMicroHeader()
   {
      string name = Context.User.Identity.Name;
      
      eMail.Text = WebUsersDatabase.RetrieveAddress(name);            
      siteName.Text = "Micro-site";
   }


   private void LoadMacroHeader()
   {
      string name = Context.User.Identity.Name;

      eMail.Text = MacroUsersDatabase.RetrieveAddress(name);            
      siteName.Text = "Macro-site";
   }

   #region Web Form Designer generated code
   override protected void OnInit(EventArgs e)
   {
      //
      // CODEGEN: This call is required by the ASP.NET Web Form Designer.
      //
      InitializeComponent();
      base.OnInit(e);
   }
      
   /// <summary>
   /// Required method for Designer support - do not modify
   /// the contents of this method with the code editor.
   /// </summary>
   private void InitializeComponent()
   {    
      this.Load += new System.EventHandler(this.Page_Load);

   }
   #endregion
} 
```

As stated previously, the Micro-site and Macro-site each use different databases to retrieve the e-mail address that is
 contained in the header. The two methods, LoadMacroHeader and LoadMicroHeader, use different database gateway classes,
 WebUsersDatabase and MacroUsersDatabase, to retrieve the address from the database. 

The Page_Load method's responsibility has changed. In the previous example, it retrieves information from the database. 
In this implementation, it determines which function, LoadMicroHeader or LoadMacroHeader, to call and then calls the appropriate 
method. If you are going to have only two sites, this implementation is sufficient. However, the base class now contains 
conditional logic. It is up to you how comfortable you feel with that logic contained in this class. Clearly, most developers
 would flinch if they saw more than a few branches in the code, but two probably would not elicit the same response. 
The main reason for limiting the conditional logic is that it is more likely to change and cause you to modify the 
implementation. Because the entire implementation is contained in one file, the changes that you make could affect other sites.
 
##Implementation Strategy 

Front Controller is usually implemented in two parts. A Handler object receives the individual requests (HTTP Get and Post)
 from the Web server, retrieves the relevant parameters, and then selects an appropriate command, based on the parameters.
 The second part of the controller, Command Processor, performs the specific actions or commands to satisfy the request. 
When finished, the commands forward to the view so that the page can be displayed. 

>Note: This implementation strategy resolves the issues raised in the earlier example. Although this example is probably 
>not sufficient to justify the change to Front Controller, it serves to illustrate why you would use Front Controller, 
>and the implementation solves problems of this type that are of a far greater complexity. Also, as with most implementations,
> there is more than one way to implement this pattern; this is just one choice. 

###Handler 


ASP.NET provides a low-level request/response API to service incoming HTTP requests. Each incoming HTTP request that ASP.NET 
receives is ultimately processed by a specific instance of a class that implements the IHTTPHandler interface.
 This low-level API is ideal for implementing the handler portion of Front Controller. 

>Note: the Microsoft .NET Framework provides multiple implementation choices for HTTP handlers. For example, 
>in a high-volume environment, you may be able to improve response times with an asynchronous HTTP handler that 
>implements the IHttpAsyncHandler interface. This solution uses a synchronous handler for sake of simplicity. 
>For more information about the implementation of asynchronous HTTP handlers, see the Microsoft Developer Network 
>(MSDN) Web site (http://msdn.microsoft.com).

Figure 1 shows the structure of the handler portion of the controller. 

[Figure 1: Handler portion of the front controller]

This solution partitions responsibilities ideally. The Handler class handles the individual Web requests and delegates
 the responsibility of determining the correct Command object to the CommandFactory class. When the CommandFactory 
returns a Command object, the Handler calls the Execute method on the Command to perform the request. 

Handler.cs 


The following code example shows how the Handler class is implemented:
 
```cs
using System;
using System.Web;

public class Handler : IHttpHandler
{
   public void ProcessRequest(HttpContext context) 
   {
      Command command = 
         CommandFactory.Make(context.Request.Params);
      command.Execute(context);
   }

   public bool IsReusable 
   { 
      get { return true;} 
   }
}
```

Command.cs 


The Command class is an example of the Command pattern [Gamma95]. The Command pattern is useful in this situation, 
because you do not want the Handler class to depend directly on the commands. They can be returned generically from 
the CommandFactory. 
 
```
using System;
using System.Web;

public interface Command
{
   void Execute(HttpContext context);
}
```

CommandFactory.cs 


The CommandFactory class is critical to the implementation. It determines, based on parameters from the query string,
 which command will be created. In this example, if the site query parameter is set to micro or is not set at all,
 the factory creates a MicroSite command object. If site is set to macro, the factory creates a MacroSite command object.
 If the value is set to anything else, the factory returns an UnknownCommand object for default error handling. 
This is an example of the Special Case pattern [Fowler03]. 
 
```cs

using System;
using System.Collections.Specialized;


public class CommandFactory
{
   public static Command Make(NameValueCollection parms)
   {
      string siteName = parms["site"];
      
      Command command = new UnknownCommand();

      if(siteName == null || siteName.Equals("micro"))
         command = new MicroSite();
      else if(siteName.Equals("macro"))
         command = new MacroSite();
      return command;
   }
}
```

###Configuring the Handler 


HTTP handlers are declared in the ASP.NET configuration as part of a web.config file. ASP.NET defines an <httphandlers>
 configuration section where handlers can be added and removed. For example, ASP.NET maps all requests for Page*.aspx 
files to the Handler class in the application's web.config file: 
 
```aspx
<httpHandlers>
   <add verb="*" path="Page*.aspx" type="Handler,FrontController" />
</httpHandlers>
```
 
###Commands 


The commands represent the variability in the Web site. In this example, the functionality to retrieve data from the database 
for each site is contained in its own class that inherits from a base class named RedirectingCommand. The RedirectingCommand
 class implements the Command interface. When Execute is called on the RedirectingCommand class, it first calls an abstract 
method called OnExecute and, on return, transfers to the view. The specific view is retrieved from a class called UrlMap.
 The UrlMap class retrieves the map from the application's web.config file. Figure 2 shows the structure of the command 
portion of the solution. 


[Figure 2: Command portion of the front controller]

RedirectingCommand.cs 


RedirectingCommand is an abstract base class that calls an abstract method named OnExecute to perform the specific command 
and then, on return, transfers to the view that is retrieved from the UrlMap. 
 
```
using System;
using System.Web;

public abstract class RedirectingCommand : Command
{
   private UrlMap map = UrlMap.SoleInstance;

   protected abstract void OnExecute(HttpContext context);

   public void Execute(HttpContext context)
   {
      OnExecute(context);
      
      string url = String.Format("{0}?{1}",
         map.Map[context.Request.Url.AbsolutePath],
         context.Request.Url.Query);

      context.Server.Transfer(url);
   }
}
```
 
UrlMap.cs 


The UrlMap class loads configuration information from the application's web.config file. The configuration information 
associates the absolute path of the requested URL to another URL specified by the file. This allows you to change the actual 
page to which a user is forwarded when an external page is requested. This provides a great deal of flexibility when changing
 views, because the actual page is never referenced by the user. The following is the UrlMap class:
 
```cs
using System;
using System.Web;
using System.Xml;
using System.Configuration;
using System.Collections.Specialized;

public class UrlMap : IConfigurationSectionHandler 
{
   private readonly NameValueCollection _commands = new NameValueCollection();

   public const string SECTION_NAME="controller.mapping";

   public static UrlMap SoleInstance 
   {
      get {return (UrlMap) ConfigurationSettings.GetConfig(SECTION_NAME);}
   }

   object IConfigurationSectionHandler.Create(object parent,object configContext, XmlNode section) 
   {
      return (object) new UrlMap(parent,configContext, section);   
   }

   private UrlMap() {/*no-op*/}

   public UrlMap(object parent,object configContext, XmlNode section) 
   {
      try 
      {
         XmlElement entriesElement = section["entries"];
         foreach(XmlElement element in entriesElement) 
         {
            _commands.Add(element.Attributes["key"].Value,element.Attributes["url"].Value);
         }
      } 
      catch (Exception ex) 
      {
         throw new ConfigurationException("Error while parsing configuration section.",ex,section);
      }
   }   
   
   public NameValueCollection Map
   {
      get { return _commands; }
   }
```
 
The following is an excerpt from the web.config file, which shows the configuration: 
 
```xml
<controller.mapping>
   <entries>
      <entry key="/patterns/frontc/3/Page1.aspx" url="ActualPage1.aspx" />
      <entry key="/patterns/frontc/3/Page2.aspx" url="ActualPage2.aspx" />
   </entries>
</controller.mapping> 
```

MicroSite.cs 


The MicroSite class is similar to the code in LoadMicroHeader earlier in this pattern. The main difference is that you no 
longer have any access to the labels that were contained in the page. Instead, you must add the information to the HttpContext
 object. The following example shows the MicroSite code:
 
```cs
using System;
using System.Web;

public class MicroSite : RedirectingCommand
{
   protected override void OnExecute(HttpContext context)
   {
      string name = context.User.Identity.Name;

      context.Items["address"] = 
         WebUsersDatabase.RetrieveAddress(name);
      context.Items["site"] = "Micro-Site";
   }
}
```

MacroSite.cs 


The MacroSite class is similar to MicroSite except that it uses a different database gateway class, MacroUsersDatabase.
 Both classes store information in the passed-in HttpContext so that the view can retrieve it. The following example shows 
the MacroSite code:
 
```cs
using System;
using System.Web;


public class MacroSite : RedirectingCommand
{
   protected override void OnExecute(HttpContext context)
   {
      string name = context.User.Identity.Name;

      context.Items["address"] = 
         MacroUsersDatabase.RetrieveAddress(name);
      context.Items["site"] = "Macro-Site";
   }
}
```

WebUsersDatabase.cs 


The WebUsersDatabase class is responsible for retrieving the e-mail address from the "webusers" database. It is an example
 of the Table Data Gateway [Fowler03] pattern. 
 
```cs
using System;
using System.Data;
using System.Data.SqlClient;

public class WebUsersDatabase
{
   public static string RetrieveAddress(string name)
   {
      string address = null;

      String selectCmd = 
         String.Format("select * from webuser where (id = '{0}')",
         name);

      SqlConnection myConnection = 
         new SqlConnection("server=(local);database=webusers;Trusted_Connection=yes");
      SqlDataAdapter myCommand = new SqlDataAdapter(selectCmd, myConnection);

      DataSet ds = new DataSet();
      myCommand.Fill(ds,"webuser");
      if(ds.Tables["webuser"].Rows.Count == 1)
      {
         DataRow row = ds.Tables["webuser"].Rows[0];
         address = row["address"].ToString();
      }

      return address;
   }

```
 
MacroUsersDatabase.cs 


The MacroUsersDatabase class is responsible for retrieving the e-mail address from the "macrousers" database. It is an 
example of the Table Data Gateway pattern. 
 
```cs
using System;
using System.Data;
using System.Data.SqlClient;

public class MacroUsersDatabase
{
   public static string RetrieveAddress(string name)
   {
      string address = null;

      String selectCmd = 
         String.Format("select * from customer where (id = '{0}')",
         name);

      SqlConnection myConnection = 
         new SqlConnection("server=(local);database=macrousers;Trusted_Connection=yes");
      SqlDataAdapter myCommand = new SqlDataAdapter(selectCmd, myConnection);

      DataSet ds = new DataSet();
      myCommand.Fill(ds,"customer");
      if(ds.Tables["customer"].Rows.Count == 1)
      {
         DataRow row = ds.Tables["customer"].Rows[0];
         address = row["email"].ToString();
      }

      return address;
   }

}
```

###Views 


The last aspect of the implementation is the views. The views from the example in "Changing Requirements" were responsible for 
retrieving information from the database depending on which site the user is accessing and then displaying the rendered page
 to the user. Because the database access code has been moved to the command, the views now retrieve the data from the HttpContext
 object. Figure 3 shows the structure of the code-behind classes. 

[Figure 3: Structure of the code-behind classes of the view]

There is still common behavior, so the BasePage class is still needed to avoid code duplication. 
BasePage.cs 


The BasePage class has changed dramatically from the example in "Changing Requirements.". It is no longer responsible for
 determining which site header to load. It simply retrieves the data that the commands stored in the HttpContext object
 and assigns them to the appropriate label: 
 
```cs
using System;
using System.Web.UI;
using System.Web.UI.WebControls;

public class BasePage : Page
{
   protected Label eMail;
   protected Label siteName;

   virtual protected void PageLoadEvent(object sender, System.EventArgs e)
   {}
   
   protected void Page_Load(object sender, System.EventArgs e)
   {
      if(!IsPostBack)
      {
         eMail.Text = (string)Context.Items["address"];
         siteName.Text = (string)Context.Items["site"];
         PageLoadEvent(sender, e);
      }
   }

   #region Web Form Designer generated code
   #endregion
}
```

ActualPage1.aspx.cs and ActualPage2.aspx 


ActualPage1 and ActualPage2 are the page-specific code-behind classes. They both inherit from BasePage to ensure that the header
 is filled in at the top of the screen: 
 
```cs
using System;
using System.Web.UI;
using System.Web.UI.WebControls;

public class ActualPage1 : BasePage
{
   protected System.Web.UI.WebControls.Label pageNumber;

   protected override void PageLoadEvent(object sender, System.EventArgs e)
   {
      pageNumber.Text = "1";
   }

   #region Web Form Designer generated code
   #endregion
}

using System;
using System.Web.UI.WebControls;

public class ActualPage2 : BasePage
{
   protected Label pageNumber;

   protected override void PageLoadEvent(object sender, System.EventArgs e)
   {
      pageNumber.Text = "2";
   }
  
  #region Web Form Designer generated code
   #endregion
}
```
 
These pages do not have to change when moving from the Page Controller implementation to the Front Controller implementation.
Testing Considerations 
The dependence of the implementation on the ASP.NET runtime makes testing more difficult. It is not possible to instantiate 
classes that inherit from System.Web.UI.Page, System.Web.UI.IHTTPHandler or the other various classes contained in the ASP.NET
 runtime. This makes unit testing of most of the individual pieces of the application impossible. The chosen way to test this
 implementation automatically is to generate HTTP requests and then retrieve the HTTP response and determine if the response 
is correct. This approach is error-prone because you are comparing the text of the response with expected text. 

CommandFixture.cs 


One aspect of the implementation that can be tested is the CommandFactory, because it is not dependent on the ASP.NET runtime.
 Therefore, you can write tests to verify that you get the correct Command object in return. The following are NUnit 
(http://nunit.org) tests for the CommandFactory class: 
 
```cs
using System;
using System.Collections.Specialized;
using NUnit.Framework;

[TestFixture]
public class CommandFixture
{
   private static readonly string microKey = "micro";
   private static readonly string macroKey = "macro";

   [SetUp]
   public void BuildCommandFactory()
   {
      NameValueCollection map = new NameValueCollection();
      map.Add(microKey, "MicroSite");
      map.Add(macroKey, "MacroSite");
   }

   [Test]
   public void DefaultToMicro()
   {
      NameValueCollection map = new NameValueCollection();
      Command command = CommandFactory.Make(map);
      Assertion.AssertNotNull(command);
      Assertion.Assert(command is MicroSite);
   }

   [Test]
   public void MicroSiteCommand()
   {
      NameValueCollection map = new NameValueCollection();
      map.Add("site", "micro");
      Command command = CommandFactory.Make(map);
      Assertion.AssertNotNull(command);
      Assertion.Assert(command is MicroSite);
   }

   [Test]
   public void MacroSiteCommand()
   {
      NameValueCollection map = new NameValueCollection();
      map.Add("site", "macro");
      Command command = CommandFactory.Make(map);
      Assertion.AssertNotNull(command);
      Assertion.Assert(command is MacroSite);
   }


   [Test]
   public void Error()
   {
      NameValueCollection map = new NameValueCollection();
      map.Add("site", "xyzcommand");
      Command command = CommandFactory.Make(map);
      Assertion.AssertNotNull(command);
      Assertion.Assert(command is UnknownCommand);
   }
}
```
 
Further work could isolate the Command class. The Execute method has a parameter that is an HttpContext object. 
You could change this parameter to make the object independent of the ASP.NET environment. This would enable you to
 unit-test the commands outside of the ASP.NET runtime. 

##Resulting Context 

The additional complexity of implementing Front Controller results in a number of benefits and liabilities:

###Benefits 

* Increased flexibility. This implementation demonstrates how to centralize and coordinate all requests through
 the Handler class. The Handler uses the CommandFactory to determine the specific action to perform. This allows 
the functionality to be modified and extended without changing the Handler class. For example, to add another site
, a specific command would have to be created and the only class that would have to change is CommandFactory. 

* Simplified views. The views in the Page Controller example retrieve data from the database and then render the pages.
 In Front Controller, they no longer depend on the database, because that work is accomplished by the individual commands. 

* Open for extension, but closed to modification. The implementation provides many opportunities for polymorphic 
dispatching. For example, the Handler simply calls the Execute method on the Command object, independent of what 
the method and object are doing. Therefore, you can add additional commands without modifying the Handler. 
The implementation could be extended further by replacing the CommandFactory with a different factory for further extension.
 
* URL mapping. The UrlMap allows the actual page names to be hidden from the user. The user enters a URL, which is 
mapped to the specific URL using the web.config file. This increases the flexibility for programmers because there 
is a level of indirection that is not present in the Page Controller implementation. 

* Thread-safety. The individual command objects, MicroSite and MacroSite, are created for each request. This means that 
you do not have to worry about thread safety in these objects. 

###Liabilities 

* Decreased performance. This possibility must be examined. All requests are processed through the Handler object. It
 uses the CommandFactory to determine which command to create. Although in this case they do not have performance problems,
 both of these classes should be examined carefully for any potential performance issues. 

* Cruel and unusual punishment. This implementation is a lot more complicated than Page Controller. This implementation 
does provide more options, but at the cost of complexity and a lot of classes. You must weigh whether or not it is worth 
it. After you have taken the leap and built the framework, it is easy to add new commands and views. However, due to the 
implementation of Page Controller in ASP.NET, you would not expect to see as many implementations of Front Controller as 
you would in other platforms. 

* Testing considerations. Because Front Controller is implemented in ASP.NET, it is difficult to test in isolation. To 
improve testability, you should separate functionality out of the ASP.NET -specific code into classes that do not depend 
on ASP.NET. You can then test these classes without having to start the ASP.NET runtime. 

* Invalid URLs. Because Front Controller determines which view to transfer to, based on input parameters and often the
 current state of the application, the URLs may not always forward to the same page. This precludes users from saving 
URLs to access the page at a later time. 

##Related Patterns 

For more information, see the following related patterns:

Template Method [Gamma95]. The PageLoadEvent method of the BasePage class is an example implementation of Template Method.
 
Intercepting Filter.

Page Controller.

Command [Gamma95].

Factory.The factories described earlier in this pattern combine elements from both Factory 
Method [Gamma95] and Abstract Factory [Gamma95].

##Acknowledgments 

[Fowler03] Fowler, Martin. Patterns of Enterprise Application Architecture. Addison-Wesley, 2003.
[Gamma95] Gamma, Helm, Johnson, and Vlissides. Design Patterns: Elements of Reusable Object-Oriented Software. Addison-Wesley, 1995.