[How can I set the default value for an HTML <select> element?](http://stackoverflow.com/questions/3518002/how-can-i-set-the-default-value-for-an-html-select-element)


## answer

I thought that adding a "value" attribute set on the <select> element below would cause the <option> containing my provided "value" to be selected by default:

```html
<select name="hall" id="hall" value="3">
  <option>1</option>
  <option>2</option>
  <option>3</option>
  <option>4</option>
  <option>5</option>
</select>
```

However, this did not work as I had expected. How can I set which <option> element is selected by default?



## answer

Set selected="selected" for the option you want to be the default.

```html
<option selected="selected">
3
</option>
```
