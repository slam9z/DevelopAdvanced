[ASP.NET控件之Content控件 ](http://www.cnblogs.com/superfang/archive/2008/06/29/1232158.html)


创建一个服务器控件，该控件包含呈现到母版页中的 ContentPlaceHolder 控件的文本、标记和其他服务器控件。 

```aspx
<asp:Content
    ContentPlaceHolderID="string"
    EnableViewState="True|False"
    ID="string"
    runat="server">
    Visible="True|False"
        <!-- child controls -->
</asp:Content>
```

Content 控件是内容页的内容和控件的容器。Content 控件只能和定义相应的 ContentPlaceHolder 控件的母版页一起使用。Content 控件不是独立的控件。
下面的代码示例说明如何使用 Content 控件定义母版页的内容。第一个网页是母版页，它使用一个 ContentPlaceHolder 控件来定义内容区域。

```aspx
<%@ Master Language="C#" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN" "http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>MasterPage Example</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:contentplaceholder id="ContentPlaceHolder1" runat="server" />
    </div>
    </form>
</body>
</html>
```

下面的代码示例演示如何使用在前一个代码示例中使用的母版页的内容页。在 Content 控件模板中定义的文本、标记和任何服务器控件呈现到母版页上的 ContentPlaceHolder。

```aspx
<%@ Page Language="C#" MasterPageFile="~/MasterPageSample_1cs.master" Title="Content Page"%>
 
<asp:content 
    runat="server"
    contentplaceholderid="ContentPlaceHolder1" >Hello, Master Pages!</asp:content>
```