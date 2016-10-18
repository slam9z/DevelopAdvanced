##设置文件名

```
Response.AddHeader("Content-Disposition", "attachment;filename=" + Server.UrlEncode(fileName));
```

##强制用户下载

image,html,pdf浏览器会直接打开，但是这不是我想要的。

```
//指定返回的是一个不能被客户端读取的流，必须被下载    
 
Response.ContentType = "application/octet-stream";
```



