[Fixed Table Cell Width](http://stackoverflow.com/questions/4185814/fixed-table-cell-width)

##Question


A lot of people still use tables to layout controls, data etc. - one example of this is the popular jqGrid. 
However, there is some magic happening that I cant seem to fathom (its tables for crying out loud, how much magic could there possibly be?)

How is it possible to set a table's column width and have it obeyed like jqGrid does!?
 If I try to replicate this, even if I set every <td style='width: 20px'>, as soon as the content of one of those cells is greater than 20px, the cell expands!
Any ideas or insights?



##Answer

###New 

You could try using the <col> tag manage table styling for all rows but you will need to set the table-layout:fixed style on the
 <table> or the tables css class and set the overflow style for the cells

https://developer.mozilla.org/en-US/docs/Web/HTML/Element/col

```html
<table class="fixed">
    <col width="20px" />
    <col width="30px" />
    <col width="40px" />
    <tr>
        <td>text</td>
        <td>text</td>
        <td>text</td>
    </tr>
</table>
```
and this be your CSS

```css
table.fixed { table-layout:fixed; }
table.fixed td { overflow: hidden; }
```

###Old

Now in HTML5/CSS3 we have better solution for the problem. In my opinion this purely CSS solution is recommended:

```css
table.fixed {table-layout:fixed; width:90px;}/*Setting the table width is important!*/
table.fixed td {overflow:hidden;}/*Hide text outside the cell.*/
table.fixed td:nth-of-type(1) {width:20px;}/*Setting the width of column 1.*/
table.fixed td:nth-of-type(2) {width:30px;}/*Setting the width of column 2.*/
table.fixed td:nth-of-type(3) {width:40px;}/*Setting the width of column 3.*/
```

```html
<table class="fixed">
    <tr>
        <td>Veryverylongtext</td>
        <td>Actuallythistextismuchlongeeeeeer</td>
        <td>We should use spaces tooooooooooooo</td>
    </tr>
</table>
```

Run code snippetExpand snippet

You need to set the table's width even in haunter's solution. Otherwise it doesn't work.
Also a new CSS3 feature that vsync suggested is: word-break:break-all;.
 This will break the words without spaces in them to multiple lines too. Just modify the code like this:

```css
table.fixed { table-layout:fixed; width:90px; word-break:break-all;}
```

##MyCode

```html
<table  style="table-layout:fixed;width:100%;word-wrap:break-word">
    <thead>
         <tr>
            <th  width="40px">
                    <span class="table-header">序号</span>
            </th>
```

word-wrap:break-word

强制换号。

table-layout:fixed

固定布局。

width:100%

设置宽度。


