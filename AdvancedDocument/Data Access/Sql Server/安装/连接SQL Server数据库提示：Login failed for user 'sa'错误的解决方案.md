[连接SQL Server数据库提示：Login failed for user 'sa'错误的解决方案](http://www.cr173.com/html/19471_1.html)

## 2刚装完SQL Server 2008 Express，尝试使用sa账号登录，但总是出现Login failed for user 'sa' 错误。觉得应该是SQL Server的认证模式没设对，SQL Server Express默认是Windows Authentication模式，我必须设成Mixed Authentication Mode才可以。

所以查了一下文档，微软的官方文档说只要把sa账号enable就可以了，但试过后同样的错误。后来截取了SQL Server Management Studio的脚本才发现还必须要改一个注册表键值才行。

在我的机器上该键值是：HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\MSSQL10.SQLEXPRESS\MSSQLServer\LoginMode

1 - Windows Authentication Mode

2 - Mixed Authentication Mode

改成2以后就能用sa账号登录了。

* 补充一下，必须得重启SQL Server的服务改动才能有效。