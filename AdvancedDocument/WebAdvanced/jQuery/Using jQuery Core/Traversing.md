[Traversing](http://learn.jquery.com/using-jquery-core/traversing/)

##Parents

The methods for finding the parents from a selection include .parent(), .parents(), .parentsUntil(), and .closest().

##Children

The methods for finding child elements from a selection include .children() and .find().
 The difference between these methods lies in how far into the child structure the selection is made. 
.children() only operates on direct child nodes, while .find() can traverse recursively into children,
 children of those children, and so on.

##Siblings

The rest of the traversal methods within jQuery all deal with finding sibling selections. 
There are a few basic methods as far as the direction of traversal is concerned. 
You can find previous elements with .prev(), next elements with .next(), and both with .siblings(). 
There are also a few other methods that build onto these basic methods:
 .nextAll(), .nextUntil(), .prevAll() and .prevUntil().

