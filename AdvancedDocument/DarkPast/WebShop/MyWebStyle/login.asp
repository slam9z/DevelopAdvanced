<!-- #include file="conn.asp" --> 
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
<title>用户登录</title>

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
	用户登录

</center> </h3>


	<div class="pro_detail">


<form name="form1" method="post" action="checkuser.asp"> 
<table width="34%" border="0"> 
<tr> 
<td width="33%" height="30">用户名:</td> 
<td width="67%" height="30"><input name="username" type="text" id="username" size="15"></td> 
</tr> 
<tr> 
<td height="30">密 码:</td> 
<td height="30"><input name="password" type="password" id="password" size="15"></td> 
</tr> 
<tr> 
<td colspan="2" align="center"><input type="submit" name="Submit" value="确定"> 
<input type="reset" name="Submit" value="重置"></td> 
</tr> 
<tr> 
<td colspan="2"><a href="reg.asp" target="_self">注册</a></td> 
</tr> 
</table> 
</form> 
	</div>
</div>
<!--主体内容结束-->
</body>
</html>
