[uploadify上传文件Firefox浏览器上传失败解决方法，uploadifyfirefox](http://www.cnblogs.com/mbailing/archive/2011/03/30/uploadify.html)


最近做文件上传使用到了uploadify

但是出现了各种奇葩的问题，而且针对各个不同浏览器问题不同

在Firefox中，很坑爹的是，每次上传就丢失session值，但是我的系统在登录，保存文件目录等处使用了session，结果session值为null；

花了大半天的时间调试，

最后发现是falsh上传的问题sessionid变了，而且在此请求上传文件地址时，系统就会从新登录，这个登录更有意思，每次都回去读IE中的cookie（我的项目中使用了cookie保存用户登录信息），而不是读Firefox中的值，最后看官网上是这么解释的；所以只要把sessionid传进回话机制里边就行；

如下

```
"uploadLimit" : 10, //允许上传的最多张数 默认是1
"swf" : "js/uploadify/uploadify.swf",  //swfUpload
"uploader" : "uploadfile.shtml 
```

只需就该“uploader"中的url，再其后面加上session的Id；将上面的改成如下：

```
第一种	"uploader" : "uploadfile.shtml;jsessionid=${pageContext.session.id}", //服务器端url 
第二种  "uploader" : "uploadfile.shtml;jsessionid=<%=session.getId()%>" //注意前面的;分号
```


> 解决方案就是自己把cookie写到url或者form里，到服务端再写入。

```cs
    private void UpdateUserCookie()
    {
        try
        {
            string sessionParamName = "userCookie";
            string sessionCookieName = AdminCookieKey;
            if (HttpContext.Current.Request.Form[sessionParamName] != null)
            {
                UpdateCookie(sessionCookieName, HttpContext.Current.Request.Form[sessionParamName]);
            }
            else if (HttpContext.Current.Request.QueryString[sessionParamName] != null)
            {
                UpdateCookie(sessionCookieName, HttpContext.Current.Request.QueryString[sessionParamName]);
            }
        }
        catch
        {
        }
    }

    private void UpdateCookie(string cookieName, string cookieValue)
    {
        HttpCookie cookie = HttpContext.Current.Request.Cookies.Get(cookieName);
        if (null == cookie)
        {
            cookie = new HttpCookie(cookieName);
        }
        cookie.Value = cookieValue;
        HttpContext.Current.Request.Cookies.Set(cookie);//重新设定请求中的cookie值，将服务器端的session值赋值给它
    }

```