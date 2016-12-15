using Peg.Base;
using Peg.Generator;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Peg.Markdown
{
    enum MarkdownKind
    {
        Heading,
        OrderedList,
        BulletList,
        ListItem,

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
            [MarkdownKind.BulletList] = new CodeTemplate(MarkdownKind.BulletList, "<ul>\n${ListBody}</ul>\n"),
            [MarkdownKind.OrderedList] = new CodeTemplate(MarkdownKind.OrderedList, "<ol>\n${ListBody}</ol>\n"),
            [MarkdownKind.ListItem] = new CodeTemplate(MarkdownKind.OrderedList, "<li>${ListItemBody}</li>\n"),

        };


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

            OpenOutFile("", ".html");
            GenHtmlForBlock();
        }

        #endregion

        public MarkdownHtmlGenerator()
        {

        }


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
                case EMarkdown.BulletList:


                case EMarkdown.OrderedList:
                    content = GetList(child);

                    break;
                default:

                    break;
            }

            _outFile.Write(content);

        }

        #region list

        private string GetList(PegNode node)
        {
            var content = string.Empty;
            var listHead = string.Empty;

            if (node.id_ == (int)EMarkdown.OrderedList)
            {
                listHead = templates[MarkdownKind.OrderedList].sCodeTemplate;
            }
            else
            {
                listHead = templates[MarkdownKind.BulletList].sCodeTemplate;
            }


            var listbody = new List<string>();

            var listItemTight = PegUtils.FindNode(node, (int)EMarkdown.ListItemTight);
            if (listItemTight != null)
            {
                for (; listItemTight != null; listItemTight = PegUtils.FindNodeNext(listItemTight, (int)EMarkdown.ListItemTight))
                {
                    listbody.Add(GetListItem(listItemTight));
                }

            }

            var listItem = PegUtils.FindNode(node, (int)EMarkdown.ListItem);
            if (listItem != null)
            {
                for (; listItem != null; listItem = PegUtils.FindNodeNext(listItem, (int)EMarkdown.ListItem))
                {
                    listbody.Add(GetListItem(listItem));
                }
            }


            content = listHead.Replace("${ListBody}", string.Join("", listbody));

            return content;
        }

        private string GetListContinuation(PegNode node)
        {
            var listHead = string.Empty;

            var listbody = new List<string>();
            var listBlockNode = PegUtils.FindNode(node, (int)EMarkdown.ListBlock);

            //TODO 暂时简化点,只检查第一个元素
            var enumeratorReg = new Regex("[0-9]+.");
            var bulletReg = new Regex("[+*-]");


            if (listBlockNode.child_.id_ == (int)EMarkdown.Symbol)
            {
                listHead = templates[MarkdownKind.BulletList].sCodeTemplate;
            }
            else
            {
                listHead = templates[MarkdownKind.OrderedList].sCodeTemplate;
            }

            if (listBlockNode != null)
            {

                for (; listBlockNode != null; listBlockNode = PegUtils.FindNodeNext(listBlockNode, (int)EMarkdown.ListBlock))
                {
                    var blockstring = GetListBlock(listBlockNode);

                    var bulletMatch = bulletReg.Match(blockstring);
                    if (bulletMatch != Match.Empty)
                    {
                        if (bulletMatch.Index == 0)
                        {
                            blockstring = blockstring.Remove(bulletMatch.Index, bulletMatch.Length);
                        }

                    }

                    var enumeratorMatch = enumeratorReg.Match(blockstring);
                    if (enumeratorMatch != Match.Empty)
                    {
                        if (enumeratorMatch.Index == 0)
                        {
                            blockstring = blockstring.Remove(enumeratorMatch.Index, enumeratorMatch.Length);
                        }
                    }


                    listbody.Add(
                        templates[MarkdownKind.ListItem].sCodeTemplate
                        .Replace("${ListItemBody}", blockstring));
                }
            }

            return listHead.Replace("${ListBody}", string.Join("", listbody));
        }

        private string GetListItem(PegNode node)
        {
            //TODO  inline
            string itemString;

            var listBlock = PegUtils.FindNode(node, (int)EMarkdown.ListBlock);
            itemString = GetListBlock(listBlock);

            var contiNode = PegUtils.FindNode(node, (int)EMarkdown.ListContinuationBlock);
            if (contiNode != null)
            {
                itemString = itemString + "\n" + GetListContinuation(contiNode);
            }

            return templates[MarkdownKind.ListItem].sCodeTemplate.Replace("${ListItemBody}", itemString);
        }


        private string GetListBlock(PegNode node)
        {
            //TODO  inline
            var itemString = node.GetAsString(_src);
            return itemString;
        }

        #endregion

        #region header

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
                var headText = heading.GetAsString(_src).Replace("#", "").Trim();
                content = CreateHeading(headerLevel, headText);
            }
            else
            {
                var selectBottom = PegUtils.FindNode(heading, (int)EMarkdown.SetextBottom1, (int)EMarkdown.SetextBottom2);
                var headText = PegUtils.GetAsString(_src, heading, selectBottom).Trim();
                content = CreateHeading(2, headText);
            }
            return content;
        }


        #endregion

        bool OpenOutFile(string sGenSubDirectory, string fileEnding)
        {
            try
            {
                var filename = _generatorParams.sourceFileTitle_;
                if (!filename.EndsWith(fileEnding))
                {
                    filename = filename + fileEnding;
                }

                var htmlDir = PegUtils.MakeFileName(
                    "", _generatorParams.outputDirectory_, sGenSubDirectory);

                _outputFileName = PegUtils.MakeFileName(
                       filename, htmlDir);

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
