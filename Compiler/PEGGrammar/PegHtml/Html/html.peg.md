
* 怎么解析带有< >字符的html

* 怎么能做到VS这样的错误提示

* unknown 虽然写的是自己想要的效果，但是没搞明白之前的为什么不work,因为下面的写法会把`</div>`也识别出来

* HtmlBlockType link要写在li前面

    ```
    HtmlBlockOpenUnknown : '<' Spnl (!'>' .)* Spnl  HtmlAttribute* '>';
    ```

* HtmlBlockUnknown 把 可以不写 end tag 的忽略掉了

* VS连ul没有end都可以解析，怎么做到的