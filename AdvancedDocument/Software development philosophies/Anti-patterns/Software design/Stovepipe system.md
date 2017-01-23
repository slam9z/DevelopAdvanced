[Stovepipe system](https://en.wikipedia.org/wiki/Stovepipe_system)

In engineering and computing, "stovepipe system" is a pejorative term for a system that has the potential to share data or functionality with other systems but which does not. The term evokes the image of stovepipes rising above buildings, each functioning individually. A simple example of a stovepipe system is one that implements its own user IDs and passwords, instead of relying on a common user ID and password shared with other systems.

Stovepipes are
> “ 	systems procured and developed to solve a specific problem, characterized by a limited focus and functionality, and containing data that cannot be easily shared with other systems. 	”

A stovepipe system is generally considered an example of an anti-pattern, particularly found in legacy systems. This is due to the lack of code reuse, and resulting software brittleness due to potentially general functions only being used on limited input.

However, in certain cases stovepipe systems are considered appropriate, due to benefits from vertical integration and avoiding dependency hell.[1] For example, the Microsoft Excel team has avoided dependencies and even maintained its own C compiler, which helped it to ship on time, have high-quality code, and generate small, cross-platform code.[1]