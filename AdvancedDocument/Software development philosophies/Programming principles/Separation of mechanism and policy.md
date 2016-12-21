[Separation of mechanism and policy](https://en.wikipedia.org/wiki/Separation_of_mechanism_and_policy)

> 看不懂

The separation of mechanism and policy[1] is a design principle in computer science. It states that mechanisms (those parts of a system implementation that control the authorization of operations and the allocation of resources) should not dictate (or overly restrict) the policies according to which decisions are made about which operations to authorize, and which resources to allocate.

This is most commonly discussed in the context of security mechanisms (authentication and authorization), but is actually applicable to a much wider range of resource allocation problems (e.g. CPU scheduling, memory allocation, quality of service), and the general question of good object abstraction.

Per Brinch Hansen introduced the concept of separation of policy and mechanism in operating systems in the RC 4000 multiprogramming system.[2] Artsy and Livny, in a 1987 paper, discussed an approach for an operating system design having an "extreme separation of mechanism and policy".[3][4] In a 2000 article, Chervenak et al. described the principles of mechanism neutrality and policy neutrality.[5]