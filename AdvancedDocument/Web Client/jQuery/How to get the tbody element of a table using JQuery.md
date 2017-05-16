[How to get the tbody element of a table using JQuery?](http://stackoverflow.com/questions/6763006/how-to-get-the-tbody-element-of-a-table-using-jquery)

## answer

```js
 $('#Table1 > tbody')
```

> will get direct children.
shareimprove this answer
	


Alternative:

```js
 $('#Table1').children('tbody') 
```