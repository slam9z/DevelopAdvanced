[HarvestedFramework](http://martinfowler.com/bliki/HarvestedFramework.html)


To build a framework by harvesting[收获], you start by not trying to build a framework, but by building an application. While you build the application you don't try to develop generic code, but you do work hard to build a well-factored and well designed application.

With one application built you then build another application which has at least some similar needs to the first one. While you do this you pay attention to any duplication between the second and first application. As you find duplication you factor out into a common area, this common area is the proto-framework.

As you develop further applications each one further refines the framework area of the code. During the first couple of applications you'd keep everything in a single code base. After a few rounds of this the framework should begin to stabilize and you can separate out the code bases.

While this sounds harder and less efficient than FoundationFramework it seems to work better in practice.
