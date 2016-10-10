using System;
using System.Collections.Generic;
using System.Drawing;
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

        /// <summary>
        ///取scale值
        /// </summary>
        /// <returns></returns>
        public static double GetMaxScaling(
            int orignWidth, int orignHeight
            , int minWidth, int maxWidth
            , int minHeight, int maxHeight
            )
        {
            if (orignWidth == 0 || orignHeight == 0)
            {
                return 0d;
            }

            var maxWidthScale = Math.Max(
                    minWidth / (double)(orignWidth)
                    , maxWidth / (double)(orignWidth)
                );

            var minWidthScale = Math.Min(
                     minWidth / (double)(orignWidth)
                     , maxWidth / (double)(orignWidth)
                 );

            var maxHeightScale = Math.Max(
                    minHeight / (double)(orignHeight)
                    , maxHeight / (double)(orignHeight)
                );

            var minHeightScale = Math.Min(
                     minHeight / (double)(orignHeight)
                    , maxHeight / (double)(orignHeight)
                );

            //无交集
            if (minHeightScale > maxWidthScale || minWidthScale > maxHeightScale)
            {
                return 0;
            }

            //相交,取两者最大里面的最小一个。
            return Math.Min(maxWidthScale, maxHeightScale);
        }
    }
}
