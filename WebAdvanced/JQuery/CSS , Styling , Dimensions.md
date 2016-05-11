[CSS, Styling, & Dimensions](http://learn.jquery.com/using-jquery-core/css-styling-dimensions/)

jQuery includes a handy way to get and set CSS properties of elements:

*.css*

##Using CSS Classes for Styling

```js
var h1 = $( "h1" );

h1.addClass( "big" );

h1.removeClass( "big" );

h1.toggleClass( "big" );

if ( h1.hasClass( "big" ) ) {
    ...
}
```


##Dimensions

```js
// Basic dimensions methods.

 

// Sets the width of all <h1> elements.

$( "h1" ).width( "50px" );
 

// Gets the width of the first <h1> element.

$( "h1" ).width();


// Sets the height of all <h1> elements.

$( "h1" ).height( "50px" );


// Gets the height of the first <h1> element.

$( "h1" ).height();


// Returns an object containing position information for

// the first <h1> relative to its "offset (positioned) parent".

$( "h1" ).position();


```

