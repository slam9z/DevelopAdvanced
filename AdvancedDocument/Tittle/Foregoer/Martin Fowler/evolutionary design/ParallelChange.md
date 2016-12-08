[ParallelChange](http://martinfowler.com/bliki/ParallelChange.html)

Making a change to an interface that impacts all its consumers requires two thinking modes: implementing the change itself, and then updating all its usages. This can be hard when you try to do both at the same time, especially if the change is on a PublishedInterface with multiple or external clients.

Parallel change, also known as expand and contract, is a pattern to implement backward-incompatible changes to an interface in a safe manner, by breaking the change into three distinct phases: expand, migrate, and contract. 