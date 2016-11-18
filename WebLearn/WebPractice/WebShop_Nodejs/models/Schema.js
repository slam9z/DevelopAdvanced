var mongoose = require('mongoose');
var config = require('./config');
var bdconfig = config.bdConfig;

var user = require('./UserModel');

exports.UserModel = mongoose.model('user', user.schema);

var db = (function () {
    function db() {
    }
    db.prototype.init = function (ignoreFailures) {
        try  {
            mongoose.connect(bdconfig.connectionstring);

            var db = mongoose.connection;
            db.on('error', function (err) {
                console.error('connect to %s error: ', bdconfig.connectionstring, err.message);
                process.exit(1);
            });
            db.once('open', function () {
                console.log('%s has been connected.', bdconfig.connectionstring);
            });
        } catch (e) {
            if (!ignoreFailures) {
                throw e;
            }
        }
    };

    db.prototype.disconnect = function () {
        mongoose.disconnect();
    };
    return db;
})();
exports.db = db;
//# sourceMappingURL=Schema.js.map
