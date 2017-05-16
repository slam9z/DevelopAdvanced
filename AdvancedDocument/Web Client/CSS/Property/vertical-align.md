[vertical-align](http://www.w3school.com.cn/cssref/pr_pos_vertical-align.asp)

>等我把工具写好再来转换吧，懒得手工改了。

<div id="maincontent">

# CSS vertical-align 属性

<div class="backtoreference">

[CSS 参考手册](/cssref/index.asp "CSS 参考手册")

</div>

<div>

## 实例

垂直对齐一幅图像：

<pre>img
  {
  `vertical-align:text-top;`
  }
</pre>

[亲自试一试](/tiy/t.asp?f=csse_vertical-align)

</div>

<div>

## 浏览器支持

所有浏览器都支持 vertical-align 属性。

<span>注释：</span>任何的版本的 Internet Explorer （包括 IE8）都不支持属性值 "inherit"。

</div>

<div>

## 定义和用法

vertical-align 属性设置元素的垂直对齐方式。

### 说明

该属性定义行内元素的基线相对于该元素所在行的基线的垂直对齐。允许指定负长度值和百分比值。这会使元素降低而不是升高。在表单元格中，这个属性会设置单元格框中的单元格内容的对齐方式。

<table class="dataintable">
  <tbody><tr>
    <th style="width:25%;">默认值：</th>
    <td style="width:75%;">baseline</td>
  </tr>
  <tr>
    <th>继承性：</th>
    <td>no</td>
  </tr>
  <tr>
    <th>版本：</th>
    <td>CSS1</td>
  </tr>
  <tr>
    <th>JavaScript 语法：</th>
    <td>_object_.style.verticalAlign="bottom"</td>
  </tr>
</tbody></table>
</div>

<div>

## 可能的值

<table class="dataintable">
<tbody><tr>
<th>值</th>
<th>描述</th>
</tr>

<tr>
<td>baseline</td>
<td>默认。元素放置在父元素的基线上。</td>
</tr>

<tr>
<td>sub</td>
<td>垂直对齐文本的下标。</td>
</tr>

<tr>
<td>super</td>
<td>垂直对齐文本的上标</td>
</tr>

<tr>
<td>top</td>
<td>把元素的顶端与行中最高元素的顶端对齐</td>
</tr>

<tr>
<td>text-top</td>
<td>把元素的顶端与父元素字体的顶端对齐</td>
</tr>

<tr>
<td>middle</td>
<td>把此元素放置在父元素的中部。</td>
</tr>

<tr>
<td>bottom</td>
<td>把元素的顶端与行中最低的元素的顶端对齐。</td>
</tr>

<tr>
<td>text-bottom</td>
<td>把元素的底端与父元素字体的底端对齐。</td>
</tr>

<tr>
<td>length</td>
<td>&nbsp;</td>
</tr>

<tr>
<td>%</td>
<td>使用 "line-height" 属性的百分比值来排列此元素。允许使用负值。</td>
</tr>

<tr>
<td>inherit</td>
<td>规定应该从父元素继承 vertical-align 属性的值。</td>
</tr>
</tbody></table>
</div>

<div class="example">

## TIY 实例

<dl>
<dt>[垂直对齐图象](/tiy/t.asp?f=csse_vertical-align)</dt>
<dd>本例演示如何在文本中垂直排列图象。</dd>
</dl>
</div>

<div>

## 相关页面

CSS 教程：[CSS 文本](/css/css_text.asp "CSS 文本")

HTML DOM 参考手册：[verticalAlign 属性](/jsref/prop_style_verticalalign.asp "HTML DOM verticalAlign 属性")

</div>

<div class="backtoreference">

[CSS 参考手册](/cssref/index.asp "CSS 参考手册")

</div>

</div>