[Jquery中的this与$(this)](http://www.cnblogs.com/iceWolf/archive/2009/08/27/1555138.html)

序言：在使用jquery操作js时，经常整不明白this与$(this)。抽空仔细测试了一把，记录下来以供在忘记的时候拉出来参考参考！

## $(this)生成的是什么

$()生成的是什么呢？实际上$()=jquery()，那么也就是说返回的是一个jquery的对象。

题外话：通常我们为了简便直接使用$(),实际上,该函数省略了一个参数context,即$(selector)=$(selector,document).如果指定context，可以指定context为一个dom元素集或者jquery对象。

那么依照，$()返回的是jquery对象这一结论，我们可以得出$(this)得到的是一个jquery对象.我们可以使用万能的alert()方法打印出一个对象：

alert($('#btn'));显示的结果：

test 该图红色框勾选出来的是一个object，不用考虑，该object自然是jquery的对象咯。也即是说我们用通过$('#btn')来调用jquery的方法和属性等。
 
## this代表什么？

this，编程的人都知道this表示上下文所处的这个对象，这个自然是不错的，可是这个对象到底是个什么对象呢？加入js里面也有getType的话返回的值会是什么呢？其实js里面不需要使用getType,因为我们有万能的alert.请看看下面的代码：

```js
$('#btn').bind("click",function(){

    alert(this); 

    alert($(this)); 

});
```

根据我们的经验（因为$()生成的是jquery的对象嘛），this自然是一个jquery的对象咯。可是我们看看返回的结果：

test1返回的是什么？【object HTMLInputElement】——伟大的html对象，嘿嘿。所以我们通常在直接使用this.val()或者直接通过this来调用jquery所特有的方法或属性的时候会报错误：mistake  为什么呢？明知故问！html对象当然“不包含属性或方法”了。那么为什么在一个jquery对象的上下文中调用this返回的是一个html对象而不是jquery对象 呢？翻遍

jquery的api文档，貌似jquery中并未对this这一关键字进行过特殊“处理”，也就是说这里this是js中的，而不是jquery重新定义了的。so...当然这仅仅是我自己的想法，如果有对此更了解的朋友可以留言更正。而我们再看一下以上代码中alert($(this));的返回，自然是jquery的对象了，在此调用jquery特有的方法和属性，完全没有问题。

结论：

this，表示当前的上下文对象是一个html对象，可以调用html对象所拥有的属性，方法

$(this),代表的上下文对象是一个jquery的上下文对象，可以调用jquery的方法和属性值。 