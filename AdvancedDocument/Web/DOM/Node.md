[Node](https://developer.mozilla.org/en-US/docs/Web/API/Node)

A Node is an interface from which a number of DOM types inherit, 
and allows these various types to be treated (or tested) similarly.

##Properties

Inherits properties from its parents EventTarget

* Node.baseURI Read only 

    Returns a DOMString representing the base URL. The concept of base URL changes from one language to another; 
    in HTML, it corresponds to the protocol, the domain name and the directory structure, that is all until the last '/'.

* Node.childNodes 

    Returns a live NodeList containing all the children of this node.
    NodeList being live means that if the children of the Node change, the NodeList object is automatically updated.

* Node.firstChild Read only 

    Returns a Node representing the first direct child node of the node, or null if the node has no child.

* Node.lastChild Read only 

    Returns a Node representing the last direct child node of the node, or null if the node has no child.

* Node.localName  Read only 

    Returns a DOMString representing the local part of the qualified name of an element. 
    In Firefox 3.5 and earlier, the property upper-cases the local name for HTML elements (but not XHTML elements). In later versions, 
    this does not happen, so the property is in lower case for both HTML and XHTML. 
     Though recent specifications require localName to be defined on the Element interface, 
    Gecko-based browsers still implement it on the Node interface.


* Node.nextSibling Read only 

    Returns a Node representing the next node in the tree, or null if there isn't such node.

* Node.nodeName Read only 

    Returns a DOMString containing the name of the Node. The structure of the name will differ with the name type
    . E.g. An HTMLElement will contain the name of the corresponding tag, like 'audio' for an HTMLAudioElement, 
    a Text node will have the '#text' string, or a Document node will have the '#document' string.

* Node.nodePrincipal 

    A nsIPrincipal representing the node principal.

* Node.nodeType Read only 

    Returns an unsigned short representing the type of the node. Possible values are: 

*　Node.nodeValue

    Is a DOMString representing the value of an object. For most Node types, this returns null and any set operation is ignored. 
    For nodes of type TEXT_NODE (Text objects), COMMENT_NODE (Comment objects), and PROCESSING_INSTRUCTION_NODE (ProcessingInstruction objects), 
    the value corresponds to the text data contained in the object.

* Node.ownerDocument Read only 

    Returns the Document that this node belongs to. If no document is associated with it, returns null.

* Node.parentNode Read only 

    Returns a Node that is the parent of this node. If there is no such node, like if this node is the top of the tree or
     if doesn't participate in a tree, this property returns null.

* Node.parentElement Read only 

    Returns an Element that is the parent of this node. If the node has no parent, or if that parent is not an Element, this property returns null.

* Node.previousSibling Read only 

    Returns a Node representing the previous node in the tree, or null if there isn't such node.

* Node.rootNode Read only 

    Returns a Node object representing the topmost node in the tree, or the current node if it's the topmost node in the tree.
     This is found by walking backward along Node.parentNode until the top is reached.

* Node.textContent

    Is a DOMString representing the textual content of an element and all its descendants.

##Methods

Inherits methods from its parent, EventTarget.[1]

* Node.appendChild()

    Insert a Node as the last child node of this element.

*　Node.cloneNode()

    Clone a Node, and optionally, all of its contents. By default, it clones the content of the node.

* Node.hasAttributes() 

    Returns a Boolean indicating if the element has any attributes, or not.

* Node.hasChildNodes()

    Returns a Boolean indicating if the element has any child nodes, or not.

* Node.insertBefore()

    Inserts the first Node given in a parameter immediately before the second, child of this element, Node.

* Node.removeChild()

    Removes a child node from the current element, which must be a child of the current node.

* Node.replaceChild()

    Replaces one child Node of the current one with the second one given in parameter.

* Node.setUserData()  

    Allows a user to attach, or remove, DOMUserData to the node.