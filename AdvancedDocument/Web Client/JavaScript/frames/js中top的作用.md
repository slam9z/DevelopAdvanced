[js中top的作用](http://blog.csdn.net/dongwujing/article/details/7764801)

每一个WINDOW对象，不论他是常规HTML页面。框架集页面。子框架还是孙子框架，都具有一个TOP属性。
这个属性返回对载入浏览器得最顶层WINDOE对象得引用； 

如果WINDOW对象是常规HTML页面，TOP就是SELF 
顶层框架及页面，
子框架，TOP指向框架得父亲，也就是说，下面两个表达式是等效的； 

SELF.TOP 
SELF.PARENT 

如果WINDOW对象是子框架，TOP指向框架得祖父。也就是说下面两个表达式是等效得 
self.top 
self.parent.parent 

这可能提醒了你一个减少按键得好方法,尤其是当你认为能为能仅适用TOP本身是
（那就是说，使用TOP本身来代替SELF.TOP或者WINDOW.TOP），这虽是可行的，但是应该特别注意：
必须确保TOP真正指向你的顶层框架  

##神奇的调用

```       
<frameset rows="80,*" frameborder="no" framespacing="0">
    <frame src="Header.aspx" noresize="noresize" scrolling="no"  name='headermenu' />
    <frame src="Menu.aspx?menugroup=c" noresize="noresize" scrolling="no" name='menu' >
</frameset>
```

Header里面可以使用top的属性调用改页面的js方法。


然后可以通过 window.frames["menu"]执行Menu的方法。