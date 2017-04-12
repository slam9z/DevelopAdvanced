[ADO.NET Overview](https://msdn.microsoft.com/en-us/library/h43ks021(v=vs.110).aspx)



## [ADO.NET Architecture](https://msdn.microsoft.com/en-us/library/27y4ybxw(v=vs.110).aspx)


Choosing a DataReader or a DataSet



When you decide whether your application should use a DataReader (see Retrieving Data Using a DataReader) or a DataSet (see DataSets, DataTables, and DataViews), consider the type of functionality that your application requires. Use a DataSet to do the following:

•Cache data locally in your application so that you can manipulate it. If you only need to read the results of a query, the DataReader is the better choice.


•Remote data between tiers or from an XML Web service.


•Interact with data dynamically such as binding to a Windows Forms control or combining and relating data from multiple sources.


•Perform extensive processing on data without requiring an open connection to the data source, which frees the connection to be used by other clients.


If you do not require the functionality provided by the DataSet, you can improve the performance of your application by using the DataReader to return your data in a forward-only, read-only manner. Although the DataAdapter uses the DataReader to fill the contents of a DataSet (see Populating a DataSet from a DataAdapter), by using the DataReader, you can boost performance because you will save memory that would be consumed by the DataSet, and avoid the processing that is required to create and fill the contents of the DataSet.
