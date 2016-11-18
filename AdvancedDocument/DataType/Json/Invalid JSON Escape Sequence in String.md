[Invalid JSON Escape Sequence in String](http://stackoverflow.com/questions/14139031/invalid-json-escape-sequence-in-string)

\用\\表示，基本是传统。因为转义字符。

##question

am working with a MySQL db that has encoded polygon for google maps. When I try to return the query as json, jsonlint complains.. I am not sure why its complaining , I did try escaping the "}" in the latlon but still get the same error.
Parse error on line 20:

```
...          "latlon": "}ciuF|a|pNcUr@d@es@
-----------------------^
Expecting 'STRING', 'NUMBER', 'NULL', 'TRUE', 'FALSE', '{', '['
```

My json is:

```json
{
    "maps": [
        {
            "group_id": "0",
            "user_id": "113",
            "group_name": "",
            "note": "",
            "field_id": "",
            "field_name": "West Pasture",
            "field_notes": "",
            "date_created": "12/31/2012",
            "acres": ""
        }
    ],
    "polygon": [
        {
            "polygon_id": "",
            "field_id": "1",
            "acres": "92",
            "latlon": "}ciuF|a|pNcUr@d@es@fIHXaNtCn@UxCjMlApAfFuBpI}E\ChJdEl@xAtE"
        }
    ]
}
```

##answer

The problem is that there is a slash before the C which is not a valid escape sequence.

```
"}ciuF|a|pNcUr@d@es@fIHXaNtCn@UxCjMlApAfFuBpI}E\ChJdEl@xAtE"
JSON.parse('"\\C"');
```

This will give you a syntax error because it is trying to parse the string \C. If you want a literal \ in your property's value, you need to escape it.
"latlon": "}ciuF|a|pNcUr@d@es@fIHXaNtCn@UxCjMlApAfFuBpI}E\\ChJdEl@xAtE"
The relevant section from the official grammar:

```
string
    ""
    " chars "
chars
    char
    char chars
char
    any-Unicode-character-
        except-"-or-\-or-
        control-character
    \"
    \\
    \/
    \b
    \f
    \n
    \r
    \t
    \u four-hex-digits 
```