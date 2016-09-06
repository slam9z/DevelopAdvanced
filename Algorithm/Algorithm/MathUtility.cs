using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm
{
    public static class MathUtility
    {
        public static int GreatestCommonDivisor(IList<int> source)
        {
            var length = source.Count;

            if (length < 2)
            {
                throw new ArgumentException("Do not use this method if there are less than two numbers.");
            }


            int temp = GreatestCommonDivisor(source[length - 1], source[length - 2]);

            for (int i = length - 3; i >= 0; i--)
            {

                temp = GreatestCommonDivisor(temp, source[i]);
            }
            return temp;
        }
        public static int GreatestCommonDivisor(int a, int b)
        {
            int remainder;

            while (b != 0)
            {
                remainder = a % b;
                a = b;
                b = remainder;
            }

            return a;
        }
    }
}
