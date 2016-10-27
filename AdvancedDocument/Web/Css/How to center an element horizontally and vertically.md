[How to center an element horizontally and vertically?](http://stackoverflow.com/questions/19461521/how-to-center-an-element-horizontally-and-vertically)

##answer

###Approach 1 - transform translateX/translateY:

Example Here / Full Screen Example
In supported browsers (most of them), you can use top: 50%/left: 50% in combination with translateX(-50%) translateY(-50%) to dynamically vertically/horizontally center the element.
.container {
    position: absolute;
    top: 50%;
    left: 50%;
    transform: translateX(-50%) translateY(-50%);
}

###Approach 2 - Flexbox method:
Example Here / Full Screen Example
In supported browsers, set the display of the targeted element to flex and use align-items: center for vertical centering and justify-content: center for horizontal centering. Just don't forget to add vendor prefixes for additional browser support (see example).
html, body, .container {
    height: 100%;
}
.container {
    display: flex;
    align-items: center;
    justify-content: center;
}

###Approach 3 - table-cell/vertical-align: middle:
Example Here / Full Screen Example
In some cases, you will need to ensure that the html/body element's height is set to 100%.
For vertical alignment, set the parent element's width/height to 100% and add display: table. Then for the child element, change the display to table-cell and add vertical-align: middle.
For horizontal centering, you could either add text-align: center to center the text and any other inline children elements. Alternatively, you could use margin: 0 auto, assuming the element is block level.
html, body {
    height: 100%;
}
.parent {
    width: 100%;
    height: 100%;
    display: table;
    text-align: center;
}
.parent > .child {
    display: table-cell;
    vertical-align: middle;
}

###Approach 4 - Absolutely positioned 50% from the top with displacement:
Example Here / Full Screen Example
This approach assumes that the text has a known height - in this instance, 18px. Just absolutely position the element 50% from the top, relative to the parent element. Use a negative margin-top value that is half of the element's known height, in this case - -9px.
html, body, .container {
    height: 100%;
}
.container {
    position: relative;
    text-align: center;
}
.container > p {
    position: absolute;
    top: 50%;
    left: 0;
    right: 0;
    margin-top: -9px;
}

###Approach 5 - The line-height method (Least flexible - not suggested):
Example Here
In some cases, the parent element will have a fixed height. For vertical centering, all you have to do is set a line-height value on the child element equal to the fixed height of the parent element.
Though this solution will work in some cases, it's worth noting that it won't work when there are multiple lines of text - like this.
.parent {
    height: 200px;
    width: 400px;
    text-align: center;
}
.parent > .child {
    line-height: 200px;
}


##answer


The best way to center a box both vertically and horizontally, is to use two containers :

###The outher container :
should have display: table;

###The inner container :
should have display: table-cell;
should have vertical-align: middle;
should have text-align: center;

###The content box :
should have display: inline-block;
should adjust the horizontal text-alignment, unless you want text to be centered
Demo :

```css
body {
    margin : 0;
}

.outer-container {
    display: table;
    width: 80%;
    height: 120px;
    background: #ccc;
}

.inner-container {
    display: table-cell;
    vertical-align: middle;
    text-align: center;
}

.centered-content {
    display: inline-block;
    text-align: left;
    background: #fff;
    padding : 20px;
    border : 1px solid #000;
}

```

```html
<div class="outer-container">
   <div class="inner-container">
     <div class="centered-content">
        Center this!
     </div>
   </div>
</div>
```

Run code snippetExpand snippet

See also this Fiddle!

###Centering in the middle of the page:

To center your content in the middle of your page, add the following to your outher container :

* position : absolute;
* width: 100%;
* height: 100%;

Here's a demo for that :

```css
body {
    margin : 0;
}

.outer-container {
    position : absolute;
    display: table;
    width: 100%;
    height: 100%;
    background: #ccc;
}

.inner-container {
    display: table-cell;
    vertical-align: middle;
    text-align: center;
}

.centered-content {
    display: inline-block;
    text-align: left;
    background: #fff;
    padding : 20px;
    border : 1px solid #000;
}
```

```html
<div class="outer-container">
   <div class="inner-container">
     <div class="centered-content">
        Center this!
     </div>
   </div>
</div>
```