using Peg.Generator;
using System;
using System.Collections.Generic;
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


    public class MarkdownHtmlGenerator
    {
        private TreeContext _context;


        CodeTemplate[] templates = {
            new CodeTemplate( MarkdownKind.Heading,@"<h${HeaderLevel}>&{Html}<h${HeaderLevel}>"),
        };

        public MarkdownHtmlGenerator(TreeContext context)
        {



        }

 
    }
}
