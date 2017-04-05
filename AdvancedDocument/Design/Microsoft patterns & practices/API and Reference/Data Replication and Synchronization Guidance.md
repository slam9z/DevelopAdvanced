[Data Replication and Synchronization Guidance](https://msdn.microsoft.com/zh-cn/library/dn589787.aspx)


## Why Replicate and Synchronize Data?


## Replicating and Synchronizing Data


* **Master-Master Replication**, in which the data in each replica is dynamic and can be updated. This topology requires a two-way synchronization mechanism to keep the replicas up to date and to resolve any conflicts that might occur

* **Master-Subordinate Replication**, in which the data in only one of the replicas is dynamic (the master), and the remaining replicas are read-only. 


## Benefits of Replication


## Implementing Synchronization

Determining how to implement data synchronization is dependent to a great extent on the nature of the data and the type of the data stores. Some examples are:

* Use a ready-built synchronization service or framework. In Azure hosted and hybrid applications you might choose to 
    
    * The Azure SQL Data Sync service.        

    * The Microsoft Sync Framework.


* Use a synchronization technology built into the data store itself. Some examples are:

    * Azure storage geo-replication. By default in Azure data is automatically replicated in three datacenters (unless you turn it off) to protect against failures in one datacenter. This service can provide a read-only replica of the data.

    * SQL Server database replication. Synchronization using the built-in features of SQL Server Replication Service can be achieved between on-premises installations of SQL Server and deployments of SQL Server in Azure Virtual Machines in the cloud, and between multiple deployments of SQL Server in Azure Virtual Machines.

* Implement a custom synchronization mechanism. For example, use a messaging technology to pass updates between deployments of the application, and include code in each application to apply these updates intelligently to the local data store and handle any update conflicts. Consider the following when building a custom mechanism:

    
    * Ready-built synchronization services may have a minimum interval for synchronization, whereas a custom implementation could offer near-immediate synchronization.
    
    * Ready-built synchronization services may not allow you to specify the order in which data stores are synchronized. A custom implementation may allow you to perform updates in a specific order between several data stores, or perform complex transformation or other operations on the data that are not supported in ready-built frameworks and services.
    
    * When you design a custom implementation you should consider two separate aspects: how to communicate updates between separate locations, and how to apply updates to the data stores. Typically, you will need to create an application or component that runs in each location where updates will be applied to local data stores. This application or component will accept instructions that it uses to update the local data store, and then pass the updates to other data stores that contain copies of the data. Within the application or component you can implement logic to manage conflicting updates. However, by passing updates between data store immediately, rather than on a fixed schedule as is the case with most ready-built synchronization services, you minimize the chances of conflicts arising.
