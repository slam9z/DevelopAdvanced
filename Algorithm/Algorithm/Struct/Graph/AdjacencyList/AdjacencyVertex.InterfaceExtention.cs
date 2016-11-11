using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Struct
{
    //这算静态类型的一个弊端吗？必须修改类或者使用继承才能解决这个问题。
    //明明只是临时用到，却要在类型系统里面留下痕迹。

    public partial class AdjacencyVertex<T> : IHeapNodeMap, IBelongedHeap
    {
        public object BelongedHeap
        {
            get; set;
        }

        public object HeapNodeMap
        {
            get; set;
        }
    }
}
