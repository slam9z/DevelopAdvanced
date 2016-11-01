using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.String
{
    public class FiniteAutomatonMatcher
    {
        /// <summary>
        /// 通用的状态转移，难点在构建状态转移函数吧
        /// </summary>
        /// <param name="inputSequence"></param>
        /// <param name="stateTranstionFunction"></param>
        /// <param name="acceptStatus"></param>
        public void FiniteAutomaton(string inputSequence, Func<int, int, int> stateTranstionFunction, int acceptState)
        {
            var n = inputSequence.Length;
            //虽然代码很少，但是思想很强大。

            //初始状态
            var q = 0;
            for (int i = 0; i < n; i++)
            {
                q = stateTranstionFunction(q, i);

                if (q == acceptState)
                {
                    Console.WriteLine($"pattern occurs with shift {i - acceptState}");
                }
            }
        }


        private static IList<char> s_epsilon;

        static FiniteAutomatonMatcher()
        {
            s_epsilon = new List<char>();
            //假设之包含Ascii,要是Unicode会死人

            //for (int i = 0; i < 256 * 256; i++)
            for (int i = 0; i < 256; i++)
            {
                s_epsilon.Add((char)i);
            }
        }

        public int Match(string value, string pattern)
        {

            var delta = ComputeTransitionFunction(pattern, s_epsilon);

            var q = 0;
            for (int i = 0; i < value.Length; i++)
            {
                q = delta[q, value[i]];
                if (q == pattern.Length)
                {
                    return i - (pattern.Length - 1);
                }
            }
            return -1;
        }

        //状态转移函数应该用什么存
        public int[,] ComputeTransitionFunction(string pattern, IList<char> epsilon)
        {

            var m = pattern.Length;

            var table = new int[m + 1, epsilon.Count];

            //这也是构建的套路。

            //q表示开始状态
            for (int q = 0; q <= m; q++)
            {
                // epsilon[i]表示当前输入字符
                for (var i = 0; i < epsilon.Count; i++)
                {

                    var character = epsilon[i];

                    var k = Math.Min(m + 1, q + 2);

                    while (true)
                    {
                        k = k - 1;

                        if (k == 0 || isSuffix(pattern, k, q, character))
                        {
                            //table[q,i]也就是k表示表示输入新字符的终态。
                            table[q, i] = k;
                            break;
                        }
                    }

                }
            }

            return table;

        }

        /// <summary>
        /// pattern[k]是pattern[q]+character的后缀
        /// </summary>
        /// <param name="pattern"></param>
        /// <param name="k"></param>
        /// <param name="q"></param>
        /// <param name="character"></param>
        /// <returns></returns>
        public bool isSuffix(string pattern, int k, int q, char character)
        {

            if (pattern[GetListIndex(k)] != character)
            {
                return false;
            }

            for (int i = k - 1; i > 0; i--)
            {
                var p = q - (k - i) + 1;

                if (p < 0)
                {
                    break;
                }
                if (pattern[GetListIndex(i)] != pattern[GetListIndex(p)])
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 通过这个转换技术终于写正确了
        /// </summary>
        /// <param name="q"></param>
        /// <returns></returns>
        private int GetListIndex(int q)
        {
            return q - 1;
        }
    }
}
