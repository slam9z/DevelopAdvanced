[了解ASP.NET MVC几种ActionResult的本质：FileResult ](http://www.cnblogs.com/artech/archive/2012/08/14/action-result-02.html)

##一、FileResult

如下面的代码片断所示，FileResult具有一个表示媒体类型的只读属性ContentType，该属性在构造函数中被初始化。
当我们基于某个物理文件创建相应的FileResult对象的时候应该根据文件的类型指定媒体类型，比如说目标文件是一个.jpg图片，那么对应的媒体类型为“image/jpeg”，
对于一个.pdf文件，则采用“application/pdf”。

``` C#
public abstract class FileResult : ActionResult
{    
    protected FileResult(string contentType);
    public override void ExecuteResult(ControllerContext context);
    protected abstract void WriteFile(HttpResponseBase response);
    
    public string ContentType { get; }
    public string FileDownloadName { get; set; }   
}
```

针对文件的响应具有两种形式，即内联（Inline）和附件（Attachment）。一般来说，前者会利用浏览器直接打开响应的文件，而后者会以独立的文件下载到客户端。
\对于后者，我们一般会为下载的文件指定一个文件名，这个文件名可以通过FileResult的FileDownloadName属性来指定。文件响应在默认情况下采用内联的方式，
如果需要采用附件的形式，需要为响应创建一个名称为Content-Disposition的报头，该报头值的格式为“attachment; filename={ FileDownloadName }”。


##二、FileContentResult


##三、FilePathResult

从名称可以看出，FilePathResult是一个根据物理文件路径创建FileResult。

##四、FileStreamResult

FileStreamResult允许我们通过一个用于读取文件内容的流来创建FileResult。

##五、实例演示：通过FileResult发布图片