<%
dim conn,rs,sql 
on error resume next 

set conn=Server.CreateObject("ADODB.Connection")
conn.Open "Provider=Microsoft.Jet.OLEDB.4.0; Data Source="&Server.MapPath("database/date.mdb")

%>


