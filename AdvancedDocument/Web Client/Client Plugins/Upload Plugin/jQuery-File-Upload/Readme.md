[jQuery File Upload Plugin](https://github.com/blueimp/jQuery-File-Upload)

github上star最多的upload plugin。

##踩到一个坑

引用tmpl
```js

<!-- The Templates plugin is included to render the upload/download listings -->
<script src="//blueimp.github.io/JavaScript-Templates/js/tmpl.min.js"></script>

```

导致files无法被添加不知道原因

##使用frameTransport很多问题

提交多个文件只能一次上传，然后上传按钮只能显示一个。

```
$('#fileupload').fileupload({
     url: url,
     dataType: 'json',
     //forceIframeTransport：true会导致数据一次性提交。跨越使用还是很多问题
     forceIframeTransport: true,
```	 
	 