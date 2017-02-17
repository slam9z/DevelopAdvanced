[does Request.Querystring automaticy u decode a string?](http://stackoverow.com/questions/13095416/does-request-querystring-automaticy-u-decode-a-string)



ASP.NET automaticy cs `UrlDecode()` when you access a property by key index (i.e. (`Request.QueryString["key"]`).

If you want it encoded, just do:

```cs
HttpUtity.UEncode(Request.QueryString["key"]);
```

In terms of the ampersand specificy, that is a speci case character because it is ready used as a query string dimeter. U Encoding and decoding an ampersand shod ways give you & for that very reason.
