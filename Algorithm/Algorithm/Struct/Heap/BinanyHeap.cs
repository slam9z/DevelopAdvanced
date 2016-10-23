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
        private int _heapSize
        {
            get
            {
                return _source.Count;
            }
        }

        private IList<T> _source;

        private Func<T, T, bool> _com;

        public BinanyHeap(IList<T> source, Func<T, T, bool> com)
        {
            Build(source, com);
        }

        public IList<T> Sort()
        {
            //进行排序它就不是Heap了，这个有点头疼。
            //copy一个操作
            var sortResult = new List<T>(_source);

            var size = sortResult.Count;
            for (int i = sortResult.Count - 1; i >= 0; i--)
            {
                Exchange(sortResult, 0, i);

                //把最大的置换到最后！
                size--;
                Heapify(sortResult, 1, size);

            }
            return sortResult;
        }

        /// <summary>
        /// BuildMaxHeap那种伪代码是面向过程的写法，选择使用面向对象的方法。
        /// 重复调用会有问题，其实属于构造函数。
        /// </summary>
        /// <param name="source"></param>
        /// <param name="com"></param>
        private void Build(IList<T> source, Func<T, T, bool> com)
        {
            _source = source;
            _com = com;

            //从1开始的
            for (int i = _heapSize / 2; i >= 1; i--)
            {
                Heapify(source, i, _heapSize);
            }
        }

        /// <summary>
        /// 让root在堆中下降，是root的为根的子树成为最大堆
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="root"></param>
        /// <param name="larger"></param>
        public void Heapify(IList<T> source, int root, int size)
        {
            var left = Left(root);
            var right = Right(root);

            var largest = 0;

            if (left <= size && _com(source[GetListIndex(left)], source[GetListIndex(root)]))
            {
                largest = left;
            }
            else
            {
                largest = root;
            }

            if (right <= size && _com(source[GetListIndex(right)], source[GetListIndex(largest)]))
            {
                largest = right;
            }

            if (largest != root)
            {
                Exchange(source, GetListIndex(root), GetListIndex(largest));
                Heapify(source, largest, size);
            }
        }

        #region priorityQueue

        #endregion

        public T Peek()
        {
            return _source[0];
        }

        public T Extract()
        {
            if (_heapSize < 1)
            {
                throw new InvalidOperationException("heap underflow");
            }
            var max = _source[0];
            _source[0] = _source[GetListIndex(_heapSize)];
            _source.RemoveAt(GetListIndex(_heapSize));

            Heapify(_source, 1, _heapSize);
            return max;
        }



        public void Insert(T key)
        {
            //没有无穷小给我取啊

            _source.Add(key);
            IncreaseKey(_heapSize, key);
        }


        /// <summary>
        /// newKey不能小于oldKey
        /// 将newKey往上移
        /// </summary>
        /// <param name="oldKey"></param>
        /// <param name="newKey"></param>
        public void IncreaseKey(int heapIndex, T newKey)
        {
            if (_com(_source[GetListIndex(heapIndex)], newKey))
            {
                throw new ArgumentException("new key is invalid)");
            }
            _source[GetListIndex(heapIndex)] = newKey;

            while (heapIndex > 1
                &&
               _com(newKey, _source[GetListIndex(Parent(heapIndex))])
               )
            {
                Exchange(_source,
                    GetListIndex(heapIndex),
                    GetListIndex(Parent(heapIndex))
                    );

                heapIndex = Parent(heapIndex);
            }
        }

        #region base

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

        #endregion

    }
}
