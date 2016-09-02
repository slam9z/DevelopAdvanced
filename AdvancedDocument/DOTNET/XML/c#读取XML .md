[c#读取XML ](http://www.cnblogs.com/a1656344531/archive/2012/11/28/2792863.html)

[XML Documentation Tutorial](https://msdn.microsoft.com/en-us/library/aa288481(v=vs.71).aspx)

XML文件是一种常用的文件格式，例如WinForm里面的app.config以及Web程序中的web.config文件，还有许多重要的场所都有它的身影。
Xml是Internet环境中跨平台的，依赖于内容的技术，是当前处理结构化文档信息的有力工具。
XML是一种简单的数据存储语言，使用一系列简单的标记描述数据，而这些标记可以用方便的方式建立，虽然XML占用的空间比二进制数据要占用更多的空间，
但XML极其简单易于掌握和使用。微软也提供了一系列类库来倒帮助我们在应用程序中存储XML文件。

“在程序中访问进而操作XML文件一般有两种模型，分别是使用DOM（文档对象模型）和流模型，使用DOM的好处在于它允许编辑和更新XML文档，
可以随机访问文档中的数据，可以使用XPath查询，但是，DOM的缺点在于它需要一次性的加载整个文档到内存中，对于大型的文档，这会造成资源问题
。流模型很好的解决了这个问题，因为它对XML文件的访问采用的是流的概念
，也就是说，任何时候在内存中只有当前节点，但它也有它的不足，它是只读的，仅向前的，不能在文档中执行向后导航操作。”
具体参见在Visual C#中使用XML指南之读取XML)

下面我将介绍三种常用的读取XML文件的方法。分别是 

1. 使用 XmlDocument 
2. 使用 XmlTextReader   
3. 使用 Linq to Xml
