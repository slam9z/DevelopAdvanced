[Worse is better](https://en.wikipedia.org/wiki/Worse_is_better)

Worse is better, also called New Jersey style, was conceived by Richard P. Gabriel in an essay "Worse is better" to describe the dynamics of software acceptance, but it has broader application. It is the idea that quality does not necessarily increase with functionality—that there is a point where less functionality ("worse") is a preferable option ("better") in terms of practicality and usability. Software that is limited, but simple to use, may be more appealing to the user and market than the reverse.

As to the oxymoronic title, Gabriel calls it a caricature, declaring the style "bad" in comparison with "The Right Thing". However he also states that "it has better survival characteristics than the-right-thing" development style and is superior to the "MIT Approach" with which he contrasted it in the original essay.[1]



[Worse is better? robbinfan](http://robbinfan.com/blog/17/worse-is-better)

> 这里的只是字面意思的理解吧


## Description

In The Rise of Worse is Better, Gabriel claimed that "Worse-is-Better" is a model of software design and implementation which has the following characteristics (in approximately descending order of importance):

### Simplicity
    The design must be simple, both in implementation and interface. It is more important for the implementation to be simple than the interface. Simplicity is the most important consideration in a design.
### Correctness
    The design should be correct in all observable aspects, but It is slightly better to be simple than correct.
### Consistency
    The design must not be overly inconsistent. Consistency can be sacrificed for simplicity in some cases, but it is better to drop those parts of the design that deal with less common circumstances than to introduce either complexity or inconsistency in the implementation.
### Completeness
    The design must cover as many important situations as is practical. All reasonably expected cases should be covered. Completeness can be sacrificed in favor of any other quality. In fact, completeness must be sacrificed whenever implementation simplicity is jeopardized. Consistency can be sacrificed to achieve completeness if simplicity is retained; especially worthless is consistency of interface. 