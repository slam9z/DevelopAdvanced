<!-- #include file="conn.asp" --> 
<% 
'打开数据库判断用户是否存在,info为表名,username为字段名 
set rsc=server.createobject("adodb.recordset") 
sqlc="select * from userinfo where username='"&request.Form("username")&"' and password='"&request.Form("password")&"'  " 
rsc.open sqlc,conn,1,1 


session("username")=rsc("username") 
session("password")=rsc("password") 

if rsc("username")="" Then

session.Timeout=30 
rsc.close 
set rsc=nothing 
response.Write("<center>")

response.Write "<a href='login.asp' target='_self'>登录失败,请重新登录</a>"

response.Write("</center>")

Else
Session("ustaus")=1
session.Timeout=30 
rsc.close 
set rsc=nothing 

response.Redirect("index.asp") 
end if






'如果用户不存在,session("username")为空 
%>