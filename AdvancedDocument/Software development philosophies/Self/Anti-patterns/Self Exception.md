
自攻自受的异常,也是长见识了。

```cs
try
{
    if(file==null)
    {

        throw new Exception("");
    }

}
catch(Exception ex)
{
    var message=ex.Message;        
}


```