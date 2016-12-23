using Peg.Base;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peg.Generator
{
    public class PegRunUtils
    {

        public static PegNode ParsePegGrammar(string src, TextWriter output)
        {
            try
            {
                var pg = new PegGrammarParser();
                pg.Construct(src, output);
                pg.SetSource(src);
                pg.SetErrorDestination(output);
                bool bMatches = pg.peg_module();
                return pg.GetRoot();
            }
            catch (PegException exp)
            {
                return null;
            }
        }

        public static void CreatePegCodeFile(ParserPostProcessParams postProcParams)
        {

            var postProcessor = (IParserPostProcessor)new PegParserGenerator();
            if (postProcessor != null)
            {
                postProcessor.Postprocess(postProcParams);
            }

        }
    }
}
