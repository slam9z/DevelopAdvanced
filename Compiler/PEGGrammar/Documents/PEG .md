## PEG

* 通过@添加错误处理

* @好像不能和+号一块

## PEG markdown

* Plain是不能包含特殊字符的，要不然没法匹配了。

* `\'"`这3个C#特殊符号要怎么表示!

* 非常容易写死循环需要特别注意,允许为空操作的不能再加*,否则必定死循环。

* 生成的代码调试起来太痛苦了

* 没有文件结束符，解析link出现问题了

* 太复杂了，不太想写了，这就是尴尬的地方，总感觉自己写的东西垃圾，不符合标准。自己写的总是要比自己知道的更浅。


## PEG html


* 怎么解析带有< >字符的html

* 怎么能做到VS这样的错误提示

* unknown 虽然写的是自己想要的效果，但是没搞明白之前的为什么不work,因为下面的写法会把`</div>`也识别出来

* HtmlBlockType link要写在li前面

    ```
    HtmlBlockOpenUnknown : '<' Spnl (!'>' .)* Spnl  HtmlAttribute* '>';
    ```

* HtmlBlockUnknown 把 可以不写 end tag 的忽略掉了

* VS连ul没有end都可以解析，怎么做到的, 浏览器会自动补全。

*  `<div id="answer-21123191" class="answer" data-answerid="21123191" itemscope itemtype="http://schema.org/Answer"></div>`
itemscope怎么这么坑,这还是特殊语法。
