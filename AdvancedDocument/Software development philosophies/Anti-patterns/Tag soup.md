[Tag soup](https://en.wikipedia.org/wiki/Tag_soup)

> 这个确实很恶心！讨厌不标准的写法。


In Web development, "tag soup" refers to syntactically or structurally incorrect HTML written for a web page. Because web browsers have historically treated HTML syntax or structural errors leniently, there has been little pressure for web developers to follow published standards, and therefore there is a need for all browser implementations to provide mechanisms to cope with the appearance of "tag soup", accepting and correcting for invalid syntax and structure where possible.

An HTML parser (part of a web browser) that is capable of interpreting HTML-like markup even if it contains invalid syntax or structure may be called a tag soup parser. All major web browsers currently have a tag soup parser for interpreting malformed HTML.

"Tag soup" encompasses many common authoring mistakes, such as malformed HTML tags, improperly nested HTML elements, and unescaped character entities (especially ampersands (&) and less-than signs (<)).

> I have used this term in my instruction for years to characterize the jumble of angle brackets acting like tags in HTML in pages that are accepted by browsers. Improper minimization, overlapping constructs ... stuff that looks like SGML markup but the creator didn't know or respect SGML rules for the HTML vocabulary. In effect a soupy collection of text and markup. [...] I've never seen the term defined anywhere.
> — G. Ken Holman, Re: [xml-dev] What is Tag Soup?, XML development mailing list, 11 Oct 2002.

The Markup Validation Service is a resource for web page authors to avoid creating tag soup.


## Overview

"Tag soup" is a term used to denigrate various practices in web authoring. Some of these (roughly ordered from most severe to least severe) include:

### Malformed markup where tags are improperly nested or incorrectly closed. For example, the following:

    <p>This is a malformed fragment of <em>HTML.</p></em>

### Invalid structure where elements are improperly nested according to the DTD for the document. Examples of this include nesting a "ul" element directly inside another "ul" element for any of the HTML 4.01 or XHTML DTDs.

### Use of proprietary or undefined elements and attributes instead of those defined in W3C recommendations.
