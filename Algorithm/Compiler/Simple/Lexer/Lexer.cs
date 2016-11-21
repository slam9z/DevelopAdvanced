using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.Simple.Lexer
{
    public class Lexer
    {
        public int Line { get; set; } = 1;

        private char _peak = ' ';

        private Hashtable _words = new Hashtable();

        public Lexer()
        {
            Reserve(new WordToken(Tags.TRUE, "true"));
            Reserve(new WordToken(Tags.FALSE, "false"));
        }

        public Token Scan()
        {
            while (true)
            {
                _peak = (char)Console.Read();

                if (_peak == ' ' || _peak == '\t') continue;

                else if (_peak == '\n') Line = Line + 1;

                else break;
            }

            if (char.IsDigit(_peak))
            {
                int v = 0;
                do
                {
                    v = 10 * v + (int)char.GetNumericValue(_peak);
                    _peak = (char)Console.Read();
                } while (char.IsDigit(_peak));

                return new NumberToken(v);
            }

            if (char.IsLetter(_peak))
            {
                var b = new StringBuilder();
                do
                {
                    b.Append(_peak);
                    _peak = (char)Console.Read();
                } while (char.IsLetterOrDigit(_peak));

                var s = b.ToString();
                var w = (WordToken)_words[s];
                if (w != null)
                {
                    return w;
                }

                w = new WordToken(Tags.ID, s);
                _words.Add(s, w);
                return w;
            }

            var t = new Token(_peak);
            _peak = ' ';
            return t;

        }


        private void Reserve(WordToken word)
        {
            _words.Add(word.Tag, word);
        }

    }
}
