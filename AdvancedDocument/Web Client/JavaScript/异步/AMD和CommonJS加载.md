
```js
(function(factory) {

  if (typeof define === 'function' && define.amd) {

    // AMD. Register as anonymous module.

    define(['jquery'], factory);

  } else if (typeof exports === 'object') {

    // CommonJS

    module.exports = factory(require('jquery'));

  } else {

    // Browser globals.

    factory(jQuery);

  }

}
```