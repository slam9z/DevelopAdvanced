using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Struct
{
    public class FlowEdge<T> : AdjacencyEdge<T> where T : IEquatable<T>
    {
        public int Flow { get; set; }

        public int Capacity { get; set; }

        /// <summary>
        /// 这个是必须的嘛
        /// </summary>
        public FlowEdge<T> Revolution { get; set; }

        public int ResidualCapacity
        {
            get
            {
                return Capacity - Flow;
            }
        }

        public FlowEdge()
        {

        }

        public FlowEdge(AdjacencyVertex<T> start, AdjacencyVertex<T> end, int capacity)
            : base(start, end)
        {
            Capacity = capacity;
        }

        /// <summary>
        /// 终于想明白了，回流肯定是可以只不过Capacity是0。
        /// </summary>
        public FlowEdge<T> GetRevolution()
        {
            var rev = new FlowEdge<T>(End, Start, 0);
            Revolution = rev;
            rev.Revolution = this;
            return rev;
        }

        public override string ToString()
        {
            return $@"Start: { Start} End:{ End},Capacity:{Capacity} Flow:{Flow} ";
        }
    }
}
