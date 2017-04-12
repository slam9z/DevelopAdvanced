[multiselect ](http://loudev.com/)


[jquery-ui-multiselect-widget/wiki](https://github.com/ehynds/jquery-ui-multiselect-widget/wiki)


## Retrieve all selected values?

The easiest way is to call val() on the select box:

```js
var values = $("select").val();
```

The same can be accomplished using the multiselect API. Call the getChecked method and map a new array:

```js
var array_of_checked_values = $("select").multiselect("getChecked").map(function(){
   return this.value;    
}).get();
```