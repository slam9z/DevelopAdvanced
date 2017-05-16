
[jQuery Object doesn't support property or method trim() in IE](http://stackoverflow.com/questions/7719508/jquery-object-doesnt-support-property-or-method-trim-in-ie)


## answer

IE doesn't have a `string.trim()` method.

Instead, you can call jQuery's `$.trim(str)`.
