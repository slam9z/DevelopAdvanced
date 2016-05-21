[Document](https://developer.mozilla.org/en-US/docs/Web/API/Document)

The Document interface represents any web page loaded in the browser and serves as an entry point into the web 
page's content, which is the DOM tree. The DOM tree includes elements such as <body> and <table>, 
among many others. It provides functionality which is global to the document, such as obtaining the
 page's URL and creating new elements in the document.

The Document interface describes the common properties and methods for any kind of document.
 Depending on the document's type (e.g. HTML, XML, SVG, …), a larger API is available: HTML documents, 
served with the text/html content type, also implement the HTMLDocument interface, wherease SVG documents
 implement the SVGDocument interface.


##Properties

This interface also inherits from the Node and EventTarget interfaces.

* Document.characterSet Read only 

    Returns the character set being used by the document.

* Document.charset Read only  

    Alias of Document.characterSet. Use this property instead.

* Document.compatMode Read only  

    Indicates whether the document is rendered in quirks or strict mode.

* Document.contentType Read only  

    Returns the Content-Type from the MIME Header of the current document.

* Document.doctype Read only 

    Returns the Document Type Definition (DTD) of the current document.

* Document.documentElement Read only 

    Returns the Element that is a direct child of the document. For HTML documents, this is normally the HTML element.

* Document.documentURI Read only 

    Returns the document location as a string.

* Document.domConfig 

    Should return a DOMConfiguration object.


##Extension for HTML document

The Document interface for HTML documents inherit from the HTMLDocument interface or, 
since HTML5,  is extended for such documents:

* Document.activeElement Read only 

    Returns the currently focused element.

* Document.alinkColor 

    Returns or sets the color of active links in the document body.

* Document.anchors

    Returns a list of all of the anchors in the document.

* Document.applets

    Returns an ordered list of the applets within a document.

* Document.bgColor 

    Gets/sets the background color of the current document.

* Document.body

    Returns the <body> element of the current document.

* Document.cookie

    Returns a semicolon-separated list of the cookies for that document or sets a single cookie.

Document.defaultView Read only 

    Returns a reference to the window object.

* Document.designMode

    Gets/sets the ability to edit the whole document.

* Document.dir Read only 

    Gets/sets directionality (rtl/ltr) of the document.

* Document.domain Read only 

    Returns the domain of the current document.


##Event handlers



##Methods

This interface also inherits from the Node and EventTarget interfaces.

* Document.adoptNode()

    Adopt node from an external document.

* Document.captureEvents() 

    See Window.captureEvents.

* Document.caretPositionFromPoint()

    Gets the CaretPosition at or near the specified coordinates.

* Document.caretRangeFromPoint()

    Gets a Range object for the document fragment under the specified coordinates.

* Document.createAttribute()

    Creates a new Attr object and returns it.

* Document.createAttributeNS()

    Creates a new attribute node in a given namespace and returns it.

* Document.createCDATASection()

    Creates a new CDATA node and returns it.

* Document.createComment()

    Creates a new comment node and returns it.

* Document.createDocumentFragment()

    Creates a new document fragment.

* Document.createElement()

    Creates a new element with the given tag name.

* Document.createElementNS()

    Creates a new element with the given tag name and namespace URI.

* Document.createEntityReference() 

    Creates a new entity reference object and returns it.

* Document.createEvent()

    Creates an event object.

* Document.createNodeIterator()

    Creates a NodeIterator object.

* Document.createProcessingInstruction()

    Creates a new ProcessingInstruction object.

* Document.createRange()

    Creates a Range object.

* Document.createTextNode()

    Creates a text node.

* Document.createTouch()

    Creates a Touch object.

* Document.createTouchList()

    Creates a TouchList object.

* Document.createTreeWalker()

    Creates a TreeWalker object.


* Document.getElementsByClassName()

    Returns a list of elements with the given class name.

* Document.getElementsByTagName()

    Returns a list of elements with the given tag name.

* Document.getElementsByTagNameNS()

    Returns a list of elements with the given tag name and namespace.


* Document.normalizeDocument() 

    Replaces entities, normalizes text nodes, etc.

* Document.registerElement() 

    Registers a web component.

* Document.releaseCapture() 

    Releases the current mouse capture if it's on an element in this document.

* Document.releaseEvents() 

    See Window.releaseEvents().

* document.getElementById(String id)

    Returns an object reference to the identified element.

* document.querySelector(String selector) 

    Returns the first Element node within the document, in document order, that matches the specified selectors.

* document.querySelectorAll(String selector) 

    Returns a list of all the Element nodes within the document that match the specified selectors.



##Extension for HTML documents

The Document interface for HTML documents inherit from the HTMLDocument interface or, since HTML5, 
 is extended for such documents:

* document.clear()

    In majority of modern browsers, including recent versions of Firefox and Internet Explorer, this method does nothing.

* document.close()

    Closes a document stream for writing.

* document.open()

    Opens a document stream for writing.

* document.write(String text)

    Writes text in a document.

* document.writeln(String text)

    Writes a line of text in a document.

```js
document.open();
document.write("<h1>Out with the old - in with the new!</h1>");
document.close();
```