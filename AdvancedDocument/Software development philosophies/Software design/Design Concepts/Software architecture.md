[Software architecture](https://en.wikipedia.org/wiki/Software_architecture)

Software architecture refers to the fundamental structures of a software system, the discipline of creating such structures, and the documentation of these structures. These structures are needed to reason about the software system. Each structure comprises software elements, relations among them, and properties of both elements and relations,[1] along with rationale for the introduction and configuration of each element. The architecture of a software system is a metaphor, analogous to the architecture of a building.[2]

Software architecture is about making fundamental structural choices which are costly to change once implemented. Software architecture choices, also called architectural decisions, include specific structural options from possibilities in the design of software. For example, the systems that controlled the space shuttle launch vehicle had the requirement of being very fast and very reliable. Therefore, an appropriate real-time computing language would need to be chosen. Additionally, to satisfy the need for reliability the choice could be made to have multiple redundant and independently produced copies of the program, and to run these copies on independent hardware while cross-checking results.

Documenting software architecture facilitates communication between stakeholders, captures decisions about the architecture design, and allows reuse of design components between projects

## Scope

Opinions vary as to the scope of software architectures:[4]


* Overall, macroscopic system structure;[5] this refers to architecture as a higher level abstraction of a software system that consists of a collection of computational components together with connectors that describe the interaction between these components.

* The important stuff—whatever that is;[6] this refers to the fact that software architects should concern themselves with those decisions that have high impact on the system and its stakeholders.

* That which is fundamental to understanding a system in its environment;[7] in this definition, the environment is characterized by stakeholder concerns, technical constraints, and various dimensions of project context.[8]

* Things that people perceive as hard to change;[6] since designing the architecture often takes place at the beginning of a software system's lifecycle, the architect should focus on decisions that "have to" be right the first time. Following this line of thought, architectural design issues may become non-architectural once their irreversibility can be overcome (see "Software architecture and agile development" below).

* A set of architectural design decisions;[9] software architecture should not be considered merely a set of models or structures, but should include the decisions that lead to these particular structures, and the rationale behind them (e.g., justifications, answers to "why" questions)). This insight has led to substantial research into software architecture knowledge management.[10][11]


There is no sharp distinction between software architecture versus design and requirements engineering (see Related fields below). They are all part of a "chain of intentionality" from high-level intentions to low-level details.[12](p18) This duality is also referred to as the "twin peaks" of software engineering



## Characteristics

Software architecture exhibits the following:


* Multitude of stakeholders: software systems have to cater to a variety of stakeholders such as business managers, application owners, developers, end users and infrastructure operators. These stakeholders all have their own concerns with respect to the system. Balancing these concerns and demonstrating how they are addressed is part of designing the system.[3]:pp.29–31 This implies that architecture involves dealing with a broad variety of concerns and stakeholders, and has a multidisciplinary nature. Software architect require non-technicals skills such as communication and negotiation competencies.

* Separation of concerns: the established way for architects to reduce complexity is to separate the concerns that drive the design. Architecture documentation shows that all stakeholder concerns are addressed by modeling and describing the architecture from separate points of view associated with the various stakeholder concerns.[14] These separate descriptions are called architectural views (see for example the 4+1 Architectural View Model).

* Quality-driven: classic software design approaches (e.g. Jackson Structured Programming) were driven by required functionality and the flow of data through the system, but the current insight[3]:pp.26–28 is that the architecture of a software system is more closely related to its quality attributes such as fault-tolerance, backward compatibility, extensibility, reliability, maintainability, availability, security, usability, and other such –ilities. Stakeholder concerns often translate into requirements and constraints on these quality attributes, which are variously called non-functional requirements, extra-functional requirements, behavioral requirements, or quality attribute requirements.

* Recurring styles: like building architecture, the software architecture discipline has developed standard ways to address recurring concerns. These "standard ways" are called by various names at various levels of abstraction. Common terms for recurring solutions are architectural style, principle,[12]:pp.273–277 tactic,[3]:pp.70–72 reference architecture[15][16] and architectural pattern.[3]:pp.203–205

* Conceptual integrity: a term introduced by Fred Brooks in The Mythical Man-Month to denote the idea that the architecture of a software system represents an overall vision of what it should do and how it should do it. This vision should be separated from its implementation. The architect assumes the role of "keeper of the vision", making sure that additions to the system are in line with the architecture, hence preserving conceptual integrity.[17]:pp.41–50



## Motivation

Software architecture is an "intellectually graspable" abstraction of a complex system.[3]:pp.5–6 This abstraction provides a number of benefits:


* It gives a basis for analysis of software systems' behavior before the system has been built.[2] The ability to verify that a future software system fulfills its stakeholders' needs without actually having to build it represents substantial cost-saving and risk-mitigation.[18] A number of techniques have been developed in academia and practice to perform such analyses, for instance ATAM, ARID and TARA.

* It provides a basis for re-use of elements and decisions.[2][3]:p.35 A complete software architecture or parts of it, like individual architectural strategies and decisions, can be re-used across multiple systems whose stakeholders require similar quality attributes or functionality, saving design costs and mitigating the risk of design mistakes (assuming that the project contexts[19] match).

* It supports early design decisions that impact a system's development, deployment, and maintenance life.[3]:p.31 Getting the early, high-impact decisions right is important to prevent schedule and budget overruns. On the other hand, a principle of lean software development is to defer decisions until the last responsible moment (M. and T. Poppendieck); however, it is not always clear when this moment for a particular subset of decisions has come.

* It facilitates communication with stakeholders, contributing to a system that better fulfills their needs.[3]:p.29–31 Communicating about complex systems from the point of view of stakeholders helps them understand the consequences of their stated requirements and the design decisions based on them. Architecture gives the ability to communicate about design decisions before the system is implemented, when they are still relatively easy to adapt.

* It helps in risk management. Software architecture helps to reduce risks and chance of failure.[12](p18)

* It enables cost reduction. Software architecture is a means to manage risk and costs in complex IT projects.[20]


