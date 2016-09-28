[C#和sqlserver中生成新的32位GUID ](http://www.cnblogs.com/youring2/archive/2012/04/06/2434178.html)

C#中用Guid.NewGuid().ToString()
Sql中用NEWID()

以上方法生成的是36位的GUID，如果需要转换成32位，则需要替换掉其中的'-'字符。
Sql中的方法：replace(newid(), '-', '')

