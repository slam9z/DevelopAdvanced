using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Struct
{
    /// <summary>
    /// 不管用什么实现堆，只要调用UpdateKey就会存在这个问题。
    /// </summary>
    public interface IHeapNodeMap
    {
        object HeapNodeMap { get; set; }
    }
}
