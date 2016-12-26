[Dependency inversion principle](https://en.wikipedia.org/wiki/Dependency_inversion_principle)

> 有时候底层需要使用高层的方法,发布接口然后让高层继承是一个很好的办法，也可以使用委托，刚好就是依赖反转。

In object-oriented design, the dependency inversion principle refers to a specific form of decoupling software modules. When following this principle, the conventional dependency relationships established from high-level, policy-setting modules to low-level, dependency modules are reversed, thus rendering high-level modules independent of the low-level module implementation details. The principle states:[1]

* A. High-level modules should not depend on low-level modules. Both should depend on abstractions.
* B. Abstractions should not depend on details. Details should depend on abstractions.

This design principle inverts the way some people may think about object-oriented programming, dictating that both high- and low-level objects must depend on the same abstraction.[2]

The idea of the writing in two points of this principle is that when designing the interaction between a high-level module and a low-level one, the interaction should be thought as an abstract interaction between them. This not only has implications on the design of the high-level module, but also on the low-level one: the low-level one should be designed with the interaction in mind and it may be necessary to change its usage interface.

In many cases, thinking the interaction in itself as an abstract concept allows to reduce the coupling of the components without introducing additional coding patterns. Only allowing to find a lighter and less implementation dependent interaction schema.

When the discovered abstract interaction schema(s) between two modules is/are generic and generalization makes sense, this design principle also leads to the following dependency inversion coding pattern.


Traditional layers pattern
Dependency inversion pattern
Dependency inversion pattern generalization
