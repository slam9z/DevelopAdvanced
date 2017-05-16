[Data Methods](http://learn.jquery.com/using-jquery-core/data-methods/)

There's often data about an element you want to store with the element. In plain JavaScript,
 you might do this by adding a property to the DOM element, but you'd have to deal with memory leaks in some browsers. 
jQuery offers a straightforward way to store data related to an element, and it manages the memory issues for you.

```js
 // Storing and retrieving data related to an element.


$( "#myDiv" ).data( "keyName", { foo: "bar" } );


$( "#myDiv" ).data( "keyName" ); //
```

 
