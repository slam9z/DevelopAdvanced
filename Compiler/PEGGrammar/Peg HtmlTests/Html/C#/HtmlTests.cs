using Microsoft.VisualStudio.TestTools.UnitTesting;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using Peg.Base;
using Peg.Generator;

namespace Peg.Html.Tests
{
    [TestClass()]
    public class HtmlTests
    {


        private string _inputFolder = "..\\..\\..\\Peg HtmlTests\\Html\\input\\";

        private string _baseFolder = "..\\..\\..\\PegHtml\\Html\\";


        private string pegSrc;

        private TextWriter errOut;
        private PegNode root;


        private string _markdownFile;


        private string htmlFile;

        private string _htmlPEGFile;



        [TestInitialize()]
        public void Init()
        {
            errOut = Console.Out;

            _htmlPEGFile = _baseFolder + "html.peg.txt";
        }


        [TestMethod()]
        public void CreateHtml()
        {

            pegSrc = File.ReadAllText(_htmlPEGFile);
            root = PegRunUtils.ParsePegGrammar(pegSrc, errOut);
            var postProcParams = new ParserPostProcessParams(GetOutputDirectory(), "Html", "Html", root, pegSrc, errOut);

            PegRunUtils.CreatePegCodeFile(postProcParams);

        }




        [TestMethod()]
        public void HtmlTest()
        {
            RunInstanceTest("html.html");
        }


        [TestMethod()]
        public void SpecialCharTest()
        {
            RunInstanceTest("specialchar.html");
        }


        private string GetOutputDirectory()
        {
            return _baseFolder;
        }


        private void RunInstanceTest(string relativePath)
        {
            var markdownSrc = File.ReadAllText(Path.Combine(_inputFolder, relativePath));
            var markdown = new Peg.Html.Html(markdownSrc, errOut);
            Console.WriteLine($"source length: {markdownSrc.Length}");
            Console.WriteLine($"source : {markdownSrc}");
            Assert.AreEqual(true, markdown.Doc());
        }
    }
}