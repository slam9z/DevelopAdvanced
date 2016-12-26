[Use of the Content-Disposition Header Field in the  Hypertext Transfer Protocol (HTTP)](https://tools.ietf.org/html/rfc6266)


#ietf

4.1.  Grammar

     content-disposition = "Content-Disposition" ":"
                            disposition-type *( ";" disposition-parm )

     disposition-type    = "inline" | "attachment" | disp-ext-type
                         ; case-insensitive
     disp-ext-type       = token

     disposition-parm    = filename-parm | disp-ext-parm

     filename-parm       = "filename" "=" value
                         | "filename*" "=" ext-value

     disp-ext-parm       = token "=" value
                         | ext-token "=" ext-value
     ext-token           = <the characters in token, followed by "*">



4.2.  Disposition Type

   If the disposition type matches "attachment" (case-insensitively),
   this indicates that the recipient should prompt the user to save the
   response locally, rather than process it normally (as per its media
   type).

   On the other hand, if it matches "inline" (case-insensitively), this
   implies default processing.  Therefore, the disposition type "inline"
   is only useful when it is augmented with additional parameters, such
   as the filename (see below).

   Unknown or unhandled disposition types SHOULD be handled by
   recipients the same way as "attachment" (see also [RFC2183],
   Section 2.8).

#usage

> 被浏览器缓存坑了一把！

```c#
if (!isPreview)
    {
        // attachment下载
        wrapper.ContentType = "application/octet-stream";
        context.Response.AddHeader("Content-Disposition"
            , string.Format("attachment;filename=\"{0}\"", Uri.EscapeDataString(entity.FileOriginName)));
    }
    else
    {
        //inline 预览
        wrapper.ContentType = mimeType;
        context.Response.AddHeader("Content-Disposition"
            , string.Format("inline;filename=\"{0}\"", Uri.EscapeDataString(entity.FileOriginName)));
    }
```