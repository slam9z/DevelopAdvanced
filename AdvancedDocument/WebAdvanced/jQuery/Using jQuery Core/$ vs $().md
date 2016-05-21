[$ vs $()](http://learn.jquery.com/using-jquery-core/dollar-object-vs-function/)

This distinction can be incredibly confusing to new jQuery users. Here's what you need to remember:

* Methods called on jQuery selections are in the *$.fn* namespace, 
and automatically receive and return the selection as *this*.

* Methods in the *$* namespace are generally utility-type methods, and do not work with selections; 
they are not automatically passed any arguments, and their return value will vary.
