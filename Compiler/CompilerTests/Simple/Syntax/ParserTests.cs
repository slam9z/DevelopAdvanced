using Microsoft.VisualStudio.TestTools.UnitTesting;
using Compiler.Simple.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Compiler.Simple.Syntax.Tests
{

    [TestClass()]
    public class ParserTests
    {
        [TestMethod()]
        public void ParserTest()
        {
            var input = "1+2-1";
            Console.WriteLine($"\n input:{input} \n");

            var sr = new StringReader(input);
            Console.SetIn(sr);
            var parser = new Parser();
            parser.Expression();
        }



    }
}