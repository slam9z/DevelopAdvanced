using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Sort
{
	public class BubbleSort : SortBase
	{
		public override IList<T> Sort<T>(IList<T> source, Func<T, T, bool> com)
		{
			var length = source.Count;

			var result = new List<T>(source);

			//冒泡的的趟数
			for (int i = 1; i <= length; i++)
			{
				//将大数浮上去

				//一个简单的冒泡排序，也容易犯边界条件的错误。
				//for (int j = 0; j < length - i+1; j++)
				for (int j = 0; j < length - i; j++)
				{
					if (com(result[j],result[j + 1]))
					{
						Exchange(result, j + 1, j);
					}
				}
			}

			return result;
		}
	}
}
