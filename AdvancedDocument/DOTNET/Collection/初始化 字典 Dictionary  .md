##Index initializers

Object and collection initializers are useful for declaratively initializing fields and properties of objects, or giving 
a collection an initial set of elements. Initializing dictionaries and other objects with indexers is less elegant. We are 
adding a new syntax to object initializers allowing you to set values to keys through any indexer that the new object has:

```cs
var numbers = new Dictionary<int, string> 
{
    [7] = "seven",
    [9] = "nine",
    [13] = "thirteen"
};

或者

```cs

var names = new Dictionary<int, string> 
{
  { 1, "Adam" },
  { 2, "Bart" },
  { 3, "Charlie" }
};

```