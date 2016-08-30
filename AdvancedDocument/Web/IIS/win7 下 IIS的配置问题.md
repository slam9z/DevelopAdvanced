[win7 下 IIS的配置问题 ](http://blog.csdn.net/hebeijg/article/details/6277985)

这人的英语一大堆错误

##浏览网页错误

在win7下安装了IIS，直接点浏览出现如下错误

###描述

http 错误500.19,-Internal server error，无法访问的请求野蛮，因为该页的相关配置数据无效，HTTP Error 500.19 - Internal Server Error
配置错误: 不能在此路径中使用此配置节。如果在父级别上锁定了该节，便会出现这种情况。锁定是默认设置的 (overrideModeDefault="Deny")，
或者是通过包含 overrideMode="Deny" 或旧有的 allowOverride="false" 的位置标记明确设置的。
 
###原因：可能是在安装IIS7的时候没有安装asp.net，IIS默认不安装ASP.NET。

解决办法：控制面板->程序和功能->打开或者关闭windows服务，在里面选择Internet信息服务，
win7默认不安装，选中，记得选中的时候必须选择，web服务（万维网服务）->应用程序开发功能里必须把asp.net选中，这样这个问题就不会出现了。