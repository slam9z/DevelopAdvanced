[Sequential coupling](https://en.wikipedia.org/wiki/Sequential_coupling)

> 我有时候也会写出这种代码。

In object-oriented programming, sequential coupling refers to a class that requires its methods to be called in a particular sequence. This may be an anti-pattern, depending on context.

Methods whose name starts with Init, Begin, Start, etc. may indicate the existence of sequential coupling.

Using a car as an analogy, if the user steps on the gas without first starting the engine, the car does not crash, fail, or throw an exception - it simply fails to accelerate.

Sequential coupling can be refactored with the template method pattern to overcome the problems posed by the usage of this anti-pattern.