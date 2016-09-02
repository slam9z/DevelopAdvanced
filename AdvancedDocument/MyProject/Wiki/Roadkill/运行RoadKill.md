##1.获取github源代码

##2.编译运行Web项目

1. 错误
    无法打开登录所请求的数据库 "Roadkill"。登录失败。
    用户 'WANGJIN\ucs_liwei' 登录失败。 
    Description: An unhandled exception occurred during the execution of the current web request. Please review the stack trace for more information about the error and where it originated in the code. 

    Exception Details: System.Data.SqlClient.SqlException: 无法打开登录所请求的数据库 "Roadkill"。登录失败。
    用户 'WANGJIN\ucs_liwei' 登录失败。

2. 解决

    创建数据库 Roadkill，连接 Server=(local);Integrated Security=true;Connect Timeout=5;database=Roadkill 。
 
    在 Roadkill.Core\Database\Schema\SqlServer 目录找到创建数据库脚本



##System.ObjectDisposedException异常

LightSpeedPageRepository
LightSpeedSettingsRepository
类的Dispose方法异常

```cs
    #region IDisposable
public void Dispose()
{
	_unitOfWork.SaveChanges();
	_unitOfWork.Dispose();
}
    #endregion
```

```
System.ObjectDisposedException was unhandled by user code
  HResult=-2146232798
  Message=Cannot access a disposed object.
Object name: 'UnitOfWork'.
  ObjectName=UnitOfWork
  Source=Mindscape.LightSpeed
  StackTrace:
       at Mindscape.LightSpeed.UnitOfWorkBase. ()
       at Mindscape.LightSpeed.UnitOfWork.SaveChanges(Boolean reset)
       at Mindscape.LightSpeed.UnitOfWorkBase.SaveChanges()
       at Roadkill.Core.Database.LightSpeed.LightSpeedSettingsRepository.Dispose() in E:\Source\MyGithub\Roadkill\src\Roadkill.Core\Database\Repositories\LightSpeed\LightSpeedSettingsRepository.cs:line 123
       at StructureMap.BasicExtensions.SafeDispose(Object target) in C:\BuildAgent\work\a395dbde6b793293\src\StructureMap.Shared\BasicExtensions.cs:line 41
  InnerException: 
```
