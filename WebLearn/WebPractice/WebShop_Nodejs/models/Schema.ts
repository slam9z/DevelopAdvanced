import mongoose = require('mongoose');
var config = require('./config');
var bdconfig = config.bdConfig;
import crypto = require('crypto');
import user = require('./UserModel');


export var UserModel = mongoose.model<user.IUser>('user', user.schema);


export class db {
    init(ignoreFailures: boolean) {
        try {
            mongoose.connect(bdconfig.connectionstring);

            var db = mongoose.connection;
            db.on('error', function (err) {
                console.error('connect to %s error: ', bdconfig.connectionstring, err.message);
                process.exit(1);
            });
            db.once('open', function () {
                console.log('%s has been connected.', bdconfig.connectionstring);
            });

        }
        catch (e) {
            if (!ignoreFailures)
            { throw e; }
        }
    }

    disconnect() { mongoose.disconnect(); }
}