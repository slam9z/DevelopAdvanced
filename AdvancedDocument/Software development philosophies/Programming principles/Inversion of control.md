[Inversion of control](https://en.wikipedia.org/wiki/Inversion_of_control)

In software engineering, inversion of control (IoC) is a design principle in which custom-written portions of a computer program receive the flow of control from a generic framework. A software architecture with this design inverts control as compared to traditional procedural programming: in traditional programming, the custom code that expresses the purpose of the program calls into reusable libraries to take care of generic tasks, but with inversion of control, it is the framework that calls into the custom, or task-specific, code.

Inversion of control is used to increase modularity of the program and make it extensible,[1] and has applications in object-oriented programming and other programming paradigms. The term was popularized by Robert C. Martin and Martin Fowler.

The term is related to, but different from, the dependency inversion principle, which concerns itself with decoupling dependencies between high-level and low-level layers through shared abstractions. The general concept is also related to event-driven programming in that it is often implemented using IoC, so that the custom code is commonly only concerned with handling of events, whereas the event loop and dispatch of events/messages is handled by the framework or the runtime environment.


## Overview

Inversion of control carries the strong connotation that the reusable code and the problem-specific code are developed independently even though they operate together in an application. **Software frameworks, callbacks, schedulers, event loops and dependency injection** are examples of design patterns that follow the inversion of control principle, although the term is most commonly used in the context of object-oriented programming.

Inversion of control serves the following design purposes:

* To decouple the execution of a task from implementation.
* To focus a module on the task it is designed for.
* To free modules from assumptions about how other systems do what they do and instead rely on contracts.
* To prevent side effects when replacing a module.

Inversion of control is sometimes facetiously referred to as the "Hollywood Principle: Don't call us, we'll call you".


## Implementation techniques

In object-oriented programming, there are several basic techniques to implement inversion of control. These are:


* Using a factory pattern

* Using a service locator pattern

* Using a dependency injection, for example
    
    * A constructor injection
    
    * A parameter injection
    
    * A setter injection
    
    * An interface injection

* Using a contextualized lookup

* Using template method design pattern

* Using strategy design pattern


In an original article by Martin Fowler,[6] the first three different techniques are discussed. In a description about inversion of control types,[7] the last one is mentioned. Often the contextualized lookup will be accomplished using a service locator.

More important than the applied technique, however, is the optimization of the purposes.