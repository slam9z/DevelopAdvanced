[javascript跨域上传文件可以如何实现？ ](http://www.dewen.net.cn/q/12309/javascript%E8%B7%A8%E5%9F%9F%E4%B8%8A%E4%BC%A0%E6%96%87%E4%BB%B6%E5%8F%AF%E4%BB%A5%E5%A6%82%E4%BD%95%E5%AE%9E%E7%8E%B0%EF%BC%9F)

##Question

javascript跨域上传文件可以如何实现？ 


想实现一个跨域上传文件的功能
但不考虑后台服务器做代理
有没有什么其他方案可以选择？
同问，除了jsonp之外还有什么其他的跨域方案？



##Ansewer

另一种方法是直接引用目标域名下面的脚本，比如

```js
<script type="text/javascript" src="http://targetdomain.com/crossdomain.js"></script>
```
这个脚本里面可以用ajax访问targetdomain.com下面的资源。可以通过回调的方式让其他脚本也能处理跨域访问的结果，比如：

```js
<script type="text/javascript">
crossDomainRequest("login",
    function(result)
    {
        if(result.success)
        {
            alert('success');
        }
    });
</script>
```

新浪微博的Javascript SDK就使用了这种技术。

跨域上传的话，HTML5下面的跨域上传和普通跨域差别不大（不过兼容性不太好），非HTML5的AJAX上传一般是用iframe的，跨域的话无法从
iframe里面获取到返回的信息。如果上传接口可以自己设计的话，可以在上传接口处加上一个回调地址的参数，然后在上传成功后HTTP 302跳转
到回调地址。回调地址是本地地址，就可以用javascript访问了。或者用和上面一样的方法解决跨域的问题。


    我了解如何使用iframe模拟ajax上传，也就是修改target属性……但是如何通过iframe进行跨域上传？可否详细说明一下？

    因为用iframe提交本身是没有跨域限制的，只要把action设置成跨域的地址就行了。问题是提交上去之后没有办法获得返回值，
    所以可以考虑用地址回调的方法获取返回值，也就是提交完之后服务器端自动跳转回到你所在的域名。 – 灵剑2012 2013-04-19 