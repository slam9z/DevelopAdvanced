[CSS - Expand child DIV height to parent's height](http://stackoverflow.com/questions/4804581/css-expand-child-div-height-to-parents-height)

##answer

A common solution to this problem uses absolute positioning or cropped floats, but these are tricky in that they require extensive tuning if your columns change in number+size, and that you need to make sure your "main" column is always the longest. Instead, I'd suggest you use one of three more robust solutions:

1. display: flex: by far the simplest & best solution and very flexible - but unsupported by IE9 and older.
1. table or display: table: very simple, very compatible (pretty much every browser ever), quite flexible.
1. display: inline-block; width:50% with a negative margin hack: quite simple, but column-bottom borders are a little tricky.

##1. display:flex

This is really simple, and it's easy to adapt to more complex or more detailed layouts - but flexbox is only supported by IE10 or later (in addition to other modern browsers).
Example: http://output.jsbin.com/hetunujuma/1
Relevant html: 
<div class="parent"><div>column 1</div><div>column 2</div></div>
Relevant css:
.parent { display: -ms-flex; display: -webkit-flex; display: flex; }
.parent>div { flex:1; }
Flexbox has support for a lot more options, but to simply have any number of columns the above suffices!

##2.<table> or display: table

A simple & extremely compatible way to do this is to use a table - I'd recommend you try that first if you need old-IE support. You're dealing with columns; divs + floats simply aren't the best way to do that (not to mention the fact that multiple levels of nested divs just to hack around css limitations is hardly more "semantic" than just using a simple table). If you do not wish to use the table element, consider css display: table (unsupported by IE7 and older). 
Example: http://jsfiddle.net/emn13/7FFp3/
Relevant html: (but consider using a plain <table> instead)
<div class="parent"><div>column 1</div><div>column 2</div></div>
Relevant css:
.parent { display: table; }
.parent > div {display: table-cell; width:50%; }
/*omit width:50% for auto-scaled column widths*/
This approach is far more robust than using overflow:hidden with floats. You can add pretty much any number of columns; you can have them auto-scale if you want; and you retain compatibility with ancient browsers. Unlike the float solution requires, you also don't need to know beforehand which column is longest; the height scales just fine.
KISS: don't use float hacks unless you specifically need to. If IE7 is an issue, I'd still pick a plain table with semantic columns over a hard-to-maintain, less flexible trick-CSS solution any day.
By the way, if you need your layout to be responsive (e.g. no columns on small mobile phones) you can use a @media query to fall back to plain block layout for small screen widths - this works whether you use <table> or any other display: table element.

##3. display:inline block with a negative margin hack.

Another alternative is to use display:inline block.
Example: http://jsbin.com/ovuqes/2/edit
Relevant html: (the absence of spaces between the div tags is significant!)
<div class="parent"><div><div>column 1</div></div><div><div>column 2</div></div></div>
Relevant css:
.parent { 
    position: relative; width: 100%; white-space: nowrap; overflow: hidden; 
}

.parent>div { 
    display:inline-block; width:50%; white-space:normal; vertical-align:top; 
}

.parent>div>div {
    padding-bottom: 32768px; margin-bottom: -32768px; 
}
This is slightly tricky, and the negative margin means that the "true" bottom of the columns is obscured. This in turn means you can't position anything relative to the bottom of those columns because that's cut off by overflow: hidden. Note that in addition to inline-blocks, you can achieve a similar effect with floats.

TL;DR: use flexbox if you can ignore IE9 and older; otherwise try a (css) table. If neither of those options work for you, there are negative margin hacks, but these can cause weird display issues that are easy to miss during development, and there are layout limitations you need to be aware of