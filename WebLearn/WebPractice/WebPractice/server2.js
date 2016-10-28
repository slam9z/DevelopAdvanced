//var http = require('http');
//var url = require('url');
//var util = require('util');

//var server =new http.Server();

//server.on('request', function (req, res) {
//    //res.writeHead(200, { 'Content-Type': 'text/html' });
//    res.writeHead(200, { 'Content-Type': 'text/plain' });
//    res.write('<hl> nodejs </h1>')
//    res.write('<hl> ')
//    res.write(util.inspect(url.parse(req.url,true)));
//    res.write('</h1>')
//    res.end('<p> Hello World </p>');
//});

//server.listen(3000,'127.0.0.1');
//console.log("server run at http://127.0.0.1:3000");


var http = require('http');
var querystring = require('querystring');
var util = require('util');

http.createServer(function (req, res) {
    var post = '';
    req.on('data', function (chunk) {
        post += chunk;
    });
    req.on('end', function () {
        post = querystring.parse(post);
        res.end(util.inspect(post));
    });
}).listen(3000);
