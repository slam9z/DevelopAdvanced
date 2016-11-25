using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace AddressingModes
{
    public enum SignSpecifier { SIGNED, UNSIGNED, UnKnown };
    public enum StorageClass { STATIC, AUTO, EXTERN, REGISTER, UnKnown };
    public enum TypeOperator { POINTER, ARRAY, FUNCTION, CHAR, INT, SHORT, LONG,  VOID, UnKnown };

    public class Symbol
    {
        public string name;
        public bool constvalue = false;
        public bool deflabel = false;


        public ArrayList Values = new ArrayList();
        public string label;
        public Int32 longvalue = 0;
        public UInt32 ulongvalue=0;
        public Int16 intvalue = 0;
        public UInt16 uintvalue = 0;
        public string stringvalue = "";
        public char charvalue = '\0';
        
        public StorageClass sclass=StorageClass.UnKnown;
        public Type type=null;
        List<SyntaxSymbol> parms = new List<SyntaxSymbol>(); // function arguments

        public bool temporary = false;
        public bool defined = false;
    };

    public class Type
    {
        public TypeOperator op=TypeOperator.UnKnown;
      
        public Type type=null;
        public int size=0;
        public SignSpecifier signed = SignSpecifier.SIGNED;
        public List<Symbol> lsym = null;
    }
}
