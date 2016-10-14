public class VarArgs
{
	static void PrintIntArray(int... args)
	{
		for(int i:args)
		{
			System.out.print(i+"  ");
		}
		System.out.println();
	}

	public static void main(String[] args)
	{
		PrintIntArray(12,12,222);
		
	}
}