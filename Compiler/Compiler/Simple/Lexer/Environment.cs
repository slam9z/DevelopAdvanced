using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.Simple.Lexer
{
    public class Environment
    {
        public Hashtable _table;

        private Environment _prev;

        public Environment(Environment prev)
        {
            _prev = prev;
            _table = new Hashtable();
        }

        public void Add(string name, Symbol symbol)
        {
            _table.Add(name, symbol);
        }

        public Symbol Get(string name)
        {
            for (var env = this; env != null; env = env._prev)
            {
                var s = (Symbol)env._table[name];
                if (s != null)
                {
                    return s;
                }
            }

            return null;
        }

    }
}
