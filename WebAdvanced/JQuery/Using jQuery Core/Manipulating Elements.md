[Manipulating Elements](http://learn.jquery.com/using-jquery-core/manipulating-elements/#getting-and-setting-information-about-elements)

##Getting and Setting Information About Elements

There are many ways to change an existing element. Among the most common tasks is changing the inner HTML 
or attribute of an element. jQuery offers simple, cross-browser methods for these sorts of manipulations. 
You can also get information about elements using many of the same methods in their getter incarnations. 
For more information on getters and setters, see the Working with Selections section. 
Here are a few methods you can use to get and set information about elements:


* .html() – Get or set the HTML contents.
* .text() – Get or set the text contents; HTML will be stripped.
* .attr() – Get or set the value of the provided attribute.
* .width() – Get or set the width in pixels of the first element in the selection as an integer.
* .height() – Get or set the height in pixels of the first element in the selection as an integer.
* .position() – Get an object with position information for the first element in the selection, relative to its first positioned ancestor. This is a getter only.
* .val() – Get or set the value of form elements.

Changing things about elements is trivial, but remember that the change will affect all elements in the selection.
 If you just want to change one element, be sure to specify that in the selection before calling a setter method.

```js
 // Changing the HTML of an element.

$( "#myDiv p:first" ).html( "New <strong>first</strong> paragraph!" );
```

##Moving, Copying, and Removing Elements

While there are a variety of ways to move elements around the DOM, there are generally two approaches:


* Place the selected element(s) relative to another element.
* Place an element relative to the selected element(s).

For example, jQuery provides .insertAfter() and .after(). The .insertAfter() method places 
the selected element(s) after the element provided as an argument. The .after() method places the element
 provided as an argument after the selected element. Several other methods follow this pattern: .insertBefore()
 and .before(), .appendTo() and .append(), and .prependTo() and .prepend().

The method that makes the most sense will depend on what elements are selected, and whether you need to 
store a reference to the elements you're adding to the page. If you need to store a reference, 
you will always want to take the first approach – placing the selected elements relative to another 
element – as it returns the element(s) you're placing. In this case, .insertAfter(), .insertBefore(), 
.appendTo(), and .prependTo() should be the tools of choice.

```js
 // Moving elements using different approaches.

 

// Make the first list item the last list item:

var li = $( "#myList li:first" ).appendTo( "#myList" );

 

// Another approach to the same problem:

$( "#myList" ).append( $( "#myList li:first" ) );

 

// Note that there's no way to access the list item

// that we moved, as this returns the list itself.
```


##Cloning Elements

Methods such as .appendTo() move the element, but sometimes a copy of the element is needed instead. 
In this case, use .clone() first:

```js
 // Making a copy of an element.

 

// Copy the first list item to the end of the list:

$( "#myList li:first" ).clone().appendTo( "#myList" );
```

If you need to copy related data and events, be sure to pass true as an argument to .clone().


##Removing Elements

There are two ways to remove elements from the page: .remove() and .detach().
 Use .remove() when you want to permanently remove the selection from the page. 
While .remove() does return the removed element(s), those elements will not have their associated 
data and events attached to them if you return them to the page.

Use .detach() if you need the data and events to persist. Like .remove(), it returns the selection,
 but it also maintains the data and events associated with the selection,
 so you can restore the selection to the page at a later time.


##Creating New Elements

jQuery offers a trivial and elegant way to create new elements using the same $() method used to make selections:

```js
 // Creating new elements from an HTML string.

$( "<p>This is a new paragraph</p>" );

$( "<li class=\"new\">new list item</li>" );
```


##Manipulating Attributes

jQuery's attribute manipulation capabilities are extensive. Basic changes are simple, but the .attr() 
method also allows for more complex manipulations. It can either set an explicit value, or set a value 
using the return value of a function. When the function syntax is used, the function receives two arguments:
the zero-based index of the element whose attribute is being changed, and the current value of the attribute
being changed.




 




 
