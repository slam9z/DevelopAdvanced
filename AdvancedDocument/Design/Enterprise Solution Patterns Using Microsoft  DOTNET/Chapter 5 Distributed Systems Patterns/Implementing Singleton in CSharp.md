[Implementing Singleton in C#](https://msdn.microsoft.com/en-us/library/ff650316.aspx)


##Context 

You are building an application in C#. You need a class that has only one instance, and you need to provide 
a global point of access to the instance. You want to be sure that your solution is efficient and that it 
takes advantage of the Microsoft .NET common language runtime features. You may also want to make sure that
 your solution is thread safe.

##Implementation Strategy 

Even though Singleton is a comparatively simple pattern, there are various tradeoffs and options, depending
 upon the implementation. The following is a series of implementation strategies with a discussion of their 
strengths and weaknesses.

###Singleton

The following implementation of the Singleton design pattern follows the solution presented in Design 
Patterns: Elements of Reusable Object-Oriented Software [Gamma95] but modifies it to take advantage of
 language features available in C#, such as properties:
 
```cs
using System;

public class Singleton
{
   private static Singleton instance;

   private Singleton() {}

   public static Singleton Instance
   {
      get 
      {
         if (instance == null)
         {
            instance = new Singleton();
         }
         return instance;
      }
   }
}
```
 
This implementation has two main advantages:

* Because the instance is created inside the Instance property method, the class can exercise additional 
functionality (for example, instantiating a subclass), even though it may introduce unwelcome dependencies.

* The instantiation is not performed until an object asks for an instance; this approach is referred to as
 lazy instantiation. Lazy instantiation avoids instantiating unnecessary singletons when the application starts. 

The main disadvantage of this implementation, however, is that it is not safe for multithreaded environments. 
If separate threads of execution enter the Instance property method at the same time, more that one instance
 of the Singleton object may be created. Each thread could execute the following statement and decide that a 
new instance has to be created: 

```cs
if (instance == null)
```

Various approaches solve this problem. One approach is to use an idiom referred to as Double-Check Locking
 [Lea99]. However, C# in combination with the common language runtime provides a static initialization approach,
 which circumvents these issues without requiring the developer to explicitly code for thread safety. 

###Static Initialization 


One of the reasons Design Patterns [Gamma95] avoided static initialization is because the C++ specification left 
some ambiguity around the initialization order of static variables. Fortunately, the .NET Framework resolves this
 ambiguity through its handling of variable initialization:
 
```cs
public sealed class Singleton
{
   private static readonly Singleton instance = new Singleton();
   
   private Singleton(){}

   public static Singleton Instance
   {
      get 
      {
         return instance; 
      }
   }
}
```
 
In this strategy, the instance is created the first time any member of the class is referenced. The common language runtime
 takes care of the variable initialization. The class is marked sealed to prevent derivation, which could add instances. 
For a discussion of the pros and cons of marking a class sealed, see [Sells03]. In addition, the variable is marked readonly,
 which means that it can be assigned only during static initialization (which is shown here) or in a class constructor. 

This implementation is similar to the preceding example, except that it relies on the common language runtime to initialize
 the variable. It still addresses the two basic problems that the Singleton pattern is trying to solve: global access and
 instantiation control. The public static property provides a global access point to the instance. Also, because the
 constructor is private, the Singleton class cannot be instantiated outside of the class itself; therefore, the variable 
refers to the only instance that can exist in the system. 

Because the Singleton instance is referenced by a private static member variable, the instantiation does not occur until 
the class is first referenced by a call to the Instance property. This solution therefore implements a form of the lazy 
instantiation property, as in the Design Patterns form of Singleton.

The only potential downside of this approach is that you have less control over the mechanics of the instantiation. In 
the Design Patterns form, you were able to use a nondefault constructor or perform other tasks before the instantiation. 
Because the .NET Framework performs the initialization in this solution, you do not have these options. In most cases, 
static initialization is the preferred approach for implementing a Singleton in .NET. 

###Multithreaded Singleton 


Static initialization is suitable for most situations. When your application must delay the instantiation, use a non-default 
constructor or perform other tasks before the instantiation, and work in a multithreaded environment, you need a different
 solution. Cases do exist, however, in which you cannot rely on the common language runtime to ensure thread safety, as in
 the Static Initialization example. In such cases, you must use specific language capabilities to ensure that only one instance 
of the object is created in the presence of multiple threads. One of the more common solutions is to use the Double-Check 
Locking [Lea99] idiom to keep separate threads from creating new instances of the singleton at the same time. 

Note: The common language runtime resolves issues related to using Double-Check Locking that are common in other environments.
 For more information about these issues, see "The 'Double-Checked Locking Is Broken' Declaration," on the University of 
Maryland, Department of Computer Science Web site, at http://www.cs.umd.edu/~pugh/java/memoryModel/DoubleCheckedLocking.html.

The following implementation allows only a single thread to enter the critical area, which the lock block identifies, 
when no instance of Singleton has yet been created:
 
```cs
using System;

public sealed class Singleton
{
   private static volatile Singleton instance;
   private static object syncRoot = new Object();

   private Singleton() {}

   public static Singleton Instance
   {
      get 
      {
         if (instance == null) 
         {
            lock (syncRoot) 
            {
               if (instance == null) 
                  instance = new Singleton();
            }
         }

         return instance;
      }
   }
}
```
 
This approach ensures that only one instance is created and only when the instance is needed. Also,
 the variable is declared to be volatile to ensure that assignment to the instance variable completes
 before the instance variable can be accessed. Lastly, this approach uses a syncRoot instance to lock on, 
rather than locking on the type itself, to avoid deadlocks. 

This double-check locking approach solves the thread concurrency problems while avoiding an exclusive lock in every
 call to the Instance property method. It also allows you to delay instantiation until the object is first accessed.
 In practice, an application rarely requires this type of implementation. In most cases, the static initialization 
approach is sufficient.

##Resulting Context 

Implementing Singleton in C# results in the following benefits and liabilities:

###Benefits 

* The static initialization approach is possible because the .NET Framework explicitly defines how and 
when static variable initialization occurs. 

* The Double-Check Locking idiom described earlier in "Multithreaded Singleton" is implemented correctly in 
the common language runtime. 

###Liabilities 

If your multithreaded application requires explicit initialization, you have to take precautions[预防] to avoid threading issues.

##Acknowledgments 

[Gamma95] Gamma, Helm, Johnson, and Vlissides. Design Patterns: Elements of Reusable Object-Oriented Software. Addison-Wesley, 1995.
[Lea99] Lea, Doug. Concurrent Programming in Java, Second Edition. Addison-Wesley, 1999.
[Sells03] Sells, Chris. "Sealed Sucks." sellsbrothers.com News. Available at: http://www.sellsbrothers.com/news/showTopic.aspx?ixTopic=411.

>Note: Despite its title, the "Sealed Sucks" article is actually a balanced discussion of the pros and cons of marking a class sealed. 