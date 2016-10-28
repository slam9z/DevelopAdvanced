<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
<title>sasa shopping</title>

<link rel="stylesheet" href="styles/main.css" type="text/css" />
<link rel="stylesheet" href="styles/index.css" type="text/css" />
<link rel="stylesheet" href="styles/skin/skin_1.css" type="text/css" id="cssfile" />



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
<!--主体开始-->
<div id="content">
	<div class="content_left">
		<div class="global_module news">
			<h3>购物须知</h3>
			 1.&nbsp;&nbsp;在本店预约商品的买家，在到货后均会收到旺旺、手机短信、电子邮件等不同方式的补款通知。
			 <br>
			 2.&nbsp;&nbsp;预订金不可退，不可转订其他商品。
		    <br>

             3.&nbsp;&nbsp;请勿未经联系，直接拍下商品支付，本店有权取消该交易。

			
		</div>

		<div class="global_module news">
			<h3>联系方式</h3>
			<div class="scrollNews" >
				<ul>
					
					<li>QQ：772555190</li>
					<li>联系手机：15921628769</li>
					
				</ul>
			</div>


		
		</div>

			<div class="global_module news">
			<h3>工作时间</h3>
			<div class="scrollNews" >
				<ul>
					
					<li>周一至周日：10:30-24:00</li>
					<li>周六至周日：10:00-24:00</li>
					
				</ul>
			</div>


		
		</div>
 		
 		
	 </div>
	



	
	<div class="content_right">

	   <div class="ad" >
			 <ul class="slider" >
				<li><img src="image/0.jpg"/></li>
			
			  </ul>
			 
		</div>
	        <div class="global_module prolist">
		    <h3>商品列表</h3>
                <div  class="prolist_content">
                <ul>
                    <li>
                        <a href="1.asp"><img src="image/1.jpg" alt="" /></a><span>Black Album 02 </a></span><span> 一口价 40 元</span>
                    </li>
					 <li>
                        <a href="1.asp"><img src="image/2.jpg" alt="" /></a><span>Carnival Fantasy </a></span><span> 一口价 40 元</span>
                    </li>
					 <li>
                        <a href="1.asp"><img src="image/3.jpg" alt="" /></a><span>Black Album 01 </a></span><span> 一口价 40 元</span>
                    </li>
					 <li>
                        <a href="1.asp"><img src="image/4.jpg" width="160"  alt="" /></a><span>梦见 Drifting Dream- </a></span><span> 一口价 70 元</span>
                    </li>
					
				</ul>
			
         </div>
       </div> 

	    </div> 



</div>
<!--主体结束-->
</body>
</html>
