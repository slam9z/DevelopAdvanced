using Microsoft.VisualStudio.TestTools.UnitTesting;
using Algorithm.Memoization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Memoization.Tests
{
    [TestClass()]
    public class FactorialCalculatorTests
    {
        [TestMethod()]
        public void FactorialTest()
        {
            var fac = new FactorialCalculator();
           // var result1 = fac.Factorial(3);
           // Console.WriteLine(result1);

            var result2 = fac.Factorial(10);
            Console.WriteLine(result2);

        }
    }
}