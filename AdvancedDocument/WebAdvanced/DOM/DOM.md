[Document Object Model](https://developer.mozilla.org/en-US/docs/Web/API/Document_Object_Model)

*The Document Object Model (DOM)* is a programming interface for HTML, XML and SVG documents. 
It provides a structured representation of the document as a tree. 
The DOM defines methods that allow access to the tree, so that they can change the document structure, style and content. 
The DOM provides a representation of the document as a structured group of nodes and objects, possessing various properties and methods.
Nodes can also have event handlers attached to them, and once an event is triggered, the event handlers get executed. Essentially,
it connects web pages to scripts or programming languages.

Although the DOM is often accessed using JavaScript, it is not a part of the JavaScript language. 
It can also be accessed by other languages.

##DOM interfaces


##HTML interfaces

A document containing HTML is described using the HTMLDocument interface. 
Note that the HTML specification also extends the Document interface.

An HTMLDocument object also gives access to various features of browsers  like the tab or the window, 
in which a page is drawn using the Window interface, the Style associated to it (usually CSS), 
the history of the browser relative to the context, History. Eventually, Selection is done on the document.
