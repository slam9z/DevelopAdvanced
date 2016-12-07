[How can I upload files asynchronously?](http://stackoverflow.com/questions/166221/how-can-i-upload-files-asynchronously)

## answer



With HTML5 you CAN make file uploads with Ajax and jQuery. Not only that, you can do file validations (name, size, and MIME-type) or handle the progress event with the HTML5 progress tag (or a div). Recently I had to make a file uploader, but I didn't want to use Flash nor Iframes or plugins and after some research I came up with the solution.

The HTML:

```html
<form enctype="multipart/form-data">
    <input name="file" type="file" />
    <input type="button" value="Upload" />
</form>
<progress></progress>
```

First, you can do some validation if you want. For example, in the onChange event of the file:

```js
$(':file').change(function(){
    var file = this.files[0];
    var name = file.name;
    var size = file.size;
    var type = file.type;
    //Your validation
});
```

Now the Ajax submit with the button's click:

```js
$(':button').click(function(){
    var formData = new FormData($('form')[0]);
    $.ajax({
        url: 'upload.php',  //Server script to process data
        type: 'POST',
        xhr: function() {  // Custom XMLHttpRequest
            var myXhr = $.ajaxSettings.xhr();
            if(myXhr.upload){ // Check if upload property exists
                myXhr.upload.addEventListener('progress',progressHandlingFunction, false); // For handling the progress of the upload
            }
            return myXhr;
        },
        //Ajax events
        beforeSend: beforeSendHandler,
        success: completeHandler,
        error: errorHandler,
        // Form data
        data: formData,
        //Options to tell jQuery not to process data or worry about content-type.
        cache: false,
        contentType: false,
        processData: false
    });
});
```

Now if you want to handle the progress.

```js
function progressHandlingFunction(e){
    if(e.lengthComputable){
        $('progress').attr({value:e.loaded,max:e.total});
    }
}
```
As you can see, with HTML5 (and some research) file uploading not only becomes possible but super easy. Try it with Google Chrome as some of the HTML5 components of the examples aren't available in every browser.
