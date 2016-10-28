import express = require('express');
import http = require('http');
import path = require('path');
import morgan = require('morgan');
var cookieParser = require('cookie-parser');
import session = require('express-session');
//var mongoStore = require('connect-mongo')(session); 

var bodyParser = require('body-parser');
var methodOverride = require('method-override');
var errorHandler = require('errorhandler');
var routes = require('./routes/routes')

var setting = { cookieSecret: "TYUIOHNJF", db: 'sid' };

var app = express();


//设置跨域访问
app.all('*', function (req, res, next) {
    res.header("Access-Control-Allow-Origin", "*");
    res.header("Access-Control-Allow-Headers", "Origin, X-Requested-With, Content-Type, Accept");
    res.header("Access-Control-Allow-Methods", "PUT,POST,GET,DELETE,OPTIONS");
    next();
});


// all environments
app.set('port', process.env.PORT || 3000);
//app.set('views', path.join(__dirname, 'views'));
//app.set('view engine', 'ejs');
app.use(morgan('dev'));   //app.use(express.logger('dev'));
app.use(express.static(path.join(__dirname, 'public')));
app.use(bodyParser.json());
app.use(bodyParser.json({ type: 'application/vnd.api+json' })); //app.use(express.bodyParser());   
app.use(methodOverride());
app.use(cookieParser());


import stylus = require('stylus');
app.use(stylus.middleware(path.join(__dirname, 'public')));
app.use(express.static(path.join(__dirname, 'public')));

app.use(session({
    secret: 'keyboard cat',
    resave: false,
    saveUninitialized: true
}));


// development only
if ('development' == app.get('env')) {
    app.use(errorHandler());
}

routes(app);


http.createServer(app).listen(app.get('port'), function () {
    console.log('Express server listening on port ' + app.get('port'));
});
