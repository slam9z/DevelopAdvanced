[GRASP (object-oriented design)](https://en.wikipedia.org/wiki/GRASP_(object-oriented_design))
    
**General responsibility assignment software patterns**(or principles), abbreviated GRASP, consist of guidelines for assigning responsibility to classes and objects in object-oriented design.

The different patterns and principles used in GRASP are: controller, creator, indirection, information expert, high cohesion, low coupling, polymorphism, protected variations, and pure fabrication. All these patterns answer some software problem, and these problems are common to almost every software development project. These techniques have not been invented to create new ways of working, but to better document and standardize old, tried-and-tested programming principles in object-oriented design.

Computer scientist Craig Larman states that "the critical design tool for software development is a mind well educated in design principles. It is not the UML or any other technology."[1] Thus, GRASP are really a mental toolset, a learning aid to help in the design of object-oriented software.


## Patterns

### Controller

The controller pattern assigns the responsibility of dealing with system events to a non-UI class that represents the overall system or a use case scenario. A controller object is a non-user interface object responsible for receiving or handling a system event.

### Creator

Creation of objects is one of the most common activities in an object-oriented system. Which class is responsible for creating objects is a fundamental property of the relationship between objects of particular classes.

### High cohesion


### Indirection


### Information expert


### Low coupling


### Polymorphism


### Protected variations 


### Pure fabrication

A pure fabrication is a class that does not represent a concept in the problem domain, specially made up to achieve low coupling, high cohesion, and the reuse potential thereof derived (when a solution presented by the information expert pattern does not). This kind of class is called a "service" in domain-driven design.


