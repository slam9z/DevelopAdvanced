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
    public class MarkdownHtmlGeneratorTests
    {

        private string _baseFolder = "..\\..\\..\\PegMarkdown\\Markdown\\";

        private string _inputBaseFolder;


        private string markdownSrc;

        private TextWriter errOut;
        private PegNode root;


        private string _markdownFile;



        [TestInitialize()]
        public void Init()
        {
            errOut = Console.Out;
            _inputBaseFolder += _baseFolder + "input";

        }

        bool RunImpl(string src, TextWriter Fout)
        {

            try
            {
                var pg = new Markdown();
                pg.Construct(src, Fout);
                pg.SetSource(src);
                pg.SetErrorDestination(Fout);
                bool bMatches = pg.Doc();
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

        private string GetOutputDirectory()
        {
            return _baseFolder;
        }


        [TestMethod()]
        public void CreateHtmlTest()
        {

            _markdownFile = Path.Combine(_inputBaseFolder, "header.md");
            markdownSrc = File.ReadAllText(_markdownFile);

            if (RunImpl(markdownSrc, errOut))
            {

                var postProcParams = new ParserPostProcessParams(
                    GetOutputDirectory()
                    , Path.GetFileNameWithoutExtension(_markdownFile), "Markdown", root, markdownSrc, errOut);

                var postProcessor = (IParserPostProcessor)new MarkdownHtmlGenerator();
                if (postProcessor != null)
                {
                    postProcessor.Postprocess(postProcParams);
                }
            }
        }
    }
}