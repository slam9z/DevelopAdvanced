using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Struct
{
    public class AdjacencyEdge<T> where T : IComparable
    {
        public AdjacencyVertex<T> Start { get; set; }
        public AdjacencyVertex<T> End { get; set; }

        public AdjacencyEdge<T> Next { get; set; }

        public AdjacencyEdge()
        {

        }

        public AdjacencyEdge(AdjacencyVertex<T> start, AdjacencyVertex<T> end)
        {
            Start = start;
            End = end;
        }

        public override string ToString()
        {
            return string.Format("Start: {0} \n End:{1}", Start, End);
        }
    }
}
