[Traverse through Javascript object properties](http://stackoverflow.com/questions/4366104/traverse-through-javascript-object-properties)


## answer

prop will reference the property name, not its value.

```js
for (var prop in obj) {
    obj[prop] = 'xxx';
}
```