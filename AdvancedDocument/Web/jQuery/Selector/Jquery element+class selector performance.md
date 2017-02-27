[Jquery element+class selector performance](http://stackoverflow.com/questions/11698935/jquery-elementclass-selector-performance)


	

Modern browsers expose a very efficient getElementsByClassName() method that returns the elements having a given class. That's why a single class selector is faster in your case.

To elaborate on your examples:

```js
$(".txtClass")                  =>  getElementsByClassName()

$("#childDiv2 .txtClass")       =>  getElementById(),
                                    then getElementsByClassName()

$("#childDiv2 > .txtClass")     =>  getElementById(),
                                    then iterate over children and check class

$("input.txtClass")             =>  getElementsByTagName(),
                                    then iterate over results and check class

$("#childDiv2 input.txtClass")  =>  getElementById(),
                                    then getElementsByTagName(),
                                    then iterate over results and check class
```                                    

As you can see, it's quite logical for the first form to be the fastest on modern browsers.
