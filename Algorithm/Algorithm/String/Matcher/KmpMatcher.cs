using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.String
{
    /// <summary>
    /// Kunth-Morris-Pratt
    /// 这个理论上最好的算法竟然是最慢的，不科学。
    /// 这样也算对的，它比普通状态机慢。
    /// </summary>
    public class KmpMatcher
    {
        public int Match(string value, string pattern)
        {
            var index = -1;
            var prefixFunction = ComputePrefixFunction(pattern);
            var p = -1;

            for (int i = 0; i < value.Length; i++)
            {
                while (p > -1 && pattern[p + 1] != value[i])
                {
                    p = prefixFunction[p];
                }
                if (pattern[p + 1] == value[i])
                {
                    p = p + 1;
                }
                if (p == pattern.Length-1)
                {
                    index = i-(pattern.Length-1);
                    p = pattern[p];
                    break;
                }
              
            }

            return index;
        }

        public IList<int> ComputePrefixFunction(string pattern)
        {
            var length = pattern.Length;

            var prefixFunction = new int[length];

            //用-1表示0，因为索引从0开始。-1表示不存在。
            var k = -1;
            prefixFunction[0] = -1;

            for (int q = 1; q < length; q++)
            {
                while (k > -1 && pattern[k + 1] != pattern[q])
                {
                    k = prefixFunction[k];
                }
                if (pattern[k + 1] == pattern[q])
                {
                    k = k + 1;
                }
                prefixFunction[q] = k;
            }


            return prefixFunction;

        }
    }
}
