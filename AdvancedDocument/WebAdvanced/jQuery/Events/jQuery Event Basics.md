[jQuery Event Basics](http://learn.jquery.com/events/event-basics/)


##Setting Up Event Responses on DOM Elements

jQuery makes it straightforward to set up event-driven responses on page elements. 
These events are often triggered by the end user's interaction with the page, 
such as when text is entered into a form element or the mouse pointer is moved. 
In some cases, such as the page load and unload events, the browser itself will trigger the event.

jQuery offers convenience methods for most native browser events. 
These methods — including .click(), .focus(), .blur(), .change(), etc. — are shorthand for jQuery's .on() method.
 The on method is useful for binding the same handler function to multiple events,
 when you want to provide data to the event handler, when you are working with custom events, 
or when you want to pass an object of multiple events and handlers.


##Extending Events to New Page Elements

It is important to note that .on() can only create event listeners on elements that exist at the 
time you set up the listeners. 


##Inside the Event Handler Function

Every event handling function receives an event object, which contains many properties and methods.
 The event object is most commonly used to prevent the default action of the event via the *.preventDefault()* method. 
However, the event object contains a number of other useful properties and methods, including:


* pageX, pageY

    The mouse position at the time the event occurred, 
    relative to the top left corner of the page display area (not the entire browser window).

* type

    The type of the event (e.g., "click").

* which

    The button or key that was pressed.

* data

    Any data that was passed in when the event was bound. For example:


    ```js
     // Event setup using the `.on()` method with data

    $( "input" ).on(

        "change",

        { foo: "bar" }, // Associate data with event binding

        function( eventObject ) {

            console.log("An input value has changed! ", eventObject.data.foo);

        }

    );
    ```

 
* target

    The DOM element that initiated the event.

* namespace

    The namespace specified when the event was triggered.

* timeStamp

    The difference in milliseconds between the time the event occurred in the browser and January 1, 1970.

* preventDefault()

    Prevent the default action of the event (e.g. following a link).

* stopPropagation()

    Stop the event from bubbling up to other elements.

    In addition to the event object, the event handling function also 
    has access to the DOM element that the handler was bound to via the keyword this. 
    To turn the DOM element into a jQuery object that we can use jQuery methods on,
     we simply do $( this ), often following this idiom:

    ```

    $( "a" ).click(function( eventObject ) {

        var elem = $( this );

        if ( elem.attr( "href" ).match( /evil/ ) ) {

            eventObject.preventDefault();

            elem.addClass( "evil" );

        }

    });
    ```

 
##Tearing Down Event Listeners

To remove an event listener, you use the .off() method and pass in the event type to off.


##Setting Up Events to Run Only Once

Sometimes you need a particular handler to run only once — after that, you may want no handler to run, 
or you may want a different handler to run. jQuery provides the .one() method for this purpose.



