[JS for and for…in with a string](http://stackoverflow.com/questions/41830444/js-for-and-for-in-with-a-string)



In the for ... in loop, the values of i will be strings, not numbers. Property names are always strings*.

Thus, i + 1 means 11 when i is "1". The .slice() code will treat that as a number, and since there are only 3 characters in the string the result of that is an empty string.

In general, for ... in is not a good idea unless you really know that what you want to do is iterate through all enumerable properties of the object and its prototype chain. When iterating through the numbered properties of an array (or array-like object), an old-fashioned for loop over a range of numeric values is much safer and guarantees that you'll iterate in the expected order. (A for ... in loop does not guarantee order of iteration.) There's also .forEach() and the other Array.prototype methods for iteration.

* property names can also be Symbol instances in modern JavaScript, but that's clearly not the issue in this question.
shareimprove this answer
	
