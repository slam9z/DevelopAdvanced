var mongoose = require('mongoose');
var config = require('./config');
var bdconfig = config.bdConfig;

var log = require('./../libs/log');

mongoose.connect(bdconfig.connectionstring);

var db = mongoose.connection;
db.on('error', function (err) {
    console.error('connect to %s error: ', bdconfig.connectionstring, err.message);
    process.exit(1);
});
db.once('open', function () {
    log.success('%s has been connected.', bdconfig.connectionstring);
});
//# sourceMappingURL=db.js.map
