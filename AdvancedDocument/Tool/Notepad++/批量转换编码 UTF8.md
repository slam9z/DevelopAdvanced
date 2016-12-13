[批量转换编码 (gbk -> utf8)](http://www.cnblogs.com/yili16438/p/3801013.html)


使用 Notepad++ 批量的转换文件编码：[Mass convert a project to UTF-8 using Notepad++](http://pw999.wordpress.com/2013/08/19/mass-convert-a-project-to-utf-8-using-notepad/)

步骤如下：

1. 一般 Noptepad++ 安装完后已经自带了一个 Plugin Manger ，在 Plugins 菜单下面可见，如果没有，自行安装. 
2. 打开 Plugin Manager ，在 Available 下面的列表中找到 Python Script ，Install，Restart. 
3. 打开 Python Script，New Script

脚本如下（其中的文件后缀可根据需要修改）：


```py
import os;
import sys;
path="D:\\Documents\\Unity\\"
for root, dirs, files in os.walk(path):
    for file in files:
        if file.endswith(".cs") or file.endswith(".js") or file.endswith(".txt"):
            notepad.open(root + "\\" + file)
            console.write(root + "\\" + file+ "\r\n")
            notepad.runMenuCommand("Encoding", "Convert to UTF-8")
            notepad.save()
            notepad.close()
```