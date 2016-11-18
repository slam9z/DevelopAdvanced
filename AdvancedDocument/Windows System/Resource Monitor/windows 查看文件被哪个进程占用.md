[windows 查看文件被哪个进程占用](http://www.cnblogs.com/youxin/p/3560111.html)　　

##查看占用

在开始菜单中的搜索框内输入“资源监视器”，回车，打开“资源监视器”。

看下图，在“资源监视器”界面中，点击第二个选项卡“CPU”。在“关联的句柄”右侧搜索框内输入文件名称，点击右侧下拉箭头，就可以查看该文件被那几个程序占用了。

选中程序，右击选择结束进程。

 
现在就可以删除文件了。结束系统进程前最好查一下，看看能不能结束，免得出现问题，那就得不偿失了。
如果不是win7怎么办？可以下载微软的一个软件http://technet.microsoft.com/zh-cn/sysinternals/bb896653.aspx  process explorer
或者命令行工具http://technet.microsoft.com/en-us/sysinternals/bb896655.aspx handler，这2个工具都可以输入文件名来查询相关的进程id。