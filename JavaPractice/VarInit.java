public class VarInit
{

	public static  void main(String[] args)
	{
		int i=0;
		i++;
		//必须初始化，否则报错。这也是与C#不同的地方
		InitialClass ini=new InitialClass();
	}
}

class  InitialClass
{
		int i;
		InitialClass()
		{
			i++;
		}
		
}