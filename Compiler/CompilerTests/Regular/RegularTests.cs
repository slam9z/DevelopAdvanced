using Microsoft.VisualStudio.TestTools.UnitTesting;
using Compiler.Regular;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.Regular.Tests
{
    [TestClass()]
    public class RegularTests
    {
        [TestMethod()]
        public void MatchTest()
        {
            var reg = new Regular("a(a|b)cd*b");

            Assert.AreEqual(reg.Match("abcdb"), true);
            Assert.AreEqual(reg.Match("abcdba"), false);
        }
    }
}