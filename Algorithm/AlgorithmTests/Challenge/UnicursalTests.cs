using Microsoft.VisualStudio.TestTools.UnitTesting;
using Algorithm.Challenge;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Challenge.Tests
{
    [TestClass()]
    public class UnicursalTests
    {
        [TestMethod()]
        public void CreateTest()
        {
            var uni = new Unicursal();
            uni.Create(3, 3);

            //一共20条边。
        }



        [TestMethod()]
        public void CalcPathTest()
        {
            var uni = new Unicursal();
            uni.Create(3, 3);
            uni.CalcPath();

            PrintResult(uni);

        }

        private void PrintResult(Unicursal uni)
        {
            var lenthPaths = uni.LengthPaths;

            Console.WriteLine($"\n vertexs:");
            for (int i = 0; i < uni.XDimensionBound*uni.YDimensionBound; i++)
            {
                if (i % uni.XDimensionBound == 0)
                {
                    Console.WriteLine();
                }
                Console.Write($"{i + 1} \t");
            }

            Console.WriteLine($"\n totalEdgeCount:{uni.Graph.GetEdges().Count() / 2} \n");

            foreach (var item in lenthPaths)
            {
                if (item.Key >= 4)
                {
                    Console.WriteLine($"\n the length is {item.Key} path count {item.Value.Count } \n");
                }
            }


            var totalPathCount = lenthPaths
        .Where(p => p.Key >= 4)
        .Sum(p => p.Value.Count);

            Console.WriteLine($"\n totalPathCount:{totalPathCount} \n");

            foreach (var item in lenthPaths)
            {
                if (item.Key >= 4)
                {
                    Console.WriteLine($"\n the length is {item.Key} path count {item.Value.Count } \n");


                    foreach (var vertexs in item.Value)
                    {
                        Console.WriteLine();
                        foreach (var vertex in vertexs)
                        {
                            Console.Write($"{vertex + 1}  \t");
                        }

                    }

                }
            }
        }
    }
}