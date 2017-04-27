[Convert Newtonsoft.Json.Linq.JArray to a list of specific object type](http://stackoverflow.com/questions/13565245/convert-newtonsoft-json-linq-jarray-to-a-list-of-specific-object-type)


## answer

Just call `array.ToObject<List<SelectableEnumItem>>()` method. It will return what you need.

Documentation: Convert JSON to a Type
