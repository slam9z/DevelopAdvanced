[Generator 函数](http://es6.ruanyifeng.com/#docs/generator)

##1.简介

###基本概念

Generator函数是ES6提供的一种异步编程解决方案，语法行为与传统函数完全不同。
本章详细介绍Generator函数的语法和API，它的异步编程应用请看《异步操作》一章。

Generator函数有多种理解角度。

从语法上，首先可以把它理解成，Generator函数是一个状态机，封装了多个内部状态。
执行Generator函数会返回一个遍历器对象，也就是说，Generator函数除了状态机，还是一个遍历器对象生成函数。
返回的遍历器对象，可以依次遍历Generator函数内部的每一个状态。

形式上，Generator函数是一个普通函数，但是有两个特征。一是，function关键字与函数名之间有一个星号；
二是，函数体内部使用yield语句，定义不同的内部状态（yield语句在英语里的意思就是“产出”）。


###yield语句

由于Generator函数返回的遍历器对象，只有调用next方法才会遍历下一个内部状态，所以其实提供了一种可以暂停执行的函数。yield语句就是暂停标志。

遍历器对象的next方法的运行逻辑如下。

（1）遇到yield语句，就暂停执行后面的操作，并将紧跟在yield后面的那个表达式的值，作为返回的对象的value属性值。

（2）下一次调用next方法时，再继续往下执行，直到遇到下一个yield语句。

（3）如果没有再遇到新的yield语句，就一直运行到函数结束，直到return语句为止，并将return语句后面的表达式的值，作为返回的对象的value属性值。

（4）如果该函数没有return语句，则返回的对象的value属性值为undefined。

需要注意的是，yield语句后面的表达式，只有当调用next方法、内部指针指向该语句时才会执行，
因此等于为JavaScript提供了手动的“惰性求值”（Lazy Evaluation）的语法功能。


###与Iterator接口的关系

上一章说过，任意一个对象的Symbol.iterator方法，等于该对象的遍历器对象生成函数，调用该函数会返回该对象的一个遍历器对象。



##2.next方法的参数

*感觉有点恶心，好难理解*

yield句本身没有返回值，或者说总是返回undefined。next方法可以带一个参数，该参数就会被当作上一个yield语句的返回值。


##3.for...of循环

for...of循环可以自动遍历Generator函数，且此时不再需要调用next方法。


##4.Generator.prototype.throw()

Generator函数返回的遍历器对象，都有一个throw方法，可以在函数体外抛出错误，然后在Generator函数体内捕获。


##5.Generator.prototype.return()

Generator函数返回的遍历器对象，还有一个return方法，可以返回给定的值，并且终结遍历Generator函数。


##6.yield*语句

如果在Generater函数内部，调用另一个Generator函数，默认情况下是没有效果的。




