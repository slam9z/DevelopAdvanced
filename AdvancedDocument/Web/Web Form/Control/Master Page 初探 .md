[Master Page 初探 ](http://blog.csdn.net/zhangleixp/article/details/753151)

Master Page （母版页）是VS.NET2005中的新特性，提供了页面模板的功能。该模板是动态的，可以由内容页面自主选择。

##1 Master Page的组成
Master Page主要由两部分组成：Master Page（母版页）本身和一个或多个Content Page（内容页）。

###1.1母版页（Master Page）

母版页与用户控件（User Control）类似，主要的不同点有：

1. 母版页的扩展名为.master,如Default.master。该扩展名和System.Web.HttpForbiddenHandler 相关联，因此客户端浏览器不能直接访问到母版页。
2. 母版页由@Master指令标记，不含有@Page或@Control指令。@Master中包含的指令和@Control中包含的指令基本相同。

	[代码示例1]@Master指令
	```aspx
	<%@ Master Language="C#" CodeFile="MasterPage.master.cs" Inherits="MasterPage" %>
	```
	
3. 母版页可以包含若干个ContentPlaceHolder控件。这些占位符控件定义了内容页（Content Page）的位置，并被内容页覆盖。
	通过VS.NET向导可以直接建立一个母版页，和建立用户控件类似。并且可以在母版页上添加你所需要的其他控件，如SiteMapPath等等。
	```aspx
	[代码示例2]一个简单母版页（Example.master）
	<%@ Master Language="C#" AutoEventWireup="true" CodeFile="Example.master.cs" Inherits="Example" %>
	 
	<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
	 
	<html xmlns="http://www.w3.org/1999/xhtml" >
	<head runat="server">
		<title>母版页标题</title>
	</head>
	<body>
		<form id="form1" runat="server">
			<table width=”100%”>
				<tr>
					<td>
						<asp:contentplaceholder id="HeaderHolder" runat="server">
						</asp:contentplaceholder>
					</td>
				</tr>
				<tr>
					<td>
						<asp:ContentPlaceHolder ID="MainHolder" runat="server">
						</asp:ContentPlaceHolder>
					</td>
				</tr>
				<tr>
					<td>
						<asp:ContentPlaceHolder ID="FooterHolder" runat="server">
						</asp:ContentPlaceHolder>
					</td>
				</tr>  
			</table>
		</form>
	</body>
	</html>
	```
	从上面的示例中可以看到，母版页中含有<HTML>，<HEAD>，<BODY> 和 <FORM> 这些顶级HTML元素，而内容页中是没有的。



	需要注意的是，母版页和内容页的关系不是继承关系，虽然和继承的特点很相近。MasterPage类的基类是UserControl。MasterPage类的声明为：public class MasterPage : UserControl 。
	最后需要注意的是，母版页不支持主题（Theme）。
	
##1.2内容页（Content Page）

内容页就是用于替换母版页中的ContentPlaceHolder的ASP.NET页面，.aspx扩展名。在VS.NET新键页面向导中选择WebForm，并选择“Select master page”，然后选择该内容页相应的母版页即可。

内容页通过@Page指令MasterPageFile，指定需要使用的母版页。如下所示：

```aspx
<%@ Page Language="C#" MasterPageFile="~/Example.master" AutoEventWireup="true" CodeFile="A.aspx.cs" Inherits="A" Title="内容页A标题" %>
```

在内容页中添加Content控件，并将其映射到母版页上的ContentPlaceHolder。然后向Content控件中添加你所需要的文本或其他控件。

[代码示例3]
```aspx
<%@ Page Language="C#" MasterPageFile="~/Example.master" AutoEventWireup="true" CodeFile="A.aspx.cs" Inherits="A" Title="内容页A标题" %>
<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeaderHolder" Runat="Server">
    Head Style 1.
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainHolder" Runat="Server">
    <p>
        使用 ASP.NET 母版页可以为应用程序中的页创建一致的布局。单个母版页可以为应用程序中的所有页（或一组页）定义所需的外观和标准行为。然后可以创建包含要显示的内容的各个内容页。当用户请求内容页时，这些内容页与母版页合并以将母版页的布局与内容页的内容组合在一起输出。
    </p>
</asp:Content>
<asp:Content ID="FooterContent" ContentPlaceHolderID="FooterHolder" Runat="Server">
    Powered by <a href="http://zhangleixp.itpub.net/" title="访问作者主页：zhangleixp.itpub.net">zhangleixp</a>
</asp:Content>
```

##2 母版页和内容页的组合及运行行为

###2.1 URL

前面已经说过，不能直接获取母版页，如：http://202.119.192.211/Example.master 这样的请求是不正确的。应该使用内容页的URL来请求某个页面。

###2.2 合并

经过编译的母版页将被合并到内容页的控件树中，Content控件中的内容合并到相应的ContentPlaceHolder控件中。如图：

可以看到，母版页是内容页的一部分，就和用户控件的行为相同。他们的关系是这样的：内容页是母版页的容器，母版页又是一个的容器，包含了内容页中相应Content中的控件。


###2.3 初始化次序

* 母版页-Init
* 内容页-Init
* 内容页-Load
* 母版页-Load
* 内容页-PreRender
* 母版页-PreRender

###2.4 页面执行环境及URL转换

母版页和内容页合并后，页面的执行环境为内容页的环境，这样会不会导致母版页中资源引用或相对URL出现错误呢？对于服务器控件，ASP.NET可以自动解决这个问题，比如母版页上的一个Image控件，其ImageUrl为相对URL：“images/banner.gif”，当母版页和内容页组合时，ASP.NET将其转换为合适的URL。对于非服务器控件或标记，如<IMG>，ASP.NET将不做任何转换，因此，在母版页中，应该尽量使用服务器控件。

##3 动态引用母版页

内容页中可以动态地引用母版页，通常在PreInit中设置需要使用的母版页。如下：

```cs
protected void Page_PreInit(object sender, EventArgs e)
{
    this.MasterPageFile = "~/Example.master";
}
```

##4 获取母版页上的控件

运行时，母版页和内容页合并了，因此可以访问母版页上的控件。可以使用FindControl方法来定位母版页上的控件。如果要访问ContentPlaceHolder中的控件，需要先获取ContentPlaceHolder控件的引用，然后再调用其FindControl方法。也就是说需要两次调用FindControl方法。
另外一种方法是在母版页中公开属性来进行访问。

##5 嵌套母版页
