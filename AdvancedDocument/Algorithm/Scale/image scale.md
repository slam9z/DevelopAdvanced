拉伸图片
 一般拉伸图片的时候需要，具体解决方案看image scale.md


##接口
```
/// <summary>
/// 一般拉伸图片的时候需要
/// </summary>
/// <returns></returns>
public static double GetScaling(
    int orignWidth, int orignHeight
    , int maxWidth, int minWidth
    , int maxHeight, int minHeight
    )
{

}
```

##数学转换

x是scale

1 y=w*x  y=(minw,maxw);
2 z=h*x  z=(minh,maxh);


求x的最大值。


###解决。

不知道怎么找方法，只能自己想了，不过应该也不难。
其实基本上也只有几个值啊。

x=(minw/h,maxw/h);  
x=(minh/w,maxh/w);
得
x=(minwScale, maxwScale);
x=(minhScale, maxhScale);

if(minwScale>maxhScale)
{
    scale=0；无解
}

if(minhScale>maxwScale)
{
    scale=0；无解
}



然后取并集，最大的一个。
