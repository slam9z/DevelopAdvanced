[win7设置新建文本文档默认编码 ](http://blog.csdn.net/inowcome/article/details/6880771)

1. 打开记事本新建一个空白的文本文档，不输入任何文字，然后保存此文档，在“另存为”对话框中将编码
由默认的 ANSI 修改为 Unicode 或 UTF-8，接着为文件取名，在此假设将新文档命名为 UNICODE.TXT。
 
2. 将 UNICODE.TXT 复制至隐含的系统文件夹 C:\Windows\ShellNew。
 
3. 打开注册表编辑器定位至：HKEY_CLASSES_ROOT\.txt\ShellNew，新建名为 FileName 的字符串值，
将此字符串值设置为 C:\Windows\ShellNew\UNICODE.TXT。
 
上述做法的目的是将 .TXT 文本文件的“新建”模板 ShellNew 设置为我们自定义的以 Unicode 编码保存的
空白文本文件。这样，如果我们再使用资源管理器右键菜单中的“新建”－“文本文档”建立新文本文档，
Windows 就会自动以 C:\Windows\ShellNew\UNICODE.TXT 做为模板来新建文本文档，文档的默认编码
就变为 Unicode 了。
 
不过，此方法只适用于以“新建”右键菜单方式建立新文本文档。假如我们首先通过开始菜单启动记事本，
然后再新建文本文档，C:\Windows\ShellNew\UNICODE.TXT 模板便不会被使用，新建的文本文档依然
还将使用默认的 ANSI 编码。

>不知道是操作系统的原因还是之前人写错了，在win10中FileName的值是UNICODE.TXT。