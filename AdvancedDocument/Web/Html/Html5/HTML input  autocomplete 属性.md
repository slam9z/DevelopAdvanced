[HTML <input> autocomplete 属性](http://www.w3school.com.cn/tags/att_input_autocomplete.asp)

##Problem

在Edge浏览器里input都会弹出下来框，显示之前输过的值。

第三方的日期控件也这样，让人崩溃。

然后看了bilibili不会，并且发现这个autocomplete属性。


##autocomplete

##定义和用法

autocomplete 属性规定输入字段是否应该启用自动完成功能。

自动完成允许浏览器预测对字段的输入。当用户在字段开始键入时，浏览器基于之前键入过的值，应该显示出在字段中填写的选项。

注释：autocomplete 属性适用于 <form>，以及下面的 <input> 类型：
text, search, url, telephone, email, password, datepickers, range 以及 color。

提示：在某些浏览器中，您可能需要手动启用自动完成功能。
HTML 4.01 与 HTML 5 之间的差异
autocomplete 属性是 HTML5 中的新属性。

##语法

```html
<input autocomplete="value">
```

##属性值

|值 |描述                |
|---|-------------------|
|on |默认。规定启用自动完成功能。|
|off|规定禁用自动完成功能。|


