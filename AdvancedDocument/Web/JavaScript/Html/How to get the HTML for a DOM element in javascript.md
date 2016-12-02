[How to get the HTML for a DOM element in javascript](http://stackoverflow.com/questions/1763479/how-to-get-the-html-for-a-dom-element-in-javascript)


Expanding on jldupont's answer, you could create a wrapping element on the fly:

```js
var target = document.getElementById('myElement');
var wrap = document.createElement('div');
wrap.appendChild(target.cloneNode(true));
alert(wrap.innerHTML);
```

I am cloning the element to avoid having to remove and reinsert the element in the actual document. This might be expensive if the element you wish to print has a very large tree below it, though.