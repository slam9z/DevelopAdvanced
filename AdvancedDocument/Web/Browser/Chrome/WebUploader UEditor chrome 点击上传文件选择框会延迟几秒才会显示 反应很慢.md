


[文件上传插件uploadify点击按钮之后半天不弹出文件选择框](https://segmentfault.com/q/1010000007501929)

文件上传插件Uploadify在配置了fileTypeExts(文件类型)之后,点击上传按钮之后要过10多秒才弹出文件选择框

只有在fileTypeExts配置rar或者zip的时候会出现这个问题,配置png,gif,jpg等正常

chrome及qq浏览器(chrome内核)都会出现这种问题,ie9正常,其他未测试

这个插件不开源,官网上的issuses也没有相关答案,求用过的大神帮忙看一下

```js
$("#uploadify").uploadify({            
    swf : 'pc/uploadify/uploadify.swf',
    uploader : 'file/upload.do',
    fileTypeDesc : '压缩文件',            
    fileTypeExts:'*.zip;',//rar或者zip会导致弹窗很慢，慎用
    removeCompleted: false
)}
```

该插件除了选择文件有问题之外,其他功能正常,支持进度条,剩余时间,网速显示功能,其他插件都没有这些功能


[WebUploader UEditor chrome 点击上传文件选择框会延迟几秒才会显示 反应很慢](http://www.cnblogs.com/liangjiang/p/5799984.html)

chrome52.0.2743.80以上，

```js
accept: {
  title: 'Images',
  extensions: 'jpg,jpeg,png',
  mimeTypes: 'image/*'
}
```

改为

```js
accept: {
  title: 'Images',
  extensions: 'jpg,jpeg,png',
  mimeTypes: 'image/jpg,image/jpeg,image/png'   //修改这行
}
```


## 自定义

自己有再客户端实现限制感觉好坑！