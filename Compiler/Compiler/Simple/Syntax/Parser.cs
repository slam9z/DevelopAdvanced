using Compiler.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.Simple.Syntax
{

    public class Parser
    {
        private static int s_lookahead;

        public Parser()
        {
            s_lookahead = Console.Read();
        }

        public void Expression()
        {
            Statement();

            while (true)
            {
                if (s_lookahead == '+')
                {
                    Match('+');
                    Statement();
                    Console.Write('+');
                }

                else if (s_lookahead == '-')
                {
                    Match('-');
                    Statement();
                    Console.Write('-');
                }

                else
                {
                    return;
                }
            }

        }

        private void Statement()
        {
            if (char.IsDigit((char)s_lookahead))
            {
                Console.Write((char)s_lookahead);
                Match(s_lookahead);
            }
            else
            {
                throw new SyntaxException();
            }

        }

        private void Match(int t)
        {
            if (s_lookahead == t)
            {
                s_lookahead = Console.Read();
            }
            else
            {
                throw new SyntaxException();
            }
        }

    }
}
