[新手学跨域之iframe ](https://segmentfault.com/a/1190000000702539)

页面嵌套iframe是比较常见的，比如QQ相关业务页面的登录框一般都是iframe的。使用ifrmae跨域要满足一个基本条件，父页面和子页面都是自己
可以控制的，如果随便把iframe指向一个其他网站，想通过跨域手段操作它基本上是不可能的。

##document.domain

document.domain是比较常用的跨域方法。实现最简单但只能用于同一个主域下不同子域之间的跨域请求，比如 foo.com 和 img.foo.com 之间，
img1.foo.com 和 img2.foo.com 之间。只要把两个页面的document.domain都指向主域就可以了，比如document.domain='foo.com';。

设置好后父页面和子页面就可以像同一个域下两个页面之间访问了。父页面通过ifr.contentWindow就可以访问子页面的window，子页面通过parent.window
或parent访问父页面的window，接下来可以进一步获取dom和js。

<!-- foo.com/a.html -->
<iframe id="ifr" src="http://img.foo.com/b.html"></iframe>
<script>
document.domain = 'foo.com';
function aa(str) {
    console.log(str);
}
window.onload = function () {
    document.querySelector('#ifr').contentWindow.bb('aaa');
}
</script>
<!-- img.foo.com/b.html -->


<script>
document.domain = 'foo.com';
function bb(str) {
    console.log(str);
}

parent.aa('bbb');
</script>


##window.name

只要不关闭浏览器，window.name可以在不同页面加载后依然保持。尝试在浏览器打开百度baidu.com，然后在控制台输入window.name='aaa';回车，
接着在地址栏输入qq.com转到腾讯首页，打开控制台输入window.name查看它的值，可以看到输出了"aaa"。

例如子页面bar.com/b.html向父页面foo.com/a.html传数据。
<!-- foo.com/a.html -->
<iframe id="ifr" src="http://bar.com/b.html"></iframe>
<script>
function callback(data) {
    console.log(data)
}
</script>
<!-- bar.com/b.html -->
<input id="txt" type="text">
<input type="button" value="发送" onclick="send();">


<script>
var proxyA = 'http://foo.com/aa.html';    // foo.com下代理页面
var proxyB = 'http://bar.com/bb.html';    // bar.com下代理空页面

var ifr = document.createElement('iframe');
ifr.style.display = 'none';
document.body.appendChild(ifr);

function send() {
    ifr.src = proxyB;
}

ifr.onload = function() {
    ifr.contentWindow.name = document.querySelector('#txt').value;
    ifr.src = proxyA;
}
</script>


<!-- foo.com/aa.html -->
top.callback(window.name)

##location.hash

较常用，把传递的数据依附在url上 例如获取子页面bar.com/b.html的高度及其他数据
<!-- foo.com/a.html -->
<iframe id="ifr" src="http://bar.com/b.html"></iframe>
<script>
function callback(data) {
    console.log(data)
}
</script>
<!-- bar.com/b.html -->
window.onload = function() {
    var ifr = document.createElement('iframe');
    ifr.style.display = 'none';
    var height = document.documentElement.scrollHeight;
    var data = '{"h":'+ height+', "json": {"a":1,"b":2}}';
    ifr.src = 'http://foo.com/aa.html#' + data;
    document.body.appendChild(ifr);
}
<!-- foo.com/aa.html -->
var data = JSON.parse(location.hash.substr(1));
top.document.getElementById('ifr').style.height = data.h + 'px';
top.callback(data);

##window.navigator

IE6的bug，父页面和子页面都可以访问window.navigator这个对象，在navigator上添加属性或方法可以共享。因为现在没有IE6环境，这里就不写例子了。

##postMessage

HTML5新增方法，现在浏览器及IE8+支持，简单易用高大上。
.postMessage(message, targetOrigin)参数说明
message: 是要发送的消息，类型为 String、Object (IE8、9 不支持) targetOrigin: 是限定消息接收范围，不限制请使用 '*'
'message', function(e)回调函数第一个参数接收 Event 对象，有三个常用属性：
data: 消息
origin: 消息来源地址
source: 源 DOMWindow 对象
一个简单的父页面foo.com/a.html 和子页面 bar.com/b.html建立通信
<!-- foo.com/a.html -->
<iframe id="ifr" src="http://bar.com/b.html"></iframe>
<script>
window.onload = function () {
    var ifr = document.querySelector('#ifr');
    ifr.contentWindow.postMessage({a: 1}, '*');
}
window.addEventListener('message', function(e) {
    console.log('bar say: '+e.data);
}, false);
</script>



<!-- bar.com/b.html -->
window.addEventListener('message', function(e){
    console.log('foo say: ' + e.data.a);
    e.source.postMessage('get', '*');
}, false)