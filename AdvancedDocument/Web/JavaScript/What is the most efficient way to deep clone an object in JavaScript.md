[What is the most efficient way to deep clone an object in JavaScript?](http://stackoverflow.com/questions/122102/what-is-the-most-efficient-way-to-deep-clone-an-object-in-javascript)

I want to note that the `.clone()` method in jQuery only clones DOM elements. In order to clone JavaScript objects, you would do:

```cs
// Shallow copy
var newObject = jQuery.extend({}, oldObject);

// Deep copy
var newObject = jQuery.extend(true, {}, oldObject);
```

More information can be found in the jQuery documentation.

I also want to note that the deep copy is actually much smarter than what is shown above – it's able to avoid many traps (trying to deep extend a DOM element, for example). It's used frequently in jQuery core and in plugins to great effect.