<!-- #include file="conn.asp" --> 
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
<title>个人中心</title>

<link rel="stylesheet" href="styles/main.css" type="text/css" />
<link rel="stylesheet" href="styles/detail.css" type="text/css" />
<link rel="stylesheet" href="styles/skin/skin_1.css" type="text/css" id="cssfile" />
<link rel="stylesheet" href="styles/thickbox.css" type="text/css" />

</head>
<body>
<!--头部开始-->
<div id="header">
	<a id="logo" href="index.asp">sasa Shopping</a>

</div>


<!--头部结束-->
<!--导航开始-->
<div id="navigation">
	<ul>
		
		 <li><a href="#">联系我们</a></li>

 <%
if  Session("ustaus") then 
response.Write("<li>")
response.Write "<a href='exit.asp'>退出</a>"
response.Write("</li>")

response.Write("<li>")
response.Write "<a href='order.asp'>购买</a>"
response.Write("</li>")

response.Write("<li>")

user=session("username")
response.Write (user)

response.Write("</li>")

Else
response.Write("<li>")
response.Write "<a href='login.asp'>登录</a>"
response.Write("</li>")
response.Write("<li>")
response.Write "<a href='reg.asp'>注册</a>"
response.Write("</li>")

End if
%> 



	</ul>
</div>
<!--导航结束-->


<!--主体内容开始-->
<div id="content" class="global_module">
	<h3><center> 
	商品信息

</center> </h3>


	<div class="pro_detail">
	<%
user=session("username")
dsql="select * from userorder where username='"+user+"'" 
set rsc=server.createobject("adodb.recordset") 
rsc.open dsql,conn,1,1
%> 

<table width="100%" border="0"   align="center" > 

<%while not rsc.eof%>

<tr>
	<td> 
	<%=rsc("orderpro")%>

	</td>

	<td>
	<%=rsc("num")%>

	</td>
	<td>
	<%=rsc("add")%>

	</td>
	<td>
	<%=rsc("phone")%>

	</td>
	<td>
	<%=rsc("time")%>

	</td>
	</tr>

<%rsc.movenext
wend%>

</table> 
	</div>
</div>
<!--主体内容结束-->
</body>
</html>
