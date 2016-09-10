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

		public abstract IList<T> Sort<T>(IList<T> source, Func<T, T, bool> greater);

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
			source[indexa] =source[indexb];
			source[indexb] = temp;
		}
	}
}
