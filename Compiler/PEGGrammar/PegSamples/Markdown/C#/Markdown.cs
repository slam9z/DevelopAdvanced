/* created on 06/12/2016 22:47:42 from peg generator V1.0 using 'Markdown' as input*/

using Peg.Base;
using System;
using System.IO;
using System.Text;
namespace Markdown
{
      
      enum EMarkdown{Document= 1, Image= 2, Link= 3, LinkText= 4, LinkUrl= 5, Code= 6, 
                      InnerCode= 7, Text= 8, SpecialChar= 9, S= 10, Eof= 11, Spacechar= 12, 
                      Nonspacechar= 13, Newline= 14, Sp= 15, Spnl= 16};
      public class Markdown : PegCharParser 
      {
        
         #region Input Properties
        public static EncodingClass encodingClass = EncodingClass.utf8;
        public static UnicodeDetection unicodeDetection = UnicodeDetection.notApplicable;
        #endregion Input Properties
        #region Constructors
        public Markdown()
            : base()
        {
            
        }
        public Markdown(string src,TextWriter FerrOut)
			: base(src,FerrOut)
        {
            
        }
        #endregion Constructors
        #region Overrides
        public override string GetRuleNameFromId(int id)
        {
            try
            {
                   EMarkdown ruleEnum = (EMarkdown)id;
                    string s= ruleEnum.ToString();
                    int val;
                    if( int.TryParse(s,out val) ){
                        return base.GetRuleNameFromId(id);
                    }else{
                        return s;
                    }
            }
            catch (Exception)
            {
                return base.GetRuleNameFromId(id);
            }
        }
        public override void GetProperties(out EncodingClass encoding, out UnicodeDetection detection)
        {
            encoding = encodingClass;
            detection = unicodeDetection;
        } 
        #endregion Overrides
		#region Grammar Rules
        public bool Document()    /*^^Document: S (Image/Link/ InnerCode /Code/Text)+ S Eof ;*/
        {

           return TreeNT((int)EMarkdown.Document,()=>
                And(()=>  
                     S()
                  && PlusRepeat(()=>    
                            
                               Image()
                            || Link()
                            || InnerCode()
                            || Code()
                            || Text() )
                  && S()
                  && Eof() ) );
		}
        public bool Image()    /*^^Image: S '!'Link S;*/
        {

           return TreeNT((int)EMarkdown.Image,()=>
                And(()=>    S() && Char('!') && Link() && S() ) );
		}
        public bool Link()    /*^^Link: S '['  LinkText ']''('  LinkUrl ')' S ;*/
        {

           return TreeNT((int)EMarkdown.Link,()=>
                And(()=>  
                     S()
                  && Char('[')
                  && LinkText()
                  && Char(']')
                  && Char('(')
                  && LinkUrl()
                  && Char(')')
                  && S() ) );
		}
        public bool LinkText()    /*^^LinkText: (!SpecialChar .)+	;*/
        {

           return TreeNT((int)EMarkdown.LinkText,()=>
                PlusRepeat(()=>  
                  And(()=>    Not(()=> SpecialChar() ) && Any() ) ) );
		}
        public bool LinkUrl()    /*^^LinkUrl: (!SpecialChar .)+	;*/
        {

           return TreeNT((int)EMarkdown.LinkUrl,()=>
                PlusRepeat(()=>  
                  And(()=>    Not(()=> SpecialChar() ) && Any() ) ) );
		}
        public bool Code()    /*^^Code: S '```'(![`] .)+ '```' S;*/
        {

           return TreeNT((int)EMarkdown.Code,()=>
                And(()=>  
                     S()
                  && Char('`','`','`')
                  && PlusRepeat(()=>    
                      And(()=>    Not(()=> OneOf("`") ) && Any() ) )
                  && Char('`','`','`')
                  && S() ) );
		}
        public bool InnerCode()    /*^^InnerCode:S '`' (![`] .)+ '`' S ;*/
        {

           return TreeNT((int)EMarkdown.InnerCode,()=>
                And(()=>  
                     S()
                  && Char('`')
                  && PlusRepeat(()=>    
                      And(()=>    Not(()=> OneOf("`") ) && Any() ) )
                  && Char('`')
                  && S() ) );
		}
        public bool Text()    /*^^Text: S (!SpecialChar .)+ S ;*/
        {

           return TreeNT((int)EMarkdown.Text,()=>
                And(()=>  
                     S()
                  && PlusRepeat(()=>    
                      And(()=>    Not(()=> SpecialChar() ) && Any() ) )
                  && S() ) );
		}
        public bool SpecialChar()    /*^SpecialChar :   [~*_`&\[\]()!#] ;*/
        {

           return TreeAST((int)EMarkdown.SpecialChar,()=>
                OneOf(optimizedCharset0) );
		}
        public bool S()    /*S:        [\n\r\t\v]*	;*/
        {

           return OptRepeat(()=> OneOf("\n\r\t\v") );
		}
        public bool Eof()    /*Eof :          !./ WARNING<" end of file">;*/
        {

           return     Not(()=> Any() ) || Warning(" end of file");
		}
        public bool Spacechar()    /*Spacechar :      ' ' / '\t';*/
        {

           return     Char(' ') || Char('\t');
		}
        public bool Nonspacechar()    /*Nonspacechar :    !Spacechar !Newline ;*/
        {

           return And(()=>  
                     Not(()=> Spacechar() )
                  && Not(()=> Newline() ) );
		}
        public bool Newline()    /*Newline :       '\n' / '\r''\n'?;*/
        {

           return   
                     Char('\n')
                  || And(()=>    Char('\r') && Option(()=> Char('\n') ) );
		}
        public bool Sp()    /*Sp :            Spacechar*;*/
        {

           return OptRepeat(()=> Spacechar() );
		}
        public bool Spnl()    /*Spnl :           Sp (Newline Sp)?;*/
        {

           return And(()=>  
                     Sp()
                  && Option(()=> And(()=>    Newline() && Sp() ) ) );
		}
		#endregion Grammar Rules

        #region Optimization Data 
        internal static OptimizedCharset optimizedCharset0;
        
        
        static Markdown()
        {
            {
               char[] oneOfChars = new char[]    {'~','*','_','`','&'
                                                  ,'\\','[',']','(',')'
                                                  ,'!','#'};
               optimizedCharset0= new OptimizedCharset(null,oneOfChars);
            }
            
            
            
        }
        #endregion Optimization Data 
           }
}