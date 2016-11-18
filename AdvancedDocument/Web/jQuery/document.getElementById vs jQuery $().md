[document.getElementById vs jQuery $()](http://stackoverflow.com/questions/4069982/document-getelementbyid-vs-jquery)

这个是非常基本的，不明白导致好多问题啊！

##question

Is this:

```js
var contents = document.getElementById('contents');
```

The same as this:

```js
var contents = $('#contents');
```
Given that jQuery is loaded?

##answer

Not exactly!!

```js
document.getElementById('contents'); //returns a HTML DOM Object

var contents = $('#contents');  //returns a jQuery Object
```

In jQuery, to get the same result as document.getElementById, you can access the jQuery Object 
and get the first element in the object (Remember JavaScript objects act similar to associative arrays).

```js
var contents = $('#contents')[0]; //returns a HTML DOM Object

//or

var contents = $('#contents').get(0);
```

