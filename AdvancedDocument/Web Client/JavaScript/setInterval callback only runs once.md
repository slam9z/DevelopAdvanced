[setInterval callback only runs once](http://stackoverflow.com/questions/10182714/setinterval-callback-only-runs-once)

## answer

ou used a function call instead of a function reference as the first parameter of the setInterval. Do it like this:

```js
function timer() {
  console.log("timer!");
}

window.setInterval(timer, 1000);
```



Or shorter (but when the function gets bigger also less readable):

```js
window.setInterval( function() {
  console.log("timer!");
}, 1000)
```

## practice

实际试我的写法会有问题,理解反了吧！

```js
window.setInterval(function () {
                $('#success').text('');
            }, 3000);

```

应该使用

```js
window.setTimeout(function () {
    $('#success').text('');
}, 3000);
```