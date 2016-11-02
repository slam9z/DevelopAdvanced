##getRange of undefined or null reference


```
SCRIPT5007: Unable to get property 'getRange' of undefined or null reference
ueditor.all.min.js (11,12214)
SCRIPT5007: Unable to get property 'getRange' of undefined or null reference
ueditor.all.min.js (10,3835)
```

一下又可以了，可能是js加载问题


 var img = editor.selection.getRange().getClosedNode();

 selection is  undefined

 
 
