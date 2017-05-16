[使用X-UA-Compatible来设置IE浏览器兼容模式](http://www.cnblogs.com/nidilzhang/archive/2010/01/09/1642887.html)

> 不设置`X-UA-Compatible`,IE8默认使用IE7的DocumentMode,来解析远程网页。

> 远程的域名被设置了默认的兼容模式。

## 认识内容属性值

内容属性值在接收到异于先前叙述的数值时是具有弹性的。这能使你对于IE如何显示你的网页更有操控性。举例来说，你可以设定内容属性值为IE=7.5。当你这样做的时候，IE尝试将这个值转换为version vector并选择最接近的结果。在这个例子中，IE会将其设定为IE7 mode。下面的范例显示该模式设定为其他值的状况。

```html
<meta http-equiv="X-UA-Compatible" content="IE=4"> <!-- IE5 mode -->
<meta http-equiv="X-UA-Compatible" content="IE=7.5"> <!-- IE7 mode -->
<meta http-equiv="X-UA-Compatible" content="IE=100"> <!-- IE8 mode -->
<meta http-equiv="X-UA-Compatible" content="IE=a"> <!-- IE5 mode -->

<!-- This header mimics Internet Explorer 7 and uses
<!DOCTYPE> to determine how to display the Web page -->
<meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7">
```

注意: 前面的范例显示单独的内容值。实际上IE只会执行网页中第一个X-UA-Compatible标头。

你也可以使用内容属性来指定复数的文件兼容性模式，这能帮助确保你的网页在未来的浏览器版本都能一致的显示。欲设定复数的文件模式，请设定内容属性以判别你想使用的模式。使用分号来分开各个模式。

如果一个特定版本的IE支持所要求的兼容性模式多于一种，**将採用列于标头内容属性中最高的可用模式**。你可以使用这个特性来排除特定的兼容性模式，虽然并不推荐这样做。举例来说，下列标头即会排除IE7 mode。

```html
<meta http-equiv="X-UA-Compatible" content="IE=5; IE=8" />
```

