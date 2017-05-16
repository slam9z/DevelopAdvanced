[UEditor](http://ueditor.baidu.com/website/)


UEditor是由百度web前端研发部开发所见即所得富文本web编辑器，具有轻量，可定制，注重用户体验等特点，开源基于MIT协议，
允许自由使用和修改代码...


也算是看到两种的文件存储方式！

一个按照数据库，一个通过文件目录！

神奇的是都是在同一个系统里，而且还是自己弄的。


## flash上传


不知道为啥flash上传出问题了，`http请求错误` 服务端没有收到`get crossdomain.xml`的请求

之前的UEditor项目，只要部署到IIS `ueditor.cmbchinaucs.com` 就没问题了。


##之前留下的坑

之前注释了search，导致下面的js报错。

```html
//image.html
<div id="tabhead" class="tabhead">
    <span class="tab" data-content-id="remote"><var id="lang_tab_remote"></var></span>
    <span class="tab focus" data-content-id="upload"><var id="lang_tab_upload"></var></span>
    <span class="tab" data-content-id="online"><var id="lang_tab_online"></var></span>
    <span class="tab" style="display:none" data-content-id="search"><var id="lang_tab_search"></var></span>
</div>
```

```js
removeClasses: function (elm, classNames) {
    if (elm == null)
    {
        console.log(classNames);
        return;
    }
    classNames = utils.isArray(classNames) ? classNames :
}
```