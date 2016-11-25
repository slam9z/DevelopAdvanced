using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;


namespace AddressingModes
{
    public partial class EAGrammar
    {
        private byte [] bb = new byte [100];
        // dump purposes
        public static string _currEqu = null;
        public static string _fName = null;
        public static string _fPath = null;
        public static FileStream _fTmp = null;
        public static FileStream _fObj = null;
        public static FileStream _fCode = null;
        public static FileStream fOut = null;
        public static StreamWriter _wrOut = null;
        public static string[] lines;
        public static string _curline = "";
        public static int _lineno = 0;
        public static int _line = 1;
        public static string ErrMsg = "";
        public static string _type = null;

        public Language lg = null;
        public EAGrammar(Language _lg)
        {
            lg = _lg;
            _lineno = 0;
            _line = 1;
        }
         
        public void Load()
        {
            int idx = 0;

            TokenLookup.TokenKind[] tokens = (TokenLookup.TokenKind[])Enum.GetValues(typeof(TokenLookup.TokenKind));
            string[] names = TokenLookup.TokenNames;
            Terminal tt = null;
            foreach (TokenLookup.TokenKind tok in tokens)
            {
                tt = new Terminal(names[idx], tok);
                lg.Term.Add(tt);
                if (lg.HashTerm[names[idx]] != null)
                    MessageBox.Show(" Terminal " + names[idx] + " already inserted");
                else
                    lg.HashTerm.Add(names[idx], tt);
                idx++;
            }
            Category[] categories = (Category[])Enum.GetValues(typeof(Category));
            idx = 0;
            NonTerminal nt = null;
            foreach (Category cat in categories)
            {
                nt = new NonTerminal(Enum.Format(typeof(Category), cat, "G"), cat);
                lg.NTerm.Add(nt);
                if (lg.HashNTerm[cat] != null)
                    MessageBox.Show(" Nonterminal " + cat + " already inserted");
                else
                    lg.HashNTerm.Add(cat.ToString(), nt);
            }
            // Productions
            List<SemanticAction> actions = new List<SemanticAction>();
            List<InheritedAttribute> inh = new List<InheritedAttribute>();
            List<SynthesizedAttribute> synth = new List<SynthesizedAttribute>();
            NonTerminal lhs = null;
            List<SyntaxSymbol> rhs = null;

             /*
                declaration:
                    function-definition
                    declaration 
            */

            lhs = lg.FindNTermByName("Declaration");
            rhs = new List<SyntaxSymbol>();
            rhs.Add(lg.FindNTermByName("Declaration_Specifiers"));
            rhs.Add(lg.FindNTermByName("Declarator"));
            rhs.Add(lg.FindTermByName("SemiColon"));
            inh = new List<InheritedAttribute>();
            inh.Add(new InheritedAttribute(lg.FindNTermByName("Declaration_Specifiers"), null));
            inh.Add(new IDeclTypeAttribute(lg.FindNTermByName("Declarator"), null));
            inh.Add(null);
            actions = new List<SemanticAction>();
            actions.Add(null);
            actions.Add(new SemanticAction(ActionOnExpandDeclarator));
            actions.Add(null);
            synth = new List<SynthesizedAttribute>();
            synth.Add(null);
            synth.Add(new SDeclTypeAttribute(lg.FindNTermByName("Declarator"), ActionProcessSymbolTableDeclarator));
            synth.Add(null);
            lg.Prod.Add(new Production( lhs, rhs, actions, synth, inh));


             /*
                 declaration-specifiers:
                      storage-class-specifier declaration-specifiersopt
                      type-specifier declaration-specifiersopt
            */
            
            lhs = lg.FindNTermByName("Declaration_Specifiers");
            rhs = new List<SyntaxSymbol>();
            rhs.Add(lg.FindNTermByName("Storage_Class_Specifier"));
            rhs.Add(lg.FindNTermByName("Sign_Specifier"));
            rhs.Add(lg.FindNTermByName("Type_Specifier"));
            inh = new List<InheritedAttribute>();
            inh.Add(new InheritedAttribute(lg.FindNTermByName("Storage_Class_Specifier"), null));
            inh.Add(new InheritedAttribute(lg.FindNTermByName("Sign_Specifier"), null));
            inh.Add(new InheritedAttribute(lg.FindNTermByName("Type_Specifier"), null));
            synth = new List<SynthesizedAttribute>();
            synth.Add(new SDeclTypeAttribute(lg.FindNTermByName("Storage_Class_Specifier"), ActionLoadStorageClassModifier));
            synth.Add(new SDeclTypeAttribute(lg.FindNTermByName("Sign_Specifier"), ActionLoadSignSpecifier));
            synth.Add(new SDeclTypeAttribute(lg.FindNTermByName("Type_Specifier"), ActionLoadBaseType));
            lg.Prod.Add(new Production( lhs, rhs, null, synth, inh));

            
            /*  storage-class specifier: one of
                   auto register static extern 
             */

            lhs = lg.FindNTermByName("Storage_Class_Specifier");
            rhs = new List<SyntaxSymbol>();
            rhs.Add(lg.FindTermByName("EXTERN"));
            actions = new List<SemanticAction>();
            actions.Add(new SemanticAction(AddStorageClassModifier));
            lg.Prod.Add(new Production( lhs, rhs, actions, null, null));

            lhs = lg.FindNTermByName("Storage_Class_Specifier");
            rhs = new List<SyntaxSymbol>();
            rhs.Add(lg.FindTermByName("STATIC"));
            actions = new List<SemanticAction>();
            actions.Add(new SemanticAction(AddStorageClassModifier));
            lg.Prod.Add(new Production(lhs, rhs, actions, null, null));

            lhs = lg.FindNTermByName("Storage_Class_Specifier");
            rhs = new List<SyntaxSymbol>();
            rhs.Add(lg.FindTermByName("AUTO"));
            actions = new List<SemanticAction>();
            actions.Add(new SemanticAction(AddStorageClassModifier));
            lg.Prod.Add(new Production(lhs, rhs, actions, null, null));

            lhs = lg.FindNTermByName("Storage_Class_Specifier");
            rhs = new List<SyntaxSymbol>();
            rhs.Add(lg.FindTermByName("REGISTER"));
            actions = new List<SemanticAction>();
            actions.Add(new SemanticAction(AddStorageClassModifier));
            lg.Prod.Add(new Production(lhs, rhs, actions, null, null));

            lhs = lg.FindNTermByName("Storage_Class_Specifier");
            rhs = new List<SyntaxSymbol>();
            rhs.Add(lg.FindTermByName("Lambda"));
            lg.Prod.Add(new Production(lhs, rhs, null, null, null));

            /*    type specifier: one of
                     void char short int long                      
           */

            lhs = lg.FindNTermByName("Type_Specifier");
            rhs = new List<SyntaxSymbol>();
            rhs.Add(lg.FindTermByName("VOID"));
            actions = new List<SemanticAction>();
            actions.Add(new SemanticAction(BuildBaseType));
            lg.Prod.Add(new Production(lhs, rhs, actions, null, null));

            lhs = lg.FindNTermByName("Type_Specifier");
            rhs = new List<SyntaxSymbol>();
            rhs.Add(lg.FindTermByName("CHAR"));
            actions = new List<SemanticAction>(); 
            actions.Add(new SemanticAction(BuildBaseType));
            lg.Prod.Add(new Production(lhs, rhs, actions, null, null));

            lhs = lg.FindNTermByName("Type_Specifier");
            rhs = new List<SyntaxSymbol>();
            rhs.Add(lg.FindTermByName("SHORT"));
            actions = new List<SemanticAction>(); 
            actions.Add(new SemanticAction(BuildBaseType));
            lg.Prod.Add(new Production(lhs, rhs, actions, null, null));

            lhs = lg.FindNTermByName("Type_Specifier");
            rhs = new List<SyntaxSymbol>();
            rhs.Add(lg.FindTermByName("INT"));
            actions = new List<SemanticAction>(); 
            actions.Add(new SemanticAction(BuildBaseType));
            lg.Prod.Add(new Production(lhs, rhs, actions, null, null));

            lhs = lg.FindNTermByName("Type_Specifier");
            rhs = new List<SyntaxSymbol>();
            rhs.Add(lg.FindTermByName("LONG"));
            actions = new List<SemanticAction>(); 
            actions.Add(new SemanticAction(BuildBaseType));
            lg.Prod.Add(new Production(lhs, rhs, actions, null, null));

            lhs = lg.FindNTermByName("Sign_Specifier");
            rhs = new List<SyntaxSymbol>();
            rhs.Add(lg.FindTermByName("SIGNED"));
            actions = new List<SemanticAction>();
            actions.Add(new SemanticAction(ActionAddSignSpecifier));
            lg.Prod.Add(new Production(lhs, rhs, actions, null, null));

            lhs = lg.FindNTermByName("Sign_Specifier");
            rhs = new List<SyntaxSymbol>();
            rhs.Add(lg.FindTermByName("UNSIGNED"));
            actions = new List<SemanticAction>();
            actions.Add(new SemanticAction(ActionAddSignSpecifier));
            lg.Prod.Add(new Production(lhs, rhs, actions, null, null));

            lhs = lg.FindNTermByName("Sign_Specifier");
            rhs = new List<SyntaxSymbol>();
            rhs.Add(lg.FindTermByName("Lambda"));
            lg.Prod.Add(new Production(lhs, rhs, null, null, null));

            lhs = lg.FindNTermByName("Declarator");
            rhs = new List<SyntaxSymbol>();
            rhs.Add(lg.FindNTermByName("Pointer"));
            rhs.Add(lg.FindNTermByName("Direct_Declarator"));
            inh = new List<InheritedAttribute>();
            inh.Add(new IDeclTypeAttribute(lg.FindNTermByName("Pointer"), null));
            inh.Add(new IDeclTypeAttribute(lg.FindNTermByName("Direct_Declarator"), null));
            actions = new List<SemanticAction>();
            actions.Add(new SemanticAction(ActionOnExpandPointer));
            actions.Add(new SemanticAction(ActionOnExpandDirectDecl));
            synth = new List<SynthesizedAttribute>();
            synth.Add(new SDeclTypeAttribute(lg.FindNTermByName("Pointer"),  null));
            synth.Add(null);
            lg.Prod.Add(new Production( lhs, rhs, actions, synth, inh));

            
            /* direct-declarator:
                  identifier
                 (declarator)
                  direct-declarator [ constant-expressionopt ]
                  direct-declarator ( parameter-type-list )
                 direct-declarator ( identifier-listopt ) 
             */

            
            lhs = lg.FindNTermByName("Direct_Declarator");
            rhs = new List<SyntaxSymbol>();
            rhs.Add(lg.FindTermByName("Id"));
            rhs.Add(lg.FindNTermByName("Direct_Declarator_Tail"));
            inh = new List<InheritedAttribute>();
            inh.Add(null);
            inh.Add(new IDeclTypeAttribute(lg.FindNTermByName("Direct_Declarator_Tail"), null));
            actions = new List<SemanticAction>();
            actions.Add(new SemanticAction(ActionOnLoadIdToDirectDeclTail));
            actions.Add(new SemanticAction(ActionOnExpandDirectDeclTail));
            lg.Prod.Add(new Production( lhs, rhs, actions, null, inh));
           
            lhs = lg.FindNTermByName("Direct_Declarator");
            rhs = new List<SyntaxSymbol>();
            rhs.Add(lg.FindTermByName("LParen"));
            rhs.Add(lg.FindNTermByName("Declarator"));
            rhs.Add(lg.FindTermByName("RParen"));
            rhs.Add(lg.FindNTermByName("Direct_Declarator_Tail"));
            inh = new List<InheritedAttribute>();
            inh.Add(null);
            inh.Add(new IDeclTypeAttribute(lg.FindNTermByName("Declarator"),null));
            inh.Add(null);
            inh.Add(new IDeclTypeAttribute(lg.FindNTermByName("Direct_Declarator_Tail"), null));
            synth = new List<SynthesizedAttribute>();
            synth.Add(null);
            synth.Add(new SDeclTypeAttribute(lg.FindNTermByName("Declarator"),ActionOnProcessTempDeclarator));
            synth.Add(null);
            synth.Add(null);
            actions = new List<SemanticAction>();
            actions.Add(null);
            actions.Add(null);
            actions.Add(null);
            actions.Add(new SemanticAction(ActionOnExpandDirectDeclTail));
            lg.Prod.Add(new Production(lhs, rhs, actions, synth, inh));

            lhs = lg.FindNTermByName("Direct_Declarator_Tail");
            rhs = new List<SyntaxSymbol>();
            rhs.Add(lg.FindTermByName("LSParen"));
            rhs.Add(lg.FindTermByName("Integer_Constant"));
            rhs.Add(lg.FindTermByName("RSParen"));
            rhs.Add(lg.FindNTermByName("Direct_Declarator_Tail"));
            inh = new List<InheritedAttribute>();
            inh.Add(null);
            inh.Add(null);
            inh.Add(null);
            inh.Add(new IDeclTypeAttribute(lg.FindNTermByName("Direct_Declarator_Tail"), null));
            actions=new List<SemanticAction>();
            actions.Add(null);
            actions.Add(new SemanticAction(ActionOnLoadNumToDirectDeclTail));
            actions.Add(null);
            actions.Add(new SemanticAction(ActionOnExpandDirectDeclTail));
            lg.Prod.Add(new Production( lhs, rhs, actions, null, inh));

            lhs = lg.FindNTermByName("Direct_Declarator_Tail");
            rhs = new List<SyntaxSymbol>();
            rhs.Add(lg.FindTermByName("LParen"));
            rhs.Add(lg.FindNTermByName("Parameter_Type_List")); 
            rhs.Add(lg.FindTermByName("RParen"));
            rhs.Add(lg.FindNTermByName("Direct_Declarator_Tail"));
            inh = new List<InheritedAttribute>();
            inh.Add(null);
            inh.Add(new IDeclTypeAttribute(lg.FindNTermByName("Parameter_Type_List"), null));
            inh.Add(null);
            inh.Add(new IDeclTypeAttribute(lg.FindNTermByName("Direct_Declarator_Tail"), null));
            actions = new List<SemanticAction>();
            actions.Add(null);
            actions.Add(new SemanticAction(ActionOnExpandParameterTypeList));
            actions.Add(null);
            actions.Add(new SemanticAction(ActionOnExpandDirectDeclTail));
            lg.Prod.Add(new Production( lhs, rhs, actions, null, inh));

            lhs = lg.FindNTermByName("Direct_Declarator_Tail");
            rhs = new List<SyntaxSymbol>();
            rhs.Add(lg.FindTermByName("Lambda"));
            lg.Prod.Add(new Production( lhs, rhs, null, null, null));
            
            /*  pointer:
                 * type-qualifier-listopt
                 * type-qualifier-listopt pointer 
            */
            lhs = lg.FindNTermByName("Pointer");
            rhs = new List<SyntaxSymbol>();
            rhs.Add(lg.FindTermByName("Star"));
            rhs.Add(lg.FindNTermByName("Pointer"));
            inh = new List<InheritedAttribute>();
            inh.Add(null);
            inh.Add(new IDeclTypeAttribute(lg.FindNTermByName("Pointer"), null));
            actions = new List<SemanticAction>();
            actions.Add(null);
            actions.Add(new SemanticAction(ActionOnExpandPointer));
            lg.Prod.Add(new Production( lhs, rhs, actions, null, inh));

            lhs = lg.FindNTermByName("Pointer");
            rhs = new List<SyntaxSymbol>();
            rhs.Add(lg.FindTermByName("Lambda"));
            lg.Prod.Add(new Production( lhs, rhs, null, null, null));
            
            lhs = lg.FindNTermByName("Parameter_Type_List");
            rhs = new List<SyntaxSymbol>();
            rhs.Add(lg.FindNTermByName("Parameter_List"));
            inh = new List<InheritedAttribute>();
            inh.Add(new InheritedAttribute(lg.FindNTermByName("Parameter_List"), null));
            lg.Prod.Add(new Production( lhs, rhs, null, null, inh));

            lhs = lg.FindNTermByName("Parameter_Type_List");
            rhs = new List<SyntaxSymbol>();
            rhs.Add(lg.FindTermByName("Lambda"));
            lg.Prod.Add(new Production( lhs, rhs, null, null, null));
            
            /*  parameter-list:
                  parameter-declaration
                  parameter-list , parameter-declaration 
            */

            lhs = lg.FindNTermByName("Parameter_List");
            rhs = new List<SyntaxSymbol>();
            rhs.Add(lg.FindNTermByName("Parameter_Declaration"));
            rhs.Add(lg.FindNTermByName("Parameter_List_Tail"));
            inh = new List<InheritedAttribute>();
            inh.Add(new InheritedAttribute(lg.FindNTermByName("Parameter_Declaration"), null));
            inh.Add(new IDeclTypeAttribute(lg.FindNTermByName("Parameter_List_Tail"), null));
            actions = new List<SemanticAction>();
            actions.Add(null);
            actions.Add(new SemanticAction(ActionOnExpandParameterList));
            lg.Prod.Add(new Production( lhs, rhs, actions, null, inh));

            lhs = lg.FindNTermByName("Parameter_List_Tail");
            rhs = new List<SyntaxSymbol>();
            rhs.Add(lg.FindTermByName("Comma"));
            rhs.Add(lg.FindNTermByName("Parameter_Declaration"));
            rhs.Add(lg.FindNTermByName("Parameter_List_Tail"));
            inh = new List<InheritedAttribute>();
            inh.Add(null);
            inh.Add(new InheritedAttribute(lg.FindNTermByName("Parameter_Declaration"), null));
            inh.Add(new IDeclTypeAttribute(lg.FindNTermByName("Parameter_List_Tail"), null));
            actions = new List<SemanticAction>();
            actions.Add(null);
            actions.Add(null);
            actions.Add(new SemanticAction(ActionOnExpandParameterList));
            lg.Prod.Add(new Production( lhs, rhs, actions, null, inh));

            lhs = lg.FindNTermByName("Parameter_List_Tail");
            rhs = new List<SyntaxSymbol>();
            rhs.Add(lg.FindTermByName("Lambda"));
            lg.Prod.Add(new Production( lhs, rhs, null, null, null));
            
            /* parameter-declaration:
                 declaration-specifiers declarator
                  declaration-specifiers abstract-declaratoropt 
            */

            lhs = lg.FindNTermByName("Parameter_Declaration");
            rhs = new List<SyntaxSymbol>();
            rhs.Add(lg.FindNTermByName("Declaration_Specifiers"));
            rhs.Add(lg.FindNTermByName("Parameter_Declaration_Tail"));
            inh = new List<InheritedAttribute>();
            inh.Add(new InheritedAttribute(lg.FindNTermByName("Declaration_Specifiers"), null));
            inh.Add(new IDeclTypeAttribute(lg.FindNTermByName("Parameter_Declaration_Tail"), null));
            actions = new List<SemanticAction>();
            actions.Add(null);
            actions.Add(new SemanticAction(ActionOnExpandParameterDeclarationTail));
            lg.Prod.Add(new Production( lhs, rhs, actions, null, inh));

            lhs = lg.FindNTermByName("Parameter_Declaration_Tail");
            rhs = new List<SyntaxSymbol>();
            rhs.Add(lg.FindNTermByName("Declarator"));
            inh = new List<InheritedAttribute>();
            inh.Add(new IDeclTypeAttribute(lg.FindNTermByName("Declarator"), null));
            synth = new List<SynthesizedAttribute>();
            synth.Add(new SDeclTypeAttribute(lg.FindNTermByName("Declarator"),ActionOnLoadParameterList));
            actions = new List<SemanticAction>();
            actions.Add(new SemanticAction(ActionOnExpandDeclarator));
            lg.Prod.Add(new Production( lhs, rhs, actions, synth, inh));


            lg.ComputeFirstSet();
            lg.ComputeFollowSet();
            lg.FillTable();

           // lg.DumpLanguage();
        }

