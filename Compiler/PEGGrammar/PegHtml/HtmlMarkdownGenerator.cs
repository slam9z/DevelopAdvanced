using Peg.Base;
using Peg.Generator;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Peg.Html
{
    enum HtmlKind
    {
        Heading,
        OrderedList,
        BulletList,
        ListItem,

    }


    struct CodeTemplate
    {
        internal CodeTemplate(HtmlKind kind, string codeTemplate)
        {
            Kind = kind;
            sCodeTemplate = codeTemplate;
        }
        internal HtmlKind Kind;
        internal string sCodeTemplate;
    }


    public class HtmlMarkdownGenerator : IParserPostProcessor
    {

        private PegNode _root;
        private string _src;
        private ParserPostProcessParams _generatorParams;

        private string _outputFileName;
        private StreamWriter _outFile;




        Dictionary<HtmlKind, CodeTemplate> templates = new Dictionary<HtmlKind, CodeTemplate>
        {
            [HtmlKind.Heading] = new CodeTemplate(HtmlKind.Heading, "<h${HeaderLevel}>&{Html}</h${HeaderLevel}>\n"),
            [HtmlKind.BulletList] = new CodeTemplate(HtmlKind.BulletList, "<ul>\n${ListBody}</ul>\n"),
            [HtmlKind.OrderedList] = new CodeTemplate(HtmlKind.OrderedList, "<ol>\n${ListBody}</ol>\n"),
            [HtmlKind.ListItem] = new CodeTemplate(HtmlKind.OrderedList, "<li>${ListItemBody}</li>\n"),

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

        public HtmlMarkdownGenerator()
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

            switch ((EHtml)child.id_)
            {
                case EHtml.HtmlBlockH1:
                case EHtml.HtmlBlockH2:
                case EHtml.HtmlBlockH3:
                case EHtml.HtmlBlockH4:
                case EHtml.HtmlBlockH5:
                case EHtml.HtmlBlockH6:

                    content = GenHeading(child);
                    break;
                case EHtml.HtmlBlockOl:


                case EHtml.HtmlBlockUl:
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

            if (node.id_ == (int)EHtml.HtmlBlockOl)
            {
                listHead = templates[HtmlKind.OrderedList].sCodeTemplate;
            }
            else
            {
                listHead = templates[HtmlKind.BulletList].sCodeTemplate;
            }


            var listbody = new List<string>();

            var listItemTight = PegUtils.FindNode(node, (int)EHtml.HtmlBlockLi);
            if (listItemTight != null)
            {
                for (; listItemTight != null; listItemTight = PegUtils.FindNodeNext(listItemTight, (int)EHtml.HtmlBlockLi))
                {
                    listbody.Add(GetListItem(listItemTight));
                }

            }

            var listItem = PegUtils.FindNode(node, (int)EHtml.HtmlBlockLi);
            if (listItem != null)
            {
                for (; listItem != null; listItem = PegUtils.FindNodeNext(listItem, (int)EHtml.HtmlBlockLi))
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
            var listBlockNode = PegUtils.FindNode(node, (int)EHtml.HtmlBlockOl);

            //TODO 暂时简化点,只检查第一个元素
            var enumeratorReg = new Regex("[0-9]+.");
            var bulletReg = new Regex("[+*-]");


            if (listBlockNode.child_.id_ == (int)EHtml.Symbol)
            {
                listHead = templates[HtmlKind.BulletList].sCodeTemplate;
            }
            else
            {
                listHead = templates[HtmlKind.OrderedList].sCodeTemplate;
            }

            if (listBlockNode != null)
            {

                for (; listBlockNode != null; listBlockNode = PegUtils.FindNodeNext(listBlockNode, (int)EHtml.HtmlBlockOl))
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
                        templates[HtmlKind.ListItem].sCodeTemplate
                        .Replace("${ListItemBody}", blockstring));
                }
            }

            return listHead.Replace("${ListBody}", string.Join("", listbody));
        }

        private string GetListItem(PegNode node)
        {
            //TODO  inline
            string itemString;

            var listBlock = PegUtils.FindNode(node, (int)EHtml.HtmlBlockOl);
            itemString = GetListBlock(listBlock);

            var contiNode = PegUtils.FindNode(node, (int)EHtml.HtmlBlockOl);
            if (contiNode != null)
            {
                itemString = itemString + "\n" + GetListContinuation(contiNode);
            }

            return templates[HtmlKind.ListItem].sCodeTemplate.Replace("${ListItemBody}", itemString);
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
            return templates[HtmlKind.Heading].sCodeTemplate
                .Replace("${HeaderLevel}", headerlevel.ToString())
                .Replace("&{Html}", html);
        }

        string GenHeading(PegNode heading)
        {
            var content = string.Empty;

            if (heading.child_.id_ == (int)EHtml.HtmlBlockH1)
            {
                var headerLevelStr = PegUtils.FindNode(heading.child_, (int)EHtml.HtmlBlockH1)
                    .GetAsString(_src);
                var headerLevel = headerLevelStr.Length;
                var headText = heading.GetAsString(_src).Replace("#", "").Trim();
                content = CreateHeading(headerLevel, headText);
            }
            else
            {
                var selectBottom = PegUtils.FindNode(heading, (int)EHtml.HtmlBlockH1, (int)EHtml.HtmlBlockH1);
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
