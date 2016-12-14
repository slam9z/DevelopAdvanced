using Microsoft.VisualStudio.TestTools.UnitTesting;
using Peg.Base;
using Peg.Generator;
using Peg.Markdown;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peg.Markdown.Tests
{
    [TestClass()]
    public class MarkdownHtmlConverterTests
    {

        private string _baseFolder = "..\\..\\..\\PegMarkdown\\Markdown\\";

        private string _inputBaseFolder;


        [TestInitialize()]
        public void Init()
        {
            _inputBaseFolder += _baseFolder + "input";

        }



        [TestMethod()]
        public void CreateHeaerTest()
        {
            var converter = new MarkdownHtmlConverter();
            converter.ConvertToFile(Path.Combine(_inputBaseFolder, "header.md"), Path.Combine(_inputBaseFolder, "Html", "header"));
        }


    }
}