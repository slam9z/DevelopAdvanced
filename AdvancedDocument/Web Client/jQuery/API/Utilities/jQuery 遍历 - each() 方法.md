[jQuery 遍历 - each() 方法](http://www.w3school.com.cn/jquery/traversing_each.asp)

实例
输出每个 li 元素的文本：

```js
$("button").click(function(){
  $("li").each(function(){
    alert($(this).text())
  });
});
```

定义和用法
each() 方法规定为每个匹配元素规定运行的函数。
提示：返回 false 可用于及早停止循环。
语法

```
$(selector).each(function(index,element))
```