        #region // Common routines

        static string _FormatType(Symbol sym)
        {
            Type ty = null;
            string ss = null;

            ss += sym.name.ToLower() + " : ";

            if (sym.sclass != StorageClass.UnKnown)
                ss += sym.sclass + " ";

            ty = sym.type;
            while (ty != null)
            {
                ss += ty.op.ToString().ToLower() + " ";
                if (ty.op == TypeOperator.ARRAY)
                    ss += "[" + ty.size + "] of ";
                else
                    if (ty.op == TypeOperator.FUNCTION)
                    {
                        if (ty.lsym != null)
                        {
                            ss += " taking parameters : ( ";
                            foreach (Symbol s in ty.lsym)
                                ss += _FormatType(s);
                            ss += " ) ";
                        }
                            ss += " returning ";
                    }
                    else
                        if (ty.op == TypeOperator.POINTER)
                            ss += " to ";
                ty = ty.type;
            }

            return ss;
        }

        static bool CheckInvalidType(Type ty)
        {
            if (ty.op == TypeOperator.VOID)
            {
                ErrMsg = " invalid type void ";
                return true;
            }
            while (ty != null)
            {
                if (ty.op != TypeOperator.POINTER &&
                        ty.op != TypeOperator.FUNCTION)
                {
                    if (ty.type != null && ty.type.op == TypeOperator.VOID)
                    {
                        ErrMsg = " invalid base type void ";
                        return true;
                    }
                }
                if (ty.op == TypeOperator.FUNCTION)
                {
                    if (ty.type.op != null && ty.type.op == TypeOperator.FUNCTION)
                    {
                        ErrMsg = " invalid type function returning function ";
                        return true;
                    }
                    else
                        if (ty.type != null && ty.type.op == TypeOperator.FUNCTION)
                        {
                            ErrMsg = " invalid type function returning array ";
                            return true;
                        }

                    if (ty.lsym != null)
                        foreach (Symbol sym in ty.lsym)
                        {
                            if (sym.type.op == TypeOperator.VOID)
                            {
                                ErrMsg = sym.name +  " : invalid argument type void ";
                                return true;
                            }
                        }
                }
                else
                    if (ty.op == TypeOperator.ARRAY)
                    {
                        if (ty.type != null && ty.type.op == TypeOperator.FUNCTION)
                        {
                            ErrMsg = " invalid type array of functions ";
                            return true;
                        }
                    }

                ty = ty.type;
            }
            return false;
        }

