[EF 批量更新/删除数据 ](http://blog.csdn.net/lee576/article/details/44922727)

在网上找了很久,得到的答案是”Entity Framework 中不能同时更新多条记录”,历经这么多版本,居然还没有这种基本功能,我真的很无语了.还要先查询出来,然后再对实体更新或删除,那效率可想而知了……
在网上找了找解决方案,比如说这个
EF架构~性能高效的批量操作(Update篇）
感觉在剑走偏锋,里面实际是在拼Sql(当然EF最终也是拼SQL),我却不喜欢这么干,完全没有Linq的感觉,也很别扭.

最后又找到个开源库,又是老外解决的
Entity Framework Extended Library
同时有一篇简单介绍
EF扩展库（批量操作）

现在update可以这么干了,用不着把实体先取出来了

```sql
public static void UpdateBalance(ChannelAccount channelAccount)
{
    using (FinanceContext context = new FinanceContext())
    {
        context.ChannelAccounts
            .Where(t => t.ChannelAccountID == channelAccount.ChannelAccountID)
            .Update(t => new ChannelAccount {Balance = channelAccount.Balance});
        context.SaveChanges();
    }
}
```