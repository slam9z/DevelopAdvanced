[Servant (design pattern)](https://en.wikipedia.org/wiki/Servant_(design_pattern))

> 不是很懂

In software engineering, the servant pattern defines an object used to offer some functionality to a group of classes without defining that functionality in each of them. A Servant is a class whose instance (or even just class) provides methods that take care of a desired service, while objects for which (or with whom) the servant does something, are taken as parameters.

## Description and simple example

Servant is used for providing some behavior to a group of classes. Instead of defining that behavior in each class - or when we cannot factor out this behavior in the common parent class - it is defined once in the Servant.

## Similar design pattern: Command

Design patterns Command and Servant are very similar and implementations of them are often virtually the same. The difference between them is the approach to the problem.

* For the Servant pattern we have some objects to which we want to offer some functionality. We create a class whose instances offer that functionality and which defines an interface that serviced objects must implement. Serviced instances are then passed as parameters to the servant.
* For the Command pattern we have some objects that we want to modify with some functionality. So, we define an interface which commands which desired functionality must be implemented. Instances of those commands are then passed to original objects as parameters of their methods.
