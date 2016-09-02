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