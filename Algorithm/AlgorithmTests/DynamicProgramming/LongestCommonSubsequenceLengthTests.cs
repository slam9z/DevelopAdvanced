using Microsoft.VisualStudio.TestTools.UnitTesting;
using Algorithm.DynamicProgramming;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlgorithmTests;

namespace Algorithm.DynamicProgramming.Tests
{
    [TestClass()]
    public class LongestCommonSubsequenceLengthTests
    {
        [TestMethod()]
        public void ComputeTest()
        {
            var value1 = "ABCBDAB".ToList();
            var value2 = "BDCABA".ToList();

            //var value1 = "ABC".ToList();
            //var value2 = "BDC".ToList();

            var lcs = new LongestCommonSubsequenceLength<char>();

            var result = lcs.Compute(value1, value2);

            Assert.AreEqual(result.Item1, 4);
            var lcsResult = new List<char>();

            TestHepler.PrintArray(result.Item2);

            lcs.Print(result.Item2, value1, value1.Count, value2.Count,
                (t) =>
                {
                    lcsResult.Insert(0, t);
                    Console.Write($"{t} ");
                }
                );


        }
    }
}