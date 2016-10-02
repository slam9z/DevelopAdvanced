[Best replacement for font tag in html](http://stackoverflow.com/questions/19280303/best-replacement-for-font-tag-in-html)


##Question

Since the font tag in HTML is being deprecated in HTML5 (and I understand why) is there a clean solution for applying certain attributes 
and styles to only portions of a paragraph text? I'm using JavaScript to parse an XML file that relies on the fact that the font tag allows
 portions of wrapping text to be formatted using class-based CSS. 
I realize the "anchor" (a) tag could also be used for this purpose, but that way seems very backwards and unnatural.

*EDIT*

When I asked this question (a couple years ago now) I was failing to understand that every DOM element falls into a display category, 
the two primary categories being:

* block - insists on taking up its own row
* inline - falls in line with other inline elements or text

HTML offers two generic container elements, each of which by default adheres to one of these display values; 
div for block display, and span for inline display.

The span element is the perfect way to designate a certain chunk of text and give it a unique style or ID because you 
can wrap it around part of a larger paragraph without breaking the selected contents into a new row.


##Best Answer

The span tag would be the best way.
Although inline CSS is typically not recommended, here is an example:
<p>
This is my <span style="font-weight:bold">paragraph</span>.
</p>
span and div are similar, but the div tag is a block element, so it will cause line-breaks.
 span is an inline tag that can be used inline with your text.