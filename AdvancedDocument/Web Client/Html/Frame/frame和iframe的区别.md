﻿##1. frame不能脱离frameSet单独使用，iframe可以； 


##2. frame不能放在body中；如下可以正常显示：

```html 
<!--<body>--> 
<frameset rows="50%,*"> 
    <frame name="frame1" src="test1.htm"/> 
    <frame name="frame2" src="test2.htm"/> 
</frameset> 
<!--<body>--> 
```

如下不能正常显示： 

```html 
<body> 
<frameset rows="50%,*"> 
    <frame name="frame1" src="test1.htm"/>
    <frame  name="frame2"  src="test2.htm"/>
</frameset> 
<body> 
```


##3. 嵌套在frameSet中的iframe必需放在body中；如下可以正常显示： 

```html
<body> 
    <frameset>
         <iframe name="frame1" src="test1.htm"/> 
         <iframe name="frame2" src="test2.htm"/>
     </frameset>
</body> 
```

如下不能正常显示： 

```html
<!--<body>--> 
<frameset> 
    <iframe name="frame1" src="test1.htm"/> 
    <iframe name="frame2"src="test2.htm"/> 
</frameset>  
<!--</body>--> 
```


###4. 不嵌套在frameSet中的iframe可以随意使用； 

如下均可以正常显示： 

```html
<body> 
    <iframe  name="frame1" src="test1.htm"/>
    <iframe  name="frame2"  src="test2.htm"/> 
</body> 


<!--<body>--> 
<iframe name="frame1" src="test1.htm"/>
<iframe name="frame2" src="test2.htm"/> 
<!--</body>--> 
```

##5. frame的高度只能通过frameSet控制；iframe可以自己控制，不能通过frameSet控制，如： 

```html
<!--<body>--> 
<frameset rows="50%,*"> 
   <frame   name="frame1"   src="test1.htm"/>  
   <frame   name="frame2"   src="test2.htm"/>  
</frameset> 
<!--</body>--> 

<body> 
<frameset> 
    <iframe height="30%"  name="frame1"   src="test1.htm"/>  
    <iframe height="100"  name="frame2"   src="test2.htm"/>  
</frameset> 
</body> 
```

##6. 如果在同一个页面使用了两个以上的iframe，在IE中可以正常显示，在firefox中只能显示出第一个；
    使用两个以上的frame在IE和firefox中均可正常 

以上代码在IE7和firefox2.0中测试。 


##7. 另外相关论坛窃取总结 
1. Frame与Iframe两者可以实现的功能基本相同，不过Iframe比Frame具有更多的灵活性。 
    frame是整个页面的框架，iframe是内嵌的网页元素，也可以说是内嵌的框架 

    Iframe标记又叫浮动帧标记，可以用它将一个HTML文档嵌入在一个HTML中显示。
    它和Frame标记的最大区别是在网页中嵌入的<Iframe></Iframe>所包含的内容与整个页面是一个整体，而<Frame></Frame>所包含的内容是一个独立的个体，是可以独立显示的。
    另外，应用Iframe还可以在同一个页面中多次显示同一内容，而不必重复这段内容的代码。 

2. iframe 可以放到表格里面。frame 则不行。 

    ```html    
    <table> 
        <tr> 
        <td><iframe id="" src=""></iframe></td><td></td> 
        </tr> 
    </table>
    ```

3. frame必须在frameset里 
    而frameset不能与body元素共存，也就说有frameset元素的文档只能是一个框架集，不能有别的东东 

4. IFrame是放在网业的什么地方都行   
    但是frame只能放到上下左右四个方向 

5. iframme 是活动帧, 而frame是非活动帧   

    iframe使用方法如下   
    ```html
    <iframe   scr="sourcefile"   frameborder=0   width="width"   height="height"></iframe> 
    ```
    iframe用起来更灵活，不需要frame那么多讲究   
    而且放的位置也可以自己设 
    iframe是内嵌的，比较灵活，不过也有不好的地方，就是位置在不同的浏览器和分辨率下有可能不同，有时会把本来好好的页面搞得变形 
    iframe就没有这个限制 
    

6. iframe可以加在网页中任何一个地方。而frame通常做框架页 

    iframe是一个网页中的子框架,两网页间是父子关系   
    
    frame是框架,由多个并列的网页构成 
    楼上的说得对，iframe是浮动的。就像是浮动面板，而frame是固定的。只能四个方向上的。   
    你可以直接在网页里用一下，看看效果就行了。 


7.  iframe是被嵌入在网页的元素，而frame用于组成一个页面的多个框架！ 
    iframe   更利于版面的设计   
    frame     一条直一条竖的不美观 
    
    frame的那一条线也可以去掉的呦！只不过，iframe更方便对其进行数据的交换吧！ 
    iframe可以放置到你想放的任意位置,控制起来比frame方便 

8. iframe是内部帧，可以嵌在一个页面里面，设置内部帧的属性可以使得整体看上去象一个完整的页面，而不是由多个页面组成，
    frame有frame的好处，比如何多网站，上面放广告条，左边放菜单，右边放内容，这样上边和左边的内容都可不动，只刷新右边页面的内容，选
    择iframe还是frame完全看自己的需求。 

    说白了，用IFrame比用Frame少一个文件（FrameSet），但支持Frame的浏览器比较多。 

    我为我公司做的网站，整个是用了iframe，linux带的浏览器都不支持，哎呀，丑呀，不过我还是喜欢用iframe 

    还有iframe可以放在表格里,然后iframe设置成width=100%   height=100%   
    我就可以只需修改我的表格的宽度和高度,这样的话有利于排版  

    其实Frame是一个控件   
    使用方法和Panle相同。 

    frame是把网页分成多个页面的页面。它要有一个框架集页面frameset
    iframe是一个浮动的框架,就是在你的页面里再加上一个页面, 

    frame用来把页面横着或竖着切开，   
    iframe用来在页面中插入一个矩形的小窗口 

    Frame一般用来设置页面布局,将整个页面分成规则的几块,每一块里面包含一个新页面.
    iframe用来在页面的任何地方插入一个新的页面.
    
    因此,Frame用来控制页面格式,比如一本书,左边是章节目录,右边是正文,正文很长,看的时候要拖动,但又不想目录也被拖动得开不到了
    因此最好将页面用Frame分成规则的2页,一左一右.

    而iframe则更灵活,不要求将整个页面划分,你可以在页面任何地方用iframe嵌入新的页面. 

##我个人认为:
    <frame>用于全页面
    <iframe>只用于局部
