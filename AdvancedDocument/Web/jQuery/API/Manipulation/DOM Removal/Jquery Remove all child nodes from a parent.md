[Remove all child nodes from a parent?](http://stackoverflow.com/questions/3044780/remove-all-child-nodes-from-a-parent)


You can use `.empty()`, like this:

```js
$("#foo").empty();
```

From the docs:

> Remove all child nodes of the set of matched elements from the DOM.

