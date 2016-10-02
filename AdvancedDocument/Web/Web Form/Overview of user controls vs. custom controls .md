[Overview of user controls vs. custom controls ](https://support.microsoft.com/en-us/kb/893667)

其实和Client的Control与UserControl的区别类似

##Overview of user controls vs. custom controls

To customize this column to your needs, we want to invite you to submit your ideas about topics that interest you and 
issues that you want to see addressed in future Knowledge Base articles and Support Voice columns. 
You can submit your ideas and feedback using the Ask For It form. There's also a link to the form at the
 bottom of this column.

##Introduction

Hi! This is Parag, and I am a support engineer working with the Microsoft ASP.NET support group for more than a year now. 
Prior to joining Microsoft, I worked on Web-based projects and desktop applications using Microsoft technologies.
 While providing quality support to customers, I have seen cases where there was some confusion around custom
 controls, and I would just like to take some time to explain some concepts around custom controls. 
As bad as it looks, believe me, once you get the hang of it, you will be in a better position to appreciate ASP.NET. 

##Overview
In this month's column, I'll discuss the following topics:

* What are user controls?
* What are custom controls?
* What are the basic differences between user controls and custom controls?

I'll also introduce a few of the advanced topics that concern custom controls, such as state management and the 
rendering of custom controls.

###What are user controls?

User controls are custom, reusable controls, and they use the same techniques that are employed by HTML and Web server controls. 
They offer an easy way to partition and reuse common user interfaces across ASP.NET Web applications. 
They use the same Web Forms programming model on which a Web Forms page works. For more details about the Web 
Forms programming model, visit the following Microsoft Developer Network (MSDN) Web sites:


Introduction to Web Forms pages
http://msdn2.microsoft.com/en-us/library/65tcbxz3(vs.71).aspx

Web Forms code model
http://msdn2.microsoft.com/en-us/library/015103yb(vs.71).aspx

###How to create a user control

The syntax you use to create a user control is similar to the syntax you use to create a Web Forms page (.aspx). 
The only difference is that a user control does not include the <html>, <body>, and <form> elements since a Web 
Forms page hosts the user control. To create a user control, follow these steps:

Open a text or HTML editor, and create a server-side code block exposing all the properties, methods, and events. 

```aspx
<script language="C#" runat="server">
   public void button1_Click(object sender, EventArgs e)
   {
      label1.Text = "Hello World!!!";
   }
</script>
```

Create a user interface for the user control. 

```aspx
<asp:Label id="label1" runat="server"/>
 <br><br>
<asp:button id="button1" text="Hit" OnClick="button1_Click" runat="server" />
```

###How to use a user control in a Web Forms page

Create a new Web Forms page (.aspx) in Microsoft Visual Studio .NET 2002, Microsoft Visual Studio .NET 2003, 
Microsoft Visual Studio 2005, or any text editor.
Declare the @ Register directive. For example, use the following code. 

###How to create an instance of a user control programmatically in the code behind file of a Web Forms page

The previous example instantiated a user control declaratively in a Web Forms page using the @ Register directive.
 However, you can instantiate a user control dynamically and add it to the page. Here are the steps for doing that: 

Create a new Web Forms page in Visual Studio.
Navigate to the code behind file generated for this Web Forms page.
In the Page_Load event of the Page class, write the following code.

```cs   
// Load the control by calling LoadControl on the page class.
Control c1 = LoadControl("test.ascx");
            
// Add the loaded control in the page controls collection.	
Page.Controls.Add(c1);
Note You can add a user control dynamically at certain events of the page life cycle.
```

For more information, visit the following Web sites:
Adding controls to a Web Forms page programmatically
http://msdn2.microsoft.com/en-us/library/kyt0fzt1(vs.71).aspx

Control execution lifecycle
http://msdn2.microsoft.com/en-us/library/aa719775(vs.71).aspx

Dynamic Web controls, postbacks, and view state, by Scott Mitchell
http://aspnet.4guysfromrolla.com/articles/092904-1.aspx

##How a user control is processed

When a page with a user control is requested, the following occurs:
The page parser parses the .ascx file specified in the Src attribute in the @ Register directive and generates
 a class that derives from the System.Web.UI.UserControl class.

The parser then dynamically compiles the class into an assembly.

If you are using Visual Studio, then at design time only, Visual Studio creates a code behind file for the user control,
 and the file is precompiled by the designer itself.
Finally, the class for the user control, which is generated through the process of dynamic code generation and compilation,
 includes the code for the code behind file (.ascx.cs) as well as the code written inside the .ascx file.


##What are custom controls?

Custom controls are compiled code components that execute on the server, expose the object model, 
and render markup text, such as HTML or XML, as a normal Web Form or user control does.

##How to choose the base class for your custom control

To write a custom control, you should directly or indirectly derive the new class from the System.Web.UI.Control
 class or from the System.Web.UI.WebControls.WebControl class:

* You should derive from System.Web.UI.Control if you want the control to render nonvisual elements. 
example, <meta> and <head> are examples of nonvisual rendering.
    
* You should derive from System.Web.UI.WebControls.WebControl if you want the control to render HTML that 
generates a visual interface on the client computer.

If you want to change the functionality of existing controls, such as a button or label, 
you can directly derive the new class with these existing classes and can change their default behavior.

In brief, the Control class provides the basic functionality by which you can place it in the control 
tree for a Page class. The WebControl class adds the functionality to the base Control class for displaying
 visual content on the client computer. For example, you can use the WebControl class to control the look and styles
 through properties like font, color, and height.

##How to create and use a simple custom control that extends from System.Web.UI.Control using Visual Studio

Override the Render method to write the output to the output stream. 
```CS
protected override void Render(HtmlTextWriter writer) 
{
	 writer.Write("Hello World from custom control");
}
```

Note The HtmlTextWriter class has the functionality of writing HTML to a text stream. 
The Write method of the HtmlTextWriter class outputs the specified text to the HTTP response stream and is 
the same as the Response.Write method.


##What are the basic differences between user controls and custom controls?

Now that you have a basic idea of what user controls and custom controls are and how to create them, 
let's take a quick look at the differences between the two. 

| Factors | User control |Custom control |
|----------|----------|----------|
| Deployment | Designed for single-application scenarios <br> Deployed in the source form (.ascx) along with the source code of the application <br> If the same control needs to be used in more than one application, it introduces redundancy and maintenance problems| Designed so that it can be used by more than one application <br> Deployed either in the application's Bin directory or in the global assembly cache <br> Distributed easily and without problems associated with redundancy and maintenance |
| Creation |Creation is similar to the way Web Forms pages are created; well-suited for rapid application development (RAD) |Writing involves lots of code because there is no designer support |
| Content |A much better choice when you need static content within a fixed layout, for example, when you make headers and footers|More suited for when an application requires dynamic content to be displayed; can be reused across an application, for example, for a data bound table control with dynamic rows |
| Design |Writing doesn't require much application designing because they are authored at design time and mostly contain static data| Writing from scratch requires a good understanding of the control's life cycle and the order in which events execute, which is normally taken care of in user controls |

##Advanced topics

Next, let's take a look at a few of the advanced features that you may use while developing custom controls.

###State management

Web applications are built on HTTP, which is stateless. A page and its child controls are created on every request and are disposed of after the request is over. To maintain state in classic ASP programming, you use session and application objects. But for that, you need to do lots of coding. To avoid this, ASP.NET provides a mechanism known as view state for maintaining state across several requests. To learn more about state management and view state, visit the following MSDN Web sites:
Introduction to Web Forms state management
http://msdn2.microsoft.com/en-us/library/75x4ha6s(vs.71).aspx

The ASP.NET view state
http://msdn.microsoft.com/msdnmag/issues/03/02/cuttingedge/default.aspx

Saving Web Forms page values using view state
http://msdn2.microsoft.com/en-us/library/4yfdwycw(vs.71).aspx

###Rendering
In this section, I'll briefly describe what methods you should override when you derive a custom control from either the Control 
class or the WebControl class.

Rendering methods of the System.Web.UI.Control class
For information about the rendering methods of the System.Web.UI.Control class, visit the following MSDN Web sites:
Control.Render method
http://msdn2.microsoft.com/en-us/library/system.web.ui.control.render(vs.71).aspx

Control.RenderControl method
http://msdn2.microsoft.com/en-us/library/system.web.ui.control.rendercontrol(vs.71).aspx

Control.RenderChildren method
http://msdn2.microsoft.com/en-us/library/system.web.ui.control.renderchildren(vs.71).aspx


###How a control is rendered on the page

Every page has a control tree that represents a collection of all the child controls for that page. To render the control tree, an object of the HtmlTextWriter class is created that contains the HTML to be rendered on the client computer. That object is passed to the RenderControl method. In turn, the RenderControl method invokes the Render method. Then, the Render method calls the RenderChildren method on each child control, making a recursive loop until the end of the collection is reached. This process is best explained by the following example code. 

```cs
public void RenderControl(HtmlTextWriter writer) 
{
    // Render method on that control will only be called if its visible property is true.
    if (Visible)
    {
        Render(writer);
    }
}

protected virtual void Render(HtmlTextWriter writer) 
{
    RenderChildren(writer);
}
protected virtual void RenderChildren(HtmlTextWriter writer) 
{
    foreach (Control c in Controls) 
    {
        c.RenderControl(writer);
    }
} 
```

###Rendering methods of the System.Web.UI.WebControl class

For information about the rendering methods of the System.Web.UI.WebControl class, visit the following MSDN Web sites:
WebControl.RenderBeginTag method
http://msdn2.microsoft.com/en-us/library/system.web.ui.webcontrols.webcontrol.renderbegintag(vs.71).aspx

WebControl.RenderContents method
http://msdn2.microsoft.com/en-us/library/system.web.ui.webcontrols.webcontrol.rendercontents(vs.71).aspx

WebControl.RenderEndTag method
http://msdn2.microsoft.com/en-us/library/system.web.ui.webcontrols.webcontrol.renderendtag(vs.71).aspx

###How the rendering of the WebControl class takes place

The following code example shows the Render method for the custom control. 

```cs
protected override void Render(HtmlTextWriter writer)
{
    RenderBeginTag(writer);
    RenderContents(writer);
    RenderEndTag(writer);
}
```
You don't need to override the Render method for the WebControl class. If you want to render contents within the WebControl class, you need to override the RenderContents method. However, if you still want to override the Render method, you must override the RenderBeginTag method as well as the RenderEndTag method in the specific order that is shown in the previous code example.

##Conclusion
That's all for now on user controls and custom controls in ASP.NET 1.0 and ASP.NET 1.1. I hope that this column helps you understand the basic differences between them and the various approaches you can take to develop them.

Thank you for your time. We expect to write more about the advanced topics for custom controls, such as state management, control styles, composite controls, and design-time support for custom controls, in the near future.

For more information about controls, visit the following MSDN Web sites:

ASP.NET server control development basics
http://msdn2.microsoft.com/en-us/library/aa310918(vs.71).aspx

An extensive examination of user controls
http://msdn2.microsoft.com/en-us/library/ms972975.aspx

Building templated custom ASP.NET server controls
http://msdn2.microsoft.com/en-us/library/Aa478964.aspx

Events in ASP.NET server controls
http://msdn2.microsoft.com/en-us/library/aa720049(vs.71).aspx

Composite control vs. user control
http://msdn2.microsoft.com/en-us/library/aa719735(vs.71).aspx

Developing ASP.NET server controls
http://msdn2.microsoft.com/en-us/library/aa719973(vs.71).aspx

Developing custom controls: Key concepts
http://msdn2.microsoft.com/en-us/library/aa720226(vs.71).aspx

Adding design-time support to ASP.NET controls 
http://msdn2.microsoft.com/en-us/library/Aa478960.aspx
