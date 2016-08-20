using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Sort
{
	public abstract class SortBase : ISort
	{
		public IList<T> Sort<T>(IList<T> source) where T : IComparable
		{
			return Sort(source, (a, b) => a.CompareTo(b) > 0);
		}

		public abstract IList<T> Sort<T>(IList<T> source, Func<T, T, bool> larger);

		//这种交换对值类型毫无意义
		//public void Exchange<T>(T a, T b)
		//{
		//	T temp;
		//	temp = a;
		//	a = b;
		//	b = temp;
		//}

		public void Exchange<T>(IList<T> source, int indexa, int indexb)
		{
			T temp;
			temp = source[indexa];
			source[indexa] = source[indexb];
			source[indexb] = temp;
		}

		protected void Print(string format,params object[] args)
		{
			Console.WriteLine(format,args);
		}

		protected void Print(string message)
		{
			Console.WriteLine(message);
		}


		protected void Print<T>(IList<T> source, int bound, int upper)
		{
			Print("");
			for (int i = bound; i <= upper; i++)
			{
				Console.Write("{0}, ",source[i]);
			}
			Print("");
		}

		protected void Print<T>(IList<T> source)
		{
			Print(source, 0, source.Count-1);
		}
	}
}
