var express = require('express');
var morgan = require('morgan');
var path = require('path');
var bodyParser = require('body-parser');
var methodOverride = require('method-override');
var errorHandler = require('errorhandler');
var cookieParser = require('cookie-parser');

var app = express();

//app.engine('html', require('jade').renderFile);  error
app.engine('html', require('ejs').renderFile);

app.set('port', process.env.PORT || 3000);
app.set('views', path.join(__dirname, 'views'));
app.set('view engine', 'html');

app.use(morgan('dev'));   //app.use(express.logger('dev'));
app.use(express.static(path.join(__dirname, 'public')));
app.use(bodyParser.json());
app.use(bodyParser.json({ type: 'application/vnd.api+json' })); //app.use(express.bodyParser());   
app.use(methodOverride());

if ('development' == app.get('env')) {
    app.use(errorHandler());

}

app.get('/', function (req, res) {
    res.render('./index.html');
    //res.renderfile('./views/index.html');
}
    );

//app.use(function (req, res) {
//    res.send('Hello');
//});

app.listen(app.get('port'));
console.info("listen in 3000 port");



