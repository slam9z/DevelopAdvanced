[Cast List<X> to List<Y>](http://stackoverflow.com/questions/5115275/cast-listx-to-listy)

## answer

If X can really be cast to Y you should be able to use

```cs
List<Y> listOfY = listOfX.Cast<Y>().ToList()
```


## answer
	

You can use `List.ConvertAll([Converter from Y to T]);`
