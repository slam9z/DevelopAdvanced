using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.Simple.Lexer
{
    public class WordToken : Token
    {
        public readonly string Value;

        public WordToken(int tag,string value):base(tag)
        {
            Value = value;
        }
    }
}