        static void __Copy_Stack(Stack<Type> dest, Stack<Type> source)
        {
            Stack tmp = new Stack();

            while (source.Count > 0)
            {
                Type ty = (Type)source.Pop();
                tmp.Push(ty);
            }
            while (tmp.Count > 0)
            {
                Type ty = (Type)tmp.Pop();
                dest.Push(ty);
            }

        }

        static bool BuildPointerType(SyntaxStack ss, Production pp, string token)
        {
            IDeclTypeAttribute sp = ss.ss[ss.sp - 4] as IDeclTypeAttribute;

            sp.basety =BuildType(TypeOperator.POINTER, sp.basety,4,null);
            
            return true;
        }

        static Type BuildType(TypeOperator op, Type type, int size, List<Symbol> lsym)
        {
            Type ty = new Type();

            ty.op = op;
            ty.size = size;
            ty.type = type;
            ty.lsym = lsym;
            
            return ty;
        }

        #endregion

        #region // Declarators

        static bool ActionOnExpandDeclarator(SyntaxStack ss, Production pp, string token)
        {
            IDeclTypeAttribute sp = ss.ss[ss.sp - 1] as IDeclTypeAttribute; //Declarator
            IDeclTypeAttribute dp = null;

            dp = pp.inh[0] as IDeclTypeAttribute; // Pointer
            dp.basety = sp.basety;
            dp.sclass = sp.sclass;
            return true;
        }
       
