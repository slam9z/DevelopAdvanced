[table标签中thead、tbody、tfoot的作用 ](http://www.cnblogs.com/sandwich/articles/3666541.html)

*这是个信仰问题，凡事尽可能更完美，所以我一定会添加*

为了让大表格(table)在下载的时候可以分段的显示,就是说在浏览器解析HTML时，table是作为一个整体解释的，使用TBODY可以优化显示。
如果表格很长，用tbody分段，可以一部分一部分地显示，不用等整个表格都下载完成。下载一块显示一块，表格巨大时有比较好的效果。

    tbody、tfoot、thead一般来说用得不是很多，对于比较复杂的页面，页面的排版用到了很多的表格，表格的结构也就相对的复杂了
，所以又将表格分割成三个部分：题头、正文和脚注。而这三部分分别用: thead, tbody, tfoot来标注。

* thead　表格的头        用来放标题之类的东西
* tbody　表格的身体    放数据本体 
* tfoot　 表格的脚       放表格的脚注之类

我觉得最直接的用处是：   

  TBODY包含行的内容下载完优先显示，不必等待表格结束.另外，还需要注意一个地方。表格行本来是从上向下显示的。但是，应用了thead/tbody/tfoot以后，
就“从头到脚”显示，不管你的行代码顺序如何。也就是说如果thead写在了tbody的后面，html显示时，还是以先thead后tbody显示。

  在做頁面的時候，經常要根據不同的操作來顯示或隱藏一個表格中的部分內容，隱藏一行直接用<tr>標簽，隱藏多行時用<tbody>就很方便。


```html
 <table> 
　   <thead> 
　　  <tr> 
　　　 <td> 
　　　 this text is in the thead. 
　　　 </td> 
　　  </tr> 
　   </thead> 
　   <tbody> 
　　  <tr> 
　　　 <td> 
　　　 this text is in the tbody. 
　　　 </td> 
　　  </tr> 
　   </tbody> 
　   <tfoot> 
　　  <tr> 
　　　 <td> 
　　　 this text is in the table footer. 
　　　 </td> 
　　  </tr> 
　   </tfoot> 
</table>
```