[Download File Using jQuery](http://stackoverflow.com/questions/1296085/download-file-using-jquery)

##Question

How can I prompt a download for a user when they click a link.
For example, instead of:

```
<a href="uploads/file.doc">Download Here</a>
```

I could use: 

```
<a href="#">Download Here</a>

 $('a').click... //Some jquery to download the file
```

This way, Google does not index my HREF's and private files.
Can this be done with jQuery, if so, how? Or should this be done with PHP or something instead?

##Answer

I might suggest this, as a more gracefully degrading solution, using preventDefault:

```
$('a').click(function(e) {
    e.preventDefault();  //stop the browser from following
    window.location.href = 'uploads/file.doc';
});

<a href="no-script.html">Download now!</a>
``

Even if there's no Javascript, at least this way the user will get some feedback.

##MyAnswer

使用

```js
window.open(url, "_black");
//替换
window.location.href = 'uploads/file.doc';
```

可以打开新的叶签