<!-- #include file="conn.asp" --> 
<html> 
<head> 
<meta http-equiv="Content-Type" content="text/html; charset=gb2312"> 
<title>订单成功</title> 
</head> 
<body> 

<%
dsql="select * from userorder " 
set rs=server.createobject("adodb.recordset") 
rs.open dsql,conn,1,3 
rs.addnew 
user=session("username")
dim a,i
a= request.Form("num")
i=cint(a)

rs("username")=user
rs("orderpro")=request.Form("mainmenu") 
rs("num")=i
rs("add")=request.Form("add") 
rs("phone")=request.Form("phone") 
rs("time")=now 
rs.update 
rs.close 
set rs=nothing 
%> 
<center> 
<a href="index.asp" target="_self">下订单成功</a> 
</center> 

</body> 
</html> 
