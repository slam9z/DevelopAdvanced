[An Overview of Project Katana](http://www.asp.net/aspnet/overview/owin-and-katana/an-overview-of-project-katana)

##Why Katana – Why Now?

##The Open Web Interface for .NET (OWIN)

The resulting abstraction consists of two core elements. The first is the environment dictionary.
This data structure is responsible for storing all of the state necessary for processing an HTTP request and response, 
as well as any relevant server state. The environment dictionary is defined as follows:

``` C#
IDictionary<string, object> 
```


The second key element of OWIN is the application delegate. This is a function signature which serves as the primary interface between 
all components in an OWIN application. The definition for the application delegate is as follows:

``` C#
Func<IDictionary<string, object>, Task>;
``` 

OWIN is a specification (http://owin.org/html/owin.html). 

##Katana Architecture
![](http://media-www-asp.azureedge.net/media/4305511/arch.png)


##Host

At present, there are 3 primary hosting options for Katana-based applications:

1. IIS/ASP.NET: Using the standard HttpModule and HttpHandler types, OWIN pipelines can run on IIS as a part of an ASP.NET request flow. 

2. Custom Host: The Katana component suite gives a developer the ability to host applications in her own custom process, whether that is a console application, Windows service, etc.

3. OwinHost.exe: While some will want to write a custom process to run Katana Web applications, many would prefer to simply launch a pre-built executable that can start a server and run their application. 


##Server

* *Microsoft.Owin.Host.SystemWeb*: As previously mentioned, IIS in concert with the ASP.NET pipeline acts as both a host and a server
* *Microsoft.Owin.Host.HttpListener*: As its name indicates, this Katana server uses the .NET Framework’s HttpListener class to open a socket and send requests into a developer-specified OWIN pipeline

##Middleware/framework

##Applications

##Components – NuGet Packages

![](http://media-www-asp.azureedge.net/media/4388678/owin.png)


##Kanata的消失

[快刀斩乱麻之 Katana](http://www.cnblogs.com/xishuai/p/asp-net-5-owin-katana.html)


[kanata source code](http://katanaproject.codeplex.com/SourceControl/latest#README)

[kanata sample](http://aspnet.codeplex.com/SourceControl/latest)
