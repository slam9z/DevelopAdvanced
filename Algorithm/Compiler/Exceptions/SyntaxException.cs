using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.Exceptions
{
    public class SyntaxException : Exception
    {
        private string _message;

        public override string Message
        {
            get
            {
                return _message;
            }
        }

        public SyntaxException()
        {
            _message = "syntax error";
        }
    }
}
