const Sequelize = require('sequelize');
const sequelize = new Sequelize('wt2018', 'root', 'root', { host:'127.0.0.1', dialect:'mysql', logging:false});

const db = {};

db.Sequelize = Sequelize;
db.sequelize = sequelize;

db.student = sequelize.import(__dirname + '/student.js');
db.godina = sequelize.import(__dirname + '/godina.js');
db.zadatak = sequelize.import(__dirname + '/zadatak.js');
db.vjezba = sequelize.import(__dirname + '/vjezba.js');

// 1-n godina <-> student
db.godina.hasMany(db.student, {foreignKey : 'studentGod', as:'studenti'}); 

// m-n godina <-> vjezba
db.godina_vjezba = db.vjezba.belongsToMany(db.godina, {as : 'godine', through : 'godina_vjezba', foreignKey : 'idvjezba'});
db.godina.belongsToMany(db.vjezba, {as : 'vjezbe', through : 'godina_vjezba', foreignKey : 'idgodina'});

// m-m vjezba <-> zadatak
db.vjezba_zadatak = db.zadatak.belongsToMany(db.vjezba, {as : 'vjezbe', through : 'vjezba_zadatak', foreignKey : 'idzadatak'});
db.vjezba.belongsToMany(db.zadatak, {as : 'zadaci', through : 'vjezba_zadatak', foreignKey : 'idvjezba'});

module.exports = db;