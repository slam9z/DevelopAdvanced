
[Cross domain uploads](https://github.com/blueimp/jQuery-File-Upload/wiki/Cross-domain-uploads)

```
Origin http://filehubcustomer.com not found in Access-Control-Allow-Origin header.
```

```js
$('#fileupload').fileupload({
    forceIframeTransport: true
});
```

使用iframe会导致获取不到返回的结果。

