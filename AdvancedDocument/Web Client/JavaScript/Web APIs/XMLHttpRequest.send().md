[XMLHttpRequest.send()](https://developer.mozilla.org/en-US/docs/Web/API/XMLHttpRequest/send)

The XMLHttpRequest.send() method sends the request. If the request is asynchronous (which is the default), this method returns as soon as the request is sent. If the request is synchronous, this method doesn't return until the response has arrived. send() accepts an optional argument for the request body. If the request method is GET or HEAD, the argument is ignored and request body is set to null.

If no Accept header has been set using the setRequestHeader(), an Accept header with the */* is sent.


Syntax

```
void send();
void send(ArrayBuffer data);
void send(ArrayBufferView data);
void send(Blob data);
void send(Document data);
void send(DOMString? data);
void send(FormData data);
```

If the data is a Document, it is serialized before being sent. When sending a Document, versions of Firefox prior to version 3 always sends the request using UTF-8 encoding; Firefox 3 properly sends the document using the encoding specified by body.xmlEncoding, or UTF-8 if no encoding is specified.

If it's an nsIInputStream, it must be compatible with nsIUploadChannel's setUploadStream()method. In that case, a Content-Length header is added to the request, with its value obtained using nsIInputStream's available()method. Any headers included at the top of the stream are treated as part of the message body. The stream's MIMEtype should be specified by setting the Content-Type header using the setRequestHeader() method prior to calling send().

The best way to send binary content (like in files upload) is using an ArrayBufferView or Blobs in conjuncton with the send() method.
Example: GET

```js
var xhr = new XMLHttpRequest();
xhr.open('GET', '/server', true);

xhr.onload = function () {
  // Request finished. Do processing here.
};

xhr.send(null);
// xhr.send('string');
// xhr.send(new Blob());
// xhr.send(new Int8Array());
// xhr.send({ form: 'data' });
// xhr.send(document);
```
