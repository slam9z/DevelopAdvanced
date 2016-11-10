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
    public class FibonacciHeap<T>
    {
        /// <summary>
        /// 最大值或者最小值
        /// </summary>
        public FibonacciNode<T> Peak { get; set; }

        public int Length { get; set; }


        protected Func<T, T, bool> _com;


        public FibonacciHeap(IList<T> source, Func<T, T, bool> com)
        {
            _com = com;
            Build(source);
        }



        protected virtual void Build(IList<T> source)
        {

        }



        #region priorityQueue

        #endregion

        public T Peek()
        {
            throw new NotImplementedException();
        }

        public virtual T Extract()
        {
            throw new NotImplementedException();
        }



        public virtual void Insert(FibonacciNode<T> newNode)
        {
            newNode.Degree = 0;
            newNode.Parent = null;
            newNode.Child = null;
            newNode.Left = newNode;
            newNode.Right = newNode;
            newNode.Mark = false;


            if (Peak == null || _com(newNode.Key, Peak.Key))
            {
                Peak = newNode;
            }

            Length = Length + 1;

        }


        /// <summary>
        /// newKey不能小于oldKey
        /// 将newKey往上移
        /// Increase和Decrease
        /// </summary>
        /// <param name="oldKey"></param>
        /// <param name="newKey"></param>
        public void UpdateKey(int heapIndex, T newKey)
        {
            throw new NotImplementedException();
        }





    }
}
