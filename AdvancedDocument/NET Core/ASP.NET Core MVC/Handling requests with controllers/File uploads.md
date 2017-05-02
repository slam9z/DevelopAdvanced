[File uploads](https://docs.microsoft.com/en-us/aspnet/core/mvc/models/file-uploads)

## Uploading small files with model binding

The individual files uploaded to the server can be accessed through Model Binding using the `IFormFile` interface. IFormFile has the following structure:


## Uploading large files with streaming

If the size or frequency of file uploads is causing resource problems for the app, consider streaming the file upload rather than buffering it in its entirety, as the model binding approach shown above does. While using IFormFile and model binding is a much simpler solution, streaming requires a number of steps to implement properly.

