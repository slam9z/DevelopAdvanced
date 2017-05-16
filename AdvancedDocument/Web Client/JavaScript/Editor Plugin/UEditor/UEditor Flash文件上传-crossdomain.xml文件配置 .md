[UEditor Flash文件上传-crossdomain.xml文件配置 ](http://www.cnblogs.com/liaochong/p/ueditorcrossdomain.html)


在使用UEditor富文本时，如果客户端的浏览器是低版本浏览器，如IE7、IE8等，UEditor的文件上传方式将会使用flash方式上传而不是html5，
而flash在跨域时唯一的限制策略就是crossdomain.xml文件，该文件限制了flash是否可以跨域读写数据以及允许从什么地方跨域读写数据。
从UEditor官方文档上看，如果使用flash方式上传文件，那么只需要设置如下即可：

但实际在IE7、IE8环境下该文件被正确请求到后仍然无法正确上传文件，而是报出“http请求错误”信息。
实际上，UEditor的设置只设置了部分，还有部分未进行设置，正确的设置如下：

```xml
<?xml version="1.0" encoding="UTF-8"?>
<!DOCTYPE cross-domain-policy SYSTEM ”http://www.macromedia.com/xml/dtds/cross-domain-policy.dtd”>
<cross-domain-policy>
    <!-- 只允许使用主策略文件 -->
    <site-control permitted-cross-domain-policies="master-only"/>
    <!-- 设置可访问来源、端口范围、是否可传输未加密信息 -->
    <allow-access-from domain="*.baidu.com" to-ports="*" secure="false"/>
    <allow-access-from domain="*.baidu.cn" to-ports="*" secure="false"/>
    <!-- 设置可传入http头 -->
    <allow-http-request-headers-from domain="*.baidu.com" headers="*" />
    <allow-http-request-headers-from domain="*.baidu.cn" headers="*" />
</cross-domain-policy>
```

如上，还应该有allow-http-request-headers-from设置，原因在于：
allow-access-from：通过检查该节点的属性值，确认能够读取本域内容的flash文件来源域；
allow-http-request-headers-from：此节点授权第三方域flash向本域发送用户定义的http头；

两者之间的区别在于：
allow-access-from节点授权第三域提取本域中的数据，而 allow-http-request-headers-from 节点授权第三方域将数据以http头的形式发送到
本域中。[简而言之，allow-access-from是控制读取权限，allow-http-request-headers-from是控制以http头形式的写入权限]。