[通过实例模拟ASP.NET MVC的Model绑定机制：简单类型+复杂类型 ](http://www.cnblogs.com/artech/archive/2012/05/23/default-model-binding-01.html)

##一、简单类型

对于旨在绑定目标Action方法参数值的Model来说，最简单的莫过于简单参数类型的情况。通过《初识Model元数据》的介绍我们知道，
*复杂类型和简单类型之间的区别仅仅在于是否支持针对字符串类型的转换。*
由于参数值的数据源在请求中以字符串的形式存在，对于支持字符串转换的简单类型来说，可以直接通过类型转换得到参数值。

##二、复杂类型

对于简单类型的参数来说，由于支持与字符串类型之间的转换，相应ValueProvider可以直接从数据源中提取相应的数据并直接转换成参数类型。
所以针对简单类型的Model绑定是一步到位的过程，但是针对复杂类型的Model绑定就没有这么简单了。复杂对象可以表示为一个树形层次化结构，
其对象本身和属性代表相应的节点，叶子节点代表简单数据类型属性。而ValueProvider采用的数据源是一个扁平的数据结构，
它通过基于属性名称前缀的Key实现与这个对象树中对应叶子节点的映射。

[通过实例模拟ASP.NET MVC的Model绑定机制：数组 ](http://www.cnblogs.com/artech/archive/2012/05/30/default-model-binding-02.html)

##一、基于名称的数组绑定

##二、基于索引的数组绑定


[过实例模拟ASP.NET MVC的Model绑定的机制：集合+字典 ](http://www.cnblogs.com/artech/archive/2012/05/31/default-model-binding-03.html)

##一、集合

##二、 字典