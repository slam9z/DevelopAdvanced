[JavaScript function in href vs. onclick](http://stackoverflow.com/questions/1070760/javascript-function-in-href-vs-onclick/11348403)


##answer

Putting the onclick within the href would offend those who believe strongly in separation of 
content from behavior/action. The argument is that your html content should remain focused
 solely on content, not on presentation or behavior.

The typical path these days is to use a javascript library (eg. jquery) and create an event 
handler using that library. It would look something like:

```js
$('a').click( function(e) {e.preventDefault(); /*your_code_here;*/ return false; } );
```

##answer

bad: 

```html
<a id="myLink" href="javascript:MyFunction();">link text</a>
```

good: 

```html
<a id="myLink" href="#" onclick="MyFunction();">link text</a>
```

better: 

```html
<a id="myLink" href="#" onclick="MyFunction();return false;">link text</a>
```

even better 1: 

```html
<a id="myLink" title="Click to do something"
 href="#" onclick="MyFunction();return false;">link text</a>
```
even better 2: 

```html
<a id="myLink" title="Click to do something"
 href="PleaseEnableJavascript.html" onclick="MyFunction();return false;">link text</a>
``` 

Why better? because return false will prevent browser from following the link
best: 
Use jQuery or other similar framework to attach onclick handler by element's ID.

```js
$('#myLink').click(function(){ MyFunction(); return false; });
```