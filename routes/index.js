var express = require('express');
var bodyParser = require('body-parser'); //post 처리

var mysql = require('mysql');
var dbconfig = require('../config/database.js');
var router  =express.Router();

// mysql set
var conn = mysql.createConnection(dbconfig); //Connection Setting 
conn.connect(); //connection

router.use(bodyParser.json()); // for parsing application/json
router.use(bodyParser.urlencoded({ extended: true })); //post moduel use

router.get('/',function (req,res){
    conn.query('SELECT * from data', function(err, rows, fields){
        if(err){
            console.log(err);
        }else{ 
            com;    
            res.render('index',{rows: rows});
            console.log(rows);
        }
    });
});

var com = conn.query('UPDATE guest SET com = com+1 where id=1',function(err, rows, fields){
    if(err){
        console.log(err);
    }else{
        console.log(rows);
    }
});
module.exports = router;