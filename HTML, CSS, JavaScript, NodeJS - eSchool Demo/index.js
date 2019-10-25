const express = require('express');
const fs = require('fs');
const bodyParser = require('body-parser');
const multer = require('multer');
const dir = require('node-dir');
const db = require('./db.js');
const Sequelize = require('sequelize');

const Op = Sequelize.Op;

db.sequelize.sync().then(function () {
    console.log("Konektovano na db!");
})

const app = express();

app.use(express.static('htmlovi')); // omogućen pristup dosadašnjim dijelovima spirale
// to je i 1. zadatak spirale3

app.set('view engine', 'pug');
app.set('views', __dirname + '/views');

app.use(bodyParser.urlencoded({
    extended: true
}));
app.use(bodyParser.json());

var storage = multer.diskStorage({
    destination: function (req, file, cb) {
        cb(null, 'pdfovi/');
    },
    filename: function (req, file, cb) {
        cb(null, req.body.naziv + '.pdf');
    }
});

var upload = multer({
    storage: storage,
    fileFilter: function (req, file, cb) {
        if (!req.body.naziv) {
            req.errorValidacija = "Nije unesen naziv!";
            cb(null, false);
        } else if (file.mimetype != 'application/pdf') {
            req.errorValidacija = "Odabrani fajl nije .pdf!";
            cb(null, false);
        } else if (fs.existsSync(__dirname + '/pdfovi/' + req.body.naziv + '.pdf')) {
            req.errorValidacija = "Postoji zadatak sa istim imenom!";
            cb(null, false);
        } else cb(null, true);
    }
});

function posaljiGresku(response, stringPoruka, stringStranica) {
    response.render('greska', {
        greska: {
            poruka: stringPoruka,
            stranica: stringStranica
        }
    });
}

app.post('/addZadatak', upload.single('postavka'), function (req, res) {
    if (req.file) {
        var tabelaZadaci = db.zadatak;

        var objekat = {
            naziv: req.body.naziv,
            postavka: 'http://localhost:8080/dodaniZadaci/' + req.body.naziv + '.pdf'
        }

        tabelaZadaci.create(objekat)
            .then(() => {
                res.setHeader('Content-Type', 'application/json');
                res.send(JSON.stringify(objekat))
            })
            .catch((e) => res.send(e));
    } else {
        if (!req.errorValidacija) req.errorValidacija = "Nije odabran fajl!"; // jer fileFilter ne rijesi
        // slucaj kad je file = null
        res.render('greska', {
            greska: {
                poruka: req.errorValidacija,
                stranica: 'http://localhost:8080/addZadatak.html'
            }
        });
    }
});

app.get('/dodaniZadaci/:imeFajla', function (req, res) { // naziv se šalje sa ekstenzijom .pdf -> naziv = imeFajla.pdf
    var imeFajla = req.params.imeFajla;

    if (!(/.+\.pdf$/.test(imeFajla))) res.send("Nije zadovoljeno ime!");

    else if (fs.existsSync(__dirname + '/pdfovi/' + imeFajla)) {
        res.download(__dirname + '/pdfovi/' + imeFajla);
    } else res.send("Ne postoji .pdf sa takvim imenom");
});

app.get('/zadatak', function (req, res) {
    var naziv = req.query.naziv; // naziv se šalje bez ekstenzije .pdf -> naziv = imeFajla

    if (!naziv) res.send("Nedostaje parametar naziv!");

    else if (fs.existsSync(__dirname + '/pdfovi/' + naziv + '.pdf')) {
        res.sendFile(__dirname + '/pdfovi/' + naziv + '.pdf');
    } else res.send("Pogrešan naziv .pdf dokumenta!"); // else - zbog asinhrone metode

});

app.post('/addGodina', function (req, res) {
    if (!(req.body.nazivGod && req.body.nazivRepVje && req.body.nazivRepSpi)) {
        posaljiGresku(res, "Nisu poslani legalni podaci!", "http://localhost:8080/addGodina.html");
    }

    var tabelaGodina = db.godina;
    var objekat = {
        nazivGod: req.body.nazivGod,
        nazivRepVje: req.body.nazivRepVje,
        nazivRepSpi: req.body.nazivRepSpi
    };

    tabelaGodina.findAll({
            where: {
                nazivGod: req.body.nazivGod
            }
        })
        .then((rezultat) => {
            if (!rezultat.length) {
                tabelaGodina.create(objekat).then(() => res.redirect('/addGodina.html')).catch((e) => res.send(e));
            } else posaljiGresku(res, "Već postoji godina sa istim nazivGod!", "http://localhost:8080/addGodina.html");
        })
        .catch((e) => res.send(e))
});

