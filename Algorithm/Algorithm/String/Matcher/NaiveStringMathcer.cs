using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.String
{
    public class NaiveStringMathcer
    {
        public int Match(string value, string pattern)
        {
            for (int i = 0; i <= value.Length - pattern.Length; i++)
            {
                if (value.Substring(i,pattern.Length) == pattern)
                {
                    return i;
                }
            }

            return -1;
        }
    }
}
