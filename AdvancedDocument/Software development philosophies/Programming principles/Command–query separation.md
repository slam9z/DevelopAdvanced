[Command–query separation](https://en.wikipedia.org/wiki/Command%E2%80%93query_separation)


Command–query separation (CQS) is a principle of imperative computer programming. It was devised by Bertrand Meyer as part of his pioneering work on the Eiffel programming language.

It states that every method should either be a command that performs an action, or a query that returns data to the caller, but not both. In other words, Asking a question should not change the answer.[1] More formally, methods should return a value only if they are referentially transparent and hence possess no side effects.

## Connection with design by contract


## Broader impact on software engineering


## Command query responsibility segregation


## Drawbacks

CQS can make it more difficult to implement re-entrant and multi-threaded software correctly. 