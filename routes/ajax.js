var express = require('express');
var bodyParser = require('body-parser'); //post 처리

var mysql = require('mysql');
var dbconfig = require('../config/database.js');
var router  =express.Router();

// mysql set
var conn = mysql.createConnection(dbconfig); //Connection Setting 
conn.connect(); //connection

router.get('/',function (req,res){
    conn.query('DESC data', function(err, rows, fields){
        if(err){
            console.log(err);
        }else{ 
            console.log(rows);
        }
    });
});

module.exports = router;