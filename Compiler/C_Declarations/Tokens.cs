using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AddressingModes
{
    public enum Category
    {
      Declaration,Declaration_Specifiers,Storage_Class_Specifier,Type_Specifier,
      Declarator,Direct_Declarator,Direct_Declarator_Tail,Pointer,Parameter_Type_List,Parameter_List,
      Parameter_List_Tail, Parameter_Declaration, Parameter_Declaration_Tail, 
      Sign_Specifier
    }
    public static class TokenLookup
    {
        public static string[] TokenNames =
        {
            "$","EOL", "Lambda","AUTO","REGISTER","STATIC","EXTERN","VOID","CHAR","SHORT","INT","LONG","SIGNED", 
            "UNSIGNED","Comma",
            "SemiColon","LParen","RParen","LSParen","RSParen","Star","Integer_Constant",
            "Id","UnKnown"
        };
        public enum TokenKind
        {
            EOI,EOL, Lambda,AUTO,REGISTER,STATIC,EXTERN,VOID,CHAR,SHORT,INT,LONG,SIGNED, 
            UNSIGNED,Comma,
            SemiColon,LParen,RParen,LSParen,RSParen,Star,Integer_Constant,
            Id,UnKnown
        };
    }
}

