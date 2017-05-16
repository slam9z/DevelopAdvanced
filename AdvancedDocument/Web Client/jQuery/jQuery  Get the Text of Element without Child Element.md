[jQuery: Get the Text of Element without Child Element](http://viralpatel.net/blogs/jquery-get-text-element-without-child-element/)


```js
jQuery.fn.justtext = function() {
  
	return $(this)	.clone()
			.children()
			.remove()
			.end()
			.text();

};
```