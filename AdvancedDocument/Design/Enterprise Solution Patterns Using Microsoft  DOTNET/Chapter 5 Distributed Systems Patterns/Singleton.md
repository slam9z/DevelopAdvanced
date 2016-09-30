[Singleton](https://msdn.microsoft.com/en-us/library/ff650849.aspx)


##Context 

In some situations, a certain type of data needs to be available to all other objects in the application. 
In most cases, this type of data is also unique in the system. For example, a user interface can have only 
one mouse pointer that all applications must access. Likewise, an enterprise solution may interface with 
a single-gateway object that manages the connection to a specific legacy system. 

##Problem 

How do you make an instance of an object globally available and guarantee that only one instance of the class is created?

>Note: The definition of singleton used here is intentionally narrower than in Design Patterns: Elements of
> Reusable Object-Oriented Software [Gamma95].

##Forces 

The following forces act on a system within this context and must be reconciled as you consider a solution to the problem:

* Many programming languages (for example, Microsoft Visual Basic version 6.0 or C++) support the definition of
 objects that are global in scope. These objects reside at the root of the namespace and are universally available
 to all objects in the application. This approach provides a simple solution to the global accessibility problem 
but does not address the one-instance requirement. It does not stop other objects from creating other instances 
of the global object. Also, other object-oriented languages, such as Visual Basic .NET or C#, do not directly
 support global variables.

* To ensure that only a single instance of a class can exist, you must control the instantiation process. This
 implies that you need to prevent other objects from creating an instance of the class by using the instantiation
 mechanism inherent in the programming language (for example, by using the new operator). The other part of
 controlling the instantiation is providing a central mechanism by which all objects can obtain a reference to the single instance.

##Solution 

Singleton provides a global, single instance by:

* Making the class create a single instance of itself. 

* Allowing other objects to access this instance through a class method that returns a reference to the instance.
 A class method is globally accessible.

* Declaring the class constructor as private so that no other object can create a new instance.

Figure 1 shows the static structure of this pattern. The UML class diagram is surprisingly simple because Singleton
 consists of a simple class that holds a reference to a single instance of itself. 

[Figure 1: Singleton structure]

Figure 1 shows that the Singleton class contains a public class-scope (static) property, which returns a
 reference to the single instance of the Singleton class. (The underline in UML indicates a class-scope property.) 
Also, the numeral 1 in the upper-right corner indicates that there can only be one instance of this class in the
 system at any time. Because the default constructor for Singleton is private, any other object in the system has
 to access the Singleton object through the Instance property. 

The Singleton pattern is often classified as an idiom rather than a pattern because the solution depends primarily 
on the features of the programming language you use (for example, class methods and static initializers).
 Separating the abstract concept from a particular implementation, as this patterns collection does, may make
 the Singleton implementation look surprisingly simple. 

##Example 

For an example, see Implementing Singleton in C#.

##Resulting Context 

Singleton results in the following benefits and liabilities:

###Benefits 

* Instance control. Singleton prevents other objects from instantiating their own copies of the Singleton object,
 ensuring that all objects access the single instance.

* Flexibility. Because the class controls the instantiation process, the class has the flexibility to change the
 instantiation process.

###Liabilities 

* Overhead. Although the amount is minuscule, there is some overhead involved in checking whether an instance 
of the class already exists every time an object requests a reference. This problem can be overcome by using
 static initialization as described in Implementing Singleton in C#.

* Possible development confusion[困惑]. When using a singleton object (especially one defined in a class library),
 developers must remember that they cannot use the new keyword to instantiate the object. Because application 
developers may not have access to the library source code, they may be surprised to find that they cannot instantiate 
this class directly.

* Object lifetime. Singleton does not address the issue of deleting the single object. In languages that provide
 memory management (for example, languages based on the .NET Framework), only the Singleton class could cause 
the instance to be deallocated because it holds a private reference to the instance. In languages, such as C++,
 other classes could delete the object instance, but doing so would lead to a dangling reference inside the Singleton class.

##Related Patterns 

For more information, see the following related patterns:

* Abstract Factory [Gamma95]. In many cases, Abstract Factories are implemented as singletons. Typically, factories should be globally accessible. Restricting the factory to a single instance ensures that the one factory globally controls object creation. This is useful if the factory allocates object instances from a pool of objects. 
* Monostate [单声道的][Martin02]. Monostate is similar to the Singleton, but it focuses on state rather than on identity. Instead of controlling the instances of an object, Monostate ensures that only one shared state exists for all instances by declaring all data members static.
* Implementing Broker with .NET Remoting Using Server-Activated Objects. This pattern uses a Singleton factory to create new objects on the server.

##Acknowledgments 

[Gamma95] Gamma, Helm, Johnson, and Vlissides. Design Patterns: Elements of Reusable Object-Oriented Software. Addison-Wesley, 1995.
[Martin02] Martin, Robert C. Agile Software Development: Principles, Patterns, and Practices. Prentice Hall, 2002.