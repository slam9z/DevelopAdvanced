[Horizontal List Scrolling with CSS](http://stackoverflow.com/questions/10402082/horizontal-list-scrolling-with-css)

You're on the right track! I think instead of setting 

```css
{
overflow: auto;  
overflow-y: hidden;
white-space:nowrap;//需要添加这个
}
```

you should just set the whole overflow to hidden, and give the list_scroller a bigger width. 
Something like this might work:

http://jsfiddle.net/mEg7g/1/

Good luck, I hope this helps. :D