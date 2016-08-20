using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Sort
{
	public class HeapSort : SortBase
	{
		private int _heapSize;

		private int _length;

		//索引问题怎么办,怎样转换。
		//有私有变量，线程不安全。

		public override IList<T> Sort<T>(IList<T> source, Func<T, T, bool> larger)
		{
			var result = new List<T>(source);
			_length = result.Count;

			//避免转换问题
			result.Insert(0, default(T));

			BuildMaxHeap(result, larger);

			for (int i = _length; i > 1; i--)
			{
				Exchange(result, 1, i);
				_heapSize--;

				MaxHeapify(result, 1, larger);
				//把最大的置换到最后！
				//MaxHeapify(result, i, larger);
			}

			result.RemoveAt(0);

			return result;
		}

		private void BuildMaxHeap<T>(IList<T> source, Func<T, T, bool> larger)
		{
			_heapSize = _length;
			//从1开始的
			for (int i = _heapSize / 2; i > 0; i--)
			{
				MaxHeapify(source, i, larger);
			}
			Print(source);
		}

		private void MaxHeapify<T>(IList<T> heap, int root, Func<T, T, bool> larger)
		{
			var left = Left(root);
			var right = Right(root);

			var largest = 0;

			if (left <= _heapSize && larger(heap[left], heap[root]))
			{
				largest = left;
			}
			else
			{
				largest = root;
			}

			if (right <= _heapSize && larger(heap[right], heap[largest]))
			{
				largest = right;
			}

			if (largest != root)
			{
				Exchange(heap, root, largest);
				MaxHeapify(heap, largest, larger);
			}
		}

		private int Left(int root)
		{
			return 2 * root;
		}

		private int Right(int root)
		{
			return 2 * root + 1;
		}

		//IList索引和堆索引转换取值
		//private int GetListIndex(int heapIndex)
		//{
		//	return heapIndex - 1;
		//}

		//private int GetHeapIndex(int listIndex)
		//{
		//	return listIndex + 1;
		//}
	}
}
