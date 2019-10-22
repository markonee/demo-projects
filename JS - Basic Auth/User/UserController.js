var express = require('express');
var bodyParser = require('body-parser');
var User = require('./User');

var router = express.Router();
router.use(bodyParser.json());
router.use(bodyParser.urlencoded({extended: true}));

router.get('/', (req, res) => {
    User.find({})
        .then(data => res.status(200).json(data))
        .catch(err => res.status(500).json(err));
});

router.post('/', (req, res) => {
    const newUser = new User({
        name: req.body.name,
        username: req.body.username,
        password: req.body.password
    });

    newUser.save()
        .then(data => res.status(200).json('New line added.'))
        .catch(err => res.status(500).json(err));
})

module.exports = router;
