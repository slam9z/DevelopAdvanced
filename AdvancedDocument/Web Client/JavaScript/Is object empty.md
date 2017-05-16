[Is object empty](http://stackoverflow.com/questions/4994201/is-object-empty)


## answer

Easy and cross-browser way is by using jQuery.isEmptyObject:

```js
if ($.isEmptyObject(obj))
{
    // do something
}
```