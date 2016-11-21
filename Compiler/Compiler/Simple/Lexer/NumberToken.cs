
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.Simple.Lexer
{
    public class NumberToken : Token
    {
        public readonly int Value;

        public NumberToken(int value) : base(Tags.NUM)
        {
            Value = value;
        }
    }
}
