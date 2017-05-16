[Two HTML elements with same id attribute: How bad is it really?](http://softwareengineering.stackexchange.com/questions/127178/two-html-elements-with-same-id-attribute-how-bad-is-it-really)


## ansewr1

Specification says UNIQUE

HTML 4.01 specification says ID must be document-wide unique.

HTML 5 specification says the same thing but in other words. It says that ID must be unique in its home subtree, which is basically the document if we read the definition of it.
Avoid duplication

But since HTML renderers are very forgiving when it comes to HTML rendering they permit duplicate IDs. This should be avoided if at all possible and strictly avoided when programmatically accessing elements by IDs in JavaScript. I'm not sure what getElementById function should return when several matching elements are found? Should it:

    return an error?
    return first matching element?
    return last matching element?
    return a set of matching elements?
    return nothing?

But even if browsers work reliably these days, nobody can guarantee this behavior in the future since this is against specification. That's why I recommend you never duplicate IDs within the same document.
