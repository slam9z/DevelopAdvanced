[URL最大长度问题 ](http://blog.csdn.net/yang_5/article/details/8174889#comments)


1. 各个浏览器对URL的长度有
2. IIS 7 对 Query String 有长度限制；默认：2048；

根据网上的资料(推荐一篇博客：<http://www.cnblogs.com/henryhappier/archive/2010/10/09/1846554.html>)
了解到各个浏览器对URL的长度现在如下：

1. IE浏览器对URL的长度现限制为2048字节(自己测试最多为2047字
2. 360极速浏览器对URL的长度限制为2118
3. Firefox(Browser)对URL的长度限制为65536
4. Safari(Browser)对URL的长度限制为80000
5. Opera(Browser)对URL的长度限制为190000
6. Google(chrome)对URL的长度限制为8182字节。

这里，我只测试过IE浏览器和360极速浏览器，其它浏览器来自于网上的资料。

另外提醒一下大家，URL中，一个汉字通过不同的编码方式大小也不一样。