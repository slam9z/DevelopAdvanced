[Click event on dynamically generated list items using jquery](http://stackoverflow.com/questions/14418451/click-event-on-dynamically-generated-list-items-using-jquery)

> When I run my program my list looks perfect and includes my static li plus my dynamic ones, but I cannot click on the dynamic ones, only static.

That's because, the way your code binds the click handler, it is only bound to elements in the page at the time that the the listener is bound. Set up the click listener just a little differently and it will work, by taking advantage of event delegation:

```js
$('#recentProjectsId').on('click', 'li', function () {
    // snip...
});
```

By specifying an additional selector argument to .on():

> A selector string to filter the descendants of the selected elements that trigger the event. If the selector is null or omitted, the event is always triggered when it reaches the selected element.v