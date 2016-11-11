[强制退出cmd命令](https://zhidao.baidu.com/question/115435059.html)

##Exit

退出控制台的指令 ：Exit
指令效果：关闭控制台，或退出有控制台执行的批处理代码。  

该指令官方给出的解释如下：
退出 CMD.EXE 程序(命令解释器)或当前批处理脚本

```
EXIT [/B] [exitCode
```

/B          指定要退出当前批处理脚本而不是 CMD.EXE。如果从一个
批处理脚本外执行，则会退出 CMD.EX
exitCode    指定一个数字号码。如果指定了 /B，将 ERRORLEVEL
设成那个数字。如果退出 CMD.EXE，则用那个数字设置
过程退出代码。


##快捷键

`Ctrl+C` 或者 `Ctrl+Z`都可以试一下