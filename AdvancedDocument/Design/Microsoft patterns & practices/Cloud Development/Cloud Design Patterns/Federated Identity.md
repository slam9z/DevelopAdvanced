[Federated Identity](https://docs.microsoft.com/en-us/azure/architecture/patterns/federated-identity)


## Context and problem

Users typically need to work with multiple applications provided and hosted by different organizations they have a business relationship with. These users might be required to use specific (and different) credentials for each one. This can:


* Cause a disjointed user experience. Users often forget sign-in credentials when they have many different ones.

* Expose security vulnerabilities. When a user leaves the company the account must immediately be deprovisioned. It's easy to overlook this in large organizations.

* Complicate user management. Administrators must manage credentials for all of the users, and perform additional tasks such as providing password reminders.

Users typically prefer to use the same credentials for all these applications.



