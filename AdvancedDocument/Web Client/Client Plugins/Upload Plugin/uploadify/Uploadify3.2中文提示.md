[Uploadify3.2中文提示](http://www.pengbotao.cn/uploadify-tips.html)

## 修改提示就是一个坑！

这个不改源代码没有办法

```js
var queuedFile = {};
for (var n in this.queueData.files) {
    queuedFile = this.queueData.files[n];
    if (queuedFile.uploaded != true && queuedFile.name == file.name) {
        var replaceQueueItem = confirm('The file named "' + file.name + '" is already in the queue.\nDo you want to replace the existing item in the queue?');
        if (!replaceQueueItem) {
            this.cancelUpload(file.id);
            this.queueData.filesCancelled++;
            return false;
        } else {
            $('#' + queuedFile.id).remove();
            this.cancelUpload(queuedFile.id);
            this.queueData.filesReplaced++;
        }
    }
}
```

```


```