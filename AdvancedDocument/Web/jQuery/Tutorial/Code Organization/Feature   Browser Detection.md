[Feature & Browser Detection](http://learn.jquery.com/code-organization/feature-browser-detection/)

##Can I Use This Browser Feature?

There are a couple of common ways to check whether or not a particular feature is supported by a user's browser:

* Browser Detection
* Specific Feature Detection

In general, we recommend specific feature detection. Let's look at why.


##Browser Detection

###Other browsers other than your target may have the same issue.

###User Agents are unreliable.


##Specific Feature Detection


##How to go about feature detection

There are several ways to go about feature detection:

* Straight JavaScript
* A Helper Library

###Straight JavaScript

###A Helper Library

Thankfully, there are some great helper libraries (like Modernizr) that provide a simple, high-level API for
 determining if a browser has a specific feature available or not.

For example, utilizing Modernizr, we are able to do the same canvas detection test with this code:

```js
 if ( Modernizr.canvas ) {

    showGraphWithCanvas();

} else {

    showTable();

}
```


##Performance Considerations
 
