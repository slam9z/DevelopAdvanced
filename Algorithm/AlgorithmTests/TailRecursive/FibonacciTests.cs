using Microsoft.VisualStudio.TestTools.UnitTesting;
using Algorithm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Tests
{
    [TestClass()]
    public class FibonacciTests
    {
        private long _n = 30;

        [TestMethod()]
        public void RecursiveTest()
        {
            var f = new Fibonacci();
            var r = f.Recursive(_n);
            Console.WriteLine($" {r}");
        }

        [TestMethod()]
        public void TailRecursiveTest()
        {
            var f = new Fibonacci();
            var r = f.TailRecursive(_n);
            Console.WriteLine($" {r}");
        }

        [TestMethod()]
        public void LoopTest()
        {
            var f = new Fibonacci();
            var r = f.Loop(_n);
            Console.WriteLine($" {r}");
        }
    }
}