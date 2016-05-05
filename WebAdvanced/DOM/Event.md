[Event](https://developer.mozilla.org/en-US/docs/Web/API/Event)

##interfaces

A lot of other interfaces implement the Event interface, 
either directly or by implementing  another interface which does so:

##Properties

This interface doesn't inherit any property.

* Event.bubbles  Read only 
    
    A boolean indicating whether the event bubbles up through the DOM or not.

* Event.cancelBubble 

    A nonstandard alternative to Event.stopPropagation().

* Event.cancelable Read only 

    A boolean indicating whether the event is cancelable.

* Event.currentTarget Read only 

    A reference to the currently registered target for the event.

* Event.defaultPrevented Read only 

    Indicates whether or not event.preventDefault() has been called on the event.

* Event.eventPhase Read only 

    Indicates which phase of the event flow is being processed.

* Event.explicitOriginalTarget  Read only 

    The explicit original target of the event (Mozilla-specific).

* Event.target Read only 

    A reference to the target to which the event was originally dispatched.

* Event.timeStamp Read only 

    The time that the event was created.

* Event.type Read only 

    The name of the event (case-insensitive).

* Event.isTrusted Read only 

    Indicates whether or not the event was initiated by
     the browser (after a user click for instance) or 
    by a script (using an event creation method, like event.initEvent)

##Methods

This interface doesn't inherit any method.

* Event.initEvent() 

    Initializes the value of an Event created. 
    If the event has already being dispatched, this method does nothing.

* Event.preventDefault()

    Cancels the event (if it is cancelable).

