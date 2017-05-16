[How to align content of a div to the bottom?](http://stackoverflow.com/questions/585945/how-to-align-content-of-a-div-to-the-bottom)


Relative+absolute positioning is your best bet:

```html
  #header {
    position: relative;
    min-height: 150px;
  }
  #header-content {
    position: absolute;
    bottom: 0;
    left: 0;
  }

<div id="header">
  <h1>Title</h1>
  <div id="header-content">Some content</div>
</div>
```