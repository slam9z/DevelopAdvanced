using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Struct
{
    public class DisjointSetNode<T> where T: IComparable
    {
        public T Value { get; set; }

        public DisjointSetNode<T> Parent { get; set; }

        public int Rank { get; set; }

        public override string ToString()
        {
            return string.Format($"Value: {Value},Rank: {Rank} Parent:{Parent.Value}");
        }
    }
}
