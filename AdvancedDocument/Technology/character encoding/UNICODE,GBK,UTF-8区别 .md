[UNICODE,GBK,UTF-8区别 ](http://www.cnblogs.com/cy163/archive/2007/05/31/766886.html)


##UNICODE,GBK,UTF-8区别

    简单来说，unicode，gbk和大五码就是编码的值，而utf-8,uft-16之类就是这个值的表现形式．而前面那三种编码是一兼容的，同一个汉字，那三个码值是完全不一样的．如＂汉＂的uncode值与gbk就是不一样的，假设uncode为a040，gbk为b030，而uft-8码，就是把那个值表现的形式．utf-8码完全只针对uncode来组织的，如果ＧＢＫ要转ＵＴＦ－８必须先转uncode码，再转utf-8就ＯＫ了．
详细的就见下面转的这篇文章．

谈谈Unicode编码，简要解释UCS、UTF、BMP、BOM等名词
这是一篇程序员写给程序员的趣味读物。所谓趣味是指可以比较轻松地了解一些原来不清楚的概念，增进知识，类似于打RPG游戏的升级。整理这篇文章的动机是两个问题：

问题一：
使用Windows记事本的“另存为”，可以在GBK、Unicode、Unicode big endian和UTF-8这几种编码方式间相互转换。同样是txt文件，Windows是怎样识别编码方式的呢？

我很早前就发现Unicode、Unicode big endian和UTF-8编码的txt文件的开头会多出几个字节，分别是FF、FE（Unicode）,FE、FF（Unicode big endian）,EF、BB、BF（UTF-8）。但这些标记是基于什么标准呢？
