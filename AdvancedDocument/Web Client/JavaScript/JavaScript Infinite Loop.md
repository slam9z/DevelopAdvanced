
在没有添加parent != this代码之前，导致edge页面卡顿，很久才能打开，IE倒是正常。
infinite loop了。

```js
<script type="text/javascript">

    function resizeIframe() {
        var obj = $("#fileframe").get(0);

        obj.style.height = obj.contentWindow.document.body.scrollHeight + 'px';

        //parent != this会循环调用
        if (parent != this && parent.resizeIframe) {
            parent.resizeIframe();
        }
    }
</script>
```