        static bool ActionOnExpandPointer(SyntaxStack ss, Production pp, string token)
        {
            IDeclTypeAttribute sp = ss.ss[ss.sp - 1] as IDeclTypeAttribute; // Pointer
            IDeclTypeAttribute ip = null; 
            switch (pp.prodno)
            {
                case (22): // Pointer -> Star Pointer
                    ip = pp.inh[1] as IDeclTypeAttribute;
                    ip.basety = sp.basety;
                    ip.sclass = sp.sclass;
                    ip.np = sp.np;
                    ip.np++;
                    break;
                case (23): // Pointer -> Lambda (copy rule) 
                    ip = ss.ss[ss.sp - 4] as IDeclTypeAttribute; // Direct_Declarator
                    ip.basety = sp.basety;
                    ip.sclass = sp.sclass;
                    ip.np = sp.np;
                    break;
            }
            return true;
        }

        static bool ActionOnExpandDirectDecl(SyntaxStack ss, Production pp, string token)
        {
            IDeclTypeAttribute sp = ss.ss[ss.sp - 1] as IDeclTypeAttribute; // Direct_Declarator
            IDeclTypeAttribute ip = null;
            
            switch (pp.prodno)
            {
                case (17): // Direct_Declarator -> Id Direct_Declarator_Tail
                    ip = pp.inh[1] as IDeclTypeAttribute;
                    ip.basety = sp.basety;
                    ip.sclass = sp.sclass;
                    ip.np = sp.np;
                    break;
                case (18): // Direct_Declarator -> LParen Declarator RParen Direct_Declator_Tail
                    ip = pp.inh[3] as IDeclTypeAttribute;
                    ip.basety = sp.basety;
                    ip.sclass = sp.sclass;
                    ip.np = sp.np;
                    break;
            }
            return true;
        }

