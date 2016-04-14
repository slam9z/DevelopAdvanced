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

