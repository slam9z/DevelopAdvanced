[C#6.0新特性之字符串嵌入 String Interpolation ](http://www.cnblogs.com/davytitan/p/4988485.html)

6.0增加了 字符串嵌入值 的新语法糖。
以前我们做拼接的时候，一般这样写 

```C#
  var s = string.Format("this is a {0} !!!" , class1.p1);\
```

写一个两个的占位符还能记得住，要是长了，10几个的时候，可能就有点儿乱了。 
在VS2015下，就可以这么简单的写了  var s = $"this is a {class1.p1} !!!";   $相当于@这种转义符， ｛｝ 这才是重点。大括号中间，只要是
能正常执行的表达式就OK，哪怕是个函数都行。
 当然，如果一定要打印 ｛｝ 这两个 ，只需要  {{{class1.p1}}}  输出就是  {value}了。