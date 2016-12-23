[Front controller](https://en.wikipedia.org/wiki/Front_controller)

The front controller software design pattern is listed in several pattern catalogs and related to the design of Web applications. It is "a controller that handles all requests for a Web site",[1] which is a useful structure for Web application developers to achieve the flexibility and reuse without code redundancy.


## Participants and responsibilities


### Controller 	

The controller is an entrance for users to handle requests in the system. It realizes authentication by playing the role of delegating helper or initiate contact retrieval. 	Dispatchers can be used for navigation and managing the view output. Users will receive next view that is determined by the 

### Dispatcher 	

dispatcher. Dispatchers are also flexible: they can be encapsulated within the controller directly or separated to another component. The dispatcher provides a static view along with the dynamic mechanism.

It also uses the RequestDispatcher object (supported in the servlet specification) and encapsulates some additional processing.

### Helper 	

A helper helps view or controller to process. Thus helper can achieve various goals.

### View

At view side, the helper collects data and sometimes stores data as an intermediate station. Before view's process, helpers serve to adapt the data model for it. Helpers do certain pre-processes such as formatting the data to Web content or providing direct access to the raw data. Multiple helpers can collaborate with one view for most conditions. They are implemented as JavaBeans components in JSP 1.0+ and custom tags in JSP 1.1+. Additionally, a helper also works as a transformer that is used to adapt and convert the model into the suitable format.
With the collaboration of helpers, view displays information to the client. It processes data from a model. The view will display if the processing succeeds and vice versa


## Comparison

Page controller is an alternative to front controller in MVC model.

|	|Page Controller |	Front Controller|
|-------|------|------|
|Base class 	|Base class is needed and will grow simultaneously with the development of the application. 	|The centralization of solving all requests is easier to modify than base class method.|
|Security 	|Low security because various objects react differently without consistency. 	|High. The controller is implemented in coordinated fashion, making the application safer.|
|Logical Page 	|Single object on each logical page. |	Only one controller handles all requests.|
|Complexity 	|Low |	High|

> 用的算是Page Controller与	Front Controller的结合体，不够比较丑。