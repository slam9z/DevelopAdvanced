var mongoose = require('mongoose');
var crypto = require('crypto');

var Schema = mongoose.Schema;
exports.schema = new Schema({
    username: String,
    hash_password: String,
    sex: Number,
    email: String,
    phone: String,
    address: String
});

exports.schema.virtual("password").set(function (password) {
    this.hash_password = encryptPassword(password);
});

exports.schema.method("authenticate", function (plainText) {
    return encryptPassword(plainText) === this.hash_password;
});

function encryptPassword(password) {
    return crypto.createHash("md5").update(password).digest("base64");
}
//# sourceMappingURL=UserModel.js.map
