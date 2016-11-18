[HTTP访问控制(CORS)](https://developer.mozilla.org/zh-CN/docs/Web/HTTP/Access_control_CORS)


一个资源会发起一个跨域HTTP请求(Cross-site HTTP request), 当它请求的一个资源是从一个与它本身提供的第
一个资源的不同的域名时 。

比如说，域名A(http://domaina.example)的某 Web 应用程序中通过<img>标签引入了域名B(http://domainb.foo)
站点的某图片资源(http://domainb.foo/image.jpg)，域名A的那 Web 应用就会导致浏览器发起一个跨站 HTTP 请求。
在当今的 Web 开发中，使用跨站 HTTP 请求加载各类资源（包括CSS、图片、JavaScript 脚本以及其它类资源），
已经成为了一种普遍且流行的方式。


正如大家所知，出于安全考虑，浏览器会限制脚本中发起的跨站请求。比如，使用 *XMLHttpRequest* 对象发起 HTTP 请求就
必须遵守*同源策略*。 具体而言，Web 应用程序能且只能使用 *XMLHttpRequest* 对象向其加载的源域名发起 HTTP 请求，
而不能向任何其它域名发起请求。为了能开发出更强大、更丰富、更安全的Web应用程序，开发人员渴望着在不丢失安全的前提下，
Web 应用技术能越来越强大、越来越丰富。比如，可以使用 XMLHttpRequest 发起跨站 HTTP 请求。（这段描述跨域不准确，
跨域并非浏览器限制了发起跨站请求，而是跨站请求可以正常发起，但是返回结果被浏览器拦截了。最好的例子是CSRF跨站攻击原理，
请求是发送到了后端服务器无论是否跨域！注意：有些浏览器不允许从HTTPS的域跨域访问HTTP，比如Chrome和Firefox，
这些浏览器在请求还未发出的时候就会拦截请求，这是一个特例。）


隶属于 W3C 的 Web 应用工作组( *Web Applications Working Group* )推荐了一种新的机制，即跨源资源共享
（*Cross-Origin Resource Sharing (CORS)*）。这种机制让Web应用服务器能支持跨站访问控制，从而使得安全地进行跨站数据
传输成为可能。需要特别注意的是，这个规范是针对API容器的。比如说，要使得 XMLHttpRequest 在现代浏览器中可以发起跨域请求。
浏览器必须能支持跨源共享带来的新的组件，包括请求头和策略执行。同样，服务器端则需要解析这些新的请求头，并按照策略返回
相应的响应头以及所请求的资源。这篇文章适用于网站管理员、服务器端程序开发人员以及前端开发人员。对于服务器端程序开发人员
，还可以阅读补充材料 cross-origin sharing from a server perspective (with PHP code snippets) 。

跨源资源共享标准( *cross-origin sharing standard* ) 使得以下场景可以使用跨站 HTTP 请求：

* 如上所述，使用 XMLHttpRequest 发起跨站 HTTP 请求。
* Web 字体 (CSS 中通过 @font-face 使用跨站字体资源), 因此，网站就可以发布 TrueType 字体资源，并只允许已授权网站进行跨站调用。
* WebGL 贴图
* 使用 drawImage API 在 canvas 上画图

接下来的文章，会对跨源资源共享做一个总览，并介绍下在 Firefox 3.5 中已实现的跨源资源共享所使用的 HTTP 头。

##概述

跨源资源共享标准通过新增一系列 HTTP 头，让服务器能声明哪些来源可以通过浏览器访问该服务器上的资源。另外，对那些会对服务器数据
造成破坏性影响的 HTTP 请求方法（特别是 GET 以外的 HTTP 方法，或者搭配某些MIME类型的POST请求），标准强烈要求浏览器必须
先以 OPTIONS 请求方式发送一个预请求(preflight request)，从而获知服务器端对跨源请求所支持 HTTP 方法。在确认服务器允许该
跨源请求的情况下，以实际的 HTTP 请求方法发送那个真正的请求。服务器端也可以通知客户端，是不是需要随同请求一起发送信用信息
（包括 Cookies 和 HTTP 认证相关数据）。

随后的章节，将对相关情景及用到的 HTTP 请求进行讨论。

##一些访问控制场景

在此，我们会用三个场景来解释跨源共享是怎么运行的。其中，所有的跨站请求都是通过 XMLHttpRequest 对象发起。

如果对以下章节中的 JavaScript 代码片段感兴趣，可以访问这儿。在所有支持跨站 XMLHttpRequest 请求的浏览中，
可以看到实际运行效果。而如果想继续了解服务器端对跨源请求的处理，则可以访问这儿。

###简单请求

所谓的简单，是指：

只使用 GET, HEAD 或者 POST 请求方法。如果使用 POST 向服务器端传送数据，则数据类型(Content-Type)只能
是 application/x-www-form-urlencoded, multipart/form-data 或 text/plain中的一种。

不会使用自定义请求头（类似于 X-Modified 这种）。

>Note: 这些跨站请求与以往浏览器发出的跨站请求并无异同。并且，如果服务器不给出适当的响应头，则不会有任何数据返
>回给请求方。因此，那些不允许跨站请求的网站无需为这一新的 HTTP 访问控制特性担心。

比如说，假如站点 http://foo.example 的网页应用想要访问 http://bar.other 的资源。以下的 JavaScript 代码应该会
在 foo.example 上执行：

```js
var invocation = new XMLHttpRequest();
var url = 'http://bar.other/resources/public-data/';
   
function callOtherDomain() {
  if(invocation) {    
    invocation.open('GET', url, true);
    invocation.onreadystatechange = handler;
    invocation.send(); 
  }
}
```


让我们看看，在这个场景中，浏览器会发送什么的请求到服务器，而服务器又会返回什么给浏览器：

```
GET /resources/public-data/ HTTP/1.1
Host: bar.other
User-Agent: Mozilla/5.0 (Macintosh; U; Intel Mac OS X 10.5; en-US; rv:1.9.1b3pre) Gecko/20081130 Minefield/3.1b3pre
Accept: text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8
Accept-Language: en-us,en;q=0.5
Accept-Encoding: gzip,deflate
Accept-Charset: ISO-8859-1,utf-8;q=0.7,*;q=0.7
Connection: keep-alive
Referer: http://foo.example/examples/access-control/simpleXSInvocation.html
Origin: http://foo.example


HTTP/1.1 200 OK
Date: Mon, 01 Dec 2008 00:23:53 GMT
Server: Apache/2.0.61 
Access-Control-Allow-Origin: *
Keep-Alive: timeout=2, max=100
Connection: Keep-Alive
Transfer-Encoding: chunked
Content-Type: application/xml

[XML Data]
```



第 1~10 行是 Firefox 3.5 发出的请求头。注意看第10行的请求头 Origin，它表明了该请求来自于 http://foo.exmaple。
第 13~22 行则是 http://bar.other 服务器的响应。如第16行所示，服务器返回了响应头 Access-Control-Allow-Origin: *，
这表明服务器接受来自任何站点的跨站请求。如果服务器端仅允许来自 http://foo.example 的跨站请求，它可以返回：

Access-Control-Allow-Origin: http://foo.example

现在，除了 http://foo.example，其它站点就不能跨站访问 http://bar.other 的资源了。

如上，通过使用 Origin 和 Access-Control-Allow-Origin 就可以完成最简单的跨站请求。不过 Access-Control-Allow-Origin
 需要为 * 或者包含由 Origin 指明的站点。

###预请求

>触发了预请求

不同于上面讨论的简单请求，“预请求”要求必须先发送一个 OPTIONS 请求给目的站点，来查明这个跨站请求对于目的站点是不
是安全可接受的。这样做，是因为跨站请求可能会对目的站点的数据造成破坏。 当请求具备以下条件，就会被当成预请求处理：

请求以 GET, HEAD 或者 POST 以外的方法发起请求。或者，使用 POST，但请求数据为 application/x-www-form-urlencoded, 
multipart/form-data 或者 text/plain 以外的数据类型。比如说，用 POST 发送数据类型为 application/xml 或者 text/xml
 的 XML 数据的请求。

使用自定义请求头（比如添加诸如 X-PINGOTHER）

>Note: 从Gecko 2.0开始，text/plain, application/x-www-form-urlencoded 和 multipart/form-data 类型的数据都可以
>直接用于跨站请求，而不需要先发起“预请求”了。之前，只有 text/plain 可以不用先发起“预请求”，进行跨站请求。

示例如下：
var invocation = new XMLHttpRequest();
var url = 'http://bar.other/resources/post-here/';
var body = '{C}{C}{C}{C}{C}{C}{C}{C}{C}{C}Arun';
    
function callOtherDomain(){
  if(invocation)
    {
      invocation.open('POST', url, true);
      invocation.setRequestHeader('X-PINGOTHER', 'pingpong');
      invocation.setRequestHeader('Content-Type', 'application/xml');
      invocation.onreadystatechange = handler;
      invocation.send(body); 
    }

......




如上，以 XMLHttpRequest 创建了一个 POST 请求，为该请求添加了一个自定义请求头(X-PINGOTHER: pingpong)，并指定数据类型
为 application/xml。所以，该请求是一个“预请求”形式的跨站请求。

让我们看看服务器与浏览器之间具体的交互过程：

```
OPTIONS /resources/post-here/ HTTP/1.1
Host: bar.other
User-Agent: Mozilla/5.0 (Macintosh; U; Intel Mac OS X 10.5; en-US; rv:1.9.1b3pre) Gecko/20081130 Minefield/3.1b3pre
Accept: text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8
Accept-Language: en-us,en;q=0.5
Accept-Encoding: gzip,deflate
Accept-Charset: ISO-8859-1,utf-8;q=0.7,*;q=0.7
Connection: keep-alive
Origin: http://foo.example
Access-Control-Request-Method: POST
Access-Control-Request-Headers: X-PINGOTHER


HTTP/1.1 200 OK
Date: Mon, 01 Dec 2008 01:15:39 GMT
Server: Apache/2.0.61 (Unix)
Access-Control-Allow-Origin: http://foo.example
Access-Control-Allow-Methods: POST, GET, OPTIONS
Access-Control-Allow-Headers: X-PINGOTHER
Access-Control-Max-Age: 1728000
Vary: Accept-Encoding, Origin
Content-Encoding: gzip
Content-Length: 0
Keep-Alive: timeout=2, max=100
Connection: Keep-Alive
Content-Type: text/plain

POST /resources/post-here/ HTTP/1.1
Host: bar.other
User-Agent: Mozilla/5.0 (Macintosh; U; Intel Mac OS X 10.5; en-US; rv:1.9.1b3pre) Gecko/20081130 Minefield/3.1b3pre
Accept: text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8
Accept-Language: en-us,en;q=0.5
Accept-Encoding: gzip,deflate
Accept-Charset: ISO-8859-1,utf-8;q=0.7,*;q=0.7
Connection: keep-alive
X-PINGOTHER: pingpong
Content-Type: text/xml; charset=UTF-8
Referer: http://foo.example/examples/preflightInvocation.html
Content-Length: 55
Origin: http://foo.example
Pragma: no-cache
Cache-Control: no-cache

Arun


HTTP/1.1 200 OK
Date: Mon, 01 Dec 2008 01:15:40 GMT
Server: Apache/2.0.61 (Unix)
Access-Control-Allow-Origin: http://foo.example
Vary: Accept-Encoding, Origin
Content-Encoding: gzip
Content-Length: 235
Keep-Alive: timeout=2, max=99
Connection: Keep-Alive
Content-Type: text/plain

[Some GZIP'd payload]

```


第1至12行，使用一个 OPTIONS 发送了一个“预请求”。Firefox 3.1 根据请求参数，决定需要发送一个“预请求”，来探明服务器端是
否接受后续真正的请求。 OPTIONS 是 HTTP/1.1 里的方法，用来获取更多服务器端的信息，是一个不应该对服务器数据造成影响的方法。
 随同 OPTIONS 请求，以下两个请求头一起被发送：

```
Access-Control-Request-Method: POST
Access-Control-Request-Headers: X-PINGOTHER
```

请求头Access-Control-Request-Method可以提醒服务器跨站请求将使用POST方法，而请求头Access-Control-Request-Headers则
告知服务器该跨站请求将携带一个自定义请求头X-PINGOTHER。这样，服务器就可以决定，在当前情况下，是否接受该跨站请求访问。
第15至27行是服务器的响应。该响应表明，服务器接受了客服端的跨站请求。具体可以看看第18至21行：

```
Access-Control-Allow-Origin: http://foo.example
Access-Control-Allow-Methods: POST, GET, OPTIONS
Access-Control-Allow-Headers: X-PINGOTHER
Access-Control-Max-Age: 1728000
```


响应头Access-Control-Allow-Methods表明服务器可以接受POST, GET和 OPTIONS的请求方法。请注意，这个响应头类似于HTTP/1.1 
Allow: response header，但仅限于访问控制的场景下。而响应头Access-Control-Allow-Headers则表示服务器接受自定义请求
头X-PINGOTHER。就像Access-Control-Allow-Methods一样，Access-Control-Allow-Headers允许以逗号分隔，传递一个可接受的
自定义请求头列表。最后，响应头Access-Control-Max-Age告诉浏览器，本次“预请求”的响应结果有效时间是多久。在上面的例子里，
1728000秒代表着20天内，浏览器在处理针对该服务器的跨站请求，都可以无需再发送“预请求”，只需根据本次结果进行判断处理。

###附带凭证信息的请求

XMLHttpRequest和访问控制功能，最有趣的特性就是，发送凭证请求（HTTP Cookies和验证信息）的功能。
一般而言，对于跨站请求，浏览器是不会发送凭证信息的。但如果将XMLHttpRequest的一个特殊标志位设置为true，
浏览器就将允许该请求的发送。

http://foo.example站点的脚本向http://bar.other站点发送一个GET请求，并设置了一个Cookies值。脚本代码如下：


```JavaScript
var invocation = new XMLHttpRequest();
var url = 'http://bar.other/resources/credentialed-content/';
function callOtherDomain(){
  if(invocation) {
    invocation.open('GET', url, true);
    invocation.withCredentials = true;
    invocation.onreadystatechange = handler;
    invocation.send(); 
}


如你所见，第七行代码将XMLHttpRequest的withCredentials标志设置为true，从而使得Cookies可以随着请求发送。因为这是一个
简单的GET请求，所以浏览器不会发送一个“预请求”。但是，如果服务器端的响应中，如果没有返回
Access-Control-Allow-Credentials: true的响应头，那么浏览器将不会把响应结果传递给发出请求的脚本程序，以保证信息的安全。

客服端与服务器端交互示例如下：

```
GET /resources/access-control-with-credentials/ HTTP/1.1
Host: bar.other
User-Agent: Mozilla/5.0 (Macintosh; U; Intel Mac OS X 10.5; en-US; rv:1.9.1b3pre) Gecko/20081130 Minefield/3.1b3pre
Accept: text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8
Accept-Language: en-us,en;q=0.5
Accept-Encoding: gzip,deflate
Accept-Charset: ISO-8859-1,utf-8;q=0.7,*;q=0.7
Connection: keep-alive
Referer: http://foo.example/examples/credential.html
Origin: http://foo.example
Cookie: pageAccess=2


HTTP/1.1 200 OK
Date: Mon, 01 Dec 2008 01:34:52 GMT
Server: Apache/2.0.61 (Unix) PHP/4.4.7 mod_ssl/2.0.61 OpenSSL/0.9.7e mod_fastcgi/2.4.2 DAV/2 SVN/1.4.2
X-Powered-By: PHP/5.2.6
Access-Control-Allow-Origin: http://foo.example
Access-Control-Allow-Credentials: true
Cache-Control: no-cache
Pragma: no-cache
Set-Cookie: pageAccess=3; expires=Wed, 31-Dec-2008 01:34:53 GMT
Vary: Accept-Encoding, Origin
Content-Encoding: gzip
Content-Length: 106
Keep-Alive: timeout=2, max=100
Connection: Keep-Alive
Content-Type: text/plain


[text/plain payload]


```



虽然第11行指定了要提交到http://bar.other的内容的Cookie信息,但是如果bar.other的响应头里没有
Access-Control-Allow-Credentials:true(第19行),则响应会被忽略. 特别注意: 给一个带有withCredentials的请
求发送响应的时候,服务器端必须指定允许请求的域名,不能使用'*'.上面这个例子中,如果响应头是这样的:
Access-Control-Allow-Origin: * ,则响应会失败. 在这个例子里,因为Access-Control-Allow-Origin的值是
http://foo.example这个指定的请求域名,所以客户端把带有凭证信息的内容被返回给了客户端. 另外注意第22行,
更多的cookie信息也被创建了.

上面这些例子的运行可以查看这里.下一部分将讨论实际的HTTP头信息.
 
##HTTP响应头

这部分里列出了跨域资源共享(Cross-Origin Resource Sharing)时,服务器端需要返回的响应头信息.上一部分内容是这部分
内容在实际运用中的一个概述.

###Access-Control-Allow-Origin

返回的资源需要有一个 Access-Control-Allow-Origin 头信息,语法如下:
Access-Control-Allow-Origin: <origin> | *

origin参数指定一个允许向该服务器提交请求的URI.对于一个不带有credentials的请求,可以指定为'*',表示允许来自所有域的请求.
举个栗子,允许来自 http://mozilla.com 的请求,你可以这样指定:
Access-Control-Allow-Origin: http://mozilla.com

如果服务器端指定了域名,而不是'*',那么响应头的Vary值里必须包含Origin.它告诉客户端: 响应是根据请求头里的Origin的值来
返回不同的内容的.

###Access-Control-Expose-Headers

Requires Gecko 2.0(Firefox 4 / Thunderbird 3.3 / SeaMonkey 2.1)

设置浏览器允许访问的服务器的头信息的白名单:
Access-Control-Expose-Headers: X-My-Custom-Header, X-Another-Custom-Header

这样, X-My-Custom-Header 和 X-Another-Custom-Header这两个头信息,都可以被浏览器得到.

###Access-Control-Max-Age

这个头告诉我们这次预请求的结果的有效期是多久,如下:
Access-Control-Max-Age: <delta-seconds>

delta-seconds 参数表示,允许这个预请求的参数缓存的秒数,在此期间,不用发出另一条预检请求. 

###Access-Control-Allow-Credentials

告知客户端,当请求的credientials属性是true的时候,响应是否可以被得到.当它作为预请求的响应的一部分时,它用来告知实际的请求
是否使用了credentials.注意,简单的GET请求不会预检,所以如果一个请求是为了得到一个带有credentials的资源,而响应里又没有
Access-Control-Allow-Credentials头信息,那么说明这个响应被忽略了.
Access-Control-Allow-Credentials: true | false

带有credential的请求在上面讨论.

###Access-Control-Allow-Methods

指明资源可以被请求的方式有哪些(一个或者多个). 这个响应头信息在客户端发出预检请求的时候会被返回. 上面有相关的例子.
Access-Control-Allow-Methods: <method>[, <method>]*

发出预检请求的例子见上,这个例子里就有向客户端发送Access-Control-Allow-Methods响应头信息.

###Access-Control-Allow-Headers

也是在响应预检请求的时候使用.用来指明在实际的请求中,可以使用哪些自定义HTTP请求头.比如
Access-Control-Allow-Headers: X-PINGOTHER
这样在实际的请求里,请求头信息里就可以有这么一条:
X-PINGOTHER: pingpong
可以有多个自定义HTTP请求头,用逗号分隔.
Access-Control-Allow-Headers: <field-name>[, <field-name>]*

##HTTP 请求头


这部分内容列出来当浏览器发出跨域请求时可能用到的HTTP请求头.注意这些请求头信息都是在请求服务器的时候已经为你设置好的,
当开发者使用跨域的XMLHttpRequest的时候,不需要手动的设置这些头信息.

###Origin

表明发送请求或者预请求的域
Origin: <origin>

参数origin是一个URI,告诉服务器端,请求来自哪里.它不包含任何路径信息,只是服务器名.
Note: Origin的值可以是一个空字符串,这是很有用的.
注意,不仅仅是跨域请求,普通请求也会带有ORIGIN头信息.

###Access-Control-Request-Method

在发出预检请求时带有这个头信息,告诉服务器在实际请求时会使用的请求方式
Access-Control-Request-Method: <method>

相关的例子可以在这里找到

###Access-Control-Request-Headers
在发出预检请求时带有这个头信息,告诉服务器在实际请求时会携带的自定义头信息.如有多个,可以用逗号分开.
Access-Control-Request-Headers: <field-name>[, <field-name>]*


相关的例子可以在这里找到

##浏览器支持

注意：

Internet Explorer 8 和 9 通过 XDomainRequest 对象来实现CORS ，但是在IE 10中有完整的实现。Firefox 3.5 就引入了
对跨站 XMLHttpRequests 和 Web 字体的支持 ，尽管存在着一些直到后续版本才取消的限制。特别的， Firefox 7 引入了对跨站
WebGL 纹理的 HTTP 请求的支持，而且 Firefox 9 添加对通过 drawImage 在 canvas 上绘图的支持。