[ASP.NET网站路径](http://www.cnblogs.com/Laeb/archive/2006/12/05/583046.html)

使用网站资源时，需要经常使用资源路径。比如，在页面中使用 URL 引用不同路径中的图片文件。类似地，Web 应用代码也可能使用物理文件路径
来读写服务器端的文件。ASP.NET 提供不同的方法来引用资源并确定页面或其他资源的路径。 

##指定资源路径

ASP.NET 允许开发者通过多种方式引用页面元素或控件的外部资源文件。选择方式区别于客户端元素或服务器控件的类型。
 
###客户端元素

页面中非服务器控件直接发送给浏览器。在此前，需要依照标准 HTML URL 规则构造用于客户端元素的资源引用路径。
也可以使用完整（绝对）URL 路径或别的相关路径。比如 img 标记，可以将 src 属性设置成以下任何一种形式： 

* 绝对 URL 路径： 

    ```html
    <img src="http://www.contoso.com/MyApplication/Images/SampleImage.jpg" />
    ```
    绝对 URL 路径适用于引用对其他网站的资源。 

* 根目录相对路径针对网站（非应用程序）根目录进行解析。下例假设网站根目录存在 Images 目录：

    ```html 
    <img src="/Images/SampleImage.jpg" />
    ```
    如果网站地址是 http://www.contoso.com，上例中的路径将解析成： 
    http://www.contoso.com/Images/SampleImage.jpg

    根目录相对路径适用于引用跨应用程序资源（图片或客户端脚本文件）。 

* 相对路径针对当前路径进行解析：
 
    ```html
    <img src="Images/SampleImage.jpg" />
    ```

* 相对路径会解析成当前页面路径。 

    ```html
    <img src="../Images/SampleImage.jpg" />
    ```
    
    注意：默认时，浏览器参考当前页面的 URL 来解析相对路径。然而，使用 HTML 的 base 元素可以进行更换。 


###服务器控件


可以使用与客户端元素相同的方式指定 ASP.NET 服务器控件的资源引用路径。相对路径的解析会以当前页面，用户控件，
或主题文件路径作为参照。比如，Controls 目录有一个用户控件，包含 ImageUrl 属性被设置成如下路径的 Image Web 控件： 

```
Images/SampleImage.jpg
```
用户控件运行时，路径被解析成如下所示： 

```
/Controls/Images/SampleImage.jpg
```


不用考虑使用该用户控件的页面路径。 
在服务器控件中引用绝对路径或相对路径有下列缺点： 

* 绝对路径无法在应用程序间移植。有造成所有链接中断的隐患。 
* 客户端元素的相对路径在资源或页面移动时加大维护难度。 

为了克服这些缺点，ASP.NET 使用了新 Web 根目录符号（~），用于设置服务器控件的路径。ASP.NET 将符号 ~ 解析成当前应用程序的根目录。
可以用符号 ~ 与目录一道来指定基于当前根目录的路径。下例使用了符号 ~ 为服务器控件 Image 指定相对路径。 

```aspx
<asp:image runat="server" id="Image1" ImageUrl="~/Images/SampleImage.jpg" />
```

上例中的图片文件将会从网站根目录下的 Images 目录中读取，且当前页面可以放在网站的任何位置。 
说明：符号 ~ 仅适用于服务器端代码的服务器控件。不能在客户端元素中使用。 

服务器控件中任何与路径有关的属性都可使用符号 ~。 
注意：在模板页中，对资源路径的解析是基于内容页面的。 


##确定当前网站的物理路径

应用程序中，可能需要确定服务器文件或资源的路径。比如，当应用程序要对一个文本文件进行读写，就需要为读取和写入功能提供目标文件的物理路径。 
在应用程序中使用硬编码的物理路径（如 C:\Webste\MyApplication）并不是一个好的习惯，且路径会随着文件的移动或布署而发生变化。
这时候可以通过基点参考的方法来为资源创建完整路径。ASP.NET 中常用来确定文件路径有两个方法：HttpRequest 对象属性，以及 MapPath 方法。

 
注意：为防止恶意用户获取应用程序的机密信息，请不要将物理文件路径发送到客户端。 
从 Request 对象的属性中确定路径
下表是 HttpRequest 对象的属性列表，可用来确定应用程序中的资源路径。 
首先，假设用浏览器发送如下请求： 

```
http://www.contoso.com/MyApplication/MyPages/Default.aspx
```
在本文的例子中，对“虚拟路径”的参考都将服务器标识作为 URI 的一部分；而本例中的虚拟路径则是这样：

``` 
/MyApplication/MyPages/Default.aspx
```
另外，还假设网站根目录的物理路径如下： 

```
C:\inetpub\wwwroot\MyApplication\
```

最后，还假设物理路径中包含子目录 MyPages。 

##使用 MapPath 方法
MapPath 方法以参数的方式接收虚拟路径，并返回对应的物理路径。在本例中，下面的代码返回网站根目录文件的物理路径： 

```c#
String rootPath = Server.MapPath("~");
```

注意：传递给 MapPath 方法的路径必须是相对路径，不能使用绝对路径。 


##补充

```
./Images/SampleImage.jpg"
```

也表示当前目录的文件