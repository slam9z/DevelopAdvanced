using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Struct
{
    //时间基本没区别，可能是TransposeGraph的问题，不过这样才算比较正常的代码
    //唯一的麻烦就是要拷贝的时候要小心。
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
