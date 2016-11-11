[How can I merge properties of two JavaScript objects dynamically?](http://stackoverflow.com/questions/171251/how-can-i-merge-properties-of-two-javascript-objects-dynamically)


##jquery answer

jQuery also has a utility for this: http://api.jquery.com/jQuery.extend/.

Taken from the jQuery documentation:

```js
// Merge options object into settings object
var settings = { validate: false, limit: 5, name: "foo" };
var options  = { validate: true, name: "bar" };
jQuery.extend(settings, options);

// Now the content of settings object is the following:
// { validate: true, limit: 5, name: "bar" }
```