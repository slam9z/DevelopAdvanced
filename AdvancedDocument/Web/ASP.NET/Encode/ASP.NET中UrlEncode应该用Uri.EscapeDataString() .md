[ASP.NET中UrlEncode应该用Uri.EscapeDataString() ](http://www.cnblogs.com/dudu/archive/2011/02/25/asp_net_UrlEncode.html)

这个问题比较坑比较有意思

##Solution

今天，茄子_2008反馈他博客中的“C++”标签失效。检查了一下代码，生成链接时用的是HttpUtility.UrlEncode(url)，从链接地址获取标签时用的是
HttpUtility.UrlDecode(url)，从Encode到Decode，“C++”变成了“C  ”(加号变成空格)。这是大家熟知的问题，这里我们分析一下这个问题，并给出解决方法。
先看一下问题发生的过程：
1. 原始链接：
http://www.cnblogs.com/xd502djj/tag/C++/
2. HttpUtility.UrlEncode之后，得到：
http://www.cnblogs.com/xd502djj/tag/C%2b%2b/
3. Request.RawUrl，得到： 
http://www.cnblogs.com/xd502djj/tag/C++/
4. HttpUtility.UrlDecode，得到：
http://www.cnblogs.com/xd502djj/tag/C  /
上面第3步已经得到正确的结果，第4步的UrlDecode反而将加号变为了空格。
看来解决方法很简单，取消多此一举的UrlDecode，开始我们也是这么干的。过了一段时间，有用户反映“Windows Phone”的标签失效了，
变成了“Windows+Phone”。我们一查，原来是在HttpUtility.UrlEncode时，空格被转换为加号，需要调用UrlDecode将加号还原为空格，于是又把
HttpUtility.UrlDecode加上（忘了之前的“C++”标签问题）。然后，“C++”标签又失效...这样反反复复，看似Bug很多，工作很忙，实际上就是一个Bug...
终于有一天，我们说“再也不能这样过”，开始寻找解决方案：

既然HttpUtility.UrlEncode()不能用，那在.NET中找找有没有替代品。
先找到了HttpUtility.UrlPathEncode()。嘿，有用，轻松搞定“C++”与空格问题，但是...后来发现搞不定“C#”，它没有对“#”进行编码。
继续寻找...找到了Uri.EscapeUriString()，与HttpUtility.UrlPathEncode()同样的问题。
继续寻找...终于找到了...Uri.EscapeDataString()，搞定！请看下面的测试代码：

```
public void UrlEncodeTest()
{
    string url = "C++ C#";
    Console.WriteLine(HttpUtility.UrlEncode(url));//C%2b%2b+C%23
    Console.WriteLine(HttpUtility.UrlPathEncode(url));//C++%20C#
    Console.WriteLine(Uri.EscapeUriString(url));//C++%20C#
    Console.WriteLine(Uri.EscapeDataString(url));//C%2B%2B%20C%23
}
```

注：运行环境.NET4。