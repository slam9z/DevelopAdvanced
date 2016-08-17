[ASP.NET MVC以ValueProvider为核心的值提供系统: DictionaryValueProvider ](http://www.cnblogs.com/artech/archive/2012/05/18/value-provider-02.html)

##一、DictionaryValueProvider<TValue>

DictionnaryValueProvider的类型全名为System.Web.Mvc.DictionaryValueProvider<TValue>，
如下面的代码片断所示，DictionaryValueProvider<TValue>实现了IEnumerableValueProvider和IValueProvider接口，
构造函数接受一个IDictionary<string, TValue>对象，该对象表示作为数据源的字典。
定义在DictionaryValueProvider<TValue>中所有方法的逻辑与定义在NameValueCollectionValueProvider中的同名方法并没有本质区别。

``` C#
public class DictionaryValueProvider<TValue> : IEnumerableValueProvider, IValueProvider
{
    public DictionaryValueProvider(IDictionary<string, TValue> dictionary, CultureInfo culture);
    public virtual bool ContainsPrefix(string prefix);
    public virtual IDictionary<string, string> GetKeysFromPrefix(string prefix);
    public virtual ValueProviderResult GetValue(string key);
}
```