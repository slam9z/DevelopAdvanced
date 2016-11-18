[Bootstrap - how to set up fixed width for <td>?](http://stackoverflow.com/questions/15115052/bootstrap-how-to-set-up-fixed-width-for-td)

##answer

For Bootstrap 3.0:
With twitter bootstrap 3 use: class="col-md-*" where * is a number of columns of width.

```html
<tr class="something">
    <td class="col-md-2">A</td>
    <td class="col-md-3">B</td>
    <td class="col-md-6">C</td>
    <td class="col-md-1">D</td>
</tr>
```

For Bootstrap 2.0:
With twitter bootstrap 2 use: class="span*" where * is a number of columns of width.

```html
<tr class="something">
    <td class="span2">A</td>
    <td class="span3">B</td>
    <td class="span6">C</td>
    <td class="span1">D</td>
</tr>
```

** If you have <th> elements set the width there and not on the <td> elements.