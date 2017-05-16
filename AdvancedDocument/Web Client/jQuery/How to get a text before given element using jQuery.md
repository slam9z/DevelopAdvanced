[How to get a text before given element using jQuery?](http://stackoverflow.com/questions/1543648/how-to-get-a-text-before-given-element-using-jquery)


## answer

How about collecting them into an array instead of two calls to $:

```js
var texts = $('span, b').map(function(){
    return this.previousSibling.nodeValue
});
texts[0]; // "Some text followed by "
texts[1]; // " and another text followed by "
```

References:

    The previousSibling property
    jQuery.map



## answer

```js
var textBefore = $('span')[0].previousSibling.nodeValue;
```

I was looking for a solution to this, and was confused why I needed to map to an array. Context was not working for me. I finally figured out how to write it.

This is the most basic solution. Putting the [0] converts the element to an elementNode, which allows you to call the previousSibling code. There's no need to put it in an array using the map function unless it is more convenient.
