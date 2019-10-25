const Sequelize = require('sequelize');

module.exports = function(sequelize, DataTypes){
    const student = sequelize.define('student',{
        imePrezime : Sequelize.STRING, 
        index : { type : Sequelize.STRING, unique : true }
    });

    return student;
}