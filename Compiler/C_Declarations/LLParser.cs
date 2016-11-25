using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Threading;

namespace AddressingModes
{
    public class LLParser
    {
        public SyntaxStack Stack = new SyntaxStack(500);
        public Lexer lexx = null;
        Production[,] M = null;
        TextBox txt = null;
        Language lg = null;
        TokenLookup.TokenKind token = TokenLookup.TokenKind.UnKnown;
        public LLParser(string stream,Language _lg )
        {
            lexx = new Lexer(stream);
            lg = _lg;
            M = lg.M;
            Stack.Push(lg.Term[0]);
            List<InheritedAttribute> ls = new List<InheritedAttribute>();
            ls.Add(new InheritedAttribute(lg.FindNTermByName(lg.Prod[0].lhs.Name),null));
            Stack.Push(ls[0]);
            token = lexx.getToken();
        }
        
        static int nLines = 0;

        public string _FormatErrMsg(string ErrMsg, TokenLookup.TokenKind token)
        {
            string _errmsg = "";

            _errmsg += " found token (" + token + ")  " + ErrMsg + " at line (" + (EAGrammar._line) + ") \n";

            return _errmsg;
        }

        static int _curline = 1;

        public bool Parse()
        {
            bool fret = false;
            Production pp = null;
            
            object sym = Stack.Top();
            
            SynthesizedAttribute syn = sym as SynthesizedAttribute;
            if (syn != null)
            {
                if (syn.FireAction != null)
                {
                    fret = syn.FireAction(Stack, pp);
                    if (fret == false)
                        return fret;
                }
                Stack.Pop();
                //DumpStack();
                return true;
            }
            
            SemanticAction sma = sym as SemanticAction;
            if (sma != null)
            {
                Stack.Pop();
                sym = Stack.Top();
            }
            InheritedAttribute inh = sym as InheritedAttribute;
            if (inh != null)
            {
                if (token != TokenLookup.TokenKind.UnKnown)
                    pp = (Production)M[(int)inh.nTerm.Category, (int)token];
                if (pp == null)
                {
                    MessageBox.Show(" syntax error !");
                    return false;
                }
                if (sma != null)
                {
                   fret = sma.FireAction(Stack, pp, lexx.TokenValue);  // copy inherited top down into next production fields
                   if (fret == false)
                       return fret;
                }
                Stack.Pop();

                for (int j = pp.rhs.Count - 1; j >= 0; j--)
                {
                    Terminal tt = pp.rhs[j] as Terminal;
                    if (tt != null && tt.Token == TokenLookup.TokenKind.Lambda)
                        continue;
                     if (pp.synth != null && pp.synth[j] != null)
                     {
                         if (pp.synth[j] is SDeclTypeAttribute)
                         {
                             SDeclTypeAttribute sa = (SDeclTypeAttribute)pp.synth[j] as SDeclTypeAttribute;
                             Stack.Push(new SDeclTypeAttribute(sa));
                         }
                         pp.synth[j].Clear();
                    
                      } 
 
                    if (tt != null)
                        Stack.Push(pp.rhs[j]);

                    if (pp.inh != null && pp.inh[j] != null)
                    {
                        if (pp.inh[j] is IDeclTypeAttribute)
                        {
                            IDeclTypeAttribute ie = pp.inh[j] as IDeclTypeAttribute;
                            Stack.Push(new IDeclTypeAttribute(ie));
                        }
                        else
                            Stack.Push(new InheritedAttribute(pp.inh[j]));
                        pp.inh[j].Clear();
                    }
                    //}

                    if (pp.action != null && pp.action[j] != null)
                        Stack.Push(pp.action[j]);

                }
                //DumpStack();
            }
            else // is  a Terminal or EOI
            {
                Terminal tt = sym as Terminal;
                if (token == TokenLookup.TokenKind.EOI)
                {
                    _curline=1;
                    MessageBox.Show("stream successfully parsed ");
                    return false;
                }
                else
                    if (token == tt.Token)
                    {
                        if (sma != null)
                        {
                            fret = sma.FireAction(Stack, null, lexx.TokenValue);
                        
                            if (fret == false)
                                return fret;
                        }
                        Stack.Pop();
                        token = lexx.getToken();
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("Unexpected token : " + token + ", expected : " + tt.Token) ;
                        return false;
                    }
            }
            return true;
        }
    }
}
