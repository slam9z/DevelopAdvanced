/* created on 12/2/2016 11:05:22 AM from peg generator V1.0 using 'Markdown' as input*/

using Peg.Base;
using System;
using System.IO;
using System.Text;
namespace Markdown
{
      
      enum EMarkdown{MarkdownText= 1, Link= 2, LinkText= 3, LinkUrl= 4, Text= 5, S= 6, 
                      expect_file_end= 7};
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
        public bool MarkdownText()    /*^^MarkdownText: S (Link / Text)* S expect_file_end ;*/
        {

           return TreeNT((int)EMarkdown.MarkdownText,()=>
                And(()=>  
                     S()
                  && OptRepeat(()=>     Link() || Text() )
                  && S()
                  && expect_file_end() ) );
		}
        public bool Link()    /*^^Link: '['  LinkText ']''('  LinkUrl ')' ;*/
        {

           return TreeNT((int)EMarkdown.Link,()=>
                And(()=>  
                     Char('[')
                  && LinkText()
                  && Char(']')
                  && Char('(')
                  && LinkUrl()
                  && Char(')') ) );
		}
        public bool LinkText()    /*^^LinkText: [#x20-#x5C#x5E-#xFFFF]+	;*/
        {

           return TreeNT((int)EMarkdown.LinkText,()=>
                PlusRepeat(()=> In('\u0020','\u005c', '\u005e','\uffff') ) );
		}
        public bool LinkUrl()    /*^^LinkUrl: [#x20-#x28#x2A-#xFFFF]+	;*/
        {

           return TreeNT((int)EMarkdown.LinkUrl,()=>
                PlusRepeat(()=> In('\u0020','\u0028', '\u002a','\uffff') ) );
		}
        public bool Text()    /*^^Text:S [#x20-#x21#x23-#xFFFF]+ S	;*/
        {

           return TreeNT((int)EMarkdown.Text,()=>
                And(()=>  
                     S()
                  && PlusRepeat(()=>    
                      In('\u0020','\u0021', '\u0023','\uffff') )
                  && S() ) );
		}
        public bool S()    /*S:        [ \n\r\t\v]*	;*/
        {

           return OptRepeat(()=> OneOf(" \n\r\t\v") );
		}
        public bool expect_file_end()    /*expect_file_end:!./ WARNING<" end of file">;*/
        {

           return     Not(()=> Any() ) || Warning(" end of file");
		}
		#endregion Grammar Rules
   }
}