[CSS display 属性](http://www.w3school.com.cn/cssref/pr_class_display.asp)

##定义和用法
display 属性规定元素应该生成的框的类型。


##说明

这个属性用于定义建立布局时元素生成的显示框类型。对于 HTML 等文档类型，如果使用 display 不谨慎会很危险,
因为可能违反 HTML 中已经定义的显示层次结构。对于 XML，由于 XML 没有内置的这种层次结构，所有 display 是绝对必要的。


##可能的值

|值       |描述           |
|---------|--------------|
|none|此元素不会被显示。|
|block|此元素将显示为块级元素，此元素前后会带有换行符。                     |
|inline|默认。此元素会被显示为内联元素，元素前后没有换行符。                 |
|inline-block|行内块元素。（CSS2.1 新增的值）                              |
|list-item|此元素会作为列表显示。                                          |
|run-in|此元素会根据上下文作为块级元素或内联元素显示。                       |
|compact|CSS 中有值 compact，不过由于缺乏广泛支持，已经从 CSS2.1 中删除。   |
|marker|CSS 中有值 marker，不过由于缺乏广泛支持，已经从 CSS2.1 中删除。     |
|table|此元素会作为块级表格来显示（类似 <table>），表格前后带有换行符。      |
|inline-table|此元素会作为内联表格来显示（类似 <table>），表格前后没有换行符|
|table-row-group|此元素会作为一个或多个行的分组来显示（类似 <tbody>）。     |
|table-header-group|此元素会作为一个或多个行的分组来显示（类似 <thead>）。  |
|table-footer-group|此元素会作为一个或多个行的分组来显示（类似 <tfoot>）。  |
|table-row|此元素会作为一个表格行显示（类似 <tr>）。                        |
|table-column-group|此元素会作为一个或多个列的分组来显示（类似 <colgroup>）。|
|table-column |此元素会作为一个单元格列显示（类似 <col>）                   |
|table-cell|此元素会作为一个表格单元格显示（类似 <td> 和 <th>）             |
|table-caption|此元素会作为一个表格标题显示（类似 <caption>）               |
|inherit|规定应该从父元素继承 display 属性的值。                            |     