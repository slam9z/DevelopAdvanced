[Reduce 和 Transduce 的含义](http://www.ruanyifeng.com/blog/2017/03/reduce_transduce.html)

## 一、reduce 的用法

reduce是一种数组运算，通常用于将数组的所有成员"累积"为一个值。

```js
    var arr = [1, 2, 3, 4];

    var sum = (a, b) => a + b;

    arr.reduce(sum, 0) // 10
```

上面代码中，reduce对数组arr的每个成员执行sum函数。sum的参数a是累积变量，参数b是当前的数组成员。每次执行时，b会加到a，最后输出a。

累积变量必须有一个初始值，上例是reduce函数的第二个参数0。如果省略该参数，那么初始值默认是数组的第一个成员。

## 二、map 是 reduce 的特例

累积变量的初始值也可以是一个数组。

事实上，所有的map方法都可以基于reduce实现。

```js
    function map(f, arr) {
      return arr.reduce(function(result, x) {
        result.push(f(x));
        return result;
      }, []);
    }
```

因此，map只是reduce的一种特例。

## 三、reduce的本质

本质上，reduce是三种运算的合成。

* 遍历
* 变形
* 累积

## 四、 transduce 的含义

reduce包含了三种运算，因此非常有用。但也带来了一个问题：代码的复用性不高。在reduce里面，变形和累积是耦合的，不太容易拆分。

每次使用reduce，开发者往往都要从头写代码，重复实现很多基本功能，很难复用别人的代码。

```js
    var handler = function (newArr, x) {
      newArr.push(x + 1);
      return newArr;
    };
```

上面的这个处理函数，就很难用在其他场合。

有没有解决方法呢？回答是有的，就是把"变形"和"累积"这两种运算分开。如果reduce允许变形运算和累积运算分开，那么代码的复用性就会大大增加。这就是transduce方法的由来。

transduce这个名字来自 transform（变形）和 reduce 这两个单词的合成。它其实就是reduce方法的一种不那么耦合的写法。

```js
    // 变形运算
    var plusOne = x => x + 1;

    // 累积运算
    var append = function (newArr, x) {
      newArr.push(x);
      return newArr;
    }; 

    R.transduce(R.map(plusOne), append, [], arr);
    // [2, 3, 4, 5]
```

上面代码中，plusOne是变形操作，append是累积操作。我使用了 Ramda 函数库的transduce实现。可以看到，transduce就是将变形和累积从reduce拆分出来，其他并无不同。

## 五、transduce 的用法

transduce最大的好处，就是代码复用更容易。


## 六、Transformer 对象

transduce函数的第一个参数是一个对象，称为 Transformer 对象（变形器）。前面例子中，R.map(plusOne)返回的就是一个 Transformer 对象。

事实上，任何一个对象只要遵守 Transformer 协议，就是 Transformer 对象。

```js
    var Map = function(f, xf) {
        return {
           "@@transducer/init": function() { 
               return xf["@@transducer/init"](); 
           },
           "@@transducer/result": function(result) { 
               return xf["@@transducer/result"](result); 
           },
           "@@transducer/step": function(result, input) {
               return xf["@@transducer/step"](result, f(input)); 
           }
        };
    };
 ```   

上面代码中，Map函数返回的就是一个 Transformer 对象。它必须具有以下三个属性。

```
        @@transducer/step：执行变形操作
        @@transducer/init：返回初始值
        @@transducer/result：返回变形后的最终值
```

所有符合这个协议的对象，都可以与其他 Transformer 对象合成，充当transduce函数的第一个参数。

因此，transduce函数的参数类型如下。

```
    transduce(
      变形器 : Object,
      累积器 : Function,
      初始值 : Any,
      原始数组 : Array
    )
```

## 七、into 方法

最后，你也许发现了，前面所有示例使用的都是同一个累积器。

