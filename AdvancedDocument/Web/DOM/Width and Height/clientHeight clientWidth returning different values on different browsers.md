[clientHeight/clientWidth returning different values on different browsers.md](http://stackoverflow.com/questions/833699/clientheight-clientwidth-returning-different-values-on-different-browsers)

##Question

Properties document.body.clientHeight and document.body.clientWidth return different values on IE7, IE8 and Firefox:
IE 8: 
document.body.clientHeight : 704 
document.body.clientWidth  : 1148
IE 7: 
document.body.clientHeight : 704 
document.body.clientWidth  : 1132
FireFox: 
document.body.clientHeight : 620 
document.body.clientWidth  : 1152
Why does this discrepancy exist?
Are there any equivalent properties that are consistent across different browsers (IE8, IE7, FireFox)?