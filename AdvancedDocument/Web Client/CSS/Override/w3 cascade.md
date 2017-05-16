[Cascade ](https://www.w3.org/TR/CSS2/cascade.html)

[Cascading ](https://www.w3.org/TR/CSS2/cascade.html#cascade)



* * *

# <a name="q6.0">6 Assigning property values, Cascading, and Inheritance</a>



**Contents**

*   [6.1 Specified, computed, and actual values](cascade.html#value-stages)

        *   [6.1.1 <span class="index-def" title="specified value"> Specified values</span>](cascade.html#specified-value)
    *   [6.1.2 <span class="index-def" title="computed value"> Computed values</span>](cascade.html#computed-value)
    *   [6.1.3 <span class="index-def" title="used value"> Used values</span>](cascade.html#used-value)
    *   [6.1.4 <span class="index-def" title="actual value"> Actual values</span>](cascade.html#actual-value)
*   [6.2 Inheritance](cascade.html#inheritance)

        *   [6.2.1 The <span class="index-def" title="inherit, definition of">'inherit'</span> value](cascade.html#value-def-inherit)
*   [6.3 The @import rule](cascade.html#at-import)
*   [6.4 The cascade](cascade.html#cascade)

        *   [6.4.1 Cascading order](cascade.html#cascading-order)
    *   [6.4.2 !important rules](cascade.html#important-rules)
    *   [6.4.3 Calculating a selector's specificity](cascade.html#specificity)
    *   [6.4.4 Precedence of non-CSS presentational hints](cascade.html#preshint)

<!-- warning -->
 <input id="annoying-warning" title="hide note" type="checkbox"> <label for="annoying-warning">(hide)</label>  

**Note:** Several sections of this specification have been updated by other specifications. Please, see ["Cascading Style Sheets (CSS) — The Official Definition"](https://www.w3.org/TR/CSS/#css) in the latest <cite>CSS Snapshot</cite> for a list of specifications and the sections they replace. 

The CSS Working Group is also developing [CSS level&nbsp;2 revision&nbsp;2 (CSS&nbsp;2.2).](http://www.w3.org/TR/CSS22/) 
 
<!-- /warning -->

## 6.1 <a name="value-stages">Specified, computed, and actual values</a>

Once a user agent has parsed a document and constructed a [document tree](conform.html#doctree), it must assign, for
every element in the tree, a value to every property that applies to the
target [media type](media.html). 

The final value of a property is the result of a four-step
calculation: the value is determined through specification (the
"specified value"), then resolved into a value that is used for
inheritance (the "computed value"), then converted into an absolute
value if necessary (the "used value"), and finally transformed
according to the limitations of the local environment (the "actual
value").

### 6.1.1 <span class="index-def" title="specified value">
<a name="specified-value">Specified values</a></span>

User agents must first assign a specified value to each property based
on the following mechanisms (in order of precedence):

1.  If the [cascade](#cascade) results in a value, use it.

2.  Otherwise, if the property is [inherited](#inheritance) and the element is not the root of the <a>document tree</a>, use the computed value of the parent element.

3.  Otherwise use the property's <a name="x1"><span class="index-def" title="initial value">initial value</span></a>. The initial value of each property is indicated in the property's definition.

### 6.1.2 <span class="index-def" title="computed value">
<a name="computed-value">Computed values</a></span>

Specified values are resolved to computed values during the cascade;
for example URIs are made absolute and 'em' and 'ex'
units are computed to pixel or absolute lengths. Computing a value
never requires the user agent to render the document.

The computed value of URIs that the UA cannot resolve to absolute
URIs is the specified value.

The computed value of a
property is determined as specified by the Computed Value line in the
definition of the
property. See the section on [inheritance](#inheritance)
for the definition of computed values when the specified value is
'inherit'.

The computed value exists even when the property does not apply, as
defined by the ['Applies To'](about.html#applies-to) line.
However, some
properties may define the computed value of a property for an element
to depend on whether the property applies to that element.

### 6.1.3 <span class="index-def" title="used value">
<a name="used-value">Used values</a></span>

Computed values are processed as far as possible without formatting
the document. Some values, however, can only be determined when the
document is being laid out. For example, if the width of an element is
set to be a certain percentage of its containing block, the width
cannot be determined until the width of the containing block has been
determined. The <dfn id="usedValue">used value</dfn> is the result of
taking the computed value and resolving any remaining dependencies
into an absolute value.

### 6.1.4 <span class="index-def" title="actual value">
<a name="actual-value">Actual values</a></span>

A used value is in principle the value used for rendering, but a
user agent may not be able to make use of the value in a given
environment. For example, a user agent may only be able to render
borders with integer pixel widths and may therefore have to
approximate the computed width, or the user agent may be forced to
use only black and white shades instead of full color. The actual
value is the used value after any approximations have been applied.

## 6.2 <a name="inheritance">Inheritance</a>

Some values are inherited by the children of an element in the [document tree](conform.html#doctree), as described [above](#specified-value). Each property [defines](about.html#property-defs) whether it is inherited or
not.



Suppose there is an H1 element with an emphasizing element (EM)
inside:

<pre>&lt;H1&gt;The headline &lt;EM&gt;is&lt;/EM&gt; important!&lt;/H1&gt;
</pre>

If no color has been assigned to the EM element, the emphasized
"is" will inherit the color of the parent element, so if H1 has the
color blue, the EM element will likewise be in blue.



When inheritance occurs, elements inherit computed values. The
computed value from the parent element becomes both the specified
value and the computed value on the child.



Example(s):

For example, given the following style sheet:

<pre>body { font-size: 10pt }
h1 { font-size: 130% }
</pre>

and this document fragment:

<pre class="html-example">&lt;BODY&gt;
  &lt;H1&gt;A &lt;EM&gt;large&lt;/EM&gt; heading&lt;/H1&gt;
&lt;/BODY&gt;
</pre>

the <span class="prop-inst-font-size">'font-size'</span> property
for the H1 element will have the computed value '13pt' (130% times
10pt, the parent's value). Since the computed value of [<span class="propinst-font-size">'font-size'</span>](fonts.html#propdef-font-size) is inherited, the EM
element will have the computed value '13pt' as well.  If the
user agent does not have the 13pt font available, the
actual value of [<span class="propinst-font-size">'font-size'</span>](fonts.html#propdef-font-size)
for both H1 and EM might be, for example, '12pt'.



Note that inheritance follows the document tree and is
not intercepted by <span class="index-inst">[anonymous boxes.](visuren.html#box-gen)</span>

### 6.2.1 The <span class="index-def" title="inherit, definition
of"><a name="value-def-inherit">'inherit'</a></span>
value

Each property may also have a cascaded value of 'inherit', which
means that, for a given element, the property takes the same specified
value as the property for the element's parent. The 'inherit' value
can be used to enforce inheritance of values, and it can also be used on
properties that are not normally inherited.

If the 'inherit' value is set on the root element, the property is
assigned its initial value.



Example(s):

In the example below, the [<span class="propinst-color">'color'</span>](colors.html#propdef-color) and [<span class="propinst-background">'background'</span>](colors.html#propdef-background) properties are set on
the BODY element. On all other elements, the 'color' value will be
inherited and the background will be transparent. If these rules are
part of the user's style sheet, black text on a white background will
be enforced throughout the document.

<pre>body {
  color: black !important; 
  background: white !important;
}

* { 
  color: inherit !important; 
  background: transparent !important;
}
</pre>



## 6.3 <a name="at-import">The @import rule</a>

The <a name="x7"><span class="index-def" title="@import"><dfn>'@import'</dfn></span></a> rule allows users to
import style rules from other style sheets. In CSS&nbsp;2.1, any
@import rules must precede all other rules (except the @charset rule,
if present). See the [section on
parsing](syndata.html#at-rules) for when user agents must ignore @import rules. The
'@import' keyword must be followed by the URI of the style sheet to
include. A string is also allowed; it will be interpreted as if it had
url(...) around it.



Example(s):

The following lines are equivalent in meaning and illustrate both
'@import' syntaxes (one with "url()" and one with a bare string):

<pre>@import "mystyle.css";
@import url("mystyle.css");
</pre>


So that user agents can avoid retrieving resources for unsupported
[media types](media.html), authors may specify
media-dependent <a name="x8"><span class="index-inst" title="@import">@import</span></a> rules.  These <a name="x9"><span class="index-def" title="conditional import|media-dependent import">conditional
imports</span></a> specify comma-separated media types after the URI.



Example(s):

The following rules illustrate how @import rules can be made media-dependent:

<pre>@import url("fineprint.css") print;
@import url("bluish.css") projection, tv;
</pre>


In the absence of any media types, the import is
unconditional. Specifying 'all' for the medium has the same effect.
The import only takes effect if the target medium matches the media
list.

A target medium matches a media list if one of the items in the
media list is the target medium or 'all'.

Note that Media Queries [<span class="informref">[MEDIAQ]</span>](refs.html#ref-MEDIAQ) extends the syntax of
media lists and the definition of matching.

When the same style sheet is imported or linked to a document in
multiple places, user agents must process (or act as though they do)
each link as though the link were to a separate style sheet.

## 6.4 <a name="cascade">The cascade</a>

Style sheets may have three different origins: author, user, and
user agent.

*   **Author**. The author specifies style sheets
for a source document according to the conventions of the document
language. For instance, in HTML, style sheets may be included in the
document or linked externally.

*   **User**: The user may be able to specify style
information for a particular document. For example, the user may
specify a file that contains a style sheet or the user agent may
provide an interface that generates a user style sheet (or behaves as
if it did).

*   **User agent**: [Conforming user agents](conform.html#conformance) must apply
a <span class="index-def" title="default style sheet"><a name="default-style-sheet"><dfn>default style sheet</dfn></a></span>
(or behave as if they did). A user agent's default style sheet should
present the
elements of the document language in ways that satisfy general
presentation expectations for the document language (e.g., for visual
browsers, the EM element in HTML is presented using an italic
font). See [A sample style sheet for HTML
](sample.html) for a recommended default style sheet for HTML documents.

    Note that the user may modify system settings (e.g.,system colors) that affect the default style sheet. However, some useragent implementations make it impossible to change the values in thedefault style sheet.

Style sheets from these three origins will overlap in scope, and
they interact according to the cascade.

The CSS <a name="x12"><span class="index-def" title="cascade">cascade</span></a>
assigns a weight to each style rule. When several rules apply, the one
with the greatest weight takes precedence.

By default, rules in author style sheets have more weight than
rules in user style sheets. Precedence is reversed, however, for
"!important" rules. All user and author rules have more weight
than rules in the UA's default style sheet.

### 6.4.1 <a name="cascading-order">Cascading order</a>

To find the value for an element/property combination, user agents
must apply the following sorting order:

1.  Find all declarations that apply to the element and property in
      question, for the target [media type](media.html).
      Declarations apply if the associated selector [matches](selector.html) the element in question and the
      target medium matches the media list on all @media rules
      containing the declaration and on all links on the path through
      which the style sheet was reached.

2.  Sort  according to importance (normal or important)
and origin (author, user, or user agent). In ascending order of
precedence:

        1.  user agent declarations

        2.  user normal declarations

        3.  author normal declarations

        4.  author important declarations

        5.  user important declarations

3.  Sort rules with the same importance and origin by [specificity](#specificity)      of selector: more specific
      selectors will override more general ones.  Pseudo-elements and
      pseudo-classes are counted as normal elements and classes,
      respectively.

4.  Finally, sort by order specified: if two declarations have the
    same weight, origin and specificity, the latter specified wins.
    Declarations in imported style sheets are considered to be before any
    declarations in the style sheet itself.

Apart from the "!important" setting on individual declarations,
this strategy gives author's style sheets higher weight than those of
the reader. User agents must give the user the ability to turn off the
influence of specific author style sheets, e.g., through a pull-down
menu. Conformance to UAAG 1.0 checkpoint 4.14 satisfies this condition [<span class="normref">[UAAG10]</span>](refs.html#ref-UAAG10).

### 6.4.2 <a name="important-rules">!important rules</a>

CSS attempts to create a balance of power between author
and user style sheets. By default, rules in an author's style
sheet override those in a user's style sheet (see cascade
rule 3). 

However, for balance, an "!important" declaration (the delimiter token 
"!" and keyword "important" follow the declaration) takes precedence over 
a normal declaration.  Both author and user style sheets may contain
"!important" declarations, and user "!important" rules override author
"!important" rules. This CSS feature improves accessibility
of documents by giving users with special requirements (large
fonts, color combinations, etc.) control over presentation.

Declaring a <a name="x13"><span class="index-inst" title="shorthand
property">shorthand property</span></a> (e.g., [<span class="propinst-background">'background'</span>](colors.html#propdef-background)) to be "!important" is
equivalent to declaring all of its sub-properties to be "!important".



Example(s):

The first rule in the user's style sheet in the following example
contains an "!important" declaration, which overrides the corresponding
declaration in the author's style sheet.  The second declaration
will also win due to being marked "!important". However, the third
rule in the user's style sheet is not "!important" and will therefore
lose to the second rule in the author's style sheet (which happens to
set style on a shorthand property). Also, the third author rule will
lose to the second author rule since the second rule is
"!important". This shows that "!important" declarations have a
function also within author style sheets.

<pre>/* From the user's style sheet */
p { text-indent: 1em ! important }
p { font-style: italic ! important }
p { font-size: 18pt }

/* From the author's style sheet */
p { text-indent: 1.5em !important }
p { font: normal 12pt sans-serif !important }
p { font-size: 24pt }
</pre>


### 6.4.3 <a name="specificity">Calculating a selector's specificity</a>

A selector's specificity is calculated as follows:

*   count 1 if the declaration is from is a 'style' attribute rather
than a rule with a
selector, 0 otherwise (= a) (In HTML, values of an element's "style"
attribute are style sheet rules. These rules have no selectors, so
a=1, b=0, c=0, and d=0.)

*   count the number of ID attributes in the selector (= b)

*   count the number of other attributes and pseudo-classesin the selector (= c)

*   count the number of element names and pseudo-elements in the selector (= d)

The specificity is based only on the form of the selector. In
particular, a selector of the form "[id=p33]" is counted as an
attribute selector (a=0, b=0, c=1, d=0), even if the id attribute is
defined as an "ID" in the source document's DTD.

Concatenating the four numbers a-b-c-d (in a number system with a large
base) gives the specificity. 



Example(s):

Some examples:

<pre> *             {}  /* a=0 b=0 c=0 d=0 -&gt; specificity = 0,0,0,0 */
 li            {}  /* a=0 b=0 c=0 d=1 -&gt; specificity = 0,0,0,1 */
 li:first-line {}  /* a=0 b=0 c=0 d=2 -&gt; specificity = 0,0,0,2 */
 ul li         {}  /* a=0 b=0 c=0 d=2 -&gt; specificity = 0,0,0,2 */
 ul ol+li      {}  /* a=0 b=0 c=0 d=3 -&gt; specificity = 0,0,0,3 */
 h1 + *[rel=up]{}  /* a=0 b=0 c=1 d=1 -&gt; specificity = 0,0,1,1 */
 ul ol li.red  {}  /* a=0 b=0 c=1 d=3 -&gt; specificity = 0,0,1,3 */
 li.red.level  {}  /* a=0 b=0 c=2 d=1 -&gt; specificity = 0,0,2,1 */
 #x34y         {}  /* a=0 b=1 c=0 d=0 -&gt; specificity = 0,1,0,0 */
 style=""          /* a=1 b=0 c=0 d=0 -&gt; specificity = 1,0,0,0 */
</pre>




<pre>&lt;HEAD&gt;
&lt;STYLE type="text/css"&gt;
  #x97z { color: red }
&lt;/STYLE&gt;
&lt;/HEAD&gt;
&lt;BODY&gt;
&lt;P ID=x97z style="color: green"&gt;
&lt;/BODY&gt;
</pre>

In the above example, the color of the P element would be
green. The declaration in the "style" attribute will override the one in
the STYLE element because of cascading rule 3, since it has a higher
specificity.



### 6.4.4 <a name="preshint">Precedence of non-CSS presentational hints</a>

The UA may choose to honor presentational attributes in an HTML source
document. If so, these attributes are translated to the corresponding
CSS rules with specificity equal to 0, and are treated as if they were
inserted at the start of the author style sheet. They may therefore be
overridden by subsequent style sheet rules. In a transition phase,
this policy will make it easier for stylistic attributes to coexist
with style sheets.

For HTML, any attribute that is not in the following list should be
considered presentational: abbr, accept-charset, accept, accesskey,
action, alt, archive, axis, charset, checked, cite, class, classid,
code, codebase, codetype, colspan, coords, data, datetime, declare,
defer, dir, disabled, enctype, for, headers, href, hreflang,
http-equiv, id, ismap, label, lang, language, longdesc, maxlength,
media, method, multiple, name, nohref, object, onblur, onchange,
onclick, ondblclick, onfocus, onkeydown, onkeypress, onkeyup, onload,
onload, onmousedown, onmousemove, onmouseout, onmouseover, onmouseup,
onreset, onselect, onsubmit, onunload, onunload, profile, prompt,
readonly, rel, rev, rowspan, scheme, scope, selected, shape, span,
src, standby, start, style, summary, title, type (except on LI, OL and 
UL elements), usemap, value, valuetype, version.

For other languages, all document language-based styling must be
translated to the corresponding CSS and either enter the cascade at
the user agent level or, as with HTML presentational hints, be treated
as author level rules with a specificity of zero placed at the start
of the author style sheet.



Example(s):

The following user style sheet would override the font weight of
'b' elements in all documents, and the color of 'font'
elements with color attributes in XML documents. It would not affect
the color of any 'font' elements with color attributes in HTML
documents:

<pre>b { font-weight: normal; }
font[color] { color: orange; }
</pre>

The following, however, would override the color of font elements in all documents:

<pre>font[color] { color: orange ! important; }
</pre>


* * *



[previous](selector.html) &nbsp;
[next](media.html) &nbsp;
[contents](cover.html#minitoc) &nbsp;
[properties](propidx.html) &nbsp;
[index](indexlist.html) &nbsp;

