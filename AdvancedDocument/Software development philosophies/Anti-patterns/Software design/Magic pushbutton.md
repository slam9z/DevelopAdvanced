[Magic pushbutton](https://en.wikipedia.org/wiki/Magic_pushbutton)

The magic pushbutton is a common anti-pattern in graphical user interfaces.[1][2]

At its core, the anti-pattern consists of a system partitioned into two parts: user interface and business logic, that are coupled through a single point, clicking the "magic pushbutton" or submitting a form of data. As it is a single point interface, this interface becomes over-complicated to implement. The temporal coupling of these units is a major problem: every interaction in the user interface must happen before the pushbutton is pressed, business logic can only be applied after the button was pressed. Cohesion of each unit also tends to be poor: features are bundled together whether they warrant this or not, simply because there is no other structured place in which to put them.

