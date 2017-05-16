[what is the default expiration time of a cookie](http://stackoverflow.com/questions/19002254/what-is-the-default-expiration-time-of-a-cookie)


The default Expires value for a cookie is not a static time, but it creates a Session cookie. This will stay active until the user closes their browser/clears their cookies. You can override this as required.

From the linked page:

> Setting the Expires property to MinValue makes this a session Cookie, which is its default value

