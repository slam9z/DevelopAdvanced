using Peg.Base;
using Peg.Generator;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peg.Markdown
{
    enum MarkdownKind
    {
        Heading,
    }


    struct CodeTemplate
    {
        internal CodeTemplate(MarkdownKind kind, string codeTemplate)
        {
            Kind = kind;
            sCodeTemplate = codeTemplate;
        }
        internal MarkdownKind Kind;
        internal string sCodeTemplate;
    }


    public class MarkdownHtmlGenerator : IParserPostProcessor
    {

        private PegNode _root;
        private string _src;
        private ParserPostProcessParams _generatorParams;

        private string _outputFileName;
        private StreamWriter _outFile;




        Dictionary<MarkdownKind, CodeTemplate> templates = new Dictionary<MarkdownKind, CodeTemplate>
        {
            [MarkdownKind.Heading] = new CodeTemplate(MarkdownKind.Heading, "<h${HeaderLevel}>&{Html}</h${HeaderLevel}>\n"),
        };

        public MarkdownHtmlGenerator()
        {

        }

        #region IParserPostProcessor

        public string DetailDesc
        {
            get
            {
                { return "Convert to html"; }
            }
        }

        public string ShortDesc
        {
            get
            {
                { return "Convert to html"; }
            }
        }

        public string ShortestDesc
        {
            get
            {
                return "Convert markdown to html";
            }
        }

        public void Postprocess(ParserPostProcessParams postProcessorParams)
        {
            _root = postProcessorParams.root_;
            _src = postProcessorParams.src_;
            _generatorParams = postProcessorParams;

            OpenOutFile("Html", ".html");
            GenHtmlForBlock();
        }

        #endregion
        PegNode GetBlockFromRoot(PegNode root)
        {
            return root.child_;
        }

        void GenHtmlForBlock()
        {
            // parent_.outFile_.WriteLine("		#region Grammar Rules");
            PegNode firstRule = GetBlockFromRoot(_root);
            for (PegNode q = firstRule; q != null; q = q.next_)
            {

                GenHtmlForBlock(q);
            }
            //  parent_.outFile_.WriteLine("		#endregion Grammar Rules");

            _outFile.Close();

        }

        void GenHtmlForBlock(PegNode block)
        {
            var child = block.child_;
            var content = string.Empty;

            switch ((EMarkdown)child.id_)
            {
                case EMarkdown.Heading:
                    content = GenHeading(child);
                    break;

                default:
                    break;
            }

            _outFile.Write(content);


        }

        string CreateHeading(int headerlevel, string html)
        {
            return templates[MarkdownKind.Heading].sCodeTemplate
                .Replace("${HeaderLevel}", headerlevel.ToString())
                .Replace("&{Html}", html);
        }

        string GenHeading(PegNode heading)
        {
            var content = string.Empty;

            if (heading.child_.id_ == (int)EMarkdown.AtxHeading)
            {
                var headerLevelStr = PegUtils.FindNode(heading.child_, (int)EMarkdown.AtxStart)
                    .GetAsString(_src);
                var headerLevel = headerLevelStr.Length;
                var headText = heading.GetAsString(_src).Replace(headerLevelStr, "").Trim();
                content = CreateHeading(headerLevel, headText);
            }
            else
            {
                content = heading.GetAsString(_src);
            }
            return content;
        }



        bool OpenOutFile(string sGenSubDirectory, string fileEnding)
        {
            try
            {
                string htmlDir =
                    PegUtils.MakeFileName("", _generatorParams.outputDirectory_, sGenSubDirectory);
                _outputFileName = PegUtils.MakeFileName(
                       _generatorParams.sourceFileTitle_ + fileEnding, htmlDir);
                if (!Directory.Exists(htmlDir))
                {
                    Directory.CreateDirectory(htmlDir);
                }
                _outFile = new StreamWriter(_outputFileName);
            }
            catch (Exception e)
            {
                _outFile.WriteLine("FATAL from <PEG_GENERATOR> FILE:'{0}' could not be opened (%s)", _outputFileName, e.Message);
                return false;
            }
            return true;
        }
    }
}
