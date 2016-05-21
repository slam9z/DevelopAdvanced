[JavaScript Style Guide](https://github.com/airbnb/javascript)

## Types

上一次面试的时候，问js的数字类型，竟然说了一大堆，打击人。

1. Primitives

    * string
    * number
    * boolean
    * null
    * undefined

2. Complex
    
    * object
    * array
    * function

##References

1. Use  const  for all of your references; avoid using  var . 

2. If you must reassign references, use  let  instead of  var .

3. Note that both  let  and  const  are block-scoped.


##Objects

1. Use the literal syntax for object creation. 

2. If your code will be executed in browsers in script context, don't use [reserved words](http://es5.github.io/#x7.6.1) as keys

3. Use readable synonyms in place of reserved words. 

4. Use computed property names when creating objects with dynamic property names.

    > Why? They allow you to define all the properties of an object in one place.

    ``` js
    function getKey(k) {
      return `a key named ${k}`;
    }

    // bad
    const obj = {
      id: 5,
      name: 'San Francisco',
    };
    obj[getKey('enabled')] = true;

    // good
    const obj = {
      id: 5,
      name: 'San Francisco',
      [getKey('enabled')]: true,
    };
    ```
5. Use object method shorthand.

6. Use property value shorthand.

7. Group your shorthand properties at the beginning of your object declaration

8. Only quote properties that are invalid identifiers.

    > Why? In general we consider it subjectively easier to read. It improves syntax highlighting, 
    > and is also more easily optimized by many JS engines.

##Arrays

1. Use the literal syntax for array creation

2. Use Array#push instead of direct assignment to add items to an array.

3. Use array spreads  ...  to copy arrays.

4. To convert an array-like object to an array, use Array#from.

5. Use return statements in array method callbacks. 
    It's ok to omit the return if the function body consists of a single statement following 


##Destructuring

1. Use object destructuring when accessing and using multiple properties of an object.

2. Use array destructuring. 

3. Use object destructuring for multiple return values, not array destructuring.


##Strings

1. Use single quotes  ''  for strings. 

2. Strings that cause the line to go over 100 characters should be written across multiple 
    lines using string concatenation.

3. Note: If overused, long strings with concatenation could impact performance.

4. When programmatically building up strings, use template strings instead of concatenation. 

5. Never use  eval()  on a string, it opens too many vulnerabilities.

6. Do not unnecessarily escape characters in strings. 

##Functions

1. Use function declarations instead of function expressions. 

2. Wrap immediately invoked function expressions in parentheses. 

3.  Never declare a function in a non-function block (if, while, etc). 
    Assign the function to a variable instead. Browsers will allow you to do it, 
    but they all interpret it differently, which is bad news bears. 

4. ECMA-262 defines a  block  as a list of statements. A function declaration is not a statement. 

5. Never name a parameter  arguments . This will take precedence over the  arguments  object that is given to every function scope.

6. Never use  arguments , opt to use rest syntax  ...  instead. 

7. Use default parameter syntax rather than mutating function arguments.

8. Avoid side effects with default parameters.

9. Always put default parameters last.(这些规范在C#中必需如此)

10. Never use the Function constructor to create a new function.

11. Spacing in a function signature.

12. Never mutate parameters. eslint:  no-param-reassign 


    > Why? Manipulating objects passed in as parameters can cause unwanted variable side effects in the original caller.

    ``` js
    // bad
    function f1(obj) {
      obj.key = 1;
    };

    // good
    function f2(obj) {
      const key = Object.prototype.hasOwnProperty.call(obj, 'key') ? obj.key : 1;
    };
    ```

13. Never reassign parameters.


    > Why? Reassigning parameters can lead to unexpected behavior, especially when accessing the  arguments  object. It can also cause optimization issues, especially in V8.

    ``` js
    // bad
    function f1(a) {
      a = 1;
    }

    function f2(a) {
      if (!a) { a = 1; }
    }

    // good
    function f3(a) {
      const b = a || 1;
    }

    function f4(a = 1) {
    }

    ```






##Arrow Functions

1. When you must use function expressions (as when passing an anonymous function), use arrow function notation. 

    ``` js
    // bad
    [1, 2, 3].map(function (x) {
      const y = x + 1;
      return x * y;
    });

    // good
    [1, 2, 3].map((x) => {
      const y = x + 1;
      return x * y;
    });
    ```
2. If the function body consists of a single expression, omit the braces and use the implicit return.
     Otherwise, keep the braces and use a  return  statement.

3. In case the expression spans over multiple lines, wrap it in parentheses for better readability.
If your function takes a single argument and doesn’t use braces, omit the parentheses. 
Otherwise, always include parentheses around arguments.


    > Why? Less visual clutter.

    ``` js
    // bad
    [1, 2, 3].map((x) => x * x);

    // good
    [1, 2, 3].map(x => x * x);

    // good
    [1, 2, 3].map(number => (
      `A long string with the ${number}. It’s so long that we’ve broken it ` +
      'over multiple lines!'
    ));

    // bad
    [1, 2, 3].map(x => {
      const y = x + 1;
      return x * y;
    });

    // good
    [1, 2, 3].map((x) => {
      const y = x + 1;
      return x * y;
    });
    ```

4.  If your function takes a single argument and doesn’t use braces, omit the parentheses. Otherwise, 
always include parentheses around arguments.

5. Avoid confusing arrow function syntax ( => ) with comparison operators ( <= ,  >= ).


##Classes & Constructors

1. Always use  class . Avoid manipulating  prototype  directly

2. Use  extends  for inheritance.

3. Methods can return  this  to help with method chaining.

4. It's okay to write a custom toString() method, just make sure it works successfully 
and causes no side effects.

5. Classes have a default constructor if one is not specified. 
An empty constructor function or one that just delegates to a parent class is unnecessary.

6. Avoid duplicate class members.    

##Modules

1. Always use modules ( import / export ) over a non-standard module system. 
You can always transpile to your preferred module system.

2. Do not use wildcard imports.

3. And do not export directly from an import.

4. Only import from a path in one place.

##Iterators and Generators


1. Don't use iterators. Prefer JavaScript's higher-order functions like  map()  and  reduce()  
instead of loops like  for-of . eslint:  no-iterator 


    Why? This enforces our immutable rule. Dealing with pure functions that return values is easier to reason about than side effects.

    ``` JS
    const numbers = [1, 2, 3, 4, 5];

    // bad
    let sum = 0;
    for (let num of numbers) {
      sum += num;
    }

    sum === 15;

    // good
    let sum = 0;
    numbers.forEach(num => sum += num);
    sum === 15;

    // best (use the functional force)
    const sum = numbers.reduce((total, num) => total + num, 0);
    sum === 15;

    ```


2.  Don't use generators for now.


    Why? They don't transpile well to ES5.


##Properties



1. Use dot notation when accessing properties. 

``` JS
const luke = {
  jedi: true,
  age: 28,
};

// bad
const isJedi = luke['jedi'];

// good
const isJedi = luke.jedi;
```



2. Use bracket notation  []  when accessing properties with a variable.

``` JS
const luke = {
  jedi: true,
  age: 28,
};

function getProp(prop) {
  return luke[prop];
}

const isJedi = getProp('jedi');
```


##Variables

1. Always use  const  to declare variables. Not doing so will result in global variables.
 We want to avoid polluting the global namespace. Captain Planet warned us of that.

2. Use one  const  declaration per variable.

3.  Group all your  const s and then group all your  let s.

4.  Assign variables where you need them, but place them in a reasonable place.


##Hoisting

1.  var  declarations get hoisted to the top of their scope, their assignment does not.  
const  and  let  declarations are blessed with a new concept called [Temporal Dead Zones (TDZ)](https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Statements/let#Temporal_dead_zone_and_errors_with_let).
 It's important to know why [typeof is no longer safe](http://es-discourse.com/t/why-typeof-is-no-longer-safe/15).

``` js
// we know this wouldn't work (assuming there
// is no notDefined global variable)
function example() {
  console.log(notDefined); // => throws a ReferenceError
}

// creating a variable declaration after you
// reference the variable will work due to
// variable hoisting. Note: the assignment
// value of `true` is not hoisted.
function example() {
  console.log(declaredButNotAssigned); // => undefined
  var declaredButNotAssigned = true;
}

// the interpreter is hoisting the variable
// declaration to the top of the scope,
// which means our example could be rewritten as:
function example() {
  let declaredButNotAssigned;
  console.log(declaredButNotAssigned); // => undefined
  declaredButNotAssigned = true;
}

// using const and let
function example() {
  console.log(declaredButNotAssigned); // => throws a ReferenceError
  console.log(typeof declaredButNotAssigned); // => throws a ReferenceError
  const declaredButNotAssigned = true;
}
```

2. Anonymous function expressions hoist their variable name, but not the function assignment.

3. Named function expressions hoist the variable name, not the function name or the function body.

4. Function declarations hoist their name and the function body.


##Comparison Operators & Equality


1. Use  ===  and  !==  over  ==  and  != . eslint:  eqeqeq 


2.  Conditional statements such as the  if  statement evaluate their expression using coercion
 with the  ToBoolean  abstract method and always follow these simple rules:

* Objects evaluate to true
* Undefined evaluates to false
* Null evaluates to false
* Booleans evaluate to the value of the boolean
* Numbers evaluate to false if +0, -0, or NaN, otherwise true
* Strings evaluate to false if an empty string  '' , otherwise true


3. Use shortcuts.


4. For more information see [Truth Equality and JavaScript](http://javascriptweblog.wordpress.com/2011/02/07/truth-equality-and-javascript/#more-2108) 
by Angus Croll.


5. Use braces to create blocks in  case  and  default  clauses that contain lexical declarations
 (e.g.  let ,  const ,  function , and  class ).

    >Why? Lexical declarations are visible in the entire  switch  block but only get initialized when assigned,
    which only happens when its  case  is reached. This causes problems when multiple  case  clauses attempt to define the same thing.


6. Ternaries should not be nested and generally be single line expressions.

    ``` JS

    // bad
    const foo = maybe1 > maybe2
      ? "bar"
      : value1 > value2 ? "baz" : null;

    // better
    const maybeNull = value1 > value2 ? 'baz' : null;

    const foo = maybe1 > maybe2
      ? 'bar'
      : maybeNull;

    // best
    const maybeNull = value1 > value2 ? 'baz' : null;

    const foo = maybe1 > maybe2 ? 'bar' : maybeNull;

    ```

7. Avoid unneeded ternary statements.

    ``` js

    // bad
    const foo = a ? a : b;
    const bar = c ? true : false;
    const baz = c ? false : true;

    // good
    const foo = a || b;
    const bar = !!c;
    const baz = !c;

    ```

##Blocks

这些我还是按照C#的写法，比较清楚。

1. Use braces with all multi-line blocks.

2. If you're using multi-line blocks with  if  and  else , put  else  on the same line as your  if  block's closing brace. 

##Comments

1. Use  /** ... */  for multi-line comments. Include a description, specify types and values for all parameters and return values.

    ``` js
    // bad
    // make() returns a new element
    // based on the passed in tag name
    //
    // @param {String} tag
    // @return {Element} element
    function make(tag) {

      // ...stuff...

      return element;
    }

    // good
    /**
     * make() returns a new element
     * based on the passed in tag name
     *
     * @param {String} tag
     * @return {Element} element
     */
    function make(tag) {

      // ...stuff...

      return element;
    }
    ```

2. Use  //  for single line comments. Place single line comments on a newline above the subject of the comment. 
Put an empty line before the comment unless it's on the first line of a block.


3. Prefixing your comments with  FIXME  or  TODO  helps other developers quickly understand 
if you're pointing out a problem that needs to be revisited, or if you're suggesting a solution to the problem that needs to be implemented.
 These are different than regular comments because they are actionable. 
The actions are  FIXME: -- need to figure this out  or  TODO: -- need to implement .

4. Use  // FIXME:  to annotate problems.


5. Use  // TODO:  to annotate solutions to problems.

##Whitespace

1. Use soft tabs set to 2 spaces. (like 4 spaces)

2. Place 1 space before the leading brace.

    ``` js
    // bad
    function test(){
      console.log('test');
    }

    // good
    function test() {
      console.log('test');
    }

    // bad
    dog.set('attr',{
      age: '1 year',
      breed: 'Bernese Mountain Dog',
    });

    // good
    dog.set('attr', {
      age: '1 year',
      breed: 'Bernese Mountain Dog',
    });
    ```

3.  Place 1 space before the opening parenthesis in control statements ( if ,  while  etc.). 
Place no space between the argument list and the function name in function calls and declarations.

4. Set off operators with spaces. 

    ``` js
    // bad
    const x=y+5;

    // good
    const x = y + 5;
    ```
5. End files with a single newline character.

6. Use indentation when making long method chains (more than 2 method chains). 
Use a leading dot, which emphasizes that the line is a method call, not a new statement. 

    ``` js
    // bad
    $('#items').find('.selected').highlight().end().find('.open').updateCount();

    // bad
    $('#items').
      find('.selected').
        highlight().
        end().
      find('.open').
        updateCount();

    // good
    $('#items')
      .find('.selected')
        .highlight()
        .end()
      .find('.open')
        .updateCount();

    // bad
    const leds = stage.selectAll('.led').data(data).enter().append('svg:svg').classed('led', true)
        .attr('width', (radius + margin) * 2).append('svg:g')
        .attr('transform', 'translate(' + (radius + margin) + ',' + (radius + margin) + ')')
        .call(tron.led);

    // good
    const leds = stage.selectAll('.led')
        .data(data)
      .enter().append('svg:svg')
        .classed('led', true)
        .attr('width', (radius + margin) * 2)
      .append('svg:g')
        .attr('transform', 'translate(' + (radius + margin) + ',' + (radius + margin) + ')')
        .call(tron.led);

    // good
    const leds = stage.selectAll('.led').data(data);
    ```

7. Leave a blank line after blocks and before the next statement

8. Do not pad your blocks with blank lines.

9. Do not add spaces inside parentheses

10. Do not add spaces inside brackets. 

11. Add spaces inside curly braces. eslint:  object-curly-spacing  jscs:  disallowSpacesInsideObjectBrackets 

    ``` js
    // bad
    const foo = {clark: 'kent'};

    // good
    const foo = { clark: 'kent' };
    ```
12. Avoid having lines of code that are longer than 100 characters (including whitespace). 

##Commas

1. Leading commas: Nope. 

    ``` js
    // bad
    const story = [
        once
      , upon
      , aTime
    ];

    // good
    const story = [
      once,
      upon,
      aTime,
    ];
    ```
    
2.  Additional trailing comma: Yup. eslint:  comma-dangle  jscs:  requireTrailingComma 

    >Why? This leads to cleaner git diffs. Also, transpilers like Babel will remove 
    the additional trailing comma in the transpiled code which means you don't have 
    to worry about the trailing comma problem in legacy browsers.

    ``` js
    // bad - git diff without trailing comma
    const hero = {
         firstName: 'Florence',
    -    lastName: 'Nightingale'
    +    lastName: 'Nightingale',
    +    inventorOf: ['coxcomb graph', 'modern nursing']
    };

    // good - git diff with trailing comma
    const hero = {
         firstName: 'Florence',
         lastName: 'Nightingale',
    +    inventorOf: ['coxcomb chart', 'modern nursing'],
    };

    // bad
    const hero = {
      firstName: 'Dana',
      lastName: 'Scully'
    };

    const heroes = [
      'Batman',
      'Superman'
    ];

    // good
    const hero = {
      firstName: 'Dana',
      lastName: 'Scully',
    };

    const heroes = [
      'Batman',
      'Superman',
    ];
    ```

##Semicolons

1. Yup. eslint:  semi  jscs:  requireSemicolons 

    ``` js
    // bad
    (function () {
      const name = 'Skywalker'
      return name
    })()

    // good
    (() => {
      const name = 'Skywalker';
      return name;
    }());

    // good (guards against the function becoming an argument when two files with IIFEs are concatenated)
    ;(() => {
      const name = 'Skywalker';
      return name;
    }());
    ```


##Type Casting & Coercion

1. Perform type coercion at the beginning of the statement.

2. Strings:

    ``` js
    // => this.reviewScore = 9;

    // bad
    const totalScore = this.reviewScore + ''; // invokes this.reviewScore.valueOf()

    // bad
    const totalScore = this.reviewScore.toString(); // isn't guaranteed to return a string

    // good
    const totalScore = String(this.reviewScore);
    ```

3. Numbers: Use  Number  for type casting and  parseInt  always with a radix for parsing strings. 

    ``` js
    const inputValue = '4';

    // bad
    const val = new Number(inputValue);

    // bad
    const val = +inputValue;

    // bad
    const val = inputValue >> 0;

    // bad
    const val = parseInt(inputValue);

    // good
    const val = Number(inputValue);

    // good
    const val = parseInt(inputValue, 10);
    ```
4. Booleans:

    ```
    const age = 0;

    // bad
    const hasAge = new Boolean(age);

    // good
    const hasAge = Boolean(age);

    // good
    const hasAge = !!age;
    ```


##Naming Conventions

1. Avoid single letter names. Be descriptive with your naming.

2. Use camelCase when naming objects, functions, and instances. 

3. Use PascalCase only when naming constructors or classes. 

4. Do not use trailing or leading underscores

5. Don't save references to  this . Use arrow functions or Function

6. If your file exports a single class, your filename should be exactly the name of the class

7. Use camelCase when you export-default a function. Your filename should be identical to your function's name.

8. Use PascalCase when you export a singleton / function library / bare object.


##Accessors

1. Accessor functions for properties are not required.

2. Do not use JavaScript getters/setters as they cause unexpected side effects and are harder to test, maintain, and reason about. 
Instead, if you do make accessor functions, use getVal() and setVal('hello').

3. If the property/method is a  boolean , use  isVal()  or  hasVal() .

4. It's okay to create get() and set() functions, but be consistent.


##Events

1. When attaching data payloads to events (whether DOM events or something more proprietary like Backbone events), pass a hash instead of a raw value. This allows a subsequent contributor to add more data to the event payload without finding and updating every handler for the event. For example, instead of:

    ``` js
    // bad
    $(this).trigger('listingUpdated', listing.id);

    ...

    $(this).on('listingUpdated', (e, listingId) => {
      // do something with listingId
    });

    prefer:

    // good
    $(this).trigger('listingUpdated', { listingId: listing.id });

    ...

    $(this).on('listingUpdated', (e, data) => {
      // do something with data.listingId
    });
    ```

##jQuery

1. Prefix jQuery object variables with a  $ . jscs:  requireDollarBeforejQueryAssignment 

    ``` js
    // bad
    const sidebar = $('.sidebar');

    // good
    const $sidebar = $('.sidebar');

    // good
    const $sidebarBtn = $('.sidebar-btn');
    ```

2. Cache jQuery lookups.

    ``` js
    // bad
    function setSidebar() {
      $('.sidebar').hide();

      // ...stuff...

      $('.sidebar').css({
        'background-color': 'pink'
      });
    }

    // good
    function setSidebar() {
      const $sidebar = $('.sidebar');
      $sidebar.hide();

      // ...stuff...

      $sidebar.css({
        'background-color': 'pink'
      });
    }
    ```

3.  For DOM queries use Cascading  $('.sidebar ul')  or parent > child  $('.sidebar > ul') 

4. Use  find  with scoped jQuery object queries.

    ``` js
    // bad
    $('ul', '.sidebar').hide();

    // bad
    $('.sidebar').find('ul').hide();

    // good
    $('.sidebar ul').hide();

    // good
    $('.sidebar > ul').hide();

    // good
    $sidebar.find('ul').hide();
    ```


##ECMAScript 5 Compatibility

1. Refer to Kangax's ES5 [compatibility table](http://kangax.github.io/es5-compat-table/).


##ECMAScript 6 Styles


##Testing

1. Yup.

    ``` js
    function foo() {
      return true;
    }
    ```

2.  No, but seriously: 

    * Whichever testing framework you use, you should be writing tests!
    * Strive to write many small pure functions, and minimize where mutations occur.
    * Be cautious about stubs and mocks - they can make your tests more brittle.
    * We primarily use  mocha  at Airbnb.  tape  is also used occasionally for small, separate modules.
    * 100% test coverage is a good goal to strive for, even if it's not always practical to reach it.
    * Whenever you fix a bug, write a regression test. A bug fixed without a regression test is almost certainly going to break again
     in the future.


## Performance

  - [On Layout & Web Performance](http://www.kellegous.com/j/2013/01/26/layout-performance/)
  - [String vs Array Concat](http://jsperf.com/string-vs-array-concat/2)
  - [Try/Catch Cost In a Loop](http://jsperf.com/try-catch-in-loop-cost)
  - [Bang Function](http://jsperf.com/bang-function)
  - [jQuery Find vs Context, Selector](http://jsperf.com/jquery-find-vs-context-sel/13)
  - [innerHTML vs textContent for script text](http://jsperf.com/innerhtml-vs-textcontent-for-script-text)
  - [Long String Concatenation](http://jsperf.com/ya-string-concat)
  - Loading...


## Resources

**Learning ES6**

  - [Draft ECMA 2015 (ES6) Spec](https://people.mozilla.org/~jorendorff/es6-draft.html)
  - [ExploringJS](http://exploringjs.com/)
  - [ES6 Compatibility Table](https://kangax.github.io/compat-table/es6/)
  - [Comprehensive Overview of ES6 Features](http://es6-features.org/)

**Read This**

  - [Standard ECMA-262](http://www.ecma-international.org/ecma-262/6.0/index.html)

**Tools**

  - Code Style Linters
    + [ESlint](http://eslint.org/) - [Airbnb Style .eslintrc](https://github.com/airbnb/javascript/blob/master/linters/.eslintrc)
    + [JSHint](http://jshint.com/) - [Airbnb Style .jshintrc](https://github.com/airbnb/javascript/blob/master/linters/.jshintrc)
    + [JSCS](https://github.com/jscs-dev/node-jscs) - [Airbnb Style Preset](https://github.com/jscs-dev/node-jscs/blob/master/presets/airbnb.json)

**Other Style Guides**

  - [Google JavaScript Style Guide](http://google-styleguide.googlecode.com/svn/trunk/javascriptguide.xml)
  - [jQuery Core Style Guidelines](http://contribute.jquery.org/style-guide/js/)
  - [Principles of Writing Consistent, Idiomatic JavaScript](https://github.com/rwaldron/idiomatic.js)

**Other Styles**

  - [Naming this in nested functions](https://gist.github.com/cjohansen/4135065) - Christian Johansen
  - [Conditional Callbacks](https://github.com/airbnb/javascript/issues/52) - Ross Allen
  - [Popular JavaScript Coding Conventions on Github](http://sideeffect.kr/popularconvention/#javascript) - JeongHoon Byun
  - [Multiple var statements in JavaScript, not superfluous](http://benalman.com/news/2012/05/multiple-var-statements-javascript/) - Ben Alman

**Further Reading**

  - [Understanding JavaScript Closures](http://javascriptweblog.wordpress.com/2010/10/25/understanding-javascript-closures/) - Angus Croll
  - [Basic JavaScript for the impatient programmer](http://www.2ality.com/2013/06/basic-javascript.html) - Dr. Axel Rauschmayer
  - [You Might Not Need jQuery](http://youmightnotneedjquery.com/) - Zack Bloom & Adam Schwartz
  - [ES6 Features](https://github.com/lukehoban/es6features) - Luke Hoban
  - [Frontend Guidelines](https://github.com/bendc/frontend-guidelines) - Benjamin De Cock

**Books**

  - [JavaScript: The Good Parts](http://www.amazon.com/JavaScript-Good-Parts-Douglas-Crockford/dp/0596517742) - Douglas Crockford
  - [JavaScript Patterns](http://www.amazon.com/JavaScript-Patterns-Stoyan-Stefanov/dp/0596806752) - Stoyan Stefanov
  - [Pro JavaScript Design Patterns](http://www.amazon.com/JavaScript-Design-Patterns-Recipes-Problem-Solution/dp/159059908X)  - Ross Harmes and Dustin Diaz
  - [High Performance Web Sites: Essential Knowledge for Front-End Engineers](http://www.amazon.com/High-Performance-Web-Sites-Essential/dp/0596529309) - Steve Souders
  - [Maintainable JavaScript](http://www.amazon.com/Maintainable-JavaScript-Nicholas-C-Zakas/dp/1449327680) - Nicholas C. Zakas
  - [JavaScript Web Applications](http://www.amazon.com/JavaScript-Web-Applications-Alex-MacCaw/dp/144930351X) - Alex MacCaw
  - [Pro JavaScript Techniques](http://www.amazon.com/Pro-JavaScript-Techniques-John-Resig/dp/1590597273) - John Resig
  - [Smashing Node.js: JavaScript Everywhere](http://www.amazon.com/Smashing-Node-js-JavaScript-Everywhere-Magazine/dp/1119962595) - Guillermo Rauch
  - [Secrets of the JavaScript Ninja](http://www.amazon.com/Secrets-JavaScript-Ninja-John-Resig/dp/193398869X) - John Resig and Bear Bibeault
  - [Human JavaScript](http://humanjavascript.com/) - Henrik Joreteg
  - [Superhero.js](http://superherojs.com/) - Kim Joar Bekkelund, Mads Mobæk, & Olav Bjorkoy
  - [JSBooks](http://jsbooks.revolunet.com/) - Julien Bouquillon
  - [Third Party JavaScript](https://www.manning.com/books/third-party-javascript) - Ben Vinegar and Anton Kovalyov
  - [Effective JavaScript: 68 Specific Ways to Harness the Power of JavaScript](http://amzn.com/0321812182) - David Herman
  - [Eloquent JavaScript](http://eloquentjavascript.net/) - Marijn Haverbeke
  - [You Don't Know JS: ES6 & Beyond](http://shop.oreilly.com/product/0636920033769.do) - Kyle Simpson

**Blogs**

  - [DailyJS](http://dailyjs.com/)
  - [JavaScript Weekly](http://javascriptweekly.com/)
  - [JavaScript, JavaScript...](http://javascriptweblog.wordpress.com/)
  - [Bocoup Weblog](https://bocoup.com/weblog)
  - [Adequately Good](http://www.adequatelygood.com/)
  - [NCZOnline](https://www.nczonline.net/)
  - [Perfection Kills](http://perfectionkills.com/)
  - [Ben Alman](http://benalman.com/)
  - [Dmitry Baranovskiy](http://dmitry.baranovskiy.com/)
  - [Dustin Diaz](http://dustindiaz.com/)
  - [nettuts](http://code.tutsplus.com/?s=javascript)

**Podcasts**

  - [JavaScript Jabber](https://devchat.tv/js-jabber/)