[do not wrap button groups within tables](https://github.com/twbs/bootstrap/issues/9939)


##commented 

Unfortunately there's no way around this given that the buttons are floated—they'll break if there is
no space to place them side-by-side. Your best bet is a fixed width, or playing with some width and 
table-layout: fixed to prevent wrapping.


##commented 


Why not change bootstrap to use display: inline-block and white-space: nowrap on btn inside btn-group
instead of using float: left? Overwriting the bootstrap css with this works fine for me in all modern browsers.

```css
.btn-group {
  white-space: nowrap;
  .btn {
    float: none;
    display: inline-block;
  }
}
```


##myAnswer

使用div把button包装起来，td layout控制起来太复杂了。
使用span控制文字显示，使用div控制元素集合显示。

