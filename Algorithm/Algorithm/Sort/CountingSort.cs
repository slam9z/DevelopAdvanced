using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Sort
{
	public class CountingSort
	{
		public IList<int> Sort(IList<int> source, int upperValue)
		{
			var result = new int[source.Count];

			var count = new List<int>();

			for (int i = 0; i < upperValue; i++)
			{
				count.Add(0);
			}

			//记录每个数出现的次数
			for (int i = 0; i < source.Count; i++)
			{
				count[source[i]] = count[source[i]] + 1;
			}

			//记录每个数小于和等于当前数的个数
			for (int i = 1; i < upperValue; i++)
			{
				count[i] = count[i] + count[i - 1];
			}

			for (int j = 0; j < source.Count; j++)
			{
				//索引从0开始
				result[count[source[j]]-1] = source[j];
				//result[count[source[j]]] = source[j];
				count[source[j]] = count[source[j]] - 1;
			}

			return result;
		}

	}
}
