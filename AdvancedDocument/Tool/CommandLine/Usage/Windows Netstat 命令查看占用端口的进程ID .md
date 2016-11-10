[Windows Netstat 命令查看占用端口的进程ID ](http://lfei.org/windows-cmd-netstat-pid/)

在 Windows 中，有时候需要查看一个端口是否被占用，或者查找被占用的端口进程，然后结束它，来启动需要此端口的程序，
那么我们就需要用到 netstat 命令，打开 cmd 命令窗口输入：

```
netstat -ano | findstr ":4300"
```

其中 4300 是被占用的端口，结果如图：