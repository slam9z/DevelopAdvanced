[Multiton pattern](https://en.wikipedia.org/wiki/Multiton_pattern)

In software engineering, the multiton pattern is a design pattern similar to the singleton, which allows only one instance of a class to be created. The multiton pattern expands on the singleton concept to manage a map of named instances as key-value pairs.

Rather than having a single instance per application (e.g. the java.lang.Runtime object in the Java programming language) the multiton pattern instead ensures a single instance per key.

Most people and textbooks consider this a singleton pattern[citation needed]. For example, multiton does not explicitly appear in the highly regarded object-oriented programming textbook Design Patterns (it appears as a more flexible approach named registry of singletons).