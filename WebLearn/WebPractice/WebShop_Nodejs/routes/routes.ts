import express = require('express');

import user = require('./user');
import shop = require('./shop');
import order = require('./order');

module.exports = function (app: express.Application) {
    app.route('/api/Account/Login').post(user.login);
    app.route('/api/Account/Register').post(user.create);
}

