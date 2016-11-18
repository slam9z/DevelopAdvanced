<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
<title>Black Ablum2</title>

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
response.Write "<a href='userct.asp'>个人中心</a>"
response.Write("</li>")
response.Write("<li>")
response.Write "<a href='exit.asp'>退出</a>"
response.Write("</li>")

response.Write("<li>")
response.Write "<a href='order.asp'>购买</a>"
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
	<h3>商品信息</h3>
	<div class="pro_detail">
		<div class="pro_detail_left">
			<div class="jqzoom"><img src="image/001.jpg" class="fs" alt=""  /></div>
				<span>
                <a href="image/001.jpg" id="thickImg"class="thickbox">
                   <img alt="点击看大图" src="image/look.gif" />
                </a>
            </span>
		
		</div>
		<div class="pro_detail_right">
			<h4>Black Ablum2</h4>
			<p>大人气ideolo的个人东方同人本Black Ablum的第二作
			<br>同样是无比值得收藏的一本

             <br>40元照旧，拍下即发。

           <br>请继续支持萌少女领域的刊物，您的每一分关注都是我们制作精品的动力m(_ _)m
          </p>
			
		</div>
	</div>
</div>
<!--主体内容结束-->
</body>
</html>
