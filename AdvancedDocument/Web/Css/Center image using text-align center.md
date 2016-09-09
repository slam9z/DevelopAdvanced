[Center image using text-align center?](http://stackoverflow.com/questions/7055393/center-image-using-text-align-center)


##question

Is the property text-align: center; a good way to center an image using CSS?

```css
img {
    text-align: center;
}
```

##Best answer

That will not work as the text-align property applies to block containers, not inline elements, and img is an inline element.
 See [the W3C spec](http://www.w3.org/TR/CSS21/text.html#alignment-prop).

Use this instead:

```css
img.center {
    display: block;
    margin: 0 auto;
}
```