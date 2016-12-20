[Coupling (computer programming)](https://en.wikipedia.org/wiki/Coupling_(computer_programming))

In software engineering, coupling is the degree of interdependence between software modules*  a measure of how closely connected two routines or modules are* [1] the strength of the relationships between modules


## Types of coupling ##

Coupling can be "low" (also "loose" and "weak") or "high" (also "tight" and "strong"). Some types of coupling, in order of highest to lowest coupling, are as follows:

### Procedural programming ###

A module here refers to a subroutine of any kind, i.e. a set of one or more statements having a name and preferably its own set of variable names.

* Content coupling (high): Content coupling (also known as '''Pathological coupling''') occurs when one module modifies or relies on the internal workings of another module (e.g., accessing local data of another module). In this situation, a change in the way the second module produces data (location, type, timing) might also require a change in the dependent module.
* Common coupling: Common coupling (also known as '''Global coupling''') occurs when two modules share the same global data (e.g., a global variable). Changing the shared resource might imply changing all the modules using it.
* External coupling: External coupling occurs when two modules share an externally imposed data format, communication protocol, or device interface. This is basically related to the communication to external tools and devices.
* Control coupling: Control coupling is one module controlling the flow of another, by passing it information on what to do (e.g., passing a what-to-do flag).
* Stamp coupling (Data-structured coupling): Stamp coupling occurs when modules share a composite data structure and use only parts of it, possibly different parts (e.g., passing a whole record to a function that only needs one field of it).
:In this situation, a modification in a field that a module does not need may lead to changing the way the module reads the record.
* Data coupling: Data coupling occurs when modules share data through, for example, parameters. Each datum is an elementary piece, and these are the only data shared (e.g., passing an integer to a function that computes a square root).
* Message coupling (low): This is the loosest type of coupling. It can be achieved by state decentralization (as in objects) and component communication is done via parameters or [[message passing]].
* No coupling: Modules do not communicate at all with one another.

### Object-oriented programming ###

* Subclass Coupling: Describes the relationship between a child and its parent. The child is connected to its parent, but the parent is not connected to the child.

* Temporal coupling: When two actions are bundled together into one module just because they happen to occur at the same time.

In recent work various other coupling concepts have been investigated and used as indicators for different modularization principles used in practice.<ref>F. Beck, S. Diehl. On the Congruence of Modularity and Code Coupling. In Proceedings of the 19th ACM SIGSOFT Symposium and the 13th European Conference on Foundations of Software Engineering (SIGSOFT/FSE '11), Szeged, Hungary, September 2011. {{doi|10.1145/2025113.2025162}}</ref>