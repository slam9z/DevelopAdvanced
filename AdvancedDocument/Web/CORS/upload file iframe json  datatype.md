IE9以及以下

```
$('#fileupload').bind('fileuploaddone', function (e, data) {

    var name = GetNameObject(data.result[0]);;
    name.insertAfter($('#fileList .block-holder'));
});


data.datatype="iframe json"  
```