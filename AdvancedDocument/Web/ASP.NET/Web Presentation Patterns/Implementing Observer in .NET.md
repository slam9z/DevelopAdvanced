[Implementing Observer in .NET](https://msdn.microsoft.com/en-us/library/ff648108.aspx)

Implementing Observer in .NET
Retired Content
This content is outdated and is no longer being maintained. It is provided as a courtesy for individuals who are still using these technologies. This page may contain URLs that were valid when originally published, but now link to sites or pages that no longer exist. Please see the patterns & practices guidance for the most current information.

Version 1.0.1
GotDotNet community for collaboration on this pattern
Complete List of patterns & practices

Context 
You are building an application in Microsoft .NET and you have to notify dependent objects of state changes without making the source object depend on the dependent objects. 
Background 
To explain how to implement Observer in .NET and the value provided by limiting the dependencies between objects, the following example refactors a solution, which has a bidirectional dependency, first into an implementation based on the Observer pattern defined in Design Patterns [Gamma95], then into a modified form of the Observer pattern for languages that have single inheritance of implementation, and finally into a solution that uses the .NET Framework language constructs delegates and events. 
The example problem has two classes, Album and BillingService (See Figure 1). 

Figure 1: Example UML static diagram

These two objects interact to play albums and to charge end-users each time an album is played. 
Album.cs 


The following example shows the implementation of the Album class:
 

using System;

public class Album 
{
   private BillingService billing;
   private String name; 

   public Album(BillingService billing, 
            string name)
   { 
      this.billing = billing;
      this.name = name; 
   }
   
   public void Play() 
   {
      billing.GenerateCharge(this);

      // code to play the album
   }

   public String Name
   {
      get { return name; }
   }
} 
BillingService.cs 


The following example shows the implementation of the BillingService class:
 

using System;

public class BillingService 
{
   public void GenerateCharge(Album album) 
   {
      string name = album.Name;
      // code to generate charge for correct album
   }
}
 
These classes have to be created in a specific order. Because the Album class needs the BillingService object for construction, it must be constructed last. After the objects are instantiated, the user is charged every time the Play method is called. 
Client.cs 


The following class, Client, demonstrates the construction process: 
 

using System;

class Client
{
   [STAThread]
   static void Main(string[] args)
   {
      BillingService service = new BillingService();
      Album album = new Album(service, "Up");
      
      album.Play();
   }
}
 
This code works, but there are at least two issues with it. The first is a bidirectional dependency between the Album class and the BillingService class. Album makes method calls on BillingService, and BillingService makes method calls on Album. This means that if you need to reuse the Album class somewhere else, you have to include BillingService with it. Also, you cannot use the BillingService class without the Album class. This is not desirable because it limits flexibility.
The second issue is that you have to modify the Album class every time you add or remove a new service. For example, if you want to add a counter service that keeps track of the number of times albums are played, you must modify the Album class's constructor and the Play method in the following manner: 
 

using System;

public class Album 
{
   private BillingService billing;
   private CounterService counter;
   private String name; 

   public Album(BillingService billing,
         CounterService counter,
             string name)
   { 
      this.billing = billing;
      this.counter = counter;
      this.name = name; 
   }
   
   public void Play() 
   {
      billing.GenerateCharge(this);
      counter.Increment(this);

      // code to play the album
   }

   public String Name
   {
      get { return name; }
   }
}
 
This gets ugly. These types of changes clearly should not involve the Album class at all. This design makes the code difficult to maintain. You can, however, use the Observer pattern to fix these problems. 
Implementation Strategy 
This strategy discusses and implements a number of approaches to the problems described in the previous section. Each solution attempts to correct issues with the previous example by removing portions of the bidirectional dependency between Album and BillingService. The first solution describes how to implement the Observer pattern by using the solution described in Design Patterns [Gamma95]. 
Observer 


The Design Patterns approach uses an abstract Subject class and an Observer interface to break the dependency between the Subject object and the Observer objects. It also allows for multiple Observers for a single Subject. In the example, the Album class inherits from the Subject class, assuming the role of the concrete subject described in the Observer pattern. The BillingService class takes the place of the concrete observer by implementing the Observer interface, because the BillingService is waiting to be told when the Play method is called. (See Figure 2.)

Figure 2: Observer class diagram

By extending the Subject class, you eliminate the direct dependence of the Album class on the BillingService. However, you now have a dependency on the Observer interface. Because Observer is an interface, the system is not dependent on the actual instances that implement the interface. This allows for easy extensions without modifying the Album class. You still have not removed the dependency between BillingService and Album. This one is less problematic, because you can easily add new services without having to change Album. The following examples show the implementation code for this solution. 
Observer.cs 


The following example shows the Observer class:
 

using System;

public interface Observer
{
   void Update(object subject);
}
 
Subject.cs 


The following example shows the Subject class:
 

using System;
using System.Collections;

public abstract class Subject
{
   private ArrayList observers = new ArrayList(); 

   public void AddObserver(Observer observer)
   {
      observers.Add(observer);
   }

   public void RemoveObserver(Observer observer)
   {
      observers.Remove(observer);
   }

   public void Notify()
   {
      foreach(Observer observer in observers)
      {
         observer.Update(this);
      }
   }
}
 
Album.cs 


The following example shows the Album class:
 

using System;

public class Album : Subject
{
   private String name; 

   public Album(String name)
   { this.name = name; }
   
   public void Play() 
   {
      Notify();

      // code to play the album
   }

   public String Name
   {
      get { return name; }
   }
}
 
BillingService.cs 


The following example shows the BillingService class:
 

using System;

public class BillingService : Observer
{
   public void Update(object subject)
   {
      if(subject is Album)
         GenerateCharge((Album)subject);
   }

   private void GenerateCharge(Album album) 
   {
      string name = album.Name;

      //code to generate charge for correct album
   }
}
 
You can verify in the example that the Album class no longer depends on the BillingService class. This is very desirable if you need to use the Album class in a different context. In the "Background" example, you would need to bring along the BillingService class if you wanted to use Album. 
Client.cs 


The following code describes how to create the various objects and the order in which to do it. The biggest difference between this construction code and the "Background" example is how the Album class finds out about BillingService. In the "Background" example, BillingService was passed explicitly as a construction parameter to Album. In this example, you call a function named AddObserver to add the BillingService, which implements the Observer interface.
 

using System;

class Client
{
   [STAThread]
   static void Main(string[] args)
   {
      BillingService billing = new BillingService();
      Album album = new Album("Up");

      album.AddObserver(billing);

      album.Play();
   }
}
 
Use of inheritance to share the Subject implementation. The Microsoft Visual Basic .NET development system and the C# language allow for single inheritance of implementation and multiple inheritance of interfaces. In this example, you need to use single inheritance to share the Subject implementation. This precludes using it to categorize Albums in an inheritance hierarchy. 
Single observable activity. The Album class notifies the observers whenever the Play method is called. If you had another function, such as Cancel, you would have to send the event along with the Album object to the services so they would know if this were a Play or Cancel event. This complicates the services, because they are notified of events that they may not be interested in. 
Less explicit, more complicated. The direct dependency is gone, but the code is less explicit. The initial implementation had a direct dependency between Album and BillingService, so it was easy to see how and when the GenerateCharge method was called. In this example, Album calls the Notify method in Subject, which iterates through a list of previously registered Observer objects and calls the Update method. This method in the BillingService class calls GenerateCharge. If you are interested in a great description of the virtues of being explicit, see "To Be Explicit," Martin Fowler's article in IEEE Software [Fowler01].
Modified Observer 


The primary liability of Observer [Gamma95] is the use of inheritance as a means for sharing the Subject implementation. This also limits the ability to be explicit about which activities Observer is interested in being notified about. To solve these problems, the next part of the example introduces the modified Observer. In this solution, you change the Subject class into an interface. You also introduce another class named SubjectHelper, which implements the Subject interface (See Figure 3). 

Figure 3: Modified Observer class diagram

The Album class contains SubjectHelper and exposes it as a public property. This allows classes like BillingService to access the specific SubjectHelper and indicate that it is interested in being notified if Album changes. This implementation also allows the Album class to have more than one SubjectHelper; perhaps, one per exposed activity. The following code implements this solution (the Observer interface and BillingService class are omitted here because they have not changed). 
Subject.cs 


In the following example, Notify has changed because you now have to pass the Subject into the SubjectHelper class. This was unnecessary in the Observer [Gamma95] example because the Subject class was the base class. 
 

using System;
using System.Collections;

public interface Subject
{
   void AddObserver(Observer observer);
   void RemoveObserver(Observer observer);
   void Notify(object realSubject);
}
 
SubjectHelper.cs 


The following example shows the newly created SubjectHelper class:
 

using System;
using System.Collections;

public class SubjectHelper : Subject
{
   private ArrayList observers = new ArrayList(); 

   public void AddObserver(Observer observer)
   {
      observers.Add(observer);
   }

   public void RemoveObserver(Observer observer)
   {
      observers.Remove(observer);
   }

   public void Notify(object realSubject)
   {
      foreach(Observer observer in observers)
      {
         observer.Update(realSubject);
      }
   }
}
 
Album.cs 


The following example shows how the Album class changes when using SubjectHelper instead of inheriting from the Subject class: 
 

using System;

public class Album
{
   private String name; 
   private Subject playSubject = new SubjectHelper();

   public Album(String name)
   { this.name = name; }
   
   public void Play() 
   {
      playSubject.Notify(this);

      // code to play the album
   }

   public String Name
   {
      get { return name; }
   }

   public Subject PlaySubject
   {
      get { return playSubject; }
   }
}
 
Client.cs 


The following example shows how the Client class changes:
 

using System;

class Client
{
   [STAThread]
   static void Main(string[] args)
   {
      BillingService billing = new BillingService();
      CounterService counter = new CounterService();
      Album album = new Album("Up");

      album.PlaySubject.AddObserver(billing);
      album.PlaySubject.AddObserver(counter);

      album.Play();
   }
}
 
You can probably already see some of the benefits of reducing coupling between the classes. For example, the BillingService class did not have to change at all, even though this refactoring rearranged the implementation of Subject and Album quite a bit. Also, the Client class is easier to read now, because you can specify to which particular event you attach the services.
More complicated. The original solution consisted of two classes that talked directly to each other in an explicit fashion; now you have two interfaces and three classes that talk indirectly, and a lot of code that was not there in the first example. No doubt, you are starting to wonder if that dependency was not so bad in the first place. Keep in mind, though, that the two interfaces and the SubjectHelper class can be reused by as many observers as you want. So it is likely that you will have to write them only once for the whole application.
Less explicit. This solution, like Observer [Gamma95], makes it difficult to determine which observer is observing the changes to Subject. 

So this solution makes good object-oriented design, but requires you to create a lot of classes, interfaces, associations, and so on. Is all of that really necessary in .NET? The answer is, "no," as the following example shows.
Observer in .NET 


The built-in features of .NET help you to implement the Observer pattern with much less code. There is no need for the Subject, SubjectHelper, and Observer types because the common language runtime makes them obsolete. The introduction of delegates and events in .NET provides a means of implementing Observer without developing specific types.

In the .NET-based implementation, an event represents an abstraction (supported by the common language runtime and various compilers) of the SubjectHelper class described earlier in "Modified Observer." The Album class exposes an event type instead of SubjectHelper. The observer role is slightly more complicated. Rather than implementing the Observer interface and registering itself with the subject, an observer must create a specific delegate instance and register this delegate with the subject's event. The observer must use a delegate instance of the type specified by the event declaration; otherwise, registration will fail. During the creation of this delegate instance, the observer provides the name of the method (instance or static) that will be notified by the subject. After the delegate is bound to the method, it may be registered with the subject's event. Likewise, this delegate may be unregistered from the event. Subjects provide notification to observers by invocation of the event. [Purdy02]

The following code examples highlight the changes you must make to the example in "Modified Observer" to use delegates and events.
Album.cs 


The following example shows how the Album class exposes the event type:
 

using System;

public class Album 
{
   private String name; 

   public delegate void PlayHandler(object sender);
   public event PlayHandler PlayEvent;
   
   public Album(String name)
   { this.name = name; }
   
   public void Play() 
   {
      Notify();

      // code to play the album
   }

   private void Notify()
   {
      if(PlayEvent != null) 
         PlayEvent(this);
   }

   public String Name
   {
      get { return name; }
   }

}
 
BillingService.cs 


As the following example shows, the changes to the BillingService class from the example in "Modified Observer" only involve removing the implementation of the Observer interface: 
 

using System;

public class BillingService
{
   public void Update(object subject)
   {
      if(subject is Album)
         GenerateCharge((Album)subject);
   }

   private void GenerateCharge(Album theAlbum) 
   {
      //code to generate charge for correct album
   }
}
 
Client.cs 


The following example shows how the Client class has been modified to use the new event that is exposed by the Album class:
 

using System;

class Client
{
   [STAThread]
   static void Main(string[] args)
   {
      BillingService billing = new BillingService();
      Album album = new Album("Up");

      album.PlayEvent += new Album.PlayHandler(billing.Update);
      album.Play();
   }
}
 
As you can see, the structure of the program is nearly identical to the previous example. The built-in features of .NET replace the explicit Observer mechanism. After you get used to the syntax of delegates and events, their use becomes more intuitive. You do not have to implement the SubjectHelper class and the Subject and Observer interfaces described in "Modified Observer." These concepts are implemented directly in the common language runtime.
The greatest benefit of delegates is their ability to refer to any method whatsoever (provided that it conforms to the same signature). This permits any class to act as an observer, independent of what interfaces it implements or classes it inherits from. While the use of the Observer and Subject interfaces reduced the coupling between the observer and subject classes, use of delegates completely eliminates it. For more information on this topic, see "Exploring the Observer Design Pattern," in the MSDN developer program library [Purdy02].
Testing Considerations 
Because delegates and events completely eliminate the bidirectional assembly between Album and BillingService, you can now write tests for each class in isolation. 
AlbumFixture.cs 


The AlbumFixture class describes example unit tests in NUnit (http://www.nunit.org) that verify that the PlayEvent is fired when the Play method is called:
 

using System;
using NUnit.Framework;

[TestFixture]
public class AlbumFixture
{
   private bool eventFired; 
   private Album album;

   [SetUp]
   public void Init()
   {
      album = new Album("Up");
      eventFired = false; 
   }

   [Test]
   public void Attach()
   {
      album.PlayEvent += new Album.PlayHandler(OnPlay);
      album.Play();

      Assertion.AssertEquals(true, eventFired);
   }

   [Test]
   public void DoNotAttach()
   {
      album.Play();
      Assertion.AssertEquals(false, eventFired);
   }

   private void OnPlay(object subject)
   {
      eventFired = true;
   }
}
 
Resulting Context 
The benefits of implementing Observer in .NET with the delegates and events model clearly outweigh the potential liabilities.
Benefits 


Implementing Observer in .NET provides the following benefits:
Eliminates dependencies. The examples clearly showed that the dependency was eliminated from the Album and BillingService classes. 
Increases extensibility. The "Observer in .NET" example demonstrated how easy it was to add new types of observers. The Album class is an example of the Open/Closed Principle, first written by Bertrand Meyer in Object-Oriented Software Construction, 2nd Edition [Bertrand00], which describes a class that is open to extension but closed to modification. The Album class embodies this principle because you can add observers of the PlayEvent without modifying the Album class. 
Improves testability. "Testing Considerations" demonstrated how you could test the Album class without having to instantiate BillingService. The tests verified that the Album class worked correctly. The tests also provide an excellent example of how to write BillingService. 
Liabilities 


As shown in the example, the implementation of Observer is simple and straightforward. However, as the number of delegates and events increases, it becomes difficult to follow what happens when an event is fired. As a result, the code can become very difficult to debug because you must search through the code for the observers. 
Related Patterns 
For more background on the concepts discussed here, see the following related patterns:
Observer
Model-View-Controller
Acknowledgments 
[Bertrand00] Meyer, Bertrand. Object-Oriented Software Construction, 2nd Edition. Prentice-Hall, 2000.
[Fowler01] Fowler, Martin. "To Be Explicit." IEEE Software, November/December 2001.
[Fowler03] Fowler, Martin. Patterns of Enterprise Application Architecture. Addison-Wesley, 2003.
[Gamma95] Gamma, Helm, Johnson, and Vlissides. Design Patterns: Elements of Reusable Object-Oriented Software. Addison-Wesley, 1995.
[Purdy02] Purdy, Doug; Richter, Jeffrey. "Exploring the Observer Design Pattern." MSDN Library, January 2002. Available at: http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dnbda/html/observerpattern.asp.
