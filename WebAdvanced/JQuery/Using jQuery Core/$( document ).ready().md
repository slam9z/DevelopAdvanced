[$( document ).ready()](http://learn.jquery.com/using-jquery-core/document-ready/)

A page can't be manipulated safely until the document is "ready." jQuery detects this state of readiness for you.
 Code included inside $( document ).ready() will only run once the page Document Object Model (DOM) is ready for 
JavaScript code to execute. Code included inside $( window ).load(function() { ... }) will run once the entire 
page (images or iframes), not just the DOM, is ready.

