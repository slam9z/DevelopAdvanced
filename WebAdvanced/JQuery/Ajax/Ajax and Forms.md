[Ajax and Forms](http://learn.jquery.com/ajax/ajax-and-forms/)

Serialization

Serializing form inputs in jQuery is extremely easy. Two methods come supported natively
: .serialize() and .serializeArray(). While the names are fairly self-explanatory,
 there are many advantages to using them.

The .serialize() method serializes a form's data into a query string. 
For the element's value to be serialized, it must have a name attribute. 
Please note that values from inputs with a type of checkbox or radio are included only if they are checked.

```js
$( "#myForm" ).serialize();

 

// Creates a query string like this:

// field_1=something&field2=somethingElse
```

 

While plain old serialization is great, sometimes your application would work better 
if you sent over an array of objects, instead of just the query string. For that,
 jQuery has the .serializeArray() method. It's very similar to the .serialize() method listed above, 
except it produces an array of objects, instead of a string.


```js
 // Creating an array of objects containing form data

$( "#myForm" ).serializeArray();

 

// Creates a structure like this:

// [

//   {

//     name : "field_1",

//     value : "something"

//   },

//   {

//     name : "field_2",

//     value : "somethingElse"

//   }

// ]
```

##Client-side validation

```js
// Using validation to check for the presence of an input

$( "#form" ).submit(function( event ) {

 

    // If .required's value's length is zero

    if ( $( ".required" ).val().length === 0 ) {

 

        // Usually show some kind of error message here

 

        // Prevent the form from submitting

        event.preventDefault();

    } else {

 

        // Run $.ajax() here

    }

});
```

##Prefiltering

A prefilter is a way to modify the ajax options before each request is sent (hence, the name prefilter).

