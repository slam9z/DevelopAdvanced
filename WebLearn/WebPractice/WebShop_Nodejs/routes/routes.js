var user = require('./user');

module.exports = function (app) {
    app.route('/api/Account/Login').post(user.login);
    app.route('/api/Account/Register').post(user.create);
};
//# sourceMappingURL=routes.js.map
