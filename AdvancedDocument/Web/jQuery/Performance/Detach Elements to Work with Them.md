[Detach Elements to Work with Them](http://learn.jquery.com/performance/detach-elements-before-work-with-them/)


```js
 var table = $( "#myTable" );

var parent = table.parent();

 

table.detach();

 

// ... add lots and lots of rows to table

 

parent.append( table );
```

 