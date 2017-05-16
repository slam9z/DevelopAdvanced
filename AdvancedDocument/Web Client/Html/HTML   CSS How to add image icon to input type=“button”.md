[HTML / CSS How to add image icon to input type=“button”?](http://stackoverflow.com/questions/2920076/html-css-how-to-add-image-icon-to-input-type-button)


## answer

If you absolutely must use input, try this:

```css
background-image: url(...);
background-repeat: no-repeat;
background-position: <left|right>;
padding-<left|right>: <width of image>px;
```

It's usually a little easier to use a button with an img inside:

```html
<button type="submit"><img> Text</button>
```

However the browser implementations of button for submitting are inconsistent, as well as the fact that all button values are sent when button is used - which kills the "what button clicked" detection in a multi-submit form.
