[细说 Request[]与Request.Params[]](http://www.cnblogs.com/fish-li/archive/2011/12/06/2278463.html)

##回顾博客原文

这二个属性都可以让我们方便地根据一个KEY去【同时搜索】QueryString、Form、Cookies 或 ServerVariables这4个集合。 通常如果请求
是用GET方法发出的，那我们一般是访问QueryString去获取用户的数据，如果请求是用POST方法提交的， 我们一般使用Form去访问用户提交
的表单数据。而使用Params，Item可以让我们在写代码时不必区分是GET还是POST。 这二个属性唯一不同的是：Item是依次访问这4个集合，
找到就返回结果，而Params是在访问时，先将4个集合的数据合并到一个新集合(集合不存在时创建)， 然后再查找指定的结果。

为了更清楚地演示这们的差别，请看以下示例代码： 


##实现方式分析

前面的示例中，我演示了在访问Request[]与Request.Params[] 时得到了不同的结果。为什么会有不同的结果呢，我想我们还是先去看一下微软在
.net framework中的实现吧。 

首先，我们来看一下Request[]的实现，它是一个默认的索引器，实现代码如下： 

```
public string this[string key]
{
    get
    {
        string str = this.QueryString[key];
        if( str != null ) {
            return str;
        }
        str = this.Form[key];
        if( str != null ) {
            return str;
        }
        HttpCookie cookie = this.Cookies[key];
        if( cookie != null ) {
            return cookie.Value;
        }
        str = this.ServerVariables[key];
        if( str != null ) {
            return str;
        }
        return null;
    }
}
```

这段代码的意思是：根据指定的key，依次访问QueryString，Form，Cookies，ServerVariables这4个集合，如果在任意一个集合中找到了，就立即返回。 
Request.Params[]的实现如下： 

```cs
public NameValueCollection Params
{
    get
    {
        //if (HttpRuntime.HasAspNetHostingPermission(AspNetHostingPermissionLevel.Low))
        //{
        //    return this.GetParams();
        //}
        //return this.GetParamsWithDemand();

        // 为了便于理解，我注释了上面的代码，其实关键还是下面的调用。
        return this.GetParams();
    }
}
private NameValueCollection GetParams()
{
    if( this._params == null ) {
        this._params = new HttpValueCollection(0x40);
        this.FillInParamsCollection();
        this._params.MakeReadOnly();
    }
    return this._params;
}
private void FillInParamsCollection()
{
    this._params.Add(this.QueryString);
    this._params.Add(this.Form);
    this._params.Add(this.Cookies);
    this._params.Add(this.ServerVariables);
}
```

它的实现方式是：先判断_params这个Field成员是否为null，如果是，则创建一个集合，并把QueryString，Form，Cookies，
ServerVariables这4个集合的数据全部填充进来， 以后的查询都直接在这个集合中进行。 


我们可以看到，这是二个截然不同的实现方式。也就是因为这个原因，在某些特殊情况下访问它们得到的结果将会不一样。 
不一样的原因是：Request.Params[]创建了一个新集合，并合并了这4个数据源，遇到同名的key，自然结果就会不同了。 



##再谈NameValueCollection

本文一开始的示例中，为什么代码 ParamsValue = Request.Params["name"]; 得到的结果是：【abc,123】？ 
根据前面示例代码我们可以得知：abc这个值是由QueryString提供的，123这值是由Form提供的，最后由Request.Params[]合并在一起了就变成这个样子了。
 有没有人想过：为什么合起来就变成了这个样子了呢？ 

要回答这个问题，我们需要回顾一下Params的定义： 

```
public NameValueCollection Params
```

注意：它的类型是NameValueCollection 。MSDN对这个集合有个简单的说明： 

>此集合基于 NameObjectCollectionBase 类。但与 NameObjectCollectionBase 不同，该类在一个键下存储多个字符串值。 