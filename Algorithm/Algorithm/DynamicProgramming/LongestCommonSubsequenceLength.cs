using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.DynamicProgramming
{
    /// <summary>
    /// LCSLength  不知道怎么命名
    /// 
    /// </summary>
    public class LongestCommonSubsequenceLength<T> where T : IEquatable<T>
    {
        public Tuple<int, char[,]> Compute(IList<T> value1, IList<T> value2)
        {
            var m = value1.Count;
            var n = value2.Count;

            var c = new int[m, n];
            var b = new char[m, n];

            //初始化0；

            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (value1[i].Equals(value2[j]))
                    {
                        c[i, j] = GetIntArrayValue(c, i - 1, j - 1) + 1;
                        b[i, j] = '\\';
                    }


                    else if (GetIntArrayValue(c, i - 1, j) >= GetIntArrayValue(c, i, j - 1))
                    {
                        c[i, j] = GetIntArrayValue(c, i - 1, j);
                        b[i, j] = '|';
                    }

                    else
                    {
                        c[i, j] = GetIntArrayValue(c, i, j - 1);
                        b[i, j] = '-';
                    }
                }
            }

            return new Tuple<int, char[,]>(c[m - 1, n - 1], b);
        }

        /// <summary>
        /// 索引问题除了转换都不好解决
        /// </summary>
        /// <param name="array"></param>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns></returns>
        private int GetIntArrayValue(int[,] array, int i, int j)
        {
            if (i < 0 || j < 0)
            {
                return 0;
            }
            else
            {
                return array[i, j];
            }
        }


        public void Print(char[,] route, IList<T> value, int length1, int length2, Action<T> action)
        {
            if (length1 == 0 || length2 == 0)
            {
                return;
            }
            if (route[length1 - 1, length2 - 1] == '\\')
            {
                Print(route, value, length1 - 1, length2 - 1, action);
                action(value[length1 - 1]);
            }

            else if (route[length1 - 1, length2 - 1] == '|')
            {
                Print(route, value, length1 - 1, length2, action);
            }

            else
            {
                Print(route, value, length1, length2 - 1, action);
            }
        }
    }
}
