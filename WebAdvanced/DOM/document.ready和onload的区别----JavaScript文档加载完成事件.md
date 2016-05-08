[document.ready和onload的区别----JavaScript文档加载完成事件 ](http://www.cnblogs.com/jackson-leung/archive/2012/05/19/2509244.html)

页面加载完成有两种事件，一是ready，表示文档结构已经加载完成（不包含图片等非文字媒体文件），二是onload，
指示页 面包含图片等文件在内的所有元素都加载完成。(可以说：ready 在onload 前加载！！！)

我的理解： 一般样式控制的，比如图片大小控制放在onload 里面加载;

              而：jS事件触发的方法，可以在ready 里面加载;
