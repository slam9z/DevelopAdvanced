[Use Stylesheets for Changing CSS on Many Elements](http://learn.jquery.com/performance/use-stylesheets-for-changing-css/)


Use Stylesheets for Changing CSS on Many Elements


If you're changing the CSS of more than 20 elements using .css(), consider adding a style tag to the page instead 
for a nearly 60% increase in speed.

```js
 // Fine for up to 20 elements, slow after that:

$( "a.swedberg" ).css( "color", "#0769ad" );

 

// Much faster:

$( "<style type=\"text/css\">a.swedberg { color: #0769ad }</style>")

    .appendTo( "head" );
```

 
