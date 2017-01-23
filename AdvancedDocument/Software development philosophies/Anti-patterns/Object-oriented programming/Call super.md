[Call super](https://en.wikipedia.org/wiki/Call_super)

> 意思是说在子类重载的时候需要调用父类方法一般是有问题的设计，可以使用模板方法改善。

Call super is a code smell or anti-pattern of some object-oriented programming languages. Call super is a design pattern in which a particular class stipulates that in a derived subclass, the user is required to override a method and call back the overridden function itself at a particular point. The overridden method may be intentionally incomplete, and reliant on the overriding method to augment its functionality in a prescribed manner. However, the fact that the language itself may not be able to enforce all conditions prescribed on this call is what makes this an anti-pattern.