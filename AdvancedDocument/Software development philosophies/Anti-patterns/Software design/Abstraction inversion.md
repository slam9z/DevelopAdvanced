[Abstraction inversion](https://en.wikipedia.org/wiki/Abstraction_inversion)

> 只提供接口，不提供默认实现吧

In computer programming, abstraction inversion is an anti-pattern arising when users of a construct need functions implemented within it but not exposed by its interface. The result is that the users re-implement the required functions in terms of the interface, which in its turn uses the internal implementation of the same functions. This may result in implementing lower-level features in terms of higher-level ones, thus the term 'abstraction inversion'.

Possible ill-effects are:

* The user of the construct is forced to obscure his implementation with complex mechanical details.
* The user of such a re-implemented function may seriously underestimate its running-costs.
* Many users attempt to solve the same problem, increasing the risk of error.



## Abstraction inversion in practice

	This section does not cite any sources. Please help improve this section by adding citations to reliable sources. Unsourced material may be challenged and removed. (March 2009) (Learn how and when to remove this template message)

Ways to avoid this anti-pattern include:

    For designers of lower-level software:

        If your system offers formally equivalent functions, choose carefully which to implement in terms of the other.
        Do not force unnecessarily weak constructs on your users.

    For implementers of higher-level software:

        Choose your infrastructure carefully.

