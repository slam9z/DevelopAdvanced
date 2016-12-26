[Performance of parsers: PEG vs LALR(1) or LL(k)](http://stackoverflow.com/questions/11373644/performance-of-parsers-peg-vs-lalr1-or-llk)

## answer

Found a good answer about [Packrat vs LALR parsin](http://stackoverflow.com/a/3800681/118478). Some quotes from it:

> L(AL)R parsers are linear time parsers, too. So in theory, neither packrat nor L(AL)R parsers are "faster".

> What matters, in practice, of course, is implementation. L(AL)R state transitions can be executed in very few machine instructions ("look token code up in vector, get next state and action") so they can be extremely fast in practice.

> An observation: most language front-ends don't spend most of their time "parsing"; rather, they spend a lot of time in lexical analysis. Optimize that ..., and the parser speed won't matter much.

