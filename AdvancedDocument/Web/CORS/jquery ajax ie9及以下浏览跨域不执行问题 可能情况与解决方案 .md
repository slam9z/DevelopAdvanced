[jquery ajax ie9及以下浏览跨域不执行问题 可能情况与解决方案 ](http://www.cnblogs.com/cnblogs-jcy/articles/5523030.html)

##问题描述

网站中搜索页面输入关键词后，通过 jquery.ajax 加载数据， chrome, firefox, IE10+ 都可以顺利加载数据，
但是IE9及以后版本不执

##产生此问题的原因

通过执行 jquery.ajax error 函数显示未执行 拒绝访问

##解决方法

* 在 jquery.ajax 调用前写 jQuery.support.cors = true （仅此法无法解决）
* 针对 拒绝访问 是由于浏览器安全机制导致的，解决方法为点击IE浏览器的的“工具->Internet 选项->安全->自定义级别”
将“其他”选项中的“通过域访问数据源”选中为“启用”或者“提示”，点击确定就可以了（但是此法需要用户自行设置不太现实）

* （推荐）对于浏览器跨域 IE10+ 才支持withCredentials属性，IE9- 不支持，跨域对象只能用XDomainRequest对象，
而jQuery并不兼容XDomainRequest.. 针对此方法网络上有解决的插件 jQuery-ajaxTransport-XDomainRequest


##使用插件需要注意的事项

> CORS requires the Access-Control-Allow-Origin header to be present in the AJAX response from the server.

* Only GET or POST 

    When POSTing, the data will always be sent with a Content-Type of text/plain
* Only HTTP or HTTPS 

    Protocol must be the same scheme as the calling page
    
* Always asynchronous
[来自插件说明]

##结合网站上的事项

* 网站上的请求为 GET
* 请求的服务端设置 header("Content-type: text/html; charset=utf-8"); header('Access-Control-Allow-Origin: *');
* 请求到的数据格式为 html
* jquery.ajax 参数需要设置 dataType: html