using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.Simple.Lexer
{
    public class Token
    {
        public readonly int Tag;

        public Token(int tag)
        {
            Tag = tag;
        }
    }
}
