
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace AddressingModes
{
    public class SemanticAction
    {
        public delegate bool Action(SyntaxStack Stack, Production pp, string token);
        public Action FireAction;
        public SemanticAction()
        {

        }
        public SemanticAction(Action action)
        {
            FireAction = action;
        }
        public override string ToString()
        {
            return "a(" + FireAction.Method.Name.ToString() + ")";
        }
    }

    public abstract class SynthesizedAttribute
    {
        public NonTerminal nTerm;
        public delegate bool Action(SyntaxStack Stack, Production pp);
        public Action FireAction;

        public virtual void Clear()
        {
        }
    }

    public class InheritedAttribute
    {
        public string ErrMsg = null;
        public NonTerminal nTerm;
        public InheritedAttribute() { }

        public InheritedAttribute(InheritedAttribute inh)
        {
            nTerm = inh.nTerm;
            ErrMsg = inh.ErrMsg;
        }
        public InheritedAttribute(NonTerminal nt)
        {
            nTerm = nt;
        }
        public InheritedAttribute(NonTerminal nt, string _errmsg)
        {
            nTerm = nt;
            ErrMsg = _errmsg;
        }
        public virtual void Clear()
        {
        }
    }

    
    public class IDeclTypeAttribute : InheritedAttribute
    {
        public ArrayList Tuples = new ArrayList();
        public SignSpecifier signed = SignSpecifier.SIGNED;
        public StorageClass sclass = StorageClass.UnKnown;
        public Type basety = null; // type of variable
        public Type tmpty = null; // temporary type
        public Symbol sym=null;
        public List<Symbol> lsym = new List<Symbol>(); // function attribute 
        public Stack<Type> stype = new Stack<Type>(); // pushed types

        public int np = 0;
        public string id = null;
        public int size; // array types

        public override void Clear()
        {
           sclass = StorageClass.UnKnown;
           signed = SignSpecifier.SIGNED;
           basety = null; // type of variable
           tmpty = null; // temporary type
           sym = null;
           lsym.Clear();// function attribute 
           stype.Clear();
           Tuples.Clear();
           np = 0;
           id = null;
           size=0; // array types
        }
        public IDeclTypeAttribute(IDeclTypeAttribute ip)
        {
            nTerm = ip.nTerm;
            ErrMsg = ip.ErrMsg;
            sym = ip.sym; 
            basety = ip.basety;
            tmpty = ip.tmpty;
            signed = ip.signed;
            sclass = ip.sclass;
            size = ip.size;
            id = ip.id;
            np = ip.np;
            foreach (Symbol ssym in ip.lsym)
                lsym.Add(ssym);

           __Copy_Stack(stype,ip.stype);
            
        }
        public IDeclTypeAttribute(NonTerminal nt, string _errmsg)
        {
            nTerm = nt;
            ErrMsg = _errmsg;
        }
        private void __Copy_Stack(Stack<Type> dest, Stack<Type> source)
        {
            Stack tmp = new Stack();

            while (source.Count > 0)
            {
                Type gg = (Type)source.Pop();
                tmp.Push(gg);
            }
            //dest.Clear();
            while (tmp.Count > 0)
            {
                Type gg = (Type)tmp.Pop();
                dest.Push(gg);
            }
        }

        public override string ToString()
        {
            return "IDeclTypeAttribute ";
        }
    }

    public class SDeclTypeAttribute : SynthesizedAttribute
    {
        public ArrayList Tuples = new ArrayList();
        public SignSpecifier signed = SignSpecifier.SIGNED;
        public StorageClass sclass = StorageClass.UnKnown;
        public Type basety = null;
        public Type tmpty = null; // temporary type
        public Symbol sym = null;

        public List<Symbol> lsym = new List<Symbol>();
        public Stack<Type> stype = new Stack<Type>();

        public int np = 0;
        public string id = null;
        public int size; // array types

        public override void Clear()
        {
            signed = SignSpecifier.SIGNED;
            sclass = StorageClass.UnKnown;
            basety = null; // type of variable
            tmpty = null; // temporary type
            sym = null;
            lsym.Clear();// function attribute 
            stype.Clear();
            Tuples.Clear();
    
            np = 0;
            id = null;
            size = 0; // array types
        }

        public SDeclTypeAttribute(SDeclTypeAttribute ip)
        {
            nTerm = ip.nTerm;
            sym = ip.sym;
            basety = ip.basety;
            tmpty = ip.tmpty;
            signed = ip.signed;
            sclass = ip.sclass;
            size = ip.size;
            id = ip.id;
            np = ip.np;
    
            foreach (Symbol ssym in ip.lsym)
                lsym.Add(ssym);

            __Copy_Stack(stype,ip.stype);
             FireAction = ip.FireAction;
        }
        public SDeclTypeAttribute(NonTerminal nt, Action _action)
        {
            nTerm = nt;
            FireAction = _action;
        }

        private void __Copy_Stack(Stack<Type> dest, Stack<Type> source)
        {
            Stack tmp = new Stack();

            while (source.Count > 0)
            {
                Type gg = (Type)source.Pop();
                tmp.Push(gg);
            }
            //dest.Clear();
            while (tmp.Count > 0)
            {
                Type gg = (Type)tmp.Pop();
                dest.Push(gg);
            }
        }


        public override string ToString()
        {
            return "SDeclTypeAttribute ";
        }
    }

    public class IPointerAttribute :InheritedAttribute
    {
        public int np=0;

        public override void Clear()
        {
            np=0;
        }
        public IPointerAttribute(IPointerAttribute ip)
        {
            nTerm = ip.nTerm;
            ErrMsg = ip.ErrMsg;
             np = ip.np;
        }
         public IPointerAttribute(NonTerminal nt, string _errmsg)
        {
            nTerm = nt;
            ErrMsg = _errmsg;
        }
        public override string  ToString()
        {
            return "IPointerAttribute : " + np.ToString();
        }
    }


    public class SyntaxStack
    {
        public object[] ss = null;
        public int sp;

        public SyntaxStack(int nn)
        {
            ss = new object[nn];
            sp = 0;
        }

        public object Top()
        {
            object sym = ss[sp - 1];
            return sym;
        }
        public void Push(object sym)
        {
            ss[sp++] = sym;
        }
        public object Pop()
        {
            return ss[--sp];
        }
    }

}
