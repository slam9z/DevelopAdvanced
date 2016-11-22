using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegularExpression;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RegularExpression.Tests
{
    [TestClass()]
    public class RegExTests
    {
        [TestMethod()]
        public void FindMatchTest()
        {
            var pattern = "a(a|b)cd*b";

            var string1 = "abcdb";
            var string2 = "abcdba";


            var regex = new Regex(pattern);

            var reg = new RegEx();

            var stringBuilder = new StringBuilder();

            reg.CompileWithStats(pattern, stringBuilder);

            Console.WriteLine(stringBuilder);

            var string1FoundBeginAt = 0;
            var string1FoundEndAt = 0;

            var string2FoundBeginAt = 0;
            var string2FoundEndAt = 0;

            Assert.AreEqual(
                reg.FindMatch(
                    string1, 0, string1.Length-1
                    , ref string1FoundBeginAt, ref string1FoundEndAt), true);

            Assert.AreEqual(
                reg.FindMatch(
                    string2, 0, string2.Length-1
                    , ref string2FoundBeginAt, ref string2FoundEndAt), true);
        }
    }
}