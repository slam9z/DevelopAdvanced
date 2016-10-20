using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Struct
{
    public class AdjacencyEdge
    {
        public int Start { get; set; }
        public int End { get; set; }

        public AdjacencyEdge Next { get; set; }

        public AdjacencyEdge()
        {

        }

        public AdjacencyEdge(int start, int end)
        {
            Start = start;
            End = end;
        }

        public override string ToString()
        {
            return string.Format("Start: {0} End:{1}", Start, End);
        }
    }
}
