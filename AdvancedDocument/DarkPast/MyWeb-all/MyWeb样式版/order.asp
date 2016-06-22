<!-- #include file="conn.asp" --> 
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
<title>订单</title>

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





	</ul>
</div>
<!--导航结束-->


<!--主体内容开始-->
<div id="content" class="global_module">
	<h3><center> 
	订单

</center> </h3>


	<div class="pro_detail">

<% 
set rsc=server.createobject("adodb.recordset") 
sqlc="select  proname  from production "
rsc.open sqlc,conn,1,1 
%> 


<% 
=request("msg") 
%> 
<form name="form1" method="post" action="addorder.asp "> 

<table border="0"   > 
<tr> 
<td >商品名</td> 
<td  >

<select name="mainmenu" size="1" >
<option value=0>请选择商品&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</option>
       <%while not rsc.eof%>
<option value="<%=rsc("proname")%>"><%=rsc("proname")%></option>
<%rsc.movenext
wend%>
</select>


</td> 
</tr> 
<tr> 
<td height="30">数量</td> 
<td height="30"><input name="num" type="text" id="num"> 
</td> 
</tr> 
<tr> 
<td height="30">联系地址</td> 
<td height="30"><input name="add" type="text" id="add"> 
</td> 
</tr> 
<tr> 
<td height="30">电话号码</td> 
<td height="30"><input name="phone" type="text" id="phone"></td> 
</tr> 

<tr> 
<tr> 
<td> </td> 
<td><input type="submit" name="Submit" value="提交"></td> 
</tr> 
</table> 
</form> 
	</div>
</div>
<!--主体内容结束-->
</body>
</html>
