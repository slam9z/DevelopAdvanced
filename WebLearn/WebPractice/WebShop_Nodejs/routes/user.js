var schema = require("./../models/Schema");
var UserModel = schema.UserModel;
var storage = new schema.db();
storage.init(false);

exports.list = function (req, res) {
    res.send("respond with a resource");
};

function create(req, res) {
    var createUser = new UserModel(req.body);
    createUser.username = req.body.UserName;
    createUser.password = req.body.Password;
    createUser.email = req.body.Email;

    UserModel.findOne({ username: req.body.username }, function (err, user) {
        if (err)
            return res.json({ err: err });
        if (user) {
            return res.json({ err: "用户名已经存在" });
        }
        createUser.save(function (err, user) {
            if (err) {
                return res.json({ err: err });
            }
            req.session["user"] = user;
            res.json({});
        });
    });
}
exports.create = create;
;

function login(req, res) {
    UserModel.findOne({ username: req.body.UserName }, function (err, user) {
        if (err)
            return res.json({ err: err });
        if (!user) {
            return res.json({ err: '用户名不存在' });
        }
        if (!user.authenticate(req.body.Password))
            return res.json({ err: '密码错误' });

        req.session["user"] = user;

        res.json(user);
    });
}
exports.login = login;
;

exports.logout = function (req, res) {
    req.session["user"] = null;
};
//# sourceMappingURL=user.js.map
