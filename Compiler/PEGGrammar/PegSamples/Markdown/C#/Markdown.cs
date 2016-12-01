/* created on 01/12/2016 22:58:40 from peg generator V1.0 using 'Markdown' as input*/

using Peg.Base;
using System;
using System.IO;
using System.Text;
namespace Markdown
{
      
      enum EMarkdown{MarkdownText= 1, Link= 2, InnerText= 3, Text= 4, S= 5, expect_file_end= 6};
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
        public bool MarkdownText()    /*^^MarkdownText: S? (Link / Text)* S? expect_file_end ;*/
        {

           return TreeNT((int)EMarkdown.MarkdownText,()=>
                And(()=>  
                     Option(()=> S() )
                  && OptRepeat(()=>     Link() || Text() )
                  && Option(()=> S() )
                  && expect_file_end() ) );
		}
        public bool Link()    /*^^Link: '[' InnerText ']''(' InnerText ')' ;*/
        {

           return TreeNT((int)EMarkdown.Link,()=>
                And(()=>  
                     Char('[')
                  && InnerText()
                  && Char(']')
                  && Char('(')
                  && InnerText()
                  && Char(')') ) );
		}
        public bool InnerText()    /*InnerText: [#x20-#x21#x23-#xFFFF]+	;*/
        {

           return PlusRepeat(()=> In('\u0020','\u0021', '\u0023','\uffff') );
		}
        public bool Text()    /*Text: [#x20-#x21#x23-#xFFFF]+	;*/
        {

           return PlusRepeat(()=> In('\u0020','\u0021', '\u0023','\uffff') );
		}
        public bool S()    /*S:        [ \n\r\t\v]*	;*/
        {

           return OptRepeat(()=> OneOf(" \n\r\t\v") );
		}
        public bool expect_file_end()    /*expect_file_end:!./ WARNING<"non-json stuff before end of file">;*/
        {

           return   
                     Not(()=> Any() )
                  || Warning("non-json stuff before end of file");
		}
		#endregion Grammar Rules
   }
}