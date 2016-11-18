
不是所有人都安装nuget，感觉这不是问题

## nuget vs local  reference


感觉目前项目组使用local reference并不会有太大的问题，反正我自己
用nuget很舒服。

##Resulting

###Benefits

* repo里面都不需要存储dll

* 也不需要特意下载dll，只需要添加nuget引用

* 不用所有的dll都放到一个位置，如果不用nuget，只能用这种方式，否则更新很麻烦，
而且有很多dll连版本号都没有。

###Liabilities 

* 不同dll之间nuget引用dll版本号不一致,这个感觉也不能怪它


[Nuget]()

##VS管理工具

右击项目Nuget管理工具，缺什么程序集，就直接搜索！
然后生成packages.config文件

会添加到当前解决方案的packages目录下

##packages.config

```xml
<?xml version="1.0" encoding="utf-8"?>
<packages>
  <package id="CommonServiceLocator" version="1.3" targetFramework="net40-client" />
</packages>

```