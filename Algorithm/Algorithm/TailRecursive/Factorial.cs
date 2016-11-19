using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm
{
    public class Factorial
    {
        
        public long Recursive(long n)
        {
            return n == 1 ? 1 : n * Recursive(n - 1);
        }

        public long TailRecursive(long n)
        {
            return n == 0 ? 1 : TailRecursive(n, 1);
        }

        private long TailRecursive(long n, long a)
        {
            return n == 1 ? a : TailRecursive(n - 1, a * n);
        }

        public long Loop(long n)
        {
            var result = 1L;
            for (int i = 1; i <= n; i++)
            {
                result = result * i;
            }

            return result;
        }
    }
}