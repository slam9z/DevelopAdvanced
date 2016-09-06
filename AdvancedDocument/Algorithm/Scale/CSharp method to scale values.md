[C# method to scale values?](http://stackoverflow.com/questions/2675196/c-sharp-method-to-scale-values)


Use this formula

```
y=mx+c
where m = (255-0)/(244-13) and c= -13*m
```

So you have to just transform the array as such

```
 public double[] GetScaling(double[] arr, double min, double max)
{
    double m = (max-min)/(arr.Max()-arr.Min());
    double c = min-arr.Min()*m;
    var newarr=new double[arr.Length];
    for(int i=0; i< newarr.Length; i++)
       newarr[i]=m*arr[i]+c;
    return newarr;
}
```