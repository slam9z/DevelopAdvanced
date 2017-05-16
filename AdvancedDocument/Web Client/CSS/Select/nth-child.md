[nth-child](https://developer.mozilla.org/en-US/docs/Web/CSS/:nth-child)

##定义和用法

:nth-child(n) 选择器匹配属于其父元素的第 N 个子元素，不论元素的类型。
n 可以是数字、关键词或公式。
提示：请参阅 :nth-of-type() 选择器，该选择器选取父元素的第 N 个指定类型的子元素。

##理解

`span:nth-child(1)`就是从`span`的parent开始的第1个元素，如果第一个元素不是`span`不会生效。

但是 `nth-child(even)`不会存在这种现象

##Example selectors

Example selectors
tr:nth-child(2n+1)
Represents the odd rows of an HTML table.
tr:nth-child(odd)
Represents the odd rows of an HTML table.
tr:nth-child(2n)
Represents the even rows of an HTML table.
tr:nth-child(even)
Represents the even rows of an HTML table.
span:nth-child(0n+1)
Represents a span element which is the first child of its parent; this is the same as the :first-child selector.
span:nth-child(1)
Equivalent to the above.
span:nth-child(-n+3)
Matches if the element is one of the first three children of its parent and also a span.