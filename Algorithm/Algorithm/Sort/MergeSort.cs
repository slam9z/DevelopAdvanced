using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Sort
{
	public class MergeSort : SortBase
	{
		public override IList<T> Sort<T>(IList<T> source, Func<T, T, bool> com)
		{
			var result = new List<T>(source);
			MergeSortCore(result, 0, result.Count - 1, com);
			return result;
		}

		private void MergeSortCore<T>(IList<T> source, int bound, int upper, Func<T, T, bool> com)
		{
			if (upper > bound)
			{
				//var split = upper + bound / 2;
				var split = (upper + bound) / 2;
				MergeSortCore(source, bound, split, com);
				MergeSortCore(source, split + 1, upper, com);
				Merge(source, bound, split, upper, com);
			}
		}

		//递归出问题最好的办法就是打印结果了。

		private void Merge<T>(IList<T> source, int bound, int split, int upper, Func<T, T, bool> com)
		{
			Print("message source bound:{0} upper:{1}",bound,upper);
			Print(source, bound, upper);

			var boundData = new List<T>();
			for (int r = bound; r <= split; r++)
			{
				boundData.Add(source[r]);
			}

			var upperData = new List<T>();
			for (int r = split+1; r <= upper; r++)
			{
				upperData.Add(source[r]);
			}

			var b = 0;
			var u = 0;
			for (int k = bound; k <= upper; k++)
			{

				//避免检查是否为空，加一个“哨兵”,因为用泛型所以没法加“哨兵啊”。
				//boundData.Add(default(T));
				//upperData.Add(default(T));

				if (boundData.Count == b&& upperData.Count != u)
				{
					source[k] = upperData[u];
					u++;
					continue;
				}

				if (upperData.Count == u&& boundData.Count != b)
				{
					source[k] = boundData[b];
					b++;
					continue;
				}

				if (upperData.Count == u && boundData.Count == b)
				{
					break;
				}

				if (com(boundData[b], upperData[u]))
				{
					source[k] = upperData[u];
					u++;
				}
				else
				{
					source[k] = boundData[b];
					b++;
				}
			}


			Print("message result bound:{0} upper:{1}", bound, upper);
			Print(source, bound, upper);
		}
	}
}
