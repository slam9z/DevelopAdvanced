[a href=#与 a href=javascript:void(0) 的区别 ](http://blog.csdn.net/fightplane/article/details/5190037)


[<a href=”#”>与 <a href=”javascript:void(0)” 的区别](http://www.cnblogs.com/lgzslf/archive/2011/10/20/2218917.html)

`<a href=”#”>`中的“#”其实是锚点的意思，默认为#top，所以当页面比较长的时候，使用这种方式会让页面刷新到页首（页面的最上部）

`javascript:void(0)`其实是一个死链接，当使用超链接处理JS脚本（一般是click），又不想回到页首（这种情况可以理解为局部刷新）时经常使用

void 操作符的用法格式如下：

```
1. javascript:void (expression_r_r)

2. javascript:void expression_r_r
```

一般是带上（），这样可读性更强

小结：

当需要整体刷新时，使用#

当实现局部刷新时，使用如下几种方式：

```html
<a href="####"></a>

<a href="javascript:void(0)"></a>

<a href="javascript:void(null)"></a>

<a href="#" onclick="return false"></a>
```