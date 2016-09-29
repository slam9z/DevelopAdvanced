[Deployment Patterns](https://msdn.microsoft.com/en-us/library/ff646997.aspx)

>"What do you mean it doesn't run in production? It ran fine in the development
> environment." - Anonymous developer


Building enterprise class solutions involves not only developing custom software, but also deploying
 this software into a production server environment. This is where software development efforts intersect
 with systems infrastructure efforts. Bringing these two disciplines together effectively requires a
 common understanding of the issues involved and a strong set of application and system infrastructure
 skills. The required skills are rarely found in a single team; therefore, deployment activities often 
involve the collaboration of several teams, with each team contributing specialized skills. To simplify
 discussion, this chapter assumes that there are two teams: the application development team and the
 system infrastructure team.


##Bringing Teams Together 

The application development team is responsible for developing and maintaining a set of software components 
that fulfill the application's requirements. This team is primarily concerned with meeting functional requirements 
quickly and flexibly. Its members seek to manage complexity by creating software abstractions that make the s
ystem easy to extend and maintain. 

The system infrastructure team is responsible for building and maintaining the servers and network infrastructure. 
Its members are primarily concerned with such operational requirements as security, availability, reliability, 
and performance. Stability and predictability are critical success factors, which are primarily addressed by
 controlling change and by managing known good configurations.


The forces acting on the application development team are quite different from the forces acting on 
the system infrastructure team. The result is an inherent tension between the two teams. If this tension
 is not addressed, the resulting solution may be optimized for one team or the other, but it will not be an
 optimal business solution. Resolving the tension is a critical element for delivering a holistic, 
software-intensive enterprise solution that is optimized for the overall needs of the business. 

The patterns in this chapter help reduce the tension between the teams by offering guidance on how to 
optimally structure your applications and technical infrastructure to efficiently fulfill the solution's
 requirements. The patterns then discuss how to map the software structure to the hardware infrastructure. 
Specifically, this chapter presents a set of patterns that enable you to:

* Organize your software application into logical layers.
* Refine your logical layering approach to provide and consume services.
* Organize your hardware into physical tiers, so you can scale out.
* Refine your physical tier strategy in a three-tiered configuration.
* Allocate processes to processors with a deployment plan.

##Patterns Overview 

Although the concepts of layer and tier are often used interchangeably, the patterns in this chapter make a strong
 distinction between the two terms. A layer is a logical structuring mechanism for the elements that make up 
your software solution; a tier is a physical structuring mechanism for the system infrastructure. The first 
set of patterns in this cluster deals with the logical structuring of the software application into layers.
 The second set of patterns explores the physical structuring of the system infrastructure into tiers. 

Figure 1 shows both sets of patterns and their interrelationships.

[Figure 1: Deployment cluster]

###Application Patterns 


The first pattern in this cluster, Layered Application, organizes a software application into a set of logical 
layers for the purpose of managing dependencies and creating pluggable components. The pattern defines exactly 
what a layer is and then describes how to define your own layers. It also describes some additional techniques 
that build on and amplify the benefits of using a layered application. One of the key benefits of Layered Application 
is that the well-defined interfaces and strong dependency management gives you a great deal of flexibility in deploy
 an application. Although it is very hard to distribute a single-layered application across multiple servers,
 it is much easier to divide the application at layer boundaries and distribute the different parts to multiple 
servers. Not every layer boundary makes a good distribution boundary, however, because the forces that mold your
 layering decisions are different from the forces that shape your distribution decisions. For more information, 
see Deployment Plan. 

Layered Application is applied extensively throughout the software development world. A common implementation 
of the pattern for enterprise applications is three-layered application. This implementation defines three layers:
 presentation, business, and data. Although you can add more layers, these three layers are almost always needed
 for enterprise business applications.

Most enterprise applications are now developed using a component-based approach. Although many definitions of a 
component exist, the simplest definition is that a component is a unit of self-contained software functionality 
that can be independently deployed. Components can be plugged into and out of an execution environment that exposes 
an agreed-on set of interfaces at runtime. This pluggability offers a great deal of flexibility when it comes to 
deployment. The self-contained aspect of components makes them the smallest units at which deployment decisions 
can be made. 

Three-Layered Services Application refines Layered Applicationto provide specific structuring guidance for enterprise
 applications that collaborate with other enterprise applications in a larger service-oriented architecture. 
It expands on the typical three layers described earlier and defines a set of component types for each layer. 

###Infrastructure Patterns 


The next set of patterns in this cluster focuses on the physical infrastructure. The context for these patterns is
 an infrastructure that supports an application distributed over more than one server. Specifically, these patterns
 do not address mainframe or other large multiprocessor infrastructure configurations.

Tiered Distribution organizes the system infrastructure into a set of physical tiers to provide specific server 
environments optimized for specific operational requirements and system resource usage. A single-tiered infrastructure 
is not very flexible; the servers must be generically configured and designed around the strictest of operational 
requirements and must support the peak usage of the largest consumers of system resources. Multiple tiers, on the 
other hand, enable multiple environments. You can optimize each environment for a specific set of operational 
requirements and system resource usage. You can then deploy components onto the tier that most closely matches
 their resource needs and enables them to best meet their operational requirements. The more tiers you use, the
 more deployment options you will have for each component.

Three-Tiered Distribution refines Tiered Distribution to provide specific guidance on structuring the infrastructure
 for Web applications with basic security and other operational requirements. The pattern suggests that the solution's
 servers be organized into three tiers: client, Web application, and data. The client and data tiers are self-explanatory, 
and the Web application tier hosts application business components as well as the Web presentation components. For 
solutions with more stringent security and operational requirements, you may want to consider moving the Web functionality
 into its own tier. 

###Bringing Applications and Infrastructure Together 


The final pattern in the cluster is Deployment Plan, which describes a process for allocating components to 
tiers. During this process, it is critical to ensure proper communication between the application development
 and system infrastructure teams. All the previous patterns increase the deployment flexibility of the software
 application and the systems infrastructure. Deployment Plan builds on this deployment flexibility, which provides
 more options for the teams to resolve conflicts of interest. Resolving these conflicts increases the chance of
 delivering a solution that provides optimum business value. The pattern concludes by describing four common models
 that typically result when this process is applied to enterprise applications: simple Web application, complex Web
 application, extended enterprise, and rich client.

###Deployment Patterns 

Table 1 lists the patterns included in the deployment cluster, along with the problem statements
 for each, which should serve as a roadmap to the patterns.

Table 1: Deployment Patterns

|---|---|
| Layered Application | How do you structure an application to support such operational requirements as maintainability, reusability, scalability, robustness, and security? |
| Three-Layered Services Application | How do you layer a service-oriented application and then determine the components in each layer?|
| Tiered Distribution | How should you structure your servers and distribute functionality across them to efficiently meet the operational requirements of the solution? |
| Three-Tiered Distribution | How many tiers should you have, and what should be in each tier? |
| Deployment Plan | How do you determine which tier you should deploy each of your components to? |