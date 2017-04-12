## [SQL Server的sa密码丢失解决 ](http://www.cnblogs.com/dudumao/archive/2011/12/16/2290292.html)

首先用windows账户登录，然后在master表里执行：

```sql
EXEC sp_password NULL, '你的新密码', 'sa'
```

```sql
USE master

EXEC sp_password NULL, 'sa123456#', 'sa'

```
## [SQLSERVER2008 18456错误](http://www.cnblogs.com/496963524-zhangying/articles/2232599.html) 

### 1. sa设置
1. 以windows验证模式进入数据库管理器。

2. 右击sa，选择属性：
 
3. 点击状态选项卡：勾选授予和启用。然后确定

### 2. SQL SERVER 设置

1. 右击实例名称（就是下图画红线的部分），选择属性。

2. 点安全性，确认选择了SQL SERVER 和Windows身份验证模式。

3. 重启SQLSERVER服务(重要)。