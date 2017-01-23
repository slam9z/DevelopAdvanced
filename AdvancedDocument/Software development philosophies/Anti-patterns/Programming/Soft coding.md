[Softcoding](https://en.wikipedia.org/wiki/Softcoding)

Softcoding is a computer coding term that refers to obtaining a value or function from some external resource, such as a preprocessor macro, external constant, configuration file, command line argument or database table. It is the opposite of hardcoding, which refers to coding values and functions in the source code.


## Potential problems

At the extreme end, soft-coded programs develop their own poorly designed and implemented scripting languages, and configuration files that require advanced programming skills to edit. This can lead to the production of utilities to assist in configuring the original program, and these utilities often end up being 'softcoded' themselves.

The boundary between proper configurability and problematic soft-coding changes with the style and nature of a program. Closed-source programs must be very configurable, as the end user does not have access to the source to make any changes. In-house software and software with limited distribution can be less configurable, as distributing altered copies is simpler. Custom-built web applications are often best with limited configurability, as altering the scripts is seldom any harder than altering a configuration file.

To avoid 'softcoding', consider the value to the end user of any additional flexibility you provide, and compare it with the increased complexity and related ongoing maintenance costs the added configurability involves.


## Achieving flexibility

Several legitimate design patterns exist for achieving the flexibility that softcoding attempts to provide. An application requiring more flexibility than is appropriate for a configuration file may benefit from the incorporation of a scripting language. In many cases, the appropriate design is a domain-specific language integrated into an established scripting language. Another approach is to move most of an application's functionality into a library, providing an API for writing related applications quickly.