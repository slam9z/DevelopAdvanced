/* created on 20/12/2016 23:45:51 from peg generator V1.0 using 'Html' as input*/

using Peg.Base;
using System;
using System.IO;
using System.Text;
namespace Peg.Html
{
      
      enum EHtml{Doc= 1, Block= 2, HtmlBlockOpenAddress= 3, HtmlBlockCloseAddress= 4, 
                  HtmlBlockAddress= 5, HtmlBlockOpenBlockquote= 6, HtmlBlockCloseBlockquote= 7, 
                  HtmlBlockBlockquote= 8, HtmlBlockOpenCenter= 9, HtmlBlockCloseCenter= 10, 
                  HtmlBlockCenter= 11, HtmlBlockOpenDir= 12, HtmlBlockCloseDir= 13, 
                  HtmlBlockDir= 14, HtmlBlockOpenDiv= 15, HtmlBlockCloseDiv= 16, 
                  HtmlBlockDiv= 17, HtmlBlockOpenDl= 18, HtmlBlockCloseDl= 19, 
                  HtmlBlockDl= 20, HtmlBlockOpenFieldset= 21, HtmlBlockCloseFieldset= 22, 
                  HtmlBlockFieldset= 23, HtmlBlockOpenForm= 24, HtmlBlockCloseForm= 25, 
                  HtmlBlockForm= 26, HtmlBlockOpenH1= 27, HtmlBlockCloseH1= 28, 
                  HtmlBlockH1= 29, HtmlBlockOpenH2= 30, HtmlBlockCloseH2= 31, HtmlBlockH2= 32, 
                  HtmlBlockOpenH3= 33, HtmlBlockCloseH3= 34, HtmlBlockH3= 35, HtmlBlockOpenH4= 36, 
                  HtmlBlockCloseH4= 37, HtmlBlockH4= 38, HtmlBlockOpenH5= 39, HtmlBlockCloseH5= 40, 
                  HtmlBlockH5= 41, HtmlBlockOpenH6= 42, HtmlBlockCloseH6= 43, HtmlBlockH6= 44, 
                  HtmlBlockOpenMenu= 45, HtmlBlockCloseMenu= 46, HtmlBlockMenu= 47, 
                  HtmlBlockOpenNoframes= 48, HtmlBlockCloseNoframes= 49, HtmlBlockNoframes= 50, 
                  HtmlBlockOpenNoscript= 51, HtmlBlockCloseNoscript= 52, HtmlBlockNoscript= 53, 
                  HtmlBlockOpenOl= 54, HtmlBlockCloseOl= 55, HtmlBlockOl= 56, HtmlBlockOpenP= 57, 
                  HtmlBlockCloseP= 58, HtmlBlockP= 59, HtmlBlockOpenPre= 60, HtmlBlockClosePre= 61, 
                  HtmlBlockPre= 62, HtmlBlockOpenTable= 63, HtmlBlockCloseTable= 64, 
                  HtmlBlockTable= 65, HtmlBlockOpenUl= 66, HtmlBlockCloseUl= 67, 
                  HtmlBlockUl= 68, HtmlBlockOpenDd= 69, HtmlBlockCloseDd= 70, HtmlBlockDd= 71, 
                  HtmlBlockOpenDt= 72, HtmlBlockCloseDt= 73, HtmlBlockDt= 74, HtmlBlockOpenFrameset= 75, 
                  HtmlBlockCloseFrameset= 76, HtmlBlockFrameset= 77, HtmlBlockOpenLi= 78, 
                  HtmlBlockCloseLi= 79, HtmlBlockLi= 80, HtmlBlockOpenTbody= 81, 
                  HtmlBlockCloseTbody= 82, HtmlBlockTbody= 83, HtmlBlockOpenTd= 84, 
                  HtmlBlockCloseTd= 85, HtmlBlockTd= 86, HtmlBlockOpenTfoot= 87, 
                  HtmlBlockCloseTfoot= 88, HtmlBlockTfoot= 89, HtmlBlockOpenTh= 90, 
                  HtmlBlockCloseTh= 91, HtmlBlockTh= 92, HtmlBlockOpenThead= 93, 
                  HtmlBlockCloseThead= 94, HtmlBlockThead= 95, HtmlBlockOpenTr= 96, 
                  HtmlBlockCloseTr= 97, HtmlBlockTr= 98, HtmlBlockOpenScript= 99, 
                  HtmlBlockCloseScript= 100, HtmlBlockScript= 101, HtmlBlockOpenHead= 102, 
                  HtmlBlockCloseHead= 103, HtmlBlockHead= 104, HtmlBlockOpenA= 105, 
                  HtmlBlockCloseA= 106, HtmlBlockA= 107, HtmlBlockOpenCode= 108, 
                  HtmlBlockCloseCode= 109, HtmlBlockCode= 110, HtmlBlockOpenSpan= 111, 
                  HtmlBlockCloseSpan= 112, HtmlBlockSpan= 113, HtmlBlockOpenUnknown= 114, 
                  HtmlBlockCloseUnknown= 115, HtmlBlockUnknown= 116, UnknownTagName= 117, 
                  HtmlBlockInTags= 118, HtmlBlock= 119, HtmlBlockSelfClosing= 120, 
                  HtmlBlockType= 121, StyleOpen= 122, StyleClose= 123, InStyleTags= 124, 
                  StyleBlock= 125, Space= 126, RawHtml= 127, BlankLine= 128, Quoted= 129, 
                  HtmlAttribute= 130, HtmlComment= 131, HtmlTag= 132, Spacechar= 133, 
                  Nonspacechar= 134, Newline= 135, Sp= 136, Spnl= 137, AlphanumericAscii= 138, 
                  SpecialChar= 139, NormalChar= 140, LiteralChar= 141, Symbol= 142, 
                  InnerPlain= 143, Eof= 144};
      public class Html : PegCharParser 
      {
        
