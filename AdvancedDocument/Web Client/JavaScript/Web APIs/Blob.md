[Blob](https://developer.mozilla.org/en-US/docs/Web/API/Blob)

A Blob object represents a file-like object of immutable, raw data. Blobs represent data that isn't necessarily in a JavaScript-native format. The File interface is based on Blob, inheriting blob functionality and expanding it to support files on the user's system.

To construct a Blob from other non-blob objects and data, use the Blob() constructor. To create a blob that contains a subset of another blob's data, use the slice() method. To obtain a Blob object for a file on the user's file system, see the File documentation.

The APIs accepting Blob objects are also listed on the File documentation.


## Constructor


Blob(blobParts[, options])
    Returns a newly created Blob object whose content consists of the concatenation of the array of values given in parameter.

##　Properties


Blob.isClosed Read only
    A boolean value, indicating whether the Blob.close() method has been called on the blob. Closed blobs can not be read.
Blob.size Read only
    The size, in bytes, of the data contained in the Blob object.
Blob.type Read only
    A string indicating the MIME type of the data contained in the Blob. If the type is unknown, this string is empty.

##　Methods


Blob.close()
    Closes the blob object, possibly freeing underlying resources.
Blob.slice([start[, end[, contentType]]])
    Returns a new Blob object containing the data in the specified range of bytes of the source Blob. 


## Examples

Blob constructor example usage

The Blob() constructor allows one to create blobs from other objects. For example, to construct a blob from string:

```js
var debug = {hello: "world"};
var blob = new Blob([JSON.stringify(debug, null, 2)], {type : 'application/json'});
```