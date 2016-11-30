/* created on 30/11/2016 22:39:28 from peg generator V1.0 using 'WikiSample.txt' as input*/

using Peg.Base;
using System;
using System.IO;
using System.Text;
namespace WikiSampleTree
{
      
      enum EWikiSampleTree{Expr= 1, Sum= 2, Product= 3, Value= 4, Number= 5, S= 6};
      class WikiSampleTree : PegCharParser 
      {
        
         #region Input Properties
        public static EncodingClass encodingClass = EncodingClass.utf8;
        public static UnicodeDetection unicodeDetection = UnicodeDetection.notApplicable;
        #endregion Input Properties
        #region Constructors
        public WikiSampleTree()
            : base()
        {
            
        }
        public WikiSampleTree(string src,TextWriter FerrOut)
			: base(src,FerrOut)
        {
            
        }
        #endregion Constructors
        #region Overrides
        public override string GetRuleNameFromId(int id)
        {
            try
            {
                   EWikiSampleTree ruleEnum = (EWikiSampleTree)id;
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
        public bool Expr()    /*[1] ^^ Expr:   S Sum(!./FATAL<"end of input expected">);*/
        {

           return TreeNT((int)EWikiSampleTree.Expr,()=>
                And(()=>  
                     S()
                  && Sum()
                  && (    Not(()=> Any() ) || Fatal("end of input expected")) ) );
		}
        public bool Sum()    /*[2] ^Sum:     Product(^[+-] S Product)*               ;*/
        {

           return TreeAST((int)EWikiSampleTree.Sum,()=>
                And(()=>  
                     Product()
                  && OptRepeat(()=>    
                      And(()=>      
                               TreeChars(()=> OneOf("+-") )
                            && S()
                            && Product() ) ) ) );
		}
        public bool Product()    /*[3] ^Product: Value(^[* /] S Value)*                    ;*/
        {

           return TreeAST((int)EWikiSampleTree.Product,()=>
                And(()=>  
                     Value()
                  && OptRepeat(()=>    
                      And(()=>      
                               TreeChars(()=> OneOf("*/") )
                            && S()
                            && Value() ) ) ) );
		}
        public bool Value()    /*[4]
Value:    Number S / '(' S Sum ')' S  /
                 FATAL<"number or  ( <Sum> )  expected">;*/
        {

           return   
                     And(()=>    Number() && S() )
                  || And(()=>    
                         Char('(')
                      && S()
                      && Sum()
                      && Char(')')
                      && S() )
                  || Fatal("number or  ( <Sum> )  expected");
		}
        public bool Number()    /*[5] ^^Number: [0-9]+ ('.' [0-9]+)?                      ;*/
        {

           return TreeNT((int)EWikiSampleTree.Number,()=>
                And(()=>  
                     PlusRepeat(()=> In('0','9') )
                  && Option(()=>    
                      And(()=>      
                               Char('.')
                            && PlusRepeat(()=> In('0','9') ) ) ) ) );
		}
        public bool S()    /*[6]
S:        [ \n\r\t\v]*                              ;*/
        {

           return OptRepeat(()=> OneOf(" \n\r\t\v") );
		}
		#endregion Grammar Rules
   }
}