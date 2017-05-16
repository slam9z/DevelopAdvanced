[How do you check for an empty string in JavaScript?](http://stackoverflow.com/questions/154059/how-do-you-check-for-an-empty-string-in-javascript)

```js
if (!a) {
  // is emtpy
}
```
To ignore white space for strings:

```js
if (!a.trim()) {
    // is empty or whitespace
}
```
If you need legacy support (IE8-) for trim(), use `$.trim` or a `polyfill`.