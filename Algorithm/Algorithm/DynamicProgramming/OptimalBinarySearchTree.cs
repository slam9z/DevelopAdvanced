using Algorithm.Struct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.DynamicProgramming
{
    //不容易啊！感谢网上的大神。

    public class OptimalBinarySearchTree<T> : BinarySearchTree<T> where T : IComparable
    {
        private int[,] _root;
        private int _length;

        private IList<T> _sequence;
        private IList<T> _distinctSequence;




        /// <summary>
        /// 算法导论上的描述太难实现了，原理确实清楚点
        /// </summary>
        /// <param name="sequenceProbabilitys"></param>
        /// <param name="dq"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public float Compute(IList<float> sequenceProbability, IList<float> distinctSequenceProbability, int length)
        {
            var e = new float[length + 2, length + 2];
            var w = new float[length + 2, length + 2];
            var root = new int[length + 1, length + 1];

            for (int i = 1; i <= length + 1; i++)
            {
                e[i, i - 1] = distinctSequenceProbability[i - 1];
                w[i, i - 1] = distinctSequenceProbability[i - 1];
            }

            //不到添加新的节点，逐步计算。

            for (int subLength = 1; subLength <= length; subLength++)
            {
                Console.WriteLine();
                Console.WriteLine($" l: {subLength}");
                Console.WriteLine();

                //for (int i = 1; i <= n - len - 1; i++)  错在这里
                for (int i = 1; i <= length - subLength + 1; i++)
                {
                    var j = i + subLength - 1;
                    e[i, j] = float.MaxValue;

                    w[i, j] = w[i, j - 1] + sequenceProbability[j] + distinctSequenceProbability[j];

                    //求取最小代价的子树的根
                    for (int r = i; r <= j; r++)
                    {
                        var temp = e[i, r - 1] + e[r + 1, j] + w[i, j];
                        if (temp < e[i, j])
                        {
                            e[i, j] = temp;
                            root[i, j] = r;

                            Console.Write($"{i},{j}: {e[i, j] }  ");
                        }
                    }

                }
            }

           
            _root = root;
            _length = length;

            return e[1, _length];
        }


        public void Create(IList<T> sequence, IList<T> distinctSequence
         )
        {
            _sequence = sequence;
            _distinctSequence = distinctSequence;
            CreateCore(1, _length, -1, null);
        }

        public void PrintRoot()
        {
            Console.WriteLine("root");
            for (int i = 0; i < _length + 1; i++)
            {
                Console.WriteLine();
                for (int j = 0; j < _length + 1; j++)
                {
                    Console.Write($"{_root[i, j]}  ");
                }
            }

        }


        public void CreateCore(
           int sequenceIndex, int distinctSequenceIndex, int rootindex, BinaryTreeNode<T> root)
        {
            // Console.WriteLine();
            // Console.WriteLine($"init {sequenceIndex} {distinctSequenceIndex} {rootindex}");


            if (distinctSequenceIndex < sequenceIndex - 1)
            {
                return;
            }

            //虚拟值
            if (distinctSequenceIndex == sequenceIndex - 1)
            {
                //  if (distinctSequenceIndex <= rootChildIndex)
                if (distinctSequenceIndex < rootindex)
                {
                    //   Console.WriteLine($"left leaf {_distinctSequence[distinctSequenceIndex]}");
                    root.Left = new BinaryTreeNode<T>(_distinctSequence[distinctSequenceIndex]);
                }
                else
                {
                    //   Console.WriteLine($"right leaf {_distinctSequence[distinctSequenceIndex]}");

                    root.Right = new BinaryTreeNode<T>(_distinctSequence[distinctSequenceIndex]);
                }
                return;
            }


            var rootChildIndex = _root[sequenceIndex, distinctSequenceIndex];

            // Console.WriteLine($"rootChildIndex {rootChildIndex}");


            //根节点
            if (rootChildIndex == _root[1, _length])
            {
                root = new BinaryTreeNode<T>(_sequence[rootChildIndex - 1]);
                Root = root;

                //  Console.WriteLine($"root node {_sequence[rootChildIndex - 1]}");

            }


            //内节点
            else
            {
                if (rootChildIndex < rootindex)
                {
                    //  Console.WriteLine($"left node {_sequence[rootChildIndex - 1]}");

                    root.Left = new BinaryTreeNode<T>(_sequence[rootChildIndex - 1]);
                    root = root.Left;
                }
                else
                {
                    //  Console.WriteLine($"right node {_sequence[rootChildIndex - 1]}");

                    root.Right = new BinaryTreeNode<T>(_sequence[rootChildIndex - 1]);
                    root = root.Right;
                }
            }


            CreateCore(sequenceIndex, rootChildIndex - 1, rootChildIndex, root);
            CreateCore(rootChildIndex + 1, distinctSequenceIndex, rootChildIndex, root);
        }
    }
}
