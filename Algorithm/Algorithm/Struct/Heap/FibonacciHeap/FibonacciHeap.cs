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

        public FibonacciHeap(Func<T, T, bool> com)
        {
            _com = com;
        }



        protected virtual void Build(IList<T> source)
        {

        }



        #region priorityQueue

        #endregion

        public FibonacciNode<T> Peek()
        {
            return Peak;
        }

        public virtual FibonacciNode<T> Extract()
        {
            var extract = Peak;
            if (extract != null)
            {


                Length = Length - 1;
            }

            return extract;
        }



        //将之前的2个堆变成1个之包含2个根节点的堆。
        public virtual void Union(FibonacciHeap<T> heap)
        {
            if (heap.Peak == null)
            {
                return;
            }

            if (Peak == null)
            {
                Peak = heap.Peak;
            }
            else if (heap.Peak != null)
            {
                // union root list
                // 这里理解错了，是将两个Peak添加到根列表，之前的都堆变成树了，但是还没有排序。
                //这样会丢失数据吧

                LinkedIn(Peak, heap.Peak);

                if (_com(heap.Peak.Key, Peak.Key))
                {
                    Peak = heap.Peak;
                }
            }

            Length = Length + heap.Length;
        }

        public virtual void Insert(T key)
        {
            var node = new FibonacciNode<T>();
            node.Key = key;
            Insert(node);
        }


        //一直插入是一个根列表
        public virtual void Insert(FibonacciNode<T> newNode)
        {
            newNode.Degree = 0;
            newNode.Parent = null;
            newNode.Child = null;
            newNode.Left = newNode;
            newNode.Right = newNode;
            newNode.Mark = false;

            if (Peak == null)
            {
                Peak = newNode;
            }
            else
            {
                //insert to root list
                AddNode(Peak, newNode);

                if (_com(newNode.Key, Peak.Key))
                {
                    Peak = newNode;
                }
            }

            Length = Length + 1;

        }




        /// <summary>
        /// 将新节点插入到双向环形链表
        /// </summary>
        /// <param name="root"></param>
        /// <param name="newNode"></param>
        private void AddNode(FibonacciNode<T> root, FibonacciNode<T> newNode)
        {

            //将新节点插入到根某节点的Left。

            newNode.Left = root.Left;
            root.Left.Right = newNode;

            newNode.Right = root;
            root.Left = newNode;
        }


        /// <summary>
        /// 合并两个双向环形链表
        /// </summary>
        /// <param name="root"></param>
        /// <param name="newRoot"></param>
        private void LinkedIn(FibonacciNode<T> root, FibonacciNode<T> newRoot)
        {
            //交换两个链表的Right。
            //因为是环形的保证可以合并成一个新的链表

            var temp = root.Right;
            root.Right = newRoot.Right;
            newRoot.Right.Left = root;

            newRoot.Right = temp;
            temp.Left = newRoot;
        }

        //使(成串地)连结[衔接]起来
        private void Concatenate()
        {

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
