/* created on 10/12/2016 16:06:09 from peg generator V1.0 using 'Markdown' as input*/

using Peg.Base;
using System;
using System.IO;
using System.Text;
namespace Markdown
{
      
      enum EMarkdown{Doc= 1, Block= 2, Para= 3, Plain= 4, AtxInline= 5, AtxStart= 6, 
                      AtxHeading= 7, SetextHeading= 8, SetextBottom1= 9, SetextBottom2= 10, 
                      SetextHeading1= 11, SetextHeading2= 12, Heading= 13, BlockQuote= 14, 
                      BlockQuoteRaw= 15, NonblankIndentedLine= 16, VerbatimChunk= 17, 
                      Verbatim= 18, HorizontalRule= 19, Bullet= 20, BulletList= 21, 
                      ListTight= 22, ListLoose= 23, ListItem= 24, ListItemTight= 25, 
                      ListBlock= 26, ListContinuationBlock= 27, Enumerator= 28, OrderedList= 29, 
                      ListBlockLine= 30, HtmlBlockOpenAddress= 31, HtmlBlockCloseAddress= 32, 
                      HtmlBlockAddress= 33, HtmlBlockOpenBlockquote= 34, HtmlBlockCloseBlockquote= 35, 
                      HtmlBlockBlockquote= 36, HtmlBlockOpenCenter= 37, HtmlBlockCloseCenter= 38, 
                      HtmlBlockCenter= 39, HtmlBlockOpenDir= 40, HtmlBlockCloseDir= 41, 
                      HtmlBlockDir= 42, HtmlBlockOpenDiv= 43, HtmlBlockCloseDiv= 44, 
                      HtmlBlockDiv= 45, HtmlBlockOpenDl= 46, HtmlBlockCloseDl= 47, 
                      HtmlBlockDl= 48, HtmlBlockOpenFieldset= 49, HtmlBlockCloseFieldset= 50, 
                      HtmlBlockFieldset= 51, HtmlBlockOpenForm= 52, HtmlBlockCloseForm= 53, 
                      HtmlBlockForm= 54, HtmlBlockOpenH1= 55, HtmlBlockCloseH1= 56, 
                      HtmlBlockH1= 57, HtmlBlockOpenH2= 58, HtmlBlockCloseH2= 59, HtmlBlockH2= 60, 
                      HtmlBlockOpenH3= 61, HtmlBlockCloseH3= 62, HtmlBlockH3= 63, HtmlBlockOpenH4= 64, 
                      HtmlBlockCloseH4= 65, HtmlBlockH4= 66, HtmlBlockOpenH5= 67, HtmlBlockCloseH5= 68, 
                      HtmlBlockH5= 69, HtmlBlockOpenH6= 70, HtmlBlockCloseH6= 71, HtmlBlockH6= 72, 
                      HtmlBlockOpenMenu= 73, HtmlBlockCloseMenu= 74, HtmlBlockMenu= 75, 
                      HtmlBlockOpenNoframes= 76, HtmlBlockCloseNoframes= 77, HtmlBlockNoframes= 78, 
                      HtmlBlockOpenNoscript= 79, HtmlBlockCloseNoscript= 80, HtmlBlockNoscript= 81, 
                      HtmlBlockOpenOl= 82, HtmlBlockCloseOl= 83, HtmlBlockOl= 84, HtmlBlockOpenP= 85, 
                      HtmlBlockCloseP= 86, HtmlBlockP= 87, HtmlBlockOpenPre= 88, HtmlBlockClosePre= 89, 
                      HtmlBlockPre= 90, HtmlBlockOpenTable= 91, HtmlBlockCloseTable= 92, 
                      HtmlBlockTable= 93, HtmlBlockOpenUl= 94, HtmlBlockCloseUl= 95, 
                      HtmlBlockUl= 96, HtmlBlockOpenDd= 97, HtmlBlockCloseDd= 98, HtmlBlockDd= 99, 
                      HtmlBlockOpenDt= 100, HtmlBlockCloseDt= 101, HtmlBlockDt= 102, 
                      HtmlBlockOpenFrameset= 103, HtmlBlockCloseFrameset= 104, HtmlBlockFrameset= 105, 
                      HtmlBlockOpenLi= 106, HtmlBlockCloseLi= 107, HtmlBlockLi= 108, 
                      HtmlBlockOpenTbody= 109, HtmlBlockCloseTbody= 110, HtmlBlockTbody= 111, 
                      HtmlBlockOpenTd= 112, HtmlBlockCloseTd= 113, HtmlBlockTd= 114, 
                      HtmlBlockOpenTfoot= 115, HtmlBlockCloseTfoot= 116, HtmlBlockTfoot= 117, 
                      HtmlBlockOpenTh= 118, HtmlBlockCloseTh= 119, HtmlBlockTh= 120, 
                      HtmlBlockOpenThead= 121, HtmlBlockCloseThead= 122, HtmlBlockThead= 123, 
                      HtmlBlockOpenTr= 124, HtmlBlockCloseTr= 125, HtmlBlockTr= 126, 
                      HtmlBlockOpenScript= 127, HtmlBlockCloseScript= 128, HtmlBlockScript= 129, 
                      HtmlBlockOpenHead= 130, HtmlBlockCloseHead= 131, HtmlBlockHead= 132, 
                      HtmlBlockInTags= 133, HtmlBlock= 134, HtmlBlockSelfClosing= 135, 
                      HtmlBlockType= 136, StyleOpen= 137, StyleClose= 138, InStyleTags= 139, 
                      StyleBlock= 140, Inlines= 141, Inline= 142, Space= 143, Str= 144, 
                      StrChunk= 145, AposChunk= 146, EscapedChar= 147, Entity= 148, 
                      Endline= 149, NormalEndline= 150, TerminalEndline= 151, LineBreak= 152, 
                      Symbol= 153, UlOrStarLine= 154, StarLine= 155, UlLine= 156, Emph= 157, 
                      Whitespace= 158, EmphStar= 159, EmphUl= 160, Strong= 161, StrongStar= 162, 
                      StrongUl= 163, Strike= 164, Image= 165, Link= 166, ReferenceLink= 167, 
                      ReferenceLinkDouble= 168, ReferenceLinkSingle= 169, ExplicitLink= 170, 
                      Source= 171, SourceContents= 172, Title= 173, TitleSingle= 174, 
                      TitleDouble= 175, AutoLink= 176, AutoLinkUrl= 177, AutoLinkEmail= 178, 
                      Reference= 179, Label= 180, RefSrc= 181, RefTitle= 182, EmptyTitle= 183, 
                      RefTitleSingle= 184, RefTitleDouble= 185, RefTitleParens= 186, 
                      References= 187, Ticks1= 188, Ticks2= 189, Ticks3= 190, Ticks4= 191, 
                      Ticks5= 192, Code= 193, RawHtml= 194, BlankLine= 195, Quoted= 196, 
                      HtmlAttribute= 197, HtmlComment= 198, HtmlTag= 199, Eof= 200, 
                      Spacechar= 201, Nonspacechar= 202, Newline= 203, Sp= 204, Spnl= 205, 
                      SpecialChar= 206, NormalChar= 207, Alphanumeric= 208, AlphanumericAscii= 209, 
                      Digit= 210, BOM= 211, HexEntity= 212, DecEntity= 213, CharEntity= 214, 
                      NonindentSpace= 215, Indent= 216, IndentedLine= 217, OptionallyIndentedLine= 218, 
                      StartList= 219, Line= 220, RawLine= 221, SkipBlock= 222, ExtendedSpecialChar= 223, 
                      Smart= 224, Apostrophe= 225, Ellipsis= 226, Dash= 227, EnDash= 228, 
                      EmDash= 229, SingleQuoteStart= 230, SingleQuoteEnd= 231, SingleQuoted= 232, 
                      DoubleQuoteStart= 233, DoubleQuoteEnd= 234, DoubleQuoted= 235, 
                      NoteReference= 236, RawNoteReference= 237, Note= 238, InlineNote= 239, 
                      Notes= 240, RawNoteBlock= 241};
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
        public bool Doc()    /*^^Doc :      Block  *  Eof;*/
        {

           var result= TreeNT((int)EMarkdown.Doc,()=>
                And(()=>    OptRepeat(()=> Block() ) && Eof() ) ); return result;
		}
        public bool Block()    /*^^Block :     BlankLine*
            ( BlockQuote
            / Verbatim
            / Note
            / Reference
            / HorizontalRule
            / Heading
            / OrderedList
            / BulletList
            / HtmlBlock
            / StyleBlock
            / Para
            / Plain );*/
        {

           var result= TreeNT((int)EMarkdown.Block,()=>
                And(()=>  
                     OptRepeat(()=> BlankLine() )
                  && (    
                         BlockQuote()
                      || Verbatim()
                      || Note()
                      || Reference()
                      || HorizontalRule()
                      || Heading()
                      || OrderedList()
                      || BulletList()
                      || HtmlBlock()
                      || StyleBlock()
                      || Para()
                      || Plain()) ) ); return result;
		}
        public bool Para()    /*^^Para :      NonindentSpace Inlines BlankLine+;*/
        {

           var result= TreeNT((int)EMarkdown.Para,()=>
                And(()=>  
                     NonindentSpace()
                  && Inlines()
                  && PlusRepeat(()=> BlankLine() ) ) ); return result;
		}
        public bool Plain()    /*^^Plain :     Inlines;*/
        {

           var result= TreeNT((int)EMarkdown.Plain,()=>
                Inlines() ); return result;
		}
        public bool AtxInline()    /*^^AtxInline : !Newline !(Sp '#'* Sp Newline) Inline;*/
        {

           var result= TreeNT((int)EMarkdown.AtxInline,()=>
                And(()=>  
                     Not(()=> Newline() )
                  && Not(()=>    
                      And(()=>      
                               Sp()
                            && OptRepeat(()=> Char('#') )
                            && Sp()
                            && Newline() ) )
                  && Inline() ) ); return result;
		}
        public bool AtxStart()    /*^^AtxStart :  ( '######' / '#####' / '####' / '###' / '##' / '#' ) ;*/
        {

           var result= TreeNT((int)EMarkdown.AtxStart,()=>
                  
                     Char('#','#','#','#','#','#')
                  || Char('#','#','#','#','#')
                  || Char('#','#','#','#')
                  || Char('#','#','#')
                  || Char('#','#')
                  || Char('#') ); return result;
		}
        public bool AtxHeading()    /*^^AtxHeading : AtxStart Sp StartList ( AtxInline  )+ (Sp '#'* Sp)?  Newline;*/
        {

           var result= TreeNT((int)EMarkdown.AtxHeading,()=>
                And(()=>  
                     AtxStart()
                  && Sp()
                  && StartList()
                  && PlusRepeat(()=> AtxInline() )
                  && Option(()=>    
                      And(()=>      
                               Sp()
                            && OptRepeat(()=> Char('#') )
                            && Sp() ) )
                  && Newline() ) ); return result;
		}
        public bool SetextHeading()    /*^^SetextHeading : SetextHeading1 / SetextHeading2;*/
        {

           var result= TreeNT((int)EMarkdown.SetextHeading,()=>
                    SetextHeading1() || SetextHeading2() ); return result;
		}
        public bool SetextBottom1()    /*^^SetextBottom1 : ':'+ Newline;*/
        {

           var result= TreeNT((int)EMarkdown.SetextBottom1,()=>
                And(()=>    PlusRepeat(()=> Char(':') ) && Newline() ) ); return result;
		}
        public bool SetextBottom2()    /*^^SetextBottom2 : '-'+ Newline;*/
        {

           var result= TreeNT((int)EMarkdown.SetextBottom2,()=>
                And(()=>    PlusRepeat(()=> Char('-') ) && Newline() ) ); return result;
		}
        public bool SetextHeading1()    /*^^SetextHeading1 : & (RawLine SetextBottom1)
                  StartList ( !Endline Inline  )+ Sp Newline
                  SetextBottom1 ;*/
        {

           var result= TreeNT((int)EMarkdown.SetextHeading1,()=>
                And(()=>  
                     Peek(()=> And(()=>    RawLine() && SetextBottom1() ) )
                  && StartList()
                  && PlusRepeat(()=>    
                      And(()=>    Not(()=> Endline() ) && Inline() ) )
                  && Sp()
                  && Newline()
                  && SetextBottom1() ) ); return result;
		}
        public bool SetextHeading2()    /*^^SetextHeading2 : & (RawLine SetextBottom2)
                  StartList ( !Endline Inline  )+ Sp Newline
                  SetextBottom2 ;*/
        {

           var result= TreeNT((int)EMarkdown.SetextHeading2,()=>
                And(()=>  
                     Peek(()=> And(()=>    RawLine() && SetextBottom2() ) )
                  && StartList()
                  && PlusRepeat(()=>    
                      And(()=>    Not(()=> Endline() ) && Inline() ) )
                  && Sp()
                  && Newline()
                  && SetextBottom2() ) ); return result;
		}
        public bool Heading()    /*^^Heading : SetextHeading / AtxHeading;*/
        {

           var result= TreeNT((int)EMarkdown.Heading,()=>
                    SetextHeading() || AtxHeading() ); return result;
		}
        public bool BlockQuote()    /*^^BlockQuote : BlockQuoteRaw;*/
        {

           var result= TreeNT((int)EMarkdown.BlockQuote,()=>
                BlockQuoteRaw() ); return result;
		}
        public bool BlockQuoteRaw()    /*^^BlockQuoteRaw :  StartList
                 (( '>' ' '? Line  )
                  ( !'>' !BlankLine Line  )*
                  ( BlankLine  )*
                 )+;*/
        {

           var result= TreeNT((int)EMarkdown.BlockQuoteRaw,()=>
                And(()=>  
                     StartList()
                  && PlusRepeat(()=>    
                      And(()=>      
                               And(()=>        
                                       Char('>')
                                    && Option(()=> Char(' ') )
                                    && Line() )
                            && OptRepeat(()=>        
                                    And(()=>          
                                                 Not(()=> Char('>') )
                                              && Not(()=> BlankLine() )
                                              && Line() ) )
                            && OptRepeat(()=> BlankLine() ) ) ) ) ); return result;
		}
        public bool NonblankIndentedLine()    /*^^NonblankIndentedLine : !BlankLine IndentedLine;*/
        {

           var result= TreeNT((int)EMarkdown.NonblankIndentedLine,()=>
                And(()=>    Not(()=> BlankLine() ) && IndentedLine() ) ); return result;
		}
        public bool VerbatimChunk()    /*^^VerbatimChunk : StartList
                ( BlankLine  )*
                ( NonblankIndentedLine  )+;*/
        {

           var result= TreeNT((int)EMarkdown.VerbatimChunk,()=>
                And(()=>  
                     StartList()
                  && OptRepeat(()=> BlankLine() )
                  && PlusRepeat(()=> NonblankIndentedLine() ) ) ); return result;
		}
        public bool Verbatim()    /*^^Verbatim :     StartList ( VerbatimChunk  )+;*/
        {

           var result= TreeNT((int)EMarkdown.Verbatim,()=>
                And(()=>  
                     StartList()
                  && PlusRepeat(()=> VerbatimChunk() ) ) ); return result;
		}
        public bool HorizontalRule()    /*^^HorizontalRule : NonindentSpace
                 ( '*' Sp '*' Sp '*' (Sp '*')*
                 / '-' Sp '-' Sp '-' (Sp '-')*
                 / '_' Sp '_' Sp '_' (Sp '_')*)
                 Sp Newline BlankLine+;*/
        {

           var result= TreeNT((int)EMarkdown.HorizontalRule,()=>
                And(()=>  
                     NonindentSpace()
                  && (    
                         And(()=>      
                               Char('*')
                            && Sp()
                            && Char('*')
                            && Sp()
                            && Char('*')
                            && OptRepeat(()=> And(()=>    Sp() && Char('*') ) ) )
                      || And(()=>      
                               Char('-')
                            && Sp()
                            && Char('-')
                            && Sp()
                            && Char('-')
                            && OptRepeat(()=> And(()=>    Sp() && Char('-') ) ) )
                      || And(()=>      
                               Char('_')
                            && Sp()
                            && Char('_')
                            && Sp()
                            && Char('_')
                            && OptRepeat(()=> And(()=>    Sp() && Char('_') ) ) ))
                  && Sp()
                  && Newline()
                  && PlusRepeat(()=> BlankLine() ) ) ); return result;
		}
        public bool Bullet()    /*^^Bullet : !HorizontalRule NonindentSpace ('+' / '*' / '-') Spacechar+;*/
        {

           var result= TreeNT((int)EMarkdown.Bullet,()=>
                And(()=>  
                     Not(()=> HorizontalRule() )
                  && NonindentSpace()
                  && (    Char('+') || Char('*') || Char('-'))
                  && PlusRepeat(()=> Spacechar() ) ) ); return result;
		}
        public bool BulletList()    /*^^BulletList : &Bullet (ListTight / ListLoose);*/
        {

           var result= TreeNT((int)EMarkdown.BulletList,()=>
                And(()=>  
                     Peek(()=> Bullet() )
                  && (    ListTight() || ListLoose()) ) ); return result;
		}
        public bool ListTight()    /*^^ListTight : StartList
            ( ListItemTight  )+
            BlankLine* !(Bullet / Enumerator);*/
        {

           var result= TreeNT((int)EMarkdown.ListTight,()=>
                And(()=>  
                     StartList()
                  && PlusRepeat(()=> ListItemTight() )
                  && OptRepeat(()=> BlankLine() )
                  && Not(()=>     Bullet() || Enumerator() ) ) ); return result;
		}
        public bool ListLoose()    /*^^ListLoose : StartList
            ( ListItem BlankLine*
            )+;*/
        {

           var result= TreeNT((int)EMarkdown.ListLoose,()=>
                And(()=>  
                     StartList()
                  && PlusRepeat(()=>    
                      And(()=>      
                               ListItem()
                            && OptRepeat(()=> BlankLine() ) ) ) ) ); return result;
		}
        public bool ListItem()    /*^^ListItem :  ( Bullet / Enumerator )
            StartList
            ListBlock 
            ( ListContinuationBlock  )*;*/
        {

           var result= TreeNT((int)EMarkdown.ListItem,()=>
                And(()=>  
                     (    Bullet() || Enumerator())
                  && StartList()
                  && ListBlock()
                  && OptRepeat(()=> ListContinuationBlock() ) ) ); return result;
		}
        public bool ListItemTight()    /*^^ListItemTight :
            ( Bullet / Enumerator )
            StartList
            ListBlock 
            ( !BlankLine
              ListContinuationBlock  )*
            !ListContinuationBlock ;*/
        {

           var result= TreeNT((int)EMarkdown.ListItemTight,()=>
                And(()=>  
                     (    Bullet() || Enumerator())
                  && StartList()
                  && ListBlock()
                  && OptRepeat(()=>    
                      And(()=>      
                               Not(()=> BlankLine() )
                            && ListContinuationBlock() ) )
                  && Not(()=> ListContinuationBlock() ) ) ); return result;
		}
        public bool ListBlock()    /*^^ListBlock : StartList
            !BlankLine Inline 
            ( ListBlockLine  )*;*/
        {

           var result= TreeNT((int)EMarkdown.ListBlock,()=>
                And(()=>  
                     StartList()
                  && Not(()=> BlankLine() )
                  && Inline()
                  && OptRepeat(()=> ListBlockLine() ) ) ); return result;
		}
        public bool ListContinuationBlock()    /*^^ListContinuationBlock : StartList
                        ( BlankLine* 
                        )
                        ( Indent ListBlock  )+;*/
        {

           var result= TreeNT((int)EMarkdown.ListContinuationBlock,()=>
                And(()=>  
                     StartList()
                  && OptRepeat(()=> BlankLine() )
                  && PlusRepeat(()=> And(()=>    Indent() && ListBlock() ) ) ) ); return result;
		}
        public bool Enumerator()    /*^^Enumerator : NonindentSpace [0-9]+ '.' Spacechar+;*/
        {

           var result= TreeNT((int)EMarkdown.Enumerator,()=>
                And(()=>  
                     NonindentSpace()
                  && PlusRepeat(()=> In('0','9') )
                  && Char('.')
                  && PlusRepeat(()=> Spacechar() ) ) ); return result;
		}
        public bool OrderedList()    /*^^OrderedList : &Enumerator (ListTight / ListLoose);*/
        {

           var result= TreeNT((int)EMarkdown.OrderedList,()=>
                And(()=>  
                     Peek(()=> Enumerator() )
                  && (    ListTight() || ListLoose()) ) ); return result;
		}
        public bool ListBlockLine()    /*^^ListBlockLine : !BlankLine
                !( Indent? (Bullet / Enumerator) )
                !HorizontalRule
                OptionallyIndentedLine;

// Parsers for different kinds of block-level HTML content.
// This is repetitive due to constraints of PEG grammar.*/
        {

           var result= TreeNT((int)EMarkdown.ListBlockLine,()=>
                And(()=>  
                     Not(()=> BlankLine() )
                  && Not(()=>    
                      And(()=>      
                               Option(()=> Indent() )
                            && (    Bullet() || Enumerator()) ) )
                  && Not(()=> HorizontalRule() )
                  && OptionallyIndentedLine() ) ); return result;
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
        public bool HtmlBlockAddress()    /*^^HtmlBlockAddress : HtmlBlockOpenAddress (HtmlBlockAddress / !HtmlBlockCloseAddress .)* HtmlBlockCloseAddress;*/
        {

           var result= TreeNT((int)EMarkdown.HtmlBlockAddress,()=>
                And(()=>  
                     HtmlBlockOpenAddress()
                  && OptRepeat(()=>    
                            
                               HtmlBlockAddress()
                            || And(()=>        
                                       Not(()=> HtmlBlockCloseAddress() )
                                    && Any() ) )
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
        public bool HtmlBlockBlockquote()    /*^^HtmlBlockBlockquote : HtmlBlockOpenBlockquote (HtmlBlockBlockquote / !HtmlBlockCloseBlockquote .)* HtmlBlockCloseBlockquote;*/
        {

           var result= TreeNT((int)EMarkdown.HtmlBlockBlockquote,()=>
                And(()=>  
                     HtmlBlockOpenBlockquote()
                  && OptRepeat(()=>    
                            
                               HtmlBlockBlockquote()
                            || And(()=>        
                                       Not(()=> HtmlBlockCloseBlockquote() )
                                    && Any() ) )
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
        public bool HtmlBlockCenter()    /*^^HtmlBlockCenter : HtmlBlockOpenCenter (HtmlBlockCenter / !HtmlBlockCloseCenter .)* HtmlBlockCloseCenter;*/
        {

           var result= TreeNT((int)EMarkdown.HtmlBlockCenter,()=>
                And(()=>  
                     HtmlBlockOpenCenter()
                  && OptRepeat(()=>    
                            
                               HtmlBlockCenter()
                            || And(()=>        
                                       Not(()=> HtmlBlockCloseCenter() )
                                    && Any() ) )
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
        public bool HtmlBlockDir()    /*^^HtmlBlockDir : HtmlBlockOpenDir (HtmlBlockDir / !HtmlBlockCloseDir .)* HtmlBlockCloseDir;*/
        {

           var result= TreeNT((int)EMarkdown.HtmlBlockDir,()=>
                And(()=>  
                     HtmlBlockOpenDir()
                  && OptRepeat(()=>    
                            
                               HtmlBlockDir()
                            || And(()=>        
                                       Not(()=> HtmlBlockCloseDir() )
                                    && Any() ) )
                  && HtmlBlockCloseDir() ) ); return result;
		}
        public bool HtmlBlockOpenDiv()    /*^^HtmlBlockOpenDiv : '<' Spnl ('div' / 'DIV') Spnl HtmlAttribute* '>';*/
        {

           var result= TreeNT((int)EMarkdown.HtmlBlockOpenDiv,()=>
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

           var result= TreeNT((int)EMarkdown.HtmlBlockCloseDiv,()=>
                And(()=>  
                     Char('<')
                  && Spnl()
                  && Char('/')
                  && (    Char('d','i','v') || Char('D','I','V'))
                  && Spnl()
                  && Char('>') ) ); return result;
		}
        public bool HtmlBlockDiv()    /*^^HtmlBlockDiv : HtmlBlockOpenDiv (HtmlBlockDiv / !HtmlBlockCloseDiv .)* HtmlBlockCloseDiv;*/
        {

           var result= TreeNT((int)EMarkdown.HtmlBlockDiv,()=>
                And(()=>  
                     HtmlBlockOpenDiv()
                  && OptRepeat(()=>    
                            
                               HtmlBlockDiv()
                            || And(()=>        
                                       Not(()=> HtmlBlockCloseDiv() )
                                    && Any() ) )
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
        public bool HtmlBlockDl()    /*^^HtmlBlockDl : HtmlBlockOpenDl (HtmlBlockDl / !HtmlBlockCloseDl .)* HtmlBlockCloseDl;*/
        {

           var result= TreeNT((int)EMarkdown.HtmlBlockDl,()=>
                And(()=>  
                     HtmlBlockOpenDl()
                  && OptRepeat(()=>    
                            
                               HtmlBlockDl()
                            || And(()=>    Not(()=> HtmlBlockCloseDl() ) && Any() ) )
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
        public bool HtmlBlockFieldset()    /*^^HtmlBlockFieldset : HtmlBlockOpenFieldset (HtmlBlockFieldset / !HtmlBlockCloseFieldset .)* HtmlBlockCloseFieldset;*/
        {

           var result= TreeNT((int)EMarkdown.HtmlBlockFieldset,()=>
                And(()=>  
                     HtmlBlockOpenFieldset()
                  && OptRepeat(()=>    
                            
                               HtmlBlockFieldset()
                            || And(()=>        
                                       Not(()=> HtmlBlockCloseFieldset() )
                                    && Any() ) )
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
        public bool HtmlBlockForm()    /*^^HtmlBlockForm : HtmlBlockOpenForm (HtmlBlockForm / !HtmlBlockCloseForm .)* HtmlBlockCloseForm;*/
        {

           var result= TreeNT((int)EMarkdown.HtmlBlockForm,()=>
                And(()=>  
                     HtmlBlockOpenForm()
                  && OptRepeat(()=>    
                            
                               HtmlBlockForm()
                            || And(()=>        
                                       Not(()=> HtmlBlockCloseForm() )
                                    && Any() ) )
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
        public bool HtmlBlockH1()    /*^^HtmlBlockH1 : HtmlBlockOpenH1 (HtmlBlockH1 / !HtmlBlockCloseH1 .)* HtmlBlockCloseH1;*/
        {

           var result= TreeNT((int)EMarkdown.HtmlBlockH1,()=>
                And(()=>  
                     HtmlBlockOpenH1()
                  && OptRepeat(()=>    
                            
                               HtmlBlockH1()
                            || And(()=>    Not(()=> HtmlBlockCloseH1() ) && Any() ) )
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
        public bool HtmlBlockH2()    /*^^HtmlBlockH2 : HtmlBlockOpenH2 (HtmlBlockH2 / !HtmlBlockCloseH2 .)* HtmlBlockCloseH2;*/
        {

           var result= TreeNT((int)EMarkdown.HtmlBlockH2,()=>
                And(()=>  
                     HtmlBlockOpenH2()
                  && OptRepeat(()=>    
                            
                               HtmlBlockH2()
                            || And(()=>    Not(()=> HtmlBlockCloseH2() ) && Any() ) )
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
        public bool HtmlBlockH3()    /*^^HtmlBlockH3 : HtmlBlockOpenH3 (HtmlBlockH3 / !HtmlBlockCloseH3 .)* HtmlBlockCloseH3;*/
        {

           var result= TreeNT((int)EMarkdown.HtmlBlockH3,()=>
                And(()=>  
                     HtmlBlockOpenH3()
                  && OptRepeat(()=>    
                            
                               HtmlBlockH3()
                            || And(()=>    Not(()=> HtmlBlockCloseH3() ) && Any() ) )
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
        public bool HtmlBlockH4()    /*HtmlBlockH4 : HtmlBlockOpenH4 (HtmlBlockH4 / !HtmlBlockCloseH4 .)* HtmlBlockCloseH4;*/
        {

           var result=And(()=>  
                     HtmlBlockOpenH4()
                  && OptRepeat(()=>    
                            
                               HtmlBlockH4()
                            || And(()=>    Not(()=> HtmlBlockCloseH4() ) && Any() ) )
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
        public bool HtmlBlockH5()    /*^^HtmlBlockH5 : HtmlBlockOpenH5 (HtmlBlockH5 / !HtmlBlockCloseH5 .)* HtmlBlockCloseH5;*/
        {

           var result= TreeNT((int)EMarkdown.HtmlBlockH5,()=>
                And(()=>  
                     HtmlBlockOpenH5()
                  && OptRepeat(()=>    
                            
                               HtmlBlockH5()
                            || And(()=>    Not(()=> HtmlBlockCloseH5() ) && Any() ) )
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
        public bool HtmlBlockH6()    /*^^HtmlBlockH6 : HtmlBlockOpenH6 (HtmlBlockH6 / !HtmlBlockCloseH6 .)* HtmlBlockCloseH6;*/
        {

           var result= TreeNT((int)EMarkdown.HtmlBlockH6,()=>
                And(()=>  
                     HtmlBlockOpenH6()
                  && OptRepeat(()=>    
                            
                               HtmlBlockH6()
                            || And(()=>    Not(()=> HtmlBlockCloseH6() ) && Any() ) )
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
        public bool HtmlBlockMenu()    /*^^HtmlBlockMenu : HtmlBlockOpenMenu (HtmlBlockMenu / !HtmlBlockCloseMenu .)* HtmlBlockCloseMenu;*/
        {

           var result= TreeNT((int)EMarkdown.HtmlBlockMenu,()=>
                And(()=>  
                     HtmlBlockOpenMenu()
                  && OptRepeat(()=>    
                            
                               HtmlBlockMenu()
                            || And(()=>        
                                       Not(()=> HtmlBlockCloseMenu() )
                                    && Any() ) )
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

           var result= TreeNT((int)EMarkdown.HtmlBlockNoframes,()=>
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

           var result= TreeNT((int)EMarkdown.HtmlBlockNoscript,()=>
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
        public bool HtmlBlockOl()    /*^^HtmlBlockOl : HtmlBlockOpenOl (HtmlBlockOl / !HtmlBlockCloseOl .)* HtmlBlockCloseOl;*/
        {

           var result= TreeNT((int)EMarkdown.HtmlBlockOl,()=>
                And(()=>  
                     HtmlBlockOpenOl()
                  && OptRepeat(()=>    
                            
                               HtmlBlockOl()
                            || And(()=>    Not(()=> HtmlBlockCloseOl() ) && Any() ) )
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
        public bool HtmlBlockP()    /*^^HtmlBlockP : HtmlBlockOpenP (HtmlBlockP / !HtmlBlockCloseP .)* HtmlBlockCloseP;*/
        {

           var result= TreeNT((int)EMarkdown.HtmlBlockP,()=>
                And(()=>  
                     HtmlBlockOpenP()
                  && OptRepeat(()=>    
                            
                               HtmlBlockP()
                            || And(()=>    Not(()=> HtmlBlockCloseP() ) && Any() ) )
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
        public bool HtmlBlockPre()    /*^^HtmlBlockPre : HtmlBlockOpenPre (HtmlBlockPre / !HtmlBlockClosePre .)* HtmlBlockClosePre;*/
        {

           var result= TreeNT((int)EMarkdown.HtmlBlockPre,()=>
                And(()=>  
                     HtmlBlockOpenPre()
                  && OptRepeat(()=>    
                            
                               HtmlBlockPre()
                            || And(()=>        
                                       Not(()=> HtmlBlockClosePre() )
                                    && Any() ) )
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
        public bool HtmlBlockTable()    /*^^HtmlBlockTable : HtmlBlockOpenTable (HtmlBlockTable / !HtmlBlockCloseTable .)* HtmlBlockCloseTable;*/
        {

           var result= TreeNT((int)EMarkdown.HtmlBlockTable,()=>
                And(()=>  
                     HtmlBlockOpenTable()
                  && OptRepeat(()=>    
                            
                               HtmlBlockTable()
                            || And(()=>        
                                       Not(()=> HtmlBlockCloseTable() )
                                    && Any() ) )
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
        public bool HtmlBlockUl()    /*^^HtmlBlockUl : HtmlBlockOpenUl (HtmlBlockUl / !HtmlBlockCloseUl .)* HtmlBlockCloseUl;*/
        {

           var result= TreeNT((int)EMarkdown.HtmlBlockUl,()=>
                And(()=>  
                     HtmlBlockOpenUl()
                  && OptRepeat(()=>    
                            
                               HtmlBlockUl()
                            || And(()=>    Not(()=> HtmlBlockCloseUl() ) && Any() ) )
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
        public bool HtmlBlockDd()    /*^^HtmlBlockDd : HtmlBlockOpenDd (HtmlBlockDd / !HtmlBlockCloseDd .)* HtmlBlockCloseDd;*/
        {

           var result= TreeNT((int)EMarkdown.HtmlBlockDd,()=>
                And(()=>  
                     HtmlBlockOpenDd()
                  && OptRepeat(()=>    
                            
                               HtmlBlockDd()
                            || And(()=>    Not(()=> HtmlBlockCloseDd() ) && Any() ) )
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
        public bool HtmlBlockDt()    /*^^HtmlBlockDt : HtmlBlockOpenDt (HtmlBlockDt / !HtmlBlockCloseDt .)* HtmlBlockCloseDt;*/
        {

           var result= TreeNT((int)EMarkdown.HtmlBlockDt,()=>
                And(()=>  
                     HtmlBlockOpenDt()
                  && OptRepeat(()=>    
                            
                               HtmlBlockDt()
                            || And(()=>    Not(()=> HtmlBlockCloseDt() ) && Any() ) )
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
        public bool HtmlBlockFrameset()    /*^^HtmlBlockFrameset : HtmlBlockOpenFrameset (HtmlBlockFrameset / !HtmlBlockCloseFrameset .)* HtmlBlockCloseFrameset;*/
        {

           var result= TreeNT((int)EMarkdown.HtmlBlockFrameset,()=>
                And(()=>  
                     HtmlBlockOpenFrameset()
                  && OptRepeat(()=>    
                            
                               HtmlBlockFrameset()
                            || And(()=>        
                                       Not(()=> HtmlBlockCloseFrameset() )
                                    && Any() ) )
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
        public bool HtmlBlockLi()    /*^^HtmlBlockLi : HtmlBlockOpenLi (HtmlBlockLi / !HtmlBlockCloseLi .)* HtmlBlockCloseLi;*/
        {

           var result= TreeNT((int)EMarkdown.HtmlBlockLi,()=>
                And(()=>  
                     HtmlBlockOpenLi()
                  && OptRepeat(()=>    
                            
                               HtmlBlockLi()
                            || And(()=>    Not(()=> HtmlBlockCloseLi() ) && Any() ) )
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
        public bool HtmlBlockTbody()    /*^^HtmlBlockTbody : HtmlBlockOpenTbody (HtmlBlockTbody / !HtmlBlockCloseTbody .)* HtmlBlockCloseTbody;*/
        {

           var result= TreeNT((int)EMarkdown.HtmlBlockTbody,()=>
                And(()=>  
                     HtmlBlockOpenTbody()
                  && OptRepeat(()=>    
                            
                               HtmlBlockTbody()
                            || And(()=>        
                                       Not(()=> HtmlBlockCloseTbody() )
                                    && Any() ) )
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
        public bool HtmlBlockTd()    /*^^HtmlBlockTd : HtmlBlockOpenTd (HtmlBlockTd / !HtmlBlockCloseTd .)* HtmlBlockCloseTd;*/
        {

           var result= TreeNT((int)EMarkdown.HtmlBlockTd,()=>
                And(()=>  
                     HtmlBlockOpenTd()
                  && OptRepeat(()=>    
                            
                               HtmlBlockTd()
                            || And(()=>    Not(()=> HtmlBlockCloseTd() ) && Any() ) )
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
        public bool HtmlBlockTfoot()    /*^^HtmlBlockTfoot : HtmlBlockOpenTfoot (HtmlBlockTfoot / !HtmlBlockCloseTfoot .)* HtmlBlockCloseTfoot;*/
        {

           var result= TreeNT((int)EMarkdown.HtmlBlockTfoot,()=>
                And(()=>  
                     HtmlBlockOpenTfoot()
                  && OptRepeat(()=>    
                            
                               HtmlBlockTfoot()
                            || And(()=>        
                                       Not(()=> HtmlBlockCloseTfoot() )
                                    && Any() ) )
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
        public bool HtmlBlockTh()    /*^^HtmlBlockTh : HtmlBlockOpenTh (HtmlBlockTh / !HtmlBlockCloseTh .)* HtmlBlockCloseTh;*/
        {

           var result= TreeNT((int)EMarkdown.HtmlBlockTh,()=>
                And(()=>  
                     HtmlBlockOpenTh()
                  && OptRepeat(()=>    
                            
                               HtmlBlockTh()
                            || And(()=>    Not(()=> HtmlBlockCloseTh() ) && Any() ) )
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
        public bool HtmlBlockThead()    /*^^HtmlBlockThead : HtmlBlockOpenThead (HtmlBlockThead / !HtmlBlockCloseThead .)* HtmlBlockCloseThead;*/
        {

           var result= TreeNT((int)EMarkdown.HtmlBlockThead,()=>
                And(()=>  
                     HtmlBlockOpenThead()
                  && OptRepeat(()=>    
                            
                               HtmlBlockThead()
                            || And(()=>        
                                       Not(()=> HtmlBlockCloseThead() )
                                    && Any() ) )
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
        public bool HtmlBlockTr()    /*^^HtmlBlockTr : HtmlBlockOpenTr (HtmlBlockTr / !HtmlBlockCloseTr .)* HtmlBlockCloseTr;*/
        {

           var result= TreeNT((int)EMarkdown.HtmlBlockTr,()=>
                And(()=>  
                     HtmlBlockOpenTr()
                  && OptRepeat(()=>    
                            
                               HtmlBlockTr()
                            || And(()=>    Not(()=> HtmlBlockCloseTr() ) && Any() ) )
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

           var result= TreeNT((int)EMarkdown.HtmlBlockScript,()=>
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

           var result= TreeNT((int)EMarkdown.HtmlBlockHead,()=>
                And(()=>  
                     HtmlBlockOpenHead()
                  && OptRepeat(()=>    
                      And(()=>    Not(()=> HtmlBlockCloseHead() ) && Any() ) )
                  && HtmlBlockCloseHead() ) ); return result;
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

           var result= TreeNT((int)EMarkdown.HtmlBlockInTags,()=>
                  
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
                  || HtmlBlockHead() ); return result;
		}
        public bool HtmlBlock()    /*^^HtmlBlock : ( HtmlBlockInTags / HtmlComment / HtmlBlockSelfClosing ) 
            BlankLine+ ;*/
        {

           var result= TreeNT((int)EMarkdown.HtmlBlock,()=>
                And(()=>  
                     (    
                         HtmlBlockInTags()
                      || HtmlComment()
                      || HtmlBlockSelfClosing())
                  && PlusRepeat(()=> BlankLine() ) ) ); return result;
		}
        public bool HtmlBlockSelfClosing()    /*^^HtmlBlockSelfClosing : '<' Spnl HtmlBlockType Spnl HtmlAttribute* '/' Spnl '>';*/
        {

           var result= TreeNT((int)EMarkdown.HtmlBlockSelfClosing,()=>
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
        public bool HtmlBlockType()    /*^^HtmlBlockType : 'address' / 'blockquote' / 'center' / 'dir' / 'div' / 'dl' / 'fieldset' / 'form' / 'h1' / 'h2' / 'h3' /
                'h4' / 'h5' / 'h6' / 'hr' / 'isindex' / 'menu' / 'noframes' / 'noscript' / 'ol' / 'p' / 'pre' / 'table' /
                'ul' / 'dd' / 'dt' / 'frameset' / 'li' / 'tbody' / 'td' / 'tfoot' / 'th' / 'thead' / 'tr' / 'script' /
                'ADDRESS' / 'BLOCKQUOTE' / 'CENTER' / 'DIR' / 'DIV' / 'DL' / 'FIELDSET' / 'FORM' / 'H1' / 'H2' / 'H3' /
                'H4' / 'H5' / 'H6' / 'HR' / 'ISINDEX' / 'MENU' / 'NOFRAMES' / 'NOSCRIPT' / 'OL' / 'P' / 'PRE' / 'TABLE' /
                'UL' / 'DD' / 'DT' / 'FRAMESET' / 'LI' / 'TBODY' / 'TD' / 'TFOOT' / 'TH' / 'THEAD' / 'TR' / 'SCRIPT'/'br'/'BR';*/
        {

           var result= TreeNT((int)EMarkdown.HtmlBlockType,()=>
                OneOfLiterals(optimizedLiterals0) ); return result;
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

           var result= TreeNT((int)EMarkdown.StyleBlock,()=>
                And(()=>  
                     InStyleTags()
                  && OptRepeat(()=> BlankLine() ) ) ); return result;
		}
        public bool Inlines()    /*^^Inlines  :  StartList ( !Endline Inline 
                        / Endline &Inline  )+ Endline? ;*/
        {

           var result= TreeNT((int)EMarkdown.Inlines,()=>
                And(()=>  
                     StartList()
                  && PlusRepeat(()=>    
                            
                               And(()=>    Not(()=> Endline() ) && Inline() )
                            || And(()=>    Endline() && Peek(()=> Inline() ) ) )
                  && Option(()=> Endline() ) ) ); return result;
		}
        public bool Inline()    /*^^Inline :
		Str
        / Endline
        / UlOrStarLine
        / Space
        / Strong
        / Emph
        / Strike
        / Image
        / Link
        / NoteReference
        / InlineNote
        / Code
        / RawHtml
        / Entity
        / EscapedChar
        / Smart
        / Symbol
		;*/
        {

           var result= TreeNT((int)EMarkdown.Inline,()=>
                  
                     Str()
                  || Endline()
                  || UlOrStarLine()
                  || Space()
                  || Strong()
                  || Emph()
                  || Strike()
                  || Image()
                  || Link()
                  || NoteReference()
                  || InlineNote()
                  || Code()
                  || RawHtml()
                  || Entity()
                  || EscapedChar()
                  || Smart()
                  || Symbol() ); return result;
		}
        public bool Space()    /*Space : Spacechar+ ;*/
        {

           var result=PlusRepeat(()=> Spacechar() ); return result;
		}
        public bool Str()    /*^^Str : StartList NormalChar+  
      ( StrChunk  )*;*/
        {

           var result= TreeNT((int)EMarkdown.Str,()=>
                And(()=>  
                     StartList()
                  && PlusRepeat(()=> NormalChar() )
                  && OptRepeat(()=> StrChunk() ) ) ); return result;
		}
        public bool StrChunk()    /*^^StrChunk :  (NormalChar / '_'+  Alphanumeric)+   /
           AposChunk  ;*/
        {

           var result= TreeNT((int)EMarkdown.StrChunk,()=>
                  
                     PlusRepeat(()=>    
                            
                               NormalChar()
                            || And(()=>        
                                       PlusRepeat(()=> Char('_') )
                                    && Alphanumeric() ) )
                  || AposChunk() ); return result;
		}
        public bool AposChunk()    /*AposChunk :  '\'' Alphanumeric ;*/
        {

           var result=And(()=>    Char('\'') && Alphanumeric() ); return result;
		}
        public bool EscapedChar()    /*EscapedChar :   '\\' !Newline  [-\\`/*_{}[\]()#+.!><]   ;*/
        {

           var result=And(()=>  
                     Char('\\')
                  && Not(()=> Newline() )
                  && OneOf(optimizedCharset0) ); return result;
		}
        public bool Entity()    /*Entity :    ( HexEntity / DecEntity / CharEntity );*/
        {

           var result=    HexEntity() || DecEntity() || CharEntity(); return result;
		}
        public bool Endline()    /*Endline :   LineBreak / TerminalEndline / NormalEndline ;*/
        {

           var result=    LineBreak() || TerminalEndline() || NormalEndline(); return result;
		}
        public bool NormalEndline()    /*NormalEndline :   Sp Newline !BlankLine !'>' !AtxStart
                  !(Line (':'+ / '-'+) Newline);*/
        {

           var result=And(()=>  
                     Sp()
                  && Newline()
                  && Not(()=> BlankLine() )
                  && Not(()=> Char('>') )
                  && Not(()=> AtxStart() )
                  && Not(()=>    
                      And(()=>      
                               Line()
                            && (        
                                       PlusRepeat(()=> Char(':') )
                                    || PlusRepeat(()=> Char('-') ))
                            && Newline() ) ) ); return result;
		}
        public bool TerminalEndline()    /*TerminalEndline : Sp Newline Eof ;*/
        {

           var result=And(()=>    Sp() && Newline() && Eof() ); return result;
		}
        public bool LineBreak()    /*LineBreak : '  ' NormalEndline ;*/
        {

           var result=And(()=>    Char(' ',' ') && NormalEndline() ); return result;
		}
        public bool Symbol()    /*^^Symbol :     SpecialChar  ;
            

// This keeps the parser from getting bogged down on long strings of '*' or '_',
// or strings of '*' or '_' with space on each side:*/
        {

           var result= TreeNT((int)EMarkdown.Symbol,()=>
                SpecialChar() ); return result;
		}
        public bool UlOrStarLine()    /*UlOrStarLine :  (UlLine / StarLine)  ;*/
        {

           var result=    UlLine() || StarLine(); return result;
		}
        public bool StarLine()    /*StarLine :      '****' '*'*  / Spacechar '*'+ Spacechar   ;*/
        {

           var result=  
                     And(()=>    
                         Char('*','*','*','*')
                      && OptRepeat(()=> Char('*') ) )
                  || And(()=>    
                         Spacechar()
                      && PlusRepeat(()=> Char('*') )
                      && Spacechar() ); return result;
		}
        public bool UlLine()    /*UlLine   :       '____' '_'* / Spacechar '_'+ Spacechar  ;*/
        {

           var result=  
                     And(()=>    
                         Char('_','_','_','_')
                      && OptRepeat(()=> Char('_') ) )
                  || And(()=>    
                         Spacechar()
                      && PlusRepeat(()=> Char('_') )
                      && Spacechar() ); return result;
		}
        public bool Emph()    /*Emph :      EmphStar / EmphUl ;*/
        {

           var result=    EmphStar() || EmphUl(); return result;
		}
        public bool Whitespace()    /*Whitespace : Spacechar / Newline ;*/
        {

           var result=    Spacechar() || Newline(); return result;
		}
        public bool EmphStar()    /*EmphStar :  '*' !Whitespace
            StartList
            ( !'*' Inline 
            / StrongStar  
            )+
            '*' ;*/
        {

           var result=And(()=>  
                     Char('*')
                  && Not(()=> Whitespace() )
                  && StartList()
                  && PlusRepeat(()=>    
                            
                               And(()=>    Not(()=> Char('*') ) && Inline() )
                            || StrongStar() )
                  && Char('*') ); return result;
		}
        public bool EmphUl()    /*EmphUl :    '_' !Whitespace
            StartList
            ( !'_' Inline 
            / StrongUl  
            )+
            '_' ;*/
        {

           var result=And(()=>  
                     Char('_')
                  && Not(()=> Whitespace() )
                  && StartList()
                  && PlusRepeat(()=>    
                            
                               And(()=>    Not(()=> Char('_') ) && Inline() )
                            || StrongUl() )
                  && Char('_') ); return result;
		}
        public bool Strong()    /*^^Strong : StrongStar / StrongUl ;*/
        {

           var result= TreeNT((int)EMarkdown.Strong,()=>
                    StrongStar() || StrongUl() ); return result;
		}
        public bool StrongStar()    /*StrongStar :    '**' !Whitespace
                StartList
                ( !'**' Inline )+
                '**'  ;*/
        {

           var result=And(()=>  
                     Char('*','*')
                  && Not(()=> Whitespace() )
                  && StartList()
                  && PlusRepeat(()=>    
                      And(()=>    Not(()=> Char('*','*') ) && Inline() ) )
                  && Char('*','*') ); return result;
		}
        public bool StrongUl()    /*StrongUl   :    '__' !Whitespace
                StartList
                ( !'__' Inline )+
                '__' ;*/
        {

           var result=And(()=>  
                     Char('_','_')
                  && Not(()=> Whitespace() )
                  && StartList()
                  && PlusRepeat(()=>    
                      And(()=>    Not(()=> Char('_','_') ) && Inline() ) )
                  && Char('_','_') ); return result;
		}
        public bool Strike()    /*Strike : 
         '~~' !Whitespace
         StartList
         ( !'~~' Inline )+
         '~~' ;*/
        {

           var result=And(()=>  
                     Char('~','~')
                  && Not(()=> Whitespace() )
                  && StartList()
                  && PlusRepeat(()=>    
                      And(()=>    Not(()=> Char('~','~') ) && Inline() ) )
                  && Char('~','~') ); return result;
		}
        public bool Image()    /*^^Image : '!' ( ExplicitLink / ReferenceLink );*/
        {

           var result= TreeNT((int)EMarkdown.Image,()=>
                And(()=>  
                     Char('!')
                  && (    ExplicitLink() || ReferenceLink()) ) ); return result;
		}
        public bool Link()    /*^^Link :  ExplicitLink / ReferenceLink / AutoLink;*/
        {

           var result= TreeNT((int)EMarkdown.Link,()=>
                    ExplicitLink() || ReferenceLink() || AutoLink() ); return result;
		}
        public bool ReferenceLink()    /*^^ReferenceLink : ReferenceLinkDouble / ReferenceLinkSingle;*/
        {

           var result= TreeNT((int)EMarkdown.ReferenceLink,()=>
                    ReferenceLinkDouble() || ReferenceLinkSingle() ); return result;
		}
        public bool ReferenceLinkDouble()    /*ReferenceLinkDouble :  Label  Spnl  !'[]'  Label;*/
        {

           var result=And(()=>  
                     Label()
                  && Spnl()
                  && Not(()=> Char('[',']') )
                  && Label() ); return result;
		}
        public bool ReferenceLinkSingle()    /*ReferenceLinkSingle :  Label  (Spnl '[]')? ;*/
        {

           var result=And(()=>  
                     Label()
                  && Option(()=> And(()=>    Spnl() && Char('[',']') ) ) ); return result;
		}
        public bool ExplicitLink()    /*^^ExplicitLink :  Label '(' Sp Source Spnl  Sp ')';*/
        {

           var result= TreeNT((int)EMarkdown.ExplicitLink,()=>
                And(()=>  
                     Label()
                  && Char('(')
                  && Sp()
                  && Source()
                  && Spnl()
                  && Sp()
                  && Char(')') ) ); return result;
		}
        public bool Source()    /*^^Source  : ( '<'  SourceContents '>' /  SourceContents  );*/
        {

           var result= TreeNT((int)EMarkdown.Source,()=>
                  
                     And(()=>    Char('<') && SourceContents() && Char('>') )
                  || SourceContents() ); return result;
		}
        public bool SourceContents()    /*SourceContents : ( ( !'(' !')' !'>' Nonspacechar )+ / '(' SourceContents ')')*;*/
        {

           var result=OptRepeat(()=>  
                      
                         PlusRepeat(()=>      
                            And(()=>        
                                       Not(()=> Char('(') )
                                    && Not(()=> Char(')') )
                                    && Not(()=> Char('>') )
                                    && Nonspacechar() ) )
                      || And(()=>      
                               Char('(')
                            && SourceContents()
                            && Char(')') ) ); return result;
		}
        public bool Title()    /*^^Title : ( TitleSingle / TitleDouble  ) ;*/
        {

           var result= TreeNT((int)EMarkdown.Title,()=>
                    TitleSingle() || TitleDouble() ); return result;
		}
        public bool TitleSingle()    /*TitleSingle : '\''  ( !( '\'' Sp ( ')' / Newline ) ) . )*  '\'';*/
        {

           var result=And(()=>  
                     Char('\'')
                  && OptRepeat(()=>    
                      And(()=>      
                               Not(()=>        
                                    And(()=>          
                                                 Char('\'')
                                              && Sp()
                                              && (    Char(')') || Newline()) ) )
                            && Any() ) )
                  && Char('\'') ); return result;
		}
        public bool TitleDouble()    /*TitleDouble : '\"' ( !( '\"' Sp ( ')' / Newline ) ) . )*  '\"' ;*/
        {

           var result=And(()=>  
                     Char('\"')
                  && OptRepeat(()=>    
                      And(()=>      
                               Not(()=>        
                                    And(()=>          
                                                 Char('\"')
                                              && Sp()
                                              && (    Char(')') || Newline()) ) )
                            && Any() ) )
                  && Char('\"') ); return result;
		}
        public bool AutoLink()    /*AutoLink : AutoLinkUrl / AutoLinkEmail ;*/
        {

           var result=    AutoLinkUrl() || AutoLinkEmail(); return result;
		}
        public bool AutoLinkUrl()    /*AutoLinkUrl :   '<'  [A-Za-z]+ '://' ( !Newline !'>' . )+  '>' ;*/
        {

           var result=And(()=>  
                     Char('<')
                  && PlusRepeat(()=> In('A','Z', 'a','z') )
                  && Char(':','/','/')
                  && PlusRepeat(()=>    
                      And(()=>      
                               Not(()=> Newline() )
                            && Not(()=> Char('>') )
                            && Any() ) )
                  && Char('>') ); return result;
		}
        public bool AutoLinkEmail()    /*AutoLinkEmail : '<' ( 'mailto:' )?  [-A-Za-z0-9+_./!%~$]+ '@' ( !Newline !'>' . )+  '>';*/
        {

           var result=And(()=>  
                     Char('<')
                  && Option(()=> Char('m','a','i','l','t','o',':') )
                  && PlusRepeat(()=> OneOf(optimizedCharset1) )
                  && Char('@')
                  && PlusRepeat(()=>    
                      And(()=>      
                               Not(()=> Newline() )
                            && Not(()=> Char('>') )
                            && Any() ) )
                  && Char('>') ); return result;
		}
        public bool Reference()    /*Reference : NonindentSpace !'[]' Label ':' Spnl RefSrc RefTitle BlankLine+;*/
        {

           var result=And(()=>  
                     NonindentSpace()
                  && Not(()=> Char('[',']') )
                  && Label()
                  && Char(':')
                  && Spnl()
                  && RefSrc()
                  && RefTitle()
                  && PlusRepeat(()=> BlankLine() ) ); return result;
		}
        public bool Label()    /*^^Label : '[' ( !'^'  )
        StartList
        ( !']' Inline  )*
        ']';*/
        {

           var result= TreeNT((int)EMarkdown.Label,()=>
                And(()=>  
                     Char('[')
                  && Not(()=> Char('^') )
                  && StartList()
                  && OptRepeat(()=>    
                      And(()=>    Not(()=> Char(']') ) && Inline() ) )
                  && Char(']') ) ); return result;
		}
        public bool RefSrc()    /*RefSrc :  Nonspacechar+  ;*/
        {

           var result=PlusRepeat(()=> Nonspacechar() ); return result;
		}
        public bool RefTitle()    /*RefTitle :  ( RefTitleSingle / RefTitleDouble / RefTitleParens / EmptyTitle );*/
        {

           var result=  
                     RefTitleSingle()
                  || RefTitleDouble()
                  || RefTitleParens()
                  || EmptyTitle(); return result;
		}
        public bool EmptyTitle()    /*EmptyTitle :  '\"\"' ;*/
        {

           var result=Char('\"','\"'); return result;
		}
        public bool RefTitleSingle()    /*RefTitleSingle : Spnl '\''  ( !( '\'' Sp Newline / Newline ) . )*  '\'';*/
        {

           var result=And(()=>  
                     Spnl()
                  && Char('\'')
                  && OptRepeat(()=>    
                      And(()=>      
                               Not(()=>        
                                              
                                                 And(()=>    Char('\'') && Sp() && Newline() )
                                              || Newline() )
                            && Any() ) )
                  && Char('\'') ); return result;
		}
        public bool RefTitleDouble()    /*RefTitleDouble : Spnl '\"'  ( !('\"' Sp Newline / Newline) . )*  '\"';*/
        {

           var result=And(()=>  
                     Spnl()
                  && Char('\"')
                  && OptRepeat(()=>    
                      And(()=>      
                               Not(()=>        
                                              
                                                 And(()=>    Char('\"') && Sp() && Newline() )
                                              || Newline() )
                            && Any() ) )
                  && Char('\"') ); return result;
		}
        public bool RefTitleParens()    /*RefTitleParens : Spnl '('  ( !(')' Sp Newline / Newline) . )*  ')';*/
        {

           var result=And(()=>  
                     Spnl()
                  && Char('(')
                  && OptRepeat(()=>    
                      And(()=>      
                               Not(()=>        
                                              
                                                 And(()=>    Char(')') && Sp() && Newline() )
                                              || Newline() )
                            && Any() ) )
                  && Char(')') ); return result;
		}
        public bool References()    /*References : StartList
             ( Reference  / SkipBlock )*;*/
        {

           var result=And(()=>  
                     StartList()
                  && OptRepeat(()=>     Reference() || SkipBlock() ) ); return result;
		}
        public bool Ticks1()    /*Ticks1 : '`' !'`' ;*/
        {

           var result=And(()=>    Char('`') && Not(()=> Char('`') ) ); return result;
		}
        public bool Ticks2()    /*Ticks2 : '``' !'`' ;*/
        {

           var result=And(()=>    Char('`','`') && Not(()=> Char('`') ) ); return result;
		}
        public bool Ticks3()    /*Ticks3 : '```' !'`';*/
        {

           var result=And(()=>    Char('`','`','`') && Not(()=> Char('`') ) ); return result;
		}
        public bool Ticks4()    /*Ticks4 : '````' !'`';*/
        {

           var result=And(()=>  
                     Char('`','`','`','`')
                  && Not(()=> Char('`') ) ); return result;
		}
        public bool Ticks5()    /*Ticks5 : '`````' !'`';*/
        {

           var result=And(()=>  
                     Char('`','`','`','`','`')
                  && Not(()=> Char('`') ) ); return result;
		}
        public bool Code()    /*^^Code : ( Ticks1 Sp  ( ( !'`' Nonspacechar )+ / !Ticks1 '`'+ / !( Sp Ticks1 ) ( Spacechar / Newline !BlankLine ) )+  Sp Ticks1
       / Ticks2 Sp  ( ( !'`' Nonspacechar )+ / !Ticks2 '`'+ / !( Sp Ticks2 ) ( Spacechar / Newline !BlankLine ) )+  Sp Ticks2
       / Ticks3 Sp  ( ( !'`' Nonspacechar )+ / !Ticks3 '`'+ / !( Sp Ticks3 ) ( Spacechar / Newline !BlankLine ) )+  Sp Ticks3
       / Ticks4 Sp  ( ( !'`' Nonspacechar )+ / !Ticks4 '`'+ / !( Sp Ticks4 ) ( Spacechar / Newline !BlankLine ) )+  Sp Ticks4
       / Ticks5 Sp  ( ( !'`' Nonspacechar )+ / !Ticks5 '`'+ / !( Sp Ticks5 ) ( Spacechar / Newline !BlankLine ) )+  Sp Ticks5
       );*/
        {

           var result= TreeNT((int)EMarkdown.Code,()=>
                  
                     And(()=>    
                         Ticks1()
                      && Sp()
                      && PlusRepeat(()=>      
                                    
                                       PlusRepeat(()=>          
                                              And(()=>            
                                                             Not(()=> Char('`') )
                                                          && Nonspacechar() ) )
                                    || And(()=>          
                                                 Not(()=> Ticks1() )
                                              && PlusRepeat(()=> Char('`') ) )
                                    || And(()=>          
                                                 Not(()=> And(()=>    Sp() && Ticks1() ) )
                                              && (            
                                                             Spacechar()
                                                          || And(()=>              
                                                                           Newline()
                                                                        && Not(()=> BlankLine() ) )) ) )
                      && Sp()
                      && Ticks1() )
                  || And(()=>    
                         Ticks2()
                      && Sp()
                      && PlusRepeat(()=>      
                                    
                                       PlusRepeat(()=>          
                                              And(()=>            
                                                             Not(()=> Char('`') )
                                                          && Nonspacechar() ) )
                                    || And(()=>          
                                                 Not(()=> Ticks2() )
                                              && PlusRepeat(()=> Char('`') ) )
                                    || And(()=>          
                                                 Not(()=> And(()=>    Sp() && Ticks2() ) )
                                              && (            
                                                             Spacechar()
                                                          || And(()=>              
                                                                           Newline()
                                                                        && Not(()=> BlankLine() ) )) ) )
                      && Sp()
                      && Ticks2() )
                  || And(()=>    
                         Ticks3()
                      && Sp()
                      && PlusRepeat(()=>      
                                    
                                       PlusRepeat(()=>          
                                              And(()=>            
                                                             Not(()=> Char('`') )
                                                          && Nonspacechar() ) )
                                    || And(()=>          
                                                 Not(()=> Ticks3() )
                                              && PlusRepeat(()=> Char('`') ) )
                                    || And(()=>          
                                                 Not(()=> And(()=>    Sp() && Ticks3() ) )
                                              && (            
                                                             Spacechar()
                                                          || And(()=>              
                                                                           Newline()
                                                                        && Not(()=> BlankLine() ) )) ) )
                      && Sp()
                      && Ticks3() )
                  || And(()=>    
                         Ticks4()
                      && Sp()
                      && PlusRepeat(()=>      
                                    
                                       PlusRepeat(()=>          
                                              And(()=>            
                                                             Not(()=> Char('`') )
                                                          && Nonspacechar() ) )
                                    || And(()=>          
                                                 Not(()=> Ticks4() )
                                              && PlusRepeat(()=> Char('`') ) )
                                    || And(()=>          
                                                 Not(()=> And(()=>    Sp() && Ticks4() ) )
                                              && (            
                                                             Spacechar()
                                                          || And(()=>              
                                                                           Newline()
                                                                        && Not(()=> BlankLine() ) )) ) )
                      && Sp()
                      && Ticks4() )
                  || And(()=>    
                         Ticks5()
                      && Sp()
                      && PlusRepeat(()=>      
                                    
                                       PlusRepeat(()=>          
                                              And(()=>            
                                                             Not(()=> Char('`') )
                                                          && Nonspacechar() ) )
                                    || And(()=>          
                                                 Not(()=> Ticks5() )
                                              && PlusRepeat(()=> Char('`') ) )
                                    || And(()=>          
                                                 Not(()=> And(()=>    Sp() && Ticks5() ) )
                                              && (            
                                                             Spacechar()
                                                          || And(()=>              
                                                                           Newline()
                                                                        && Not(()=> BlankLine() ) )) ) )
                      && Sp()
                      && Ticks5() ) ); return result;
		}
        public bool RawHtml()    /*^^RawHtml :    (HtmlComment / HtmlBlockScript / HtmlTag) ;*/
        {

           var result= TreeNT((int)EMarkdown.RawHtml,()=>
                    HtmlComment() || HtmlBlockScript() || HtmlTag() ); return result;
		}
        public bool BlankLine()    /*^^BlankLine :     Sp Newline;*/
        {

           var result= TreeNT((int)EMarkdown.BlankLine,()=>
                And(()=>    Sp() && Newline() ) ); return result;
		}
        public bool Quoted()    /*Quoted :        '\"' (!'\"' .)* '\"' / '\'' (!'\'' .)* '\'';*/
        {

           var result=  
                     And(()=>    
                         Char('\"')
                      && OptRepeat(()=>      
                            And(()=>    Not(()=> Char('\"') ) && Any() ) )
                      && Char('\"') )
                  || And(()=>    
                         Char('\'')
                      && OptRepeat(()=>      
                            And(()=>    Not(()=> Char('\'') ) && Any() ) )
                      && Char('\'') ); return result;
		}
        public bool HtmlAttribute()    /*^^HtmlAttribute : (AlphanumericAscii / '-')+ Spnl (':' Spnl (Quoted / (!'>' Nonspacechar)+))? Spnl ;*/
        {

           var result= TreeNT((int)EMarkdown.HtmlAttribute,()=>
                And(()=>  
                     PlusRepeat(()=>     AlphanumericAscii() || Char('-') )
                  && Spnl()
                  && Option(()=>    
                      And(()=>      
                               Char(':')
                            && Spnl()
                            && (        
                                       Quoted()
                                    || PlusRepeat(()=>          
                                              And(()=>            
                                                             Not(()=> Char('>') )
                                                          && Nonspacechar() ) )) ) )
                  && Spnl() ) ); return result;
		}
        public bool HtmlComment()    /*^^HtmlComment :   '!--' (!'-->' .)* '-->';*/
        {

           var result= TreeNT((int)EMarkdown.HtmlComment,()=>
                And(()=>  
                     Char('!','-','-')
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
        public bool Eof()    /*Eof :          !./ WARNING<" end of file">;*/
        {

           var result=    Not(()=> Any() ) || Warning(" end of file"); return result;
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
        public bool Spnl()    /*Spnl :          Sp (Newline Sp)?;*/
        {

           var result=And(()=>  
                     Sp()
                  && Option(()=> And(()=>    Newline() && Sp() ) ) ); return result;
		}
        public bool SpecialChar()    /*^^SpecialChar :   '~' / '*' / '_' / '`' / '&' / '[' / ']' / '(' / ')' / '<' / '!' / '#' / '\\' / '\'' / '\"' / '='/ExtendedSpecialChar;*/
        {

           var result= TreeNT((int)EMarkdown.SpecialChar,()=>
                  
                     Char('~')
                  || Char('*')
                  || Char('_')
                  || Char('`')
                  || Char('&')
                  || Char('[')
                  || Char(']')
                  || Char('(')
                  || Char(')')
                  || Char('<')
                  || Char('!')
                  || Char('#')
                  || Char('\\')
                  || Char('\'')
                  || Char('\"')
                  || Char('=')
                  || ExtendedSpecialChar() ); return result;
		}
        public bool NormalChar()    /*^^NormalChar :    !( SpecialChar / Spacechar / Newline ) .;*/
        {

           var result= TreeNT((int)EMarkdown.NormalChar,()=>
                And(()=>  
                     Not(()=>     SpecialChar() || Spacechar() || Newline() )
                  && Any() ) ); return result;
		}
        public bool Alphanumeric()    /*Alphanumeric : [0-9A-Za-z]  ;*/
        {

           var result=In('0','9', 'A','Z', 'a','z'); return result;
		}
        public bool AlphanumericAscii()    /*AlphanumericAscii : [A-Za-z0-9] ;*/
        {

           var result=In('A','Z', 'a','z', '0','9'); return result;
		}
        public bool Digit()    /*Digit : [0-9];*/
        {

           var result=In('0','9'); return result;
		}
        public bool BOM()    /*BOM : ' ';*/
        {

           var result=Char(' '); return result;
		}
        public bool HexEntity()    /*HexEntity :      '&' '#' [Xx] [0-9a-fA-F]+ ';' ;*/
        {

           var result=And(()=>  
                     Char('&')
                  && Char('#')
                  && OneOf("Xx")
                  && PlusRepeat(()=> In('0','9', 'a','f', 'A','F') )
                  && Char(';') ); return result;
		}
        public bool DecEntity()    /*DecEntity :      '&' '#' [0-9]+  ';' ;*/
        {

           var result=And(()=>  
                     Char('&')
                  && Char('#')
                  && PlusRepeat(()=> In('0','9') )
                  && Char(';') ); return result;
		}
        public bool CharEntity()    /*CharEntity :     '&' [A-Za-z0-9]+ ';' ;*/
        {

           var result=And(()=>  
                     Char('&')
                  && PlusRepeat(()=> In('A','Z', 'a','z', '0','9') )
                  && Char(';') ); return result;
		}
        public bool NonindentSpace()    /*NonindentSpace :    '   ' / '  ' / ' '?;*/
        {

           var result=  
                     Char(' ',' ',' ')
                  || Char(' ',' ')
                  || Option(()=> Char(' ') ); return result;
		}
        public bool Indent()    /*Indent :            '\t' / '    ';*/
        {

           var result=    Char('\t') || Char(' ',' ',' ',' '); return result;
		}
        public bool IndentedLine()    /*IndentedLine :      Indent Line;*/
        {

           var result=And(()=>    Indent() && Line() ); return result;
		}
        public bool OptionallyIndentedLine()    /*OptionallyIndentedLine : Indent? Line;

// StartList starts a list data structure that can be added to with cons:*/
        {

           var result=And(()=>    Option(()=> Indent() ) && Line() ); return result;
		}
        public bool StartList()    /*StartList : &. ;*/
        {

           var result=Peek(()=> Any() ); return result;
		}
        public bool Line()    /*Line :  RawLine;*/
        {

           var result=RawLine(); return result;
		}
        public bool RawLine()    /*RawLine : (  (!'\r' !'\n' .)* Newline  /  .+  Eof );*/
        {

           var result=  
                     And(()=>    
                         OptRepeat(()=>      
                            And(()=>        
                                       Not(()=> Char('\r') )
                                    && Not(()=> Char('\n') )
                                    && Any() ) )
                      && Newline() )
                  || And(()=>    PlusRepeat(()=> Any() ) && Eof() ); return result;
		}
        public bool SkipBlock()    /*SkipBlock : HtmlBlock
          / ( !'#' !SetextBottom1 !SetextBottom2 !BlankLine RawLine )+ BlankLine*
          / BlankLine+
          / RawLine;

// Syntax extensions*/
        {

           var result=  
                     HtmlBlock()
                  || And(()=>    
                         PlusRepeat(()=>      
                            And(()=>        
                                       Not(()=> Char('#') )
                                    && Not(()=> SetextBottom1() )
                                    && Not(()=> SetextBottom2() )
                                    && Not(()=> BlankLine() )
                                    && RawLine() ) )
                      && OptRepeat(()=> BlankLine() ) )
                  || PlusRepeat(()=> BlankLine() )
                  || RawLine(); return result;
		}
        public bool ExtendedSpecialChar()    /*ExtendedSpecialChar :  ('.' / '-' / '\'' / '\"')
                    /  ( '^' );*/
        {

           var result=  
                     (    Char('.') || Char('-') || Char('\'') || Char('\"'))
                  || Char('^'); return result;
		}
        public bool Smart()    /*Smart : 
        ( Ellipsis / Dash / SingleQuoted / DoubleQuoted / Apostrophe );*/
        {

           var result=  
                     Ellipsis()
                  || Dash()
                  || SingleQuoted()
                  || DoubleQuoted()
                  || Apostrophe(); return result;
		}
        public bool Apostrophe()    /*Apostrophe : '\'';*/
        {

           var result=Char('\''); return result;
		}
        public bool Ellipsis()    /*Ellipsis : ('...' / '. . .');*/
        {

           var result=    Char('.','.','.') || Char('.',' ','.',' ','.'); return result;
		}
        public bool Dash()    /*Dash : EmDash / EnDash;*/
        {

           var result=    EmDash() || EnDash(); return result;
		}
        public bool EnDash()    /*EnDash : '-' Digit;*/
        {

           var result=And(()=>    Char('-') && Digit() ); return result;
		}
        public bool EmDash()    /*EmDash : ('---' / '--');*/
        {

           var result=    Char('-','-','-') || Char('-','-'); return result;
		}
        public bool SingleQuoteStart()    /*SingleQuoteStart : '\'' !(Spacechar / Newline);*/
        {

           var result=And(()=>  
                     Char('\'')
                  && Not(()=>     Spacechar() || Newline() ) ); return result;
		}
        public bool SingleQuoteEnd()    /*SingleQuoteEnd : '\'' !Alphanumeric;*/
        {

           var result=And(()=>    Char('\'') && Not(()=> Alphanumeric() ) ); return result;
		}
        public bool SingleQuoted()    /*SingleQuoted : SingleQuoteStart
               StartList
               ( !SingleQuoteEnd Inline  )+
               SingleQuoteEnd ;*/
        {

           var result=And(()=>  
                     SingleQuoteStart()
                  && StartList()
                  && PlusRepeat(()=>    
                      And(()=>    Not(()=> SingleQuoteEnd() ) && Inline() ) )
                  && SingleQuoteEnd() ); return result;
		}
        public bool DoubleQuoteStart()    /*DoubleQuoteStart : '\"';*/
        {

           var result=Char('\"'); return result;
		}
        public bool DoubleQuoteEnd()    /*DoubleQuoteEnd : '\"' ;*/
        {

           var result=Char('\"'); return result;
		}
        public bool DoubleQuoted()    /*DoubleQuoted :  DoubleQuoteStart
                StartList
                ( !DoubleQuoteEnd Inline  )+
                DoubleQuoteEnd;*/
        {

           var result=And(()=>  
                     DoubleQuoteStart()
                  && StartList()
                  && PlusRepeat(()=>    
                      And(()=>    Not(()=> DoubleQuoteEnd() ) && Inline() ) )
                  && DoubleQuoteEnd() ); return result;
		}
        public bool NoteReference()    /*NoteReference : 
                RawNoteReference;*/
        {

           var result=RawNoteReference(); return result;
		}
        public bool RawNoteReference()    /*RawNoteReference : '[^'  ( !Newline !']' . )+  ']';*/
        {

           var result=And(()=>  
                     Char('[','^')
                  && PlusRepeat(()=>    
                      And(()=>      
                               Not(()=> Newline() )
                            && Not(()=> Char(']') )
                            && Any() ) )
                  && Char(']') ); return result;
		}
        public bool Note()    /*Note :          
                NonindentSpace RawNoteReference ':' Sp
                StartList
                ( RawNoteBlock  )
                ( Indent RawNoteBlock  )*;*/
        {

           var result=And(()=>  
                     NonindentSpace()
                  && RawNoteReference()
                  && Char(':')
                  && Sp()
                  && StartList()
                  && RawNoteBlock()
                  && OptRepeat(()=>    
                      And(()=>    Indent() && RawNoteBlock() ) ) ); return result;
		}
        public bool InlineNote()    /*InlineNote :    &
                '^['
                StartList
                ( !']' Inline  )+
                ']';*/
        {

           var result=And(()=>  
                     Peek(()=> Char('^','[') )
                  && StartList()
                  && PlusRepeat(()=>    
                      And(()=>    Not(()=> Char(']') ) && Inline() ) )
                  && Char(']') ); return result;
		}
        public bool Notes()    /*Notes :         StartList
                ( Note  / SkipBlock )*;*/
        {

           var result=And(()=>  
                     StartList()
                  && OptRepeat(()=>     Note() || SkipBlock() ) ); return result;
		}
        public bool RawNoteBlock()    /*RawNoteBlock :  StartList
                    ( !BlankLine OptionallyIndentedLine  )+
                (  BlankLine*   );*/
        {

           var result=And(()=>  
                     StartList()
                  && PlusRepeat(()=>    
                      And(()=>      
                               Not(()=> BlankLine() )
                            && OptionallyIndentedLine() ) )
                  && OptRepeat(()=> BlankLine() ) ); return result;
		}
		#endregion Grammar Rules

        #region Optimization Data 
        internal static OptimizedCharset optimizedCharset0;
        internal static OptimizedCharset optimizedCharset1;
        
        internal static OptimizedLiterals optimizedLiterals0;
        
        static Markdown()
        {
            {
               char[] oneOfChars = new char[]    {'-','\\','`','/','*'
                                                  ,'_','{','}','[',']'
                                                  ,'(',')','#','+','.'
                                                  ,'!','>','<'};
               optimizedCharset0= new OptimizedCharset(null,oneOfChars);
            }
            
            {
               OptimizedCharset.Range[] ranges = new OptimizedCharset.Range[]
                  {new OptimizedCharset.Range('A','Z'),
                   new OptimizedCharset.Range('a','z'),
                   new OptimizedCharset.Range('0','9'),
                   };
               char[] oneOfChars = new char[]    {'-','+','_','.','/'
                                                  ,'!','%','~','$'};
               optimizedCharset1= new OptimizedCharset(ranges,oneOfChars);
            }
            
            
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
                  "TD","TFOOT","TH","THEAD","TR","SCRIPT","br","BR" };
               optimizedLiterals0= new OptimizedLiterals(literals);
            }

            
        }
        #endregion Optimization Data 
           }
}