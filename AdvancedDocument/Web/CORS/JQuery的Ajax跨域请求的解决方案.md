[JQuery的Ajax跨域请求的解决方案](http://www.open-open.com/lib/view/open1334026513327.html)

>这种方案是在ie9和ie9以下能有效的，而且只支持get!,当然新的浏览器也是可以的。


今天在项目中需要做远程数据加载并渲染页面，直到开发阶段才意识到ajax跨域请求的问题，隐约记得Jquery有提过一个ajax跨域请求的解决方式，于是即刻翻出Jquery的API出来研究，发现JQuery对于Ajax的跨域请求有两类解决方案，不过都是只支持get方式。分别是JQuery的 jquery.ajax jsonp格式和jquery.getScript方式。 
什么是jsonp格式呢？API原文：如果获取的数据文件存放在远程服务器上（域名不同，也就是跨域获取数据），则需要使用jsonp类型。使用这种类型的话，会创建一个查询字符串参数 callback=? ，这个参数会加在请求的URL后面。服务器端应当在JSON数据前加上回调函数名，以便完成一个有效的JSONP请求。意思就是远程服务端需要对返回的数据做下处理，根据客户端提交的callback的参数，返回一个callback(json)的数据，而客户端将会用script的方式处理返回数据，来对json数据做处理。JQuery.getJSON也同样支持jsonp的数据方式调用。
客户端JQuery.ajax的调用代码示例：

```js
$.ajax({
	type : "get",
	async:false,
	url : "http://www.xxx.com/ajax.do",
	dataType : "jsonp",
	jsonp: "callbackparam",//服务端用于接收callback调用的function名的参数
	jsonpCallback:"success_jsonpCallback",//callback的function名称
	success : function(json){
		alert(json);
		alert(json[0].name);
	},
	error:function(){
		alert('fail');
	}
});
```

服务端返回数据的示例代码：

```cs
public void ProcessRequest (HttpContext context) {
	context.Response.ContentType = "text/plain";
	String callbackFunName = context.Request["callbackparam"];
	context.Response.Write(callbackFunName + "([ { name:\"John\"}])");
}
```