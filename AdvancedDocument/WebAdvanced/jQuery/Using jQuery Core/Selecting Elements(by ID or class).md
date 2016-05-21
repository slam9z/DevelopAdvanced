[Selecting Elements](http://learn.jquery.com/using-jquery-core/selecting-elements/)

The most basic concept of jQuery is to "select some elements and do something with them." 
jQuery supports most CSS3 selectors, as well as some non-standard selectors. For a complete selector reference,
 visit the [Selectors documentation on api.jquery.com](http://api.jquery.com/category/selectors/).

###Selecting Elements by ID

```js
 $( "#myId" ); // Note IDs must be unique per page.
```
 

###Selecting Elements by Class Name

```js
 $( ".myClass" );
```
 

###Selecting Elements by Attribute

```js
 $( "input[name='first_name']" );
```


###Selecting Elements by Compound CSS Selector

```js
 $( "#contents ul.people li" );
```

###Selecting Elements with a Comma-separated List of Selectors

```js
 $( "div.myClass, ul.people" );
```
 

###Pseudo-Selectors

```js
 $( "a.external:first" );

$( "tr:odd" );

 

// Select all input-like elements in a form (more on this below).

$( "#myForm :input" );

$( "div:visible" );

 

// All except the first three divs.

$( "div:gt(2)" );

 

// All currently animated divs.

$( "div:animated" );
```


##Choosing Selectors

###Does My Selection Contain Any Elements?
 

This won't work. When a selection is made using $(), an object is always returned, 
and objects always evaluate to true. Even if the selection doesn't contain any elements, the code inside the if statement 
will still run.

The best way to determine if there are any elements is to test the selection's .length property,
 which tells you how many elements were selected. If the answer is 0, the .length property will
 evaluate to false when used as a boolean value:

```js
 // Testing whether a selection contains elements.

if ( $( "div.foo" ).length ) {

    ...

}
```

###Saving Selections

jQuery doesn't cache elements for you. If you've made a selection that you might need to make again,
 you should save the selection in a variable rather than making the selection repeatedly.

```js
 var divs = $( "div" );
```

###Refining & Filtering Selections

Sometimes the selection contains more than what you're after. jQuery offers several methods for refining 
and filtering selections.

```js
 // Refining selections.

$( "div.foo" ).has( "p" );         // div.foo elements that contain <p> tags

$( "h1" ).not( ".bar" );           // h1 elements that don't have a class of bar

$( "ul li" ).filter( ".current" ); // unordered list items with class of current

$( "ul li" ).first();              // just the first unordered list item

$( "ul li" ).eq( 5 );              // the sixth
```

###Selecting Form Elements

jQuery offers several pseudo-selectors that help find elements in forms. 
These are especially helpful because it can be difficult to distinguish between form elements based on their state or type using standard CSS selectors.

####link :checked

Not to be confused with :checkbox, :checked targets checked checkboxes, 
but keep in mind that this selector works also for checked radio buttons, 
and <select> elements (for <select> elements only, use the :selected selector):

```js
 $( "form :checked" );
```

 


 


 

 


 
