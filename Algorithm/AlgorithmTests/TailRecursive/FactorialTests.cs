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
    public class FactorialTests
    {
        private long _n = 10;

        [TestMethod()]
        public void RecursiveTest()
        {
            var f = new Factorial();
            var r = f.Recursive(_n);
            Console.WriteLine($" {r}");
        }

        [TestMethod()]
        public void TailRecursiveTest()
        {
            var f = new Factorial();
            var r = f.TailRecursive(_n);
            Console.WriteLine($" {r}");
        }

        [TestMethod()]
        public void LoopTest()
        {
            var f = new Factorial();
            var r = f.Loop(_n);
            Console.WriteLine($" {r}");
        }
    }
}