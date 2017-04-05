[Event Sourcing](https://docs.microsoft.com/en-us/azure/architecture/patterns/event-sourcing)

Instead of storing just the current state of the data in a domain, use an append-only store to record the full series of actions taken on that data. The store acts as the system of record and can be used to materialize the domain objects. This can simplify tasks in complex domains, by avoiding the need to synchronize the data model and the business domain, while improving performance, scalability, and responsiveness. It can also provide consistency for transactional data, and maintain full audit trails and history that can enable compensating actions.


## Context and problem

The CRUD approach has some limitations:

* CRUD systems perform update operations directly against a data store, which can slow down performance and responsiveness, and limit scalability, due to the processing overhead it requires.

* In a collaborative domain with many concurrent users, data update conflicts are more likely because the update operations take place on a single item of data.

* Unless there's an additional auditing mechanism that records the details of each operation in a separate log, history is lost.
