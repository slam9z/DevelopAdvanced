[Calling a function inside a jQuery plugin from outside](http://stackoverflow.com/questions/18185956/calling-a-function-inside-a-jquery-plugin-from-outside)



First thing first we need to understand each step in building a jQuery plugin, its like build a javascript plugin (class) but we have in addition to it a jQuery class.

```js
//We start with a function and pass a jQuery class to it as a 
//parameter $ to avoid the conflict with other javascript 
//plugins that uses '$ as a name
(function($){
    //We now append our function to the jQuery namespace, 
    //with an option parameter
    $.fn.myplugin = function(options) {
        //the settings parameter will be our private parameter to our function
        //'myplugin', using jQuery.extend append 'options' to our settings
        var settings = jQuery.extend({
            param:'value',
        }, options);
        //Define a reference to our function myplugin which it's 
        //part of jQuery namespace functions, so we can use later
        //within inside functions
        var $jquery=this;

        //Define an output object that will work as a reference
        //for our function
        var output={
            //Setup our plugin functions as an object elements
            'function1':function(param){
                //Call jQuery reference that goes through jQuery selector
                $jquery.each(function(){
                    //Define a reference of each element of jQuery 
                    //selector elements
                    var _this=this;
                });
                //This steps is required if you want to call nested
                //functions like jQuery.
                return output;
            },
            //If we want to make our plugin to do a specific operations
            //when called, we define a function for that
            'init':function(){
                $jquery.each(function(){
                    var _this=this;
                    //Note that _this param linked to each jQuery 
                    //functions not element, thus wont behave like 
                    //jQuery function.
                    //And for that we set a parameter to reference the
                    //jQuery element
                    _this.$this=$(this);

                    //We can define a private function for 'init'
                    //function
                    var privatefun=function(){}
                    privatefun();

                    //We can now do jQuery stuffs on each element
                    _this.$this.on('click',function(){
                        //jQuery related stuffs
                    });
                });
                //We can call whatever function we want or parameter
                //that belongs to our plugin
                output.function1("value");
            }
        };
        //Our output is ready, if we want our plugin to execute a
        //function whenever it called we do it now
        output.init();

        //And the final critical step, return our object output to
        //the plugin
        return output;
    };
//Pass the jQuery class so we can use it inside our plugin 'class'
})(jQuery);
```

Using our function now is very easy

```html
<div class="plugintest">
    <span>1</span>
    <span>2</span>
    <span>3</span>
    <span>4</span>
</div>

<script>
    $(function(){
        var myplugin=$(".plugintest > span").myplugin({
            param:'somevalue'
        });
        myplugin.function1(1).function1(2).function1(3);
    });
</script>
```

In short, jQuery plugins and any Javascript plugins are simply about parameters scope.

Fiddle version https://jsfiddle.net/eiadsamman/a59uwmga/
