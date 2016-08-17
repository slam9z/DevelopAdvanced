[EventTarget](https://developer.mozilla.org/en-US/docs/Web/API/EventTarget)
    
##EventTarget


EventTarget is an interface implemented by objects that can receive events and may have listeners for them.

*Element*, *document*, and *window* are the most common event targets, but other objects can be event targets too,
 for example XMLHttpRequest, AudioNode, AudioContext, and others.

Many event targets (including elements, documents, and windows) also support setting event handlers via on... properties and attributes.

###Methods

* [EventTarget.addEventListener()](https://developer.mozilla.org/en-US/docs/Web/API/EventTarget/addEventListener)

    Register an event handler of a specific event type on the EventTarget.
    ```js
     target.addEventListener(type, listener[, options]);
    ```

    listener

    The object that receives a notification when an event of the specified type occurs. 
    This must be an object implementing the [EventListener](https://developer.mozilla.org/en-US/docs/Web/API/EventListener)
     interface, or simply a JavaScript function.
    

* EventTarget.removeEventListener()

    Removes an event listener from the EventTarget.

* EventTarget.dispatchEvent()

    Dispatch an event to this EventTarget.
