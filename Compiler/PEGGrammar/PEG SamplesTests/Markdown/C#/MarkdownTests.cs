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

        private string _markdownFile;

        private string _markdownPEGFile;
        private string grammarFileName;
        private string src;
        private string markdownSrc;
        private TextWriter errOut;
        private PegNode root;

        public class OutputWriter : TextWriter
        {
            public override Encoding Encoding
            {
                get
                {
                    return Encoding.UTF8;
                }
            }
        }

        [TestInitialize()]
        public void Init()
        {
            errOut = new OutputWriter();
            _markdownFile = _baseFolder + "input\\markdown.md";
            _markdownPEGFile = _baseFolder + "markdown.peg.txt";
            grammarFileName = "Markdown";
            src = File.ReadAllText(_markdownPEGFile);
            markdownSrc = File.ReadAllText(_markdownFile);
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
            catch (PegException)
            {
                return false;
            }
        }


        [TestMethod()]
        public void CreateMarkdown()
        {
            RunImpl(src, null);

            var postProcParams = new ParserPostProcessParams(GetOutputDirectory(), GetSourceFileTitle(), grammarFileName, root, src, errOut);

            var postProcessor = (IParserPostProcessor)new PegParserGenerator();
            if (postProcessor != null)
            {
                postProcessor.Postprocess(postProcParams);
            }

        }

        private string GetSourceFileTitle()
        {
            return "Markdown";
        }

        private string GetOutputDirectory()
        {
            return _baseFolder;
        }

        [TestMethod()]
        public void LinkTest()
        {
            var markdown = new Markdown(markdownSrc, errOut);
            markdown.MarkdownText();
        }
    }
}