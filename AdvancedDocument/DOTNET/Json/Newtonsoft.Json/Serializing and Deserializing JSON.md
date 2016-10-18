[Serializing and Deserializing JSON](http://www.newtonsoft.com/json/help/html/SerializingJSON.htm)

##JsonConvert

For simple scenarios where you want to convert to and from a JSON string, the SerializeObject()  and DeserializeObject() 
 methods on JsonConvert provide an easy-to-use wrapper over JsonSerializer.


##JsonSerializer

For more control over how an object is serialized, the JsonSerializer can be used directly.
 The JsonSerializer is able to read and write JSON text directly to a stream via JsonTextReader and JsonTextWriter. 
Other kinds of JsonWriters can also be used, such as JTokenReader/JTokenWriter, to convert your object to and from LINQ to JSON objects,
 or BsonReader/BsonWriter, to convert to and from BSON.
