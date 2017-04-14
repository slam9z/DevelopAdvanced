[可排序的 COMB 类型 GUID](http://www.cnblogs.com/hmking/p/3965546.html)


最新代码在这儿：CombGuid.cs

首先这里不做GUID与整形作为主键的优劣之争，GUID自有它优势，但GUID本身是乱序的，会对索引的维护带来性能上的损耗，数据量越大越明显。

COMB 类型 GUID 是由Jimmy Nilsson在他的“The Cost of GUIDs as Primary Keys”一文中设计出来的。

基本设计思路是这样的：既然GUID数据因毫无规律可言造成索引效率低下，影响了系统的性能，那么能不能通过组合的方式，保留GUID的前10个字节，用后6个字节表示GUID生成的时间（DateTime），这样我们将时间信息与GUID组合起来，在保留GUID的唯一性的同时增加了有序性，以此来提高索引效率。


