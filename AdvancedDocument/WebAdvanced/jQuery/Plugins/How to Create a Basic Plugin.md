[How to Create a Basic Plugin](http://learn.jquery.com/plugins/basic-plugin-creation/)

##Basic Plugin Authoring

Let's say we want to create a plugin that makes text within a set of retrieved elements green.
 All we have to do is add a function called greenify to $.fn and it will be available just like any other
 jQuery object method.

```js
 $.fn.greenify = function() {

    this.css( "color", "green" );

};

 

$( "a" ).greenify(); 
```

##Chaining

This works, but there are a couple of things we need to do for our plugin to survive in the real world. 


##Protecting the $ Alias and Adding Scope

The $ variable is very popular among JavaScript libraries, and if you're using another library with jQuery, 
you will have to make jQuery not use the $ with jQuery.noConflict(). However, this will break our plugin since 
it is written with the assumption that $ is an alias to the jQuery function. To work well with other plugins,
 and still use the jQuery $ alias, we need to put all of our code inside of an Immediately Invoked Function Expression, 
and then pass the function jQuery, and name the parameter $:

```js
 (function ( $ ) {

    $.fn.greenify = function() {

        this.css( "color", "green" );

        return this;

    };

}( jQuery ));
```


##Minimizing Plugin Footprint


##Accepting Options

As your plugins get more and more complex, it's a good idea to make your plugin customizable by accepting options.

