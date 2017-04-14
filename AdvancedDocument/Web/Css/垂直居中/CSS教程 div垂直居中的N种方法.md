[CSS教程:div垂直居中的N种方法](http://www.cnblogs.com/chuncn/archive/2008/10/09/1307321.html)

在说到这个问题的时候，也许有人会问CSS中不是有vertical-align属性来设置垂直居中的吗？即使是某些浏览器不支持我只需做少许的CSS

Hack技术就可以啊！所以在这里我还要啰嗦两句，CSS中的确是有vertical-align属性，但是它只对(X)HTML元素中拥有valign特性的元素才生

效，例如表格元素中的<td>、<th>、<caption>等，而像<div>、<span>这样的元素是没有valign特性的，因此使用vertical-align对它们不起

作用。 

相关教程：div水平居中的N种方法

## 一、单行文字垂直居中 

> 貌似只能实现文件的垂直居中 


如果一个容器中只有一行文字，对它实现居中相对比较简单，我们只需要设置它的实际高度height和所在行的高度line-height相等即可。

如： 

```css
div {  
        height:25px;  
        line-height:25px;  
        overflow:hidden;  
 }  
``` 
    这段代码很简，后面使用overflow:hidden的设置是为了防止内容超出容器或者产生自动换行，这样就达不到垂直居中效果了。更多CSS教

程。  


## 二、多行未知高度文字的垂直居中 

如果一段内容，它的高度是可变的那么我们就可以使用上一节讲到的实现水平居中时使用到的最后一种方法，就是设定Padding，使上下的

padding值相同即可。同样的，这也是一种“看起来”的垂直居中方式，它只不过是使文字把<div>完全填充的一种访求而已。可以使用类似下

面的代码： 

```css
div {  
 padding:25px;  
}  
```
这种方法的优点就是它可以在任何浏览器上运行，并且代码很简单，只不过这种方法应用的前提就是容器的高度必须是可伸缩的。  


## 三、多行文本固定高度的居中

在本文的一开始，我们已经说过CSS中的vertical-align属性只会对拥有valign特性的(X)HTML标签起作用，但是在CSS中还有一个display

属性能够模拟<table>，所以我们可以使用这个属性来让<div>模拟<table>就可以使用vertical-align了。注意，display:table和

display:table-cell的使用方法，前者必须设置在父元素上，后者必须设置在子元素上，因此我们要为需要定位的文本再增加一个<div>元素：

> 原来还有这样的讲究！在div两个元素居中怎么办？ IE7就有问题，IE8OK。

```css
div#wrap {  
    height:400px;  
    display:table;  
}  
div#content {  
    vertical-align:middle;  
    display:table-cell;  
    border:1px solid #FF0099;  
    background-color:#FFCCFF;  
    width:760px;  
}  
```

这个方法应该是很理想了，但是不幸的是Internet Explorer 6 并不能正确地理解display:table和display:table-cell，因此这种方法在

Internet Explorer 6及以下的版本中是无效的。嗯，这让人很郁闷！不过我们还其它的办法


## 四、在Internet Explorer中的解决方案

在Internet Explorer 6及以下版本中，在高度的计算上存在着缺陷的。在Internet Explorer 6中对父元素进行定位后，如果再对子元素

进行百分比计算时，计算的基础似乎是有继承性的（如果定位的数值是绝对数值没有这个问题，但是使用百分比计算的基础将不再是该元素的

高度，而从父元素继承来的定位高度）。例如，我们有下面这样一个(X)HTML代码段： 

```html
<div id="wrap"> 
 <div id="subwrap"> 
   <div id="content"> 
 </div> 
</div>
</div>
```
    如果我们对subwrap进行了绝对定位，那么content也会继承了这个这个属性，虽然它不会在页面中马上显示出来，但是如果再对content进

行相对定位的时候，你使用的100%分比将不再是content原有的高度。例如，我们设定了subwrap的position为40%，我们如果想使content的上

边缘和wrap重合的话就必须设置top:-80%;那么，如果我们设定subwrap的top:50%的话，我们必须使用100%才能使content回到原来的位置上去

，但是如果我们把content也设置50%呢？那么它就正好垂直居中了。所以我们可以使用这中方法来实现Internet Explorer 6中的垂直居中： 

```css
div#wrap {  
    border:1px solid #FF0099;  
 background-color:#FFCCFF;  
 width:760px;  
  height:400px;  
 position:relative;  
}  
div#subwrap {  
  position:absolute;  
    border:1px solid #000;  
    top:50%;  
}  
div#content {  
    border:1px solid #000;  
    position:relative;  
    top:-50%;  
}  
```

    当然，这段代码只能在Internet Exlporer 6等计算存在问题的浏览器中才会有作用。（不过我不解，我查阅了很多文章，不知道是因为出

处相同还是什么原因，似乎很多人都不愿意去解释Internet Exlporer 6中这这个Bug的原理，我也只是了解了一点皮毛，还要再研究）  