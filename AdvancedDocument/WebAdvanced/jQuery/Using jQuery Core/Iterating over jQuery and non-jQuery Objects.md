[Iterating over jQuery and non-jQuery Objects](http://learn.jquery.com/using-jquery-core/iterating/)

##$.each()

$.each() is a generic iterator function for looping over object, arrays, and array-like objects. 
Plain objects are iterated via their named properties while arrays and array-like objects are iterated via their indices.

$.each() is essentially a drop-in replacement of a traditional for or for-in loop


###Sometimes .each() Isn't Necessary


## .map()

There is a common iteration use case that can be better handled by using the .map() method.
 Anytime we want to create an array or concatenated string based on all matched elements in our jQuery selector, 
we're better served using .map().

For example instead of doing this:


```js
 var newArr = [];

 

$( "li" ).each( function() {

    newArr.push( this.id );

});


 $( "li" ).map( function(index, element) {

    return this.id;

}).get();

```
 

Notice the .get() chained at the end. .map() actually returns a jQuery-wrapped collection, 
even if we return strings out of the callback. We need to use the argument-less version of .get() 
in order to return a basic JavaScript array that we can work with. To concatenate into a string, 
we can chain the plain JS .join() array method after .get().

## $.map

Like $.each() and .each(), there is a $.map() as well as .map(). The difference is also very similar to both .each() methods. 

$.map() works on plain JavaScript arrays while .map() works on jQuery element collections.

 Because it's working on a plain array, $.map() returns a plain array and .get() does not need to be called – in fact, 

it will throw an error as it's not a native JavaScript method.

A word of warning: $.map() switches the order of callback arguments. This was done in order to match the native JavaScript 
.map() method made available in ECMAScript 5.


