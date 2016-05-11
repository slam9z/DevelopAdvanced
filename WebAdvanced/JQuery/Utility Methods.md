[Utility Methods](http://learn.jquery.com/using-jquery-core/utility-methods/)

jQuery offers several utility methods in the $ namespace. These methods are helpful for accomplishing 
routine programming tasks.For a complete reference on jQuery utility methods, visit the 
[utilities documentation on api.jquery.com](http://api.jquery.com/category/utilities/).


##$.trim()

Removes leading and trailing whitespace:


##$.each()

Iterates over arrays and objects:


##$.inArray()

Returns a value's index in an array, or -1 if the value is not in the array:


##$.extend()

Changes the properties of the first object using the properties of subsequent objects:

```js
 var firstObject = { foo: "bar", a: "b" };

var secondObject = { foo: "baz" };

var newObject = $.extend( firstObject, secondObject );

console.log( firstObject.foo ); // "baz"

console.log( newObject.foo ); // "baz"
```


##$.proxy()

Returns a function that will always run in the provided scope — that is, sets the meaning of this
 inside the passed function to the second argument.

```js
 var myFunction = function() {

    console.log( this );
};

var myObject = {

    foo: "bar"
};
 

myFunction(); // window
 

var myProxyFunction = $.proxy( myFunction, myObject );


myProxyFunction(); // myObject
```

##Testing Type

Sometimes the *typeof* operator [can be confusing or inconsistent](https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Operators/typeof),
 so instead of using typeof, 
jQuery offers utility methods to help determine the type of a value.

First of all, you have methods to test if a specific value is of a specific type.

```js
 $.isArray([]); // true

$.isFunction(function() {}); // true

$.isNumeric(3.14); // true
```

 

Additionally, there is $.type() which checks for the internal class used to create a value.
 You can see the method as a better alternative for the typeof operator.

```js
 $.type( true ); // "boolean"

$.type( 3 ); // "number"

$.type( "test" ); // "string"
```

 



 
