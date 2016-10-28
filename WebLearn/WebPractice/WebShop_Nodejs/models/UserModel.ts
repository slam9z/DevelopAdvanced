import mongoose = require('mongoose');
import crypto = require('crypto');

var Schema = mongoose.Schema;
export var schema = new Schema({
    username: String,
    hash_password: String,
    sex: Number,
    email: String,
    phone: String,
    address: String
});

schema.virtual("password").set(function (password) {
    this.hash_password = encryptPassword(password);
});

schema.method("authenticate", function (plainText) {
    return encryptPassword(plainText) === this.hash_password;
});


function encryptPassword(password) {
    return crypto.createHash("md5").update(password).digest("base64");
}

export interface IUser extends mongoose.Document{
    _id: string
    username: string;
    password: string;
    sex: string;
    email: string;
    phone: string;
    address: string;
    authenticate(plainText:string);

}

