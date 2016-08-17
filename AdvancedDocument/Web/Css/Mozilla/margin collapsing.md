[Mastering margin collapsing](https://developer.mozilla.org/en-US/docs/Web/CSS/CSS_Box_Model/Mastering_margin_collapsing)

复杂的东西

##Adjacent siblings

The margins of adjacent siblings are collapsed (except when the later sibling needs to be cleared past floats). 


##Parent and first/last child

If there is no border, padding, inline content, or clearance to separate the margin-top of a block 
from the margin-top of its *first child block*, 
or no border, padding, inline content, height, min-height, or max-height to separate the margin-bottom of a block from the margin-bottom of
its last child, then those margins collapse. The collapsed margin ends up outside the parent.


##Empty blocks

If there is no border, padding, inline content, height, or min-height to separate a block's margin-top from its margin-bottom, 
then its top and bottom margins collapse.


Margins of *floating* and *absolutely positioned* elements never collapse.