         #region Input Properties
        public static EncodingClass encodingClass = EncodingClass.utf8;
        public static UnicodeDetection unicodeDetection = UnicodeDetection.notApplicable;
        #endregion Input Properties
        #region Constructors
        public Html()
            : base()
        {
            
        }
        public Html(string src,TextWriter FerrOut)
			: base(src,FerrOut)
        {
            
        }
        #endregion Constructors
        #region Overrides
        public override string GetRuleNameFromId(int id)
        {
            try
            {
                   EHtml ruleEnum = (EHtml)id;
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
        public bool Doc()    /*^^Doc :      Block  *  Eof;*/
        {

           var result= TreeNT((int)EHtml.Doc,()=>
                And(()=>    OptRepeat(()=> Block() ) && Eof() ) ); return result;
		}
        public bool Block()    /*^^Block :     BlankLine*
            ( HtmlBlock / StyleBlock);


// Parsers for different kinds of block-level HTML content.
// This is repetitive due to constraints of PEG grammar.*/
        {

           var result= TreeNT((int)EHtml.Block,()=>
                And(()=>  
                     OptRepeat(()=> BlankLine() )
                  && (    HtmlBlock() || StyleBlock()) ) ); return result;
		}
        public bool HtmlBlockOpenAddress()    /*HtmlBlockOpenAddress : '<' Spnl ('address' / 'ADDRESS') Spnl HtmlAttribute* '>';*/
        {

           var result=And(()=>  
                     Char('<')
                  && Spnl()
                  && (    
                         Char('a','d','d','r','e','s','s')
                      || Char('A','D','D','R','E','S','S'))
                  && Spnl()
                  && OptRepeat(()=> HtmlAttribute() )
                  && Char('>') ); return result;
		}
        public bool HtmlBlockCloseAddress()    /*HtmlBlockCloseAddress : '<' Spnl '/' ('address' / 'ADDRESS') Spnl '>' ;*/
        {

           var result=And(()=>  
                     Char('<')
                  && Spnl()
                  && Char('/')
                  && (    
                         Char('a','d','d','r','e','s','s')
                      || Char('A','D','D','R','E','S','S'))
                  && Spnl()
                  && Char('>') ); return result;
		}
        public bool HtmlBlockAddress()    /*^^HtmlBlockAddress : HtmlBlockOpenAddress (&HtmlBlockCloseAddress / HtmlBlock+) HtmlBlockCloseAddress;*/
        {

           var result= TreeNT((int)EHtml.HtmlBlockAddress,()=>
                And(()=>  
                     HtmlBlockOpenAddress()
                  && (    
                         Peek(()=> HtmlBlockCloseAddress() )
                      || PlusRepeat(()=> HtmlBlock() ))
                  && HtmlBlockCloseAddress() ) ); return result;
		}
        public bool HtmlBlockOpenBlockquote()    /*HtmlBlockOpenBlockquote : '<' Spnl ('blockquote' / 'BLOCKQUOTE') Spnl HtmlAttribute* '>';*/
        {

           var result=And(()=>  
                     Char('<')
                  && Spnl()
                  && (    Char("blockquote") || Char("BLOCKQUOTE"))
                  && Spnl()
                  && OptRepeat(()=> HtmlAttribute() )
                  && Char('>') ); return result;
		}
        public bool HtmlBlockCloseBlockquote()    /*HtmlBlockCloseBlockquote : '<' Spnl '/' ('blockquote' / 'BLOCKQUOTE') Spnl '>';*/
        {

           var result=And(()=>  
                     Char('<')
                  && Spnl()
                  && Char('/')
                  && (    Char("blockquote") || Char("BLOCKQUOTE"))
                  && Spnl()
                  && Char('>') ); return result;
		}
        public bool HtmlBlockBlockquote()    /*^^HtmlBlockBlockquote : HtmlBlockOpenBlockquote (&HtmlBlockCloseBlockquote / HtmlBlock+) HtmlBlockCloseBlockquote;*/
        {

           var result= TreeNT((int)EHtml.HtmlBlockBlockquote,()=>
                And(()=>  
                     HtmlBlockOpenBlockquote()
                  && (    
                         Peek(()=> HtmlBlockCloseBlockquote() )
                      || PlusRepeat(()=> HtmlBlock() ))
                  && HtmlBlockCloseBlockquote() ) ); return result;
		}
        public bool HtmlBlockOpenCenter()    /*HtmlBlockOpenCenter : '<' Spnl ('center' / 'CENTER') Spnl HtmlAttribute* '>';*/
        {

           var result=And(()=>  
                     Char('<')
                  && Spnl()
                  && (    
                         Char('c','e','n','t','e','r')
                      || Char('C','E','N','T','E','R'))
                  && Spnl()
                  && OptRepeat(()=> HtmlAttribute() )
                  && Char('>') ); return result;
		}
        public bool HtmlBlockCloseCenter()    /*HtmlBlockCloseCenter : '<' Spnl '/' ('center' / 'CENTER') Spnl '>';*/
        {

           var result=And(()=>  
                     Char('<')
                  && Spnl()
                  && Char('/')
                  && (    
                         Char('c','e','n','t','e','r')
                      || Char('C','E','N','T','E','R'))
                  && Spnl()
                  && Char('>') ); return result;
		}
        public bool HtmlBlockCenter()    /*^^HtmlBlockCenter : HtmlBlockOpenCenter (&HtmlBlockCloseCenter / HtmlBlock+) HtmlBlockCloseCenter;*/
        {

           var result= TreeNT((int)EHtml.HtmlBlockCenter,()=>
                And(()=>  
                     HtmlBlockOpenCenter()
                  && (    
                         Peek(()=> HtmlBlockCloseCenter() )
                      || PlusRepeat(()=> HtmlBlock() ))
                  && HtmlBlockCloseCenter() ) ); return result;
		}
        public bool HtmlBlockOpenDir()    /*HtmlBlockOpenDir : '<' Spnl ('dir' / 'DIR') Spnl HtmlAttribute* '>';*/
        {

           var result=And(()=>  
                     Char('<')
                  && Spnl()
                  && (    Char('d','i','r') || Char('D','I','R'))
                  && Spnl()
                  && OptRepeat(()=> HtmlAttribute() )
                  && Char('>') ); return result;
		}
        public bool HtmlBlockCloseDir()    /*HtmlBlockCloseDir : '<' Spnl '/' ('dir' / 'DIR') Spnl '>';*/
        {

           var result=And(()=>  
                     Char('<')
                  && Spnl()
                  && Char('/')
                  && (    Char('d','i','r') || Char('D','I','R'))
                  && Spnl()
                  && Char('>') ); return result;
		}
        public bool HtmlBlockDir()    /*^^HtmlBlockDir : HtmlBlockOpenDir (&HtmlBlockCloseDir / HtmlBlock+) HtmlBlockCloseDir;*/
        {

           var result= TreeNT((int)EHtml.HtmlBlockDir,()=>
                And(()=>  
                     HtmlBlockOpenDir()
                  && (    
                         Peek(()=> HtmlBlockCloseDir() )
                      || PlusRepeat(()=> HtmlBlock() ))
                  && HtmlBlockCloseDir() ) ); return result;
		}
        public bool HtmlBlockOpenDiv()    /*^^HtmlBlockOpenDiv : '<' Spnl ('div' / 'DIV') Spnl HtmlAttribute* '>';*/
        {

           var result= TreeNT((int)EHtml.HtmlBlockOpenDiv,()=>
                And(()=>  
                     Char('<')
                  && Spnl()
                  && (    Char('d','i','v') || Char('D','I','V'))
                  && Spnl()
                  && OptRepeat(()=> HtmlAttribute() )
                  && Char('>') ) ); return result;
		}
        public bool HtmlBlockCloseDiv()    /*^^HtmlBlockCloseDiv : '<' Spnl '/' ('div' / 'DIV') Spnl '>';*/
        {

           var result= TreeNT((int)EHtml.HtmlBlockCloseDiv,()=>
                And(()=>  
                     Char('<')
                  && Spnl()
                  && Char('/')
                  && (    Char('d','i','v') || Char('D','I','V'))
                  && Spnl()
                  && Char('>') ) ); return result;
		}
        public bool HtmlBlockDiv()    /*^^HtmlBlockDiv : HtmlBlockOpenDiv (&  HtmlBlockCloseDiv / HtmlBlock+) HtmlBlockCloseDiv;*/
        {

           var result= TreeNT((int)EHtml.HtmlBlockDiv,()=>
                And(()=>  
                     HtmlBlockOpenDiv()
                  && (    
                         Peek(()=> HtmlBlockCloseDiv() )
                      || PlusRepeat(()=> HtmlBlock() ))
                  && HtmlBlockCloseDiv() ) ); return result;
		}
        public bool HtmlBlockOpenDl()    /*HtmlBlockOpenDl : '<' Spnl ('dl' / 'DL') Spnl HtmlAttribute* '>';*/
        {

           var result=And(()=>  
                     Char('<')
                  && Spnl()
                  && (    Char('d','l') || Char('D','L'))
                  && Spnl()
                  && OptRepeat(()=> HtmlAttribute() )
                  && Char('>') ); return result;
		}
        public bool HtmlBlockCloseDl()    /*HtmlBlockCloseDl : '<' Spnl '/' ('dl' / 'DL') Spnl '>';*/
        {

           var result=And(()=>  
                     Char('<')
                  && Spnl()
                  && Char('/')
                  && (    Char('d','l') || Char('D','L'))
                  && Spnl()
                  && Char('>') ); return result;
		}
        public bool HtmlBlockDl()    /*^^HtmlBlockDl : HtmlBlockOpenDl (&HtmlBlockCloseDl / HtmlBlock+) HtmlBlockCloseDl;*/
        {

           var result= TreeNT((int)EHtml.HtmlBlockDl,()=>
                And(()=>  
                     HtmlBlockOpenDl()
                  && (    
                         Peek(()=> HtmlBlockCloseDl() )
                      || PlusRepeat(()=> HtmlBlock() ))
                  && HtmlBlockCloseDl() ) ); return result;
		}
        public bool HtmlBlockOpenFieldset()    /*HtmlBlockOpenFieldset : '<' Spnl ('fieldset' / 'FIELDSET') Spnl HtmlAttribute* '>';*/
        {

           var result=And(()=>  
                     Char('<')
                  && Spnl()
                  && (    Char("fieldset") || Char("FIELDSET"))
                  && Spnl()
                  && OptRepeat(()=> HtmlAttribute() )
                  && Char('>') ); return result;
		}
        public bool HtmlBlockCloseFieldset()    /*HtmlBlockCloseFieldset : '<' Spnl '/' ('fieldset' / 'FIELDSET') Spnl '>';*/
        {

           var result=And(()=>  
                     Char('<')
                  && Spnl()
                  && Char('/')
                  && (    Char("fieldset") || Char("FIELDSET"))
                  && Spnl()
                  && Char('>') ); return result;
		}
        public bool HtmlBlockFieldset()    /*^^HtmlBlockFieldset : HtmlBlockOpenFieldset (&HtmlBlockCloseFieldset / HtmlBlock+) HtmlBlockCloseFieldset;*/
        {

           var result= TreeNT((int)EHtml.HtmlBlockFieldset,()=>
                And(()=>  
                     HtmlBlockOpenFieldset()
                  && (    
                         Peek(()=> HtmlBlockCloseFieldset() )
                      || PlusRepeat(()=> HtmlBlock() ))
                  && HtmlBlockCloseFieldset() ) ); return result;
		}
        public bool HtmlBlockOpenForm()    /*HtmlBlockOpenForm : '<' Spnl ('form' / 'FORM') Spnl HtmlAttribute* '>';*/
        {

           var result=And(()=>  
                     Char('<')
                  && Spnl()
                  && (    Char('f','o','r','m') || Char('F','O','R','M'))
                  && Spnl()
                  && OptRepeat(()=> HtmlAttribute() )
                  && Char('>') ); return result;
		}
        public bool HtmlBlockCloseForm()    /*HtmlBlockCloseForm : '<' Spnl '/' ('form' / 'FORM') Spnl '>';*/
        {

           var result=And(()=>  
                     Char('<')
                  && Spnl()
                  && Char('/')
                  && (    Char('f','o','r','m') || Char('F','O','R','M'))
                  && Spnl()
                  && Char('>') ); return result;
		}
        public bool HtmlBlockForm()    /*^^HtmlBlockForm : HtmlBlockOpenForm (&HtmlBlockCloseForm / HtmlBlock+) HtmlBlockCloseForm;*/
        {

           var result= TreeNT((int)EHtml.HtmlBlockForm,()=>
                And(()=>  
                     HtmlBlockOpenForm()
                  && (    
                         Peek(()=> HtmlBlockCloseForm() )
                      || PlusRepeat(()=> HtmlBlock() ))
                  && HtmlBlockCloseForm() ) ); return result;
		}
        public bool HtmlBlockOpenH1()    /*HtmlBlockOpenH1 : '<' Spnl ('h1' / 'H1') Spnl HtmlAttribute* '>';*/
        {

           var result=And(()=>  
                     Char('<')
                  && Spnl()
                  && (    Char('h','1') || Char('H','1'))
                  && Spnl()
                  && OptRepeat(()=> HtmlAttribute() )
                  && Char('>') ); return result;
		}
        public bool HtmlBlockCloseH1()    /*HtmlBlockCloseH1 : '<' Spnl '/' ('h1' / 'H1') Spnl '>';*/
        {

           var result=And(()=>  
                     Char('<')
                  && Spnl()
                  && Char('/')
                  && (    Char('h','1') || Char('H','1'))
                  && Spnl()
                  && Char('>') ); return result;
		}
        public bool HtmlBlockH1()    /*^^HtmlBlockH1 : HtmlBlockOpenH1 (&HtmlBlockCloseH1 / HtmlBlock+) HtmlBlockCloseH1;*/
        {

           var result= TreeNT((int)EHtml.HtmlBlockH1,()=>
                And(()=>  
                     HtmlBlockOpenH1()
                  && (    
                         Peek(()=> HtmlBlockCloseH1() )
                      || PlusRepeat(()=> HtmlBlock() ))
                  && HtmlBlockCloseH1() ) ); return result;
		}
        public bool HtmlBlockOpenH2()    /*HtmlBlockOpenH2 : '<' Spnl ('h2' / 'H2') Spnl HtmlAttribute* '>';*/
        {

           var result=And(()=>  
                     Char('<')
                  && Spnl()
                  && (    Char('h','2') || Char('H','2'))
                  && Spnl()
                  && OptRepeat(()=> HtmlAttribute() )
                  && Char('>') ); return result;
		}
        public bool HtmlBlockCloseH2()    /*HtmlBlockCloseH2 : '<' Spnl '/' ('h2' / 'H2') Spnl '>';*/
        {

           var result=And(()=>  
                     Char('<')
                  && Spnl()
                  && Char('/')
                  && (    Char('h','2') || Char('H','2'))
                  && Spnl()
                  && Char('>') ); return result;
		}
        public bool HtmlBlockH2()    /*^^HtmlBlockH2 : HtmlBlockOpenH2 (&HtmlBlockCloseH2 / HtmlBlock+) HtmlBlockCloseH2;*/
        {

           var result= TreeNT((int)EHtml.HtmlBlockH2,()=>
                And(()=>  
                     HtmlBlockOpenH2()
                  && (    
                         Peek(()=> HtmlBlockCloseH2() )
                      || PlusRepeat(()=> HtmlBlock() ))
                  && HtmlBlockCloseH2() ) ); return result;
		}
        public bool HtmlBlockOpenH3()    /*HtmlBlockOpenH3 : '<' Spnl ('h3' / 'H3') Spnl HtmlAttribute* '>';*/
        {

           var result=And(()=>  
                     Char('<')
                  && Spnl()
                  && (    Char('h','3') || Char('H','3'))
                  && Spnl()
                  && OptRepeat(()=> HtmlAttribute() )
                  && Char('>') ); return result;
		}
        public bool HtmlBlockCloseH3()    /*HtmlBlockCloseH3 : '<' Spnl '/' ('h3' / 'H3') Spnl '>';*/
        {

           var result=And(()=>  
                     Char('<')
                  && Spnl()
                  && Char('/')
                  && (    Char('h','3') || Char('H','3'))
                  && Spnl()
                  && Char('>') ); return result;
		}
        public bool HtmlBlockH3()    /*^^HtmlBlockH3 : HtmlBlockOpenH3 (&HtmlBlockCloseH3 / HtmlBlock+) HtmlBlockCloseH3;*/
        {

           var result= TreeNT((int)EHtml.HtmlBlockH3,()=>
                And(()=>  
                     HtmlBlockOpenH3()
                  && (    
                         Peek(()=> HtmlBlockCloseH3() )
                      || PlusRepeat(()=> HtmlBlock() ))
                  && HtmlBlockCloseH3() ) ); return result;
		}
        public bool HtmlBlockOpenH4()    /*HtmlBlockOpenH4 : '<' Spnl ('h4' / 'H4') Spnl HtmlAttribute* '>';*/
        {

           var result=And(()=>  
                     Char('<')
                  && Spnl()
                  && (    Char('h','4') || Char('H','4'))
                  && Spnl()
                  && OptRepeat(()=> HtmlAttribute() )
                  && Char('>') ); return result;
		}
        public bool HtmlBlockCloseH4()    /*HtmlBlockCloseH4 : '<' Spnl '/' ('h4' / 'H4') Spnl '>';*/
        {

           var result=And(()=>  
                     Char('<')
                  && Spnl()
                  && Char('/')
                  && (    Char('h','4') || Char('H','4'))
                  && Spnl()
                  && Char('>') ); return result;
		}
        public bool HtmlBlockH4()    /*HtmlBlockH4 : HtmlBlockOpenH4 (&HtmlBlockCloseH4 / HtmlBlock+) HtmlBlockCloseH4;*/
        {

           var result=And(()=>  
                     HtmlBlockOpenH4()
                  && (    
                         Peek(()=> HtmlBlockCloseH4() )
                      || PlusRepeat(()=> HtmlBlock() ))
                  && HtmlBlockCloseH4() ); return result;
		}
        public bool HtmlBlockOpenH5()    /*HtmlBlockOpenH5 : '<' Spnl ('h5' / 'H5') Spnl HtmlAttribute* '>';*/
        {

           var result=And(()=>  
                     Char('<')
                  && Spnl()
                  && (    Char('h','5') || Char('H','5'))
                  && Spnl()
                  && OptRepeat(()=> HtmlAttribute() )
                  && Char('>') ); return result;
		}
        public bool HtmlBlockCloseH5()    /*HtmlBlockCloseH5 : '<' Spnl '/' ('h5' / 'H5') Spnl '>';*/
        {

           var result=And(()=>  
                     Char('<')
                  && Spnl()
                  && Char('/')
                  && (    Char('h','5') || Char('H','5'))
                  && Spnl()
                  && Char('>') ); return result;
		}
        public bool HtmlBlockH5()    /*^^HtmlBlockH5 : HtmlBlockOpenH5 (&HtmlBlockCloseH5 / HtmlBlock+) HtmlBlockCloseH5;*/
        {

           var result= TreeNT((int)EHtml.HtmlBlockH5,()=>
                And(()=>  
                     HtmlBlockOpenH5()
                  && (    
                         Peek(()=> HtmlBlockCloseH5() )
                      || PlusRepeat(()=> HtmlBlock() ))
                  && HtmlBlockCloseH5() ) ); return result;
		}
        public bool HtmlBlockOpenH6()    /*HtmlBlockOpenH6 : '<' Spnl ('h6' / 'H6') Spnl HtmlAttribute* '>';*/
        {

           var result=And(()=>  
                     Char('<')
                  && Spnl()
                  && (    Char('h','6') || Char('H','6'))
                  && Spnl()
                  && OptRepeat(()=> HtmlAttribute() )
                  && Char('>') ); return result;
		}
        public bool HtmlBlockCloseH6()    /*HtmlBlockCloseH6 : '<' Spnl '/' ('h6' / 'H6') Spnl '>';*/
        {

           var result=And(()=>  
                     Char('<')
                  && Spnl()
                  && Char('/')
                  && (    Char('h','6') || Char('H','6'))
                  && Spnl()
                  && Char('>') ); return result;
		}
        public bool HtmlBlockH6()    /*^^HtmlBlockH6 : HtmlBlockOpenH6 (&HtmlBlockCloseH6 / HtmlBlock+) HtmlBlockCloseH6;*/
        {

           var result= TreeNT((int)EHtml.HtmlBlockH6,()=>
                And(()=>  
                     HtmlBlockOpenH6()
                  && (    
                         Peek(()=> HtmlBlockCloseH6() )
                      || PlusRepeat(()=> HtmlBlock() ))
                  && HtmlBlockCloseH6() ) ); return result;
		}
        public bool HtmlBlockOpenMenu()    /*HtmlBlockOpenMenu : '<' Spnl ('menu' / 'MENU') Spnl HtmlAttribute* '>';*/
        {

           var result=And(()=>  
                     Char('<')
                  && Spnl()
                  && (    Char('m','e','n','u') || Char('M','E','N','U'))
                  && Spnl()
                  && OptRepeat(()=> HtmlAttribute() )
                  && Char('>') ); return result;
		}
        public bool HtmlBlockCloseMenu()    /*HtmlBlockCloseMenu : '<' Spnl '/' ('menu' / 'MENU') Spnl '>';*/
        {

           var result=And(()=>  
                     Char('<')
                  && Spnl()
                  && Char('/')
                  && (    Char('m','e','n','u') || Char('M','E','N','U'))
                  && Spnl()
                  && Char('>') ); return result;
		}
        public bool HtmlBlockMenu()    /*^^HtmlBlockMenu : HtmlBlockOpenMenu (&HtmlBlockCloseMenu / HtmlBlock+) HtmlBlockCloseMenu;*/
        {

           var result= TreeNT((int)EHtml.HtmlBlockMenu,()=>
                And(()=>  
                     HtmlBlockOpenMenu()
                  && (    
                         Peek(()=> HtmlBlockCloseMenu() )
                      || PlusRepeat(()=> HtmlBlock() ))
                  && HtmlBlockCloseMenu() ) ); return result;
		}
        public bool HtmlBlockOpenNoframes()    /*HtmlBlockOpenNoframes : '<' Spnl ('noframes' / 'NOFRAMES') Spnl HtmlAttribute* '>';*/
        {

           var result=And(()=>  
                     Char('<')
                  && Spnl()
                  && (    Char("noframes") || Char("NOFRAMES"))
                  && Spnl()
                  && OptRepeat(()=> HtmlAttribute() )
                  && Char('>') ); return result;
		}
        public bool HtmlBlockCloseNoframes()    /*HtmlBlockCloseNoframes : '<' Spnl '/' ('noframes' / 'NOFRAMES') Spnl '>';*/
        {

           var result=And(()=>  
                     Char('<')
                  && Spnl()
                  && Char('/')
                  && (    Char("noframes") || Char("NOFRAMES"))
                  && Spnl()
                  && Char('>') ); return result;
		}
        public bool HtmlBlockNoframes()    /*^^HtmlBlockNoframes : HtmlBlockOpenNoframes (HtmlBlockNoframes / !HtmlBlockCloseNoframes .)* HtmlBlockCloseNoframes;*/
        {

           var result= TreeNT((int)EHtml.HtmlBlockNoframes,()=>
                And(()=>  
                     HtmlBlockOpenNoframes()
                  && OptRepeat(()=>    
                            
                               HtmlBlockNoframes()
                            || And(()=>        
                                       Not(()=> HtmlBlockCloseNoframes() )
                                    && Any() ) )
                  && HtmlBlockCloseNoframes() ) ); return result;
		}
        public bool HtmlBlockOpenNoscript()    /*HtmlBlockOpenNoscript : '<' Spnl ('noscript' / 'NOSCRIPT') Spnl HtmlAttribute* '>';*/
        {

           var result=And(()=>  
                     Char('<')
                  && Spnl()
                  && (    Char("noscript") || Char("NOSCRIPT"))
                  && Spnl()
                  && OptRepeat(()=> HtmlAttribute() )
                  && Char('>') ); return result;
		}
        public bool HtmlBlockCloseNoscript()    /*HtmlBlockCloseNoscript : '<' Spnl '/' ('noscript' / 'NOSCRIPT') Spnl '>';*/
        {

           var result=And(()=>  
                     Char('<')
                  && Spnl()
                  && Char('/')
                  && (    Char("noscript") || Char("NOSCRIPT"))
                  && Spnl()
                  && Char('>') ); return result;
		}
        public bool HtmlBlockNoscript()    /*^^HtmlBlockNoscript : HtmlBlockOpenNoscript (HtmlBlockNoscript / !HtmlBlockCloseNoscript .)* HtmlBlockCloseNoscript;*/
        {

           var result= TreeNT((int)EHtml.HtmlBlockNoscript,()=>
                And(()=>  
                     HtmlBlockOpenNoscript()
                  && OptRepeat(()=>    
                            
                               HtmlBlockNoscript()
                            || And(()=>        
                                       Not(()=> HtmlBlockCloseNoscript() )
                                    && Any() ) )
                  && HtmlBlockCloseNoscript() ) ); return result;
		}
        public bool HtmlBlockOpenOl()    /*HtmlBlockOpenOl : '<' Spnl ('ol' / 'OL') Spnl HtmlAttribute* '>';*/
        {

           var result=And(()=>  
                     Char('<')
                  && Spnl()
                  && (    Char('o','l') || Char('O','L'))
                  && Spnl()
                  && OptRepeat(()=> HtmlAttribute() )
                  && Char('>') ); return result;
		}
        public bool HtmlBlockCloseOl()    /*HtmlBlockCloseOl : '<' Spnl '/' ('ol' / 'OL') Spnl '>';*/
        {

           var result=And(()=>  
                     Char('<')
                  && Spnl()
                  && Char('/')
                  && (    Char('o','l') || Char('O','L'))
                  && Spnl()
                  && Char('>') ); return result;
		}
        public bool HtmlBlockOl()    /*^^HtmlBlockOl : HtmlBlockOpenOl (&HtmlBlockCloseOl / HtmlBlock+) HtmlBlockCloseOl;*/
        {

           var result= TreeNT((int)EHtml.HtmlBlockOl,()=>
                And(()=>  
                     HtmlBlockOpenOl()
                  && (    
                         Peek(()=> HtmlBlockCloseOl() )
                      || PlusRepeat(()=> HtmlBlock() ))
                  && HtmlBlockCloseOl() ) ); return result;
		}
        public bool HtmlBlockOpenP()    /*HtmlBlockOpenP : '<' Spnl ('p' / 'P') Spnl HtmlAttribute* '>';*/
        {

           var result=And(()=>  
                     Char('<')
                  && Spnl()
                  && (    Char('p') || Char('P'))
                  && Spnl()
                  && OptRepeat(()=> HtmlAttribute() )
                  && Char('>') ); return result;
		}
        public bool HtmlBlockCloseP()    /*HtmlBlockCloseP : '<' Spnl '/' ('p' / 'P') Spnl '>';*/
        {

           var result=And(()=>  
                     Char('<')
                  && Spnl()
                  && Char('/')
                  && (    Char('p') || Char('P'))
                  && Spnl()
                  && Char('>') ); return result;
		}
        public bool HtmlBlockP()    /*^^HtmlBlockP : HtmlBlockOpenP (&HtmlBlockCloseP / HtmlBlock +) HtmlBlockCloseP;*/
        {

           var result= TreeNT((int)EHtml.HtmlBlockP,()=>
                And(()=>  
                     HtmlBlockOpenP()
                  && (    
                         Peek(()=> HtmlBlockCloseP() )
                      || PlusRepeat(()=> HtmlBlock() ))
                  && HtmlBlockCloseP() ) ); return result;
		}
        public bool HtmlBlockOpenPre()    /*HtmlBlockOpenPre : '<' Spnl ('pre' / 'PRE') Spnl HtmlAttribute* '>';*/
        {

           var result=And(()=>  
                     Char('<')
                  && Spnl()
                  && (    Char('p','r','e') || Char('P','R','E'))
                  && Spnl()
                  && OptRepeat(()=> HtmlAttribute() )
                  && Char('>') ); return result;
		}
        public bool HtmlBlockClosePre()    /*HtmlBlockClosePre : '<' Spnl '/' ('pre' / 'PRE') Spnl '>';*/
        {

           var result=And(()=>  
                     Char('<')
                  && Spnl()
                  && Char('/')
                  && (    Char('p','r','e') || Char('P','R','E'))
                  && Spnl()
                  && Char('>') ); return result;
		}
        public bool HtmlBlockPre()    /*^^HtmlBlockPre : HtmlBlockOpenPre (HtmlBlockCode/(HtmlBlockPre / !HtmlBlockClosePre .)*) HtmlBlockClosePre;*/
        {

           var result= TreeNT((int)EHtml.HtmlBlockPre,()=>
                And(()=>  
                     HtmlBlockOpenPre()
                  && (    
                         HtmlBlockCode()
                      || OptRepeat(()=>      
                                    
                                       HtmlBlockPre()
                                    || And(()=>          
                                                 Not(()=> HtmlBlockClosePre() )
                                              && Any() ) ))
                  && HtmlBlockClosePre() ) ); return result;
		}
        public bool HtmlBlockOpenTable()    /*HtmlBlockOpenTable : '<' Spnl ('table' / 'TABLE') Spnl HtmlAttribute* '>';*/
        {

           var result=And(()=>  
                     Char('<')
                  && Spnl()
                  && (    
                         Char('t','a','b','l','e')
                      || Char('T','A','B','L','E'))
                  && Spnl()
                  && OptRepeat(()=> HtmlAttribute() )
                  && Char('>') ); return result;
		}
        public bool HtmlBlockCloseTable()    /*HtmlBlockCloseTable : '<' Spnl '/' ('table' / 'TABLE') Spnl '>';*/
        {

           var result=And(()=>  
                     Char('<')
                  && Spnl()
                  && Char('/')
                  && (    
                         Char('t','a','b','l','e')
                      || Char('T','A','B','L','E'))
                  && Spnl()
                  && Char('>') ); return result;
		}
        public bool HtmlBlockTable()    /*^^HtmlBlockTable : HtmlBlockOpenTable (&HtmlBlockCloseTable / HtmlBlock+) HtmlBlockCloseTable;*/
        {

           var result= TreeNT((int)EHtml.HtmlBlockTable,()=>
                And(()=>  
                     HtmlBlockOpenTable()
                  && (    
                         Peek(()=> HtmlBlockCloseTable() )
                      || PlusRepeat(()=> HtmlBlock() ))
                  && HtmlBlockCloseTable() ) ); return result;
		}
        public bool HtmlBlockOpenUl()    /*HtmlBlockOpenUl : '<' Spnl ('ul' / 'UL') Spnl HtmlAttribute* '>';*/
        {

           var result=And(()=>  
                     Char('<')
                  && Spnl()
                  && (    Char('u','l') || Char('U','L'))
                  && Spnl()
                  && OptRepeat(()=> HtmlAttribute() )
                  && Char('>') ); return result;
		}
        public bool HtmlBlockCloseUl()    /*HtmlBlockCloseUl : '<' Spnl '/' ('ul' / 'UL') Spnl '>';*/
        {

           var result=And(()=>  
                     Char('<')
                  && Spnl()
                  && Char('/')
                  && (    Char('u','l') || Char('U','L'))
                  && Spnl()
                  && Char('>') ); return result;
		}
        public bool HtmlBlockUl()    /*^^HtmlBlockUl : HtmlBlockOpenUl (&HtmlBlockCloseUl /  HtmlBlock+ ) HtmlBlockCloseUl;*/
        {

           var result= TreeNT((int)EHtml.HtmlBlockUl,()=>
                And(()=>  
                     HtmlBlockOpenUl()
                  && (    
                         Peek(()=> HtmlBlockCloseUl() )
                      || PlusRepeat(()=> HtmlBlock() ))
                  && HtmlBlockCloseUl() ) ); return result;
		}
        public bool HtmlBlockOpenDd()    /*HtmlBlockOpenDd : '<' Spnl ('dd' / 'DD') Spnl HtmlAttribute* '>';*/
        {

           var result=And(()=>  
                     Char('<')
                  && Spnl()
                  && (    Char('d','d') || Char('D','D'))
                  && Spnl()
                  && OptRepeat(()=> HtmlAttribute() )
                  && Char('>') ); return result;
		}
        public bool HtmlBlockCloseDd()    /*HtmlBlockCloseDd : '<' Spnl '/' ('dd' / 'DD') Spnl '>';*/
        {

           var result=And(()=>  
                     Char('<')
                  && Spnl()
                  && Char('/')
                  && (    Char('d','d') || Char('D','D'))
                  && Spnl()
                  && Char('>') ); return result;
		}
        public bool HtmlBlockDd()    /*^^HtmlBlockDd : HtmlBlockOpenDd (&HtmlBlockCloseDd / HtmlBlock+) HtmlBlockCloseDd;*/
        {

           var result= TreeNT((int)EHtml.HtmlBlockDd,()=>
                And(()=>  
                     HtmlBlockOpenDd()
                  && (    
                         Peek(()=> HtmlBlockCloseDd() )
                      || PlusRepeat(()=> HtmlBlock() ))
                  && HtmlBlockCloseDd() ) ); return result;
		}
        public bool HtmlBlockOpenDt()    /*HtmlBlockOpenDt : '<' Spnl ('dt' / 'DT') Spnl HtmlAttribute* '>';*/
        {

           var result=And(()=>  
                     Char('<')
                  && Spnl()
                  && (    Char('d','t') || Char('D','T'))
                  && Spnl()
                  && OptRepeat(()=> HtmlAttribute() )
                  && Char('>') ); return result;
		}
        public bool HtmlBlockCloseDt()    /*HtmlBlockCloseDt : '<' Spnl '/' ('dt' / 'DT') Spnl '>';*/
        {

           var result=And(()=>  
                     Char('<')
                  && Spnl()
                  && Char('/')
                  && (    Char('d','t') || Char('D','T'))
                  && Spnl()
                  && Char('>') ); return result;
		}
        public bool HtmlBlockDt()    /*^^HtmlBlockDt : HtmlBlockOpenDt (&HtmlBlockCloseDt / HtmlBlock+) HtmlBlockCloseDt;*/
        {

           var result= TreeNT((int)EHtml.HtmlBlockDt,()=>
                And(()=>  
                     HtmlBlockOpenDt()
                  && (    
                         Peek(()=> HtmlBlockCloseDt() )
                      || PlusRepeat(()=> HtmlBlock() ))
                  && HtmlBlockCloseDt() ) ); return result;
		}
        public bool HtmlBlockOpenFrameset()    /*HtmlBlockOpenFrameset : '<' Spnl ('frameset' / 'FRAMESET') Spnl HtmlAttribute* '>';*/
        {

           var result=And(()=>  
                     Char('<')
                  && Spnl()
                  && (    Char("frameset") || Char("FRAMESET"))
                  && Spnl()
                  && OptRepeat(()=> HtmlAttribute() )
                  && Char('>') ); return result;
		}
        public bool HtmlBlockCloseFrameset()    /*HtmlBlockCloseFrameset : '<' Spnl '/' ('frameset' / 'FRAMESET') Spnl '>';*/
        {

           var result=And(()=>  
                     Char('<')
                  && Spnl()
                  && Char('/')
                  && (    Char("frameset") || Char("FRAMESET"))
                  && Spnl()
                  && Char('>') ); return result;
		}
        public bool HtmlBlockFrameset()    /*^^HtmlBlockFrameset : HtmlBlockOpenFrameset (&HtmlBlockCloseFrameset / HtmlBlock+ ) HtmlBlockCloseFrameset;*/
        {

           var result= TreeNT((int)EHtml.HtmlBlockFrameset,()=>
                And(()=>  
                     HtmlBlockOpenFrameset()
                  && (    
                         Peek(()=> HtmlBlockCloseFrameset() )
                      || PlusRepeat(()=> HtmlBlock() ))
                  && HtmlBlockCloseFrameset() ) ); return result;
		}
        public bool HtmlBlockOpenLi()    /*HtmlBlockOpenLi : '<' Spnl ('li' / 'LI') Spnl HtmlAttribute* '>';*/
        {

           var result=And(()=>  
                     Char('<')
                  && Spnl()
                  && (    Char('l','i') || Char('L','I'))
                  && Spnl()
                  && OptRepeat(()=> HtmlAttribute() )
                  && Char('>') ); return result;
		}
        public bool HtmlBlockCloseLi()    /*HtmlBlockCloseLi : '<' Spnl '/' ('li' / 'LI') Spnl '>';*/
        {

           var result=And(()=>  
                     Char('<')
                  && Spnl()
                  && Char('/')
                  && (    Char('l','i') || Char('L','I'))
                  && Spnl()
                  && Char('>') ); return result;
		}
        public bool HtmlBlockLi()    /*^^HtmlBlockLi : HtmlBlockOpenLi (&HtmlBlockCloseLi/HtmlBlock+  ) HtmlBlockCloseLi;*/
        {

           var result= TreeNT((int)EHtml.HtmlBlockLi,()=>
                And(()=>  
                     HtmlBlockOpenLi()
                  && (    
                         Peek(()=> HtmlBlockCloseLi() )
                      || PlusRepeat(()=> HtmlBlock() ))
                  && HtmlBlockCloseLi() ) ); return result;
		}
        public bool HtmlBlockOpenTbody()    /*HtmlBlockOpenTbody : '<' Spnl ('tbody' / 'TBODY') Spnl HtmlAttribute* '>';*/
        {

           var result=And(()=>  
                     Char('<')
                  && Spnl()
                  && (    
                         Char('t','b','o','d','y')
                      || Char('T','B','O','D','Y'))
                  && Spnl()
                  && OptRepeat(()=> HtmlAttribute() )
                  && Char('>') ); return result;
		}
        public bool HtmlBlockCloseTbody()    /*HtmlBlockCloseTbody : '<' Spnl '/' ('tbody' / 'TBODY') Spnl '>';*/
        {

           var result=And(()=>  
                     Char('<')
                  && Spnl()
                  && Char('/')
                  && (    
                         Char('t','b','o','d','y')
                      || Char('T','B','O','D','Y'))
                  && Spnl()
                  && Char('>') ); return result;
		}
        public bool HtmlBlockTbody()    /*^^HtmlBlockTbody : HtmlBlockOpenTbody (&HtmlBlockCloseTbody / HtmlBlock+ ) HtmlBlockCloseTbody;*/
        {

           var result= TreeNT((int)EHtml.HtmlBlockTbody,()=>
                And(()=>  
                     HtmlBlockOpenTbody()
                  && (    
                         Peek(()=> HtmlBlockCloseTbody() )
                      || PlusRepeat(()=> HtmlBlock() ))
                  && HtmlBlockCloseTbody() ) ); return result;
		}
        public bool HtmlBlockOpenTd()    /*HtmlBlockOpenTd : '<' Spnl ('td' / 'TD') Spnl HtmlAttribute* '>';*/
        {

           var result=And(()=>  
                     Char('<')
                  && Spnl()
                  && (    Char('t','d') || Char('T','D'))
                  && Spnl()
                  && OptRepeat(()=> HtmlAttribute() )
                  && Char('>') ); return result;
		}
        public bool HtmlBlockCloseTd()    /*HtmlBlockCloseTd : '<' Spnl '/' ('td' / 'TD') Spnl '>';*/
        {

           var result=And(()=>  
                     Char('<')
                  && Spnl()
                  && Char('/')
                  && (    Char('t','d') || Char('T','D'))
                  && Spnl()
                  && Char('>') ); return result;
		}
        public bool HtmlBlockTd()    /*^^HtmlBlockTd : HtmlBlockOpenTd (&HtmlBlockCloseTd / HtmlBlock+) HtmlBlockCloseTd;*/
        {

           var result= TreeNT((int)EHtml.HtmlBlockTd,()=>
                And(()=>  
                     HtmlBlockOpenTd()
                  && (    
                         Peek(()=> HtmlBlockCloseTd() )
                      || PlusRepeat(()=> HtmlBlock() ))
                  && HtmlBlockCloseTd() ) ); return result;
		}
        public bool HtmlBlockOpenTfoot()    /*HtmlBlockOpenTfoot : '<' Spnl ('tfoot' / 'TFOOT') Spnl HtmlAttribute* '>';*/
        {

           var result=And(()=>  
                     Char('<')
                  && Spnl()
                  && (    
                         Char('t','f','o','o','t')
                      || Char('T','F','O','O','T'))
                  && Spnl()
                  && OptRepeat(()=> HtmlAttribute() )
                  && Char('>') ); return result;
		}
        public bool HtmlBlockCloseTfoot()    /*HtmlBlockCloseTfoot : '<' Spnl '/' ('tfoot' / 'TFOOT') Spnl '>';*/
        {

           var result=And(()=>  
                     Char('<')
                  && Spnl()
                  && Char('/')
                  && (    
                         Char('t','f','o','o','t')
                      || Char('T','F','O','O','T'))
                  && Spnl()
                  && Char('>') ); return result;
		}
        public bool HtmlBlockTfoot()    /*^^HtmlBlockTfoot : HtmlBlockOpenTfoot (&HtmlBlockCloseTfoot / HtmlBlock+) HtmlBlockCloseTfoot;*/
        {

           var result= TreeNT((int)EHtml.HtmlBlockTfoot,()=>
                And(()=>  
                     HtmlBlockOpenTfoot()
                  && (    
                         Peek(()=> HtmlBlockCloseTfoot() )
                      || PlusRepeat(()=> HtmlBlock() ))
                  && HtmlBlockCloseTfoot() ) ); return result;
		}
        public bool HtmlBlockOpenTh()    /*HtmlBlockOpenTh : '<' Spnl ('th' / 'TH') Spnl HtmlAttribute* '>';*/
        {

           var result=And(()=>  
                     Char('<')
                  && Spnl()
                  && (    Char('t','h') || Char('T','H'))
                  && Spnl()
                  && OptRepeat(()=> HtmlAttribute() )
                  && Char('>') ); return result;
		}
        public bool HtmlBlockCloseTh()    /*HtmlBlockCloseTh : '<' Spnl '/' ('th' / 'TH') Spnl '>';*/
        {

           var result=And(()=>  
                     Char('<')
                  && Spnl()
                  && Char('/')
                  && (    Char('t','h') || Char('T','H'))
                  && Spnl()
                  && Char('>') ); return result;
		}
        public bool HtmlBlockTh()    /*^^HtmlBlockTh : HtmlBlockOpenTh (&HtmlBlockCloseTh / HtmlBlock+) HtmlBlockCloseTh;*/
        {

           var result= TreeNT((int)EHtml.HtmlBlockTh,()=>
                And(()=>  
                     HtmlBlockOpenTh()
                  && (    
                         Peek(()=> HtmlBlockCloseTh() )
                      || PlusRepeat(()=> HtmlBlock() ))
                  && HtmlBlockCloseTh() ) ); return result;
		}
        public bool HtmlBlockOpenThead()    /*HtmlBlockOpenThead : '<' Spnl ('thead' / 'THEAD') Spnl HtmlAttribute* '>';*/
        {

           var result=And(()=>  
                     Char('<')
                  && Spnl()
                  && (    
                         Char('t','h','e','a','d')
                      || Char('T','H','E','A','D'))
                  && Spnl()
                  && OptRepeat(()=> HtmlAttribute() )
                  && Char('>') ); return result;
		}
        public bool HtmlBlockCloseThead()    /*HtmlBlockCloseThead : '<' Spnl '/' ('thead' / 'THEAD') Spnl '>';*/
        {

           var result=And(()=>  
                     Char('<')
                  && Spnl()
                  && Char('/')
                  && (    
                         Char('t','h','e','a','d')
                      || Char('T','H','E','A','D'))
                  && Spnl()
                  && Char('>') ); return result;
		}
        public bool HtmlBlockThead()    /*^^HtmlBlockThead : HtmlBlockOpenThead (&HtmlBlockCloseThead / HtmlBlockTr+ ) HtmlBlockCloseThead;*/
        {

           var result= TreeNT((int)EHtml.HtmlBlockThead,()=>
                And(()=>  
                     HtmlBlockOpenThead()
                  && (    
                         Peek(()=> HtmlBlockCloseThead() )
                      || PlusRepeat(()=> HtmlBlockTr() ))
                  && HtmlBlockCloseThead() ) ); return result;
		}
        public bool HtmlBlockOpenTr()    /*HtmlBlockOpenTr : '<' Spnl ('tr' / 'TR') Spnl HtmlAttribute* '>';*/
        {

           var result=And(()=>  
                     Char('<')
                  && Spnl()
                  && (    Char('t','r') || Char('T','R'))
                  && Spnl()
                  && OptRepeat(()=> HtmlAttribute() )
                  && Char('>') ); return result;
		}
        public bool HtmlBlockCloseTr()    /*HtmlBlockCloseTr : '<' Spnl '/' ('tr' / 'TR') Spnl '>';*/
        {

           var result=And(()=>  
                     Char('<')
                  && Spnl()
                  && Char('/')
                  && (    Char('t','r') || Char('T','R'))
                  && Spnl()
                  && Char('>') ); return result;
		}
        public bool HtmlBlockTr()    /*^^HtmlBlockTr : HtmlBlockOpenTr (&HtmlBlockCloseTr / HtmlBlock+) HtmlBlockCloseTr;*/
        {

           var result= TreeNT((int)EHtml.HtmlBlockTr,()=>
                And(()=>  
                     HtmlBlockOpenTr()
                  && (    
                         Peek(()=> HtmlBlockCloseTr() )
                      || PlusRepeat(()=> HtmlBlock() ))
                  && HtmlBlockCloseTr() ) ); return result;
		}
        public bool HtmlBlockOpenScript()    /*HtmlBlockOpenScript : '<' Spnl ('script' / 'SCRIPT') Spnl HtmlAttribute* '>';*/
        {

           var result=And(()=>  
                     Char('<')
                  && Spnl()
                  && (    
                         Char('s','c','r','i','p','t')
                      || Char('S','C','R','I','P','T'))
                  && Spnl()
                  && OptRepeat(()=> HtmlAttribute() )
                  && Char('>') ); return result;
		}
        public bool HtmlBlockCloseScript()    /*HtmlBlockCloseScript : '<' Spnl '/' ('script' / 'SCRIPT') Spnl '>';*/
        {

           var result=And(()=>  
                     Char('<')
                  && Spnl()
                  && Char('/')
                  && (    
                         Char('s','c','r','i','p','t')
                      || Char('S','C','R','I','P','T'))
                  && Spnl()
                  && Char('>') ); return result;
		}
        public bool HtmlBlockScript()    /*^^HtmlBlockScript : HtmlBlockOpenScript (!HtmlBlockCloseScript .)* HtmlBlockCloseScript;*/
        {

           var result= TreeNT((int)EHtml.HtmlBlockScript,()=>
                And(()=>  
                     HtmlBlockOpenScript()
                  && OptRepeat(()=>    
                      And(()=>      
                               Not(()=> HtmlBlockCloseScript() )
                            && Any() ) )
                  && HtmlBlockCloseScript() ) ); return result;
		}
        public bool HtmlBlockOpenHead()    /*HtmlBlockOpenHead : '<' Spnl ('head' / 'HEAD') Spnl HtmlAttribute* '>';*/
        {

           var result=And(()=>  
                     Char('<')
                  && Spnl()
                  && (    Char('h','e','a','d') || Char('H','E','A','D'))
                  && Spnl()
                  && OptRepeat(()=> HtmlAttribute() )
                  && Char('>') ); return result;
		}
        public bool HtmlBlockCloseHead()    /*HtmlBlockCloseHead : '<' Spnl '/' ('head' / 'HEAD') Spnl '>';*/
        {

           var result=And(()=>  
                     Char('<')
                  && Spnl()
                  && Char('/')
                  && (    Char('h','e','a','d') || Char('H','E','A','D'))
                  && Spnl()
                  && Char('>') ); return result;
		}
        public bool HtmlBlockHead()    /*^^HtmlBlockHead : HtmlBlockOpenHead (!HtmlBlockCloseHead .)* HtmlBlockCloseHead ;*/
        {

           var result= TreeNT((int)EHtml.HtmlBlockHead,()=>
                And(()=>  
                     HtmlBlockOpenHead()
                  && OptRepeat(()=>    
                      And(()=>    Not(()=> HtmlBlockCloseHead() ) && Any() ) )
                  && HtmlBlockCloseHead() ) ); return result;
		}
        public bool HtmlBlockOpenA()    /*HtmlBlockOpenA : '<' Spnl ('a' / 'A') Spnl HtmlAttribute* '>';*/
        {

           var result=And(()=>  
                     Char('<')
                  && Spnl()
                  && (    Char('a') || Char('A'))
                  && Spnl()
                  && OptRepeat(()=> HtmlAttribute() )
                  && Char('>') ); return result;
		}
        public bool HtmlBlockCloseA()    /*HtmlBlockCloseA : '<' Spnl '/' ('a' / 'A') Spnl '>';*/
        {

           var result=And(()=>  
                     Char('<')
                  && Spnl()
                  && Char('/')
                  && (    Char('a') || Char('A'))
                  && Spnl()
                  && Char('>') ); return result;
		}
        public bool HtmlBlockA()    /*^^HtmlBlockA : HtmlBlockOpenA (&HtmlBlockCloseA / HtmlBlock+) HtmlBlockCloseA ;*/
        {

           var result= TreeNT((int)EHtml.HtmlBlockA,()=>
                And(()=>  
                     HtmlBlockOpenA()
                  && (    
                         Peek(()=> HtmlBlockCloseA() )
                      || PlusRepeat(()=> HtmlBlock() ))
                  && HtmlBlockCloseA() ) ); return result;
		}
        public bool HtmlBlockOpenCode()    /*HtmlBlockOpenCode : '<' Spnl ('code' / 'CODE') Spnl  '>';*/
        {

           var result=And(()=>  
                     Char('<')
                  && Spnl()
                  && (    Char('c','o','d','e') || Char('C','O','D','E'))
                  && Spnl()
                  && Char('>') ); return result;
		}
        public bool HtmlBlockCloseCode()    /*HtmlBlockCloseCode  : '<' Spnl '/' ('code' / 'CODE') Spnl '>';*/
        {

           var result=And(()=>  
                     Char('<')
                  && Spnl()
                  && Char('/')
                  && (    Char('c','o','d','e') || Char('C','O','D','E'))
                  && Spnl()
                  && Char('>') ); return result;
		}
        public bool HtmlBlockCode()    /*^^HtmlBlockCode : HtmlBlockOpenCode ( !HtmlBlockCloseCode .)* HtmlBlockCloseCode ;*/
        {

           var result= TreeNT((int)EHtml.HtmlBlockCode,()=>
                And(()=>  
                     HtmlBlockOpenCode()
                  && OptRepeat(()=>    
                      And(()=>    Not(()=> HtmlBlockCloseCode() ) && Any() ) )
                  && HtmlBlockCloseCode() ) ); return result;
		}
        public bool HtmlBlockOpenSpan()    /*HtmlBlockOpenSpan : '<' Spnl ('span' / 'SPAN') Spnl  '>';*/
        {

           var result=And(()=>  
                     Char('<')
                  && Spnl()
                  && (    Char('s','p','a','n') || Char('S','P','A','N'))
                  && Spnl()
                  && Char('>') ); return result;
		}
        public bool HtmlBlockCloseSpan()    /*HtmlBlockCloseSpan  : '<' Spnl '/' ('span' / 'SPAN') Spnl '>';*/
        {

           var result=And(()=>  
                     Char('<')
                  && Spnl()
                  && Char('/')
                  && (    Char('s','p','a','n') || Char('S','P','A','N'))
                  && Spnl()
                  && Char('>') ); return result;
		}
        public bool HtmlBlockSpan()    /*^^HtmlBlockSpan : HtmlBlockOpenSpan ( &HtmlBlockCloseSpan /HtmlBlock+) HtmlBlockCloseSpan ;*/
        {

           var result= TreeNT((int)EHtml.HtmlBlockSpan,()=>
                And(()=>  
                     HtmlBlockOpenSpan()
                  && (    
                         Peek(()=> HtmlBlockCloseSpan() )
                      || PlusRepeat(()=> HtmlBlock() ))
                  && HtmlBlockCloseSpan() ) ); return result;
		}
        public bool HtmlBlockOpenUnknown()    /*HtmlBlockOpenUnknown : '<' Spnl ![>/]  UnknownTagName Spnl  HtmlAttribute* '>';*/
        {

           var result=And(()=>  
                     Char('<')
                  && Spnl()
                  && Not(()=> OneOf(">/") )
                  && UnknownTagName()
                  && Spnl()
                  && OptRepeat(()=> HtmlAttribute() )
                  && Char('>') ); return result;
		}
        public bool HtmlBlockCloseUnknown()    /*HtmlBlockCloseUnknown : '<' Spnl '/'  !'>'  UnknownTagName  Spnl '>';*/
        {

           var result=And(()=>  
                     Char('<')
                  && Spnl()
                  && Char('/')
                  && Not(()=> Char('>') )
                  && UnknownTagName()
                  && Spnl()
                  && Char('>') ); return result;
		}
        public bool HtmlBlockUnknown()    /*^^HtmlBlockUnknown : HtmlBlockOpenUnknown ( &HtmlBlockCloseUnknown /HtmlBlock+) HtmlBlockCloseUnknown ;*/
        {

           var result= TreeNT((int)EHtml.HtmlBlockUnknown,()=>
                And(()=>  
                     HtmlBlockOpenUnknown()
                  && (    
                         Peek(()=> HtmlBlockCloseUnknown() )
                      || PlusRepeat(()=> HtmlBlock() ))
                  && HtmlBlockCloseUnknown() ) ); return result;
		}
        public bool UnknownTagName()    /*^^UnknownTagName:(LiteralChar)*;*/
        {

           var result= TreeNT((int)EHtml.UnknownTagName,()=>
                OptRepeat(()=> LiteralChar() ) ); return result;
		}
        public bool HtmlBlockInTags()    /*^^HtmlBlockInTags : HtmlBlockAddress
                / HtmlBlockBlockquote
                / HtmlBlockCenter
                / HtmlBlockDir
                / HtmlBlockDiv
                / HtmlBlockDl
                / HtmlBlockFieldset
                / HtmlBlockForm
                / HtmlBlockH1
                / HtmlBlockH2
                / HtmlBlockH3
                / HtmlBlockH4
                / HtmlBlockH5
                / HtmlBlockH6
                / HtmlBlockMenu
                / HtmlBlockNoframes
                / HtmlBlockNoscript
                / HtmlBlockOl
                / HtmlBlockP
                / HtmlBlockPre
                / HtmlBlockTable
                / HtmlBlockUl
                / HtmlBlockDd
                / HtmlBlockDt
                / HtmlBlockFrameset
                / HtmlBlockLi
                / HtmlBlockTbody
                / HtmlBlockTd
                / HtmlBlockTfoot
                / HtmlBlockTh
                / HtmlBlockThead
                / HtmlBlockTr
                / HtmlBlockScript
                / HtmlBlockHead 
				/ HtmlBlockA
				/ HtmlBlockCode
				/ HtmlBlockSpan
		        / HtmlBlockUnknown
				;*/
        {

           var result= TreeNT((int)EHtml.HtmlBlockInTags,()=>
                  
                     HtmlBlockAddress()
                  || HtmlBlockBlockquote()
                  || HtmlBlockCenter()
                  || HtmlBlockDir()
                  || HtmlBlockDiv()
                  || HtmlBlockDl()
                  || HtmlBlockFieldset()
                  || HtmlBlockForm()
                  || HtmlBlockH1()
                  || HtmlBlockH2()
                  || HtmlBlockH3()
                  || HtmlBlockH4()
                  || HtmlBlockH5()
                  || HtmlBlockH6()
                  || HtmlBlockMenu()
                  || HtmlBlockNoframes()
                  || HtmlBlockNoscript()
                  || HtmlBlockOl()
                  || HtmlBlockP()
                  || HtmlBlockPre()
                  || HtmlBlockTable()
                  || HtmlBlockUl()
                  || HtmlBlockDd()
                  || HtmlBlockDt()
                  || HtmlBlockFrameset()
                  || HtmlBlockLi()
                  || HtmlBlockTbody()
                  || HtmlBlockTd()
                  || HtmlBlockTfoot()
                  || HtmlBlockTh()
                  || HtmlBlockThead()
                  || HtmlBlockTr()
                  || HtmlBlockScript()
                  || HtmlBlockHead()
                  || HtmlBlockA()
                  || HtmlBlockCode()
                  || HtmlBlockSpan()
                  || HtmlBlockUnknown() ); return result;
		}
        public bool HtmlBlock()    /*^^HtmlBlock :(Spnl (HtmlBlockInTags / HtmlComment / HtmlBlockSelfClosing )Spnl)/InnerPlain ;*/
        {

           var result= TreeNT((int)EHtml.HtmlBlock,()=>
                  
                     And(()=>    
                         Spnl()
                      && (      
                               HtmlBlockInTags()
                            || HtmlComment()
                            || HtmlBlockSelfClosing())
                      && Spnl() )
                  || InnerPlain() ); return result;
		}
        public bool HtmlBlockSelfClosing()    /*^^HtmlBlockSelfClosing : '<' Spnl HtmlBlockType Spnl HtmlAttribute* '/' Spnl '>';*/
        {

           var result= TreeNT((int)EHtml.HtmlBlockSelfClosing,()=>
                And(()=>  
                     Char('<')
                  && Spnl()
                  && HtmlBlockType()
                  && Spnl()
                  && OptRepeat(()=> HtmlAttribute() )
                  && Char('/')
                  && Spnl()
                  && Char('>') ) ); return result;
		}
        public bool HtmlBlockType()    /*HtmlBlockType : 'address' / 'blockquote' / 'center' / 'dir' / 'div' / 'dl' / 'fieldset' / 'form' / 'h1' / 'h2' / 'h3' /
                'h4' / 'h5' / 'h6' / 'hr' / 'isindex' / 'menu' / 'noframes' / 'noscript' / 'ol' / 'p' / 'pre' / 'table' /
                'ul' / 'dd' / 'dt' / 'frameset' / 'li' / 'tbody' / 'td' / 'tfoot' / 'th' / 'thead' / 'tr' / 'script' /
                'ADDRESS' / 'BLOCKQUOTE' / 'CENTER' / 'DIR' / 'DIV' / 'DL' / 'FIELDSET' / 'FORM' / 'H1' / 'H2' / 'H3' /
                'H4' / 'H5' / 'H6' / 'HR' / 'ISINDEX' / 'MENU' / 'NOFRAMES' / 'NOSCRIPT' / 'OL' / 'P' / 'PRE' / 'TABLE' /
                'UL' / 'DD' / 'DT' / 'FRAMESET' / 'LI' / 'TBODY' / 'TD' / 'TFOOT' / 'TH' / 'THEAD' / 'TR' / 'SCRIPT'/'br'/'BR'/
				'a'/'A';*/
        {

           var result=OneOfLiterals(optimizedLiterals0); return result;
		}
        public bool StyleOpen()    /*StyleOpen :     '<' Spnl ('style' / 'STYLE') Spnl HtmlAttribute* '>'  ;*/
        {

           var result=And(()=>  
                     Char('<')
                  && Spnl()
                  && (    
                         Char('s','t','y','l','e')
                      || Char('S','T','Y','L','E'))
                  && Spnl()
                  && OptRepeat(()=> HtmlAttribute() )
                  && Char('>') ); return result;
		}
        public bool StyleClose()    /*StyleClose :    '<' Spnl '/' ('style' / 'STYLE') Spnl '>' ;*/
        {

           var result=And(()=>  
                     Char('<')
                  && Spnl()
                  && Char('/')
                  && (    
                         Char('s','t','y','l','e')
                      || Char('S','T','Y','L','E'))
                  && Spnl()
                  && Char('>') ); return result;
		}
        public bool InStyleTags()    /*InStyleTags :   StyleOpen (!StyleClose .)* StyleClose ;*/
        {

           var result=And(()=>  
                     StyleOpen()
                  && OptRepeat(()=>    
                      And(()=>    Not(()=> StyleClose() ) && Any() ) )
                  && StyleClose() ); return result;
		}
        public bool StyleBlock()    /*^^StyleBlock :    InStyleTags 
                BlankLine*  ;*/
        {

           var result= TreeNT((int)EHtml.StyleBlock,()=>
                And(()=>  
                     InStyleTags()
                  && OptRepeat(()=> BlankLine() ) ) ); return result;
		}
        public bool Space()    /*Space : Spacechar+ ;*/
        {

           var result=PlusRepeat(()=> Spacechar() ); return result;
		}
        public bool RawHtml()    /*RawHtml :    (HtmlComment / HtmlBlockScript / HtmlTag) ;*/
        {

           var result=    HtmlComment() || HtmlBlockScript() || HtmlTag(); return result;
		}
        public bool BlankLine()    /*BlankLine :     Sp Newline;*/
        {

           var result=And(()=>    Sp() && Newline() ); return result;
		}
        public bool Quoted()    /*^^Quoted :         '\'' (!'\'' .)* '\''  /    '\"' (!'\"' .)* '\"';*/
        {

           var result= TreeNT((int)EHtml.Quoted,()=>
                  
                     And(()=>    
                         Char('\'')
                      && OptRepeat(()=>      
                            And(()=>    Not(()=> Char('\'') ) && Any() ) )
                      && Char('\'') )
                  || And(()=>    
                         Char('\"')
                      && OptRepeat(()=>      
                            And(()=>    Not(()=> Char('\"') ) && Any() ) )
                      && Char('\"') ) ); return result;
		}
        public bool HtmlAttribute()    /*^^HtmlAttribute : (AlphanumericAscii / '-')+ Spnl ('=' Spnl (Quoted / (!'>' Nonspacechar)+)) Spnl ;*/
        {

           var result= TreeNT((int)EHtml.HtmlAttribute,()=>
                And(()=>  
                     PlusRepeat(()=>     AlphanumericAscii() || Char('-') )
                  && Spnl()
                  && And(()=>    
                         Char('=')
                      && Spnl()
                      && (      
                               Quoted()
                            || PlusRepeat(()=>        
                                    And(()=>          
                                                 Not(()=> Char('>') )
                                              && Nonspacechar() ) )) )
                  && Spnl() ) ); return result;
		}
        public bool HtmlComment()    /*^^HtmlComment :   '<!--' (!'-->' .)* '-->';*/
        {

           var result= TreeNT((int)EHtml.HtmlComment,()=>
                And(()=>  
                     Char('<','!','-','-')
                  && OptRepeat(()=>    
                      And(()=>    Not(()=> Char('-','-','>') ) && Any() ) )
                  && Char('-','-','>') ) ); return result;
		}
        public bool HtmlTag()    /*HtmlTag :       '<' Spnl '/'? AlphanumericAscii+ Spnl HtmlAttribute* '/'? Spnl '>';*/
        {

           var result=And(()=>  
                     Char('<')
                  && Spnl()
                  && Option(()=> Char('/') )
                  && PlusRepeat(()=> AlphanumericAscii() )
                  && Spnl()
                  && OptRepeat(()=> HtmlAttribute() )
                  && Option(()=> Char('/') )
                  && Spnl()
                  && Char('>') ); return result;
		}
        public bool Spacechar()    /*Spacechar :     ' ' / '\t';*/
        {

           var result=    Char(' ') || Char('\t'); return result;
		}
        public bool Nonspacechar()    /*Nonspacechar :  !Spacechar !Newline .;*/
        {

           var result=And(()=>  
                     Not(()=> Spacechar() )
                  && Not(()=> Newline() )
                  && Any() ); return result;
		}
        public bool Newline()    /*Newline :       '\n' / '\r' '\n'?;*/
        {

           var result=  
                     Char('\n')
                  || And(()=>    Char('\r') && Option(()=> Char('\n') ) ); return result;
		}
        public bool Sp()    /*Sp :            Spacechar*;*/
        {

           var result=OptRepeat(()=> Spacechar() ); return result;
		}
        public bool Spnl()    /*Spnl :          (Newline / Spacechar)*;*/
        {

           var result=OptRepeat(()=>     Newline() || Spacechar() ); return result;
		}
        public bool AlphanumericAscii()    /*AlphanumericAscii : [A-Za-z0-9] ;*/
        {

           var result=In('A','Z', 'a','z', '0','9'); return result;
		}
        public bool SpecialChar()    /*SpecialChar :   '<' / '>' ;*/
        {

           var result=    Char('<') || Char('>'); return result;
		}
        public bool NormalChar()    /*NormalChar :    !( SpecialChar) .;*/
        {

           var result=And(()=>    Not(()=> SpecialChar() ) && Any() ); return result;
		}
        public bool LiteralChar()    /*LiteralChar :    !( SpecialChar/Spacechar/Newline) .;*/
        {

           var result=And(()=>  
                     Not(()=>     SpecialChar() || Spacechar() || Newline() )
                  && Any() ); return result;
		}
        public bool Symbol()    /*^^Symbol :     SpecialChar  ;*/
        {

           var result= TreeNT((int)EHtml.Symbol,()=>
                SpecialChar() ); return result;
		}
        public bool InnerPlain()    /*^^InnerPlain :  NormalChar+  ;*/
        {

           var result= TreeNT((int)EHtml.InnerPlain,()=>
                PlusRepeat(()=> NormalChar() ) ); return result;
		}
        public bool Eof()    /*Eof :          !./ WARNING<" end of file">;*/
        {

           var result=    Not(()=> Any() ) || Warning(" end of file"); return result;
		}
		#endregion Grammar Rules

        #region Optimization Data 
        
        internal static OptimizedLiterals optimizedLiterals0;
        
        static Html()
        {
            
            {
               string[] literals=
               { "address","blockquote","center","dir","div","dl","fieldset","form",
                  "h1","h2","h3","h4","h5","h6","hr","isindex",
                  "menu","noframes","noscript","ol","p","pre","table","ul",
                  "dd","dt","frameset","li","tbody","td","tfoot","th",
                  "thead","tr","script","ADDRESS","BLOCKQUOTE","CENTER","DIR","DIV",
                  "DL","FIELDSET","FORM","H1","H2","H3","H4","H5",
                  "H6","HR","ISINDEX","MENU","NOFRAMES","NOSCRIPT","OL","P",
                  "PRE","TABLE","UL","DD","DT","FRAMESET","LI","TBODY",
                  "TD","TFOOT","TH","THEAD","TR","SCRIPT","br","BR",
                  "a","A" };
               optimizedLiterals0= new OptimizedLiterals(literals);
            }

            
        }
        #endregion Optimization Data 
           }
}