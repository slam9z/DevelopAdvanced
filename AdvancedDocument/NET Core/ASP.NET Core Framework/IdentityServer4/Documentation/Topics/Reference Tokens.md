[Reference Tokens](http://docs.identityserver.io/en/release/topics/reference_tokens.html)

Access tokens can come in two flavours - self-contained or reference.

A JWT token would be a self-contained access token - it’s a protected data structure with claims and an expiration. Once an API has learned about the key material, it can validate self-contained tokens without needing to communicate with the issuer. This makes JWTs hard to revoke. They will stay valid until they expire.

When using reference tokens - IdentityServer will store the contents of the token in a data store and will only issue a unique identifier for this token back to the client. The API receiving this reference must then open a back-channel communication to IdentityServer to validate the token.


