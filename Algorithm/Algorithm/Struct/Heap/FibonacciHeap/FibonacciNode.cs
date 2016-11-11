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

        public int Degree { get; set; }

        public bool Mark { get; set; }

        public FibonacciNode<T> Parent { get; set; }

        public FibonacciNode<T> Child { get; set; }

        public FibonacciNode<T> Left { get; set; }

        public FibonacciNode<T> Right { get; set; }

        public override string ToString()
        {
            return $"{Key}";
        }

    }
}
