using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Extensions
{

    //头一次用BitArray，发现它根本没法用啊，没有集合操作的集合。
    //微软为什么不提供这些方法呢
    //Concatenate  stackoverflow的方法
    public static class BitArrayExtensions
    {
        public static BitArray Prepend(this BitArray current, BitArray before)
        {
            var bools = new bool[current.Count + before.Count];
            before.CopyTo(bools, 0);
            current.CopyTo(bools, before.Count);
            return new BitArray(bools);

        }

        public static BitArray Append(this BitArray current, BitArray after)
        {
            var bools = new bool[current.Count + after.Count];
            current.CopyTo(bools, 0);
            after.CopyTo(bools, current.Count);
            return new BitArray(bools);
        }

        public static string ToZeroOneString(this BitArray value)
        {
            if (value != null)
            {
                var bools = new bool[value.Count];
                value.CopyTo(bools, 0);
                return new string(bools.Select(o => o ? '1' : '0').ToArray());
            }
            return string.Empty;
        }
    }


}