        static bool ActionOnExpandDirectDeclTail(SyntaxStack ss, Production pp, string token)
        {
            IDeclTypeAttribute sp = ss.ss[ss.sp - 1] as IDeclTypeAttribute; // Direct_Declarator_Tail
            IDeclTypeAttribute ip = null;

            switch (pp.prodno)
            {
                case (19): // Direct_Declarator_Tail -> LQParen Num RQParen Direct_Declator_Tail
                    ip = pp.inh[3] as IDeclTypeAttribute;
                    ip.basety = sp.basety;
                    ip.tmpty = sp.tmpty;
                    ip.sclass = sp.sclass;
                    ip.np = sp.np;
                    ip.id = sp.id;
                    __Copy_Stack(ip.stype, sp.stype);
                    break;
                case (20): // Direct_Declarator_Tail -> LParen Parameter_Type_list RParen Direct_Declator_Tail
                    ip = pp.inh[3] as IDeclTypeAttribute;
                    ip.basety = sp.basety;
                    ip.tmpty = sp.tmpty;
                    ip.sclass = sp.sclass;
                    ip.np = sp.np;
                    ip.id = sp.id;
                    __Copy_Stack(ip.stype, sp.stype);
                    break;
                case (21): // Direct_Declarator_Tail -> Lambda
                    SDeclTypeAttribute dp = ss.ss[ss.sp - 2] as SDeclTypeAttribute; // Declarator
                    dp.basety = sp.basety; 
                    dp.tmpty = sp.tmpty;
                    dp.sclass = sp.sclass;
                    dp.np = sp.np;
                    dp.id = sp.id;
                    __Copy_Stack(dp.stype, sp.stype);
                    break;
            }
            return true;
        }
        static bool ActionOnLoadIdToDirectDeclTail(SyntaxStack ss, Production pp, string token)
        {
            IDeclTypeAttribute sp = ss.ss[ss.sp - 3] as IDeclTypeAttribute; // Direct_declarator_Tail

            sp.id = token.ToUpper();
            return true;
        }

