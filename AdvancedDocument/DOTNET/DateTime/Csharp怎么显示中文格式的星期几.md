[C# 怎么显示中文格式的星期几 ](http://www.cnblogs.com/huangtailang/p/3197726.html)

1. DateTime.Now.ToString("dddd",new System.Globalization.CultureInfo("zh-cn")); 
2. new string[] { "星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六", }[Convert.ToInt16(DateTime.Now.DayOfWeek.ToString("D"))];

3. "星期"+DateTime.Now.DayOfWeek.ToString(("d"))  

4.

```cs 
/// <summary>
/// 返回日期几
/// </summary>
/// <returns></returns>
public static string DayOfWeek
{
    get
    {
        switch (DateTime.Now.DayOfWeek.ToString("D"))
        {
            case "0":
            return "星期日 ";
            case "1":
            return "星期一 ";
            case "2":
            return "星期二 ";
            case "3":
            return "星期三 ";
            case "4":
            return "星期四 ";
            case "5":
            return "星期五 ";
            case "6":
            return "星期六 ";
        }
    }
}
```