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

        public int DegreeCountBound
        {
            get
            {
                if (Length == 0)
                {
                    return 0;
                }
                return (int)Math.Ceiling(Math.Log(Length, 2));
            }
        }

        public bool IsEmpty
        {
            get
            {
                return Length == 0;
            }
        }

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
            foreach (var item in source)
            {
                Insert(item);
            }
        }



        public FibonacciNode<T> Peek()
        {
            return Peak;
        }

        #region Extract

        public virtual FibonacciNode<T> Extract()
        {
            var extract = Peak;
            if (extract != null)
            {
                foreach (var item in GetList(Peak.Child))
                {
                    AddNode(Peak, item);
                    item.Parent = null;
                }

                DeleteNode(extract);

                //只有一个节点才会这样
                if (extract.Right == extract)
                {
                    Peak = null;
                }
                else
                {
                    Peak = extract.Right;
                    Concatenate();
                }
                Length = Length - 1;
            }

            return extract;
        }


        //使(成串地)连结[衔接]起来
        private void Concatenate()
        {
            var degreeArray = new FibonacciNode<T>[DegreeCountBound];

            var roots = GetList(Peak).ToList();

            foreach (var root in roots)
            {
                var currentNode = root;
                var degree = currentNode.Degree;

                //将相同degree的Node合并成树。

                while (degreeArray[degree] != null)
                {
                    var oldNode = degreeArray[degree];

                    //构建一个有序树。

                    if (_com(oldNode.Key, currentNode.Key))
                    {
                        Exchange(ref currentNode, ref oldNode);
                    }

                    Link(oldNode, currentNode);

                    degreeArray[degree] = null;
                    degree = degree + 1;
                }

                degreeArray[degree] = currentNode;

            }

            #region    //new peak and root list

            Peak = null;

            foreach (var item in degreeArray)
            {
                if (item != null)
                {
                    if (Peak == null)
                    {
                        //这个操作比较重要，新的root节点left和right都指向自己
                        item.Left = item;
                        item.Right = item;
                        Peak = item;
                    }

                    else
                    {
                        AddNode(Peak, item);
                        if (_com(item.Key, Peak.Key))
                        {
                            Peak = item;
                        }
                    }
                }
            }

            #endregion
        }


        private void Link(FibonacciNode<T> child, FibonacciNode<T> parent)
        {
            DeleteNode(child);

            if (parent.Child == null)
            {
                parent.Child = child;
                child.Left = child;
                child.Right = child;
            }
            else
            {
                AddNode(parent.Child, child);
            }
            child.Parent = parent;

            parent.Degree += 1;
            child.Mark = false;
        }




        #endregion

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

            //删除heap。
            heap.Peak = null;

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


        public void Traverse(FibonacciNode<T> startNode, Action<FibonacciNode<T>> action)
        {
            foreach (var item in TraverseList(startNode))
            {
                action(item);
                Traverse(item.Child, action);
            }
        }

        /// <summary>
        /// 不要在回修改引用的情况下使用这个方法
        /// </summary>
        /// <param name="startNode"></param>
        /// <param name="action"></param>
        public IEnumerable<FibonacciNode<T>> TraverseList(FibonacciNode<T> node)
        {
            if (node == null)
            {
                yield break;
            }
            var temp = node;
            do
            {
                yield return temp;
                temp = temp.Left;
            }
            while (temp != node);
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

        #region list

        //双向环形链表有很多好处

        private void DeleteNode(FibonacciNode<T> node)
        {
            node.Right.Left = node.Left;

            node.Left.Right = node.Right;
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

        //不能使用 yield ，各种修改引用很容易出问题。

        private IEnumerable<FibonacciNode<T>> GetList(FibonacciNode<T> node)
        {
            var result = new List<FibonacciNode<T>>();

            if (node == null)
            {
                return result;
            }
            var temp = node;
            do
            {
                result.Add(temp);
                temp = temp.Left;
            }
            while (temp != node);

            return result;

        }

        //这么基本的操作都有点搞不清，只是交换值不会有什么影响

        public void Exchange(ref FibonacciNode<T> x, ref FibonacciNode<T> y)
        {
            var temp = x;
            x = y;
            y = temp;
        }

        #endregion



    }
}