        static bool ActionOnLoadNumToDirectDeclTail(SyntaxStack ss, Production pp, string token)
        {
            IDeclTypeAttribute sp = ss.ss[ss.sp - 4] as IDeclTypeAttribute; // Direct_declarator_Tail

            sp.stype.Push(BuildType(TypeOperator.ARRAY,null, Convert.ToInt16(token),null));
            return true;
        }

        static bool ActionOnProcessTempDeclarator(SyntaxStack ss, Production pp)
        {
            SDeclTypeAttribute sp = ss.ss[ss.sp - 1] as SDeclTypeAttribute; // Declarator
            IDeclTypeAttribute ip = ss.ss[ss.sp - 4] as IDeclTypeAttribute; // Direct_Declarator_Tail
            Type ty = null, tty = null;
                        
            for (int _j = 0; _j < sp.np; _j++)
                tty = BuildType(TypeOperator.POINTER, tty, 4,null);
            for (int _j = 0; _j < sp.stype.Count; _j++)
            {
                ty = sp.stype.Pop();
                tty = BuildType(ty.op, tty, ty.size,ty.lsym);
            }
            ty = sp.tmpty;
            while (ty != null && ty.type != null)
                ty = ty.type;

            if (sp.tmpty == null)
                sp.tmpty = tty;
            else
                ty.type = tty;

            ip.tmpty = sp.tmpty;

            ip.id = sp.id;
            return true;
        }

