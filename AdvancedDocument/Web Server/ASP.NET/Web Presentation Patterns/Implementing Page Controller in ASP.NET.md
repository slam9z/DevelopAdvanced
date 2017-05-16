[Implementing Page Controller in ASP.NET](https://msdn.microsoft.com/en-us/library/ff649415.aspx)

##Context 

You are building a Web application in ASP.NET and you want to take advantage of the event-driven nature of ASP.NET by
 using the built-in page controller. 

##Implementation Strategy 

The concepts described in the Page Controller pattern are implemented in ASP.NET by default. The ASP.NET page framework 
implements these concepts in such a way that the underlying mechanism of capturing an event on the client, transmitting 
it to the server, and calling the appropriate method is automatic and invisible to the implementer. The page controller
 is extensible in that it exposes various events at specific points in the life cycle (see "Page Life Cycle," later in 
this pattern) so that application-specific actions can be run when they are appropriate. 

For example, assume the user is interacting with a Web Forms page that contains one button server control 
(see "Simple Page Example," later in this pattern). When the user clicks the button control, an event is transmitted 
as an HTTP post to the server, where the ASP.NET page framework interprets the posted information and associates the 
raised event with an appropriate event handler. The framework automatically calls the appropriate event handler for 
the button as part of the framework's normal processing. As a result, you no longer need to implement this functionality. 
Furthermore, you can use the built-in controller, or you can replace the built-in controller with you own customized 
controller (see Front Controller). 

###Page Life Cycle 


The following list contains the most common stages of the page life cycle in the order in which they occur. It also includes 
the specific events that are raised and some typical actions that could be performed at the various stages in the processing
 of the request: 

* ASP.NET page framework initialization (Event: Init). This is the first step in the life cycle, which initializes the ASP.NET
 runtime for the request. 

* User code initialization (Event: Load). You should perform common tasks specific to your application, such as opening database
 connections, when the page controller raises the Load event. You can assume that when the Load event is raised, server controls 
are created and initialized, state has been restored, and form controls reflect client-side changes. [Reilly02]

* Application-specific event handling. At this stage, you should perform processing specific to your application in response to
 the events raised by the controller. 

* Cleanup (Event: Unload). The page has finished rendering and is ready to be discarded. You should close any database connections
 that the Load event opened and discard any objects that are no longer needed. The Microsoft.NET Framework closes database
 connections automatically, after the connection object is garbage collected. However, you do not have any control over when 
the garbage collection occurs. Therefore, it is good practice to close database connections explicitly to make efficient use
 of the database connection pool.


Note: There are several more stages of page processing than are listed here. However, they are not used for most page 
processing scenarios. 

###Simple Page Example 


The first example is a simple page that takes input from the user and then displays the input on the screen. 
The example illustrates the event-driven model that ASP.NET uses to implement server controls. 


[Figure 1: Simple page]

When the user types his or her name and then clicks the Click Here button, the name appears directly below the button,
 as shown in Figure 2. 

[Figure 2: Simple page displaying user input]

In ASP.NET pages, the user interface programming is divided into two distinct pieces: the visual component, or
 view, and the logic, which is a combination of the model and the controller. This division separates the visible
 portion of the page (the view) from the code behind the page with which the page interacts (model and controller). 


The visual element is called the Web Forms page. The page consists of a file containing static HTML or ASP.NET server
 controls, or both simultaneously. For this example, the Web Forms page is named SimplePage.aspx and consists of the following code: 
 
```aspx
<%@ Page language="c#" Codebehind="SimplePage.aspx.cs" AutoEventWireup="false" Inherits="SimplePage" %>
<HTML>
   <body>
      <form id="Form1" runat="server">
         Name:<asp:textbox id="name" runat="server" />
         <p />
         <asp:button id="MyButton" text="Click Here" OnClick="SubmitBtn_Click" runat="server" />
         <p />
         <span id="mySpan" runat="server"></span>
      </form>
   </body>
</HTML>
```
 
The logic for the Web Forms page consists of code that you create to interact with the form. The programming logic resides
 in a file that is separate from the user interface file. This file, referred to as the code-behind file, is named 
SimplePage.aspx.cs:
 
```cs
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

public class SimplePage : System.Web.UI.Page
{
   protected System.Web.UI.WebControls.TextBox name;
   protected System.Web.UI.WebControls.Button MyButton;
   protected System.Web.UI.HtmlControls.HtmlGenericControl mySpan;

   public void SubmitBtn_Click(Object sender, EventArgs e) 
   {
      mySpan.InnerHtml = "Hello, " + name.Text + "."; 
   }
}
```

The purpose of this code is to indicate to the page controller that when the user clicks the button, a request will 
be sent back to the server and the SubmitBtn_Click function will be executed.


This implementation shows how simple it is to connect to the events that the controller provides. It also illustrates 
that code written in this fashion is easier to understand because the application logic is not combined with the low-level
 code that manages the event dispatching. 

###Common Look and Feel Example 


The following example uses a typical implementation strategy of the page controller to provide a banner that displays 
dynamic content: the authenticated user's e-mail address (which is retrieved from the database) on every page in the application. 

The common implementation is contained in a base class from which all of the page objects in the site inherit. Figure 3 
shows one of the pages in the site. 

[Figure 3: Banner displaying dynamic content]

The individual pages in the site are responsible for rendering their own content, while the base class is responsible 
for rendering the header. Because the individual pages inherit from the base class, they all have the same functionality. 

This implementation uses a design pattern called Template Method. The pattern defines the skeleton of an algorithm in an
 operation, deferring some steps to subclasses. Template Method lets subclasses redefine certain steps of an algorithm 
without changing the algorithm's structure. [Gamma95]

Applying Template Method to this problem involves moving common code from the individual pages into a base class. 
This ensures that the common code is contained in one place and is easily maintainable. In this example, the base 
class is named BasePage and is responsible for connecting the Page_Load method to the Load event. After the work 
associated with the BasePage; which is retrieving the user's e-mail address from the database and setting the site name,
 the Page_Load function calls a method named PageLoadEvent. Subclasses implement PageLoadEvent to perform their own
 specific Load functionality. Figure 4 shows the structure of this solution. 

[Figure 4: Structure of the code-behind pages implementation]

When a page is requested, the ASP.NET runtime fires the Load event, which in turn calls the Page_Load method on BasePage.
 The BasePage method retrieves the data it needs and then calls PageLoadEvent on the specific page that was requested
 to perform any page-specific loading that is required. Figure 5 shows the page request sequence.

[Figure 5: Page request sequence]

Implementing the common functionality in this manner frees the pages from having to set up the header and also 
allows for site-wide changes to be made easily. If the header rendering and initialization code is not contained
 in a single file, the changes must be made to all files that contain code that is related to the header. 

BasePage.cs 


* The code for the base class implements the following functionality:
* Connects the Load event to the Page_Load method for request-specific initialization. 
* Retrieves the authenticated user's name from the request context and using the DatabaseGateway class finds the user's 
record in the database. The code assigns the eMail label to the user's e-mail address. 
* Assigns the site name to the siteName label. 
* Calls the PageLoadEvent method, which derived classes can implement for any page-specific loading. 

Note: It would be better to define the BasePage class as abstract, because doing so would force the implementers to 
provide an implementation for PageLoadEvent. However, in Microsoft Visual Studio .NET, it is not possible to define 
this base class as abstract. Instead, the class provides a default implementation that can be overridden by derived classes. 
 
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

BasePage.inc 


Not only do you have to provide a common base class for the logic code behind the page, but you also have to provide a 
common file that holds the view or UI rendering code. The code is included in each .aspx page. This HTML file is not intended
 to be displayed on its own. Using a common file enhances your ability to make changes in one place and have them propagate
 to all the pages that include the file. The following example code shows the common file for this example, named BasePage.inc:

```
<table width="100%" cellspacing="0" cellpadding="0">
   <tr>
      <td align="right" bgcolor="#9c0001" cellspacing="0" cellpadding="0" width="100%" height="20">
         <font size="2" color="#ffffff">Welcome:
         <asp:Label id="eMail" runat="server">username</asp:Label>&nbsp; </font>
      </td>
   </tr>
   <tr>
      <td align="right" width="100%" bgcolor="#d3c9c7" height="70">
         <font size="6" color="#ffffff">
         <asp:Label id="siteName" Runat="server">Micro-site Banner</asp:Label>&nbsp; </font>
      </td>
   </tr>
</table>
```
 
DatabaseGateway.cs 


This class encapsulates all access to the database for these pages. This is an example of a Table Data Gateway [Fowler03] 
which represents the model code for the pages in this application. 

```cs
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

public class DatabaseGateway
{
   public static string RetrieveAddress(string name)
   {
         String address = null;

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
}
```
 
Page1.aspx 


The following is an example of how to use the common functionality in a page: 
 

```aspx
<%@ Page language="c#" Codebehind="Page1.aspx.cs" AutoEventWireup="false" Inherits="Page1" %>
<HTML>
   <HEAD>
      <title>Page-1</title>
   </HEAD>
   <body>
      <!-- #include virtual="BasePage.inc" -->
      <form id="Page1" method="post" runat="server">
         <h1>Page:
            <asp:label id="pageNumber" Runat="server">NN</asp:label></h1>
      </form>
   </body>
</HTML>
 ```
The following directive from the file loads the common HTML for the header:
 
```aspx
<!-- #include virtual="BasePage.inc" -->
``` 

Page1.aspx.cs 


The code-behind class must inherit from the BasePage class and then implement the PageLoadEvent method to do any
 page-specific loading. In this example, the page-specific activity is to assign the number 1 to the pageNumber label. 
 
```cs
using System;
using System.Web.UI;
using System.Web.UI.WebControls;

public class Page1 : BasePage
{
   protected System.Web.UI.WebControls.Label pageNumber;

   protected override void PageLoadEvent(object sender, System.EventArgs e)
   {
      pageNumber.Text = "1";
   }
}
```
 
##Testing Considerations 

The dependence on the ASP.NET runtime makes testing of the implementation difficult. It is not possible to instantiate 
classes that inherit from System.Web.UI.Page or the other various classes contained in the environment. This makes it 
impossible to unit test the individual pieces of the application in isolation. The only remaining way to test this 
implementation automatically is to generate HTTP requests and then retrieve the HTTP response and determine if the
 response is correct. This approach is prone to error because you are comparing the text of the response with expected text. 

##Resulting Context 

The built-in ASP.NET page controller functionality results in the following benefits and liabilities:

###Benefits 

* Takes advantage of framework features. The page controller functionality is built into ASP.NET and can be easily
 extended by connecting application-specific actions to the events exposed by the controller. It is also easy to
 separate the controller-specific code from the model and view code by using the code-behind feature. 

* Explicit URLs. The URL that the user enters refers to an actual page in the application. This means that the pages
 can be bookmarked and entered later. The URLs also tend to have fewer parameters making them easier for users to enter.
 
* Increases modularity and reuse. The Common Look and Feel example demonstrated how you could reuse BasePage for
 many pages without having to modify the BasePage class or HTML file.

###Liabilities 

* Requires code changes. To share common functionality, as demonstrated[示范] in the Common Look and Feel example, 
the individual pages have to be modified to inherit from the newly defined base class instead of System.Web.UI.Page. 
The Intercepting Filter pattern describes a mechanism for adding common functionality by changing the Web.config file
 and not the pages themselves.

* Uses inheritance. The Common Look and Feel example uses inheritance to share the implementation across multiple pages.
 Most programmers who learn object-oriented programming initially like inheritance. However, using inheritance to share 
implementation can often lead to software that is difficult to change. If the base class become complicated with conditional 
logic, it is better to introduce helper classes or to consider using Front Controller instead. 

* Difficult to test. Because the page controller is implemented in ASP.NET, it is difficult to test in isolation. 
To improve the testability, you should separate as much functionality out of the ASP.NET - specific code in classes
 that do not depend on ASP.NET. This enables you to test without having to start the ASP.NET runtime. 

##Related Patterns 

For more information, see the following related patterns:

Template Method [Gamma95]. The BasePage class and the Page_Load method are an example implementation of this pattern. 
Intercepting Filter.
Front Controller.

##Acknowledgments 

[Gamma95] Gamma, Helm, Johnson, and Vlissides. Design Patterns: Elements of Reusable Object-Oriented Software. Addison-Wesley, 1995.
[Reilly02] Reilly, Douglas J. Designing Microsoft ASP.NET Applications. Microsoft Press, 2002.
[Fowler03] Fowler, Martin. Patterns of Enterprise Application Architecture. Addison-Wesley, 2003.