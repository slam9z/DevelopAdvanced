##创建ascx

```xml
<%@ Control Language="C#" AutoEventWireup="false" EnableViewState="false" ClassName="DbConfigContext" %>
```


##[ascx文件用法](http://blog.163.com/freestyle_le/blog/static/183279448201182103854666/)

ascx即WebUserControl,和WindowsApplication中的UserControl作用类似,主要用于代码的复用，使用之前需要在页首加饮用。
模块化的开发机制也常用ascx作为功能载体。用众多的功能模块（每个模块包含多个UserControl）合成一个网站。

在你要引用ASCX文件的ASPX页面头部加上：

```xml
<%@ Register TagPrefix="uc" TagName="ucSample" Src="你的ASCX文件在项目中的相对路径" %>
```

或者全局配置 

```xml
<system.web>
	<pages>
		<controls>
			<add tagPrefix="UCS" src="~/UserControls/UrlPager.ascx" tagName="UrlPager"/>

```


在需要用到ASCX文件的地方加入：

```xml
<uc:ucSample ID="uc1" runat="server" />就可以了。
```