[Three-Layered Services Application](https://msdn.microsoft.com/en-us/library/ff648105.aspx)

##Context 

You are designing a Layered Application. You want to expose some of the core functionality of your application as 
services that other applications can consume, and you want your application to consume services exposed by other applications.

##Problem 

How do you layer a service-oriented application and then determine the components in each layer?

##Forces 

In addition to the forces discussed in Layered Application, the following forces apply:

* You always want to minimize the impact of adding services to an existing application.

* Services are often exposed to clients outside the corporate firewall, and therefore have different 
security and operational requirements than do business components.

* Communicating with other services involves significant knowledge of protocols and data formats.

* You want to separate the concerns of your components so that you are only changing them for one reason,
 such as to isolate your business logic from the technology required to access an external service. 

##Solution 

Base your layered architecture on three layers: presentation, business, and data. This pattern presents an overview
 of the responsibilities of each layer and the components that compose each layer. For more information, see the 
article, "Application Architecture for .NET: Designing Applications and Services." [PnP02]. 

[Figure 1: Three-Layered Services Application]

Three-Layered Services Application, as presented here, is basically a relaxed three-layered architecture. 
The three layers are:

* Presentation. The presentation layer provides the application's user interface (UI). Typically, this involves the
 use of Windows Forms for smart client interaction, and ASP.NET technologies for browser-based interaction.

* Business. The business layer implements the business functionality of the application. The domain layer is 
typically composed of a number of components implemented using one or more .NET - enabled programming languages. 
These components may be augmented with Microsoft .NET Enterprise Services for scalable distributed component solutions
 and Microsoft BizTalk Server for workflow orchestration.

* Data The data layer provides access to external systems such as databases. The primary .NET technology involved at
 this layer is ADO.NET. However, it is not uncommon to use some .NET XML capabilities here as well.

Each layer should be structured as described in the following paragraphs.

###Presentation Layer 


For most business applications, a form metaphor is used to structure the presentation layer. The application 
consists of a series of forms (pages) with which the user interacts. Each form contains a number of fields that display
 output from lower layers and collect user input. 

Two types of components that implement forms-based user interfaces are:

* User interface components
* User interface process components

####User Interface Components 

For rich-client applications, this pattern uses UI components from the System.Windows.Forms namespace of the .NET 
Framework. For Web applications, this pattern uses ASP.NET components. If the standard .NET components do not meet
 your needs, .NET supports subclassing of the standard UI components, as well as plugging your own custom components
 into the framework. 

####User Interface Process Components 


Complex user interfaces often require many highly complex forms. To increase reusability, maintainability,
 and extensibility, you can create a separate user interface process (UIP) component to encapsulate dependencies
 between forms and the logic associated with navigating between them. You can apply the same concept to the dependencies, 
validation, and navigation between components of a single form. These UIP components are typically custom components 
that are based on design patterns such as Front Controller, Application Controller [Fowler03], and Mediator [Gamma95].

The interaction between UI and UIP components often follows the Model-View-Controller or
 Presentation-Abstraction-Controller [Buschmann96] pattern. 

###Business Layer 


Large enterprise applications are often structured around the concepts of business processes and business components. 
These concepts are addressed through a number of components, entities, agents, and interfaces in the business layer. 

####Business Components 


In Business Component Factory, Peter Herzum and Oliver Sims define a business component as follows:

>The software implementation of an autonomous business concept or business process. It consists of all 
>the software artifacts necessary to represent, implement, and deploy a given business concept as an autonomous,
> reusable element of a larger distributed information system. [Herzum00]

Business components are the software realization of business concepts. They are the primary units of design,
 implementation, deployment, maintenance, and management for the life cycle of the business application. Business 
components encapsulate the business logic, also called business rules. These rules constrain the behavior of a 
business concept to match the needs of a particular company. For example, the business rule that determines 
whether a given customer is approved for a line of credit may be encapsulated in the customer business component
 for small solutions. For larger solutions, it is likely that all credit-related business logic is encapsulated 
in a separate credit component.

>Note:Three-Layered Services Application diverges from the Herzum and Oliver definition in that business processes 
>components are separated into their own class: Business Workflow Components.

####Business Workflows 


Business processes reflect the macro-level activities that the business performs. Examples include order processing,
 customer support, and procurement of materials. These business processes are encapsulated by business workflow 
components that orchestrate one or more business components to implement a business process. For example, a ProcessOrder
 business workflow component may interact with Customer, Order, and Fulfillment business components to carry out the
 Process Order business process. You can use any .NET language to develop custom business workflow components. 
Alternatively, you can use BizTalk Server to define the business process and automatically orchestrate the business components.

####Business Entities 


Business entities are data containers. They encapsulate and hide the details of specific data representation formats. 
For instance, a business entity may initially encapsulate a recordset obtained from a relational database. Later, that
 same business entity may be modified to wrap an XML document with minimal impact to the rest of the application. 

Business and business workflow components can interact with independent business entity components, or they can use 
a business entity to set their own state and then discard the business entity. Business entities are often used as
 Data Transfer Objects [Fowler03]. The data access components will often return business entities instead of 
database-specific structures. This helps significantly in isolating database-specific details to the data layer.

####Service Interfaces 


An application may expose some of its functionality as a service that other applications can use. A service interface
 presents this service to the outside world. Ideally, it hides the implementation details and exposes only a course-grained
 business interface. Service interfaces are often implemented using XML Web services.

If you are using a domain model, classes in your domain model are often realized by one or more domain layer components.

###Data Layer 


Most business applications must access data that is stored in corporate databases, which are most often relational
 databases. Data access components in this data layer are responsible for exposing the data stored in these databases
 to the business layer.

####Data Access Components 


Data access components isolate the business layer from the details of the specific data storage solution.
 This isolation provides the following benefits:

Minimizes the impact[冲击] of a change in database provider.
Minimizes the impact of a change in data representation (for example, a change in database schema).
Encapsulates all code that manipulates a particular data item in one place. This greatly simplifies testing and maintenance.

ADO.NET can be used directly as the data access components for simple applications. More complex applications may 
benefit from developing a set of classes over ADO.NET that help you to manage the complexities of object-relational mapping.

####Service Gateways 

Business components often must access internal and external services or applications. A service gateway is a 
component that encapsulates the interface, protocol, and code required to use such services. For example, 
a business solution often requires information from the accounting system to complete a business process. 
The solution would delegate all interaction with the accounting system to a service gateway. The service 
gateway makes it much easier to change the external service provider. The service gateway can even simulate 
the external service to facilitate testing of the domain layer. 

###Foundation Services 


In addition to the three standard layers, Three-Layered Services Application defines a set of foundation services 
that all layers can use. These services fall into three basic categories:

* Security. These services maintain application security. 

* Operational management. These services manage components and associated resources, and also meet operational 
requirements such as scalability and fault tolerance.

* Communication. These are services, such as .NET remoting, SOAP, and asynchronous messaging, which provide 
communication between components.


##Resulting Context
 
Using Three-Layered Services Application results in the following benefits and liabilities:

###Benefits 

The three layers prescribed by this pattern are a great starting point for designing your own solutions.
 You accrue most of the benefits of the Layered Application pattern while minimizing the negative effects 
of having to cross too many layers.

###Liabilities 


For complex solutions, it may be necessary to further divide the domain layer, especially if reuse is a high priority or 
if you are designing a family of solutions based on a common set of components. In such cases, it is common to replace 
the one business layer described in this pattern with the following three layers (For details, see Larman02):

* Application. The application layer contains business components that are unique to the application.

* Domain. The domain layer contains business components that are common within the business domain. Examples include
 components related to the insurance, energy, or banking industry.

* Business services. The business services layer contains business components that provide common business functionality 
such as financial, product, and order functionality.

One user interface layer may be insufficient for solutions that provide complex user interfaces. Data validation,
 command processing, printing, and undo/redo are a few examples of functionality that may require additional layering.

##Acknowledgments 

[Buschmann96] Buschmann, Frank, et al. Pattern-Oriented Software Architecture. John Wiley & Sons Ltd, 1996.
[Fowler03] Fowler, Martin. Patterns of Enterprise Application Architecture. Addison-Wesley, 2003.
[Gamma95] Gamma, Helm, Johnson, and Vlissides. Design Patterns: Elements of Reusable Object-Oriented Software. Addison-Wesley, 1995.
[Herzum00] Herzum, Peter and Sims, Oliver. Business Component Factory. John Wiley and Sons, Inc., 2000. 
[Larman02] Larman, Craig. Applying UML and Patterns. Prentice-Hall PTR, 2002.
[PnP02] patterns & practices, Microsoft Corporation. "Application Architecture for .NET: Designing Applications and Services."
 MSDN Library. Available at:http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dnbda/html/distapp.asp.