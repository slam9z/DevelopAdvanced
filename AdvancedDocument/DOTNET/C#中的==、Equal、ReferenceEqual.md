[C#中的==、Equal、ReferenceEqual](http://www.cnblogs.com/zagelover/articles/2741409.html)


1. ReferenceEquals, == , Equals 
Equals , == , ReferenceEquals都可以用于判断两个对象的个体是不是相等。

a) ReferenceEquals 
ReferenceEquals是Object的静态方法，用于比较两个引用类型的对象是否是对于同一个对象的引用。对于值类型它总是返回false。（因为Box以后的对象总是不同的，hehe）

b) ==是一个可以重载的二元操作符,可以用于比较两个对象是否相等。 
对于内置值类型，==判断的是两个对象的代数值是否相等。它会根据需要自动进行必要的类型转换，并根据两个对象的值是否相等返回true或者false。例如：


c) Equals 作为Object内置方法，Equals支持对于任意两个CTS对象的比较。 
Equals它有静态方法和可重载的一个版本，下面的程序片断解释了这两个方法的用法，


事实上，这两个版本的结果完全相同，如果用户重载了Equals，调用的都是用户重载后的Equals。Equals的静态方法的好处是可以不必考虑用于比较的对象是否为null。

Equals方法对于值类型和引用类型的定义不同，对于值类型，类型相同，并且数值相同(对于struct的每个成员都必须相同)，则Equals返回 true,否则返回false。而对于引用类型，默认的行为与ReferenceEquals的行为相同，仅有两个对象指向同一个Reference的时 候才返回true。可以根据需要对Equals进行重载，例如String类的Equals用于判断两个字符串的内容是否相等。