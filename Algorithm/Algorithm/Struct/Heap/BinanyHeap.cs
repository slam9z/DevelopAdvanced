using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Struct
{
    /// <summary>
    /// 将原来写在HeapSort里面的重构出来
    /// 最大堆和最小堆能写在一块吗？还是用基类
    /// </summary>
    public class BinanyHeap<T>
    {
        private int _heapSize;

        private IList<T> _source;

        private Func<T, T, bool> _larger;

        public bool IsMaxHeap { get; set; }

        public BinanyHeap(bool isMaxHeap)
        {
            IsMaxHeap = isMaxHeap;
        }

        public BinanyHeap()
        {
            IsMaxHeap = true;
        }

        public IList<T> Sort()
        {
            //进行排序它就不是Heap了，这个有点头疼。
            //copy一个操作
            var sortResult = new List<T>(_source);
            for (int i = sortResult.Count- 1; i >= 0; i--)
            {
                Exchange(sortResult, 0, i);
                _heapSize--;

                //把最大的置换到最后！
                MaxHeapify(sortResult, 1);
                
            }
            return sortResult;
        }

        public void BuildMaxHeap(IList<T> source, Func<T, T, bool> larger)
        {
            _heapSize = source.Count;
            _source = source;
            _larger = larger;

            //从1开始的
            for (int i = _heapSize / 2; i >= 1; i--)
            {
                MaxHeapify(source, i);
            }
        }

        /// <summary>
        /// 让root在堆中下降，是root的为根的子树成为最大堆
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="root"></param>
        /// <param name="larger"></param>
        public void MaxHeapify(IList<T> source, int root)
        {
            var left = Left(root);
            var right = Right(root);

            var largest = 0;

            if (left <= _heapSize && _larger(source[GetListIndex(left)], source[GetListIndex(root)]))
            {
                largest = left;
            }
            else
            {
                largest = root;
            }

            if (right <= _heapSize && _larger(source[GetListIndex(right)], source[GetListIndex(largest)]))
            {
                largest = right;
            }

            if (largest != root)
            {
                Exchange(source, GetListIndex(root), GetListIndex(largest));
                MaxHeapify(source, largest);
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

        private int Parent(int node)
        {
            return node / 2;
        }

        protected void Exchange<T>(IList<T> source, int indexa, int indexb)
        {
            T temp;
            temp = source[indexa];
            source[indexa] = source[indexb];
            source[indexb] = temp;
        }


        //IList索引和堆索引转换取值
        private int GetListIndex(int heapIndex)
        {
            return heapIndex - 1;
        }

        private int GetHeapIndex(int listIndex)
        {
            return listIndex + 1;
        }


    }
}
