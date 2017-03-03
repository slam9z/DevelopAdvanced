[CSS fixed width in a span](http://stackoverflow.com/questions/257505/css-fixed-width-in-a-span)

In an ideal world you'd achieve this simply using the following css

```css
<style type="text/css">

span {
  display: inline-block;
  width: 50px;
}

</style>
```

This works on all browsers apart from FF2 and below.