[OIDC–基于 OAuth2 的下一代身份认证授权协议](http://www.oschina.net/news/73532/oidc-oauth-2)


OIDC(OpenID Connect)，下一代的身份认证授权协议；当前发布版本1.0；

OIDC是基于OAuth2+OpenID整合的新的认证授权协议；OAuth2是一个授权(authorization)的开放协议, 在全世界得到广泛使用，但在实际使用中，OAuth2只解决了授权问题，没有实现认证部分，往往需要添加额外的API来实现认证；而OpenID呢，是一个认证(authentication )的协议，二者在实际使用过程中都有其局限性；

综合二者，即是OIDC；通过OIDC，既能有OAUTH2的功能，也有OpenID的功能；恰到好处…

OIDC将是替换(或升级)OAuth2，OpenID的不二选择..

OIDC在OAuth2的access_token的基础上增加了身份认证信息；通过公钥私钥配合校验获取身份等其他信息——即idToken;

一个使用JWT生成的idToken(base64):

```
     eyJhbGciOiJSUzI1NiIsImtpZCI6IjM3MTc2NjA0OTExODEyNzkwNzgifQ.eyJpc3MiOiIxMTExIiwiYXVkIjoiMTExMSIsImF0X2hhc2giOiI4ZjgxYThjOS1jNWJiLTQwOWMtYjI0Ni1lMzEyZmUwYzM4NWMiLCJyZWdpc3RyYXRpb24iOiIxMjM0NTY3OCIsImV4cCI6MTQ2MzYyMjA4NiwianRpIjoiRnl5aGZOYnQtU0NLR2tpTWRGMVg2dyIsImlhdCI6MTQ2MzU3ODg4NiwibmJmIjoxNDYzNTc4ODI2LCJzdWIiOiJsc3otb2lkYyJ9.hDCcs8PISdwUPp6Eyd-9JCeeTJ2ZtscBeuPITIt43gMYbddiUBLC90uT9bxKe6e3awHels3asEMreFtlnlY09PwdCxXvhjYcEiXO_dnzqu-zQXESHzPEE6d1WsZUcbj6yxoxMh0laba24uu3CbqSRQbOrsYmh2_XA5Q5eP66iOajRUDhNXhmsWEL85jtL9_h0SyfRNPZ9C0mRu2x9YZTHT129O53ggqtjwQxrXLAbCd1dd35DyIztagqQWDpo3gFG7YseNEiQ6Mf2D6nIBU9llAqH4sTThq_ahME06qKENat_sxnmIJN2UHw7u0E08S-59oxtOY9winT78Qj5IfWJw
```

在OIDC协议的实现中, 其底层是基于OAuth2. 一些常用的库如: JWT(https://jwt.io/), JWS; OAuth2的实现如: Spring Security OAuth，OLTU。

更多信息可参考: http://openid.net/connect/