using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.String
{
    public class FiniteAutomatonMatcher
    {


        //状态转移函数应该用什么存
        public int[,] ComputeTransitionFunction(string pattern, IList<char> epsilon)
        {

            var m = pattern.Length;

            var table = new int[m + 1, epsilon.Count];

            var k = 0;

            for (int q = 0; q < m; q++)
            {
           //     var pa = pattern[q];
                for (var i = 0; i < epsilon.Count; i++)
                {
                  
                    var character = epsilon[i];


                    k = Math.Min(m+1, q + 2);

                    while (true)
                    {
                        k = k - 1;

                        if (k == 0)
                        {
                            table[q, i] = 0;
                            break;
                        }
                        if (
                        isSuffix(pattern, k, q-1, character)
                         )
                        {
                            table[q, i] = k+1;
                            break;
                        }
                    }

                }
            }

            return table;

        }

        //pattern[k]是pattern[q]+character的后缀
        public bool isSuffix(string pattern, int k, int q, char character)
        {

            if (pattern[k] != character)
            {
                return false;
            }

            for (int i = k - 1; i >= 0; i--)
            {
                var p = q - (k - i + 1);

                if (p < 0)
                {
                    break;
                }
                if (pattern[i] != pattern[p])
                {
                    return false;
                }
            }

            return true;
        }
    }
}
