using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Struct
{
    /// <summary>
    /// 感觉我的代码要爆炸了！在Prim算法中用到。这个东西扩展性有点差！
    /// 除了通过节点扩展T，也可以使用接口让T包含node，再扩展node！
    /// 我只能想到这两个方法。
    /// </summary>
    public interface IBelongedHeap
    {
        object BelongedHeap { get; set; }
    }
}
