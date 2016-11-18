using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Sort
{
	public class InsertSort : SortBase
	{
		public override IList<T> Sort<T>(IList<T> source, Func<T, T, bool> com)
		{
			var length = source.Count;

			var result = new List<T>(source);

			//还是在原来的数值上计算比较好

			//第一和最后一个就不要插入了
			//j是在i-1的基础上的。

			for (int i = 1; i < result.Count ; i++)
			//for (int i = 1; i < source.Count; i++)
			{
				var j = i - 1;

				//需要存储起来，否则就覆盖了。
				//万恶的key，要存储起来。这是在原来的基础上修改的
				var key = result[i];

				//这里不太适合for循环，而是适合while循环,因为结束条件是动态的，
				//而且结果在外面也要用到
				while (j >= 0 && com(result[j], key))
				{
					Exchange(result, j, j + 1);
					j--;
				}

				result[j + 1] = key;
			}

			return result;
		}

	}
}