        static bool ActionProcessSymbolTableDeclarator(SyntaxStack ss, Production pp)
        {
            SDeclTypeAttribute sp = ss.ss[ss.sp - 1] as SDeclTypeAttribute; // Declarator

            Type ty;
            int nitems = 0;

            for (int _j = 0; _j < sp.np; _j++)
                sp.basety = BuildType(TypeOperator.POINTER, sp.basety, 4, null);

            nitems = sp.stype.Count;
            for (int _j = 0; _j < nitems; _j++)
            {
                ty = sp.stype.Pop();
                sp.basety = BuildType(ty.op, sp.basety, ty.size, ty.lsym);
            }

            ty = sp.tmpty;
            while (ty != null && ty.type != null)
                ty = ty.type;
            if (ty != null)
            {
                ty.type = sp.basety;
                sp.basety = sp.tmpty;
            }

            Symbol sym = new Symbol();
            sym.name = sp.id;
            sym.sclass = sp.sclass;
            sym.type = sp.basety;

            if (CheckInvalidType(sp.basety))
                return false;

            EAGrammar._type = _FormatType(sym); // formatted output
            
            return true;
        }

         #endregion // declarators

        #region // Parameter Management

        static bool ActionOnExpandParameterTypeList(SyntaxStack ss, Production pp, string token)
        {
            IDeclTypeAttribute dp = null;
            switch (pp.prodno)
            {
                case (24):
                    dp = ss.ss[ss.sp - 4] as IDeclTypeAttribute; 
                    break;
                case (25):
                    dp = ss.ss[ss.sp - 4] as IDeclTypeAttribute; // dump empty arg list to Direct_Declarator_Tail
                    dp.stype.Push(BuildType(TypeOperator.FUNCTION, null, 0, null)); // no argument 
                    break;
             
            }
            return true;
        }


        static bool ActionOnExpandParameterList(SyntaxStack ss, Production pp, string token)
        {
            List<Symbol> lsym = new List<Symbol>();
            IDeclTypeAttribute sp = ss.ss[ss.sp - 1] as IDeclTypeAttribute;
            IDeclTypeAttribute dp=null;
            switch (pp.prodno)
            {
                case (27): // Parameter_List_Tail -> Comma Parameter_Declaration Parameter_List_Tail 
                     dp = pp.inh[2] as IDeclTypeAttribute;
                     foreach (Symbol sym in sp.lsym)
                         dp.lsym.Add(sym);
                     break; // Parameter_List_Tail ->Lambda 
                case (28):
                    dp = ss.ss[ss.sp - 4] as IDeclTypeAttribute; // dump symbols to Direct_Declarator_Tail
                    foreach (Symbol sym in sp.lsym)
                        lsym.Add(sym);

                    dp.stype.Push(BuildType(TypeOperator.FUNCTION,null,0,lsym));
                    break;
            }
            return true;
        }

        static bool ActionOnLoadParameterList(SyntaxStack ss, Production pp)
        {
            SDeclTypeAttribute sp = ss.ss[ss.sp - 1] as SDeclTypeAttribute;
            IDeclTypeAttribute ip = ss.ss[ss.sp - 3] as IDeclTypeAttribute;
            Type ty;
            int nitems = 0;

            for (int _j = 0; _j < sp.np; _j++)
                sp.basety = BuildType(TypeOperator.POINTER, sp.basety, 4,null);

            nitems = sp.stype.Count;
            for (int _j = 0; _j < nitems; _j++)
            {
                ty = sp.stype.Pop();
                sp.basety = BuildType(ty.op, sp.basety, ty.size,ty.lsym);
            }

            ty = sp.tmpty;
            while (ty != null && ty.type != null)
                ty = ty.type;
            if (ty != null)
            {
                ty.type = sp.basety;
                sp.basety = sp.tmpty;
            }

            // create a sym
            Symbol sym = new Symbol();
            sym.name = sp.id;
            sym.sclass = sp.sclass;
            sym.type = sp.basety;

            if (ScanParameterList(ip.lsym, sym.name))
                ip.lsym.Add(sym);
            else
            {
                ErrMsg = "duplicate parameter name : " + sym.name;
                return false;
            }
           
            return true;
        }

