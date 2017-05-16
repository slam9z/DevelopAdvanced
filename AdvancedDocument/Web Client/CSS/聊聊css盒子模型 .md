[聊聊css盒子模型 ](http://www.cnblogs.com/WebShare-hilda/p/4684161.html)


##css盒子模型原理：

在网页设计中常听的属性名：内容(content)、填充/内边距(padding)、边框(border)、外边距(margin)， CSS盒子模式都具备这些属性。

这些属性我们可以把它转移到我们日常生活中的盒子（箱子）上来理解，日常生活中所见的盒子也就是能装东西的一种箱子，也具有这些属性，
所以叫它盒子模式。

CSS中， Box Model叫盒子模型（或框模型），Box Model规定了元素内容（element content）、内边距（padding）、边框（border）
 和 外边距（margin） 的方式。


##css盒子尺寸的计算：

我们通过给高宽赋值，来定义content(内容)的高度和宽度。如果没有做任何声明，那么高度和宽度的默认值将是自动(auto)。
即在css中给一个块级元素的width和height属性赋值时比如div{width ：200px; height: 200px}时，其中的width 
和height只是对content部分设置的，即上图中content区域的长和宽。而不是内容，内边距，边框的总和
（但在IE的早期版本包括IE6中，盒子模型的width和height却是内容+内边距+边框的总和，尽管符合人们思考的逻辑习惯，
但是不符合规范，造成了很多兼容性问题。）
