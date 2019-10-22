var express = require('express');
var bodyParser = require('body-parser');
var User = require('../User/User');

var router = express.Router();
router.use(bodyParser.json());
router.use(bodyParser.urlencoded({extended: true}));

// auth modules & config
var jwt = require('jsonwebtoken');
var bcrypt = require('bcryptjs');
var config = require('./config');

const expTime = 7200;

router.post('/register', (req, res) => {
    let hashedPw = bcrypt.hashSync(req.body.password, 8);

    const newUser = new User({
        name: req.body.name,
        username: req.body.username,
        password: hashedPw
    });

    newUser.save()
        .then(data => {
            let token = jwt.sign({user_id: data._id}, config.secret, {
                expiresIn: expTime
            })

            if(token) res.status(200).json({auth: true, token: token, expiresIn: expTime})
        })
        .catch(err => res.status(500).json({auth:false, msg: err}))
})

router.post('/verify', (req, res) => {
    let authHeader = req.get('Authorization');
    // Authorization: 'Bearer token'
    if(!authHeader) res.status(500).json({auth: false, msg: 'Authorization header not provided.'});

    const token = authHeader.substring(authHeader.indexOf(' ') + 1, authHeader.length);
    
    jwt.verify(token, config.secret, (err, decoded) => {
        if(err) res.status(500).json({auth: false, msg: err});

        else {
            User.findById(decoded.user_id, {password: 0})
                .then(data => res.status(200).json({auth: true, user: data}))
                .catch(err => res.status(404).json({auth: false, msg: 'User not found.'}))
        }
    });
});

router.post('/login', (req, res) => {
    User.find({ username: req.body.username })
        .then(data => {
            const matchingPassword = bcrypt.compareSync(req.body.password, data[0].password);
            if(!matchingPassword) res.status(500).json({
                auth: false, 
                msg: 'Wrong password.'
            });

            else {
                let token = jwt.sign({ user_id: data._id }, config.secret, {
                    expiresIn: expTime
                });

                res.status(200).json({ auth: true, token: token, expiresIn: expTime })
            }
        })
        .catch(err =>
            res.status(404).json({ auth: false, msg: 'User not found.' })
        )
});

module.exports = router;