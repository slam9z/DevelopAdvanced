[table-layout](https://developer.mozilla.org/en-US/docs/Web/CSS/table-layout)

##Values

###auto

An automatic table layout algorithm is commonly used by most browsers for table layout. The 
width of the table and its cells depends on the content thereof.

###fixed

Table and column widths are set by the widths of table and col elements or by the width of the 
first row of cells. Cells in subsequent rows do not affect column widths.

Under the "fixed" layout method, the entire table can be rendered once the first table row has 
been downloaded and analyzed. This can speed up rendering time over the "automatic" layout method,
but subsequent cell content may not fit in the column widths provided. Any cell that has content
that overflows uses the *overflow* property to determine whether to clip the overflow content.