/* created on 24/12/2016 00:04:54 from peg generator V1.0 using 'Html' as input*/

using Peg.Base;
using System;
using System.IO;
using System.Text;
namespace Peg.Html
{
      
      enum EHtml{Doc= 1, Block= 2, Doctype= 3, HtmlBlockOpenAddress= 4, HtmlBlockCloseAddress= 5, 
                  HtmlBlockAddress= 6, HtmlBlockOpenBlockquote= 7, HtmlBlockCloseBlockquote= 8, 
                  HtmlBlockBlockquote= 9, HtmlBlockOpenCenter= 10, HtmlBlockCloseCenter= 11, 
                  HtmlBlockCenter= 12, HtmlBlockOpenDir= 13, HtmlBlockCloseDir= 14, 
                  HtmlBlockDir= 15, HtmlBlockOpenDiv= 16, HtmlBlockCloseDiv= 17, 
                  HtmlBlockDiv= 18, HtmlBlockOpenDl= 19, HtmlBlockCloseDl= 20, 
                  HtmlBlockDl= 21, HtmlBlockOpenFieldset= 22, HtmlBlockCloseFieldset= 23, 
                  HtmlBlockFieldset= 24, HtmlBlockOpenForm= 25, HtmlBlockCloseForm= 26, 
                  HtmlBlockForm= 27, HtmlBlockOpenH1= 28, HtmlBlockCloseH1= 29, 
                  HtmlBlockH1= 30, HtmlBlockOpenH2= 31, HtmlBlockCloseH2= 32, HtmlBlockH2= 33, 
                  HtmlBlockOpenH3= 34, HtmlBlockCloseH3= 35, HtmlBlockH3= 36, HtmlBlockOpenH4= 37, 
                  HtmlBlockCloseH4= 38, HtmlBlockH4= 39, HtmlBlockOpenH5= 40, HtmlBlockCloseH5= 41, 
                  HtmlBlockH5= 42, HtmlBlockOpenH6= 43, HtmlBlockCloseH6= 44, HtmlBlockH6= 45, 
                  HtmlBlockOpenMenu= 46, HtmlBlockCloseMenu= 47, HtmlBlockMenu= 48, 
                  HtmlBlockOpenNoframes= 49, HtmlBlockCloseNoframes= 50, HtmlBlockNoframes= 51, 
                  HtmlBlockOpenNoscript= 52, HtmlBlockCloseNoscript= 53, HtmlBlockNoscript= 54, 
                  HtmlBlockOpenOl= 55, HtmlBlockCloseOl= 56, HtmlBlockOl= 57, HtmlBlockOpenP= 58, 
                  HtmlBlockCloseP= 59, HtmlBlockP= 60, HtmlBlockOpenPre= 61, HtmlBlockClosePre= 62, 
                  HtmlBlockPre= 63, HtmlBlockOpenTable= 64, HtmlBlockCloseTable= 65, 
                  HtmlBlockTable= 66, HtmlBlockOpenUl= 67, HtmlBlockCloseUl= 68, 
                  HtmlBlockUl= 69, HtmlBlockOpenDd= 70, HtmlBlockCloseDd= 71, HtmlBlockDd= 72, 
                  HtmlBlockOpenDt= 73, HtmlBlockCloseDt= 74, HtmlBlockDt= 75, HtmlBlockOpenFrameset= 76, 
                  HtmlBlockCloseFrameset= 77, HtmlBlockFrameset= 78, HtmlBlockOpenLi= 79, 
                  HtmlBlockCloseLi= 80, HtmlBlockLi= 81, HtmlBlockOpenTbody= 82, 
                  HtmlBlockCloseTbody= 83, HtmlBlockTbody= 84, HtmlBlockOpenTd= 85, 
                  HtmlBlockCloseTd= 86, HtmlBlockTd= 87, HtmlBlockOpenTfoot= 88, 
                  HtmlBlockCloseTfoot= 89, HtmlBlockTfoot= 90, HtmlBlockOpenTh= 91, 
                  HtmlBlockCloseTh= 92, HtmlBlockTh= 93, HtmlBlockOpenThead= 94, 
                  HtmlBlockCloseThead= 95, HtmlBlockThead= 96, HtmlBlockOpenTr= 97, 
                  HtmlBlockCloseTr= 98, HtmlBlockTr= 99, HtmlBlockOpenScript= 100, 
                  HtmlBlockCloseScript= 101, HtmlBlockScript= 102, HtmlBlockOpenHead= 103, 
                  HtmlBlockCloseHead= 104, HtmlBlockHead= 105, HtmlBlockOpenA= 106, 
                  HtmlBlockCloseA= 107, HtmlBlockA= 108, HtmlBlockOpenCode= 109, 
                  HtmlBlockCloseCode= 110, HtmlBlockCode= 111, HtmlBlockOpenSpan= 112, 
                  HtmlBlockCloseSpan= 113, HtmlBlockSpan= 114, HtmlBlockOpenHtml= 115, 
                  HtmlBlockCloseHtml= 116, HtmlBlockHtml= 117, HtmlBlockOpenBody= 118, 
                  HtmlBlockCloseBody= 119, HtmlBlockBody= 120, HtmlBlockOpenHeader= 121, 
                  HtmlBlockCloseHeader= 122, HtmlBlockHeader= 123, HtmlBlockOpenUnknown= 124, 
                  HtmlBlockCloseUnknown= 125, HtmlBlockUnknown= 126, UnknownTagName= 127, 
                  HtmlBlockInTags= 128, HtmlBlock= 129, UnknownHtmlBlockSelfClosing= 130, 
                  HtmlBlockSelfClosing= 131, SingleHtmlBlockType= 132, HtmlBlockSelfClosingType= 133, 
                  StyleOpen= 134, StyleClose= 135, InStyleTags= 136, StyleBlock= 137, 
                  Space= 138, RawHtml= 139, BlankLine= 140, QuotedContents= 141, 
                  Quoted= 142, GlobalAttributes= 143, HtmlAttributeHeader= 144, 
                  ExpectedAttributeHeaderEnd= 145, ExpectedAttributeValue= 146, 
                  HtmlAttributes= 147, HtmlAttribute= 148, HtmlComment= 149, HtmlTag= 150, 
                  Spacechar= 151, Nonspacechar= 152, Newline= 153, Sp= 154, Spnl= 155, 
                  AlphanumericAscii= 156, SpecialChar= 157, NormalChar= 158, LiteralChar= 159, 
                  Symbol= 160, InnerPlain= 161, PurePlainContents= 162, LeftPlainContents= 163, 
                  RightPlainContents= 164, Eof= 165};
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
        public bool Doc()    /*^^Doc :    Doctype?  Block  *  Eof;*/
        {

           var result= TreeNT((int)EHtml.Doc,()=>
                And(()=>  
                     Option(()=> Doctype() )
                  && OptRepeat(()=> Block() )
                  && Eof() ) ); return result;
		}
        public bool Block()    /*^^Block :     BlankLine*
            ( HtmlBlock / StyleBlock);*/
        {

           var result= TreeNT((int)EHtml.Block,()=>
                And(()=>  
                     OptRepeat(()=> BlankLine() )
                  && (    HtmlBlock() || StyleBlock()) ) ); return result;
		}
        public bool Doctype()    /*^^Doctype: Spnl '<' Spnl '!DOCTYPE' Spnl 'html'\i  (!'>' .)* '>' Spnl ;

// Parsers for different kinds of block-level HTML content.
// This is repetitive due to constraints of PEG grammar.*/
        {

           var result= TreeNT((int)EHtml.Doctype,()=>
                And(()=>  
                     Spnl()
                  && Char('<')
                  && Spnl()
                  && Char("!DOCTYPE")
                  && Spnl()
                  && IChar('h','t','m','l')
                  && OptRepeat(()=>    
                      And(()=>    Not(()=> Char('>') ) && Any() ) )
                  && Char('>')
                  && Spnl() ) ); return result;
		}
        public bool HtmlBlockOpenAddress()    /*HtmlBlockOpenAddress : '<' Spnl ('address'  \i ) Spnl HtmlAttributes '>';*/
        {

           var result=And(()=>  
                     Char('<')
                  && Spnl()
                  && IChar('a','d','d','r','e','s','s')
                  && Spnl()
                  && HtmlAttributes()
                  && Char('>') ); return result;
		}
        public bool HtmlBlockCloseAddress()    /*HtmlBlockCloseAddress : '<' Spnl '/' ('address'  \i ) Spnl '>' ;*/
        {

           var result=And(()=>  
                     Char('<')
                  && Spnl()
                  && Char('/')
                  && IChar('a','d','d','r','e','s','s')
                  && Spnl()
                  && Char('>') ); return result;
		}
        public bool HtmlBlockAddress()    /*^^HtmlBlockAddress : HtmlBlockOpenAddress (&HtmlBlockCloseAddress / HtmlBlock+) @HtmlBlockCloseAddress;*/
        {

           var result= TreeNT((int)EHtml.HtmlBlockAddress,()=>
                And(()=>  
                     HtmlBlockOpenAddress()
                  && (    
                         Peek(()=> HtmlBlockCloseAddress() )
                      || PlusRepeat(()=> HtmlBlock() ))
                  && (    
                         HtmlBlockCloseAddress()
                      || Fatal("<<HtmlBlockCloseAddress>> expected")) ) ); return result;
		}
        public bool HtmlBlockOpenBlockquote()    /*HtmlBlockOpenBlockquote : '<' Spnl ('blockquote'  \i ) Spnl HtmlAttributes '>';*/
        {

           var result=And(()=>  
                     Char('<')
                  && Spnl()
                  && IChar("blockquote")
                  && Spnl()
                  && HtmlAttributes()
                  && Char('>') ); return result;
		}
        public bool HtmlBlockCloseBlockquote()    /*HtmlBlockCloseBlockquote : '<' Spnl '/' ('blockquote'  \i ) Spnl '>';*/
        {

           var result=And(()=>  
                     Char('<')
                  && Spnl()
                  && Char('/')
                  && IChar("blockquote")
                  && Spnl()
                  && Char('>') ); return result;
		}
        public bool HtmlBlockBlockquote()    /*^^HtmlBlockBlockquote : HtmlBlockOpenBlockquote (&HtmlBlockCloseBlockquote / HtmlBlock+) @HtmlBlockCloseBlockquote;*/
        {

           var result= TreeNT((int)EHtml.HtmlBlockBlockquote,()=>
                And(()=>  
                     HtmlBlockOpenBlockquote()
                  && (    
                         Peek(()=> HtmlBlockCloseBlockquote() )
                      || PlusRepeat(()=> HtmlBlock() ))
                  && (    
                         HtmlBlockCloseBlockquote()
                      || Fatal("<<HtmlBlockCloseBlockquote>> expected")) ) ); return result;
		}
        public bool HtmlBlockOpenCenter()    /*HtmlBlockOpenCenter : '<' Spnl ('center'  \i ) Spnl HtmlAttributes '>';*/
        {

           var result=And(()=>  
                     Char('<')
                  && Spnl()
                  && IChar('c','e','n','t','e','r')
                  && Spnl()
                  && HtmlAttributes()
                  && Char('>') ); return result;
		}
        public bool HtmlBlockCloseCenter()    /*HtmlBlockCloseCenter : '<' Spnl '/' ('center'  \i ) Spnl '>';*/
        {

           var result=And(()=>  
                     Char('<')
                  && Spnl()
                  && Char('/')
                  && IChar('c','e','n','t','e','r')
                  && Spnl()
                  && Char('>') ); return result;
		}
        public bool HtmlBlockCenter()    /*^^HtmlBlockCenter : HtmlBlockOpenCenter (&HtmlBlockCloseCenter / HtmlBlock+) @HtmlBlockCloseCenter;*/
        {

           var result= TreeNT((int)EHtml.HtmlBlockCenter,()=>
                And(()=>  
                     HtmlBlockOpenCenter()
                  && (    
                         Peek(()=> HtmlBlockCloseCenter() )
                      || PlusRepeat(()=> HtmlBlock() ))
                  && (    
                         HtmlBlockCloseCenter()
                      || Fatal("<<HtmlBlockCloseCenter>> expected")) ) ); return result;
		}
        public bool HtmlBlockOpenDir()    /*HtmlBlockOpenDir : '<' Spnl ('dir'  \i ) Spnl HtmlAttributes '>';*/
        {

           var result=And(()=>  
                     Char('<')
                  && Spnl()
                  && IChar('d','i','r')
                  && Spnl()
                  && HtmlAttributes()
                  && Char('>') ); return result;
		}
        public bool HtmlBlockCloseDir()    /*HtmlBlockCloseDir : '<' Spnl '/' ('dir'  \i ) Spnl '>';*/
        {

           var result=And(()=>  
                     Char('<')
                  && Spnl()
                  && Char('/')
                  && IChar('d','i','r')
                  && Spnl()
                  && Char('>') ); return result;
		}
        public bool HtmlBlockDir()    /*^^HtmlBlockDir : HtmlBlockOpenDir (&HtmlBlockCloseDir / HtmlBlock+) @HtmlBlockCloseDir;*/
        {

           var result= TreeNT((int)EHtml.HtmlBlockDir,()=>
                And(()=>  
                     HtmlBlockOpenDir()
                  && (    
                         Peek(()=> HtmlBlockCloseDir() )
                      || PlusRepeat(()=> HtmlBlock() ))
                  && (    
                         HtmlBlockCloseDir()
                      || Fatal("<<HtmlBlockCloseDir>> expected")) ) ); return result;
		}
        public bool HtmlBlockOpenDiv()    /*^^HtmlBlockOpenDiv : '<' Spnl ('div'  \i ) Spnl HtmlAttributes '>';*/
        {

           var result= TreeNT((int)EHtml.HtmlBlockOpenDiv,()=>
                And(()=>  
                     Char('<')
                  && Spnl()
                  && IChar('d','i','v')
                  && Spnl()
                  && HtmlAttributes()
                  && Char('>') ) ); return result;
		}
        public bool HtmlBlockCloseDiv()    /*^^HtmlBlockCloseDiv : '<' Spnl '/' ('div'  \i ) Spnl '>';*/
        {

           var result= TreeNT((int)EHtml.HtmlBlockCloseDiv,()=>
                And(()=>  
                     Char('<')
                  && Spnl()
                  && Char('/')
                  && IChar('d','i','v')
                  && Spnl()
                  && Char('>') ) ); return result;
		}
        public bool HtmlBlockDiv()    /*^^HtmlBlockDiv : HtmlBlockOpenDiv (&  HtmlBlockCloseDiv / HtmlBlock+) @HtmlBlockCloseDiv;*/
        {

           var result= TreeNT((int)EHtml.HtmlBlockDiv,()=>
                And(()=>  
                     HtmlBlockOpenDiv()
                  && (    
                         Peek(()=> HtmlBlockCloseDiv() )
                      || PlusRepeat(()=> HtmlBlock() ))
                  && (    
                         HtmlBlockCloseDiv()
                      || Fatal("<<HtmlBlockCloseDiv>> expected")) ) ); return result;
		}
        public bool HtmlBlockOpenDl()    /*HtmlBlockOpenDl : '<' Spnl ('dl'  \i ) Spnl HtmlAttributes '>';*/
        {

           var result=And(()=>  
                     Char('<')
                  && Spnl()
                  && IChar('d','l')
                  && Spnl()
                  && HtmlAttributes()
                  && Char('>') ); return result;
		}
        public bool HtmlBlockCloseDl()    /*HtmlBlockCloseDl : '<' Spnl '/' ('dl'  \i ) Spnl '>';*/
        {

           var result=And(()=>  
                     Char('<')
                  && Spnl()
                  && Char('/')
                  && IChar('d','l')
                  && Spnl()
                  && Char('>') ); return result;
		}
        public bool HtmlBlockDl()    /*^^HtmlBlockDl : HtmlBlockOpenDl (&HtmlBlockCloseDl / HtmlBlock+) @HtmlBlockCloseDl;*/
        {

           var result= TreeNT((int)EHtml.HtmlBlockDl,()=>
                And(()=>  
                     HtmlBlockOpenDl()
                  && (    
                         Peek(()=> HtmlBlockCloseDl() )
                      || PlusRepeat(()=> HtmlBlock() ))
                  && (    
                         HtmlBlockCloseDl()
                      || Fatal("<<HtmlBlockCloseDl>> expected")) ) ); return result;
		}
        public bool HtmlBlockOpenFieldset()    /*HtmlBlockOpenFieldset : '<' Spnl ('fieldset'  \i ) Spnl HtmlAttributes '>';*/
        {

           var result=And(()=>  
                     Char('<')
                  && Spnl()
                  && IChar("fieldset")
                  && Spnl()
                  && HtmlAttributes()
                  && Char('>') ); return result;
		}
        public bool HtmlBlockCloseFieldset()    /*HtmlBlockCloseFieldset : '<' Spnl '/' ('fieldset'  \i ) Spnl '>';*/
        {

           var result=And(()=>  
                     Char('<')
                  && Spnl()
                  && Char('/')
                  && IChar("fieldset")
                  && Spnl()
                  && Char('>') ); return result;
		}
        public bool HtmlBlockFieldset()    /*^^HtmlBlockFieldset : HtmlBlockOpenFieldset (&HtmlBlockCloseFieldset / HtmlBlock+) @HtmlBlockCloseFieldset;*/
        {

           var result= TreeNT((int)EHtml.HtmlBlockFieldset,()=>
                And(()=>  
                     HtmlBlockOpenFieldset()
                  && (    
                         Peek(()=> HtmlBlockCloseFieldset() )
                      || PlusRepeat(()=> HtmlBlock() ))
                  && (    
                         HtmlBlockCloseFieldset()
                      || Fatal("<<HtmlBlockCloseFieldset>> expected")) ) ); return result;
		}
        public bool HtmlBlockOpenForm()    /*HtmlBlockOpenForm : '<' Spnl ('form'  \i ) Spnl HtmlAttributes '>';*/
        {

           var result=And(()=>  
                     Char('<')
                  && Spnl()
                  && IChar('f','o','r','m')
                  && Spnl()
                  && HtmlAttributes()
                  && Char('>') ); return result;
		}
        public bool HtmlBlockCloseForm()    /*HtmlBlockCloseForm : '<' Spnl '/' ('form'  \i ) Spnl '>';*/
        {

           var result=And(()=>  
                     Char('<')
                  && Spnl()
                  && Char('/')
                  && IChar('f','o','r','m')
                  && Spnl()
                  && Char('>') ); return result;
		}
        public bool HtmlBlockForm()    /*^^HtmlBlockForm : HtmlBlockOpenForm (&HtmlBlockCloseForm / HtmlBlock+) @HtmlBlockCloseForm;*/
        {

           var result= TreeNT((int)EHtml.HtmlBlockForm,()=>
                And(()=>  
                     HtmlBlockOpenForm()
                  && (    
                         Peek(()=> HtmlBlockCloseForm() )
                      || PlusRepeat(()=> HtmlBlock() ))
                  && (    
                         HtmlBlockCloseForm()
                      || Fatal("<<HtmlBlockCloseForm>> expected")) ) ); return result;
		}
        public bool HtmlBlockOpenH1()    /*HtmlBlockOpenH1 : '<' Spnl ('h1' / 'H1') Spnl HtmlAttributes '>';*/
        {

           var result=And(()=>  
                     Char('<')
                  && Spnl()
                  && (    Char('h','1') || Char('H','1'))
                  && Spnl()
                  && HtmlAttributes()
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
        public bool HtmlBlockH1()    /*^^HtmlBlockH1 : HtmlBlockOpenH1 (&HtmlBlockCloseH1 / HtmlBlock+) @HtmlBlockCloseH1;*/
        {

           var result= TreeNT((int)EHtml.HtmlBlockH1,()=>
                And(()=>  
                     HtmlBlockOpenH1()
                  && (    
                         Peek(()=> HtmlBlockCloseH1() )
                      || PlusRepeat(()=> HtmlBlock() ))
                  && (    
                         HtmlBlockCloseH1()
                      || Fatal("<<HtmlBlockCloseH1>> expected")) ) ); return result;
		}
        public bool HtmlBlockOpenH2()    /*HtmlBlockOpenH2 : '<' Spnl ('h2' / 'H2') Spnl HtmlAttributes '>';*/
        {

           var result=And(()=>  
                     Char('<')
                  && Spnl()
                  && (    Char('h','2') || Char('H','2'))
                  && Spnl()
                  && HtmlAttributes()
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
        public bool HtmlBlockH2()    /*^^HtmlBlockH2 : HtmlBlockOpenH2 (&HtmlBlockCloseH2 / HtmlBlock+) @HtmlBlockCloseH2;*/
        {

           var result= TreeNT((int)EHtml.HtmlBlockH2,()=>
                And(()=>  
                     HtmlBlockOpenH2()
                  && (    
                         Peek(()=> HtmlBlockCloseH2() )
                      || PlusRepeat(()=> HtmlBlock() ))
                  && (    
                         HtmlBlockCloseH2()
                      || Fatal("<<HtmlBlockCloseH2>> expected")) ) ); return result;
		}
        public bool HtmlBlockOpenH3()    /*HtmlBlockOpenH3 : '<' Spnl ('h3' / 'H3') Spnl HtmlAttributes '>';*/
        {

           var result=And(()=>  
                     Char('<')
                  && Spnl()
                  && (    Char('h','3') || Char('H','3'))
                  && Spnl()
                  && HtmlAttributes()
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
        public bool HtmlBlockH3()    /*^^HtmlBlockH3 : HtmlBlockOpenH3 (&HtmlBlockCloseH3 / HtmlBlock+) @HtmlBlockCloseH3;*/
        {

           var result= TreeNT((int)EHtml.HtmlBlockH3,()=>
                And(()=>  
                     HtmlBlockOpenH3()
                  && (    
                         Peek(()=> HtmlBlockCloseH3() )
                      || PlusRepeat(()=> HtmlBlock() ))
                  && (    
                         HtmlBlockCloseH3()
                      || Fatal("<<HtmlBlockCloseH3>> expected")) ) ); return result;
		}
        public bool HtmlBlockOpenH4()    /*HtmlBlockOpenH4 : '<' Spnl ('h4' / 'H4') Spnl HtmlAttributes '>';*/
        {

           var result=And(()=>  
                     Char('<')
                  && Spnl()
                  && (    Char('h','4') || Char('H','4'))
                  && Spnl()
                  && HtmlAttributes()
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
        public bool HtmlBlockH4()    /*HtmlBlockH4 : HtmlBlockOpenH4 (&HtmlBlockCloseH4 / HtmlBlock+) @HtmlBlockCloseH4;*/
        {

           var result=And(()=>  
                     HtmlBlockOpenH4()
                  && (    
                         Peek(()=> HtmlBlockCloseH4() )
                      || PlusRepeat(()=> HtmlBlock() ))
                  && (    
                         HtmlBlockCloseH4()
                      || Fatal("<<HtmlBlockCloseH4>> expected")) ); return result;
		}
        public bool HtmlBlockOpenH5()    /*HtmlBlockOpenH5 : '<' Spnl ('h5' / 'H5') Spnl HtmlAttributes '>';*/
        {

           var result=And(()=>  
                     Char('<')
                  && Spnl()
                  && (    Char('h','5') || Char('H','5'))
                  && Spnl()
                  && HtmlAttributes()
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
        public bool HtmlBlockH5()    /*^^HtmlBlockH5 : HtmlBlockOpenH5 (&HtmlBlockCloseH5 / HtmlBlock+) @HtmlBlockCloseH5;*/
        {

           var result= TreeNT((int)EHtml.HtmlBlockH5,()=>
                And(()=>  
                     HtmlBlockOpenH5()
                  && (    
                         Peek(()=> HtmlBlockCloseH5() )
                      || PlusRepeat(()=> HtmlBlock() ))
                  && (    
                         HtmlBlockCloseH5()
                      || Fatal("<<HtmlBlockCloseH5>> expected")) ) ); return result;
		}
        public bool HtmlBlockOpenH6()    /*HtmlBlockOpenH6 : '<' Spnl ('h6' / 'H6') Spnl HtmlAttributes '>';*/
        {

           var result=And(()=>  
                     Char('<')
                  && Spnl()
                  && (    Char('h','6') || Char('H','6'))
                  && Spnl()
                  && HtmlAttributes()
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
        public bool HtmlBlockH6()    /*^^HtmlBlockH6 : HtmlBlockOpenH6 (&HtmlBlockCloseH6 / HtmlBlock+) @HtmlBlockCloseH6;*/
        {

           var result= TreeNT((int)EHtml.HtmlBlockH6,()=>
                And(()=>  
                     HtmlBlockOpenH6()
                  && (    
                         Peek(()=> HtmlBlockCloseH6() )
                      || PlusRepeat(()=> HtmlBlock() ))
                  && (    
                         HtmlBlockCloseH6()
                      || Fatal("<<HtmlBlockCloseH6>> expected")) ) ); return result;
		}
        public bool HtmlBlockOpenMenu()    /*HtmlBlockOpenMenu : '<' Spnl ('menu'  \i ) Spnl HtmlAttributes '>';*/
        {

           var result=And(()=>  
                     Char('<')
                  && Spnl()
                  && IChar('m','e','n','u')
                  && Spnl()
                  && HtmlAttributes()
                  && Char('>') ); return result;
		}
        public bool HtmlBlockCloseMenu()    /*HtmlBlockCloseMenu : '<' Spnl '/' ('menu'  \i ) Spnl '>';*/
        {

           var result=And(()=>  
                     Char('<')
                  && Spnl()
                  && Char('/')
                  && IChar('m','e','n','u')
                  && Spnl()
                  && Char('>') ); return result;
		}
        public bool HtmlBlockMenu()    /*^^HtmlBlockMenu : HtmlBlockOpenMenu (&HtmlBlockCloseMenu / HtmlBlock+) @HtmlBlockCloseMenu;*/
        {

           var result= TreeNT((int)EHtml.HtmlBlockMenu,()=>
                And(()=>  
                     HtmlBlockOpenMenu()
                  && (    
                         Peek(()=> HtmlBlockCloseMenu() )
                      || PlusRepeat(()=> HtmlBlock() ))
                  && (    
                         HtmlBlockCloseMenu()
                      || Fatal("<<HtmlBlockCloseMenu>> expected")) ) ); return result;
		}
        public bool HtmlBlockOpenNoframes()    /*HtmlBlockOpenNoframes : '<' Spnl ('noframes'  \i ) Spnl HtmlAttributes '>';*/
        {

           var result=And(()=>  
                     Char('<')
                  && Spnl()
                  && IChar("noframes")
                  && Spnl()
                  && HtmlAttributes()
                  && Char('>') ); return result;
		}
        public bool HtmlBlockCloseNoframes()    /*HtmlBlockCloseNoframes : '<' Spnl '/' ('noframes'  \i ) Spnl '>';*/
        {

           var result=And(()=>  
                     Char('<')
                  && Spnl()
                  && Char('/')
                  && IChar("noframes")
                  && Spnl()
                  && Char('>') ); return result;
		}
        public bool HtmlBlockNoframes()    /*^^HtmlBlockNoframes : HtmlBlockOpenNoframes (HtmlBlockNoframes / !HtmlBlockCloseNoframes .)* @HtmlBlockCloseNoframes;*/
        {

           var result= TreeNT((int)EHtml.HtmlBlockNoframes,()=>
                And(()=>  
                     HtmlBlockOpenNoframes()
                  && OptRepeat(()=>    
                            
                               HtmlBlockNoframes()
                            || And(()=>        
                                       Not(()=> HtmlBlockCloseNoframes() )
                                    && Any() ) )
                  && (    
                         HtmlBlockCloseNoframes()
                      || Fatal("<<HtmlBlockCloseNoframes>> expected")) ) ); return result;
		}
        public bool HtmlBlockOpenNoscript()    /*HtmlBlockOpenNoscript : '<' Spnl ('noscript'  \i ) Spnl HtmlAttributes '>';*/
        {

           var result=And(()=>  
                     Char('<')
                  && Spnl()
                  && IChar("noscript")
                  && Spnl()
                  && HtmlAttributes()
                  && Char('>') ); return result;
		}
        public bool HtmlBlockCloseNoscript()    /*HtmlBlockCloseNoscript : '<' Spnl '/' ('noscript'  \i ) Spnl '>';*/
        {

           var result=And(()=>  
                     Char('<')
                  && Spnl()
                  && Char('/')
                  && IChar("noscript")
                  && Spnl()
                  && Char('>') ); return result;
		}
        public bool HtmlBlockNoscript()    /*^^HtmlBlockNoscript : HtmlBlockOpenNoscript (HtmlBlockNoscript / !HtmlBlockCloseNoscript .)* @HtmlBlockCloseNoscript;*/
        {

           var result= TreeNT((int)EHtml.HtmlBlockNoscript,()=>
                And(()=>  
                     HtmlBlockOpenNoscript()
                  && OptRepeat(()=>    
                            
                               HtmlBlockNoscript()
                            || And(()=>        
                                       Not(()=> HtmlBlockCloseNoscript() )
                                    && Any() ) )
                  && (    
                         HtmlBlockCloseNoscript()
                      || Fatal("<<HtmlBlockCloseNoscript>> expected")) ) ); return result;
		}
        public bool HtmlBlockOpenOl()    /*HtmlBlockOpenOl : '<' Spnl ('ol'  \i ) Spnl HtmlAttributes '>';*/
        {

           var result=And(()=>  
                     Char('<')
                  && Spnl()
                  && IChar('o','l')
                  && Spnl()
                  && HtmlAttributes()
                  && Char('>') ); return result;
		}
        public bool HtmlBlockCloseOl()    /*HtmlBlockCloseOl : '<' Spnl '/' ('ol'  \i ) Spnl '>';*/
        {

           var result=And(()=>  
                     Char('<')
                  && Spnl()
                  && Char('/')
                  && IChar('o','l')
                  && Spnl()
                  && Char('>') ); return result;
		}
        public bool HtmlBlockOl()    /*^^HtmlBlockOl : HtmlBlockOpenOl (&HtmlBlockCloseOl / HtmlBlock+) @HtmlBlockCloseOl;*/
        {

           var result= TreeNT((int)EHtml.HtmlBlockOl,()=>
                And(()=>  
                     HtmlBlockOpenOl()
                  && (    
                         Peek(()=> HtmlBlockCloseOl() )
                      || PlusRepeat(()=> HtmlBlock() ))
                  && (    
                         HtmlBlockCloseOl()
                      || Fatal("<<HtmlBlockCloseOl>> expected")) ) ); return result;
		}
        public bool HtmlBlockOpenP()    /*HtmlBlockOpenP : '<' Spnl ('p'  \i ) !(LiteralChar) Spnl  HtmlAttributes '>';*/
        {

           var result=And(()=>  
                     Char('<')
                  && Spnl()
                  && IChar('p')
                  && Not(()=> LiteralChar() )
                  && Spnl()
                  && HtmlAttributes()
                  && Char('>') ); return result;
		}
        public bool HtmlBlockCloseP()    /*HtmlBlockCloseP : '<' Spnl '/' ('p'  \i ) Spnl '>';*/
        {

           var result=And(()=>  
                     Char('<')
                  && Spnl()
                  && Char('/')
                  && IChar('p')
                  && Spnl()
                  && Char('>') ); return result;
		}
        public bool HtmlBlockP()    /*^^HtmlBlockP : HtmlBlockOpenP (&HtmlBlockCloseP / HtmlBlock +) @HtmlBlockCloseP;*/
        {

           var result= TreeNT((int)EHtml.HtmlBlockP,()=>
                And(()=>  
                     HtmlBlockOpenP()
                  && (    
                         Peek(()=> HtmlBlockCloseP() )
                      || PlusRepeat(()=> HtmlBlock() ))
                  && (    
                         HtmlBlockCloseP()
                      || Fatal("<<HtmlBlockCloseP>> expected")) ) ); return result;
		}
        public bool HtmlBlockOpenPre()    /*HtmlBlockOpenPre : '<' Spnl ('pre'  \i ) Spnl HtmlAttributes '>';*/
        {

           var result=And(()=>  
                     Char('<')
                  && Spnl()
                  && IChar('p','r','e')
                  && Spnl()
                  && HtmlAttributes()
                  && Char('>') ); return result;
		}
        public bool HtmlBlockClosePre()    /*HtmlBlockClosePre : '<' Spnl '/' ('pre'  \i ) Spnl '>';*/
        {

           var result=And(()=>  
                     Char('<')
                  && Spnl()
                  && Char('/')
                  && IChar('p','r','e')
                  && Spnl()
                  && Char('>') ); return result;
		}
        public bool HtmlBlockPre()    /*^^HtmlBlockPre : HtmlBlockOpenPre (HtmlBlockCode/(HtmlBlockPre / !HtmlBlockClosePre .)*) @HtmlBlockClosePre;*/
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
                  && (    
                         HtmlBlockClosePre()
                      || Fatal("<<HtmlBlockClosePre>> expected")) ) ); return result;
		}
        public bool HtmlBlockOpenTable()    /*HtmlBlockOpenTable : '<' Spnl ('table'  \i ) Spnl HtmlAttributes '>';*/
        {

           var result=And(()=>  
                     Char('<')
                  && Spnl()
                  && IChar('t','a','b','l','e')
                  && Spnl()
                  && HtmlAttributes()
                  && Char('>') ); return result;
		}
        public bool HtmlBlockCloseTable()    /*HtmlBlockCloseTable : '<' Spnl '/' ('table'  \i ) Spnl '>';*/
        {

           var result=And(()=>  
                     Char('<')
                  && Spnl()
                  && Char('/')
                  && IChar('t','a','b','l','e')
                  && Spnl()
                  && Char('>') ); return result;
		}
        public bool HtmlBlockTable()    /*^^HtmlBlockTable : HtmlBlockOpenTable (&HtmlBlockCloseTable / HtmlBlock+) @HtmlBlockCloseTable;*/
        {

           var result= TreeNT((int)EHtml.HtmlBlockTable,()=>
                And(()=>  
                     HtmlBlockOpenTable()
                  && (    
                         Peek(()=> HtmlBlockCloseTable() )
                      || PlusRepeat(()=> HtmlBlock() ))
                  && (    
                         HtmlBlockCloseTable()
                      || Fatal("<<HtmlBlockCloseTable>> expected")) ) ); return result;
		}
        public bool HtmlBlockOpenUl()    /*HtmlBlockOpenUl : '<' Spnl ('ul'  \i ) Spnl HtmlAttributes '>';*/
        {

           var result=And(()=>  
                     Char('<')
                  && Spnl()
                  && IChar('u','l')
                  && Spnl()
                  && HtmlAttributes()
                  && Char('>') ); return result;
		}
        public bool HtmlBlockCloseUl()    /*HtmlBlockCloseUl : '<' Spnl '/' ('ul'  \i ) Spnl '>';*/
        {

           var result=And(()=>  
                     Char('<')
                  && Spnl()
                  && Char('/')
                  && IChar('u','l')
                  && Spnl()
                  && Char('>') ); return result;
		}
        public bool HtmlBlockUl()    /*^^HtmlBlockUl : HtmlBlockOpenUl (&HtmlBlockCloseUl /  HtmlBlock+ ) @HtmlBlockCloseUl;*/
        {

           var result= TreeNT((int)EHtml.HtmlBlockUl,()=>
                And(()=>  
                     HtmlBlockOpenUl()
                  && (    
                         Peek(()=> HtmlBlockCloseUl() )
                      || PlusRepeat(()=> HtmlBlock() ))
                  && (    
                         HtmlBlockCloseUl()
                      || Fatal("<<HtmlBlockCloseUl>> expected")) ) ); return result;
		}
        public bool HtmlBlockOpenDd()    /*HtmlBlockOpenDd : '<' Spnl ('dd'  \i ) Spnl HtmlAttributes '>';*/
        {

           var result=And(()=>  
                     Char('<')
                  && Spnl()
                  && IChar('d','d')
                  && Spnl()
                  && HtmlAttributes()
                  && Char('>') ); return result;
		}
        public bool HtmlBlockCloseDd()    /*HtmlBlockCloseDd : '<' Spnl '/' ('dd'  \i ) Spnl '>';*/
        {

           var result=And(()=>  
                     Char('<')
                  && Spnl()
                  && Char('/')
                  && IChar('d','d')
                  && Spnl()
                  && Char('>') ); return result;
		}
        public bool HtmlBlockDd()    /*^^HtmlBlockDd : HtmlBlockOpenDd (&HtmlBlockCloseDd / HtmlBlock+) @HtmlBlockCloseDd;*/
        {

           var result= TreeNT((int)EHtml.HtmlBlockDd,()=>
                And(()=>  
                     HtmlBlockOpenDd()
                  && (    
                         Peek(()=> HtmlBlockCloseDd() )
                      || PlusRepeat(()=> HtmlBlock() ))
                  && (    
                         HtmlBlockCloseDd()
                      || Fatal("<<HtmlBlockCloseDd>> expected")) ) ); return result;
		}
        public bool HtmlBlockOpenDt()    /*HtmlBlockOpenDt : '<' Spnl ('dt'  \i ) Spnl HtmlAttributes '>';*/
        {

           var result=And(()=>  
                     Char('<')
                  && Spnl()
                  && IChar('d','t')
                  && Spnl()
                  && HtmlAttributes()
                  && Char('>') ); return result;
		}
        public bool HtmlBlockCloseDt()    /*HtmlBlockCloseDt : '<' Spnl '/' ('dt'  \i ) Spnl '>';*/
        {

           var result=And(()=>  
                     Char('<')
                  && Spnl()
                  && Char('/')
                  && IChar('d','t')
                  && Spnl()
                  && Char('>') ); return result;
		}
        public bool HtmlBlockDt()    /*^^HtmlBlockDt : HtmlBlockOpenDt (&HtmlBlockCloseDt / HtmlBlock+) @HtmlBlockCloseDt;*/
        {

           var result= TreeNT((int)EHtml.HtmlBlockDt,()=>
                And(()=>  
                     HtmlBlockOpenDt()
                  && (    
                         Peek(()=> HtmlBlockCloseDt() )
                      || PlusRepeat(()=> HtmlBlock() ))
                  && (    
                         HtmlBlockCloseDt()
                      || Fatal("<<HtmlBlockCloseDt>> expected")) ) ); return result;
		}
        public bool HtmlBlockOpenFrameset()    /*HtmlBlockOpenFrameset : '<' Spnl ('frameset'  \i ) Spnl HtmlAttributes '>';*/
        {

           var result=And(()=>  
                     Char('<')
                  && Spnl()
                  && IChar("frameset")
                  && Spnl()
                  && HtmlAttributes()
                  && Char('>') ); return result;
		}
        public bool HtmlBlockCloseFrameset()    /*HtmlBlockCloseFrameset : '<' Spnl '/' ('frameset'  \i ) Spnl '>';*/
        {

           var result=And(()=>  
                     Char('<')
                  && Spnl()
                  && Char('/')
                  && IChar("frameset")
                  && Spnl()
                  && Char('>') ); return result;
		}
        public bool HtmlBlockFrameset()    /*^^HtmlBlockFrameset : HtmlBlockOpenFrameset (&HtmlBlockCloseFrameset / HtmlBlock+ ) @HtmlBlockCloseFrameset;*/
        {

           var result= TreeNT((int)EHtml.HtmlBlockFrameset,()=>
                And(()=>  
                     HtmlBlockOpenFrameset()
                  && (    
                         Peek(()=> HtmlBlockCloseFrameset() )
                      || PlusRepeat(()=> HtmlBlock() ))
                  && (    
                         HtmlBlockCloseFrameset()
                      || Fatal("<<HtmlBlockCloseFrameset>> expected")) ) ); return result;
		}
        public bool HtmlBlockOpenLi()    /*HtmlBlockOpenLi : '<' Spnl ('li'  \i ) !(LiteralChar)  Spnl HtmlAttributes '>';*/
        {

           var result=And(()=>  
                     Char('<')
                  && Spnl()
                  && IChar('l','i')
                  && Not(()=> LiteralChar() )
                  && Spnl()
                  && HtmlAttributes()
                  && Char('>') ); return result;
		}
        public bool HtmlBlockCloseLi()    /*HtmlBlockCloseLi : '<' Spnl '/' ('li'  \i ) Spnl '>';*/
        {

           var result=And(()=>  
                     Char('<')
                  && Spnl()
                  && Char('/')
                  && IChar('l','i')
                  && Spnl()
                  && Char('>') ); return result;
		}
        public bool HtmlBlockLi()    /*^^HtmlBlockLi : HtmlBlockOpenLi (&HtmlBlockCloseLi/HtmlBlock+  ) @HtmlBlockCloseLi;*/
        {

           var result= TreeNT((int)EHtml.HtmlBlockLi,()=>
                And(()=>  
                     HtmlBlockOpenLi()
                  && (    
                         Peek(()=> HtmlBlockCloseLi() )
                      || PlusRepeat(()=> HtmlBlock() ))
                  && (    
                         HtmlBlockCloseLi()
                      || Fatal("<<HtmlBlockCloseLi>> expected")) ) ); return result;
		}
        public bool HtmlBlockOpenTbody()    /*HtmlBlockOpenTbody : '<' Spnl ('tbody'  \i ) Spnl HtmlAttributes '>';*/
        {

           var result=And(()=>  
                     Char('<')
                  && Spnl()
                  && IChar('t','b','o','d','y')
                  && Spnl()
                  && HtmlAttributes()
                  && Char('>') ); return result;
		}
        public bool HtmlBlockCloseTbody()    /*HtmlBlockCloseTbody : '<' Spnl '/' ('tbody' \i ) Spnl '>';*/
        {

           var result=And(()=>  
                     Char('<')
                  && Spnl()
                  && Char('/')
                  && IChar('t','b','o','d','y')
                  && Spnl()
                  && Char('>') ); return result;
		}
        public bool HtmlBlockTbody()    /*^^HtmlBlockTbody : HtmlBlockOpenTbody (&HtmlBlockCloseTbody / HtmlBlock+ ) @HtmlBlockCloseTbody;*/
        {

           var result= TreeNT((int)EHtml.HtmlBlockTbody,()=>
                And(()=>  
                     HtmlBlockOpenTbody()
                  && (    
                         Peek(()=> HtmlBlockCloseTbody() )
                      || PlusRepeat(()=> HtmlBlock() ))
                  && (    
                         HtmlBlockCloseTbody()
                      || Fatal("<<HtmlBlockCloseTbody>> expected")) ) ); return result;
		}
        public bool HtmlBlockOpenTd()    /*HtmlBlockOpenTd : '<' Spnl ('td'  \i ) Spnl HtmlAttributes '>';*/
        {

           var result=And(()=>  
                     Char('<')
                  && Spnl()
                  && IChar('t','d')
                  && Spnl()
                  && HtmlAttributes()
                  && Char('>') ); return result;
		}
        public bool HtmlBlockCloseTd()    /*HtmlBlockCloseTd : '<' Spnl '/' ('td'  \i ) Spnl '>';*/
        {

           var result=And(()=>  
                     Char('<')
                  && Spnl()
                  && Char('/')
                  && IChar('t','d')
                  && Spnl()
                  && Char('>') ); return result;
		}
        public bool HtmlBlockTd()    /*^^HtmlBlockTd : HtmlBlockOpenTd (&HtmlBlockCloseTd / HtmlBlock+) @HtmlBlockCloseTd;*/
        {

           var result= TreeNT((int)EHtml.HtmlBlockTd,()=>
                And(()=>  
                     HtmlBlockOpenTd()
                  && (    
                         Peek(()=> HtmlBlockCloseTd() )
                      || PlusRepeat(()=> HtmlBlock() ))
                  && (    
                         HtmlBlockCloseTd()
                      || Fatal("<<HtmlBlockCloseTd>> expected")) ) ); return result;
		}
        public bool HtmlBlockOpenTfoot()    /*HtmlBlockOpenTfoot : '<' Spnl ('tfoot'  \i ) Spnl HtmlAttributes '>';*/
        {

           var result=And(()=>  
                     Char('<')
                  && Spnl()
                  && IChar('t','f','o','o','t')
                  && Spnl()
                  && HtmlAttributes()
                  && Char('>') ); return result;
		}
        public bool HtmlBlockCloseTfoot()    /*HtmlBlockCloseTfoot : '<' Spnl '/' ('tfoot'  \i ) Spnl '>';*/
        {

           var result=And(()=>  
                     Char('<')
                  && Spnl()
                  && Char('/')
                  && IChar('t','f','o','o','t')
                  && Spnl()
                  && Char('>') ); return result;
		}
        public bool HtmlBlockTfoot()    /*^^HtmlBlockTfoot : HtmlBlockOpenTfoot (&HtmlBlockCloseTfoot / HtmlBlock+) @HtmlBlockCloseTfoot;*/
        {

           var result= TreeNT((int)EHtml.HtmlBlockTfoot,()=>
                And(()=>  
                     HtmlBlockOpenTfoot()
                  && (    
                         Peek(()=> HtmlBlockCloseTfoot() )
                      || PlusRepeat(()=> HtmlBlock() ))
                  && (    
                         HtmlBlockCloseTfoot()
                      || Fatal("<<HtmlBlockCloseTfoot>> expected")) ) ); return result;
		}
        public bool HtmlBlockOpenTh()    /*HtmlBlockOpenTh : '<' Spnl ('th'  \i ) Spnl HtmlAttributes '>';*/
        {

           var result=And(()=>  
                     Char('<')
                  && Spnl()
                  && IChar('t','h')
                  && Spnl()
                  && HtmlAttributes()
                  && Char('>') ); return result;
		}
        public bool HtmlBlockCloseTh()    /*HtmlBlockCloseTh : '<' Spnl '/' ('th'  \i ) Spnl '>';*/
        {

           var result=And(()=>  
                     Char('<')
                  && Spnl()
                  && Char('/')
                  && IChar('t','h')
                  && Spnl()
                  && Char('>') ); return result;
		}
        public bool HtmlBlockTh()    /*^^HtmlBlockTh : HtmlBlockOpenTh (&HtmlBlockCloseTh / HtmlBlock+) @HtmlBlockCloseTh;*/
        {

           var result= TreeNT((int)EHtml.HtmlBlockTh,()=>
                And(()=>  
                     HtmlBlockOpenTh()
                  && (    
                         Peek(()=> HtmlBlockCloseTh() )
                      || PlusRepeat(()=> HtmlBlock() ))
                  && (    
                         HtmlBlockCloseTh()
                      || Fatal("<<HtmlBlockCloseTh>> expected")) ) ); return result;
		}
        public bool HtmlBlockOpenThead()    /*HtmlBlockOpenThead : '<' Spnl ('thead'  \i ) Spnl HtmlAttributes '>';*/
        {

           var result=And(()=>  
                     Char('<')
                  && Spnl()
                  && IChar('t','h','e','a','d')
                  && Spnl()
                  && HtmlAttributes()
                  && Char('>') ); return result;
		}
        public bool HtmlBlockCloseThead()    /*HtmlBlockCloseThead : '<' Spnl '/' ('thead' \i ) Spnl '>';*/
        {

           var result=And(()=>  
                     Char('<')
                  && Spnl()
                  && Char('/')
                  && IChar('t','h','e','a','d')
                  && Spnl()
                  && Char('>') ); return result;
		}
        public bool HtmlBlockThead()    /*^^HtmlBlockThead : HtmlBlockOpenThead (&HtmlBlockCloseThead / HtmlBlockTr+ ) @HtmlBlockCloseThead;*/
        {

           var result= TreeNT((int)EHtml.HtmlBlockThead,()=>
                And(()=>  
                     HtmlBlockOpenThead()
                  && (    
                         Peek(()=> HtmlBlockCloseThead() )
                      || PlusRepeat(()=> HtmlBlockTr() ))
                  && (    
                         HtmlBlockCloseThead()
                      || Fatal("<<HtmlBlockCloseThead>> expected")) ) ); return result;
		}
        public bool HtmlBlockOpenTr()    /*HtmlBlockOpenTr : '<' Spnl ('tr'   \i ) Spnl HtmlAttributes '>';*/
        {

           var result=And(()=>  
                     Char('<')
                  && Spnl()
                  && IChar('t','r')
                  && Spnl()
                  && HtmlAttributes()
                  && Char('>') ); return result;
		}
        public bool HtmlBlockCloseTr()    /*HtmlBlockCloseTr : '<' Spnl '/' ('tr'  \i ) Spnl '>';*/
        {

           var result=And(()=>  
                     Char('<')
                  && Spnl()
                  && Char('/')
                  && IChar('t','r')
                  && Spnl()
                  && Char('>') ); return result;
		}
        public bool HtmlBlockTr()    /*^^HtmlBlockTr : HtmlBlockOpenTr (&HtmlBlockCloseTr / HtmlBlock+) @HtmlBlockCloseTr;*/
        {

           var result= TreeNT((int)EHtml.HtmlBlockTr,()=>
                And(()=>  
                     HtmlBlockOpenTr()
                  && (    
                         Peek(()=> HtmlBlockCloseTr() )
                      || PlusRepeat(()=> HtmlBlock() ))
                  && (    
                         HtmlBlockCloseTr()
                      || Fatal("<<HtmlBlockCloseTr>> expected")) ) ); return result;
		}
        public bool HtmlBlockOpenScript()    /*HtmlBlockOpenScript : '<' Spnl ('script'  \i ) Spnl HtmlAttributes '>';*/
        {

           var result=And(()=>  
                     Char('<')
                  && Spnl()
                  && IChar('s','c','r','i','p','t')
                  && Spnl()
                  && HtmlAttributes()
                  && Char('>') ); return result;
		}
        public bool HtmlBlockCloseScript()    /*HtmlBlockCloseScript : '<' Spnl '/' ('script'   \i ) Spnl '>';*/
        {

           var result=And(()=>  
                     Char('<')
                  && Spnl()
                  && Char('/')
                  && IChar('s','c','r','i','p','t')
                  && Spnl()
                  && Char('>') ); return result;
		}
        public bool HtmlBlockScript()    /*^^HtmlBlockScript : HtmlBlockOpenScript (!HtmlBlockCloseScript .)* @HtmlBlockCloseScript;*/
        {

           var result= TreeNT((int)EHtml.HtmlBlockScript,()=>
                And(()=>  
                     HtmlBlockOpenScript()
                  && OptRepeat(()=>    
                      And(()=>      
                               Not(()=> HtmlBlockCloseScript() )
                            && Any() ) )
                  && (    
                         HtmlBlockCloseScript()
                      || Fatal("<<HtmlBlockCloseScript>> expected")) ) ); return result;
		}
        public bool HtmlBlockOpenHead()    /*HtmlBlockOpenHead : '<' Spnl ('head'  \i )  !(LiteralChar) Spnl HtmlAttributes '>';*/
        {

           var result=And(()=>  
                     Char('<')
                  && Spnl()
                  && IChar('h','e','a','d')
                  && Not(()=> LiteralChar() )
                  && Spnl()
                  && HtmlAttributes()
                  && Char('>') ); return result;
		}
        public bool HtmlBlockCloseHead()    /*HtmlBlockCloseHead : '<' Spnl '/' ('head'  \i ) Spnl '>';*/
        {

           var result=And(()=>  
                     Char('<')
                  && Spnl()
                  && Char('/')
                  && IChar('h','e','a','d')
                  && Spnl()
                  && Char('>') ); return result;
		}
        public bool HtmlBlockHead()    /*^^HtmlBlockHead : HtmlBlockOpenHead (&HtmlBlockCloseHead/ HtmlBlock+) @HtmlBlockCloseHead ;*/
        {

           var result= TreeNT((int)EHtml.HtmlBlockHead,()=>
                And(()=>  
                     HtmlBlockOpenHead()
                  && (    
                         Peek(()=> HtmlBlockCloseHead() )
                      || PlusRepeat(()=> HtmlBlock() ))
                  && (    
                         HtmlBlockCloseHead()
                      || Fatal("<<HtmlBlockCloseHead>> expected")) ) ); return result;
		}
        public bool HtmlBlockOpenA()    /*HtmlBlockOpenA : '<' Spnl ('a'  \i ) !(LiteralChar)  Spnl HtmlAttributes '>';*/
        {

           var result=And(()=>  
                     Char('<')
                  && Spnl()
                  && IChar('a')
                  && Not(()=> LiteralChar() )
                  && Spnl()
                  && HtmlAttributes()
                  && Char('>') ); return result;
		}
        public bool HtmlBlockCloseA()    /*HtmlBlockCloseA : '<' Spnl '/' ('a'  \i ) Spnl '>';*/
        {

           var result=And(()=>  
                     Char('<')
                  && Spnl()
                  && Char('/')
                  && IChar('a')
                  && Spnl()
                  && Char('>') ); return result;
		}
        public bool HtmlBlockA()    /*^^HtmlBlockA : HtmlBlockOpenA (&HtmlBlockCloseA /HtmlBlock+) @HtmlBlockCloseA ;*/
        {

           var result= TreeNT((int)EHtml.HtmlBlockA,()=>
                And(()=>  
                     HtmlBlockOpenA()
                  && (    
                         Peek(()=> HtmlBlockCloseA() )
                      || PlusRepeat(()=> HtmlBlock() ))
                  && (    
                         HtmlBlockCloseA()
                      || Fatal("<<HtmlBlockCloseA>> expected")) ) ); return result;
		}
        public bool HtmlBlockOpenCode()    /*HtmlBlockOpenCode : '<' Spnl ('code'  \i ) Spnl  '>';*/
        {

           var result=And(()=>  
                     Char('<')
                  && Spnl()
                  && IChar('c','o','d','e')
                  && Spnl()
                  && Char('>') ); return result;
		}
        public bool HtmlBlockCloseCode()    /*HtmlBlockCloseCode  : '<' Spnl '/' ('code'  \i ) Spnl '>';*/
        {

           var result=And(()=>  
                     Char('<')
                  && Spnl()
                  && Char('/')
                  && IChar('c','o','d','e')
                  && Spnl()
                  && Char('>') ); return result;
		}
        public bool HtmlBlockCode()    /*^^HtmlBlockCode : HtmlBlockOpenCode ( !HtmlBlockCloseCode .)* @HtmlBlockCloseCode ;*/
        {

           var result= TreeNT((int)EHtml.HtmlBlockCode,()=>
                And(()=>  
                     HtmlBlockOpenCode()
                  && OptRepeat(()=>    
                      And(()=>    Not(()=> HtmlBlockCloseCode() ) && Any() ) )
                  && (    
                         HtmlBlockCloseCode()
                      || Fatal("<<HtmlBlockCloseCode>> expected")) ) ); return result;
		}
        public bool HtmlBlockOpenSpan()    /*HtmlBlockOpenSpan : '<' Spnl ('span'  \i ) Spnl HtmlAttributes   '>';*/
        {

           var result=And(()=>  
                     Char('<')
                  && Spnl()
                  && IChar('s','p','a','n')
                  && Spnl()
                  && HtmlAttributes()
                  && Char('>') ); return result;
		}
        public bool HtmlBlockCloseSpan()    /*HtmlBlockCloseSpan  : '<' Spnl '/' ('span' \i) Spnl '>';*/
        {

           var result=And(()=>  
                     Char('<')
                  && Spnl()
                  && Char('/')
                  && IChar('s','p','a','n')
                  && Spnl()
                  && Char('>') ); return result;
		}
        public bool HtmlBlockSpan()    /*^^HtmlBlockSpan : HtmlBlockOpenSpan ( &HtmlBlockCloseSpan /HtmlBlock+) @HtmlBlockCloseSpan ;*/
        {

           var result= TreeNT((int)EHtml.HtmlBlockSpan,()=>
                And(()=>  
                     HtmlBlockOpenSpan()
                  && (    
                         Peek(()=> HtmlBlockCloseSpan() )
                      || PlusRepeat(()=> HtmlBlock() ))
                  && (    
                         HtmlBlockCloseSpan()
                      || Fatal("<<HtmlBlockCloseSpan>> expected")) ) ); return result;
		}
        public bool HtmlBlockOpenHtml()    /*HtmlBlockOpenHtml : '<' Spnl ('html'  \i ) Spnl  HtmlAttributes  '>';*/
        {

           var result=And(()=>  
                     Char('<')
                  && Spnl()
                  && IChar('h','t','m','l')
                  && Spnl()
                  && HtmlAttributes()
                  && Char('>') ); return result;
		}
        public bool HtmlBlockCloseHtml()    /*HtmlBlockCloseHtml  : '<' Spnl '/' ('html' \i) Spnl '>';*/
        {

           var result=And(()=>  
                     Char('<')
                  && Spnl()
                  && Char('/')
                  && IChar('h','t','m','l')
                  && Spnl()
                  && Char('>') ); return result;
		}
        public bool HtmlBlockHtml()    /*^^HtmlBlockHtml : HtmlBlockOpenHtml ( &HtmlBlockCloseHtml /HtmlBlock+) @HtmlBlockCloseHtml ;*/
        {

           var result= TreeNT((int)EHtml.HtmlBlockHtml,()=>
                And(()=>  
                     HtmlBlockOpenHtml()
                  && (    
                         Peek(()=> HtmlBlockCloseHtml() )
                      || PlusRepeat(()=> HtmlBlock() ))
                  && (    
                         HtmlBlockCloseHtml()
                      || Fatal("<<HtmlBlockCloseHtml>> expected")) ) ); return result;
		}
        public bool HtmlBlockOpenBody()    /*HtmlBlockOpenBody : '<' Spnl ('body'  \i ) Spnl  HtmlAttributes  '>';*/
        {

           var result=And(()=>  
                     Char('<')
                  && Spnl()
                  && IChar('b','o','d','y')
                  && Spnl()
                  && HtmlAttributes()
                  && Char('>') ); return result;
		}
        public bool HtmlBlockCloseBody()    /*HtmlBlockCloseBody  : '<' Spnl '/' ('body' \i) Spnl '>';*/
        {

           var result=And(()=>  
                     Char('<')
                  && Spnl()
                  && Char('/')
                  && IChar('b','o','d','y')
                  && Spnl()
                  && Char('>') ); return result;
		}
        public bool HtmlBlockBody()    /*^^HtmlBlockBody : HtmlBlockOpenBody ( &HtmlBlockCloseBody /HtmlBlock+) @HtmlBlockCloseBody ;*/
        {

           var result= TreeNT((int)EHtml.HtmlBlockBody,()=>
                And(()=>  
                     HtmlBlockOpenBody()
                  && (    
                         Peek(()=> HtmlBlockCloseBody() )
                      || PlusRepeat(()=> HtmlBlock() ))
                  && (    
                         HtmlBlockCloseBody()
                      || Fatal("<<HtmlBlockCloseBody>> expected")) ) ); return result;
		}
        public bool HtmlBlockOpenHeader()    /*HtmlBlockOpenHeader : '<' Spnl ('header'  \i )  !(LiteralChar) Spnl HtmlAttributes '>';*/
        {

           var result=And(()=>  
                     Char('<')
                  && Spnl()
                  && IChar('h','e','a','d','e','r')
                  && Not(()=> LiteralChar() )
                  && Spnl()
                  && HtmlAttributes()
                  && Char('>') ); return result;
		}
        public bool HtmlBlockCloseHeader()    /*HtmlBlockCloseHeader : '<' Spnl '/' ('header'  \i ) Spnl '>';*/
        {

           var result=And(()=>  
                     Char('<')
                  && Spnl()
                  && Char('/')
                  && IChar('h','e','a','d','e','r')
                  && Spnl()
                  && Char('>') ); return result;
		}
        public bool HtmlBlockHeader()    /*^^HtmlBlockHeader : HtmlBlockOpenHeader (&HtmlBlockCloseHeader/ HtmlBlock+) @HtmlBlockCloseHeader ;*/
        {

           var result= TreeNT((int)EHtml.HtmlBlockHeader,()=>
                And(()=>  
                     HtmlBlockOpenHeader()
                  && (    
                         Peek(()=> HtmlBlockCloseHeader() )
                      || PlusRepeat(()=> HtmlBlock() ))
                  && (    
                         HtmlBlockCloseHeader()
                      || Fatal("<<HtmlBlockCloseHeader>> expected")) ) ); return result;
		}
        public bool HtmlBlockOpenUnknown()    /*HtmlBlockOpenUnknown : '<' Spnl ![>/] !HtmlBlockSelfClosingType UnknownTagName Spnl  HtmlAttributes '>';*/
        {

           var result=And(()=>  
                     Char('<')
                  && Spnl()
                  && Not(()=> OneOf(">/") )
                  && Not(()=> HtmlBlockSelfClosingType() )
                  && UnknownTagName()
                  && Spnl()
                  && HtmlAttributes()
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
        public bool HtmlBlockInTags()    /*^^HtmlBlockInTags : 
				HtmlBlockHtml
				/HtmlBlockAddress
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
				/ HtmlBlockHeader
                / HtmlBlockHead 
				/ HtmlBlockA
				/ HtmlBlockCode
				/ HtmlBlockSpan
				/ HtmlBlockBody
		        / HtmlBlockUnknown
				;*/
        {

           var result= TreeNT((int)EHtml.HtmlBlockInTags,()=>
                  
                     HtmlBlockHtml()
                  || HtmlBlockAddress()
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
                  || HtmlBlockHeader()
                  || HtmlBlockHead()
                  || HtmlBlockA()
                  || HtmlBlockCode()
                  || HtmlBlockSpan()
                  || HtmlBlockBody()
                  || HtmlBlockUnknown() ); return result;
		}
        public bool HtmlBlock()    /*^^HtmlBlock :(Spnl (HtmlComment/ HtmlBlockInTags  / HtmlBlockSelfClosing /UnknownHtmlBlockSelfClosing )Spnl)/InnerPlain ;*/
        {

           var result= TreeNT((int)EHtml.HtmlBlock,()=>
                  
                     And(()=>    
                         Spnl()
                      && (      
                               HtmlComment()
                            || HtmlBlockInTags()
                            || HtmlBlockSelfClosing()
                            || UnknownHtmlBlockSelfClosing())
                      && Spnl() )
                  || InnerPlain() ); return result;
		}
        public bool UnknownHtmlBlockSelfClosing()    /*^^UnknownHtmlBlockSelfClosing : '<' Spnl (  ( !'/' !HtmlBlockSelfClosingType !SingleHtmlBlockType (LiteralChar)*  Spnl HtmlAttributes '/')/ ( !'/'!HtmlBlockSelfClosingType !SingleHtmlBlockType (LiteralChar)*  Spnl HtmlAttributes '/'?) ) Spnl '>';*/
        {

           var result= TreeNT((int)EHtml.UnknownHtmlBlockSelfClosing,()=>
                And(()=>  
                     Char('<')
                  && Spnl()
                  && (    
                         And(()=>      
                               Not(()=> Char('/') )
                            && Not(()=> HtmlBlockSelfClosingType() )
                            && Not(()=> SingleHtmlBlockType() )
                            && OptRepeat(()=> LiteralChar() )
                            && Spnl()
                            && HtmlAttributes()
                            && Char('/') )
                      || And(()=>      
                               Not(()=> Char('/') )
                            && Not(()=> HtmlBlockSelfClosingType() )
                            && Not(()=> SingleHtmlBlockType() )
                            && OptRepeat(()=> LiteralChar() )
                            && Spnl()
                            && HtmlAttributes()
                            && Option(()=> Char('/') ) ))
                  && Spnl()
                  && Char('>') ) ); return result;
		}
        public bool HtmlBlockSelfClosing()    /*^^HtmlBlockSelfClosing : '<' Spnl ( (SingleHtmlBlockType  Spnl HtmlAttributes '/')/ (HtmlBlockSelfClosingType  Spnl HtmlAttributes '/'?) ) Spnl '>';*/
        {

           var result= TreeNT((int)EHtml.HtmlBlockSelfClosing,()=>
                And(()=>  
                     Char('<')
                  && Spnl()
                  && (    
                         And(()=>      
                               SingleHtmlBlockType()
                            && Spnl()
                            && HtmlAttributes()
                            && Char('/') )
                      || And(()=>      
                               HtmlBlockSelfClosingType()
                            && Spnl()
                            && HtmlAttributes()
                            && Option(()=> Char('/') ) ))
                  && Spnl()
                  && Char('>') ) ); return result;
		}
        public bool SingleHtmlBlockType()    /*^^SingleHtmlBlockType : 'address'\i / 'blockquote'\i / 'center' \i/ 'dir'\i / 'div'\i / 'dl' \i/ 'fieldset' \i/ 'form'\i / 'h1'\i / 'h2'\i / 'h3' \i/
                'h4' \i/ 'h5' \i/ 'h6' \i/ 'isindex'\i / 'menu' \i/ 'noframes' \i/ 'noscript'\i / 'ol' \i/ 'p'\i  !(LiteralChar) / 'pre' \i/ 'table'\i /
                'ul' \i/ 'dd'\i / 'dt'\i / 'frameset' \i/ 'tbody'\i / 'td' \i/ 'tfoot' \i/ 'th'\i / 'thead'\i / 'tr'\i / 'script'\i /
                 'a'  \i !(LiteralChar) /'span'  \i  ;*/
        {

           var result= TreeNT((int)EHtml.SingleHtmlBlockType,()=>
                  
                     IChar('a','d','d','r','e','s','s')
                  || IChar("blockquote")
                  || IChar('c','e','n','t','e','r')
                  || IChar('d','i','r')
                  || IChar('d','i','v')
                  || IChar('d','l')
                  || IChar("fieldset")
                  || IChar('f','o','r','m')
                  || IChar('h','1')
                  || IChar('h','2')
                  || IChar('h','3')
                  || IChar('h','4')
                  || IChar('h','5')
                  || IChar('h','6')
                  || IChar('i','s','i','n','d','e','x')
                  || IChar('m','e','n','u')
                  || IChar("noframes")
                  || IChar("noscript")
                  || IChar('o','l')
                  || And(()=>    IChar('p') && Not(()=> LiteralChar() ) )
                  || IChar('p','r','e')
                  || IChar('t','a','b','l','e')
                  || IChar('u','l')
                  || IChar('d','d')
                  || IChar('d','t')
                  || IChar("frameset")
                  || IChar('t','b','o','d','y')
                  || IChar('t','d')
                  || IChar('t','f','o','o','t')
                  || IChar('t','h')
                  || IChar('t','h','e','a','d')
                  || IChar('t','r')
                  || IChar('s','c','r','i','p','t')
                  || And(()=>    IChar('a') && Not(()=> LiteralChar() ) )
                  || IChar('s','p','a','n') ); return result;
		}
        public bool HtmlBlockSelfClosingType()    /*^^HtmlBlockSelfClosingType:  'br' \i / 'hr'  /'meta' \i /'input' \i / 'img' \i /'image' \i  /'link' \i;*/
        {

           var result= TreeNT((int)EHtml.HtmlBlockSelfClosingType,()=>
                  
                     IChar('b','r')
                  || Char('h','r')
                  || IChar('m','e','t','a')
                  || IChar('i','n','p','u','t')
                  || IChar('i','m','g')
                  || IChar('i','m','a','g','e')
                  || IChar('l','i','n','k') ); return result;
		}
        public bool StyleOpen()    /*StyleOpen :     '<' Spnl ('style'  \i ) Spnl HtmlAttributes '>'  ;*/
        {

           var result=And(()=>  
                     Char('<')
                  && Spnl()
                  && IChar('s','t','y','l','e')
                  && Spnl()
                  && HtmlAttributes()
                  && Char('>') ); return result;
		}
        public bool StyleClose()    /*StyleClose :    '<' Spnl '/' ('style'  \i ) Spnl '>' ;*/
        {

           var result=And(()=>  
                     Char('<')
                  && Spnl()
                  && Char('/')
                  && IChar('s','t','y','l','e')
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
        public bool QuotedContents()    /*^^QuotedContents :         ('\'' (!'\'' .)* '\''  /    '\"' (!'\"' .)* '\"' /  '\"' QuotedContents '\"'  / '\'' QuotedContents '\'')* ;*/
        {

           var result= TreeNT((int)EHtml.QuotedContents,()=>
                OptRepeat(()=>  
                      
                         And(()=>      
                               Char('\'')
                            && OptRepeat(()=>        
                                    And(()=>    Not(()=> Char('\'') ) && Any() ) )
                            && Char('\'') )
                      || And(()=>      
                               Char('\"')
                            && OptRepeat(()=>        
                                    And(()=>    Not(()=> Char('\"') ) && Any() ) )
                            && Char('\"') )
                      || And(()=>      
                               Char('\"')
                            && QuotedContents()
                            && Char('\"') )
                      || And(()=>      
                               Char('\'')
                            && QuotedContents()
                            && Char('\'') ) ) ); return result;
		}
        public bool Quoted()    /*^^Quoted :       '\"' QuotedContents '\"'  / '\'' QuotedContents '\'' ;*/
        {

           var result= TreeNT((int)EHtml.Quoted,()=>
                  
                     And(()=>    
                         Char('\"')
                      && QuotedContents()
                      && Char('\"') )
                  || And(()=>    
                         Char('\'')
                      && QuotedContents()
                      && Char('\'') ) ); return result;
		}
        public bool GlobalAttributes()    /*^^GlobalAttributes :  'itemscope' /'readonly' /'hidden'; 

//到底哪些合法*/
        {

           var result= TreeNT((int)EHtml.GlobalAttributes,()=>
                  
                     Char("itemscope")
                  || Char("readonly")
                  || Char('h','i','d','d','e','n') ); return result;
		}
        public bool HtmlAttributeHeader()    /*HtmlAttributeHeader: AlphanumericAscii / '-'/'.'/':'/'_' ;*/
        {

           var result=  
                     AlphanumericAscii()
                  || Char('-')
                  || Char('.')
                  || Char(':')
                  || Char('_'); return result;
		}
        public bool ExpectedAttributeHeaderEnd()    /*ExpectedAttributeHeaderEnd:  Spnl '=' /&GlobalAttributes;*/
        {

           var result=  
                     And(()=>    Spnl() && Char('=') )
                  || Peek(()=> GlobalAttributes() ); return result;
		}
        public bool ExpectedAttributeValue()    /*ExpectedAttributeValue:   (Quoted / (!'>' Nonspacechar)+);*/
        {

           var result=  
                     Quoted()
                  || PlusRepeat(()=>    
                      And(()=>    Not(()=> Char('>') ) && Nonspacechar() ) ); return result;
		}
        public bool HtmlAttributes()    /*^^HtmlAttributes: HtmlAttribute*;*/
        {

           var result= TreeNT((int)EHtml.HtmlAttributes,()=>
                OptRepeat(()=> HtmlAttribute() ) ); return result;
		}
        public bool HtmlAttribute()    /*^^HtmlAttribute : ( GlobalAttributes Spnl!'=' /(HtmlAttributeHeader )+ (ExpectedAttributeHeaderEnd Spnl @ExpectedAttributeValue  ) ) Spnl ;*/
        {

           var result= TreeNT((int)EHtml.HtmlAttribute,()=>
                And(()=>  
                     (    
                         And(()=>      
                               GlobalAttributes()
                            && Spnl()
                            && Not(()=> Char('=') ) )
                      || And(()=>      
                               PlusRepeat(()=> HtmlAttributeHeader() )
                            && And(()=>        
                                       ExpectedAttributeHeaderEnd()
                                    && Spnl()
                                    && (          
                                                 ExpectedAttributeValue()
                                              || Fatal("<<ExpectedAttributeValue>> expected")) ) ))
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
        public bool HtmlTag()    /*HtmlTag :       '<' Spnl '/'? AlphanumericAscii+ Spnl HtmlAttributes '/'? Spnl '>';*/
        {

           var result=And(()=>  
                     Char('<')
                  && Spnl()
                  && Option(()=> Char('/') )
                  && PlusRepeat(()=> AlphanumericAscii() )
                  && Spnl()
                  && HtmlAttributes()
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
        public bool InnerPlain()    /*^^InnerPlain :  PurePlainContents/LeftPlainContents/RightPlainContents ;*/
        {

           var result= TreeNT((int)EHtml.InnerPlain,()=>
                  
                     PurePlainContents()
                  || LeftPlainContents()
                  || RightPlainContents() ); return result;
		}
        public bool PurePlainContents()    /*^^PurePlainContents :  ( ![<>] .)+ ;*/
        {

           var result= TreeNT((int)EHtml.PurePlainContents,()=>
                PlusRepeat(()=>  
                  And(()=>    Not(()=> OneOf("<>") ) && Any() ) ) ); return result;
		}
        public bool LeftPlainContents()    /*^^LeftPlainContents : PurePlainContents?'<' PurePlainContents? !'>' ;*/
        {

           var result= TreeNT((int)EHtml.LeftPlainContents,()=>
                And(()=>  
                     Option(()=> PurePlainContents() )
                  && Char('<')
                  && Option(()=> PurePlainContents() )
                  && Not(()=> Char('>') ) ) ); return result;
		}
        public bool RightPlainContents()    /*^^RightPlainContents : PurePlainContents? '>' PurePlainContents?  ;*/
        {

           var result= TreeNT((int)EHtml.RightPlainContents,()=>
                And(()=>  
                     Option(()=> PurePlainContents() )
                  && Char('>')
                  && Option(()=> PurePlainContents() ) ) ); return result;
		}
        public bool Eof()    /*Eof :          !./ FATAL <" end of file">;*/
        {

           var result=    Not(()=> Any() ) || Fatal(" end of file"); return result;
		}
		#endregion Grammar Rules
   }
}