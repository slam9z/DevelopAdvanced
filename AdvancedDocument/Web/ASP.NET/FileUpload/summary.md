
为了兼容Web，默认实现使用表单form的enctype="multipart/form-data"。

##部署方式

* 独立的Web站点

    使用WebApi或者MVC的Control进行处理，实现比较简单

* 可以嵌入的模块

    使用IHttpHandler与IRouteHandler进行处理

不管那种逻辑使用IFileService接口进行封装处理。

##上传


###跨域上传文件不知道会有问题吗？


这个好像是不可行的，使用我的网页。

先将Data发送到业务服务器，再发送到文件服务器.这样没有减轻负担。


上传多个文件有难度吗？

###需要使用保存的数据

通过form字段或者querystring提交。

###使用场景

* 网页

    input file type直接提交

* Ajax调用

    使用ajax提交数据

##下载

通过什么参数指定文件，尤其是上传多个文件的时候。

例如使用业务逻辑与文件名

##文件存储

* 目录
    
    一定的业务逻辑

* 文件名

    上传的文件名或者Guid

