[浏览器内核控制Meta标签说明文档](http://www.cnblogs.com/PEIYANGXINQU/p/3751040.html)

```html
<meta name="renderer" content="webkit"/>极速
<meta name="renderer" content="ie-comp"/>ie兼容
<meta name="renderer" content="ie-stand"/>ie标准
```

目前该功能已经在所有的360安全浏览器实现。我们也建议其它浏览器厂商一起支持这个实现。让这个控制标签成为行业标准。
在head标签中添加一行代码：

```html
<html>
  <head>
    <meta name="renderer" content="webkit|ie-comp|ie-stand">
  </head>
  <body>
  </body>
</html>
```

若页面需默认用极速核，增加标签：`<meta name="renderer" content="webkit"/>`  
若页面需默认用ie兼容内核，增加标签：`<meta name="renderer" content="ie-comp"/>`  
若页面需默认用ie标准内核，增加标签：`<meta name="renderer" content="ie-stand"/>`  

本人建议优先选择极速模式，不要用兼容模式，会出现样式兼容性问题。

其他更多功能：<http://se.360.cn/v6/help/meta.html>
