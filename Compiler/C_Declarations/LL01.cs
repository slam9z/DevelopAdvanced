using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace AddressingModes
{
    public class Language
    {
        public Hashtable HashNTerm = new Hashtable();
        public Hashtable HashTerm = new Hashtable();
        public List<Production> Prod = new List<Production>();
        public List<NonTerminal> NTerm = new List<NonTerminal>();
        public List<Terminal> Term = new List<Terminal>();

        public Production[,] M = null;

        public Language()
        {
        }
        public NonTerminal FindNTermByName(string name)
        {
            NonTerminal nt = null;
            nt = (NonTerminal)HashNTerm[name];
            if (nt == null)
            {
                MessageBox.Show(" NonTerminal " + name + " does not exist");
                return null;
            }
            return nt;
        }
        public Terminal FindTermByName(string name)
        {
            Terminal t = null;
            t = (Terminal)HashTerm[name];
            if (t == null)
            {
                MessageBox.Show(" Terminal " + name + " does not exist");
                return null;
            }
            return t;
        }

        public bool ComputeProductionFirstSet(Production pp)
        {
            int k = 0;
            bool Continue = false;
            
            k = 0;
            NonTerminal lhs = pp.lhs as NonTerminal;
            do
            {
                Continue = false;
                NonTerminal nt = pp.rhs[k] as NonTerminal;
                if (nt != null)
                {
                    foreach (TokenLookup.TokenKind tok in nt.First)
                    {
                        if (tok != TokenLookup.TokenKind.Lambda)
                        {
                            Production p = (Production)M[(int)lhs.Category, (int)tok];
                            if (p != null && p.prodno != pp.prodno)
                            {
                                MessageBox.Show( "duplicate table entry productions : " + p.prodno + " and " + pp.prodno + 
                                                " category " + lhs.Category + " token " + tok);
                            }
                            else
                              M[(int)lhs.Category, (int)tok] = pp;
                        }
                        else
                            Continue = true;
                    }
                }
                else
                {
                    Terminal t = pp.rhs[k] as Terminal;
                    if (t.Token != TokenLookup.TokenKind.Lambda)
                    {
                        Production p = (Production)M[(int)lhs.Category, (int)t.Token];
                        if (p != null && p.prodno != pp.prodno)
                            {
                            MessageBox.Show( "duplicate table entry productions : " + p.prodno + " and " + pp.prodno + 
                                    " category " + lhs.Category + " token " + t.Token);
                            }
                            else
                               M[(int)lhs.Category, (int)t.Token] = pp;
                    }
                    else
                        Continue = true;
                }
                k = k + 1;
            } while (Continue && k <= pp.rhs.Count - 1);
            return Continue;
        }

        /// <summary>
        /// 预测分析表
        /// </summary>
        public void FillTable()
        {
            bool FoundLambda = false;
            Production lpp = null;

            M = (Production[,])new Production[NTerm.Count, Term.Count];

            foreach (Production pp in Prod)
            {
                FoundLambda = ComputeProductionFirstSet(pp);
                if (FoundLambda)
                {
                    NonTerminal lhs = pp.lhs as NonTerminal;
                    lpp = FindLambdaProduction(lhs);
                    if (lpp == null)
                        continue;
                    foreach (TokenLookup.TokenKind tok in lhs.Follow)
                    {
                        if (tok == TokenLookup.TokenKind.UnKnown)
                            continue;
                        Production p = (Production)M[(int)lhs.Category, (int)tok]; // first set has precedence
                        if (p == null)
                        {
                            M[(int)lhs.Category, (int)tok] = lpp;
                        //        MessageBox.Show("duplicate table entry productions : " + p.prodno + " and " + pp.prodno +
                        //            " category " + lhs.Category + " token " + tok);
                        }
                        //else
                    }
                }
            }
        }

        private Production FindLambdaProduction(NonTerminal lhs)
        {
            foreach (Production pp in Prod)
            {
                Terminal t = pp.rhs[0] as Terminal;
                if (t == null)
                    continue;
                if (pp.lhs == lhs && t.Token == TokenLookup.TokenKind.Lambda)
                    return pp;
            }
            return null;
        }
        public void ComputeFirstSet()
        {
            int k = 0;
            bool FoundLambda = false;
            bool Continue = true;
            NonTerminal lhs = null;

            foreach (Terminal term in Term)
                ((IList)term.First).Add(term.Token);
            do
            {
                FirstSetChanged = false;
                foreach (Production pp in Prod)
                {
                    k = 0;
                    Continue = true;
                    while (Continue && k <= pp.rhs.Count - 1)
                    {
                        NonTerminal nt = pp.rhs[k] as NonTerminal;
                        if (nt != null)
                        {
                            FoundLambda = false;
                            foreach (TokenLookup.TokenKind tok in nt.First)
                            {
                                lhs = pp.lhs as NonTerminal;
                                if (tok != TokenLookup.TokenKind.Lambda && !((IList)lhs.First).Contains(tok))
                                {
                                    ((IList)lhs.First).Add(tok);
                                    lhs.First.HasChanged = true;
                                }
                                if (tok == TokenLookup.TokenKind.Lambda)
                                    FoundLambda = true;
                            }
                            Continue = FoundLambda;
                        }
                        else
                        {
                            FoundLambda = false;
                            Terminal t = pp.rhs[k] as Terminal;
                            lhs = pp.lhs as NonTerminal;

                            if (t.Token != TokenLookup.TokenKind.Lambda && !((IList)lhs.First).Contains(t.Token))
                            {
                                ((IList)lhs.First).Add(t.Token);
                                lhs.First.HasChanged = true;
                            }
                            if (t.Token == TokenLookup.TokenKind.Lambda)
                                FoundLambda = true;
                            Continue = FoundLambda;
                        }
                        k = k + 1;
                }
                if (Continue && !((IList)lhs.First).Contains(TokenLookup.TokenKind.Lambda))
                {
                   ((IList)lhs.First).Add(TokenLookup.TokenKind.Lambda);
                   lhs.First.HasChanged = true;
                   
                }
              }
            } while (FirstSetChanged);
        }

        public bool ComputeFirstSetHelper(Production pp, int index)
        {
            int k = 0;
            bool Continue = false;
            NonTerminal lhs = null;
            
            if (index == pp.rhs.Count - 1)
                return true;

            lhs = pp.rhs[index] as NonTerminal;
            if (lhs == null)
                return false;
            k = index + 1;

            do
            {
                Continue = false;
                NonTerminal nt = pp.rhs[k] as NonTerminal;
                if (nt != null)
                {
                    foreach (TokenLookup.TokenKind tok in nt.First)
                    {
                        if (tok != TokenLookup.TokenKind.Lambda && !((IList)lhs.Follow).Contains(tok))
                        {
                            ((IList)lhs.Follow).Add(tok);
                            lhs.Follow.HasChanged = true;
                        }
                        if (tok == TokenLookup.TokenKind.Lambda)
                            Continue = true;
                    }
                }
                else
                {
                    Terminal t = pp.rhs[k] as Terminal;
                    if (t.Token != TokenLookup.TokenKind.Lambda && !((IList)lhs.Follow).Contains(t.Token))
                    {
                        ((IList)lhs.Follow).Add(t.Token);
                        lhs.Follow.HasChanged = true;
                    }
                    if (t.Token == TokenLookup.TokenKind.Lambda)
                        Continue = true;
                }
                k = k + 1;
            } while (Continue && k <= pp.rhs.Count - 1);

            return Continue;
        }

        public void ComputeFollowSet()
        {
            bool FoundLambda = false;

            NonTerminal lhs = Prod[0].lhs as NonTerminal;
            ((IList)lhs.Follow).Add(TokenLookup.TokenKind.EOI);

            do
            {
                FollowSetChanged = false;
                foreach (Production pp in Prod)
                {
                    for (int j = 0; j < pp.rhs.Count; j++)
                    {
                        FoundLambda = ComputeFirstSetHelper(pp, j);
                        if (FoundLambda)
                        {
                            NonTerminal nt = pp.rhs[j] as NonTerminal;
                            if (nt == null)
                                continue;
                            foreach (TokenLookup.TokenKind tok in ((NonTerminal)pp.lhs).Follow)
                                if (!((IList)nt.Follow).Contains(tok))
                                    ((IList)nt.Follow).Add(tok);
                        }
                    }
                }
            } while (FollowSetChanged);

        }

        public bool FirstSetChanged
        {
            get
            {
                foreach (NonTerminal nt in NTerm)
                    if (nt.First.HasChanged)
                        return true;
                return false;
            }
            set
            {
                foreach (NonTerminal nt in NTerm)
                    nt.First.HasChanged = value;
            }
        }
        public bool FollowSetChanged
        {
            get
            {
                foreach (NonTerminal nt in NTerm)
                    if (nt.Follow.HasChanged)
                        return true;
                return false;
            }
            set
            {
                foreach (NonTerminal nt in NTerm)
                    nt.Follow.HasChanged = value;
            }
        }

        public void DumpLanguageAttributes()
        {
            FileStream _fs = null;
            StreamWriter _wr = null;

            FileInfo fi = new FileInfo("c:\\CInherited");
            if (fi.Exists)
                fi.Delete();

            _fs = new FileStream("c:\\CInherited", FileMode.CreateNew, FileAccess.Write);
            _wr = new StreamWriter(_fs);

            foreach (Production p in Prod)
            {
                _wr.Write("prod : " + p.prodno + " lhs : " + p.lhs + " ");
                _wr.WriteLine();
                if (p.inh != null && p.inh.Count > 0)
                    for (int _j = 0; _j < p.inh.Count; _j++)
                        if (p.inh[_j] != null)
                        {
                            if (p.inh[_j].nTerm == null)
                                _wr.Write(" ****************************** NULL **********************************");
                            else
                                _wr.Write(p.inh[_j].nTerm + " ");
                        }
                _wr.WriteLine();
            }
            _wr.Close();
            _wr.Dispose();
            _fs.Close();
            _fs.Dispose();
         
        }
        

        public void DumpLanguage()
        {
            FileStream _fs = null;
            StreamWriter _wr = null;
            
            FileInfo fi = new FileInfo("c:\\CGrammarLL01");
            if (fi.Exists)
                fi.Delete();

            _fs = new FileStream("c:\\CGrammarLL01",FileMode.CreateNew,FileAccess.Write);
            _wr = new StreamWriter(_fs);

            foreach (Production p in Prod)
            { 
                _wr.Write("prod : " + p.prodno + " lhs : " + p.lhs + " --> rhs : ");
                for (int _j=0;_j< p.rhs.Count; _j++)
                    _wr.Write( p.rhs[_j] + " ");
                _wr.WriteLine();
                _wr.WriteLine();
            }
            
            foreach (NonTerminal nt in NTerm)
            {
                _wr.WriteLine();
                _wr.Write("NonTerm : " + nt.Name );
                _wr.WriteLine();
                _wr.Write("First Set : ");
                foreach (TokenLookup.TokenKind tt in nt.First)
                    _wr.Write( tt.ToString()+ " ");
                _wr.WriteLine(); 
                _wr.Write("Follow Set : ");
                foreach (TokenLookup.TokenKind tt in nt.Follow)
                    _wr.Write( tt.ToString() + " ");
                _wr.WriteLine();
            }

            _wr.Close();
            _wr.Dispose();
            _fs.Close();
            _fs.Dispose();
        }

    }



    public class SyntaxSymbol
    {
        public string Name;
    }
    public class Terminal : SyntaxSymbol
    {
        public Terminal(string name, TokenLookup.TokenKind tok)
        {
            Name = name;
            Token = tok;
            First = new FirstSet();
        }
        public TokenLookup.TokenKind Token;
        public FirstSet First;

        public override string ToString()
        {
            return Name ;
        }
    }
    public class NonTerminal : SyntaxSymbol
    {
        public NonTerminal(string name, Category cat)
        {
            Name = name;
            Category = cat;
            First = new FirstSet();
            Follow = new FollowSet();
        }
        public override string ToString()
        {
            return Name;
        }
        public Category Category;
        public FirstSet First;
        public FollowSet Follow;
    }
    public class Production
    {
        static int _prodno=0;
        public Production() { }
        public Production( SyntaxSymbol _lhs, List<SyntaxSymbol> _rhs, List<SemanticAction> _action, List<SynthesizedAttribute> _synth, List<InheritedAttribute> _inh)
        {
            ++_prodno;
            prodno = _prodno;
            lhs = _lhs;
            rhs = _rhs;
            action = _action;
            synth = _synth;
            inh = _inh;
        }
        public int prodno;
        public SyntaxSymbol lhs;
        public List<SynthesizedAttribute> synth = new List<SynthesizedAttribute>();
        public List<InheritedAttribute> inh = new List<InheritedAttribute>();
        public List<SyntaxSymbol> rhs = new List<SyntaxSymbol>();
        public List<SemanticAction> action = new List<SemanticAction>();

        public override string ToString()
        {
            string str = prodno.ToString() + " " + lhs + " ";
            
            return str;
        }
        
    }
    public class FirstSet : IEnumerable, IList
    {
        ArrayList ar = new ArrayList();
        private bool _haschanged = false;
        public bool HasChanged { get { return _haschanged; } set { _haschanged = value; } }

        public object this[int index] { get { return ar[index]; } set { ar[index] = value; } }
        int IList.Add(object o) { return ar.Add(o); }
        void IList.Clear() { ar.Clear(); }
        bool IList.Contains(object o) { return ar.Contains(o); }
        int IList.IndexOf(object o) { return ar.IndexOf(o); }
        void IList.Insert(int index, object o) { ar.Insert(index, o); }
        bool IList.IsFixedSize { get { return false; } }
        bool IList.IsReadOnly { get { return false; } }
        void IList.Remove(object o) { ar.Remove(o); }
        void IList.RemoveAt(int index) { ar.RemoveAt(index); }
        IEnumerator IEnumerable.GetEnumerator() { return ar.GetEnumerator(); }
        bool ICollection.IsSynchronized { get { return false; } }
        int ICollection.Count { get { return ar.Count; } }
        void ICollection.CopyTo(Array aa, int index) { ar.CopyTo(aa, index); }
        object ICollection.SyncRoot { get { return null; } }
    }
    public class FollowSet : IEnumerable, IList
    {
        ArrayList ar = new ArrayList();
        private bool _haschanged = false;
        public bool HasChanged { get { return _haschanged; } set { _haschanged = value; } }

        public object this[int index] { get { return ar[index]; } set { ar[index] = value; } }
        int IList.Add(object o) { return ar.Add(o); }
        void IList.Clear() { ar.Clear(); }
        bool IList.Contains(object o) { return ar.Contains(o); }
        int IList.IndexOf(object o) { return ar.IndexOf(o); }
        void IList.Insert(int index, object o) { ar.Insert(index, o); }
        bool IList.IsFixedSize { get { return false; } }
        bool IList.IsReadOnly { get { return false; } }
        void IList.Remove(object o) { ar.Remove(o); }
        void IList.RemoveAt(int index) { ar.RemoveAt(index); }
        IEnumerator IEnumerable.GetEnumerator() { return ar.GetEnumerator(); }
        bool ICollection.IsSynchronized { get { return false; } }
        int ICollection.Count { get { return ar.Count; } }
        void ICollection.CopyTo(Array aa, int index) { ar.CopyTo(aa, index); }
        object ICollection.SyncRoot { get { return null; } }
    }
}
