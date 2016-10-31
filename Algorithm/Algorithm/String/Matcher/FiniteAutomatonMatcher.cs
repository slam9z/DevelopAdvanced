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

            var table = new int[m+1, epsilon.Count];

            var k = 0;

            for (int q = 0; q <= m ; q++)
            {
                for (var i = 0; i < epsilon.Count; i++)
                {
                    var character = epsilon[i];

                    while (true)
                    {
                        k = Math.Min(m, q + 1);
                        k = k - 1;

                        if (
                        isSuffix(pattern, k, q, character)
                         )
                        {
                            Console.WriteLine($"{q},{epsilon[i]}:{k}");
                            table[q, i] = k;
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
                if (pattern[i] != pattern[p])
                {
                    return false;
                }
            }

            return true;
        }
    }
}
