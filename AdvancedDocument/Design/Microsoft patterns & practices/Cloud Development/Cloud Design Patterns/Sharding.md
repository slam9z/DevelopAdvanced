[Sharding](https://docs.microsoft.com/en-us/azure/architecture/patterns/sharding)


Divide a data store into a set of horizontal partitions or shards. This can improve scalability when storing and accessing large volumes of data.


## Sharding strategies


### The Lookup strategy.


### The Range strategy. 


### The Hash strategy. 

The purpose of this strategy is to reduce the chance of hotspots (shards that receive a disproportionate amount of load). It distributes the data across the shards in a way that achieves a balance between the size of each shard and the average load that each shard will encounter. 


