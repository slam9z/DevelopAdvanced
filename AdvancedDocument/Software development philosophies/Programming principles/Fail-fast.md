[Fail-fast](https://en.wikipedia.org/wiki/Fail-fast)

In systems design, a fail-fast system is one which immediately reports at its interface any condition that is likely to indicate a failure. Fail-fast systems are usually designed to stop normal operation rather than attempt to continue a possibly flawed process. Such designs often check the system's state at several points in an operation, so any failures can be detected early. A fail-fast module passes the responsibility for handling errors, but not detecting them, to the next-highest level of the system.

Fail-fast systems or modules are desirable in several circumstances:


* When building a fault-tolerant system by means of redundant components, the individual components should be fail-fast to give the system enough information to successfully tolerate a failure.

* Fail-fast components are often used in situations where failure in one component might not be visible until it leads to failure in another component.

* Finding the cause of a failure is easier in a fail-fast system, because the system reports the failure with as much information as possible as close to the time of failure as possible. In a fault-tolerant system, the failure might go undetected, whereas in a system that is neither fault-tolerant nor fail-fast the failure might be temporarily hidden until it causes some seemingly unrelated problem later.

* A fail-fast system that is designed to halt as well as report the error on failure is less likely to erroneously perform an irreversible or costly operation.
