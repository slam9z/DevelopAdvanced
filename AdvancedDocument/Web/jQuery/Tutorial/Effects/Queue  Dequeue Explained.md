[Queue & Dequeue Explained](http://learn.jquery.com/effects/queue-and-dequeue-explained/)

Queues are the foundation for all animations in jQuery, they allow a series functions to be executed asynchronously 
on an element. Methods such as .slideUp(), .slideDown(), .fadeIn(), and .fadeOut() all use .animate(), 
which leverages queues to build up the series of steps that will transition one or more CSS values
 throughout the duration of the animation.

We can pass a callback function to the .animate() method, which will execute once the animation has completed.
