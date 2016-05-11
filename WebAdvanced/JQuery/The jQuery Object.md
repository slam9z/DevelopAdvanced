[The jQuery Object](http://learn.jquery.com/using-jquery-core/jquery-object/)

When creating new elements (or selecting existing ones), jQuery returns the elements in a collection.
Many developers new to jQuery assume that this collection is an array. It has a zero-indexed sequence of DOM elements, 
some familiar array functions, and a .length property, after all. Actually, the jQuery object is more complicated than that.


##DOM and DOM Elements

##The jQuery Object

###Compatibility 
    The implementation of element methods varies across browser vendors and versions.

###Convenience 
     There are also a lot of common DOM manipulation use cases that are awkward to accomplish with pure DOM methods.


###Not All jQuery Objects are Created 


An important detail regarding this "wrapping" behavior is that each wrapped object is unique. 
This is true *even if the object was created with the same selector or contain references to the exact same DOM elements.*

```js
 // Creating two jQuery objects for the same element.
var logo1 = $( "#logo" );

var logo2 = $( "#logo" );
```

 
###jQuery Objects Are Not "Live"

Given a jQuery object with all the paragraph elements on the page:

```js
 // Selecting all <p> elements on the page.

var allParagraphs = $( "p" );
```
one might expect that the contents will grow and shrink over time as <p> elements are added and removed from the document. 
jQuery objects do not behave in this manner. The set of elements contained within a jQuery object will not change unless 
explicitly modified. This means that the collection is not "live" 


###Wrapping Up