[github上Markdown不支持LaTeX吗？](https://www.zhihu.com/question/26887527)

##answer

Markdown 原生就是不支持LaTeX的，GitHub Flavored Markdown 同样不支持。Markdown 的初衷就是成为一种与HTML兼容的
“易读易写”的轻量级的网络标记语言，并不要求支持 LaTeX。只能说你遇到的支持 LaTeX 的网站比较用心（或者说其用户群体有输
入数学公式的特殊需求），给用户提供了 MathJax 来转换 LaTeX 指令。


##answer

谢邀。Github的markdown解析是通过SunDown库实现的。这个库的宗旨就是"Standards compliant, fast,securemarkdown 
processing library in C"。快就够用就行。并没打算加latex功能。真的要写的话在CodeCogs Equation Editor线上生成然后
 ![](http://latex.codecogs.com/gif.latex?\\frac{1}{1+sin(x)})

效果：来插入就好，（反正是直接在url里面写就好一样很流畅，注意要双反斜线\\来escape）线上想画什么基本都有实现，比如哪天
想在github上加uml图的话可以用http://yuml.me/diagram/scruffy/class/samples。

作者：Alex dcrozz
链接：https://www.zhihu.com/question/26887527/answer/127906478
来源：知乎
著作权归作者所有，转载请联系作者获得授权。