[Element](https://developer.mozilla.org/en-US/docs/Web/API/Element)

The Element interface represents an object of a *Document*. 
This interface describes methods and properties common to all kinds of elements. 
Specific behaviors are described in interfaces which inherit from Element but add additional functionality.
 For example, the *HTMLElement* interface is the base interface for HTML elements, while the [SVGElement](https://developer.mozilla.org/en-US/docs/Web/API/SVGElement)
 interface is the basis for all SVG elements.
Languages outside the realm of the Web platform, like XUL through the XULElement interface, also implement it.

##Properties

Inherits properties from its parents *Node*, and its own parent, *EventTarget*, and implements those of
 ParentNode, ChildNode, NonDocumentTypeChildNode, and Animatable.

* Element.attributes Read only 

    Returns a NamedNodeMap that lists all attributes associated with the element.
    key/value pair 

* ParentNode.childElementCount Read only 

    Is a Number representing the number of child nodes that are elements.

* ParentNode.children Read only 

    Is a live HTMLCollection containing all child elements of the element, as a collection.

* Element.classList Read only 

    Returns a DOMTokenList containing the list of class attributes.

* Element.className

    Is a DOMString representing the class of the element.

* ParentNode.firstElementChild Read only 

    Is a Element, the first direct child element of an element, or null if the element has no child elements.

* Element.id

    Is a DOMString representing the id of the element.

* Element.innerHTML

    Is a DOMString representing the markup of the element's content.

* ParentNode.lastElementChild Read only 

    Is a Element, the last direct child element of an element, or null if the element has no child elements

* Element.tagName Read only 

    Returns a String with the name of the tag for the given element.

##Methods

Inherits methods from its parents Node, and its own parent, EventTarget, and implements those of ParentNode,
 ChildNode, NonDocumentTypeChildNode, and Animatable.

* EventTarget.addEventListener()

    Registers an event handler to a specific event type on the element.

* Element.attachShadow()

    Attatches a shadow DOM tree to the specified element and returns a reference to its ShadowRoot.

* Element.animate() 

    A shortcut method to create and run an animation on an element. Returns the created Animation object instance.

* Element.closest() 

    Returns the Element, descendant of this element (or this element itself), 
    that is the closest ancestor of the elements selected by the selectors given in parameter.

* Element.createShadowRoot() 

    Creates a shadow DOM on on the element, turning it into a shadow host. Returns a ShadowRoot.

* EventTarget.dispatchEvent()

    Dispatches an event to this node in the DOM and returns a Boolean that indicates 
    that at least one handler has not canceled it.

* Element.find()

    ...

* Element.findAll()

    ...

* Element.getAnimations() 

    Returns an array of Animation objects currently active on the element.

* Element.getAttribute()

    Retrieves the value of the named attribute from the current node and returns it as an Object.

* Element.getAttributeNames()

 

* Element.getAttributeNS()

    Retrieves the value of the attribute with the specified name and namespace, from the current node and returns it as an Object.

* Element.getAttributeNode() 

    Retrieves the node representation of the named attribute from the current node and returns it as an Attr.

* Element.getAttributeNodeNS()

    Retrieves the node representation of the attribute with the specified name and namespace, from the current node and returns it as an Attr.

* Element.getBoundingClientRect()

    ...

* Element.getClientRects()

    Returns a collection of rectangles that indicate the bounding rectangles for each line of text in a client.

* Element.getDestinationInsertionPoints()  

    …

* Element.getElementsByClassName()

    Returns a live HTMLCollection that contains all descendant of the current element that posses the list of classes given in parameter.

* Element.getElementsByTagName()

    Returns a live HTMLCollection containing all descendant elements, of a particular tag name, from the current element.

* Element.getElementsByTagNameNS()

    Returns a live HTMLCollection containing all descendant elements, of a particular tag name and namespace, from the current element.

* Element.hasAttribute()

    Returns a Boolean indicating if the element has the specified attribute or not.

* Element.hasAttributeNS()

    Returns a Boolean indicating if the element has the specified attribute, in the specified namespace, or not.

* Element.hasAttributes()

    Returns a Boolean indicating if the element has one or more HTML attributes present.

* Element.insertAdjacentHTML  

    Parses the text as HTML or XML and inserts the resulting nodes into the tree in the position given.

* Element.matches() 

    Returns a Boolean indicating whether or not the element would be selected by the specified selector string.

* Element.querySelector()

    Returns the first Node which matches the specified selector string relative to the element.

* Element.querySelectorAll

    Returns a NodeList of nodes which match the specified selector string relative to the element.

* Element.releasePointerCapture

    Releases (stops) pointer capture that was previously set for a specific pointer event.

* ChildNode.remove() 

    Removes the element from the children list of its parent.

* Element.removeAttribute()

    Removes the named attribute from the current node.

* Element.removeAttributeNS()

    Removes the attribute with the specified name and namespace, from the current node.

* Element.removeAttributeNode() 

    Removes the node representation of the named attribute from the current node.

* EventTarget.removeEventListener()

    Removes an event listener from the element.

* Element.requestFullscreen() 

    Asynchronously asks the browser to make the element full-screen.

* Element.requestPointerLock() 

    Allows to asynchronously ask for the pointer to be locked on the given element.

* Element.scrollIntoView() 

    Scrolls the page until the element gets into the view.

* Element.setAttribute()

    Sets the value of a named attribute of the current node.

* Element.setAttributeNS()

    Sets the value of the attribute with the specified name and namespace, from the current node.

* Element.setAttributeNode()  

    Sets the node representation of the named attribute from the current node.

* Element.setAttributeNodeNS() 

    Setw the node representation of the attribute with the specified name and namespace, from the current node.