app.get('/godine', function (req, res) {
    var tabelaGodina = db.godina;

    tabelaGodina.findAll().then((json) => res.json(json)).catch((e) => res.send(e))
});

app.get('/vjezbe', function (req, res ) {
    var tabelaVjezba = db.vjezba;

    tabelaVjezba.findAll().then((json) => res.json(json)).catch((e) => res.send(e))
})

app.get('/zadaci', function (req, res) {
    var tabelaZadaci = db.zadatak;

    var headeri = req.get('Accept');
    var json, xml, csv = false;

    if (!headeri) res.send("Nema Accept headera...");

    if (headeri.indexOf("application/json") != -1) json = true; // provjeri ove formate obavezno!
    else if (headeri.indexOf("text/xml") != -1 || headeri.indexOf("application/xml") != -1) xml = true;
    else if (headeri.indexOf("text/csv") != -1) csv = true;
    else{
        res.render('greska', {
            greska: {
                poruka: "Nijedan dozvoljeni Accept header nije odabran...",
                stranica: 'http://localhost:8080/zadaci'
            }
        });
    }

    tabelaZadaci.findAll({
        attributes : ['naziv', 'postavka'] 
    }).then((jsonArray) => {
        if (json) {
            vratiJSON(jsonArray, function (tmp) {
                res.type('application/json');
                res.send(tmp);
            });
        } else if (xml) {
            vratiXML(jsonArray, function (tmp) {
                res.type('application/xml');
                res.send(tmp);
            });
        } else if (csv) {
            vratiCSV(jsonArray, function (tmp) {
                res.type('text/csv');
                res.send(tmp);
            });
        }
    }).catch((e) => {
        res.render('greska', {
            greska: {
                poruka: e,
                stranica: 'http://localhost:8080/zadaci'
            }
        });
    })
});

function vratiJSON(jsonArray, callback) {
    var jsonTmp = [];
    
    for(i in jsonArray){
        jsonTmp.push({naziv : jsonArray[i].dataValues.naziv, postavka : jsonArray[i].dataValues.postavka});
    }

    callback(JSON.stringify(jsonArray));
}

function vratiXML(jsonArray, callback) {
    let string = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n" +
        "<zadaci>\n";

    for (var i in jsonArray) {
        string += "\t<zadatak>\n";
        string += "\t\t<naziv> " + jsonArray[i].dataValues.naziv + " </naziv>\n";
        string += "\t\t<postavka> " + jsonArray[i].dataValues.postavka + " </postavka>\n";
        string += "\t</zadatak>\n";
    }

    string += "</zadaci>";
    callback(string);
}

function vratiCSV(jsonArray, callback) {
    let string = "";

    for (var i in jsonArray) {

        string += jsonArray[i].dataValues.naziv + "," + jsonArray[i].dataValues.postavka;
        if (i != jsonArray.length - 1) string += "\n";
    }

    callback(string);
}

app.post('/addVjezba', function(req, res){
    if(req.body.sVjezbe){
        if(!req.body.sGodine) res.send("Pogrešni parametri...");
        var tabelaGodina = db.godina;
        var tabelaVjezba = db.vjezba;

        tabelaGodina.findOne({where : {id : req.body.sGodine}})
            .then((g) => {
                tabelaVjezba.findOne({where : {id : req.body.sVjezbe}})
                    .then((v) => {
                            g.addVjezbe(v)
                            .then(() => res.redirect('/addVjezba.html')).catch((e) => res.send(e))
                        })
                        .catch((e) => res.send(e))
            })
            .catch((e) => res.send(e));
    }
    else if(req.body.naziv && req.body.spirala != null){
        if(!req.body.sGodine) res.send("Pogrešni parametri...");

        var tabelaVjezba = db.vjezba;
        var tabelaGodina = db.godina;
        var spirala = req.body.spirala ? true : false;
        var objekat = { naziv : req.body.naziv, spirala : spirala };
        
        tabelaVjezba.findAll({
            where: {
                naziv: req.body.naziv
            }
        })
        .then((rezultat) => {
            if (!rezultat.length) {
                tabelaVjezba.create(objekat)
                    .then((v) => {
                        tabelaGodina.findOne({
                            where : {
                                id : req.body.sGodine
                            }
                        })
                            .then((g) => g.addVjezbe(v).then((x) => res.redirect('/addVjezba.html') ).catch((e) => res.send(e)))
                            .catch((e) => res.send(e));
                    })
                    .catch((e) => res.send(e));
                } else posaljiGresku(res, "Već postoji vježba sa istim nazivom!", "http://localhost:8080/addVjezba.html");
            }) 
            .catch((e) => res.send(e));
    }

    else res.send('Zalutao...');
});

