/* created on 07/12/2016 00:03:53 from peg generator V1.0 using 'Html' as input*/

using Peg.Base;
using System;
using System.IO;
using System.Text;
namespace Html
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
                  HtmlBlockCloseHead= 103, HtmlBlockHead= 104, HtmlBlockInTags= 105, 
                  HtmlBlock= 106, HtmlBlockSelfClosing= 107, HtmlBlockType= 108, 
                  StyleOpen= 109, StyleClose= 110, InStyleTags= 111, StyleBlock= 112, 
                  Space= 113, RawHtml= 114, BlankLine= 115, Quoted= 116, HtmlAttribute= 117, 
                  HtmlComment= 118, HtmlTag= 119, Spacechar= 120, Nonspacechar= 121, 
                  Newline= 122, Sp= 123, Spnl= 124, AlphanumericAscii= 125, Eof= 126};
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

           return TreeNT((int)EHtml.Doc,()=>
                And(()=>    OptRepeat(()=> Block() ) && Eof() ) );
		}
        public bool Block()    /*^^Block :     BlankLine*
            ( HtmlBlock / StyleBlock);


// Parsers for different kinds of block-level HTML content.
// This is repetitive due to constraints of PEG grammar.*/
        {

           return TreeNT((int)EHtml.Block,()=>
                And(()=>  
                     OptRepeat(()=> BlankLine() )
                  && (    HtmlBlock() || StyleBlock()) ) );
		}
        public bool HtmlBlockOpenAddress()    /*HtmlBlockOpenAddress : '<' Spnl ('address' / 'ADDRESS') Spnl HtmlAttribute* '>';*/
        {

           return And(()=>  
                     Char('<')
                  && Spnl()
                  && (    
                         Char('a','d','d','r','e','s','s')
                      || Char('A','D','D','R','E','S','S'))
                  && Spnl()
                  && OptRepeat(()=> HtmlAttribute() )
                  && Char('>') );
		}
        public bool HtmlBlockCloseAddress()    /*HtmlBlockCloseAddress : '<' Spnl '/' ('address' / 'ADDRESS') Spnl '>' ;*/
        {

           return And(()=>  
                     Char('<')
                  && Spnl()
                  && Char('/')
                  && (    
                         Char('a','d','d','r','e','s','s')
                      || Char('A','D','D','R','E','S','S'))
                  && Spnl()
                  && Char('>') );
		}
        public bool HtmlBlockAddress()    /*^^HtmlBlockAddress : HtmlBlockOpenAddress (HtmlBlockAddress / !HtmlBlockCloseAddress .)* HtmlBlockCloseAddress;*/
        {

           return TreeNT((int)EHtml.HtmlBlockAddress,()=>
                And(()=>  
                     HtmlBlockOpenAddress()
                  && OptRepeat(()=>    
                            
                               HtmlBlockAddress()
                            || And(()=>        
                                       Not(()=> HtmlBlockCloseAddress() )
                                    && Any() ) )
                  && HtmlBlockCloseAddress() ) );
		}
        public bool HtmlBlockOpenBlockquote()    /*HtmlBlockOpenBlockquote : '<' Spnl ('blockquote' / 'BLOCKQUOTE') Spnl HtmlAttribute* '>';*/
        {

           return And(()=>  
                     Char('<')
                  && Spnl()
                  && (    Char("blockquote") || Char("BLOCKQUOTE"))
                  && Spnl()
                  && OptRepeat(()=> HtmlAttribute() )
                  && Char('>') );
		}
        public bool HtmlBlockCloseBlockquote()    /*HtmlBlockCloseBlockquote : '<' Spnl '/' ('blockquote' / 'BLOCKQUOTE') Spnl '>';*/
        {

           return And(()=>  
                     Char('<')
                  && Spnl()
                  && Char('/')
                  && (    Char("blockquote") || Char("BLOCKQUOTE"))
                  && Spnl()
                  && Char('>') );
		}
        public bool HtmlBlockBlockquote()    /*^^HtmlBlockBlockquote : HtmlBlockOpenBlockquote (HtmlBlockBlockquote / !HtmlBlockCloseBlockquote .)* HtmlBlockCloseBlockquote;*/
        {

           return TreeNT((int)EHtml.HtmlBlockBlockquote,()=>
                And(()=>  
                     HtmlBlockOpenBlockquote()
                  && OptRepeat(()=>    
                            
                               HtmlBlockBlockquote()
                            || And(()=>        
                                       Not(()=> HtmlBlockCloseBlockquote() )
                                    && Any() ) )
                  && HtmlBlockCloseBlockquote() ) );
		}
        public bool HtmlBlockOpenCenter()    /*HtmlBlockOpenCenter : '<' Spnl ('center' / 'CENTER') Spnl HtmlAttribute* '>';*/
        {

           return And(()=>  
                     Char('<')
                  && Spnl()
                  && (    
                         Char('c','e','n','t','e','r')
                      || Char('C','E','N','T','E','R'))
                  && Spnl()
                  && OptRepeat(()=> HtmlAttribute() )
                  && Char('>') );
		}
        public bool HtmlBlockCloseCenter()    /*HtmlBlockCloseCenter : '<' Spnl '/' ('center' / 'CENTER') Spnl '>';*/
        {

           return And(()=>  
                     Char('<')
                  && Spnl()
                  && Char('/')
                  && (    
                         Char('c','e','n','t','e','r')
                      || Char('C','E','N','T','E','R'))
                  && Spnl()
                  && Char('>') );
		}
        public bool HtmlBlockCenter()    /*^^HtmlBlockCenter : HtmlBlockOpenCenter (HtmlBlockCenter / !HtmlBlockCloseCenter .)* HtmlBlockCloseCenter;*/
        {

           return TreeNT((int)EHtml.HtmlBlockCenter,()=>
                And(()=>  
                     HtmlBlockOpenCenter()
                  && OptRepeat(()=>    
                            
                               HtmlBlockCenter()
                            || And(()=>        
                                       Not(()=> HtmlBlockCloseCenter() )
                                    && Any() ) )
                  && HtmlBlockCloseCenter() ) );
		}
        public bool HtmlBlockOpenDir()    /*HtmlBlockOpenDir : '<' Spnl ('dir' / 'DIR') Spnl HtmlAttribute* '>';*/
        {

           return And(()=>  
                     Char('<')
                  && Spnl()
                  && (    Char('d','i','r') || Char('D','I','R'))
                  && Spnl()
                  && OptRepeat(()=> HtmlAttribute() )
                  && Char('>') );
		}
        public bool HtmlBlockCloseDir()    /*HtmlBlockCloseDir : '<' Spnl '/' ('dir' / 'DIR') Spnl '>';*/
        {

           return And(()=>  
                     Char('<')
                  && Spnl()
                  && Char('/')
                  && (    Char('d','i','r') || Char('D','I','R'))
                  && Spnl()
                  && Char('>') );
		}
        public bool HtmlBlockDir()    /*^^HtmlBlockDir : HtmlBlockOpenDir (HtmlBlockDir / !HtmlBlockCloseDir .)* HtmlBlockCloseDir;*/
        {

           return TreeNT((int)EHtml.HtmlBlockDir,()=>
                And(()=>  
                     HtmlBlockOpenDir()
                  && OptRepeat(()=>    
                            
                               HtmlBlockDir()
                            || And(()=>        
                                       Not(()=> HtmlBlockCloseDir() )
                                    && Any() ) )
                  && HtmlBlockCloseDir() ) );
		}
        public bool HtmlBlockOpenDiv()    /*^^HtmlBlockOpenDiv : '<' Spnl ('div' / 'DIV') Spnl HtmlAttribute* '>';*/
        {

           return TreeNT((int)EHtml.HtmlBlockOpenDiv,()=>
                And(()=>  
                     Char('<')
                  && Spnl()
                  && (    Char('d','i','v') || Char('D','I','V'))
                  && Spnl()
                  && OptRepeat(()=> HtmlAttribute() )
                  && Char('>') ) );
		}
        public bool HtmlBlockCloseDiv()    /*^^HtmlBlockCloseDiv : '<' Spnl '/' ('div' / 'DIV') Spnl '>';*/
        {

           return TreeNT((int)EHtml.HtmlBlockCloseDiv,()=>
                And(()=>  
                     Char('<')
                  && Spnl()
                  && Char('/')
                  && (    Char('d','i','v') || Char('D','I','V'))
                  && Spnl()
                  && Char('>') ) );
		}
        public bool HtmlBlockDiv()    /*^^HtmlBlockDiv : HtmlBlockOpenDiv (HtmlBlockDiv / !HtmlBlockCloseDiv .)* HtmlBlockCloseDiv;*/
        {

           return TreeNT((int)EHtml.HtmlBlockDiv,()=>
                And(()=>  
                     HtmlBlockOpenDiv()
                  && OptRepeat(()=>    
                            
                               HtmlBlockDiv()
                            || And(()=>        
                                       Not(()=> HtmlBlockCloseDiv() )
                                    && Any() ) )
                  && HtmlBlockCloseDiv() ) );
		}
        public bool HtmlBlockOpenDl()    /*HtmlBlockOpenDl : '<' Spnl ('dl' / 'DL') Spnl HtmlAttribute* '>';*/
        {

           return And(()=>  
                     Char('<')
                  && Spnl()
                  && (    Char('d','l') || Char('D','L'))
                  && Spnl()
                  && OptRepeat(()=> HtmlAttribute() )
                  && Char('>') );
		}
        public bool HtmlBlockCloseDl()    /*HtmlBlockCloseDl : '<' Spnl '/' ('dl' / 'DL') Spnl '>';*/
        {

           return And(()=>  
                     Char('<')
                  && Spnl()
                  && Char('/')
                  && (    Char('d','l') || Char('D','L'))
                  && Spnl()
                  && Char('>') );
		}
        public bool HtmlBlockDl()    /*^^HtmlBlockDl : HtmlBlockOpenDl (HtmlBlockDl / !HtmlBlockCloseDl .)* HtmlBlockCloseDl;*/
        {

           return TreeNT((int)EHtml.HtmlBlockDl,()=>
                And(()=>  
                     HtmlBlockOpenDl()
                  && OptRepeat(()=>    
                            
                               HtmlBlockDl()
                            || And(()=>    Not(()=> HtmlBlockCloseDl() ) && Any() ) )
                  && HtmlBlockCloseDl() ) );
		}
        public bool HtmlBlockOpenFieldset()    /*HtmlBlockOpenFieldset : '<' Spnl ('fieldset' / 'FIELDSET') Spnl HtmlAttribute* '>';*/
        {

           return And(()=>  
                     Char('<')
                  && Spnl()
                  && (    Char("fieldset") || Char("FIELDSET"))
                  && Spnl()
                  && OptRepeat(()=> HtmlAttribute() )
                  && Char('>') );
		}
        public bool HtmlBlockCloseFieldset()    /*HtmlBlockCloseFieldset : '<' Spnl '/' ('fieldset' / 'FIELDSET') Spnl '>';*/
        {

           return And(()=>  
                     Char('<')
                  && Spnl()
                  && Char('/')
                  && (    Char("fieldset") || Char("FIELDSET"))
                  && Spnl()
                  && Char('>') );
		}
        public bool HtmlBlockFieldset()    /*^^HtmlBlockFieldset : HtmlBlockOpenFieldset (HtmlBlockFieldset / !HtmlBlockCloseFieldset .)* HtmlBlockCloseFieldset;*/
        {

           return TreeNT((int)EHtml.HtmlBlockFieldset,()=>
                And(()=>  
                     HtmlBlockOpenFieldset()
                  && OptRepeat(()=>    
                            
                               HtmlBlockFieldset()
                            || And(()=>        
                                       Not(()=> HtmlBlockCloseFieldset() )
                                    && Any() ) )
                  && HtmlBlockCloseFieldset() ) );
		}
        public bool HtmlBlockOpenForm()    /*HtmlBlockOpenForm : '<' Spnl ('form' / 'FORM') Spnl HtmlAttribute* '>';*/
        {

           return And(()=>  
                     Char('<')
                  && Spnl()
                  && (    Char('f','o','r','m') || Char('F','O','R','M'))
                  && Spnl()
                  && OptRepeat(()=> HtmlAttribute() )
                  && Char('>') );
		}
        public bool HtmlBlockCloseForm()    /*HtmlBlockCloseForm : '<' Spnl '/' ('form' / 'FORM') Spnl '>';*/
        {

           return And(()=>  
                     Char('<')
                  && Spnl()
                  && Char('/')
                  && (    Char('f','o','r','m') || Char('F','O','R','M'))
                  && Spnl()
                  && Char('>') );
		}
        public bool HtmlBlockForm()    /*^^HtmlBlockForm : HtmlBlockOpenForm (HtmlBlockForm / !HtmlBlockCloseForm .)* HtmlBlockCloseForm;*/
        {

           return TreeNT((int)EHtml.HtmlBlockForm,()=>
                And(()=>  
                     HtmlBlockOpenForm()
                  && OptRepeat(()=>    
                            
                               HtmlBlockForm()
                            || And(()=>        
                                       Not(()=> HtmlBlockCloseForm() )
                                    && Any() ) )
                  && HtmlBlockCloseForm() ) );
		}
        public bool HtmlBlockOpenH1()    /*HtmlBlockOpenH1 : '<' Spnl ('h1' / 'H1') Spnl HtmlAttribute* '>';*/
        {

           return And(()=>  
                     Char('<')
                  && Spnl()
                  && (    Char('h','1') || Char('H','1'))
                  && Spnl()
                  && OptRepeat(()=> HtmlAttribute() )
                  && Char('>') );
		}
        public bool HtmlBlockCloseH1()    /*HtmlBlockCloseH1 : '<' Spnl '/' ('h1' / 'H1') Spnl '>';*/
        {

           return And(()=>  
                     Char('<')
                  && Spnl()
                  && Char('/')
                  && (    Char('h','1') || Char('H','1'))
                  && Spnl()
                  && Char('>') );
		}
        public bool HtmlBlockH1()    /*^^HtmlBlockH1 : HtmlBlockOpenH1 (HtmlBlockH1 / !HtmlBlockCloseH1 .)* HtmlBlockCloseH1;*/
        {

           return TreeNT((int)EHtml.HtmlBlockH1,()=>
                And(()=>  
                     HtmlBlockOpenH1()
                  && OptRepeat(()=>    
                            
                               HtmlBlockH1()
                            || And(()=>    Not(()=> HtmlBlockCloseH1() ) && Any() ) )
                  && HtmlBlockCloseH1() ) );
		}
        public bool HtmlBlockOpenH2()    /*HtmlBlockOpenH2 : '<' Spnl ('h2' / 'H2') Spnl HtmlAttribute* '>';*/
        {

           return And(()=>  
                     Char('<')
                  && Spnl()
                  && (    Char('h','2') || Char('H','2'))
                  && Spnl()
                  && OptRepeat(()=> HtmlAttribute() )
                  && Char('>') );
		}
        public bool HtmlBlockCloseH2()    /*HtmlBlockCloseH2 : '<' Spnl '/' ('h2' / 'H2') Spnl '>';*/
        {

           return And(()=>  
                     Char('<')
                  && Spnl()
                  && Char('/')
                  && (    Char('h','2') || Char('H','2'))
                  && Spnl()
                  && Char('>') );
		}
        public bool HtmlBlockH2()    /*^^HtmlBlockH2 : HtmlBlockOpenH2 (HtmlBlockH2 / !HtmlBlockCloseH2 .)* HtmlBlockCloseH2;*/
        {

           return TreeNT((int)EHtml.HtmlBlockH2,()=>
                And(()=>  
                     HtmlBlockOpenH2()
                  && OptRepeat(()=>    
                            
                               HtmlBlockH2()
                            || And(()=>    Not(()=> HtmlBlockCloseH2() ) && Any() ) )
                  && HtmlBlockCloseH2() ) );
		}
        public bool HtmlBlockOpenH3()    /*HtmlBlockOpenH3 : '<' Spnl ('h3' / 'H3') Spnl HtmlAttribute* '>';*/
        {

           return And(()=>  
                     Char('<')
                  && Spnl()
                  && (    Char('h','3') || Char('H','3'))
                  && Spnl()
                  && OptRepeat(()=> HtmlAttribute() )
                  && Char('>') );
		}
        public bool HtmlBlockCloseH3()    /*HtmlBlockCloseH3 : '<' Spnl '/' ('h3' / 'H3') Spnl '>';*/
        {

           return And(()=>  
                     Char('<')
                  && Spnl()
                  && Char('/')
                  && (    Char('h','3') || Char('H','3'))
                  && Spnl()
                  && Char('>') );
		}
        public bool HtmlBlockH3()    /*^^HtmlBlockH3 : HtmlBlockOpenH3 (HtmlBlockH3 / !HtmlBlockCloseH3 .)* HtmlBlockCloseH3;*/
        {

           return TreeNT((int)EHtml.HtmlBlockH3,()=>
                And(()=>  
                     HtmlBlockOpenH3()
                  && OptRepeat(()=>    
                            
                               HtmlBlockH3()
                            || And(()=>    Not(()=> HtmlBlockCloseH3() ) && Any() ) )
                  && HtmlBlockCloseH3() ) );
		}
        public bool HtmlBlockOpenH4()    /*HtmlBlockOpenH4 : '<' Spnl ('h4' / 'H4') Spnl HtmlAttribute* '>';*/
        {

           return And(()=>  
                     Char('<')
                  && Spnl()
                  && (    Char('h','4') || Char('H','4'))
                  && Spnl()
                  && OptRepeat(()=> HtmlAttribute() )
                  && Char('>') );
		}
        public bool HtmlBlockCloseH4()    /*HtmlBlockCloseH4 : '<' Spnl '/' ('h4' / 'H4') Spnl '>';*/
        {

           return And(()=>  
                     Char('<')
                  && Spnl()
                  && Char('/')
                  && (    Char('h','4') || Char('H','4'))
                  && Spnl()
                  && Char('>') );
		}
        public bool HtmlBlockH4()    /*HtmlBlockH4 : HtmlBlockOpenH4 (HtmlBlockH4 / !HtmlBlockCloseH4 .)* HtmlBlockCloseH4;*/
        {

           return And(()=>  
                     HtmlBlockOpenH4()
                  && OptRepeat(()=>    
                            
                               HtmlBlockH4()
                            || And(()=>    Not(()=> HtmlBlockCloseH4() ) && Any() ) )
                  && HtmlBlockCloseH4() );
		}
        public bool HtmlBlockOpenH5()    /*HtmlBlockOpenH5 : '<' Spnl ('h5' / 'H5') Spnl HtmlAttribute* '>';*/
        {

           return And(()=>  
                     Char('<')
                  && Spnl()
                  && (    Char('h','5') || Char('H','5'))
                  && Spnl()
                  && OptRepeat(()=> HtmlAttribute() )
                  && Char('>') );
		}
        public bool HtmlBlockCloseH5()    /*HtmlBlockCloseH5 : '<' Spnl '/' ('h5' / 'H5') Spnl '>';*/
        {

           return And(()=>  
                     Char('<')
                  && Spnl()
                  && Char('/')
                  && (    Char('h','5') || Char('H','5'))
                  && Spnl()
                  && Char('>') );
		}
        public bool HtmlBlockH5()    /*^^HtmlBlockH5 : HtmlBlockOpenH5 (HtmlBlockH5 / !HtmlBlockCloseH5 .)* HtmlBlockCloseH5;*/
        {

           return TreeNT((int)EHtml.HtmlBlockH5,()=>
                And(()=>  
                     HtmlBlockOpenH5()
                  && OptRepeat(()=>    
                            
                               HtmlBlockH5()
                            || And(()=>    Not(()=> HtmlBlockCloseH5() ) && Any() ) )
                  && HtmlBlockCloseH5() ) );
		}
        public bool HtmlBlockOpenH6()    /*HtmlBlockOpenH6 : '<' Spnl ('h6' / 'H6') Spnl HtmlAttribute* '>';*/
        {

           return And(()=>  
                     Char('<')
                  && Spnl()
                  && (    Char('h','6') || Char('H','6'))
                  && Spnl()
                  && OptRepeat(()=> HtmlAttribute() )
                  && Char('>') );
		}
        public bool HtmlBlockCloseH6()    /*HtmlBlockCloseH6 : '<' Spnl '/' ('h6' / 'H6') Spnl '>';*/
        {

           return And(()=>  
                     Char('<')
                  && Spnl()
                  && Char('/')
                  && (    Char('h','6') || Char('H','6'))
                  && Spnl()
                  && Char('>') );
		}
        public bool HtmlBlockH6()    /*^^HtmlBlockH6 : HtmlBlockOpenH6 (HtmlBlockH6 / !HtmlBlockCloseH6 .)* HtmlBlockCloseH6;*/
        {

           return TreeNT((int)EHtml.HtmlBlockH6,()=>
                And(()=>  
                     HtmlBlockOpenH6()
                  && OptRepeat(()=>    
                            
                               HtmlBlockH6()
                            || And(()=>    Not(()=> HtmlBlockCloseH6() ) && Any() ) )
                  && HtmlBlockCloseH6() ) );
		}
        public bool HtmlBlockOpenMenu()    /*HtmlBlockOpenMenu : '<' Spnl ('menu' / 'MENU') Spnl HtmlAttribute* '>';*/
        {

           return And(()=>  
                     Char('<')
                  && Spnl()
                  && (    Char('m','e','n','u') || Char('M','E','N','U'))
                  && Spnl()
                  && OptRepeat(()=> HtmlAttribute() )
                  && Char('>') );
		}
        public bool HtmlBlockCloseMenu()    /*HtmlBlockCloseMenu : '<' Spnl '/' ('menu' / 'MENU') Spnl '>';*/
        {

           return And(()=>  
                     Char('<')
                  && Spnl()
                  && Char('/')
                  && (    Char('m','e','n','u') || Char('M','E','N','U'))
                  && Spnl()
                  && Char('>') );
		}
        public bool HtmlBlockMenu()    /*^^HtmlBlockMenu : HtmlBlockOpenMenu (HtmlBlockMenu / !HtmlBlockCloseMenu .)* HtmlBlockCloseMenu;*/
        {

           return TreeNT((int)EHtml.HtmlBlockMenu,()=>
                And(()=>  
                     HtmlBlockOpenMenu()
                  && OptRepeat(()=>    
                            
                               HtmlBlockMenu()
                            || And(()=>        
                                       Not(()=> HtmlBlockCloseMenu() )
                                    && Any() ) )
                  && HtmlBlockCloseMenu() ) );
		}
        public bool HtmlBlockOpenNoframes()    /*HtmlBlockOpenNoframes : '<' Spnl ('noframes' / 'NOFRAMES') Spnl HtmlAttribute* '>';*/
        {

           return And(()=>  
                     Char('<')
                  && Spnl()
                  && (    Char("noframes") || Char("NOFRAMES"))
                  && Spnl()
                  && OptRepeat(()=> HtmlAttribute() )
                  && Char('>') );
		}
        public bool HtmlBlockCloseNoframes()    /*HtmlBlockCloseNoframes : '<' Spnl '/' ('noframes' / 'NOFRAMES') Spnl '>';*/
        {

           return And(()=>  
                     Char('<')
                  && Spnl()
                  && Char('/')
                  && (    Char("noframes") || Char("NOFRAMES"))
                  && Spnl()
                  && Char('>') );
		}
        public bool HtmlBlockNoframes()    /*^^HtmlBlockNoframes : HtmlBlockOpenNoframes (HtmlBlockNoframes / !HtmlBlockCloseNoframes .)* HtmlBlockCloseNoframes;*/
        {

           return TreeNT((int)EHtml.HtmlBlockNoframes,()=>
                And(()=>  
                     HtmlBlockOpenNoframes()
                  && OptRepeat(()=>    
                            
                               HtmlBlockNoframes()
                            || And(()=>        
                                       Not(()=> HtmlBlockCloseNoframes() )
                                    && Any() ) )
                  && HtmlBlockCloseNoframes() ) );
		}
        public bool HtmlBlockOpenNoscript()    /*HtmlBlockOpenNoscript : '<' Spnl ('noscript' / 'NOSCRIPT') Spnl HtmlAttribute* '>';*/
        {

           return And(()=>  
                     Char('<')
                  && Spnl()
                  && (    Char("noscript") || Char("NOSCRIPT"))
                  && Spnl()
                  && OptRepeat(()=> HtmlAttribute() )
                  && Char('>') );
		}
        public bool HtmlBlockCloseNoscript()    /*HtmlBlockCloseNoscript : '<' Spnl '/' ('noscript' / 'NOSCRIPT') Spnl '>';*/
        {

           return And(()=>  
                     Char('<')
                  && Spnl()
                  && Char('/')
                  && (    Char("noscript") || Char("NOSCRIPT"))
                  && Spnl()
                  && Char('>') );
		}
        public bool HtmlBlockNoscript()    /*^^HtmlBlockNoscript : HtmlBlockOpenNoscript (HtmlBlockNoscript / !HtmlBlockCloseNoscript .)* HtmlBlockCloseNoscript;*/
        {

           return TreeNT((int)EHtml.HtmlBlockNoscript,()=>
                And(()=>  
                     HtmlBlockOpenNoscript()
                  && OptRepeat(()=>    
                            
                               HtmlBlockNoscript()
                            || And(()=>        
                                       Not(()=> HtmlBlockCloseNoscript() )
                                    && Any() ) )
                  && HtmlBlockCloseNoscript() ) );
		}
        public bool HtmlBlockOpenOl()    /*HtmlBlockOpenOl : '<' Spnl ('ol' / 'OL') Spnl HtmlAttribute* '>';*/
        {

           return And(()=>  
                     Char('<')
                  && Spnl()
                  && (    Char('o','l') || Char('O','L'))
                  && Spnl()
                  && OptRepeat(()=> HtmlAttribute() )
                  && Char('>') );
		}
        public bool HtmlBlockCloseOl()    /*HtmlBlockCloseOl : '<' Spnl '/' ('ol' / 'OL') Spnl '>';*/
        {

           return And(()=>  
                     Char('<')
                  && Spnl()
                  && Char('/')
                  && (    Char('o','l') || Char('O','L'))
                  && Spnl()
                  && Char('>') );
		}
        public bool HtmlBlockOl()    /*^^HtmlBlockOl : HtmlBlockOpenOl (HtmlBlockOl / !HtmlBlockCloseOl .)* HtmlBlockCloseOl;*/
        {

           return TreeNT((int)EHtml.HtmlBlockOl,()=>
                And(()=>  
                     HtmlBlockOpenOl()
                  && OptRepeat(()=>    
                            
                               HtmlBlockOl()
                            || And(()=>    Not(()=> HtmlBlockCloseOl() ) && Any() ) )
                  && HtmlBlockCloseOl() ) );
		}
        public bool HtmlBlockOpenP()    /*HtmlBlockOpenP : '<' Spnl ('p' / 'P') Spnl HtmlAttribute* '>';*/
        {

           return And(()=>  
                     Char('<')
                  && Spnl()
                  && (    Char('p') || Char('P'))
                  && Spnl()
                  && OptRepeat(()=> HtmlAttribute() )
                  && Char('>') );
		}
        public bool HtmlBlockCloseP()    /*HtmlBlockCloseP : '<' Spnl '/' ('p' / 'P') Spnl '>';*/
        {

           return And(()=>  
                     Char('<')
                  && Spnl()
                  && Char('/')
                  && (    Char('p') || Char('P'))
                  && Spnl()
                  && Char('>') );
		}
        public bool HtmlBlockP()    /*^^HtmlBlockP : HtmlBlockOpenP (HtmlBlockP / !HtmlBlockCloseP .)* HtmlBlockCloseP;*/
        {

           return TreeNT((int)EHtml.HtmlBlockP,()=>
                And(()=>  
                     HtmlBlockOpenP()
                  && OptRepeat(()=>    
                            
                               HtmlBlockP()
                            || And(()=>    Not(()=> HtmlBlockCloseP() ) && Any() ) )
                  && HtmlBlockCloseP() ) );
		}
        public bool HtmlBlockOpenPre()    /*HtmlBlockOpenPre : '<' Spnl ('pre' / 'PRE') Spnl HtmlAttribute* '>';*/
        {

           return And(()=>  
                     Char('<')
                  && Spnl()
                  && (    Char('p','r','e') || Char('P','R','E'))
                  && Spnl()
                  && OptRepeat(()=> HtmlAttribute() )
                  && Char('>') );
		}
        public bool HtmlBlockClosePre()    /*HtmlBlockClosePre : '<' Spnl '/' ('pre' / 'PRE') Spnl '>';*/
        {

           return And(()=>  
                     Char('<')
                  && Spnl()
                  && Char('/')
                  && (    Char('p','r','e') || Char('P','R','E'))
                  && Spnl()
                  && Char('>') );
		}
        public bool HtmlBlockPre()    /*^^HtmlBlockPre : HtmlBlockOpenPre (HtmlBlockPre / !HtmlBlockClosePre .)* HtmlBlockClosePre;*/
        {

           return TreeNT((int)EHtml.HtmlBlockPre,()=>
                And(()=>  
                     HtmlBlockOpenPre()
                  && OptRepeat(()=>    
                            
                               HtmlBlockPre()
                            || And(()=>        
                                       Not(()=> HtmlBlockClosePre() )
                                    && Any() ) )
                  && HtmlBlockClosePre() ) );
		}
        public bool HtmlBlockOpenTable()    /*HtmlBlockOpenTable : '<' Spnl ('table' / 'TABLE') Spnl HtmlAttribute* '>';*/
        {

           return And(()=>  
                     Char('<')
                  && Spnl()
                  && (    
                         Char('t','a','b','l','e')
                      || Char('T','A','B','L','E'))
                  && Spnl()
                  && OptRepeat(()=> HtmlAttribute() )
                  && Char('>') );
		}
        public bool HtmlBlockCloseTable()    /*HtmlBlockCloseTable : '<' Spnl '/' ('table' / 'TABLE') Spnl '>';*/
        {

           return And(()=>  
                     Char('<')
                  && Spnl()
                  && Char('/')
                  && (    
                         Char('t','a','b','l','e')
                      || Char('T','A','B','L','E'))
                  && Spnl()
                  && Char('>') );
		}
        public bool HtmlBlockTable()    /*^^HtmlBlockTable : HtmlBlockOpenTable (HtmlBlockTable / !HtmlBlockCloseTable .)* HtmlBlockCloseTable;*/
        {

           return TreeNT((int)EHtml.HtmlBlockTable,()=>
                And(()=>  
                     HtmlBlockOpenTable()
                  && OptRepeat(()=>    
                            
                               HtmlBlockTable()
                            || And(()=>        
                                       Not(()=> HtmlBlockCloseTable() )
                                    && Any() ) )
                  && HtmlBlockCloseTable() ) );
		}
        public bool HtmlBlockOpenUl()    /*HtmlBlockOpenUl : '<' Spnl ('ul' / 'UL') Spnl HtmlAttribute* '>';*/
        {

           return And(()=>  
                     Char('<')
                  && Spnl()
                  && (    Char('u','l') || Char('U','L'))
                  && Spnl()
                  && OptRepeat(()=> HtmlAttribute() )
                  && Char('>') );
		}
        public bool HtmlBlockCloseUl()    /*HtmlBlockCloseUl : '<' Spnl '/' ('ul' / 'UL') Spnl '>';*/
        {

           return And(()=>  
                     Char('<')
                  && Spnl()
                  && Char('/')
                  && (    Char('u','l') || Char('U','L'))
                  && Spnl()
                  && Char('>') );
		}
        public bool HtmlBlockUl()    /*^^HtmlBlockUl : HtmlBlockOpenUl (HtmlBlockUl / !HtmlBlockCloseUl .)* HtmlBlockCloseUl;*/
        {

           return TreeNT((int)EHtml.HtmlBlockUl,()=>
                And(()=>  
                     HtmlBlockOpenUl()
                  && OptRepeat(()=>    
                            
                               HtmlBlockUl()
                            || And(()=>    Not(()=> HtmlBlockCloseUl() ) && Any() ) )
                  && HtmlBlockCloseUl() ) );
		}
        public bool HtmlBlockOpenDd()    /*HtmlBlockOpenDd : '<' Spnl ('dd' / 'DD') Spnl HtmlAttribute* '>';*/
        {

           return And(()=>  
                     Char('<')
                  && Spnl()
                  && (    Char('d','d') || Char('D','D'))
                  && Spnl()
                  && OptRepeat(()=> HtmlAttribute() )
                  && Char('>') );
		}
        public bool HtmlBlockCloseDd()    /*HtmlBlockCloseDd : '<' Spnl '/' ('dd' / 'DD') Spnl '>';*/
        {

           return And(()=>  
                     Char('<')
                  && Spnl()
                  && Char('/')
                  && (    Char('d','d') || Char('D','D'))
                  && Spnl()
                  && Char('>') );
		}
        public bool HtmlBlockDd()    /*^^HtmlBlockDd : HtmlBlockOpenDd (HtmlBlockDd / !HtmlBlockCloseDd .)* HtmlBlockCloseDd;*/
        {

           return TreeNT((int)EHtml.HtmlBlockDd,()=>
                And(()=>  
                     HtmlBlockOpenDd()
                  && OptRepeat(()=>    
                            
                               HtmlBlockDd()
                            || And(()=>    Not(()=> HtmlBlockCloseDd() ) && Any() ) )
                  && HtmlBlockCloseDd() ) );
		}
        public bool HtmlBlockOpenDt()    /*HtmlBlockOpenDt : '<' Spnl ('dt' / 'DT') Spnl HtmlAttribute* '>';*/
        {

           return And(()=>  
                     Char('<')
                  && Spnl()
                  && (    Char('d','t') || Char('D','T'))
                  && Spnl()
                  && OptRepeat(()=> HtmlAttribute() )
                  && Char('>') );
		}
        public bool HtmlBlockCloseDt()    /*HtmlBlockCloseDt : '<' Spnl '/' ('dt' / 'DT') Spnl '>';*/
        {

           return And(()=>  
                     Char('<')
                  && Spnl()
                  && Char('/')
                  && (    Char('d','t') || Char('D','T'))
                  && Spnl()
                  && Char('>') );
		}
        public bool HtmlBlockDt()    /*^^HtmlBlockDt : HtmlBlockOpenDt (HtmlBlockDt / !HtmlBlockCloseDt .)* HtmlBlockCloseDt;*/
        {

           return TreeNT((int)EHtml.HtmlBlockDt,()=>
                And(()=>  
                     HtmlBlockOpenDt()
                  && OptRepeat(()=>    
                            
                               HtmlBlockDt()
                            || And(()=>    Not(()=> HtmlBlockCloseDt() ) && Any() ) )
                  && HtmlBlockCloseDt() ) );
		}
        public bool HtmlBlockOpenFrameset()    /*HtmlBlockOpenFrameset : '<' Spnl ('frameset' / 'FRAMESET') Spnl HtmlAttribute* '>';*/
        {

           return And(()=>  
                     Char('<')
                  && Spnl()
                  && (    Char("frameset") || Char("FRAMESET"))
                  && Spnl()
                  && OptRepeat(()=> HtmlAttribute() )
                  && Char('>') );
		}
        public bool HtmlBlockCloseFrameset()    /*HtmlBlockCloseFrameset : '<' Spnl '/' ('frameset' / 'FRAMESET') Spnl '>';*/
        {

           return And(()=>  
                     Char('<')
                  && Spnl()
                  && Char('/')
                  && (    Char("frameset") || Char("FRAMESET"))
                  && Spnl()
                  && Char('>') );
		}
        public bool HtmlBlockFrameset()    /*^^HtmlBlockFrameset : HtmlBlockOpenFrameset (HtmlBlockFrameset / !HtmlBlockCloseFrameset .)* HtmlBlockCloseFrameset;*/
        {

           return TreeNT((int)EHtml.HtmlBlockFrameset,()=>
                And(()=>  
                     HtmlBlockOpenFrameset()
                  && OptRepeat(()=>    
                            
                               HtmlBlockFrameset()
                            || And(()=>        
                                       Not(()=> HtmlBlockCloseFrameset() )
                                    && Any() ) )
                  && HtmlBlockCloseFrameset() ) );
		}
        public bool HtmlBlockOpenLi()    /*HtmlBlockOpenLi : '<' Spnl ('li' / 'LI') Spnl HtmlAttribute* '>';*/
        {

           return And(()=>  
                     Char('<')
                  && Spnl()
                  && (    Char('l','i') || Char('L','I'))
                  && Spnl()
                  && OptRepeat(()=> HtmlAttribute() )
                  && Char('>') );
		}
        public bool HtmlBlockCloseLi()    /*HtmlBlockCloseLi : '<' Spnl '/' ('li' / 'LI') Spnl '>';*/
        {

           return And(()=>  
                     Char('<')
                  && Spnl()
                  && Char('/')
                  && (    Char('l','i') || Char('L','I'))
                  && Spnl()
                  && Char('>') );
		}
        public bool HtmlBlockLi()    /*^^HtmlBlockLi : HtmlBlockOpenLi (HtmlBlockLi / !HtmlBlockCloseLi .)* HtmlBlockCloseLi;*/
        {

           return TreeNT((int)EHtml.HtmlBlockLi,()=>
                And(()=>  
                     HtmlBlockOpenLi()
                  && OptRepeat(()=>    
                            
                               HtmlBlockLi()
                            || And(()=>    Not(()=> HtmlBlockCloseLi() ) && Any() ) )
                  && HtmlBlockCloseLi() ) );
		}
        public bool HtmlBlockOpenTbody()    /*HtmlBlockOpenTbody : '<' Spnl ('tbody' / 'TBODY') Spnl HtmlAttribute* '>';*/
        {

           return And(()=>  
                     Char('<')
                  && Spnl()
                  && (    
                         Char('t','b','o','d','y')
                      || Char('T','B','O','D','Y'))
                  && Spnl()
                  && OptRepeat(()=> HtmlAttribute() )
                  && Char('>') );
		}
        public bool HtmlBlockCloseTbody()    /*HtmlBlockCloseTbody : '<' Spnl '/' ('tbody' / 'TBODY') Spnl '>';*/
        {

           return And(()=>  
                     Char('<')
                  && Spnl()
                  && Char('/')
                  && (    
                         Char('t','b','o','d','y')
                      || Char('T','B','O','D','Y'))
                  && Spnl()
                  && Char('>') );
		}
        public bool HtmlBlockTbody()    /*^^HtmlBlockTbody : HtmlBlockOpenTbody (HtmlBlockTbody / !HtmlBlockCloseTbody .)* HtmlBlockCloseTbody;*/
        {

           return TreeNT((int)EHtml.HtmlBlockTbody,()=>
                And(()=>  
                     HtmlBlockOpenTbody()
                  && OptRepeat(()=>    
                            
                               HtmlBlockTbody()
                            || And(()=>        
                                       Not(()=> HtmlBlockCloseTbody() )
                                    && Any() ) )
                  && HtmlBlockCloseTbody() ) );
		}
        public bool HtmlBlockOpenTd()    /*HtmlBlockOpenTd : '<' Spnl ('td' / 'TD') Spnl HtmlAttribute* '>';*/
        {

           return And(()=>  
                     Char('<')
                  && Spnl()
                  && (    Char('t','d') || Char('T','D'))
                  && Spnl()
                  && OptRepeat(()=> HtmlAttribute() )
                  && Char('>') );
		}
        public bool HtmlBlockCloseTd()    /*HtmlBlockCloseTd : '<' Spnl '/' ('td' / 'TD') Spnl '>';*/
        {

           return And(()=>  
                     Char('<')
                  && Spnl()
                  && Char('/')
                  && (    Char('t','d') || Char('T','D'))
                  && Spnl()
                  && Char('>') );
		}
        public bool HtmlBlockTd()    /*^^HtmlBlockTd : HtmlBlockOpenTd (HtmlBlockTd / !HtmlBlockCloseTd .)* HtmlBlockCloseTd;*/
        {

           return TreeNT((int)EHtml.HtmlBlockTd,()=>
                And(()=>  
                     HtmlBlockOpenTd()
                  && OptRepeat(()=>    
                            
                               HtmlBlockTd()
                            || And(()=>    Not(()=> HtmlBlockCloseTd() ) && Any() ) )
                  && HtmlBlockCloseTd() ) );
		}
        public bool HtmlBlockOpenTfoot()    /*HtmlBlockOpenTfoot : '<' Spnl ('tfoot' / 'TFOOT') Spnl HtmlAttribute* '>';*/
        {

           return And(()=>  
                     Char('<')
                  && Spnl()
                  && (    
                         Char('t','f','o','o','t')
                      || Char('T','F','O','O','T'))
                  && Spnl()
                  && OptRepeat(()=> HtmlAttribute() )
                  && Char('>') );
		}
        public bool HtmlBlockCloseTfoot()    /*HtmlBlockCloseTfoot : '<' Spnl '/' ('tfoot' / 'TFOOT') Spnl '>';*/
        {

           return And(()=>  
                     Char('<')
                  && Spnl()
                  && Char('/')
                  && (    
                         Char('t','f','o','o','t')
                      || Char('T','F','O','O','T'))
                  && Spnl()
                  && Char('>') );
		}
        public bool HtmlBlockTfoot()    /*^^HtmlBlockTfoot : HtmlBlockOpenTfoot (HtmlBlockTfoot / !HtmlBlockCloseTfoot .)* HtmlBlockCloseTfoot;*/
        {

           return TreeNT((int)EHtml.HtmlBlockTfoot,()=>
                And(()=>  
                     HtmlBlockOpenTfoot()
                  && OptRepeat(()=>    
                            
                               HtmlBlockTfoot()
                            || And(()=>        
                                       Not(()=> HtmlBlockCloseTfoot() )
                                    && Any() ) )
                  && HtmlBlockCloseTfoot() ) );
		}
        public bool HtmlBlockOpenTh()    /*HtmlBlockOpenTh : '<' Spnl ('th' / 'TH') Spnl HtmlAttribute* '>';*/
        {

           return And(()=>  
                     Char('<')
                  && Spnl()
                  && (    Char('t','h') || Char('T','H'))
                  && Spnl()
                  && OptRepeat(()=> HtmlAttribute() )
                  && Char('>') );
		}
        public bool HtmlBlockCloseTh()    /*HtmlBlockCloseTh : '<' Spnl '/' ('th' / 'TH') Spnl '>';*/
        {

           return And(()=>  
                     Char('<')
                  && Spnl()
                  && Char('/')
                  && (    Char('t','h') || Char('T','H'))
                  && Spnl()
                  && Char('>') );
		}
        public bool HtmlBlockTh()    /*^^HtmlBlockTh : HtmlBlockOpenTh (HtmlBlockTh / !HtmlBlockCloseTh .)* HtmlBlockCloseTh;*/
        {

           return TreeNT((int)EHtml.HtmlBlockTh,()=>
                And(()=>  
                     HtmlBlockOpenTh()
                  && OptRepeat(()=>    
                            
                               HtmlBlockTh()
                            || And(()=>    Not(()=> HtmlBlockCloseTh() ) && Any() ) )
                  && HtmlBlockCloseTh() ) );
		}
        public bool HtmlBlockOpenThead()    /*HtmlBlockOpenThead : '<' Spnl ('thead' / 'THEAD') Spnl HtmlAttribute* '>';*/
        {

           return And(()=>  
                     Char('<')
                  && Spnl()
                  && (    
                         Char('t','h','e','a','d')
                      || Char('T','H','E','A','D'))
                  && Spnl()
                  && OptRepeat(()=> HtmlAttribute() )
                  && Char('>') );
		}
        public bool HtmlBlockCloseThead()    /*HtmlBlockCloseThead : '<' Spnl '/' ('thead' / 'THEAD') Spnl '>';*/
        {

           return And(()=>  
                     Char('<')
                  && Spnl()
                  && Char('/')
                  && (    
                         Char('t','h','e','a','d')
                      || Char('T','H','E','A','D'))
                  && Spnl()
                  && Char('>') );
		}
        public bool HtmlBlockThead()    /*^^HtmlBlockThead : HtmlBlockOpenThead (HtmlBlockThead / !HtmlBlockCloseThead .)* HtmlBlockCloseThead;*/
        {

           return TreeNT((int)EHtml.HtmlBlockThead,()=>
                And(()=>  
                     HtmlBlockOpenThead()
                  && OptRepeat(()=>    
                            
                               HtmlBlockThead()
                            || And(()=>        
                                       Not(()=> HtmlBlockCloseThead() )
                                    && Any() ) )
                  && HtmlBlockCloseThead() ) );
		}
        public bool HtmlBlockOpenTr()    /*HtmlBlockOpenTr : '<' Spnl ('tr' / 'TR') Spnl HtmlAttribute* '>';*/
        {

           return And(()=>  
                     Char('<')
                  && Spnl()
                  && (    Char('t','r') || Char('T','R'))
                  && Spnl()
                  && OptRepeat(()=> HtmlAttribute() )
                  && Char('>') );
		}
        public bool HtmlBlockCloseTr()    /*HtmlBlockCloseTr : '<' Spnl '/' ('tr' / 'TR') Spnl '>';*/
        {

           return And(()=>  
                     Char('<')
                  && Spnl()
                  && Char('/')
                  && (    Char('t','r') || Char('T','R'))
                  && Spnl()
                  && Char('>') );
		}
        public bool HtmlBlockTr()    /*^^HtmlBlockTr : HtmlBlockOpenTr (HtmlBlockTr / !HtmlBlockCloseTr .)* HtmlBlockCloseTr;*/
        {

           return TreeNT((int)EHtml.HtmlBlockTr,()=>
                And(()=>  
                     HtmlBlockOpenTr()
                  && OptRepeat(()=>    
                            
                               HtmlBlockTr()
                            || And(()=>    Not(()=> HtmlBlockCloseTr() ) && Any() ) )
                  && HtmlBlockCloseTr() ) );
		}
        public bool HtmlBlockOpenScript()    /*HtmlBlockOpenScript : '<' Spnl ('script' / 'SCRIPT') Spnl HtmlAttribute* '>';*/
        {

           return And(()=>  
                     Char('<')
                  && Spnl()
                  && (    
                         Char('s','c','r','i','p','t')
                      || Char('S','C','R','I','P','T'))
                  && Spnl()
                  && OptRepeat(()=> HtmlAttribute() )
                  && Char('>') );
		}
        public bool HtmlBlockCloseScript()    /*HtmlBlockCloseScript : '<' Spnl '/' ('script' / 'SCRIPT') Spnl '>';*/
        {

           return And(()=>  
                     Char('<')
                  && Spnl()
                  && Char('/')
                  && (    
                         Char('s','c','r','i','p','t')
                      || Char('S','C','R','I','P','T'))
                  && Spnl()
                  && Char('>') );
		}
        public bool HtmlBlockScript()    /*^^HtmlBlockScript : HtmlBlockOpenScript (!HtmlBlockCloseScript .)* HtmlBlockCloseScript;*/
        {

           return TreeNT((int)EHtml.HtmlBlockScript,()=>
                And(()=>  
                     HtmlBlockOpenScript()
                  && OptRepeat(()=>    
                      And(()=>      
                               Not(()=> HtmlBlockCloseScript() )
                            && Any() ) )
                  && HtmlBlockCloseScript() ) );
		}
        public bool HtmlBlockOpenHead()    /*HtmlBlockOpenHead : '<' Spnl ('head' / 'HEAD') Spnl HtmlAttribute* '>';*/
        {

           return And(()=>  
                     Char('<')
                  && Spnl()
                  && (    Char('h','e','a','d') || Char('H','E','A','D'))
                  && Spnl()
                  && OptRepeat(()=> HtmlAttribute() )
                  && Char('>') );
		}
        public bool HtmlBlockCloseHead()    /*HtmlBlockCloseHead : '<' Spnl '/' ('head' / 'HEAD') Spnl '>';*/
        {

           return And(()=>  
                     Char('<')
                  && Spnl()
                  && Char('/')
                  && (    Char('h','e','a','d') || Char('H','E','A','D'))
                  && Spnl()
                  && Char('>') );
		}
        public bool HtmlBlockHead()    /*^^HtmlBlockHead : HtmlBlockOpenHead (!HtmlBlockCloseHead .)* HtmlBlockCloseHead ;*/
        {

           return TreeNT((int)EHtml.HtmlBlockHead,()=>
                And(()=>  
                     HtmlBlockOpenHead()
                  && OptRepeat(()=>    
                      And(()=>    Not(()=> HtmlBlockCloseHead() ) && Any() ) )
                  && HtmlBlockCloseHead() ) );
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
                / HtmlBlockHead ;*/
        {

           return TreeNT((int)EHtml.HtmlBlockInTags,()=>
                  
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
                  || HtmlBlockHead() );
		}
        public bool HtmlBlock()    /*^^HtmlBlock : ( HtmlBlockInTags / HtmlComment / HtmlBlockSelfClosing ) 
            BlankLine+ ;*/
        {

           return TreeNT((int)EHtml.HtmlBlock,()=>
                And(()=>  
                     (    
                         HtmlBlockInTags()
                      || HtmlComment()
                      || HtmlBlockSelfClosing())
                  && PlusRepeat(()=> BlankLine() ) ) );
		}
        public bool HtmlBlockSelfClosing()    /*^^HtmlBlockSelfClosing : '<' Spnl HtmlBlockType Spnl HtmlAttribute* '/' Spnl '>';*/
        {

           return TreeNT((int)EHtml.HtmlBlockSelfClosing,()=>
                And(()=>  
                     Char('<')
                  && Spnl()
                  && HtmlBlockType()
                  && Spnl()
                  && OptRepeat(()=> HtmlAttribute() )
                  && Char('/')
                  && Spnl()
                  && Char('>') ) );
		}
        public bool HtmlBlockType()    /*HtmlBlockType : 'address' / 'blockquote' / 'center' / 'dir' / 'div' / 'dl' / 'fieldset' / 'form' / 'h1' / 'h2' / 'h3' /
                'h4' / 'h5' / 'h6' / 'hr' / 'isindex' / 'menu' / 'noframes' / 'noscript' / 'ol' / 'p' / 'pre' / 'table' /
                'ul' / 'dd' / 'dt' / 'frameset' / 'li' / 'tbody' / 'td' / 'tfoot' / 'th' / 'thead' / 'tr' / 'script' /
                'ADDRESS' / 'BLOCKQUOTE' / 'CENTER' / 'DIR' / 'DIV' / 'DL' / 'FIELDSET' / 'FORM' / 'H1' / 'H2' / 'H3' /
                'H4' / 'H5' / 'H6' / 'HR' / 'ISINDEX' / 'MENU' / 'NOFRAMES' / 'NOSCRIPT' / 'OL' / 'P' / 'PRE' / 'TABLE' /
                'UL' / 'DD' / 'DT' / 'FRAMESET' / 'LI' / 'TBODY' / 'TD' / 'TFOOT' / 'TH' / 'THEAD' / 'TR' / 'SCRIPT';*/
        {

           return OneOfLiterals(optimizedLiterals0);
		}
        public bool StyleOpen()    /*StyleOpen :     '<' Spnl ('style' / 'STYLE') Spnl HtmlAttribute* '>'  ;*/
        {

           return And(()=>  
                     Char('<')
                  && Spnl()
                  && (    
                         Char('s','t','y','l','e')
                      || Char('S','T','Y','L','E'))
                  && Spnl()
                  && OptRepeat(()=> HtmlAttribute() )
                  && Char('>') );
		}
        public bool StyleClose()    /*StyleClose :    '<' Spnl '/' ('style' / 'STYLE') Spnl '>' ;*/
        {

           return And(()=>  
                     Char('<')
                  && Spnl()
                  && Char('/')
                  && (    
                         Char('s','t','y','l','e')
                      || Char('S','T','Y','L','E'))
                  && Spnl()
                  && Char('>') );
		}
        public bool InStyleTags()    /*InStyleTags :   StyleOpen (!StyleClose .)* StyleClose ;*/
        {

           return And(()=>  
                     StyleOpen()
                  && OptRepeat(()=>    
                      And(()=>    Not(()=> StyleClose() ) && Any() ) )
                  && StyleClose() );
		}
        public bool StyleBlock()    /*^^StyleBlock :    InStyleTags 
                BlankLine*  ;*/
        {

           return TreeNT((int)EHtml.StyleBlock,()=>
                And(()=>  
                     InStyleTags()
                  && OptRepeat(()=> BlankLine() ) ) );
		}
        public bool Space()    /*Space : Spacechar+ ;*/
        {

           return PlusRepeat(()=> Spacechar() );
		}
        public bool RawHtml()    /*RawHtml :    (HtmlComment / HtmlBlockScript / HtmlTag) ;*/
        {

           return     HtmlComment() || HtmlBlockScript() || HtmlTag();
		}
        public bool BlankLine()    /*BlankLine :     Sp Newline;*/
        {

           return And(()=>    Sp() && Newline() );
		}
        public bool Quoted()    /*^^Quoted :         '\'' (!'\'' .)* '\''  /    '\"' (!'\"' .)* '\"';*/
        {

           return TreeNT((int)EHtml.Quoted,()=>
                  
                     And(()=>    
                         Char('\'')
                      && OptRepeat(()=>      
                            And(()=>    Not(()=> Char('\'') ) && Any() ) )
                      && Char('\'') )
                  || And(()=>    
                         Char('\"')
                      && OptRepeat(()=>      
                            And(()=>    Not(()=> Char('\"') ) && Any() ) )
                      && Char('\"') ) );
		}
        public bool HtmlAttribute()    /*^^HtmlAttribute : (AlphanumericAscii / '-')+ Spnl ('=' Spnl (Quoted / (!'>' Nonspacechar)+)) Spnl ;*/
        {

           return TreeNT((int)EHtml.HtmlAttribute,()=>
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
                  && Spnl() ) );
		}
        public bool HtmlComment()    /*^^HtmlComment :   '<!--' (!'-->' .)* '-->';*/
        {

           return TreeNT((int)EHtml.HtmlComment,()=>
                And(()=>  
                     Char('<','!','-','-')
                  && OptRepeat(()=>    
                      And(()=>    Not(()=> Char('-','-','>') ) && Any() ) )
                  && Char('-','-','>') ) );
		}
        public bool HtmlTag()    /*HtmlTag :       '<' Spnl '/'? AlphanumericAscii+ Spnl HtmlAttribute* '/'? Spnl '>';*/
        {

           return And(()=>  
                     Char('<')
                  && Spnl()
                  && Option(()=> Char('/') )
                  && PlusRepeat(()=> AlphanumericAscii() )
                  && Spnl()
                  && OptRepeat(()=> HtmlAttribute() )
                  && Option(()=> Char('/') )
                  && Spnl()
                  && Char('>') );
		}
        public bool Spacechar()    /*Spacechar :     ' ' / '\t';*/
        {

           return     Char(' ') || Char('\t');
		}
        public bool Nonspacechar()    /*Nonspacechar :  !Spacechar !Newline .;*/
        {

           return And(()=>  
                     Not(()=> Spacechar() )
                  && Not(()=> Newline() )
                  && Any() );
		}
        public bool Newline()    /*Newline :       '\n' / '\r' '\n'?;*/
        {

           return   
                     Char('\n')
                  || And(()=>    Char('\r') && Option(()=> Char('\n') ) );
		}
        public bool Sp()    /*Sp :            Spacechar*;*/
        {

           return OptRepeat(()=> Spacechar() );
		}
        public bool Spnl()    /*Spnl :          Sp (Newline Sp)? ;*/
        {

           return And(()=>  
                     Sp()
                  && Option(()=> And(()=>    Newline() && Sp() ) ) );
		}
        public bool AlphanumericAscii()    /*AlphanumericAscii : [A-Za-z0-9] ;*/
        {

           return In('A','Z', 'a','z', '0','9');
		}
        public bool Eof()    /*Eof :          !./ WARNING<" end of file">;*/
        {

           return     Not(()=> Any() ) || Warning(" end of file");
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
                  "TD","TFOOT","TH","THEAD","TR","SCRIPT" };
               optimizedLiterals0= new OptimizedLiterals(literals);
            }

            
        }
        #endregion Optimization Data 
           }
}