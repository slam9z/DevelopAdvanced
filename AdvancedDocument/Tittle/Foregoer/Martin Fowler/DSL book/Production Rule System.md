[Production Rule System](http://martinfowler.com/dslCatalog/productionRule.html)

> Organize logic through a set of production rules, each having a condition and an action.

There are many situations that are easily thought of as a set of conditional tests. If you are validating some data, you can think of each validation as a condition where you raise an error if the condition is false. Qualifying for some position can often be thought of as a chain of conditions where you qualify if you make it all the way up the chain. Diagnosing a failure can be thought of a series of questions, with each question leading to new questions, and hopefully to the identification of the root fault.

The Production Rule System computational model implements the notion of a set of rules, where each rule has a condition and a consequential action. The system runs the rules on the data it has through a series of cycles, each cycle identifying the rules whose conditions match, then executes the rules' actions. A Production Rule System is usually at the heart of an expert system.

For more details see chapter 50 of the DSL book

| Catalog of DSL patterns |