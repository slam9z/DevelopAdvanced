[Microsoft REST API Guidelines](https://github.com/wei772/api-guidelines)

##7.10 Response format

[7.10 Response format](https://github.com/wei772/api-guidelines/blob/master/Guidelines.md#710-response-formats)

[7.10.2 Error condition responses](https://github.com/wei772/api-guidelines/blob/master/Guidelines.md#7102-error-condition-responses)

The error response MUST be a single JSON object. This object MUST have a name/value pair named "error." The value MUST be a JSON object.

This object MUST contain name/value pairs with the names "code" and "message," and it MAY contain name/value pairs with the names "target," "details" and "innererror."
