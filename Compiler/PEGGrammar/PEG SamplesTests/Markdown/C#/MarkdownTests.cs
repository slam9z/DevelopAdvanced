using Microsoft.VisualStudio.TestTools.UnitTesting;
using Markdown;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Peg.Samples;
using System.IO;
using Peg.Base;

namespace Markdown.Tests
{
    [TestClass()]
    public class MarkdownTests
    {
        private string _baseFolder = "..\\..\\..\\PegSamples\\Markdown\\";

        private string _inputBaseFolder;


        private string pegSrc;

        private TextWriter errOut;
        private PegNode root;


        private string _markdownFile;

        private string _markdownPEGFile;



        private string htmlFile;

        private string _htmlPEGFile;




        public class OutputWriter : TextWriter
        {
            public override Encoding Encoding
            {
                get
                {
                    return Encoding.UTF8;
                }
            }

            public override void WriteLine(string format, params object[] arg)
            {
                Console.WriteLine(format, arg);
            }

            public override void WriteLine(string message)
            {
                Console.WriteLine(message);
            }
        }

        [TestInitialize()]
        public void Init()
        {
            errOut = new OutputWriter();
            _inputBaseFolder += _baseFolder + "input";


            _markdownPEGFile = _baseFolder + "markdown.peg.transform.txt";
            _htmlPEGFile = _baseFolder + "html.peg.txt";
        }

        bool RunImpl(string src, TextWriter Fout)
        {

            try
            {
                var pg = new PegGrammarParser();
                pg.Construct(src, Fout);
                pg.SetSource(src);
                pg.SetErrorDestination(Fout);
                bool bMatches = pg.peg_module();
                root = pg.GetRoot();
                return bMatches;
            }
            catch (PegException exp)
            {
                // Console.WriteLine($"{exp.Message},{exp.StackTrace}");
                Assert.Fail(exp.Message);
                return false;
            }
        }
        //         errOut_.WriteLine("<{0},{1}>{2}:{3}", lineNo, colNo, sErrKind, sMsg);

        [TestMethod()]
        public void CreateMarkdown()
        {
            _markdownPEGFile = _baseFolder + "markdown.peg.txt";
            pegSrc = File.ReadAllText(_markdownPEGFile);

            if (RunImpl(pegSrc, errOut))
            {

                var postProcParams = new ParserPostProcessParams(GetOutputDirectory(), "Markdown", "Markdown", root, pegSrc, errOut);

                var postProcessor = (IParserPostProcessor)new PegParserGenerator();
                if (postProcessor != null)
                {
                    postProcessor.Postprocess(postProcParams);
                }
            }

        }

        [TestMethod()]
        public void CreateHtml()
        {
            pegSrc = File.ReadAllText(_htmlPEGFile);
            if (RunImpl(pegSrc, errOut))
            {

                var postProcParams = new ParserPostProcessParams(GetOutputDirectory(), "Html", "Html", root, pegSrc, errOut);

                var postProcessor = (IParserPostProcessor)new PegParserGenerator();
                if (postProcessor != null)
                {
                    postProcessor.Postprocess(postProcParams);
                }
            }

        }



        private string GetOutputDirectory()
        {
            return _baseFolder;
        }

        [TestMethod()]
        public void LinkTest()
        {
            var markdownSrc = File.ReadAllText(Path.Combine(_inputBaseFolder, "link.md"));
            var markdown = new Markdown(markdownSrc, errOut);
            Console.WriteLine($"source length: {markdownSrc.Length}");
            Console.WriteLine($"source : {markdownSrc}");
            markdown.Document();
        }

        [TestMethod()]
        public void HtmlTest()
        {
            var markdownSrc = File.ReadAllText(Path.Combine(_inputBaseFolder, "html.html"));
            var markdown = new Html.Html(markdownSrc, errOut);
            Console.WriteLine($"source length: {markdownSrc.Length}");
            Console.WriteLine($"source : {markdownSrc}");
            markdown.Doc();
        }
    }
}