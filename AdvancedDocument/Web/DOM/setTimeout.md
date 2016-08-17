[setTimeout](https://developer.mozilla.org/en-US/docs/Web/API/WindowTimers/setTimeout)

##setTimeout

Calls a function or executes a code snippet after a specified delay.

```js
var timeoutID = window.setTimeout(func, [delay, param1, param2, ...]);
var timeoutID = window.setTimeout(code, [delay]);
```

where

* timeoutID is the numerical ID of the timeout, which can be used later with window.
clearTimeout().
* func is the function you want to execute after delay milliseconds.
* code in the alternate syntax is a string of code you want to execute after delay
 milliseconds (using this syntax is not recommended for the same reasons as using eval())
* delay is the number of milliseconds (thousandths of a second) that the function
 call should be delayed by. If omitted, it defaults to 0. The actual delay may be longer; 
see Notes below.
* param1, param2, and so forth are additional parameters which are passed through 
to the function specified by func.


##window.clearTimeout(timeoutID)

* timeoutID is the ID of the timeout you wish to clear, 
as returned by WindowTimers.setTimeout().
