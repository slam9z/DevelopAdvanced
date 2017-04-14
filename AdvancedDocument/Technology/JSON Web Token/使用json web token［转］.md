[使用json web token［转］](http://www.cnblogs.com/rhett-web/p/4939524.html)

## JWTs

JWTs是一份草案，尽管在本质上它是一个老生常谈的一种更加具体的认证授权的机制。一个JWT被周期（period）分成了三个部分。JWT是URL-safe的，意味着可以用来查询字符参数。（译者注：也就是可以脱离URL，不用考虑URL的信息）。关于Json Web Token，参考http://self-issued.info/docs/draft-ietf-oauth-json-web-token.html

```json
{
  "typ" : "JWT",
  "alg" : "HS256"
}
```
 

在加密之后，这个对象变成了一个字符串：
eyJ0eXAiOiJKV1QiLA0KICJhbGciOiJIUzI1NiJ9
JWT的第二部分是token的核心，这部分同样是对一个js对象的编码，包含了一些摘要信息。有一些是必须的，有一些是选择性的。实例如下：

```json
{
  "iss": "joe",
  "exp": 1300819380,
  "http://example.com/is_root": true
}

````
 

这个结构被称为JWT Claims Set。这个iss是issuer的简写，表明请求的实体，可以是发出请求的用户的信息。exp是expires的简写，是用来指定token的生命周期。（相关参数参看：the document）加密编码之后如下：



eyJpc3MiOiJqb2UiLA0KICJleHAiOjEzMDA4MTkzODAsDQogImh0dHA6Ly9leGFtcGxlLmNvbS9pc19yb290Ijp0cnVlfQ

 

JWT的第三个部分，是JWT根据第一部分和第二部分的签名（Signature）。像这个样子：


dBjftJeZ4CVP-mB92K27uhbUJU1p1r_wW1gFWFOEjXk

 

最后将上面的合并起来，JWT token如下：

	

eyJ0eXAiOiJKV1QiLA0KICJhbGciOiJIUzI1NiJ9.eyJpc3MiOiJqb2UiLA0KICJleHAiOjEzMDA4MTkzODAsDQogImh0dHA6Ly9leGFtcGxlLmNvbS9pc19yb290Ijp0cnVlfQ.dBjftJeZ4CVP-mB92K27uhbUJU1p1r_wW1gFWFOEjXk