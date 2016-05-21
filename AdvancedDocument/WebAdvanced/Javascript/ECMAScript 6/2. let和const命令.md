[let和const命令](http://es6.ruanyifeng.com/#docs/let)

##1.let命令

###基本用法

ES6新增了let命令，用来声明变量。它的用法类似于var，但是所声明的变量，只在let命令所在的代码块内有效。

###不存在变量提升

let不像var那样会发生“变量提升”现象。所以，变量一定要在声明后使用，否则报错。

###暂时性死区

只要块级作用域内存在let命令，它所声明的变量就“绑定”（binding）这个区域，不再受外部的影响。


###不允许重复声明

let不允许在相同作用域内，重复声明同一个变量。


##2.块级作用域 

###为什么需要块级作用域？


* 第一种场景，内层变量可能会覆盖外层变量。

* 第二种场景，用来计数的循环变量泄露为全局变量。


###ES6的块级作用域

ES6也规定，函数本身的作用域，在其所在的块级作用域之内。


##3.const命令

const也用来声明变量，但是声明的是常量。一旦声明，常量的值就不能改变。

如果真的想将对象冻结，应该使用Object.freeze方法。

###跨模块常量

上面说过，const声明的常量只在当前代码块有效。如果想设置跨模块的常量，可以采用下面的写法。

``` javascript
// constants.js 模块
export const A = 1;
export const B = 3;
export const C = 4;

// test1.js 模块
import * as constants from './constants';
console.log(constants.A); // 1
console.log(constants.B); // 3

// test2.js 模块
import {A, B} from './constants';
console.log(A); // 1
console.log(B); // 3
```


