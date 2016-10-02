[ASP.NET的页面中对其他文件的引用 ](http://www.cnblogs.com/awpatp/archive/2010/02/07/1665573.html)

##先来看看ASP风格的


##在ASP.NET页面中动态包括HTML文件和客户段脚本文件

因为ASP.NET应用程序在发送给客户端之前要经过编译, 运行的, 所以在服务器端include文件的时候, 你不能使用变量来替代文件名
, 你可以使用Response或者StreamReader对象来向HTML流中写入被include的文件.
 
举例:

```aspx
 <%@ Page Language="vb" AutoEventWireup="false"%>
 <html>
 <body>
      <%           
        Response.WriteFile ("Yourfile.inc")
      %>
 </body>
 </html>
 ```
 
在ASP.NET页面中插入指定文件的内容, 包括Web pages(.aspx文件), user control files(.ascs文件), 还有Global.asax文件

语法, 与ASP风格的include 相同.

```aspx
<!-- #include file|virtual="filename" -->
``` 

Remarks
赋予File或者Virtual属性的值必须被双引号括起来(""). 被included的文件会在任何动态代码执行前被处理. Include文件能被用来包含
从静态文本(比如说普通的页面header或者公司地址), 到包含土工的服务器端代码(server-side code), 控件(control), 或者开发者想要
插入到其他页面中的HTML标签块.
 
尽管你也可以使用这个#include 的方式来重用代码, 在ASP.NET中通常的更好的方式是使用Web user controls. 因为user control提供了
面向对象的编程模型, 而且比服务器端的include功能更加强大.
 
 #include标签必须被HTML或XML的注释边界符括起来, 来避免它被解释成为字面上的文本.
 
举例:

```aspx
<html>
   <body>
      <!-- #Include virtual="/include/header.inc" -->
        Here is the main body of the .aspx file.
      <!-- #Include virtual="/include/footer.inc" -->
   </body>
</html>
```
 
参考资料:
How To Dynamically Include Files in ASP.NET
http://support.microsoft.com/kb/306575
Server-Side Include Directive Syntax 
http://msdn.microsoft.com/en-us/library/3207d0e3.aspx
ASP Including Files
http://www.w3schools.com/asp/asp_incfiles.asp