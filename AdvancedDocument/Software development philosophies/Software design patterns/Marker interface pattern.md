[Marker interface pattern](https://en.wikipedia.org/wiki/Marker_interface_pattern)

> 这个自己用过

The marker interface pattern is a design pattern in computer science, used with languages that provide run-time type information about objects. It provides a means to associate metadata with a class where the language does not have explicit support for such metadata.

To use this pattern, a class implements a marker interface[1] (also called tagging interface), and methods that interact with instances of that class test for the existence of the interface. Whereas a typical interface specifies functionality (in the form of method declarations) that an implementing class must support, a marker interface need not do so. The mere presence of such an interface indicates specific behavior on the part of the implementing class. Hybrid interfaces, which both act as markers and specify required methods, are possible but may prove confusing if improperly used.

An example of the application of marker interfaces from the Java programming language is the Serializable interface. A class implements this interface to indicate that its non-transient data members can be written to an ObjectOutputStream. The ObjectOutputStream private method writeObject() contains a series of instanceof tests to determine writeability, one of which looks for the Serializable interface. If any of these tests fails, the method throws a NotSerializableException.


## Critique

A major problem with marker interfaces is that an interface defines a contract for implementing classes, and that contract is inherited by all subclasses. This means that you cannot "unimplement" a marker. In the example given, if you create a subclass that you do not want to serialize (perhaps because it depends on transient state), you must resort to explicitly throwing NotSerializableException (per ObjectOutputStream docs)

Another solution is for the language to support metadata directly: