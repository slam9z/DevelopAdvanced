[Uploadify ruins file upload when no flash in browser](http://stackoverflow.com/questions/5137093/uploadify-ruins-file-upload-when-no-flash-in-browser)

## answer
	

If you are using the latest version of uploadify you can use the onFallback event to detect if flash is installed (or if the required flash version of flash is supported):

```js
$("#file_upload").uploadify({
    'swf'        : '/uploadify/uploadify.swf',
    'uploader'   : '/uploadify/uploadify.php',
    'onFallback' : function() {
        alert('Flash was not detected or flash version is not supported.');
    }
});
```
