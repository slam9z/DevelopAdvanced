using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Sort
{
	public class QuickSort : SortBase
	{
		public override IList<T> Sort<T>(IList<T> source, Func<T, T, bool> com)
		{
			var result = new List<T>(source);

			//	QuickSortCore(result, 0, result.Count - 1, larger);

			HoareQuickSortCore(result, 0, result.Count - 1, com);

			//Print(result);
			return result;
		}

		private void QuickSortCore<T>(IList<T> source, int bound, int upper, Func<T, T, bool> com)
		{
			if (bound < upper)
			{
				var partition = Partition(source, bound, upper, com);
				QuickSortCore(source, bound, partition - 1, com);
				QuickSortCore(source, partition + 1, upper, com);
			}
		}


		//到底发生了什么
		private void HoareQuickSortCore<T>(IList<T> source, int bound, int upper, Func<T, T, bool> com)
		{
			if (upper > bound)
			{
				var partition = HoarePartition(source, bound, upper, com);

				HoareQuickSortCore(source, bound, partition, com);
				HoareQuickSortCore(source, partition + 1, upper, com);
			}
		}

		//分为2组
		private int HoarePartition<T>(IList<T> source, int bound, int upper, Func<T, T, bool> com)
		{
			var key = source[bound];
			var smallPointer = bound - 1;
			var largePointer = upper + 1;


			while (true)
			{
				do
				{
					largePointer--;
				}
				while (com(source[largePointer], key));

				do
				{
					smallPointer++;
				}
				while (com(key, source[smallPointer]));

				if (largePointer > smallPointer)
				{
					//Print(source, bound, upper);
					//Print("Exchange:{0},{1}", smallPointer, largePointer);

					Exchange(source, smallPointer, largePointer);
				}
				else
				{
					//Print(source, bound, upper);
					//Print("return" + largePointer);

					return largePointer;
				}
			}
		}


		//这个划分方式不是很直观还是记住
		//分为3组
		private int Partition<T>(IList<T> source, int bound, int upper, Func<T, T, bool> com)
		{
			var key = source[upper];
			var smallPartition = bound - 1;
			var largePartition = bound;

			for (largePartition = bound; largePartition < upper; largePartition++)
			{
				if (com(key, source[largePartition]))
				{
					smallPartition++;
					Exchange(source, smallPartition, largePartition);
				}
			}
			Exchange(source, smallPartition + 1, upper);
			return smallPartition + 1;
		}

	}
}
