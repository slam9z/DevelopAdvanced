[Premature optimization](https://en.wikipedia.org/wiki/Program_optimization#When_to_optimize)

Coding early-on for perceived efficiency, sacrificing good design, maintainability, and sometimes even real-world efficiency

## When to optimize

Optimization can reduce readability and add code that is used only to improve the performance. This may complicate programs or systems, making them harder to maintain and debug. As a result, optimization or performance tuning is often performed at the end of the development stage.

Donald Knuth made the following two statements on optimization:

> "We should forget about small efficiencies, say about 97% of the time: premature optimization is the root of all evil. Yet we should not pass up our opportunities in that critical 3%"[3]

(He also attributed the quote to Tony Hoare several years later,[4] although this might have been an error as Hoare disclaims having coined the phrase.[5])

> "In established engineering disciplines a 12% improvement, easily obtained, is never considered marginal and I believe the same viewpoint should prevail in software engineering"[3]

"Premature optimization" is a phrase used to describe a situation where a programmer lets performance considerations affect the design of a piece of code. This can result in a design that is not as clean as it could have been or code that is incorrect, because the code is complicated by the optimization and the programmer is distracted by optimizing.

When deciding whether to optimize a specific part of the program, Amdahl's Law should always be considered: the impact on the overall program depends very much on how much time is actually spent in that specific part, which is not always clear from looking at the code without a performance analysis.

A better approach is therefore to design first, code from the design and then profile/benchmark the resulting code to see which parts should be optimized. A simple and elegant design is often easier to optimize at this stage, and profiling may reveal unexpected performance problems that would not have been addressed by premature optimization.

In practice, it is often necessary to keep performance goals in mind when first designing software, but the programmer balances the goals of design and optimization.