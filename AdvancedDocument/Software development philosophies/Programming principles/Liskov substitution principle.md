[Liskov substitution principle](https://en.wikipedia.org/wiki/Liskov_substitution_principle)

> 不是很懂

Substitutability is a principle in object-oriented programming that states that, in a computer program, if S is a subtype of T, then objects of type T may be replaced with objects of type S (i.e. an object of type T may be substituted with any object of a subtype S) without altering any of the desirable properties of T (correctness, task performed, etc.). More formally, the Liskov substitution principle (LSP) is a particular definition of a subtyping relation, called (strong) behavioral subtyping, that was initially introduced by Barbara Liskov in a 1987 conference keynote address titled Data abstraction and hierarchy. It is a semantic rather than merely syntactic relation because it intends to guarantee semantic interoperability of types in a hierarchy, object types in particular. Barbara Liskov and Jeannette Wing formulated the principle succinctly in a 1994 paper as follows:

> Subtype Requirement: Let ϕ ( x ) {\displaystyle \phi (x)} \phi (x) be a property provable about objects x {\displaystyle x} x of type T. Then ϕ ( y ) {\displaystyle \phi (y)} {\displaystyle \phi (y)} should be true for objects y {\displaystyle y} y of type S where S is a subtype of T.

In the same paper, Liskov and Wing detailed their notion of behavioral subtyping in an extension of Hoare logic, which bears a certain resemblance to Bertrand Meyer's Design by Contract in that it considers the interaction of subtyping with preconditions, postconditions and invariants.