HttpFileCollection

HttpHandler获取上传的Multipart/form-data

```cs
private void Upload(HttpContext context)
{
    HttpFileCollection files = context.Request.Files;
}

```

for与foreach结果是不一致的前面可以获取到HttpPostedFile对象，而后者只能获取到string对象。