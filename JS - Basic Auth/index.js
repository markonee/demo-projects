var express = require('express');
var app = express();
var UserController = require('./User/UserController');
var AuthController = require('./Auth/AuthController');
var db = require('./db');

let port = process.env.PORT || 3000;

app.use('/users', UserController);
app.use('/auth', AuthController);

app.listen(port, () => {
    console.log('Server at port ', port);
})

module.exports = app;
