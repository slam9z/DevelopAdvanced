[jquery cookie的用法](http://www.cnblogs.com/qiantuwuliang/archive/2009/07/19/1526663.html)

jQuery cookie是个很好的cookie插件，大概的使用方法如下
example $.cookie(’name’, ‘value’);
设置cookie的值，把name变量的值设为value
example $.cookie(’name’, ‘value’, {expires: 7, path: ‘/’, domain: ‘jquery.com’, secure: true});
新建一个cookie 包括有效期 路径 域名等
example $.cookie(’name’, ‘value’);
新建cookie
example $.cookie(’name’, null);
删除一个cookie

var account= $.cookie('name');
取一个cookie(name)值给myvar

代码如下
jQuery.cookie = function(name, value, options) {
    if (typeof value != 'undefined') { // name and value given, set cookie
        options = options || {};
        if (value === null) {
            value = '';
            options.expires = -1;
        }
        var expires = '';
        if (options.expires && (typeof options.expires == 'number' || options.expires.toUTCString)) {
            var date;
            if (typeof options.expires == 'number') {
                date = new Date();
                date.setTime(date.getTime() + (options.expires * 24 * 60 * 60 * 1000));
            } else {
                date = options.expires;
            }
            expires = '; expires=' + date.toUTCString(); // use expires attribute, max-age is not supported by IE
        }
        var path = options.path ? '; path=' + options.path : '';
        var domain = options.domain ? '; domain=' + options.domain : '';
        var secure = options.secure ? '; secure' : '';
        document.cookie = [name, '=', encodeURIComponent(value), expires, path, domain, secure].join('');
    } else { // only name given, get cookie
        var cookieValue = null;
        if (document.cookie && document.cookie != '') {
            var cookies = document.cookie.split(';');
            for (var i = 0; i < cookies.length; i++) {
                var cookie = jQuery.trim(cookies[i]);
                // Does this cookie string begin with the name we want?
                if (cookie.substring(0, name.length + 1) == (name + '=')) {
                    cookieValue = decodeURIComponent(cookie.substring(name.length + 1));
                    break;
                }
            }
        }
        return cookieValue;
    }
};

然后看了下Discuz!中对cookie的操作方法
如下,发现少了个遍历用;分割的数组的处理
function getcookie(name) {
var cookie_start = document.cookie.indexOf(name);
var cookie_end = document.cookie.indexOf(";", cookie_start);
return cookie_start == -1 ? '' : unescape(document.cookie.substring(cookie_start + name.length + 1, (cookie_end > cookie_start ? cookie_end : document.cookie.length)));
}

function setcookie(cookieName, cookieValue, seconds, path, domain, secure) {
var expires = new Date();
expires.setTime(expires.getTime() + seconds);
document.cookie = escape(cookieName) + '=' + escape(cookieValue)
+ (expires ? '; expires=' + expires.toGMTString() : '')
+ (path ? '; path=' + path : '/')
+ (domain ? '; domain=' + domain : '')
+ (secure ? '; secure' : '');
}