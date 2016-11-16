[CSS text-align 属性](http://www.w3school.com.cn/cssref/pr_text_text-align.asp)

<div id="maincontent">

# CSS text-align 属性

<div class="backtoreference">

[CSS 参考手册](/cssref/index.asp "CSS 参考手册")

</div>

<div>

## 实例

设置 h1、h2、h3 元素的文本对齐方式：

<pre>h1 {`text-align:center`}
h2 {`text-align:left`}
h3 {`text-align:right`}
</pre>

[亲自试一试](/tiy/t.asp?f=csse_text-align)

</div>

<div>

## 浏览器支持

<table class="dataintable browsersupport">
  <tbody><tr>
      <th>IE</th>
      <th>Firefox</th>
      <th>Chrome</th>
      <th>Safari</th>
      <th>Opera</th>
  </tr>
  <tr>
      <td class="bsIE"></td>						
      <td class="bsFirefox"></td>
      <td class="bsChrome"></td>
      <td class="bsSafari"></td>				
      <td class="bsOpera"></td>				
  </tr>
</tbody></table>

所有浏览器都支持 text-align 属性。

<span>注释：</span>任何的版本的 Internet Explorer （包括 IE8）都不支持属性值 "inherit"。

</div>

<div>

## 定义和用法

text-align 属性规定元素中的文本的水平对齐方式。

该属性通过指定行框与哪个点对齐，从而设置块级元素内文本的水平对齐方式。通过允许用户代理调整行内容中字母和字之间的间隔，可以支持值 justify；不同用户代理可能会得到不同的结果。

<table class="dataintable">
  <tbody><tr>
    <th style="width:25%;">默认值：</th>
    <td style="width:75%;">如果 direction 属性是 ltr，则默认值是 left；如果 direction 是 rtl，则为 right。</td>
  </tr>
  <tr>
    <th>继承性：</th>
    <td>yes</td>
  </tr>
  <tr>
    <th>版本：</th>
    <td>CSS1</td>
  </tr>
  <tr>
    <th>JavaScript 语法：</th>
    <td>_object_.style.textAlign="right"</td>
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
<td>left</td>
<td>把文本排列到左边。默认值：由浏览器决定。</td>
</tr>

<tr>
<td>right</td>
<td>把文本排列到右边。</td>
</tr>

<tr>
<td>center</td>
<td>把文本排列到中间。</td>
</tr>

<tr>
<td>justify</td>
<td>实现两端对齐文本效果。</td>
</tr>

<tr>
<td>inherit</td>
<td>规定应该从父元素继承 text-align 属性的值。</td>
</tr>
</tbody></table>

### 值 justify

最后一个水平对齐属性是 justify，它会带来自己的一些问题。

值 justify 可以使文本的两端都对齐。在两端对齐文本中，文本行的左右两端都放在父元素的内边界上。然后，调整单词和字母间的间隔，使各行的长度恰好相等。您也许已经注意到了，两端对齐文本在打印领域很常见。不过在 CSS 中，还需要多做些考虑。

要由用户代理（而不是 CSS）来确定两端对齐文本如何拉伸，以填满父元素左右边界之间的空间。例如，有些浏览器可能只在单词之间增加额外的空间，而另外一些浏览器可能会平均分布字母间的额外空间（不过 CSS 规范特别指出，如果 [letter-spacing 属性](pr_text_letter-spacing.asp "CSS letter-spacing 属性")指定为一个长度值，“用户代理不能进一步增加或减少字符间的空间”）。还有一些用户代理可能会减少某些行的空间，使文本挤得更紧密。所有这些做法都会影响元素的外观，甚至改变其高度，这取决于用户代理的对齐选择影响了多少文本行。

CSS 也没有指定应当如何处理连字符（注1）。大多数两端对齐文本都使用连字符将长单词分开放在两行上，从而缩小单词之间的间隔，改善文本行的外观。不过，由于 CSS 没有定义连字符行为，用户代理不太可能自动加连字符。因此，在 CSS 中，两端对齐文本看上去没有打印出来好看，特别是元素可能太窄，以至于每行只能放下几个单词。当然，使用窄设计元素是可以的，不过要当心相应的缺点。

<span>注1：</span>CSS 中没有说明如何处理连字符，因为不同的语言有不同的连字符规则。规范没有尝试去调和这样一些很可能不完备的规则，而是干脆不提这个问题。

</div>

<div class="example">

## TIY 实例

<dl>
<dt>[对齐文本](/tiy/t.asp?f=csse_text-align)</dt>
<dd>本例演示如何对齐文本。</dd>
</dl>
</div>

<div>

## 相关页面

CSS 教程：[CSS 文本](/css/css_text.asp "CSS 文本")

HTML DOM 参考手册：[textAlign 属性](/jsref/prop_style_textalign.asp "HTML DOM textAlign 属性")

</div>

<div class="backtoreference">

[CSS 参考手册](/cssref/index.asp "CSS 参考手册")

</div>

</div>