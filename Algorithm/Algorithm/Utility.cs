using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm
{
    public class Utility
    {
        public static void PrintArray<T>(T[,] array, string message = "")
        {
            if (!string.IsNullOrWhiteSpace(message))
            {
                Console.WriteLine();
            }
            Console.WriteLine(message);

            for (int i = array.GetLowerBound(0); i < array.GetUpperBound(0); i++)
            {
                Console.WriteLine();
                for (int j = array.GetLowerBound(1); j < array.GetUpperBound(0); j++)
                {
                    Console.Write($"{array[i, j]}  \t");
                }
            }

            Console.WriteLine();
        }
    }
}
