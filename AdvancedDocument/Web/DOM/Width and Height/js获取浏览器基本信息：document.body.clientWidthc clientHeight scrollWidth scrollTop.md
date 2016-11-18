[js获取浏览器基本信息：document.body.clientWidth/clientHeight/scrollWidth/scrollTop](http://blog.csdn.net/zjlovety/article/details/6641644)


网页可见区域宽：document.body.clientWidth
网页可见区域高：document.body.clientHeight
网页可见区域宽：document.body.offsetWidth (包括边线的宽)
网页可见区域高：document.body.offsetHeight (包括边线的宽)
网页正文全文宽：document.body.scrollWidth
网页正文全文高：document.body.scrollHeight
网页被卷去的高：document.body.scrollTop
网页被卷去的左：document.body.scrollLeft
网页正文部分上：window.screenTop
网页正文部分左：window.screenLeft
屏幕分辨率的高：window.screen.height
屏幕分辨率的宽：window.screen.width
屏幕可用工作区高度：window.screen.availHeight
屏幕可用工作区宽度：window.screen.availWidth
HTML精确定位:scrollLeft,scrollWidth,clientWidth,offsetWidth
scrollHeight: 获取对象的滚动高度。
scrollLeft:设置或获取位于对象左边界和窗口中目前可见内容的最左端之间的距离
scrollTop:设置或获取位于对象最顶端和窗口中可见内容的最顶端之间的距离
scrollWidth:获取对象的滚动宽度
offsetHeight:获取对象相对于版面或由父坐标 offsetParent 属性指定的父坐标的高度
offsetLeft:获取对象相对于版面或由 offsetParent 属性指定的父坐标的计算左侧位置
offsetTop:获取对象相对于版面或由 offsetTop 属性指定的父坐标的计算顶端位置
event.clientX 相对文档的水平座标
event.clientY 相对文档的垂直座标
event.offsetX 相对容器的水平坐标
event.offsetY 相对容器的垂直坐标
document.documentElement.scrollTop 垂直方向滚动的值
event.clientX+document.documentElement.scrollTop 相对文档的水平座标+垂直方向滚动的量

IE，FireFox 差异如下：
IE6.0、FF1.06+：
clientWidth = width + padding
clientHeight = height + padding
offsetWidth = width + padding + border
offsetHeight = height + padding + border
IE5.0/5.5：
clientWidth = width - border
clientHeight = height - border
offsetWidth = width
offsetHeight = height
(需要提一下：CSS中的margin属性，与clientWidth、offsetWidth、clientHeight、offsetHeight均无关)
网页可见区域宽： document.body.clientWidth
网页可见区域高： document.body.clientHeight
网页可见区域宽： document.body.offsetWidth (包括边线的宽)
网页可见区域高： document.body.offsetHeight (包括边线的高)
网页正文全文宽： document.body.scrollWidth
网页正文全文高： document.body.scrollHeight
网页被卷去的高： document.body.scrollTop
网页被卷去的左： document.body.scrollLeft
网页正文部分上： window.screenTop
网页正文部分左： window.screenLeft
屏幕分辨率的高： window.screen.height
屏幕分辨率的宽： window.screen.width
屏幕可用工作区高度： window.screen.availHeight
屏幕可用工作区宽度： window.screen.availWidth
－－－－－－－－－－－－－－－－－－－
技术要点
本节代码主要使用了Document对象关于窗口的一些属性，这些属性的主要功能和用法如下。
要得到窗口的尺寸，对于不同的浏览器，需要使用不同的属性和方法：若要检测窗口的真实尺寸，在Netscape下需要使用Window的属性；在 IE下需要深入Document内部对body进行检测；在DOM环境下，若要得到窗口的尺寸，需要注意根元素的尺寸，而不是元素。
Window对象的innerWidth属性包含当前窗口的内部宽度。Window对象的innerHeight属性包含当前窗口的内部高度。
Document对象的body属性对应HTML文档的标签。Document对象的documentElement属性则表示HTML文档的根节点。
document.body.clientHeight表示HTML文档所在窗口的当前高度。document.body. clientWidth表示HTML文档所在窗口的当前宽度。

关于获取各种浏览器可见窗口大小的一点点研究。
在我本地测试当中：在IE、FireFox、Opera下都可以使用
document.body.clientWidth
document.body.clientHeight即可获得，很简单，很方便。
而在公司项目当中：Opera仍然使用
document.body.clientWidth
document.body.clientHeight
可是IE和FireFox则使用
document.documentElement.clientWidth
document.documentElement.clientHeight
原来是W3C的标准在作怪啊http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
如果在页面中添加这行标记的话
在IE中：document.body.clientWidth ==> BODY对象宽度
document.body.clientHeight ==> BODY对象高度
document.documentElement.clientWidth ==> 可见区域宽度
document.documentElement.clientHeight ==> 可见区域高度
在FireFox中：document.body.clientWidth ==> BODY对象宽度
document.body.clientHeight ==> BODY对象高度
document.documentElement.clientWidth ==> 可见区域宽度
document.documentElement.clientHeight ==> 可见区域高度?
在Opera中： document.body.clientWidth ==> 可见区域宽度
document.body.clientHeight ==> 可见区域高度
document.documentElement.clientWidth ==> 页面对象宽度（即BODY对象宽度加上Margin宽）document.documentElement.clientHeight ==> 页面对象高度（即BODY对象高度加上Margin高）
而如果没有定义W3C的标准，
则IE为：document.documentElement.clientWidth ==> 0
document.documentElement.clientHeight ==> 0
FireFox为:document.documentElement.clientWidth ==> 页面对象宽度（即BODY对象宽度加上Margin宽）
document.documentElement.clientHeight ==> 页面对象高度（即BODY对象高度加上Margin高）
Opera为：document.documentElement.clientWidth ==> 页面对象宽度（即BODY对象宽度加上Margin宽）
document.documentElement.clientHeight ==> 页面对象高度（即BODY对象高度加上Margin高）
真是一件麻烦事情，其实就开发来看，宁可少一些对象和方法，不使用最新的标准要方便许多啊。