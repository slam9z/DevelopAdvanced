[jQuery.each()](http://api.jquery.com/jQuery.each/)

Description: A generic iterator function, which can be used to seamlessly iterate over both objects and arrays. Arrays and array-like objects with a length property (such as a function's arguments object) are iterated by numeric index, from 0 to length-1. Other objects are iterated via their named properties.

version added: 1.0
jQuery.each( array, callback ) 
array
Type: Array 
The array to iterate over.
callback
Type: Function( Integer indexInArray, Object value )
The function that will be executed on every object.
version added: 1.0
jQuery.each( object, callback ) 
object
Type: Object 
The object to iterate over.
callback
Type: Function( String propertyName, Object valueOfProperty )
The function that will be executed on every object.