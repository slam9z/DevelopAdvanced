[The Packrat Parsing and Parsing Expression Grammars Page](http://bford.info/packrat/)


# The Packrat Parsing and 

Parsing Expression Grammars Page

</center>

### Introduction

_Parsing expression grammars_ (PEGs) are
an alternative to context free grammars for formally specifying syntax, and
_packrat parsers_ are parsers for PEGs
that operate in guaranteed linear time through the use of memoization.
For a brief technical summary see
[
the Wikipedia entry on PEGs](http://en.wikipedia.org/wiki/Parsing_expression_grammar).
For more in-depth descriptions see the original
[PEG paper](/pub/lang/peg) and
[packrat parsing paper](/pub/lang/packrat-icfp02),
and related papers below.
Bryan Ford, the maintainer of this site,
coined the modern terms PEGs and packrat parsing,
but much of their formal theory existed earlier
and all the more recent work on this topic is by others.

### Mailing List

The following mailing list is open to anyone
wishing to announce software or papers related to PEGs and packrat parsing,
or discuss ideas and issues:

> **[
> The PEG Mailing List](https://lists.csail.mit.edu/mailman/listinfo/peg)**

### PEG/Packrat Parsing Bibliography

The following research papers
develop and explore PEGs and packrat parsing in detail;
most were written by various authors working independently.
The papers are listed chronologically starting from most recent.
Some of the papers have associated code available online.

*   [From Regular Expressions to Parsing Expression Grammars](http://www.dcomp.ufs.br/~sergio/docs/repeg.pdf).
	[Sérgio Medeiros](http://www.dcomp.ufs.br/~sergio/),
	Fabio Mascarenhas, and
	[Roberto Ierusalimschy](http://www.inf.puc-rio.br/%7Eroberto/docs/peg.pdf).
	[SBLP](http://www.each.usp.br/cbsoft2011/ingles/sblp/sblp_en.html),
	September 2011.

*   [Parsing Expression Grammars for Structured Data](http://www.dcomp.ufs.br/~sergio/docs/peglist.pdf).
	Fabio Mascarenhas,
	 [Sérgio Medeiros](http://www.dcomp.ufs.br/~sergio/),
	and
	[Roberto Ierusalimschy](http://www.inf.puc-rio.br/%7Eroberto/docs/peg.pdf).
	[SBLP](http://www.each.usp.br/cbsoft2011/ingles/sblp/sblp_en.html),
	September 2011.

*   **[
	_LL(*)_: The Foundation of the ANTLR Parser Generator](http://dl.acm.org/citation.cfm?id=1993548)**.
	[Terence Parr](http://www.cs.usfca.edu/~parrt/) and
	[Kathleen Fisher](http://www.cs.tufts.edu/~kfisher/Kathleen_Fisher/Home.html),
	[PLDI](http://pldi11.cs.utah.edu/),
	June 2011.

*   **[
	TRX: A Formally Verified Parser Interpreter](http://www.lmcs-online.org/ojs/viewarticle.php?id=708)
	([PDF](http://arxiv.org/pdf/1105.2576))**.
	[Logical Methods in Computer Science](http://www.lmcs-online.org/),
	June 2011.

*   **[
	BITES instead of FIRST for Parsing Expression Grammar (PDF)](http://www.romanredz.se/papers/FI2011.pdf)**.
	[Roman R. Redziejowski](http://www.romanredz.se/),
	Fundamenta Informaticae 109(3),
	2011.

*   **[Direct Left-Recursive Parsing Expression Grammars](http://tratt.net/laurie/research/publications/html/tratt__direct_left_recursive_parsing_expression_grammars/)
	([PDF](http://tratt.net/laurie/research/publications/papers/tratt__direct_left_recursive_parsing_expression_grammars.pdf))**.
	[Laurence Tratt](http://tratt.net/laurie/),
	Technical report EIS-10-01, Middlesex University,
	October 2010.

*   **[Converting regexes to Parsing Expression Grammars
		(PDF)](http://www.inf.puc-rio.br/%7Eroberto/docs/ry10-01.pdf)**.
	Marcelo Oikawa,
	[Roberto Ierusalimschy](http://www.inf.puc-rio.br/%7Eroberto/docs/peg.pdf), and Ana Lúcia de Moura.
	[SBLP](http://wiki.dcc.ufba.br/CBSOFT/SBLP2010),
	September 2010.

*   **[Packrat Parsers Can Handle Practical Grammars in Mostly Constant Space (PDF)](http://ialab.cs.tsukuba.ac.jp/~mizusima/publications/paste513-mizushima.pdf)**.
	Kota Mizushima, Atusi Maeda, and Yoshinori Yamaguchi.
	[PASTE](http://cseweb.ucsd.edu/paste2010/program.html),
	June 2010.

*   **[
	Mouse: From Parsing Expressions to a Practical Parser (PDF)](http://www.romanredz.se/papers/CSP2009.Mouse.pdf)**.
	[Roman R. Redziejowski](http://www.romanredz.se/),
	[CS&amp;P 2009](http://csp2009.mimuw.edu.pl/),
	September 2009.

*   **[
	Applying classical concepts to Parsing Expression Grammar (PDF)](http://www.romanredz.se/papers/FI2009.pdf)**.
	[Roman R. Redziejowski](http://www.romanredz.se/),
	Fundamenta Informaticae 93(1-3),
	2009.

*   **[A Text Pattern-Matching Tool based on
		Parsing Expression Grammars (PDF)](http://www.inf.puc-rio.br/%7Eroberto/docs/peg.pdf)**.
	[Roberto Ierusalimschy](http://www.inf.puc-rio.br/~roberto/),
	Software: Practice and Experience 39(3),
	March 2009.

*   **[Packrat Parsing in Scala (PDF)](http://scala-programming-language.1934581.n4.nabble.com/attachment/1956909/0/packrat_parsers.pdf)**.
	Project Report, Manohar Jonnalagedda, EPFL,
	January 2009.

*   **[
	Some Aspects of Parsing Expression Grammar (PDF)](http://www.romanredz.se/papers/FI2008.pdf)**.
	[Roman R. Redziejowski](http://www.romanredz.se/),
	Fundamenta Informaticae 85(1-4),
	2008.

*   **[
	A Parsing Machine for PEGs (PDF)](http://www.inf.puc-rio.br/~roberto/docs/ry08-4.pdf)**.
	Sérgio Medeiros and Roberto Ierusalimschy.
	[DLS](http://www.dynamic-languages-symposium.org/dls-08/index.html),
	July 2008.

*   [**
	Packrat parsers can support left recursion (PDF)**](http://www.cs.ucla.edu/~todd/research/pepm08.pdf).
	[Alessandro Warth](http://tinlizzie.org/~awarth/),
	James R. Douglass, and
	[Todd Millstein](http://www.cs.ucla.edu/~todd/),
	[PEPM '08](http://www.program-transformation.org/PEPM08),
	January 2008.

*   [**
	DCGs + Memoing = Packrat Parsing: But is it worth it?**](http://www.mercury.csse.unimelb.edu.au/information/papers/packrat.pdf)
	[Ralph Becket](http://www.cs.mu.oz.au/~rafe/) and
	[Zoltan Somogyi](http://www.cs.mu.oz.au/~zs/),
	[PADL '08](http://www.ist.unomaha.edu/padl2008/),
	January 2008.

*   **[
	OMeta: an Object-Oriented Language for Pattern Matching (PDF)](http://tinlizzie.org/~awarth/papers/dls07.pdf)**.
	[Alessandro Warth](http://tinlizzie.org/~awarth/) and
	[Ian Piumarta](http://piumarta.com/cv/bio.html),
	[DLS 2007](http://www.dynamic-languages-symposium.org/dls-07/index.html),
	October 2007.

*   **[
		A Programming Language Where the Syntax and Semantics
		Are Mutable at Runtime](http://www.chrisseaton.com/katahdin/)**
	([PDF](http://www.chrisseaton.com/katahdin/katahdin-thesis.pdf)).
	[Chris Seaton](http://www.chrisseaton.com/),
	Master's thesis, University of Bristol, May 2007.

*   **[
	Parsing Expression Grammar as a primitive recursive-descent parser
	with backtracking (PDF)](http://www.romanredz.se/papers/FI2007.pdf)**
	[Roman Redziejowski](http://www.romanredz.se/),
	[CS&amp;P'2006](http://www2.informatik.hu-berlin.de/ki/CSP2006/),
	September 2006.

*   **[
	Modular Syntax Demands Verification (PDF)](http://charybde.homeunix.org/~schmitz/pub/modular.pdf)**.
	[Sylvain Schmitz](http://charybde.homeunix.org/~schmitz/),
	Tech Report I3S/RR-2006-32-FR,
	Université de Nice,
	October 2006.

*   **[
	Better Extensibility through Modular Syntax (PDF)](http://www.cs.nyu.edu/rgrimm/papers/pldi06.pdf)**.
	[Robert Grimm](http://www.cs.nyu.edu/rgrimm/),
	[PLDI](http://research.microsoft.com/en-us/um/redmond/events/pldi06/),
	June 2006.

*   **[Parsing Expression Grammars:
		A Recognition-Based Syntactic Foundation](/pub/lang/peg)**.
	([PDF](/pub/lang/peg.pdf)).
	[Bryan Ford](http://bford.info/),
	[POPL](http://www.cs.princeton.edu/~dpw/popl/04/),
	January 2004.

*   **[Packrat Parsing:
		Simple, Powerful, Lazy, Linear Time](/pub/lang/packrat-icfp02)**.
	[Bryan Ford](http://bford.info/),
	([PDF](/pub/lang/packrat-icfp02.pdf)).
	[ICFP](http://icfp2002.cs.brown.edu/),
	October 2002.

*   **[Packrat Parsing:
		a Practical Linear-Time Algorithm with Backtracking](/pub/lang/thesis)**
	([PDF](/pub/lang/thesis.pdf)).
	[Bryan Ford](http://bford.info/),
	Master's thesis, MIT, September 2002.

*   **[Parsing algorithms with backtrack](http://ieeexplore.ieee.org/xpls/abs_all.jsp?arnumber=4569646)**.
	Alexander Birman and Jeffrey D. Ullman,
	[
	Information and Control](http://theory.lcs.mit.edu/~iandc/), 23(1):1-34, August 1973.

*   **[The TMG Recognition Schema (PDF)](ref/birman70tmg.pdf)**.
	Alexander Birman,
	Ph.D. dissertation, Princeton, February 1970.

### PEG-related Projects

The following projects have implemented PEG parsers, parser generators,
and/or combinator libraries in a variety of languages:


* C#:

    * [NPEG](http://www.codeplex.com/NPEG) is a library
	providing objects to build PEGs incrementally in C#.

    * [IronMeta](http://ironmeta.sourceforge.net/)
	is a C# implementation of
	[OMeta](http://cs.ucla.edu/~awarth/ometa/).


### Sample PEGs

This section contains pointers to some "nontrivial" grammars
expressed as PEGs or PEG-like languages:

*   [xtc](http://www.cs.nyu.edu/rgrimm/xtc)
	contains modular _Rats!_ grammars for C and Java.

*   [Mouse](http://www.romanredz.se/freesoft.htm)
	includes grammars for Java and C.

*   [parboiled](http://www.parboiled.org/)
	includes a grammar for Java.

*   [PEG.js](http://pegjs.majda.cz/)
	includes a grammar for JavaScript/ECMAScript.

* * *
Maintainer: [Bryan Ford](http://bford.info/).
Additions or corrections to this page are welcome —
with apologies if I sometimes take a long time to respond!

* * *

