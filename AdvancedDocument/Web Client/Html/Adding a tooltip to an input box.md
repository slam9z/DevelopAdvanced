[Adding a tooltip to an input box](http://stackoverflow.com/questions/19480010/adding-a-tooltip-to-an-input-box)


## answer
	

I know this is a question regarding the CSS.Tooltips library. However, for anyone else came here resulting from google search "tooltip for input box" like I did, here is the simplest way:

```html
<input title="This is the text of the tooltip" value="44"/>
```
shareimprove this answer
	
	
   	 
	
it doesn't work for me in Chrome – Mladen Adamovic Feb 4 at 11:23
   	 
	
There seems to be a bug in some of the older chrome versions. Try to use alt and title together. see stackoverflow.com/questions/10391327/… – user227353 Feb 6 at 17:30 