[Working with Selections](http://learn.jquery.com/using-jquery-core/working-with-selections/)


##Getters & Setters

Some jQuery methods can be used to either assign or read some value on a selection.
When the method is called with a value as an argument,
it's referred to as a setter because it sets (or assigns) that value. 
When the method is called with no argument, it gets (or reads) the value of the element. 
Setters affect all elements in a selection, whereas getters return the requested value only for the first element 
in the selection, with the exception of .text(), which retrieves the values of all the elements.

```js
 // The .html() method sets all the h1 elements' html to be "hello world":

$( "h1" ).html( "hello world" );

 // The .html() method returns the html of the first h1 element:

$( "h1" ).html();

// > "hello world"

```

 Setters return a jQuery object, allowing you to continue calling jQuery methods on your selection. 
Getters return whatever they were asked to get, so you can't continue to call jQuery methods on the 
value returned by the getter.

```js
 // Attempting to call a jQuery method after calling a getter.

// This will NOT work:

$( "h1" ).html().addClass( "test" );
```

##Chaining

If you call a method on a selection and that method returns a jQuery object,
 you can continue to call jQuery methods on the object without pausing for a semicolon. 
This practice is referred to as "chaining":

```js
 $( "#content" )

    .find( "h3" )

    .eq( 2 )

    .html( "new text for the third h3!" );
```

 


 
