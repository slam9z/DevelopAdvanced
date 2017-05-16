
##Table width algorithms: the 'table-layout' property

```
'table-layout' 
Value:  
auto | fixed | inherit 
Initial:  
auto 
```

###Fixed table layout

目前发现的问题，一旦cell设置了大小就不会改变，但是width%下没有设置width的会被压缩到很小。

Fixed布局真的很坑,除非没有办法就不要用这个了。

如果内容比较少可能Fixed比较适合。


###Automatic table layout

按cell的内容显示保证每个cell都能显示，但是cell里面的button会浮动。

默认是设置width，内容过成的时候会被撑大。

而且如果表格很多空间，也会比实际大。


##Control

使用div把button包装起来，td layout控制起来太复杂了。
使用span控制文字显示，使用div控制元素集合显示。


搞了这么久终于基本上掌握了这个属性，十分麻烦，思维方式有点问题。