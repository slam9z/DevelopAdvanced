[Data Consistency Primer](https://msdn.microsoft.com/library/dn589800.aspx)


## Managing Data Consistency



## Strong Consistency

In the strong consistency model, all changes are atomic. If a transaction updates multiple data items, the transaction is not allowed to complete until either all of the changes have been made successfully, or (in the event of a failure) they have all been undone. In the time between a transaction starting and completing, other concurrent transactions may not be able access any of the data that has been modified; they will be blocked. If data is being replicated, a transaction that implements strong consistency may not be allowed to complete until every copy of each item that has changed has been successfully updated. 



## Eventual Consistency

Eventual consistency is a rather more pragmatic approach to data consistency. In many cases, strong consistency is not actually required as long all the work performed by a transaction is completed or rolled back at some point, and no updates are lost. In the eventual consistency model, data update operations that span multiple sites can ripple through the various data stores in their own time, without blocking concurrent application instances that access the same data. 


One of the drives for eventual consistency is that distributed data stores are subject to the CAP Theorem. This theorem states that a distributed system can implement only two of the three features (Consistency, Availability, and Partition Tolerance) at any one time. In practice, this means that you can either:

* Provide a consistent view of distributed (partitioned) data at the cost of blocking access to that data while anyinconsistencies are resolved. This may take an indeterminate time, especially in systems that exhibit a high degreeof latency or if a network failure causes loss of connectivity to one or more partitions.

* Provide immediate access to the data at the risk of it being inconsistent across sites. Traditional databasemanagement systems focus on providing strong consistency, whereas cloud-based solutions that utilize partitioneddata stores are typically motivated by ensuring higher availability, and are therefore more oriented towardseventual consistency.


### Retrying Failing Steps



### Partitioning Data and Using Idempotent Commands


### Implementing Compensating Logic