app.post('/vjezba/:idVjezbe/zadatak', function (req, res){
    var tabelaVjezba = db.vjezba;
    var tabelaZadaci = db.zadatak;

    tabelaVjezba.findByPk(req.body.sVjezbe).then(function(v){
        tabelaZadaci.findByPk(req.body.sZadatak).then(function(z){
            v.addZadaci(z).then((x) => res.redirect('/addVjezba.html')).catch((e) => res.send(e));
        }).catch((e) => res.send(e));
    }).catch((e) => res.send(e));
});

app.post('/zadaciDB', function(req, res){
    var tabelaZadaci = db.zadatak;
    var idVjezbe = req.body.idVjezbe;
    
    // svi redovi tabelaZadaci koji su povezani sa vjezbom sa idVjezbe  == parametru bodya idVjezbe
    tabelaZadaci.findAll({
        attributes : ['id'],
        include : [{
            model : db.vjezba,
            as : 'vjezbe',
            where : {
                id : idVjezbe
            }
        }]
    })
    .then((json) => {
        var niz = [];

        for(i in json){ 
            niz.push(json[i].id);
        }

        if(!niz.length) niz.push(-1);
        tabelaZadaci.findAll({
            where : {
                id : {
                    [Op.notIn] : niz
                }
            }
        })
        .then((json) => res.send(json)) .catch((e) => res.send(e));
        
    })
    .catch((e) => res.send(e));
});

app.get('/godina/:idGodine/repozitoriji', function (req, res){
    var tabelaGodina = db.godina;
    var idGodine = req.params.idGodine;

    tabelaGodina.findAll({
        where : {
            id : idGodine
        },
        attributes : ['nazivRepSpi', 'nazivRepVje']
    }).then((json) => res.json(json)).catch((e) => res.json(e));
});

app.post('/student', function (req, res){
    var tabelaStudent = db.student;
    var tabelaGodina = db.godina;
    var studenti = req.body.studenti;
    var godina = req.body.godina;

    studenti = izbaciDuplePoIndexu(studenti);

    var indexi = studenti.map((student) => {
        return parseInt(student.index, 10);       // izdvajanje svih indexa novih studenata
    });

    tabelaStudent.findAll({     // ovako dobijemo sve indexe iz baze koji se nalaze u poslanom nizu
        where : {
            index : {
                [Op.in] : indexi
            }
        }
    }).then((v) => {    // poredimo sa indexima koji su poslani u postu
        var indexiDB = v.map((studentDB) => {
            return parseInt(studentDB.index, 10);
        });

        var studentiTmp = [];

        for(i in indexi){
            if(indexiDB.indexOf(indexi[i]) == -1) studentiTmp.push(studenti[i]);
        }
        
        tabelaStudent.bulkCreate(studentiTmp).then((s) => {
            tabelaGodina.findOne({
                where : {
                    id : godina
                } 
            }).then((g) => {
                g.addStudenti(s).then(() => {
                    g.addStudenti(v).then((e) => res.json({message : 'Dodano je ' + studentiTmp.length + 
                    ' novih studenata i upisano ' + studenti.length + ' na godinu ' + g.nazivGod}))
                    .catch((e) => res.json({message : 'Greška...'}))
                }).catch((e) => res.json({message : 'Greška...'}));
            }).catch((e) => res.json({message : 'Greška...'}));
        }).catch((e) => res.json({message : 'Greška...'}));
    })
    .catch((e) => res.json({message : 'Greška...'}));
});

function izbaciDuplePoIndexu(studentArr){
    let indexiCache = {};
    return studentArr.filter(x => !indexiCache[x.index] && (indexiCache[x.index] = true))
}

app.listen(8080);