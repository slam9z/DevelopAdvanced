[jQuery’s Ajax-Related Methods](http://learn.jquery.com/ajax/jquery-ajax-methods/)

While jQuery does offer many Ajax-related convenience methods, the core *$.ajax()* method is at the heart of all of them, 
and understanding it is imperative. We'll review it first, and then touch briefly on the convenience methods.

##$.ajax()

jQuery’s core $.ajax() method is a powerful and straightforward way of creating Ajax requests. 

```js
 // Using the core $.ajax() method

$.ajax({

 

    // The URL for the request

    url: "post.php",

 

    // The data to send (will be converted to a query string)

    data: {

        id: 123

    },

 

    // Whether this is a POST or GET request

    type: "GET",

 

    // The type of data we expect back

    dataType : "json",

})

  // Code to run if the request succeeds (is done);

  // The response is passed to the function

  .done(function( json ) {

     $( "<h1>" ).text( json.title ).appendTo( "body" );

     $( "<div class=\"content\">").html( json.html ).appendTo( "body" );

  })

  // Code to run if the request fails; the raw request and

  // status codes are passed to the function

  .fail(function( xhr, status, errorThrown ) {

    alert( "Sorry, there was a problem!" );

    console.log( "Error: " + errorThrown );

    console.log( "Status: " + status );

    console.dir( xhr );

  })

  // Code to run regardless of success or failure;

  .always(function( xhr, status ) {

    alert( "The request is complete!" );

  });
```

##$.ajax() Options

There are many, many options for the $.ajax() method, which is part of its power. For a complete list of options, 
visit http://api.jquery.com/jQuery.ajax/; here are several that you will use frequently:

* async

    Set to false if the request should be sent synchronously. Defaults to true. Note that if you set this option to false,
     your request will block execution of other code until the response is received.

* cache

    Whether to use a cached response if available. Defaults to true for all dataTypes except "script" and "jsonp". 
    When set to false, the URL will simply have a cachebusting parameter appended to it.


##Convenience Methods

If you don't need the extensive configurability of $.ajax(), 
and you don't care about handling errors, the Ajax convenience functions provided by jQuery can be useful, 
terse ways to accomplish Ajax requests. These methods are just "wrappers" around the core $.ajax() method, 
and simply pre-set some of the options on the $.ajax() method.

The convenience methods provided by jQuery are:

* $.get

    Perform a GET request to the provided URL.

* $.post

    Perform a POST request to the provided URL.

* $.getScript

    Add a script to the page.

* $.getJSON

    Perform a GET request, and expect JSON to be returned

 