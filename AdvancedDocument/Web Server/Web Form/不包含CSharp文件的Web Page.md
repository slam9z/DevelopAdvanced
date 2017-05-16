* 无法使用继承
    
    Note that BasePage shouldn't be a full ASPX page, just a normal *.cs class file. 
  
    Why?系统限制吧，两个网页Render肯定有问题。

* 复用估计只能通过ascx文件了

    * ascx里面定义类会和恶心，都是内部类


* 页面也是很多相似的，也要想办法复用，写重复代码真是很难让我接受。

    页面复用比较难，复用无非就是部分查询和部分列表。