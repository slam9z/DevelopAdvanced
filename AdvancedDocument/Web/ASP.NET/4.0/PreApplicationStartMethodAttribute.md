[ASP.NET 4.0 一些隐性的扩展](http://kb.cnblogs.com/page/71599/)

ASP.NET 4.0在很多方面都做了改进，在这篇ASP.NET 4.0白皮书就描述了很多ASP.NET 4.0的机制
改变和改进。在我的博客中，也有几篇关于ASP.NET4.0的特性修改的文章。但是作为一个全新的框架和
运行时，内部肯定还会有很多API和扩展点不会暴露的那么明显。比如今天从这篇文章Three Hidden 
Extensibility Gems in ASP.NET 4的介绍中，我又了解了一些在我平常开发中绝对非常有用的扩展点。

##PreApplicationStartMethodAttribute
　　
这个新的Attribute可以让我们指定一个公共的静态函数，让它在站点的Application_Start之前执行该函
数。如果你的站点有App_code目录，这个函数同样也会在App_code目录下的代码被编译之前执行。从我的直
觉中，这是一个相当有用的扩展点。

我们必须从assembly级别上来使用这个attribute，也就是通常情况下我们会用在AssemblyInfo.cs中：

```cs
[assembly: PreApplicationStartMethod(
typeof(SomeClassLib.Initializer), "Initialize")]
```

我们需要指定的是类型和类型里面静态函数的名称，这个静态函数必须是不带参数和返回值的公共函数。

这个功能最重要的地方是在于，它可以做一些我们原来在Application_Start无法完成的事情，所以
很多事情在执行到Application_Start时已经完成了，不可改变的，比如下面要介绍的关于编译的扩展。
　　
###BuildProvider.RegisterBuildProvider

原来我们要注册BuildProvider都是通过添加web.config的<buildproviders>来完成。在ASP.NET 
4.0当中，我们就可以配合PreApplicationStartMethodAttribute，在站点启动前添加自定义的来
BuildProvider达到目的。

###BuildManager.AddReferencedAssembly

在做.ASPX/.ASPCX和App_code目录下的代码文件编译时，需要依赖一些程序集。以前，我们都需要
将这些程序集配置在web.config的<assemblies>节点下来完成。现在，你就只需要配合以上的attribute
和这个新的方法，直接通过代码的形式来增加这些依赖。

###Config-free IHttpModule Registration

这也是对PreApplicationStartMethodAttribute的一个绝对的妙用，在Nikhil Kothari的这篇文章中
有详细的介绍。它的主要目标，也是脱离Web.config就可以通过代码来注册IHttpModule。

总之虽然只是一个简单的attribute，可是它却非常的有用。当我们开发是一个可复用的框架时，我们
不可避免的会需要在程序中做很多相关的配置才能让程序跑起来。而以前，我们就只能要求用户通过
Web.config来完成，而当用户少了一个配置就可能会导致整个框架无法运行。如果我们能将这些必须的
配置，都在我们的框架内部来完成，这样就可以大大降低框架的使用门槛。