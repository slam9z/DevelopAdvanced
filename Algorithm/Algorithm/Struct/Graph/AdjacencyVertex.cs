using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Struct
{

    public partial class AdjacencyVertex<T> : IEquatable<AdjacencyVertex<T>> where T : IEquatable<T>
    {
        /// <summary>
        /// 使用Key做边和顶点的标志是有问题的，因为Key可以重复。
        /// 使用索引又各种不灵活，不能改变顺序，所以加一个唯一标准。
        /// 但是可能会造成搜索的时间增加。这个数据结构调整有点大。
        /// </summary>
        public int Identifier { get; internal set; }

        public T Key { get; set; }

        public AdjacencyEdge<T> FirstEdge { get; set; }

        //某些算法用到,不属于自身性质
        public AdjacencyVertex<T> Predecessor { get; set; }


        public int Distance { get; set; }

        /// <summary>
        /// 通用的字段，否则Vertex类会爆炸，这也是静态语言不方便的地方。
        /// </summary>
        public int TempStorage { get; set; }

        /// <summary>
        /// 最短路径会使用
        /// </summary>
        public int WeightBound { get; set; }

        internal Color Color { get; set; }

        internal int FisrtVisitTime { get; set; }

        internal int FinalVisitTime { get; set; }


        public override string ToString()
        {
            return $"Identifier:{Identifier} Key: {Key}"
                // + $"MapIndex:{MapIndex} WeightBound: {WeightBound}"
               // + $"MapIndex:{MapIndex} TempStorage: {TempStorage}"
                ;
        }


        public bool Equals(AdjacencyVertex<T> other)
        {
            return Key.Equals(other.Key);
        }
    }
}
