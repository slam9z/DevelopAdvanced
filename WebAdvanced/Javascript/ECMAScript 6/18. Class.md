[Class](http://es6.ruanyifeng.com/#docs/class)

## 1.Class基本语法

###（1）概述

JavaScript语言的传统方法是通过构造函数，定义并生成新对象。下面是一个例子。

``` js
function Point(x,y){
  this.x = x;
  this.y = y;
}

Point.prototype.toString = function () {
  return '(' + this.x + ', ' + this.y + ')';
}
```

上面这种写法跟传统的面向对象语言（比如C++和Java）差异很大，很容易让新学习这门语言的程序员感到困惑。

ES6提供了更接近传统语言的写法，引入了Class（类）这个概念，作为对象的模板。通过class关键字，可以定义类。
基本上，ES6的class可以看作只是一个语法糖，它的绝大部分功能，ES5都可以做到，
新的class写法只是让对象原型的写法更加清晰、更像面向对象编程的语法而已。上面的代码用ES6的“类”改写，就是下面这样。

``` js
//定义类
class Point {

  constructor(x, y) {
    this.x = x;
    this.y = y;
  }

  toString() {
    return '(' + this.x + ', ' + this.y + ')';
  }

}
```

上面代码定义了一个“类”，可以看到里面有一个constructor方法，这就是构造方法，而this关键字则代表实例对象。
也就是说，ES5的构造函数Point，对应ES6的Point类的构造方法。


###（2）constructor方法

constructor方法是类的默认方法，通过new命令生成对象实例时，自动调用该方法。
一个类必须有constructor方法，如果没有显式定义，一个空的constructor方法会被默认添加。

``` js
constructor() {}
```

constructor方法默认返回实例对象（即this），完全可以指定返回另外一个对象。

``` js
class Foo {
  constructor() {
    return Object.create(null);
  }
}

new Foo() instanceof Foo
// false
```

上面代码中，constructor函数返回一个全新的对象，结果导致实例对象不是Foo类的实例。


###（3）实例对象

生成实例对象的写法，与ES5完全一样，也是使用new命令。如果忘记加上new，像函数那样调用Class，将会报错。

###（4）name属性

###（5）Class表达式


###（6）不存在变量提升

###（7）严格模式



##2.Class的继承

###基本用法

Class之间可以通过extends关键字实现继承，这比ES5的通过修改原型链实现继承，要清晰和方便很多。


###类的prototype属性和__proto__属性 

###Object.getPrototypeOf()

Object.getPrototypeOf方法可以用来从子类上获取父类。


###super关键字

上面讲过，在子类中，super关键字代表父类实例。


##3.原生构造函数的继承 

原生构造函数是指语言内置的构造函数，通常用来生成数据结构。ECMAScript的原生构造函数大致有下面这些。


##4.Class的取值函数（getter）和存值函数（setter）

与ES5一样，在Class内部可以使用get和set关键字，对某个属性设置存值函数和取值函数，拦截该属性的存取行为。


##5.Class的Generator方法 

如果某个方法之前加上星号（*），就表示该方法是一个Generator函数。



##6.Class的静态方法

类相当于实例的原型，所有在类中定义的方法，都会被实例继承。
如果在一个方法前，加上static关键字，就表示该方法不会被实例继承，而是直接通过类来调用，这就称为“静态方法”。

##7.Class的静态属性和实例属性

目前不支持

静态属性指的是Class本身的属性，即Class.propname，而不是定义在实例对象（this）上的属性。

``` JS
class Foo {
}

Foo.prop = 1;
Foo.prop // 1
```

上面的写法为Foo类定义了一个静态属性prop。

目前，只有这种写法可行，因为ES6明确规定，Class内部只有静态方法，没有静态属性。


##8.new.target属性 

new是从构造函数生成实例的命令。ES6为new命令引入了一个new.target属性，
（在构造函数中）返回new命令作用于的那个构造函数。如果构造函数不是通过new命令调用的，new.target会返回undefined，
因此这个属性可以用来确定构造函数是怎么调用的。


##9.Mixin模式的实现

Mixin模式指的是，将多个类的接口“混入”（mix in）另一个类。它在ES6的实现如下。

``` JS
function mix(...mixins) {
  class Mix {}

  for (let mixin of mixins) {
    copyProperties(Mix, mixin);
    copyProperties(Mix.prototype, mixin.prototype);
  }

  return Mix;
}

function copyProperties(target, source) {
  for (let key of Reflect.ownKeys(source)) {
    if ( key !== "constructor"
      && key !== "prototype"
      && key !== "name"
    ) {
      let desc = Object.getOwnPropertyDescriptor(source, key);
      Object.defineProperty(target, key, desc);
    }
  }
}


上面代码的mix函数，可以将多个对象合成为一个类。使用的时候，只要继承这个类即可。
class DistributedEdit extends mix(Loggable, Serializable) {
  // ...
}
```


