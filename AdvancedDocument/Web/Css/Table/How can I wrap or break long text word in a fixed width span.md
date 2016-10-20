[How can I wrap or break long text/word in a fixed width span?](http://stackoverflow.com/questions/18225302/how-can-i-wrap-or-break-long-text-word-in-a-fixed-width-span)

##Question


I want to create a span with a fixed width that when I type any thing in the span like 
```<span>lgasdfjksdajgdsglkgsadfasdfadfasdfadsfasdfasddkgjk</span>```, a long string of non-spaced text, 
the word(s) break or wrap to next line.
Any ideas?

##Answer

You can use the CSS property word-wrap:break-word;, which will break words if they are too long for your span width. 

```css
span { 
    display:block;
    width:150px;
    word-wrap:break-word;
}

<span>VeryLongLongLongLongLongLongLongLongLongLongLongLongExample</span>
```

###comment
 
Works well for the asp.net label control. Thanks! – etlds Jun 27 '14 at 15:31 

  
Adding white-space: normal helps to override outer styling that may get in the way :) .. inline-block works as well as block – katia Jan 18 '15 at 12:32 
   
  
white-space: normal did work for me – Ziggler May 31 at 23:42 
   
  
What about breaking two spans which are inside a block element? – AllDani Sep 2 at 


##MyAnswer

这个方法可以很好出处理table cell被撑大的问题，之前的研究是使用table-layout:fixed解决但是不是一个想要的解决方法。

给tb里的文字加上span会比较好控制，这个才是我想要的解决方案。可以用*max-width*替代width更加合适，找回xaml开发的感觉。

```
display:block;
width:150px;
word-wrap:break-word;
```

这3个属性值一个都不能少。