        static bool ScanParameterList(List<Symbol> lsym, string id)
        {
            int _j = 0;

            for (; _j < lsym.Count; _j++)
                if (id == lsym[_j].name)
                    return false;
            return true;
        }

        

        static bool ActionOnExpandParameterDeclarationTail(SyntaxStack ss, Production pp, string token)
        {
            IDeclTypeAttribute sp = ss.ss[ss.sp - 1] as IDeclTypeAttribute;
            IDeclTypeAttribute dp = null;
            switch (pp.prodno)
            {
                case (30): // Parameter_Declaration_Tail -> Declarator
                    dp = pp.inh[0] as IDeclTypeAttribute;
                    dp.basety = sp.basety;
                    dp.sclass = sp.sclass;
                    break;
            }
            return true;
        }

        #endregion

        static bool BuildBaseType(SyntaxStack ss, Production pp, string token)
        {
            SDeclTypeAttribute sp = ss.ss[ss.sp - 2] as SDeclTypeAttribute; // Type_specifier

            string ty = token.ToUpper();
            switch (ty)
            {
                case ("CHAR"):
                    sp.basety = BuildType(TypeOperator.CHAR, null, 1,null);
                    break;
                case ("INT"):
                    sp.basety = BuildType(TypeOperator.INT, null, 2,null);
                    break;
                case ("LONG"):
                    sp.basety = BuildType(TypeOperator.LONG, null, 4,null);
                    break;
                case ("VOID"):
                    sp.basety = BuildType(TypeOperator.VOID, null, 0, null);
                    break;
            }
            return true;
        }

        static bool ActionLoadBaseType(SyntaxStack ss, Production pp)
        {
            IDeclTypeAttribute ip = ss.ss[ss.sp - 3] as IDeclTypeAttribute; // Declarator
            SDeclTypeAttribute sp = ss.ss[ss.sp - 1] as SDeclTypeAttribute; // Type_specifier

            switch (sp.basety.op)
            {
                case (TypeOperator.VOID):
                case (TypeOperator.CHAR):
                case (TypeOperator.INT):
                case (TypeOperator.LONG):
                    ip.basety = sp.basety;
                    ip.basety.signed = ip.signed;
                break;
            }
            return true;
        }

        static bool ActionLoadStorageClassModifier(SyntaxStack ss, Production pp)
        {
            IDeclTypeAttribute ip = ss.ss[ss.sp - 7] as IDeclTypeAttribute;
            SDeclTypeAttribute sp = ss.ss[ss.sp - 1] as SDeclTypeAttribute;

            ip.sclass = sp.sclass;
            return true;
        }

        static bool ActionLoadSignSpecifier(SyntaxStack ss, Production pp)
        {
            IDeclTypeAttribute ip = ss.ss[ss.sp - 5] as IDeclTypeAttribute;
            SDeclTypeAttribute sp = ss.ss[ss.sp - 1] as SDeclTypeAttribute;

            ip.signed = sp.signed;
            return true;
        }

         static bool AddStorageClassModifier(SyntaxStack ss, Production pp, string token)
        {
            SDeclTypeAttribute sp = ss.ss[ss.sp - 2] as SDeclTypeAttribute;

            string ty = token.ToUpper();
            switch (ty)
            {
                case ("AUTO"):
                    sp.sclass = StorageClass.AUTO;
                    break;
                case ("STATIC"):
                    sp.sclass = StorageClass.STATIC;
                    break;
                case ("EXTERN"):
                    sp.sclass = StorageClass.EXTERN;
                    break;
                case ("REGISTER"):
                    sp.sclass = StorageClass.REGISTER;
                    break;
            }
            return true;
        }

         static bool ActionAddSignSpecifier(SyntaxStack ss, Production pp, string token)
         {
             SDeclTypeAttribute sp = ss.ss[ss.sp - 2] as SDeclTypeAttribute;

             string ty = token.ToUpper();
             switch (ty)
             {
                 case ("SIGNED"):
                     sp.signed = SignSpecifier.SIGNED;
                     break;
                 case ("UNSIGNED"):
                     sp.signed = SignSpecifier.UNSIGNED;
                     break;
             }
             return true;
         }

     }
}
