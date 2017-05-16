[Observer](https://msdn.microsoft.com/en-us/library/ff649896.aspx)

##Context 

In object-oriented programming, objects contain both data and behavior that, together, represent a specific aspect of the
 business domain. One advantage of using objects to build applications is that all manipulation of the data can be encapsulated[压缩] 
inside the object. This makes the object self-contained and increases the potential[潜在的] for reuse of the object in other 
applications. However, objects cannot simply work in isolation. In all but the most trivial applications, objects must
 collaborate to accomplish[完成] more complex tasks. When objects collaborate, the objects may have to notify each other when 
an object changes state. For example, the Model-View-Controller pattern prescribes the separation of business data (the model)
 and the presentation logic (the view). When the model changes, the system must notify the view so that it can refresh the
 visual presentation and accurately reflect the model's state. In other words, the view is dependent on the model to inform
 it of changes to the model's internal state.


##Problem 

How can an object notify other objects of state changes without being dependent on their classes?

##Forces 

A solution to this problem has to reconcile the following forces and considerations: 

* The easiest way to inform dependent objects of a state change is to call them directly. However, direct collaboration[合作]
 between objects creates dependency between their classes. For example, if the model object calls the view object to 
inform it of changes, the model class is now also dependent on the view class. This kind of direct coupling between
 two objects (also called tight coupling) decreases the reusability of classes. For example, whenever you want to reuse
 the model class, you have to also reuse the view class because the model makes calls to it. If you have more than one 
view, the problem is compounded. 

* The need to decouple classes occurs frequently in event-driven frameworks. The framework must be able to notify the 
application of events, but the framework cannot be dependent on specific application classes.

* Likewise, if you make a change to the view class, the model is likely to be affected. Applications that contain many
 tightly coupled classes tend to be brittle and difficult to maintain, because changes in one class could affect all 
the tightly coupled classes.

* If you call the dependent objects directly, every time a new dependent is added, the code inside the source object 
has to be modified.

* In some cases, the number of dependent objects may be unknown at design time. For example, if you allow the user to 
open multiple windows (views) for a specific model, you will have to update multiple views when the model state changes.

* A direct function call is still the most efficient way to pass information between two objects (second only to having
 the functionality of both objects inside a single object). As a result, decoupling objects with other mechanisms is 
likely to adversely affect performance. Depending on the performance requirements of the application, you may have to
 consider this tradeoff.

##Solution
 
Use the Observer pattern to maintain a list of interested dependents (observers) in a separate object (the subject). 
Have all individual observers implement a common Observer interface to eliminate direct dependencies between the subject 
and the dependent objects (see Figure 1).

[Figure 1: Basic Observer structure]

When a state change occurs in the client that is relevant to the dependent objects, ConcreteSubject invokes the Notify() 
method. The Subject superclass maintains a list of all interested observers so that the Notify() method can loop through
 the list of all observers and invoke the Update() method on each registered observer. The observers register and unregister
 for updates by calling the subscribe() and unsubscribe() methods on Subject (see Figure 2). One or more instances of 
ConcreteObserver may also access ConcreteSubject for more information and therefore usually depend on the ConcreteSubject
 class. However, as Figure 1 illustrates, there is no direct or indirect dependency from the ConcreteSubject class on the
 ConcreteObserver class.

[Figure 2: Basic Observer interaction]

With this generic way of communicating between the subject and observers, collaborations can be built dynamically instead of
 statically. Due to the separation of notification logic and synchronization logic, new observers can be added without modifying 
the notification logic, and notification logic can also be changed without affecting the synchronization logic in observers.
 The code is now much more separate, and thus easier to maintain and reuse. 

Notifying objects of changes without incurring a dependency on their classes is such a common requirement that some 
platforms provide language features to perform this function. For example, the Microsoft .NET Framework defines 
the notion of delegates and events to accomplish the Observer role. Therefore, you would rarely implement the Observer
 pattern explicitly in .NET, but should use delegates and events instead. Most .NET developers will think of the Observer
 pattern as a complicated way to implement events.

The solution presented in Figure 1 shows the ConcreteSubject class inheriting from the Subject class. The Subject 
class contains the implementations of the methods to add or remove observers and to iterate through the list of 
observers. All ConcreteSubject has to do is to inherit from Subject and call Notify() when a state change occurs. 
In languages that only support single inheritance (such as Java or C#), inheriting from Subject precludes the class
 from inheriting from any other class. This can be a problem because in many cases ConcreteSubject is a domain object 
that may inherit from a domain object base class. Therefore, it is a better idea to replace the Subject class with a 
Subject interface and to provide a helper class for the implementation (see Figure 3). This way, you do not exhaust 
your single superclass relationship with the Subject class but can use the ConcreteSubject in another inheritance hierarchy. 
Some languages (for example, Smalltalk) even implement the Subject interface as part of the Objectclass, from which every
 class inherits implicitly.

[Figure 3: Using a helper class to avoid inheriting from the Subject class]

Unfortunately, you now have to add code to each class that inherits from the Subject interface to implement the methods 
defined in the interface. This task can become very repetitious. Also, because the domain object coincides with 
ConcreteSubject, it cannot differentiate between different types of state changes that would be associated with different
 subjects. This only allows observers to subscribe to all state changes of ConcreteSubject, even though you may want to
 be more selective (for example, if the source object contains a list, you may want to be notified of updates, but not 
of insertions). You could have the observers filter out notifications that are not relevant, but that makes the solution
 less efficient, because ConcreteSubject calls all the observers just to find out that they are really not interested.

You can resolve these issues by separating the subject completely from the source class (see Figure 4). Doing so reduces
 ConcreteSubject to the implementation of the Subject interface; it has no other responsibilities. This allows 
DomainObject to be associated with more than one ConcreteSubject so that you can differentiate between different 
types of events for a single domain class.

[Figure 4: Separating DomainObject and Subject]

The event and delegate features in the .NET Framework implement this approach as a language construct so that you 
do not even have to implement your own ConcreteSubject classes anymore. Basically, events replace the ConcreteSubject 
classes, and delegates implement the role of the Observer interface. 


###Propagating[传播] State Information 


So far, this solution has described how a client object can notify the observers when a state change occurs, but has 
not yet discussed how the observers find out which state the client object is in. There are two mechanisms for passing
 this information to the observers:

* Push model. In the push model, the client sends all relevant[紧密相关的] information regarding the state change to the subject, 
which passes the information on to each observer. If the information is passed in a neutral format (for example, XML), 
this model keeps the dependent observers from having to access the client directly for more information. On the other 
hand, the subject has to make some assumptions about which information is relevant to the observers. If a new observer
 is added, the subject may have to publish additional information required by that observer. This would make the subject 
and the client once again dependent on the observers - the problem you were trying to solve in the first place. So when 
using the push model, you should err on the side of inclusion when determining the amount of information to pass to the
 observers. In many cases, you would include a reference to the subject in the call to the observer. The observers can 
use that reference to obtain state information.

* Pull model. In the pull model, the client notifies the subject of a state change. After the observers receive notification,
 they access the subject or the client for additional data (see Figure 5) by using a getState() method. This model does not
 require the subject to pass any information along with the update() method, but it may require that the observer call 
getState() just to figure out that the state change was not relevant. As a result, this model can be a little more inefficient.
 Another possible complication occurs when the observer and the subject run in different threads (for example, if you use RMI
 to notify the observers). In this scenario, the internal state of the subject may have changed again by the time the observer
 obtains the state information through the callback. This may cause the observer to skip an operation.


[Figure 5: State propagation using the pull model]

###When to Trigger an Update 


When implementing the Observer pattern, you have two options for managing the triggering of the update. The first option 
is to insert the call to Notify() in the client right after each call to Subject that affects an internal state change.
 This gives the client full control over the frequency of the notification, but also adds extra responsibility to the
 client, which can lead to errors if the developer forgets to call Notify(). The other choice is to encapsulate the
 call to Notify() inside each state-changing operation of Subject. This way, a state change always causes Notify() to
 be called without additional action from the client. The downside is that several nested operations might cause multiple
 notifications. Figure 6 shows an example of this in which Operation A calls Sub-Operation B and an observer might receive 
two calls to its Update method.

[Figure 6: Extraneous notifications]

Calling multiple updates for a single, but nested operation can cause some inefficiency, but also leads to more
 serious side effects: The subject could be in an invalid state when the nested Notify method is invoked at the e
nd of Operation B (see Figure 6) because Operation A has only been processed part of the way. In this case, the nested 
notify should be avoided. For example, Operation B can be extracted out into a method without notification logic and can 
rely on the call to Notify() inside Operation A. Template Method [Gamma95] is a useful construct for making sure the 
observers are notified only once.

###Observers Affecting State Change 


In some cases, an observer may change the state of the subject while it is processing the update() call. This could 
lead to problems if the subject automatically calls Notify() after each state change. Figure 7 shows why.

[Figure 7: Modifying object state from within Update causes an infinite loop]

In this example, the observer performs Operation A in response to the state change notification. If Operation 
A changes the state of DomainObject, it then triggers another call to Notify(), which in turn calls the Update 
method of the observer again. This results in an infinite loop. The infinite loop is easy to spot in this simple
 example, but if relationships are more complicated, it may be difficult to determine the dependency chain. One 
way to reduce the likelihood of infinite loops is to make the notification interest-specific. For example,
 in C#, use the following interface for subject, where Interest could be an enumeration of all types of interest:
 
```cs
interface Subject 
{
public void addObserver(Observer o, Interest a);
public void notify(Interest a);
...
}

interface Observer
{
   public void update(Subject s, Interest a);
}
```

Allowing observers to be notified only when an event related to their specific interest occurs reduces the 
dependency chain and helps to avoid infinite loops. This is equivalent to defining multiple, more narrowly
 defined, event types in .NET. The other option for avoiding the loop is to introduce a locking mechanism 
to keep the subject from publishing new notifications while it is still inside the original Notify() loop.

##Example 

See Implementing Observer in .NET.


##Resulting Context 

Because Observer supports loose coupling and reduces dependencies, should you loosely couple every pair of objects that 
depend on each other? Certainly not. As is the case with most patterns, one solution rarely solves all problems. You 
need to consider the following tradeoffs when employing the Observer pattern.

###Benefits 

* Loose coupling and reduced dependencies. The client is no longer dependent on the observers because it is
 isolated through the use of a subject and the Observer interface. This advantage is used in many frameworks 
where application components can register to be notified when (lower-level) framework events occur. As a result, 
the framework calls the application component, but is not dependent on it.

* Variable number of observers. Observers can attach and detach during runtime because the subject makes no 
assumptions about the number of observers. This feature is useful in scenarios where the number of observers
 is not known at design time (for example, if you need an observer for each window that the user opens in the application).

###Liabilities 

* Decreased performance. In many implementations, the update() methods of the observers may execute in the same thread
 as the subject. If the list of observers is long, the Notify() method may take a long time. Abstracting object dependencies 
does not mean that adding observers has no impact on the application.

* Memory leaks. The callback mechanism (when an object registers to be called later) used in Observer can lead to a
 common mistake that results in memory leaks, even in managed C# code. Assuming that an observer goes out of scope
 but forgets to unsubscribe from the subject, the subject still maintains a reference to the observer. This reference 
prevents garbage collection from reallocating the memory associated with the observer until the subject object is 
destroyed as well. This can lead to serious memory leaks if the lifetime of the observers is much shorter than the
 lifetime of the subject (which is often the case).

* Hidden dependencies. The use of observers turns explicit dependencies (through method invocations) into implicit 
dependencies (via observers). If observers are used extensively throughout an application, it becomes nearly impossible 
for a developer to understand what is happening by looking at the source code. This makes it very difficult to understand 
the implications of code changes. The problem grows exponentially with the levels of propagation (for example, an observer
 acting as Subject). Therefore, you should limit the use of observers to few well-defined interactions, such as the
 interaction between model and view in the Model-View-Controller pattern. The use of observers between domain objects 
should generally be cause for suspicion.

* Testing/Debugging difficulties. As much as loose coupling is a great architectural feature, it can make development
 more difficult. The more you decouple two objects, the more difficult it becomes to understand the dependencies 
between them when looking at the source code or a class diagram. Therefore, you should only loosely couple objects 
if you can safely ignore the association between them (for example, if the observer is free of side effects).

##Related Patterns 

For more information, see Implementing Observer in .NET.

##Acknowledgments 

[Gamma95] Gamma, Helm, Johnson, and Vlissides. Design Patterns: Elements of Reusable Object-Oriented Software. Addison-Wesley, 1995.