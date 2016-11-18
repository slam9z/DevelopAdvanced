[AJAX 浏览器支持](http://www.w3school.com.cn/ajax/ajax_browsers.asp)

AJAX - 浏览器支持
AJAX 的要点是 XMLHttpRequest 对象。
不同的浏览器创建 XMLHttpRequest 对象的方法是有差异的。
IE 浏览器使用 ActiveXObject，而其他的浏览器使用名为 XMLHttpRequest 的 JavaScript 内建对象。
如需针对不同的浏览器来创建此对象，我们要使用一条 "try and catch" 语句。您可以在我们的 JavaScript 教程中阅读更多有关 try 和 catch 语句 的内容。
让我们用这段创建 XMLHttpRequest 对象的 JavaScript 来更新一下我们的 "testAjax.htm" 文件：

```html
<html>
<body>

<script type="text/javascript">

function ajaxFunction()
 {
 var xmlHttp;
 
 try
    {
   // Firefox, Opera 8.0+, Safari
    xmlHttp=new XMLHttpRequest();
    }
 catch (e)
    {

  // Internet Explorer
   try
      {
      xmlHttp=new ActiveXObject("Msxml2.XMLHTTP");
      }
   catch (e)
      {

      try
         {
         xmlHttp=new ActiveXObject("Microsoft.XMLHTTP");
         }
      catch (e)
         {
         alert("您的浏览器不支持AJAX！");
         return false;
         }
      }
    }
 }
</script>

<form name="myForm">
用户: <input type="text" name="username" />
时间: <input type="text" name="time" />
</form></body>
</html>
```

例子解释：
首先声明一个保存 XMLHttpRequest 对象的 xmlHttp 变量。
然后使用 XMLHttp=new XMLHttpRequest() 来创建此对象。这条语句针对 Firefox、Opera 以及 Safari 浏览器。假如失败，则尝试针对 Internet 
Explorer 6.0+ 的 xmlHttp=new ActiveXObject("Msxml2.XMLHTTP")，假如也不成功，则尝试针对 Internet Explorer 5.5+ 的 xmlHttp=new 
ActiveXObject("Microsoft.XMLHTTP")。

假如这三种方法都不起作用，那么这个用户所使用的浏览器已经太过时了，他或她会看到一个声明此浏览器不支持 AJAX 的提示。
注释：上面这些浏览器定制的代码很长，也很复杂。不过，每当您希望创建 XMLHttpRequest 对象时，这些代码就能派上用场，因此您可以在任何需要使
用的时间拷贝粘贴这些代码。上面这些代码兼容所有的主流浏览器：Internet Explorer、Opera、Firefox 以及 Safari。
