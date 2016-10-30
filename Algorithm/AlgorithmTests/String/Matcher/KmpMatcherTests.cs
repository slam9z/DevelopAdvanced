using Microsoft.VisualStudio.TestTools.UnitTesting;
using Algorithm.String;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlgorithmTests;

namespace Algorithm.String.Tests
{
    [TestClass()]
    public class KmpMatcherTests
    {
        [TestMethod()]
        public void ComputePrefixFunctionTest()
        {

            var kmp = new KmpMatcher();
            var prefixFunction = kmp.ComputePrefixFunction("ababababca");
            TestHepler.PrintList(prefixFunction);

        }
    }
}