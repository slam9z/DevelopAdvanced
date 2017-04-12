﻿这里就是自己C#的编程规范，一定要遵守。

## Naming Conventions

Use camelCase与PascalCase命名方式。

1. 方法名和类名与属性使用PascalCase方式

1. 局部变量使用camelCase方式

1. 私有变量使用camelCase方式加_前缀

1. 静态私有变量使用camelCase方式加s_前缀

1. const声明使用全大写+_。

1. Enum值要中文！

1. 命名不要使用中文。

	[C#成员中文命名？](http://www.zhihu.com/question/29426006)

1. 类名和方法名不要使用编号。

	和中文一样，有时候是缺乏领域开发的认识，提取关键字。

1. 不能改的代码连咸鱼都不如！

1. 部分类可以加Partial后缀



## Define Group

这个比较麻烦，自己也没弄好过，需要好好看一下书。主要是看类型与含义，这两者是有冲突的。

1. 属性与它的私有变量定义，需要Group，先私有变量再属性。

1. 一般采用变量与属性，构造函数，方法从上到下定义。

## Define Method

1. 一般习惯将callback方法，写在最后，但是default参数冲突。如果callback不可为空，怎作为第一个参数，可为空就放在最后。 

1. 使用模板方法的时候方法名加 Internal 后缀。

1. 定义一堆Extentions Method 编程感觉不错，可以使用链式的编程方法。


## Define Class

1. 尽可能不要使用静态类，毫无扩展性，可以用单实例对象替代静态类。


## Define Enum


## Expression

1. 使用Expression来构建查询语句是一个非常好的办法：动态，安全，高效。

