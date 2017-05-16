[Implementing Model-View-Controller in ASP.NET](https://msdn.microsoft.com/en-us/library/ff647462.aspx)


>Note:
>This content was developed in June 2003. It pre-dates ASP.NET MVC, and describes how to implement the Model-View-Controller pattern on top of ASP.NET Web Forms. For more information about ASP.NET MVC, see: 
>ASP.NET MVC: The Official Microsoft ASP.NET Site
>ASP.NET Model View Controller (MVC) on MSDN


##Context 

You are building a Web application in Microsoft ASP.NET, and, based on the complexity of your application, you need to separate
 different aspects of the program to reduce code duplication and to limit the propagation[传播] of change. 


##Implementation Strategy 

To explain how to implement the Model-View-Controller pattern in ASP.NET and the value provided by separating the model, 
view, and controller roles in software, the following example refactors a single-page solution, which does not separate 
all three roles, into a solution that separates the roles. The example application is a single Web page (shown in Figure 1) 
with a drop-down list, which displays recordings that are stored in a database. 

[Figure 1: Example Web page]

The user selects a specific recording from the drop-down list and then clicks the Submit button. The application then retrieves 
the list of all tracks from this recording from the database and displays the results in a table. All three solutions described 
in this pattern implement the exact same functionality. 

###Single ASP.NET Page 


There are many ways to implement this page in ASP.NET. The simplest and most straightforward is to put everything in one file 
called "Solution.aspx," as in the following code example: 
 
```aspx
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="System.Data.SqlClient" %>
<html>
   <head>
      <title>start</title>
      <script language="c#" runat="server">
         void Page_Load(object sender, System.EventArgs e)
         {
            String selectCmd = "select * from Recording";

            SqlConnection myConnection = 
               new SqlConnection(
                  "server=(local);database=recordings;Trusted_Connection=yes");
            SqlDataAdapter myCommand = new SqlDataAdapter(selectCmd, 
               myConnection);

            DataSet ds = new DataSet();
            myCommand.Fill(ds, "Recording");
            
            recordingSelect.DataSource = ds;
            recordingSelect.DataTextField = "title";
            recordingSelect.DataValueField = "id";
            recordingSelect.DataBind();
         }
         
         void SubmitBtn_Click(Object sender, EventArgs e) 
         {   
            String selectCmd = 
               String.Format(
               "select * from Track where recordingId = {0} order by id",
               (string)recordingSelect.SelectedItem.Value);

            SqlConnection myConnection = 
               new SqlConnection(
                  "server=(local);database=recordings;Trusted_Connection=yes");

            SqlDataAdapter myCommand = new SqlDataAdapter(selectCmd,
               myConnection);

            DataSet ds = new DataSet();
            myCommand.Fill(ds, "Track");

            MyDataGrid.DataSource = ds;
            MyDataGrid.DataBind();
         }
      </script>
   </head>
   <body>
      <form id="start" method="post" runat="server">
         <h3>Recordings</h3>
         Select a Recording:<br />
         <asp:dropdownlist id="recordingSelect" runat="server" />
         <asp:button runat="server" text="Submit" OnClick="SubmitBtn_Click" />
         <p/>
         <asp:datagrid id="MyDataGrid" runat="server" width="700" 
               backcolor="#ccccff" bordercolor="black" showfooter="false" 
               cellpadding="3" cellspacing="0" font-name="Verdana" 
               font-size="8pt" headerstyle-backcolor="#aaaadd" 
               enableviewstate="false" />
      </form>
   </body>
</html>
 ```


This file implements all three roles from the pattern but does not separate them into different files or classes. 
The view role is represented by the HTML-specific rendering code. This page uses an implementation of Bound Data 
Control to display the DataSet object that is returned from the database. The model role is implemented in the 
Page_Load and SubmitBtn_Click functions. The controller role is not represented directly, but it is implicit in
 ASP.NET; see Page Controller. The page updates when the user makes a request. Model-View-Controller describes 
this as a passive controller. ASP.NET implements the controller role, but the programmer is responsible for connecting 
the actions to the events to which the controller will respond. In this example, the controller calls the Page_Load
 function before the page loads. The controller calls the SubmitBtn_Click function when the user clicks the Submit button.

* You want to increase parallelism[相似] and reduce potential[潜在的] for errors. You may want different people working 
on the view code and the model code to increase the amount of parallelism and limit the potential for introducing errors.
 For example, if all of the code is in one page, a developer could change the formatting of the DataGrid and inadvertently
 change some of the source code that accesses the database. You would not discover this error until the page was viewed 
again, because the page is not compiled until it is viewed. 

* You want to reuse the database access code on multiple pages. In this current implementation, there is no way to 
reuse any of the code in other pages without duplicating it. Duplicate code is difficult to maintain, because if a 
change occurs in the database code, you have to modify all the pages that access the database.


To address some of these issues, the implementers of ASP.NET introduced the code-behind feature. 

###Code-Behind Refactoring 


The code-behind feature of the Microsoft Visual Studio .NET development system makes it easy to separate the presentation (view) code from the model-controller code. Each ASP.NET page has a mechanism that allows methods that are called from the page to be implemented in a separate class. This mechanism is facilitated by Visual Studio .NET and it has many advantages, such as Microsoft IntelliSense technology. When you use the code-behind feature to implement your pages, you can use IntelliSense to show a list of available methods of the objects that you are using in the code behind the page. IntelliSense does not work in .aspx pages. 

The following is the same example, this time using the code-behind feature to implement ASP.NET. 
View 


The presentation code is now in a separate file called Solution.aspx: 
 
```aspx
<%@ Page language="c#" Codebehind="Solution.aspx.cs" 
   AutoEventWireup="false" Inherits="Solution" %>
<html>
   <head>
      <title>Solution</title>
   </head>
   <body>
      <form id="Solution" method="post" runat="server">
         <h3>Recordings</h3>
         Select a Recording:<br/>
         <asp:dropdownlist id="recordingSelect" runat="server" />
         <asp:button id="submit" runat="server" text="Submit" 
            enableviewstate="False" />
         <p/>
         <asp:datagrid id="MyDataGrid" runat="server" width="700"
               backcolor="#ccccff" bordercolor="black" showfooter="false"
               cellpadding="3" cellspacing="0" font-name="Verdana" font-size="8pt"
               headerstyle-backcolor="#aaaadd" enableviewstate="false" />
      </form>
   </body>
</html>
```
 
Most of this code is similar to the code used in the first implementation. The main difference is the first line:
 
```aspx
<%@ Page language="c#" Codebehind="Solution.aspx.cs" 
   AutoEventWireup="false" Inherits="Solution" %>
```

This line indicates to the ASP.NET environment that a code-behind class implements methods that are referenced in this page.
 Because the page is free of any code that accesses the database, there is no longer any need to modify this page if the 
database access code changes. Someone who is familiar with the design of the user interface can modify this code without 
introducing any errors to the database access code.


###Model-Controller 


The second part of the solution is the following code-behind page:
 
```cs
using System;
using System.Data;
using System.Data.SqlClient;

public class Solution : System.Web.UI.Page
{
   protected System.Web.UI.WebControls.Button submit;
   protected System.Web.UI.WebControls.DataGrid MyDataGrid;
   protected System.Web.UI.WebControls.DropDownList recordingSelect;
   
   private void Page_Load(object sender, System.EventArgs e)
   {
      if(!IsPostBack)
      {
         String selectCmd = "select * from Recording";

         SqlConnection myConnection = 
            new SqlConnection(
               "server=(local);database=recordings;Trusted_Connection=yes");
         SqlDataAdapter myCommand = new SqlDataAdapter(selectCmd, myConnection);

         DataSet ds = new DataSet();
         myCommand.Fill(ds, "Recording");

         recordingSelect.DataSource = ds;
         recordingSelect.DataTextField = "title";
         recordingSelect.DataValueField = "id";
         recordingSelect.DataBind();
      }
   }

   void SubmitBtn_Click(Object sender, EventArgs e) 
   {   
      String selectCmd = 
         String.Format(
         "select * from Track where recordingId = {0} order by id",
         (string)recordingSelect.SelectedItem.Value);

      SqlConnection myConnection = 
         new SqlConnection(
            "server=(local);database=recordings;Trusted_Connection=yes");
      SqlDataAdapter myCommand = new SqlDataAdapter(selectCmd, myConnection);

      DataSet ds = new DataSet();
      myCommand.Fill(ds, "Track");

      MyDataGrid.DataSource = ds;
      MyDataGrid.DataBind();
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
      this.submit.Click += new System.EventHandler(this.SubmitBtn_Click);
      this.Load += new System.EventHandler(this.Page_Load);

   }
   #endregion
}
```
 
This code has been moved from the single ASP.NET page into its own file. A few syntactic changes are required to link the two 
entities together. The member variables defined in the class share the same name as the ones referenced in the Solution.aspx file.
 The other aspect that must be explicitly defined is how the controller links the events that occur to the actions that must 
be performed. The InitializeComponent method links the two events in this example. The first is the Load event, which is 
linked to the Page_Load function. The second is the Click event, which triggers the SubmitBtn_Click function to run when 
the Submit button is clicked. 

The code-behind feature is an elegant[优美的] mechanism for separating the view role from the model and controller roles. It may
 become insufficient when you need to reuse the code that is present in the code-behind class for another page. It is
 technically possible to reuse the code from the code-behind page, but highly undesirable, due to the increase in coupling
 of all the pages that share the code-behind class.

###Model-View-Controller Refactoring 


To resolve the last issue, you need to further separate the model code from the controller. The view code is identical 
to the code used in the previous implementation. 

###Model 


The following code example describes the model and is dependent on the database only; it does not contain any view-dependent 
code (code with ASP.NET dependencies):
 
```cs
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

public class DatabaseGateway
{
   public static DataSet GetRecordings()
   {
      String selectCmd = "select * from Recording";

      SqlConnection myConnection = 
         new SqlConnection(
            "server=(local);database=recordings;Trusted_Connection=yes");
      SqlDataAdapter myCommand = new SqlDataAdapter(selectCmd, myConnection);

      DataSet ds = new DataSet();
      myCommand.Fill(ds, "Recording");
      return ds;
   }

   public static DataSet GetTracks(string recordingId)
   {
      String selectCmd = 
         String.Format(
         "select * from Track where recordingId = {0} order by id",
         recordingId);

      SqlConnection myConnection = 
         new SqlConnection(
            "server=(local);database=recordings;Trusted_Connection=yes");
      SqlDataAdapter myCommand = new SqlDataAdapter(selectCmd, myConnection);

      DataSet ds = new DataSet();
      myCommand.Fill(ds, "Track");
      return ds;
   }
```

This is now the only file that depends on the database. This class is an excellent example of a Table Data Gateway. 
A Table Data Gateway holds all the SQL code for accessing a single table or view; selects, inserts, updates, and deletes.
 Other code calls its methods for all interaction with the database. [Fowler03]


###Controller 


This refactoring uses the code-behind feature to adapt the model code to the data controls that exist on the page and 
to map the events that the controller forwards to the specific action methods. Because the model here returns a DataSet 
object, its job is straightforward. This code, like the view code, does not depend on how data is retrieved from the database.
 
```
using System;
using System.Data;
using System.Collections;
using System.Web.UI.WebControls;

public class Solution : System.Web.UI.Page
{
   protected System.Web.UI.WebControls.Button submit;
   protected System.Web.UI.WebControls.DataGrid MyDataGrid;
   protected System.Web.UI.WebControls.DropDownList recordingSelect;
   
   private void Page_Load(object sender, System.EventArgs e)
   {
      if(!IsPostBack)
      {
         DataSet ds = DatabaseGateway.GetRecordings();
         recordingSelect.DataSource = ds;
         recordingSelect.DataTextField = "title";
         recordingSelect.DataValueField = "id";
         recordingSelect.DataBind();
      }
   }

   void SubmitBtn_Click(Object sender, EventArgs e) 
   {   
      DataSet ds = 
         DatabaseGateway.GetTracks(
         (string)recordingSelect.SelectedItem.Value);

      MyDataGrid.DataSource = ds;
      MyDataGrid.DataBind();
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
      this.submit.Click += new System.EventHandler(this.SubmitBtn_Click);
      this.Load += new System.EventHandler(this.Page_Load);

   }
   #endregion
}
```

###Tests 

Separating the model from the ASP.NET environment makes testing of the model code easier. To test this code inside the 
ASP.NET environment, you must test the output of the process. This means reading HTML and determining if it is correct,
 which is tedious and error-prone. The separation of the model so that it can run without ASP.NET allows you to avoid the
 tedium and test the code in isolation. The following are sample unit tests in NUnit (http://nunit.org) for the model code:
 
```cs
using System;

using NUnit.Framework;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

[TestFixture]
public class GatewayFixture
{
   [Test]
   public void Tracks1234Query()
   {

      DataSet ds = DatabaseGateway.GetTracks("1234");
      Assertion.AssertEquals(10, ds.Tables["Track"].Rows.Count);
   }

   [Test]
   public void Tracks2345Query()
   {
      DataSet ds = DatabaseGateway.GetTracks("2345");
      Assertion.AssertEquals(3, ds.Tables["Track"].Rows.Count);
   }

   [Test]
   public void Recordings()
   {
      DataSet ds = DatabaseGateway.GetRecordings();
      Assertion.AssertEquals(4, ds.Tables["Recording"].Rows.Count);

      DataTable recording = ds.Tables["Recording"];
      Assertion.AssertEquals(4, recording.Rows.Count);

      DataRow firstRow = recording.Rows[0];
      string title = (string)firstRow["title"];
      Assertion.AssertEquals("Up", title.Trim());
   }
}
  
```


##Resulting Context 

Implementing MVC in ASP.NET results in the following benefits and liabilities:

###Benefits 

* Reduced dependencies. An ASP.NET page allows the programmer to implement methods within a page. As the Single A
SP.NET Page shows, this can be useful for prototypes and small short-lived Web applications. As the complexity of 
the page, or the need to share code between pages, increases, it becomes more useful to separate portions of the code. 

* Reduced code duplication. The GetRecordings and GetTracks methods in the DatabaseGateway class can now be used by
 other pages. This eliminates[消除] the need to copy the methods into multiple views.

* Separation of duties and concerns. The skill set for modifying the ASP.NET pages is different from the skill set 
for writing code that accesses the database. Separating the view and the model, as shown earlier, allows specialists
 in each area to work in parallel.

* Optimizing opportunities[优化的机会]. Separating the responsibilities into specific classes, as shown earlier, increases the 
opportunities for optimization. In the example described previously, the data is loaded from the database every time 
a request is made. It would be possible to cache the data in certain situations, which could improve the overall 
performance of the application. This, however, would be difficult or impossible without separating the code. 

* Testability. Isolating the model from the view makes it possible to test the model outside the ASP.NET environment. 


###Liabilities 

* Additional code and complexity. The example shown earlier adds more files and code, which increases the maintenance 
cost of the code when changes must be made to all three roles. In some cases, making the changes in one file is
 easier than separating out the changes into multiple files. The extra cost must be weighed against the reasons for
 separating the code. For small applications, the cost might not be justified. 

##Related Patterns 

For more information, see the following related patterns:

Table Data Gateway. This pattern is an object that acts as a gateway to a database table. One instance handles all the 
roles in a table. [Fowler03]

Bound Data Control. This pattern is a user interface component that is bound to a data source and can render itself on the 
screen or page. 

##Acknowledgments 

[Fowler03] Fowler, Martin. Patterns of Enterprise Application Architecture. Addison-Wesley, 2003.