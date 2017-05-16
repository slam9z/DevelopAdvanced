```
ReferenceError event is not defined  firefox
```

```js
if (event)
    title = $(event.srcElement).text();
```

```
if (window.event) //或者this.event  ==undefine
    title = $(window.event.srcElement).text();    
```    