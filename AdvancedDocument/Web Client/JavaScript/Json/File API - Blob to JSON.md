[File API - Blob to JSON](http://stackoverflow.com/questions/12786818/file-api-blob-to-json)


## answer

What you're doing is conceptually wrong. JSON is a string representation of an object, not an object itself. So, when you send a binary representation of JSON over the wire, you're sending a binary representation of the string. There's no way to get around parsing JSON on the client side to convert a JSON string to a JavaScript Object.

You absolutely should always send JSON as text to the client, and you should always call JSON.parse. Nothing else is going to be easy for you.
