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
    public class MarkdownHtmlConverter
    {

        private TextWriter errOut;
        private PegNode root;

        private string _outputFolder;
        private string _outputFileName;


        public MarkdownHtmlConverter()
        {
            errOut = Console.Out;
        }

        bool CreatePeg(string src)
        {

            try
            {
                var pg = new Markdown();
                pg.Construct(src, errOut);
                pg.SetSource(src);
                pg.SetErrorDestination(errOut);
                bool bMatches = pg.Doc();
                root = pg.GetRoot();
                return bMatches;
            }
            catch (PegException exp)
            {
                throw;
            }
        }


        public void CreateHtml(string markdownSrc)
        {


            if (CreatePeg(markdownSrc))
            {

                var postProcParams = new ParserPostProcessParams(
                    _outputFolder
                    , _outputFileName, "Markdown", root, markdownSrc, errOut);

                var postProcessor = (IParserPostProcessor)new MarkdownHtmlGenerator();
                if (postProcessor != null)
                {
                    postProcessor.Postprocess(postProcParams);
                }
            }
        }


        public void ConvertToFile(string sourcePath, string outputPath = null)
        {
            if (string.IsNullOrWhiteSpace(outputPath))
            {
                outputPath = sourcePath;
            }

            _outputFolder = Path.GetDirectoryName(outputPath);

            _outputFileName = Path.GetFileNameWithoutExtension(outputPath);
            if (string.IsNullOrWhiteSpace(_outputFileName))
            {
                _outputFileName = Path.GetFileNameWithoutExtension(sourcePath);
            }

            CreateHtml(File.ReadAllText(sourcePath));
        }


        //public string ConvertFile(string sourcePath)
        //{
        //    return Convert(File.ReadAllText(sourcePath));
        //}

        ///// <summary>
        ///// Converts an Html string to a Markdown string
        ///// </summary>
        ///// <param name="html">The Html string you wish to convert</param>
        ///// <returns>A Markdown representation of the passed in Html</returns>
        //public string Convert(string markdown)
        //{
        //    var html = string.Empty;

        //    return html;
        //}
    }
}
