using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;

namespace AddressingModes
{
    public class Lexer 
    {
            int _index = 0;
            string _stream = null;
            public string TokenValue;
            public string NextTokenValue;
            public Hashtable keywords;
            public Lexer(string stream)
            {
                keywords = new Hashtable();
                TokenLookup.TokenKind[] tokens = (TokenLookup.TokenKind[])Enum.GetValues(typeof(TokenLookup.TokenKind));
                foreach (TokenLookup.TokenKind tok in tokens)
                        if (tok <= TokenLookup.TokenKind.UNSIGNED)
                        keywords.Add(tok.ToString().ToUpper(), tok);
                _stream = stream;
             }

            public TokenLookup.TokenKind getToken()
            {
                bool alpha = false;
                TokenValue = "";

                while (_index < _stream.Length && (_stream[_index] == ' ' || _stream[_index] == '\t' || _stream[_index] == '\n' || _stream[_index] == '\r'))
                  _index++;
                if (_index == _stream.Length || _stream[_index] == '$')
                    return TokenLookup.TokenKind.EOI;

                if ((_stream[_index]  >= '0' && _stream[_index] <= '9'))
                {
                    TokenValue += _stream[_index++]; // 1st char always 0..9 eg 0FFh 1234 1010b
                    while ((_stream[_index] >= '0' && _stream[_index] <= '9') || 
                           (_stream[_index] >= 'A' && _stream[_index] <= 'F') ||
                           (_stream[_index] >= 'a' && _stream[_index] <= 'f'))
                        TokenValue += _stream[_index++];

                    return TokenLookup.TokenKind.Integer_Constant;
                }

                if ((_stream[_index] >= 'a' && _stream[_index] <= 'z') || (_stream[_index] >= 'A' && _stream[_index] <= 'Z') || _stream[_index] == '_')
                {
                    if (_stream[_index] == '_')
                        TokenValue += _stream[_index++];

                    while ((_stream[_index] >= 'a' && _stream[_index] <= 'z') || (_stream[_index] >= 'A' && _stream[_index] <= 'Z')
                           || (_stream[_index] >= '0' && _stream[_index] <= '9') || _stream[_index] == '_')
                        {
                            if (_stream[_index] != '_')
                                 alpha = true;
                             TokenValue += _stream[_index++];
                        }
                    if (alpha == false || _stream[_index-1] == '_') // no alpha chars or lasty underline means error
                        return TokenLookup.TokenKind.UnKnown;

                   object obj = keywords[TokenValue.ToUpper()];
                   if (obj != null)
                        return (TokenLookup.TokenKind) obj;
                   return TokenLookup.TokenKind.Id;
                }
                switch (_stream[_index++])
                {
                    case ('['):
                        return TokenLookup.TokenKind.LSParen; 
                    case (']'):
                        return TokenLookup.TokenKind.RSParen;
                    case ('('):
                        return TokenLookup.TokenKind.LParen;
                    case (')'):
                        return TokenLookup.TokenKind.RParen;
                    case (';'):
                        return TokenLookup.TokenKind.SemiColon;
                    case (','):
                        return TokenLookup.TokenKind.Comma;
                    case ('*'):
                        return TokenLookup.TokenKind.Star;
                }
                return TokenLookup.TokenKind.UnKnown;
            }
        }
}