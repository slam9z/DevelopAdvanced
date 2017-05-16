[table style](http://getbootstrap.com/css/#tables)

##table class

```
table           table-striped   table-bordered
table-bordered  table-hover     table-condensed

```

##Basic example
For basic styling—light padding and only horizontal dividers—add the base class .table to any <table>. 
It may seem super redundant, but given the widespread use of tables for other plugins like calendars
and date pickers, we've opted to isolate our custom table styles.

```html
<table class="table">
  ...
</table>
```

##Striped rows

Use .table-striped to add zebra-striping to any table row within the <tbody>.
Cross-browser compatibility
Striped tables are styled via the :nth-child CSS selector, which is not available in Internet Explorer 8.

```html
<table class="table table-striped">
  ...
</table>
```

##Bordered table

Add .table-bordered for borders on all sides of the table and cells.


```html
<table class="table table-bordered">
  ...
</table>
```

##Hover rows

Add .table-hover to enable a hover state on table rows within a <tbody>.

```html
<table class="table table-hover">
  ...
</table>
```

##Condensed table

Add .table-condensed to make tables more compact by cutting cell padding in half.


```html          
<table class="table table-condensed">
  ...
</table>
```

##Contextual classes

Use contextual classes to color table rows or individual cells.

```html
<!-- On rows -->
<tr class="active">...</tr>
<tr class="success">...</tr>
<tr class="warning">...</tr>
<tr class="danger">...</tr>
<tr class="info">...</tr>

<!-- On cells (`td` or `th`) -->
<tr>
  <td class="active">...</td>
  <td class="success">...</td>
  <td class="warning">...</td>
  <td class="danger">...</td>
  <td class="info">...</td>
</tr>
```

Conveying meaning to assistive technologies
Using color to add meaning to a table row or individual cell only provides a visual indication, 
which will not be conveyed to users of assistive technologies – such as screen readers. Ensure 
that information denoted by the color is either obvious from the content itself (the visible 
text in the relevant table row/cell), or is included through alternative means, such as additional
 text hidden with the .sr-only class.

##Responsive tables

Create responsive tables by wrapping any .table in .table-responsive to make them scroll horizontally
 on small devices (under 768px). When viewing on anything larger than 768px wide, you will not see 
 any difference in these tables.

Vertical clipping/truncation
Responsive tables make use of overflow-y: hidden, which clips off any content that goes beyond
 the bottom or top edges of the table. In particular, this can clip off dropdown menus and other
  third-party widgets.
Firefox and fieldsets
Firefox has some awkward fieldset styling involving width that interferes with the responsive 
table. This cannot be overridden without a Firefox-specific hack that we don't provide in Bootstrap:

```css
@-moz-document url-prefix() {
  fieldset { display: table-cell; }
}
```

For more information, read this Stack Overflow answer.