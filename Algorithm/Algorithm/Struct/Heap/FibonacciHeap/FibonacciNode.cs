using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Struct
{
    public class FibonacciNode<T>
    {

        public T Key { get; set; }

        /// <summary>
        /// 子女的个数
        /// </summary>
        public int Degree { get; set; }

        /// <summary>
        /// 是否失去过一个孩子
        /// </summary>
        public bool Mark { get; set; }

        public FibonacciNode<T> Parent { get; set; }

        public FibonacciNode<T> Child { get; set; }

        public FibonacciNode<T> Left { get; set; }

        public FibonacciNode<T> Right { get; set; }

        public FibonacciNode()
        {

        }

        public FibonacciNode(T key)
        {
            Key = key;
        }

        public override string ToString()
        {
            return $"key: {Key} Degree:{Degree}  ";
        }

    }
}
