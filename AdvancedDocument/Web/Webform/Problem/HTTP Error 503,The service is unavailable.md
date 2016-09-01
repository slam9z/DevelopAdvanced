HTTP Error 503,The service is unavailable



[重启IIS服务的方法 ](http://www.cnblogs.com/weixing/archive/2012/03/23/2413667.html)


WINDOWS提供WEB服务的IIS有时候会出现访问过大导致网站打不开，这时重启IIS是最好的选择。

* 1、界面操作
    打开“控制面板”->“管理工具”->“服务”。找到“IIS Admin Service” 右键点击“重新启动” 弹出 “停止其它服务” 窗口，点击“是”。

* 2、Net 命令操作
    点击 “开始”->“运行”，输入cmd 打开命令窗口；
    输入 net stop iisadmin /y  回车停止IIS；
    再输入 net start iisadmin  回车启动IIS；
    再输入 net start w3svc 回车WEB服务。

* 3、IISReset 命令操作
    点击 “开始”->“运行”，输入iisreset 回车。