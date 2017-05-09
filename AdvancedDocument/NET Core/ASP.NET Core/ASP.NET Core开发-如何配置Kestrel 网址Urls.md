[ASP.NET Core开发-如何配置Kestrel 网址Urls](http://www.cnblogs.com/linezero/p/aspnetcorekestrelurls.html)

## UseUrls

## 配置文件

下面使用配置文件来设置网址。

首先在项目中添加一个ASP.NET 配置文件hosting.json，在配置文件中加入server.urls 节点。

```json
{
  "server.urls": "http://localhost:5001;http://localhost:5002;http://*:5003"
}
```

这里首先需要添加两个引用

```
"Microsoft.Extensions.Configuration.FileExtensions": "1.0.0",
"Microsoft.Extensions.Configuration.Json": "1.0.0"
```

然后Program.cs


```cs

public static void Main(string[] args)
{
    var config = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("hosting.json", optional: true)
        .Build();
    var host = new WebHostBuilder()
        .UseConfiguration(config)
        .UseKestrel()
        .UseContentRoot(Directory.GetCurrentDirectory())
        .UseIISIntegration()
        .UseStartup<Startup>()
        .Build();

    host.Run();
}

```

使用Kestrel运行程序,http://localhost:5001 http://localhost:5002 http://localhost:5003 均可访问。