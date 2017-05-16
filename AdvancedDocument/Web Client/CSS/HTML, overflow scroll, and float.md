[HTML, overflow:scroll, and float](http://stackoverflow.com/questions/5926076/html-overflowscroll-and-float)

>要想动态的设置float元素显示几行。只有动态的计算出parent的width，感觉没有其它方法了，规范也是这样说的。

##answer

You need to:
Make the <li> also float.
Set fixed width to each <ul>.
Set fixed width to the containing <div>, enough to hold all the lists.
For example:

```css
ul { width: 250px; }
li { margin-left: 5px; }
ul, li { float: left;  }
div { overflow-x: scroll; width: 750px; }
```
