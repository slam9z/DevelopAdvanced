[CSS Horizontal list items](http://stackoverflow.com/questions/15710701/css-horizontal-list-items)

*Run code snippet* stackoverflow这个功能不错。

##answer

I've noticed a lot of people are using this answer so I decided to update it a little
bit. If you want to see the original answer, check below. The new answer demonstrates 
how you can add some style to your list.

```css
ul > li {
    display: inline-block;
    /* You can also add some margins here to make it look prettier */
    zoom:1;
    *display:inline;
    /* this fix is needed for IE7- */
}
```

```html
<ul>
    <li> <a href="#">some item</a>

    </li>
    <li> <a href="#">another item</a>

    </li>
</ul>
```