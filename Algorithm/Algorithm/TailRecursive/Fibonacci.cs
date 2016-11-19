using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm
{
    public class Fibonacci
    {

        public long Recursive(long n)
        {
            if (n == 1 || n == 2)
            {
                return 1;
            }
            return Recursive(n-1) + Recursive(n - 2);
        }

        public long TailRecursive(long n)
        {
            if (n == 1 || n == 2)
            {
                return 1;
            }
            return TailRecursive(n, 1, 1, 3);
        }

        public long TailRecursive(long n, long f1, long f2, long begin)
        {
            if (n == begin)
            {
                return f1 + f2;
            }

            return TailRecursive(n, f2, f1 + f2, begin + 1);
        }

        public long Loop(long n)
        {
            long f1 = 1, f2 = 1, begin;

            for (begin = 3; begin <= n; begin++)
            {
                var temp = f1;
                f1 = f2;
                f2 = temp + f2;
            }

            return f2;
        }

    }
}

