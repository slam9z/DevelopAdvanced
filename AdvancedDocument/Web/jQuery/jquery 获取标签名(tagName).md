[jquery 获取标签名(tagName)](http://www.cnblogs.com/youring2/archive/2012/08/14/2638415.html)


如果是为了取到tagName后再进行判断，那直接用下面的代码会更方便：

$(element).is('input')

如果是要取到标签用作到别的地方，可以使用一下代码：

$(element)[0].tagName
或：
$(element).get(0).tagName

