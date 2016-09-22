之前在repeater里面使用这种定义Template是能够正常work的。

```aspx 
<div id="panel"
   runat="server"
   visible='<%# hasResult%>'>
 ```

##Solution

错误提示：无法从其“Visible”属性的字符串表示形式“<%= true %>”创建“System.Boolean”类型的对象。
英文：Cannot create an object of type 'System.Boolean' from its string representation '<%= true %>'
 for the 'Visible' property.

解决办法：改为

```aspx
<% if(true) { %>
 
内容
 
<% } %>
```