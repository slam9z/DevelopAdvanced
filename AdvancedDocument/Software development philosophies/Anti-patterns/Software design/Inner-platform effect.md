[Inner-platform effect](https://en.wikipedia.org/wiki/Inner-platform_effect)


The inner-platform effect is the tendency of software architects to create a system so customizable as to become a replica, and often a poor replica, of the software development platform they are using. This is generally inefficient and such systems are often considered by William J. Brown et al. to be examples of an anti-pattern.


## Effect

It is normal for software developers to create a library of custom functions that relate to their specific project. The inner-platform effect occurs when this library expands to include general purpose functions that duplicate functionality already available as part of the programming language or platform. Since each of these new functions will generally call a number of the original functions, they tend to be slower, and if poorly coded, less reliable as well.[citation needed]

On the other hand, such functions are often created to present a simpler (and often more portable) abstraction layer on top of lower level services that either have an awkward interface, are too complex, non-portable or insufficiently portable, or simply a poor match for higher level application code.

## Appropriate uses

An inner platform can be useful for portability and privilege separation reasons—in other words, so that the same application can run on a wide variety of outer platforms without affecting anything outside a sandbox managed by the inner platform. For example, Sun Microsystems designed the Java platform to meet both of these goals.