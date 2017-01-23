[Loop-switch sequence](https://en.wikipedia.org/wiki/Loop-switch_sequence)


A loop-switch sequence[1] (also known as the for-case paradigm[2] or Anti-Duff's Device) is a programming antipattern where a clear set of steps is implemented as a switch-within-a-loop. The loop-switch sequence is a specific derivative of spaghetti code.

It is not necessarily an antipattern to use a switch statement within a loop—it is only considered incorrect when used to model a known sequence of steps. The most common example of the correct use of a switch within a loop is an inversion of control such as an event handler. In event handler loops, the sequence of events is not known at compile-time, so the repeated switch is both necessary and correct (see event-driven programming, event loop and event-driven finite state machine).

Note that this is not a performance antipattern, though it may lead to an inconsequential performance penalty due to the lack of an unrolled loop. Rather, this is a clarity antipattern, as in any non-trivial example, it is much more difficult to decipher the intent and actual function of the code than the more straightforward refactored solution.
